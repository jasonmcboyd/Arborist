using Arborist.Core;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Arborist.Treenumerators
{
  /// <summary>
  /// Depth-first treenumerator. Emits the (Mode, Node, VisitCount, Position) visit stream in
  /// depth-first order -- a node is scheduled, then visited to completion (subtree and all) before its
  /// next sibling is scheduled. An accepted node is visited once up front plus once between/after each
  /// surviving child; a SkipNode'd or SkipNodeAndDescendants'd node contributes no visit of its own.
  /// Depth and sibling index are the RAW inner values; this engine never compresses depth or renumbers
  /// promoted siblings (that is the Where operator's job).
  ///
  /// <para>All of the engine's structural state lives in
  /// <see cref="DepthFirstPath{TNode, TChildEnumerator}"/>; this class is a thin driver over it. The
  /// only operation that touches the source is advancing a child enumerator
  /// (<see cref="TryPushNextChild"/>) -- the single I/O seam a future async treenumerator would swap
  /// for an awaited pull. Every other line is shared synchronous state on the path.</para>
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
      _Path = new DepthFirstPath<TNode, TChildEnumerator>(childEnumeratorFactory);
      _Map = map;
    }

    private readonly IEnumerator<TNode> _RootsEnumerator;
    // Non-readonly so calls bind `ref this` and the struct's state mutations persist (a readonly field
    // would force a defensive copy and silently lose them).
    private DepthFirstPath<TNode, TChildEnumerator> _Path;
    private readonly Func<TNode, TValue> _Map;

    private bool _RootsEnumeratorFinished = false;

    protected override bool OnMoveNext(NodeTraversalStrategies nodeTraversalStrategies)
    {
      // Nothing descended yet: schedule the next root (the first move, or the gap between roots).
      if (_Path.IsEmpty)
        return MoveToNextRootNode();

      // The strategy applies to the node just scheduled; visiting nodes ignore it.
      if (Mode == TreenumeratorMode.SchedulingNode)
        return OnScheduling(nodeTraversalStrategies);

      return OnVisiting();
    }

    private bool MoveToNextRootNode()
    {
      if (_RootsEnumeratorFinished || !_RootsEnumerator.MoveNext())
        return false;

      Publish(ref _Path.PushRoot(_RootsEnumerator.Current));

      return true;
    }

    // Apply the consumer's strategy to the node just scheduled, then emit its first visit (or move on
    // if it is skipped/pruned).
    private bool OnScheduling(NodeTraversalStrategies nodeTraversalStrategies)
    {
      if (nodeTraversalStrategies.HasNodeTraversalStrategies(NodeTraversalStrategies.SkipSiblings))
        if (_Path.SkipRemainingSiblings())
          _RootsEnumeratorFinished = true;

      // SkipNodeAndDescendants is a superset of SkipNode (HasNodeTraversalStrategies is an all-bits
      // test), so it must be checked first -- otherwise it would route into the SkipNode promotion path
      // and wrongly promote the descendants we are meant to prune.
      if (nodeTraversalStrategies.HasNodeTraversalStrategies(NodeTraversalStrategies.SkipNodeAndDescendants))
        return Backtrack();

      if (nodeTraversalStrategies.HasNodeTraversalStrategies(NodeTraversalStrategies.SkipNode))
      {
        _Path.SkipCurrentNode();

        if (TryPushNextChild())
          return true;

        // No children to promote: a childless SkipNode'd node emits nothing.
        return Backtrack();
      }

      if (nodeTraversalStrategies.HasNodeTraversalStrategies(NodeTraversalStrategies.SkipDescendants))
        _Path.DisposeCurrentEnumerator();

      // Accept (TraverseAll, or the SkipDescendants fall-through): emit the node's first visit.
      Publish(ref _Path.TakeNextVisit());

      return true;
    }

    // A VisitingNode was just emitted: descend into its next child, or backtrack if it has none left.
    private bool OnVisiting()
    {
      if (TryPushNextChild())
        return true;

      return Backtrack();
    }

    // Unwind finished levels and emit the next owed visit. Each iteration pops one exhausted level and
    // decides what the level we returned to owes: descend into its next child, or -- for an accepted
    // node owed a between/after-children visit -- re-emit it.
    private bool Backtrack()
    {
      while (true)
      {
        _Path.PopFinishedLevel();

        // Unwound the whole forest path: advance to the next root (or finish).
        if (_Path.IsEmpty)
          return MoveToNextRootNode();

        // No visit is owed at the level we returned to -- it already took its return visit, or it has
        // no accepted node (a chain of skipped roots), or it is a SkipNode'd level -- so promote its
        // next child without a parent visit.
        if (_Path.TopLevelAlreadyVisited || !_Path.HasAcceptedNodes || _Path.TopLevelWasSkipped)
        {
          if (TryPushNextChild())
            return true;

          continue;
        }

        // The accepted node at this level owes its next between/after-children visit.
        Publish(ref _Path.TakeNextVisit());

        return true;
      }
    }

    // THE SEAM: advance the active level's child enumerator and schedule the child it yields. A future
    // async treenumerator replaces the synchronous MoveNext here with an awaited MoveNextAsync; nothing
    // else changes.
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private bool TryPushNextChild()
    {
      if (!_Path.TopEnumerator.MoveNext(out var child))
        return false;

      Publish(ref _Path.PushChild(child.Node, child.SiblingIndex));

      return true;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void Publish(ref DepthFirstNodeState<TNode> node)
    {
      Mode = node.VisitCount == 0 ? TreenumeratorMode.SchedulingNode : TreenumeratorMode.VisitingNode;
      Node = _Map(node.Node);
      VisitCount = node.VisitCount;
      Position = node.Position;
    }

    #region Dispose

    protected override void OnDisposing()
    {
      base.OnDisposing();

      _RootsEnumerator?.Dispose();
      _Path.Dispose();
    }

    #endregion Dispose
  }
}
