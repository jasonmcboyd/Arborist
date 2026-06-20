using Arborist.Core;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Arborist.Treenumerators
{
  /// <summary>
  /// Breadth-first treenumerator. Emits the SAME (Mode, Node, VisitCount, Position) visits as the
  /// depth-first treenumerator, but in level order. Every node is scheduled exactly once and ends
  /// with VisitCount == (raw child count + 1), independent of any skip strategy.
  ///
  /// State is two pairs of ref-returning deques (node value and its mutable-struct child enumerator
  /// kept in lockstep, never copied by value):
  ///   * _VisitQueue (FIFO): accepted nodes that are scheduled but not yet fully visited. The front
  ///     is the current effective parent whose VisitCount is driven toward (raw children + 1),
  ///     emitting an interleaved parent visit between each child.
  ///   * _SchedulingStack (LIFO): the node currently being classified, plus any SkipNode'd ancestors
  ///     kept resident so their remaining children can be promoted into the skipped node's slot. The
  ///     engine does not compress depth: a promoted child keeps its raw reported depth.
  ///
  /// Cadence: visiting is deferred. All nodes at a level are scheduled (with parent visits
  /// interleaved) before any of them is visited; a node is visited only once it reaches the queue front.
  ///
  /// Deferred parent visit (the subtle part, and the source of past regressions): when a SkipNode'd
  /// node's children are promoted, scheduling jumps to a promoted sibling instead of returning to the
  /// queue-front parent, so that parent's interleaved visit is swallowed. It is recorded in
  /// _OwesPromotedParentVisit and paid later -- either by the subtree's last normal enqueue, or, if
  /// the subtree exhausts with no further enqueue (its last promoted child is itself SkipNode'd or
  /// SkipNodeAndDescendants'd), by PayOwedParentVisitAndDeferChild, which emits the owed parent visit
  /// now and stashes the just-scheduled child in _HasDeferredScheduledChild for the next MoveNext to
  /// re-surface, preserving the order "owed parent visit, then promoted child".
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

    // The accepted (non-skipped) nodes that have been scheduled but not yet fully
    // visited. FIFO: the front is the current effective parent whose children are
    // being scheduled/visited and whose VisitCount is driven to (raw children + 1).
    private readonly RefSemiDeque<InternalNodeVisitState<TNode>> _VisitQueue = new RefSemiDeque<InternalNodeVisitState<TNode>>();
    private readonly RefSemiDeque<TChildEnumerator> _VisitQueueChildEnumerators = new RefSemiDeque<TChildEnumerator>();

    // The scheduling-descent path: the node currently being classified, plus any
    // SkipNode'd ancestors kept resident so their remaining children can be promoted
    // into the skipped node's slot. LIFO: the top is the node being acted on.
    private readonly RefSemiDeque<InternalNodeVisitState<TNode>> _SchedulingStack = new RefSemiDeque<InternalNodeVisitState<TNode>>();
    private readonly RefSemiDeque<TChildEnumerator> _SchedulingStackChildEnumerators = new RefSemiDeque<TChildEnumerator>();

    private int _RootNodesSeen = 0;
    private bool _RootsEnumeratorFinished = false;
    // Depth of the last node actually acted on (visited, or scheduled-as-accepted -- NOT a
    // node carrying the SkipNode bit this step, i.e. SkipNode or SkipNodeAndDescendants). The
    // skip paths use it to decide whether the visit-queue front's subtree is finished and the
    // front can be retired.
    private int _DepthOfLastActedOnNode = -1;
    // A child was scheduled but an owed parent visit had to be emitted first, so the
    // child's emission is deferred to the next MoveNext. See PayOwedParentVisitAndDeferChild.
    private bool _HasDeferredScheduledChild = false;

    protected override bool OnMoveNext(NodeTraversalStrategies nodeTraversalStrategies)
    {
      if (Mode == TreenumeratorMode.VisitingNode || !nodeTraversalStrategies.HasNodeTraversalStrategies(NodeTraversalStrategies.SkipNode))
        _DepthOfLastActedOnNode = Position.Depth;

      // Release the child deferred so an owed parent visit could be emitted first.
      if (_HasDeferredScheduledChild)
      {
        _HasDeferredScheduledChild = false;
        UpdateState(ref _SchedulingStack.GetLast());
        return true;
      }

      if (_VisitQueue.Count == 0 && _SchedulingStack.Count == 0)
        return MoveToNextRootNode();

      if (Mode == TreenumeratorMode.SchedulingNode)
        return OnScheduling(nodeTraversalStrategies);

      return OnVisiting();
    }

    private bool MoveToNextRootNode()
    {
      if (_RootsEnumeratorFinished || !_RootsEnumerator.MoveNext())
        return false;

      PushNewNodeVisit(_RootsEnumerator.Current, new NodePosition(_RootNodesSeen, 0));

      _RootNodesSeen++;

      UpdateState(ref _SchedulingStack.GetLast());

      return true;
    }

    private bool OnScheduling(NodeTraversalStrategies nodeTraversalStrategies)
    {
      if (nodeTraversalStrategies.HasNodeTraversalStrategies(NodeTraversalStrategies.SkipSiblings))
      {
        // TODO: I could probably avoid having to eagerly dispose of all of the
        // skipped node's child enumerators, but it would require storing more
        // state in the stack. I would have to benchmark it to see how it performed.
        for (int i = 1; i < _SchedulingStackChildEnumerators.Count; i++)
          _SchedulingStackChildEnumerators.GetFromBack(i).Dispose();

        // The node being SkipSibling'd is effectively a root when its only ancestors
        // are SkipNode'd ones still on the stack below it (accepted ancestors live in
        // the queue, skipped ones stay on the stack). That is the BFT analog of DFT's
        // `_Stack.Count == 1`: Position.Depth == (skipped ancestors) == _SchedulingStack.Count - 1.
        // In that case skipping siblings ends the root enumeration. Otherwise the node
        // has an accepted parent at the queue front whose remaining children we skip.
        if (Position.Depth == _SchedulingStack.Count - 1 || _VisitQueueChildEnumerators.Count == 0)
          _RootsEnumeratorFinished = true;
        else
          _VisitQueueChildEnumerators.GetFirst().Dispose();
      }

      // Order matters: SkipNodeAndDescendants carries the SkipNode bit (it is a superset) and
      // HasNodeTraversalStrategies is an all-bits test, so it must be checked first -- otherwise a
      // SkipNodeAndDescendants node would route into PromoteChildren and its descendants would be
      // wrongly promoted instead of pruned. (DFT depends on the same ordering.)
      if (nodeTraversalStrategies.HasNodeTraversalStrategies(NodeTraversalStrategies.SkipNodeAndDescendants))
        return SkipSubtree();

      if (nodeTraversalStrategies.HasNodeTraversalStrategies(NodeTraversalStrategies.SkipNode))
        return PromoteChildren();

      if (nodeTraversalStrategies.HasNodeTraversalStrategies(NodeTraversalStrategies.SkipDescendants))
        _SchedulingStackChildEnumerators.GetLast().Dispose();

      _VisitQueue.AddLast(_SchedulingStack.RemoveLast());
      _VisitQueueChildEnumerators.AddLast(_SchedulingStackChildEnumerators.RemoveLast());

      // A SkipNode'd ancestor still on the stack means this enqueued node is a promoted
      // child, and Backtrack is about to schedule its sibling instead of returning to the
      // queue-front parent -- so the parent's visit gets swallowed. Defer it: it'll be
      // retriggered by the subtree's last enqueue, or paid at skip-exhaustion if none.
      //
      // ...but only when a real accepted parent exists. If EVERY ancestor on the descent
      // is SkipNode'd (raw depth == skipped-ancestor count, the BFT analog of the Bug B
      // effective-root test), this node is itself an effective ROOT: the queue front is an
      // unrelated earlier root, not its parent, so no parent visit was swallowed. Owing one
      // there leaves a stale visit that a later skip payment mis-pays to that unrelated root
      // -- the +1 over-count seen only once three concurrent promotions interact.
      var swallowedParentVisit =
        _SchedulingStack.Count > 0
        && Position.Depth > _SchedulingStack.Count;

      if (Backtrack())
      {
        // Record the owe ON the effective parent's queue entry (the front), not an
        // engine-wide flag, so it is paid only to THIS parent and discarded when the
        // parent retires -- never leaked to a later, unrelated front.
        if (swallowedParentVisit)
          _VisitQueue.GetFirst().OwesPromotedParentVisit = true;

        return true;
      }

      // Normal path: this visit is the (re)trigger that pays any owed parent visit.
      ref var previousVisit = ref _VisitQueue.GetFirst();

      previousVisit.OwesPromotedParentVisit = false;
      previousVisit.VisitCount++;

      UpdateState(ref previousVisit);

      return true;
    }

    private bool OnVisiting()
    {
      ref var previousVisit = ref _VisitQueue.GetFirst();
      ref var previousVisitChildEnumerator = ref _VisitQueueChildEnumerators.GetFirst();

      if (TryPushNextChild(ref previousVisit, ref previousVisitChildEnumerator))
        return true;

      // We have exhausted all children of the current node. We can remove it.
      _VisitQueue.RemoveFirst();
      _VisitQueueChildEnumerators.RemoveFirst().Dispose();

      // If there are no nodes left in the queue, return false.
      if (_VisitQueue.Count == 0)
        return false;

      // Otherwise,
      previousVisit = ref _VisitQueue.GetFirst();

      previousVisit.VisitCount++;

      UpdateState(ref previousVisit);

      return true;
    }

    // Handles the full SkipNode strategy: the node is removed and its children are promoted
    // into its slot among its siblings (contrast SkipSubtree, which prunes them), or, if it
    // has no children, scheduling simply backtracks to the next sibling/root.
    private bool PromoteChildren()
    {
      if (TryPushNextChild(ref _SchedulingStack.GetLast(), ref _SchedulingStackChildEnumerators.GetLast()))
        return true;

      PopStacks();

      if (Backtrack())
        return true;

      if (_VisitQueue.Count == 0)
        return false;

      ref var previousVisit = ref _VisitQueue.GetFirst();

      if (_SchedulingStackChildEnumerators.Count == 0 && previousVisit.VisitCount != 0)
      {
        if (TryPushNextChild(ref _VisitQueue.GetFirst(), ref _VisitQueueChildEnumerators.GetFirst()))
        {
          // Finished a skipped node's subtree and moved to the parent's next child. If the
          // parent still owes a visit for that subtree (no enqueue retriggered it), pay it now.
          if (previousVisit.OwesPromotedParentVisit)
            PayOwedParentVisitAndDeferChild();

          return true;
        }

        if (_DepthOfLastActedOnNode <= previousVisit.Position.Depth)
        {
          _VisitQueue.RemoveFirst();
          _VisitQueueChildEnumerators.RemoveFirst().Dispose();

          if (_VisitQueue.Count == 0)
            return false;
          else
            previousVisit = ref _VisitQueue.GetFirst();
        }
      }

      previousVisit.VisitCount++;

      UpdateState(ref previousVisit);

      return true;
    }

    private bool SkipSubtree()
    {
      PopStacks();

      if (Backtrack())
        return true;

      if (_VisitQueue.Count == 0)
        return false;

      if (Position.Depth != 0)
      {
        // Only try to push next child if the queue front has already been visited.
        // If VisitCount == 0, the node hasn't been visited yet and should be visited first.
        if (_VisitQueue.GetFirst().VisitCount != 0
          && TryPushNextChild(ref _VisitQueue.GetFirst(), ref _VisitQueueChildEnumerators.GetFirst()))
        {
          // Same as PromoteChildren's payment, but the skipped node's LAST promoted child
          // exhausted the subtree via skip-subtree rather than running out of children:
          // the parent may still owe a swallowed visit, so pay it before its next child.
          if (_VisitQueue.GetFirst().OwesPromotedParentVisit)
            PayOwedParentVisitAndDeferChild();

          return true;
        }

        // Only remove from queue if the node has already been visited and we've
        // finished processing its descendants (depth check ensures we've backtracked
        // to the same level or higher).
        // If VisitCount == 0, the node hasn't been visited yet and should still be visited.
        if (_VisitQueue.GetFirst().VisitCount != 0 && _DepthOfLastActedOnNode <= _VisitQueue.GetFirst().Position.Depth)
        {
          _VisitQueue.RemoveFirst();
          _VisitQueueChildEnumerators.RemoveFirst().Dispose();

          if (_VisitQueue.Count == 0)
            return false;
        }
      }

      ref var previousVisit = ref _VisitQueue.GetFirst();

      previousVisit.VisitCount++;

      UpdateState(ref previousVisit);

      return true;
    }

    // Advance scheduling past exhausted nodes: pull the next child of the deepest node
    // still on the scheduling stack (popping exhausted ones), else move to the next root.
    private bool Backtrack()
    {
      while (_SchedulingStack.Count > 0)
      {
        if (TryPushNextChild(ref _SchedulingStack.GetLast(), ref _SchedulingStackChildEnumerators.GetLast()))
          return true;

        PopStacks();
      }

      return MoveToNextRootNode();
    }

    // The child just scheduled by the caller occupies a promoted slot, so the parent's
    // swallowed visit must be emitted before it. Pay that visit now and defer the child to
    // the next MoveNext (released at the top of OnMoveNext), keeping the emission order
    // "owed parent visit, then promoted child".
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void PayOwedParentVisitAndDeferChild()
    {
      _HasDeferredScheduledChild = true;
      ref var owedParent = ref _VisitQueue.GetFirst();
      owedParent.OwesPromotedParentVisit = false;
      owedParent.VisitCount++;
      UpdateState(ref owedParent);
    }

    private bool TryPushNextChild(
      ref InternalNodeVisitState<TNode> nodeVisit,
      ref TChildEnumerator childEnumerator)
    {
      if (!childEnumerator.MoveNext(out var childNodeSiblingContext))
        return false;

      PushNewNodeVisit(childNodeSiblingContext.Node, new NodePosition(childNodeSiblingContext.SiblingIndex, nodeVisit.Position.Depth + 1));

      UpdateState(ref _SchedulingStack.GetLast());

      return true;
    }

    private void PushNewNodeVisit(
      TNode node,
      NodePosition nodePosition)
    {
      var internalNodeVisitState = new InternalNodeVisitState<TNode>(node, nodePosition);
      var nodeChildEnumerator = _ChildEnumeratorFactory(new NodeContext<TNode>(internalNodeVisitState.Node, internalNodeVisitState.Position));

      _SchedulingStack.AddLast(internalNodeVisitState);
      _SchedulingStackChildEnumerators.AddLast(nodeChildEnumerator);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void PopStacks()
    {
      _SchedulingStack.RemoveLast();
      _SchedulingStackChildEnumerators.RemoveLast().Dispose();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
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
      DisposeChildEnumerators(_SchedulingStackChildEnumerators);
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
