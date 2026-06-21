using Arborist.Core;
using Arborist.Linq.Extensions;
using System;
using System.Collections.Generic;

namespace Arborist.Linq.Treenumerators
{
  // SPIKE (spike-dft-extraction): clean-room DFT Where via stream REWRITE ("extraction"),
  // NOT the production collapse-then-reconcile approach.
  //
  // Thesis under test: extraction is a clean primitive. Instead of telling the inner engine to
  // SkipNode (which collapses a promoted brood into one parent visit-slot at raw positions) and
  // then reconciling, we consume the FULL inner DFT stream (always TraverseAll) and:
  //   * suppress extracted nodes' own scheduling events,
  //   * REASSIGN an extracted node's interleave visits to its nearest accepted ancestor
  //     (the inner stream already contains those visits, at the right time -> no defer/cache),
  //   * relabel accepted nodes with compressed depth + renumbered sibling indices.
  //
  // Predicate semantics: TRUE = keep (accept); FALSE = extract.
  //
  // NOTE: predicate-only. Consumer traversal strategies are ignored for the spike (validated
  // against a TraverseAll corpus). DFT only; BFT path is intentionally absent.
  internal sealed class WhereDepthFirstTreenumerator2<TNode>
    : TreenumeratorWrapper<TNode>
  {
    public WhereDepthFirstTreenumerator2(
      Func<ITreenumerator<TNode>> innerTreenumeratorFactory,
      Func<NodeContext<TNode>, bool> predicate)
      : base(innerTreenumeratorFactory)
    {
      _Predicate = predicate;
    }

    private readonly Func<NodeContext<TNode>, bool> _Predicate;

    private sealed class Frame
    {
      public TNode Node;
      public int SourceDepth;
      public bool Extracted;
      public NodePosition EffectivePosition;   // accepted only
      public int EffectiveChildCount;          // accepted only: # accepted effective children scheduled
      public int EffectiveVisitsEmitted;       // accepted only
      public bool YieldedAccepted;             // subtree has produced >= 1 accepted node
      public bool CollapseSkipped;             // consumer SkipNode'd: anchors children's positions
                                               // but emits none of its own visits (engine collapse)
    }

    private readonly List<Frame> _Path = new List<Frame>();
    private int _SeenEffectiveRootCount = 0;

    // Dedup for an unwind cascade: after one effective child completes, several nested extracted
    // ancestors' visits fire consecutively, all wanting to emit the SAME ancestor's boundary visit.
    // Only the deepest (first) should. Reset on any scheduling event and on any accepted-node emit.
    private bool _BoundaryConsumed = false;

    // SkipSiblings is promotion-aware (effective siblings != inner siblings), so we can't delegate it
    // to the inner. While active, we prune everything the inner schedules until it exits the effective
    // parent's subtree (inner depth <= _SkipSiblingsParentDepth), excluding the SkipSiblings'd node's
    // own subtree (inner depth > _SkipSiblingsNodeDepth). The visit cap (in TryHandleVisiting) stops
    // the effective parent from being visited for the pruned siblings.
    // Active SkipSiblings scopes, innermost last (a stack -- nested SkipSiblings can overlap). Each
    // prunes its effective parent's remaining children (inner depth in (ParentDepth, NodeDepth]) until
    // the traversal exits that effective parent's subtree (inner depth <= ParentDepth).
    private readonly List<(int NodeDepth, int ParentDepth)> _SkipSiblings = new List<(int, int)>();
    private bool _PruneNext = false;

    protected override bool OnMoveNext(NodeTraversalStrategies nodeTraversalStrategies)
    {
      // The consumer's strategy applies to the accepted node we last emitted as a scheduling node
      // (== the inner's current scheduling node, and the top of _Path).
      var innerStrategies = NodeTraversalStrategies.TraverseAll;

      if (Mode == TreenumeratorMode.SchedulingNode)
      {
        var strategies = nodeTraversalStrategies;

        // SkipSiblings: arm pruning of the effective parent's remaining children. The node itself
        // keeps the strategy's other bits (SkipNode / SkipDescendants), forwarded to the inner.
        if (strategies.HasNodeTraversalStrategies(NodeTraversalStrategies.SkipSiblings))
        {
          var node = _Path[_Path.Count - 1];
          // The effective parent for sibling purposes is the nearest REAL node in the effective
          // tree: not extracted (predicate) and not collapse-skipped (consumer SkipNode). If there
          // is none, the node is an effective root and SkipSiblings ends the remaining forest.
          var effectiveParent = NearestSiblingParent(_Path.Count - 2);
          _SkipSiblings.Add((node.SourceDepth, effectiveParent?.SourceDepth ?? -1));
          strategies &= ~NodeTraversalStrategies.SkipSiblings;
        }

        // Forward the rest to the inner: its native SkipNode IS the engine-collapse we want (skipped
        // node not visited, children promoted at their depth). We additionally MARK a pure-SkipNode'd
        // node so we suppress reassigning its extracted descendants' interleaves to it -- otherwise
        // the extraction rewrite emits phantom parent visits for a node the consumer collapsed.
        innerStrategies = strategies;

        var skipsNode = strategies.HasNodeTraversalStrategies(NodeTraversalStrategies.SkipNode);
        var skipsDescendants = strategies.HasNodeTraversalStrategies(NodeTraversalStrategies.SkipDescendants);

        if (skipsNode && !skipsDescendants)
          _Path[_Path.Count - 1].CollapseSkipped = true;
      }

      while (InnerTreenumerator.MoveNext(innerStrategies))
      {
        innerStrategies = NodeTraversalStrategies.TraverseAll;

        if (InnerTreenumerator.Mode == TreenumeratorMode.SchedulingNode)
        {
          if (TryHandleScheduling())
            return true;

          // A pruned effective sibling: advance the inner past its whole subtree.
          if (_PruneNext)
          {
            _PruneNext = false;
            innerStrategies = NodeTraversalStrategies.SkipNodeAndDescendants;
          }
        }
        else if (TryHandleVisiting())
        {
          return true;
        }
      }

      return false;
    }

    private bool TryHandleScheduling()
    {
      var depth = InnerTreenumerator.Position.Depth;

      // A scheduling event opens a new boundary.
      _BoundaryConsumed = false;

      // Remove the previous sibling (same depth) and any of its leftover subtree.
      while (_Path.Count > 0 && _Path[_Path.Count - 1].SourceDepth >= depth)
        _Path.RemoveAt(_Path.Count - 1);

      // SkipSiblings: drop scopes whose effective parent's subtree we've now exited, then -- if this
      // node is an effective sibling within the innermost remaining scope -- prune it and its subtree.
      while (_SkipSiblings.Count > 0 && depth <= _SkipSiblings[_SkipSiblings.Count - 1].ParentDepth)
        _SkipSiblings.RemoveAt(_SkipSiblings.Count - 1);

      if (_SkipSiblings.Count > 0 && depth <= _SkipSiblings[_SkipSiblings.Count - 1].NodeDepth)
      {
        _PruneNext = true;
        return false;
      }

      if (!_Predicate(InnerTreenumerator.ToNodeContext()))
      {
        _Path.Add(new Frame
        {
          Node = InnerTreenumerator.Node,
          SourceDepth = depth,
          Extracted = true,
          YieldedAccepted = false,
        });
        return false; // suppress the extracted node's own schedule
      }

      // Accepted: compute effective position via nearest accepted ancestor.
      var ancestor = NearestAcceptedAncestor(_Path.Count - 1);

      NodePosition effectivePosition;
      if (ancestor == null)
      {
        effectivePosition = new NodePosition(_SeenEffectiveRootCount, 0);
        _SeenEffectiveRootCount++;
      }
      else
      {
        effectivePosition = new NodePosition(ancestor.EffectiveChildCount, ancestor.EffectivePosition.Depth + 1);
        ancestor.EffectiveChildCount++;
      }

      // This node is accepted -> every ancestor (and itself) has now yielded an accepted node.
      for (int i = 0; i < _Path.Count; i++)
        _Path[i].YieldedAccepted = true;

      var frame = new Frame
      {
        Node = InnerTreenumerator.Node,
        SourceDepth = depth,
        Extracted = false,
        EffectivePosition = effectivePosition,
        EffectiveChildCount = 0,
        EffectiveVisitsEmitted = 0,
        YieldedAccepted = true,
      };
      _Path.Add(frame);

      Mode = TreenumeratorMode.SchedulingNode;
      Node = frame.Node;
      VisitCount = 0;
      Position = effectivePosition;
      return true;
    }

    private bool TryHandleVisiting()
    {
      var depth = InnerTreenumerator.Position.Depth;
      var visitCount = InnerTreenumerator.VisitCount;

      // Pop the completed child subtree (deeper than this node); capture the direct child (depth+1).
      Frame completedChild = null;
      while (_Path.Count > 0 && _Path[_Path.Count - 1].SourceDepth > depth)
      {
        var popped = _Path[_Path.Count - 1];
        if (popped.SourceDepth == depth + 1)
          completedChild = popped;
        _Path.RemoveAt(_Path.Count - 1);
      }

      var frame = _Path[_Path.Count - 1]; // the node being visited, at `depth`

      if (frame.Extracted)
      {
        // An extracted node's arrival visit coincides with its effective parent's
        // "before first promoted child" boundary -> always dropped.
        if (visitCount == 1)
          return false;

        // A non-arrival visit is a real boundary only if the completed child actually
        // produced an accepted node. Reassign it to the nearest accepted ancestor, once.
        if (completedChild != null && completedChild.YieldedAccepted && !_BoundaryConsumed)
        {
          var ancestor = NearestAcceptedAncestor(_Path.Count - 2);
          // A collapse-skipped ancestor absorbs the boundary visit (no interleaves emitted for it).
          if (ancestor != null && !ancestor.CollapseSkipped)
          {
            ancestor.EffectiveVisitsEmitted++;
            EmitReassigned(ancestor);
            return true;
          }
        }

        return false;
      }

      // Accepted node. (A pure-SkipNode'd node's own visits never arrive -- the inner suppresses
      // them under SkipNode -- so there is nothing to drop here; only its reassigned interleaves,
      // suppressed above, would have leaked.)
      if (visitCount == 1)
      {
        frame.EffectiveVisitsEmitted = 1;
        EmitAccepted(frame, 1);
        return true;
      }

      // Interleave / final visit after the just-completed source child.
      if (completedChild != null)
      {
        // An extracted child's interleaves were already reassigned to this frame.
        if (completedChild.Extracted)
          return false;

        // A consumer-collapsed child whose promoted brood was entirely predicate-filtered is an
        // empty effective slot: the inner still emits a parent visit for it (it saw the promoted
        // child before we extracted it), but the effective tree has no child there, so drop it.
        if (completedChild.CollapseSkipped && completedChild.EffectiveChildCount == 0)
          return false;
      }

      // A node has exactly (effective children + 1) visits. The inner emits extra parent visits for
      // children we pruned via SkipSiblings (never counted as effective children), so cap here.
      if (frame.EffectiveVisitsEmitted > frame.EffectiveChildCount)
        return false;

      frame.EffectiveVisitsEmitted++;
      EmitAccepted(frame, frame.EffectiveVisitsEmitted);
      return true;
    }

    private void EmitAccepted(Frame frame, int visitCount)
    {
      Mode = TreenumeratorMode.VisitingNode;
      Node = frame.Node;
      VisitCount = visitCount;
      Position = frame.EffectivePosition;
      _BoundaryConsumed = false;
    }

    private void EmitReassigned(Frame ancestor)
    {
      Mode = TreenumeratorMode.VisitingNode;
      Node = ancestor.Node;
      VisitCount = ancestor.EffectiveVisitsEmitted;
      Position = ancestor.EffectivePosition;
      _BoundaryConsumed = true;
    }

    private Frame NearestAcceptedAncestor(int fromIndex)
    {
      for (int i = fromIndex; i >= 0; i--)
        if (!_Path[i].Extracted)
          return _Path[i];

      return null;
    }

    // The nearest ancestor that is a REAL node in the effective tree (neither extracted nor
    // collapse-skipped) -- i.e. the node whose remaining children are this node's effective siblings.
    private Frame NearestSiblingParent(int fromIndex)
    {
      for (int i = fromIndex; i >= 0; i--)
        if (!_Path[i].Extracted && !_Path[i].CollapseSkipped)
          return _Path[i];

      return null;
    }
  }
}
