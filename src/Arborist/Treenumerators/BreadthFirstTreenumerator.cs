using Arborist.Core;
using System;
using System.Collections.Generic;

namespace Arborist.Treenumerators
{
  /// <summary>
  /// Breadth-first treenumerator. Emits the SAME (Mode, Node, VisitCount, Position) visit multiset as
  /// the depth-first treenumerator -- the load-bearing invariant -- but in level order. Every node is
  /// scheduled exactly once; an accepted node is then visited once up front plus once per surviving
  /// child (so an unskipped subtree gives the familiar raw-children + 1 visits, while a child that is
  /// SkipNodeAndDescendants'd, or SkipNode'd with nothing to promote, adds no visit).
  ///
  /// <para>The engine has two structures, each a value deque kept in lockstep with its node's
  /// (mutable-struct) child enumerator so neither is ever copied by value:</para>
  ///
  /// <list type="bullet">
  /// <item><b>_VisitQueue</b> (FIFO): accepted nodes that are scheduled but not yet fully visited.
  /// The <i>front</i> is the active parent. We visit it once up front, then drive its children, and
  /// visit it again after every child slot that enqueued at least one accepted node. Children are
  /// appended to the back, which is what makes the traversal breadth-first: a node is only ever
  /// visited once it reaches the front, by which point every node scheduled before it has been
  /// visited.</item>
  /// <item><b>_ScheduleStack</b> (LIFO): the node currently being classified, plus any SkipNode'd
  /// ancestors kept resident so their children can be promoted into the skipped node's slot. The
  /// engine does not compress depth -- a promoted child keeps its raw reported depth.</item>
  /// </list>
  ///
  /// <para><b>Cadence.</b> Each <see cref="OnMoveNext"/> applies the consumer's strategy to the
  /// node just scheduled (if any), then emits exactly one visit via <see cref="Advance"/>:</para>
  ///
  /// <list type="number">
  /// <item>while a node sits on the schedule stack, schedule its next child (descending into
  /// SkipNode'd children, whose own children are promoted) -- no parent visits are emitted here;</item>
  /// <item>while roots remain unscheduled, schedule the next root (the forest's children, also
  /// without surrounding visits);</item>
  /// <item>otherwise visit the queue front: its first (initial) visit, then a return visit owed for
  /// the child slot that just finished, then its next child, and finally retire it.</item>
  /// </list>
  ///
  /// <para>Whether the front is owed a return visit is a single bit, <see cref="_CurrentSlotEnqueuedNode"/>:
  /// a child slot earns the parent a visit exactly when it enqueues at least one accepted node.
  /// A normal/SkipDescendants child enqueues itself; a SkipNode'd child enqueues whatever it
  /// promotes; a SkipNodeAndDescendants'd child (or a childless SkipNode'd one) enqueues nothing and
  /// so is skipped over without a parent visit. This is what previously required a tangle of deferred
  /// parent-visit bookkeeping.</para>
  /// </summary>
  public sealed class BreadthFirstTreenumerator<TValue, TNode, TChildEnumerator>
    : TreenumeratorBase<TValue>
    where TChildEnumerator : IChildEnumerator<TNode>
  {
    public BreadthFirstTreenumerator(
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

    // Accepted nodes, scheduled but not yet fully visited. The front is the active parent.
    private readonly RefSemiDeque<InternalNodeVisitState<TNode>> _VisitQueue = new RefSemiDeque<InternalNodeVisitState<TNode>>();
    private readonly RefSemiDeque<TChildEnumerator> _VisitQueueChildEnumerators = new RefSemiDeque<TChildEnumerator>();

    // The node being classified, plus any SkipNode'd ancestors whose children are being promoted.
    private readonly RefSemiDeque<InternalNodeVisitState<TNode>> _ScheduleStack = new RefSemiDeque<InternalNodeVisitState<TNode>>();
    private readonly RefSemiDeque<TChildEnumerator> _ScheduleStackChildEnumerators = new RefSemiDeque<TChildEnumerator>();

    private int _RootNodesSeen = 0;
    private bool _RootsEnumeratorFinished = false;
    private bool _RootsScheduled = false;

    // True when the front parent's in-progress child slot has enqueued at least one accepted node,
    // so the front is owed a return visit before its next child is scheduled. Accepting a root also
    // sets it (roots have no front to owe), so Advance clears it once at the root/visit phase boundary.
    private bool _CurrentSlotEnqueuedNode = false;

    protected override bool OnMoveNext(NodeTraversalStrategies nodeTraversalStrategies)
    {
      // A strategy only applies to the node just scheduled (an empty stack means we have not
      // scheduled anything yet -- the very first move). Visiting nodes ignore the strategy.
      if (Mode == TreenumeratorMode.SchedulingNode && _ScheduleStack.Count > 0)
        ApplyStrategy(nodeTraversalStrategies);

      return Advance();
    }

    // Produce the next single visit (or false when the traversal is exhausted).
    private bool Advance()
    {
      while (true)
      {
        // 1) Descend: schedule the next child of the node on top of the schedule stack. SkipNode'd
        //    nodes stay here while their children are promoted; no parent visit is emitted.
        if (_ScheduleStack.Count > 0)
        {
          if (TryScheduleNextChild(ref _ScheduleStack.GetLast(), ref _ScheduleStackChildEnumerators.GetLast()))
            return true;

          PopScheduleStack();
          continue;
        }

        // 2) Schedule the next root (the forest's children -- no surrounding visits).
        if (!_RootsScheduled)
        {
          if (TryScheduleNextRoot())
            return true;

          _RootsScheduled = true;
          // Enqueues made while scheduling roots have no owing parent; clear the carry.
          _CurrentSlotEnqueuedNode = false;
          continue;
        }

        if (_VisitQueue.Count == 0)
          return false;

        // 3) Visit the active parent and drive its children.
        ref var parent = ref _VisitQueue.GetFirst();

        if (parent.VisitCount == 0)
        {
          // Initial visit, before any child is scheduled.
          parent.VisitCount = 1;
          UpdateState(ref parent);
          return true;
        }

        if (_CurrentSlotEnqueuedNode)
        {
          // The child slot that just finished enqueued at least one node, so the parent is visited.
          _CurrentSlotEnqueuedNode = false;
          parent.VisitCount++;
          UpdateState(ref parent);
          return true;
        }

        if (TryScheduleNextChild(ref parent, ref _VisitQueueChildEnumerators.GetFirst()))
          return true;

        // The parent has no more children: retire it. The next turn visits the new front.
        _VisitQueue.RemoveFirst();
        _VisitQueueChildEnumerators.RemoveFirst().Dispose();
      }
    }

    // Classify the node just scheduled (the schedule-stack top) by the consumer's strategy.
    private void ApplyStrategy(NodeTraversalStrategies nodeTraversalStrategies)
    {
      if (nodeTraversalStrategies.HasNodeTraversalStrategies(NodeTraversalStrategies.SkipSiblings))
        SkipRemainingSiblings();

      // SkipNodeAndDescendants is a superset of SkipNode (HasNodeTraversalStrategies is an all-bits
      // test), so it must be checked first -- otherwise it would route into the SkipNode promotion
      // path and wrongly promote the descendants we are meant to prune. (DFT depends on this too.)
      if (nodeTraversalStrategies.HasNodeTraversalStrategies(NodeTraversalStrategies.SkipNodeAndDescendants))
      {
        // Erase the node and its subtree; the slot enqueues nothing.
        PopScheduleStack();
        return;
      }

      if (nodeTraversalStrategies.HasNodeTraversalStrategies(NodeTraversalStrategies.SkipNode))
        // Keep the node resident so Advance can promote its children into its slot.
        return;

      if (nodeTraversalStrategies.HasNodeTraversalStrategies(NodeTraversalStrategies.SkipDescendants))
        // Accept the node but give it no children, then fall through to the accept block below.
        _ScheduleStackChildEnumerators.GetLast().Dispose();

      // Accept (reached for TraverseAll and the SkipDescendants fall-through): move the node onto the
      // visit queue, and record that this slot enqueued a node.
      _VisitQueue.AddLast(_ScheduleStack.RemoveLast());
      _VisitQueueChildEnumerators.AddLast(_ScheduleStackChildEnumerators.RemoveLast());
      _CurrentSlotEnqueuedNode = true;
    }

    // SkipSiblings: drop the remaining siblings of the node just scheduled. Its siblings are the
    // remaining children of its nearest accepted ancestor (skipped ancestors are transparent), so
    // we silence that ancestor's enumerator along with every skipped ancestor in between.
    private void SkipRemainingSiblings()
    {
      // Every schedule-stack enumerator except the node's own (the top) belongs to a skipped
      // ancestor that would otherwise promote more of the node's siblings.
      for (int i = 1; i < _ScheduleStackChildEnumerators.Count; i++)
        _ScheduleStackChildEnumerators.GetFromBack(i).Dispose();

      // The schedule stack holds only the node and its skipped ancestors (accepted ancestors live in
      // the queue), so _ScheduleStack.Count - 1 is the node's skipped-ancestor count. When that
      // equals the node's depth every ancestor is skipped: the node is an effective root, its
      // siblings are the remaining roots, and we end root enumeration. Otherwise the node has an
      // accepted ancestor -- necessarily the queue front -- whose remaining children we silence.
      if (_ScheduleStack.GetLast().Position.Depth == _ScheduleStack.Count - 1)
        _RootsEnumeratorFinished = true;
      else
        _VisitQueueChildEnumerators.GetFirst().Dispose();
    }

    private bool TryScheduleNextChild(
      ref InternalNodeVisitState<TNode> parent,
      ref TChildEnumerator parentChildEnumerator)
    {
      if (!parentChildEnumerator.MoveNext(out var child))
        return false;

      PushScheduled(child.Node, new NodePosition(child.SiblingIndex, parent.Position.Depth + 1));

      UpdateState(ref _ScheduleStack.GetLast());

      return true;
    }

    private bool TryScheduleNextRoot()
    {
      if (_RootsEnumeratorFinished || !_RootsEnumerator.MoveNext())
        return false;

      PushScheduled(_RootsEnumerator.Current, new NodePosition(_RootNodesSeen, 0));

      _RootNodesSeen++;

      UpdateState(ref _ScheduleStack.GetLast());

      return true;
    }

    private void PushScheduled(
      TNode node,
      NodePosition position)
    {
      _ScheduleStack.AddLast(new InternalNodeVisitState<TNode>(node, position));
      _ScheduleStackChildEnumerators.AddLast(_ChildEnumeratorFactory(new NodeContext<TNode>(node, position)));
    }

    private void PopScheduleStack()
    {
      _ScheduleStack.RemoveLast();
      _ScheduleStackChildEnumerators.RemoveLast().Dispose();
    }

    private void UpdateState(ref InternalNodeVisitState<TNode> nodeVisit)
    {
      Mode = nodeVisit.VisitCount == 0 ? TreenumeratorMode.SchedulingNode : TreenumeratorMode.VisitingNode;
      Node = _Map(nodeVisit.Node);
      VisitCount = nodeVisit.VisitCount;
      Position = nodeVisit.Position;
    }

    #region Dispose

    protected override void OnDisposing()
    {
      base.OnDisposing();

      _RootsEnumerator?.Dispose();

      DisposeChildEnumerators(_VisitQueueChildEnumerators);
      DisposeChildEnumerators(_ScheduleStackChildEnumerators);
    }

    private void DisposeChildEnumerators(RefSemiDeque<TChildEnumerator> childEnumerators)
    {
      if (childEnumerators == null)
        return;

      while (childEnumerators.Count > 0)
        childEnumerators.RemoveLast().Dispose();
    }

    #endregion Dispose
  }
}
