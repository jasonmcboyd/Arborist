using Arborist.Core;
using System;
using System.Collections.Generic;

namespace Arborist.Treenumerators
{
  /// <summary>
  /// Depth-first treenumerator. Emits the SAME (Mode, Node, VisitCount, Position) visit multiset as
  /// the breadth-first treenumerator -- the load-bearing invariant -- but in pre-/post-order. A node
  /// is scheduled exactly once; an accepted node is then visited once up front plus once after every
  /// surviving child (so an unskipped subtree gives the familiar raw-children + 1 visits).
  ///
  /// <para>The engine is a single stack of <see cref="Frame"/>s describing the path from a root down
  /// to the node currently being processed -- one frame per descended <i>raw</i> level. Each frame
  /// bundles a node's visit state with its (mutable-struct) child enumerator, which is always driven
  /// in place by <c>ref</c> via <see cref="RefSemiDeque{T}.GetLast"/> so it is never copied by value.
  /// Because there is one frame per raw level, the current raw depth is simply
  /// <c>_Stack.Count - 1</c>, and the next child's depth is <c>_Stack.Count</c>.</para>
  ///
  /// <para>A node is classified the move after it is scheduled (see <see cref="OnScheduling"/>):</para>
  ///
  /// <list type="bullet">
  /// <item><b>Accepted</b> (TraverseAll / SkipDescendants): the frame's initial visit is emitted.</item>
  /// <item><b>SkipNode</b>: the node is swallowed but its frame stays resident, marked
  /// <see cref="Frame.IsSkipped"/>, so its children are promoted into its slot (keeping their raw
  /// depth and sibling index -- the engine does not compress depth). It emits no visit of its own.</item>
  /// <item><b>SkipNodeAndDescendants</b>: the node and its subtree are dropped; the frame is popped
  /// having produced nothing.</item>
  /// </list>
  ///
  /// <para><b>Cadence.</b> Scheduling always draws from the <i>top</i> frame's enumerator -- the
  /// deepest level, which under SkipNode is a swallowed promoter feeding its children into its parent's
  /// slot. An accepted parent is visited once up front, then once more after each of its own direct
  /// child slots completes <i>provided that slot ultimately produced at least one accepted node</i>
  /// (so a swallowed childless child, or a dropped subtree, adds no visit, while a swallowed promoter
  /// counts as the single slot it occupies regardless of how many nodes it promotes).</para>
  ///
  /// <para>Each frame carries <see cref="Frame.ProducedAccepted"/> -- whether any accepted node was
  /// emitted anywhere in its subtree. When a completed frame is popped this bit flows into its parent:
  /// a swallowed parent ORs it in (its own slot is productive if any promoted child was), and an
  /// accepted parent reads it to decide whether the just-finished slot earns a return visit.</para>
  /// <para><b>Design.</b> This single-frame engine replaced an earlier two-deque design -- a separate
  /// accepted-node stack and a child-enumerator stack that deliberately diverged under SkipNode, with a
  /// <c>_DepthOfLastVisitedNode</c> heuristic driving a multi-branch backtrack. Folding each node's
  /// visit state and its child enumerator into one <see cref="Frame"/> ("struct cohesion") removed that
  /// divergence and the heuristic, and measured slightly faster (a node and its enumerator now share a
  /// cache line); do not split it back into parallel deques.</para>
  /// </summary>
  public sealed class DepthFirstTreenumerator<TValue, TNode, TChildEnumerator>
    : TreenumeratorBase<TValue>
    where TChildEnumerator : IChildEnumerator<TNode>
  {
    public DepthFirstTreenumerator(
      IEnumerable<TNode> rootNodes,
      Func<NodeContext<TNode>, TChildEnumerator> childEnumeratorFactory,
      Func<TNode, TValue> map)
    {
      _RootsEnumerator = rootNodes.GetEnumerator();
      _ChildEnumeratorFactory = childEnumeratorFactory;
      _Map = map;
    }

    private readonly IEnumerator<TNode> _RootsEnumerator;
    private readonly Func<NodeContext<TNode>, TChildEnumerator> _ChildEnumeratorFactory;
    private readonly Func<TNode, TValue> _Map;

    // The path from a root to the node currently being processed: one frame per descended raw level,
    // including SkipNode'd levels (kept resident so their children can be promoted).
    private readonly RefSemiDeque<Frame> _Stack = new RefSemiDeque<Frame>();

    private int _RootNodesSeen = 0;
    private bool _RootsEnumeratorFinished = false;

    private struct Frame
    {
      public Frame(TNode node, NodePosition position, TChildEnumerator childEnumerator)
      {
        Node = node;
        Position = position;
        VisitCount = 0;
        IsSkipped = false;
        ProducedAccepted = false;
        ChildEnumerator = childEnumerator;
      }

      public TNode Node;
      public NodePosition Position;
      public int VisitCount;
      public bool IsSkipped;          // SkipNode'd: resident only to promote its children.
      public bool ProducedAccepted;   // An accepted node was emitted somewhere in this frame's subtree.
      public TChildEnumerator ChildEnumerator;
    }

    protected override bool OnMoveNext(NodeTraversalStrategies nodeTraversalStrategies)
    {
      if (_Stack.Count == 0)
        return MoveToNextRootNode();

      if (Mode == TreenumeratorMode.SchedulingNode)
        return OnScheduling(nodeTraversalStrategies);

      // A node was just visited: descend into its next child, else unwind to its parent.
      if (TryScheduleNextChild())
        return true;

      return Backtrack();
    }

    // Classify the node just scheduled (the stack top) by the consumer's strategy, then emit the
    // next visit.
    private bool OnScheduling(NodeTraversalStrategies nodeTraversalStrategies)
    {
      if (nodeTraversalStrategies.HasNodeTraversalStrategies(NodeTraversalStrategies.SkipSiblings))
        SkipRemainingSiblings();

      // SkipNodeAndDescendants is a superset of SkipNode (HasNodeTraversalStrategies is an all-bits
      // test), so it must be checked first -- otherwise it would route into the SkipNode promotion
      // path and wrongly promote the descendants we are meant to prune.
      if (nodeTraversalStrategies.HasNodeTraversalStrategies(NodeTraversalStrategies.SkipNodeAndDescendants))
        return Backtrack();

      if (nodeTraversalStrategies.HasNodeTraversalStrategies(NodeTraversalStrategies.SkipNode))
      {
        // Swallow the node but keep its frame resident so its children promote into its slot.
        _Stack.GetLast().IsSkipped = true;

        if (TryScheduleNextChild())
          return true;

        return Backtrack();
      }

      if (nodeTraversalStrategies.HasNodeTraversalStrategies(NodeTraversalStrategies.SkipDescendants))
        // Accept the node but prune its children: dispose its enumerator so it yields none.
        _Stack.GetLast().ChildEnumerator.Dispose();

      // Accept: emit the node's initial visit (TraverseAll, or the SkipDescendants fall-through).
      ref var frame = ref _Stack.GetLast();
      frame.ProducedAccepted = true;
      frame.VisitCount++;
      UpdateState(ref frame);
      return true;
    }

    // The top frame's subtree is complete. Pop it, carrying whether it produced any accepted node up
    // to its parent, then emit the next visit. A swallowed parent ORs the bit in and resumes promoting
    // its children (no visit of its own). An accepted parent earns a return visit only if the slot
    // that just finished produced an accepted node; otherwise it simply moves to its next child.
    private bool Backtrack()
    {
      while (true)
      {
        ref var completed = ref _Stack.RemoveLast();
        var slotProducedAccepted = completed.ProducedAccepted;
        completed.ChildEnumerator.Dispose();

        if (_Stack.Count == 0)
          return MoveToNextRootNode();

        ref var parent = ref _Stack.GetLast();

        if (parent.IsSkipped)
        {
          parent.ProducedAccepted |= slotProducedAccepted;
        }
        else if (slotProducedAccepted)
        {
          parent.VisitCount++;
          UpdateState(ref parent);
          return true;
        }

        if (TryScheduleNextChild())
          return true;
      }
    }

    private bool MoveToNextRootNode()
    {
      if (_RootsEnumeratorFinished || !_RootsEnumerator.MoveNext())
        return false;

      PushFrame(_RootsEnumerator.Current, new NodePosition(_RootNodesSeen, 0));

      _RootNodesSeen++;

      UpdateState(ref _Stack.GetLast());

      return true;
    }

    private bool TryScheduleNextChild()
    {
      if (!_Stack.GetLast().ChildEnumerator.MoveNext(out var child))
        return false;

      PushFrame(child.Node, new NodePosition(child.SiblingIndex, _Stack.Count));

      UpdateState(ref _Stack.GetLast());

      return true;
    }

    private void PushFrame(TNode node, NodePosition position)
    {
      var childEnumerator = _ChildEnumeratorFactory(new NodeContext<TNode>(node, position));
      _Stack.AddLast(new Frame(node, position, childEnumerator));
    }

    // SkipSiblings: stop enumerating the remaining siblings of the node just scheduled (the stack
    // top). Its siblings are the remaining children of its nearest accepted ancestor (skipped
    // ancestors are transparent), so we silence that ancestor's enumerator along with every skipped
    // ancestor in between. If every ancestor is skipped, the node is effectively a root and its
    // siblings are the remaining roots, so we end root enumeration instead.
    private void SkipRemainingSiblings()
    {
      for (int i = 1; i < _Stack.Count; i++)
      {
        ref var ancestor = ref _Stack.GetFromBack(i);
        ancestor.ChildEnumerator.Dispose();

        if (!ancestor.IsSkipped)
          return;
      }

      _RootsEnumeratorFinished = true;
    }

    private void UpdateState(ref Frame frame)
    {
      Mode = frame.VisitCount == 0 ? TreenumeratorMode.SchedulingNode : TreenumeratorMode.VisitingNode;
      Node = _Map(frame.Node);
      VisitCount = frame.VisitCount;
      Position = frame.Position;
    }

    #region Dispose

    protected override void OnDisposing()
    {
      base.OnDisposing();

      _RootsEnumerator?.Dispose();

      if (_Stack != null)
        while (_Stack.Count > 0)
          _Stack.RemoveLast().ChildEnumerator.Dispose();
    }

    #endregion Dispose
  }
}
