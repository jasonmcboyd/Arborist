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

    protected override bool OnMoveNext(NodeTraversalStrategies nodeTraversalStrategies)
    {
      // The consumer's strategy applies to the accepted node we last emitted as a scheduling node
      // (== the inner's current scheduling node, and the top of _Path).
      var innerStrategies = NodeTraversalStrategies.TraverseAll;

      if (Mode == TreenumeratorMode.SchedulingNode)
      {
        // Forward the consumer strategy to the inner: its native SkipNode IS the engine-collapse we
        // want (skipped node not visited, children promoted at their depth). We additionally MARK a
        // pure-SkipNode'd node so we suppress reassigning its extracted descendants' interleaves to
        // it -- otherwise the extraction stream-rewrite emits phantom parent visits for a node the
        // consumer collapsed. (SkipNodeAndDescendants / SkipAll prune the subtree, so no promotion.)
        innerStrategies = nodeTraversalStrategies;

        var skipsNode = nodeTraversalStrategies.HasNodeTraversalStrategies(NodeTraversalStrategies.SkipNode);
        var skipsDescendants = nodeTraversalStrategies.HasNodeTraversalStrategies(NodeTraversalStrategies.SkipDescendants);

        if (skipsNode && !skipsDescendants)
          _Path[_Path.Count - 1].CollapseSkipped = true;   // TODO: SkipSiblings bit not yet handled
      }

      while (InnerTreenumerator.MoveNext(innerStrategies))
      {
        innerStrategies = NodeTraversalStrategies.TraverseAll;

        if (InnerTreenumerator.Mode == TreenumeratorMode.SchedulingNode)
        {
          if (TryHandleScheduling())
            return true;
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
  }
}
