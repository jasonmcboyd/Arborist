using Arborist.Core;
using System;
using System.Runtime.CompilerServices;

namespace Arborist.Treenumerators
{
  // The visit-state of one accepted node on the depth-first path.
  internal struct DepthFirstNodeState<TNode>
  {
    public DepthFirstNodeState(TNode node, NodePosition position)
    {
      Node = node;
      Position = position;
      VisitCount = 0;
    }

    public TNode Node;
    public NodePosition Position;
    public int VisitCount;
  }

  /// <summary>
  /// The root-to-current-node path of a depth-first traversal and all of its structural bookkeeping,
  /// isolated behind intention-revealing operations so the treenumerator that drives it stays a thin
  /// shell.
  ///
  /// <para><b>Sans-I/O.</b> The path NEVER pulls a child itself. It stores the per-level child
  /// enumerators and exposes the active one by <c>ref</c> via <see cref="TopEnumerator"/> for the
  /// driver to advance -- synchronously today, asynchronously in a future async treenumerator. Every
  /// other operation here is pure synchronous state, so a sync and an async treenumerator can share
  /// this class verbatim and differ only at the one line that advances the enumerator.</para>
  ///
  /// <para><b>Memory.</b> Two deques are kept rather than one cohesive frame per node, specifically so
  /// a SkipNode'd node costs only its (small) child enumerator and not a full node-state frame:</para>
  /// <list type="bullet">
  /// <item><b>_AcceptedNodes</b>: the visit-state of the ACCEPTED nodes on the path only.</item>
  /// <item><b>_Enumerators</b>: one child enumerator per descended inner level, INCLUDING SkipNode'd
  /// levels (kept resident to promote their children). Defines the raw <see cref="Depth"/>.</item>
  /// </list>
  /// Under SkipNode the two diverge: the skipped node leaves <c>_AcceptedNodes</c> but its enumerator
  /// stays on <c>_Enumerators</c>, so a level whose accepted top is shallower than <see cref="Depth"/>
  /// is a skipped level (<see cref="TopLevelWasSkipped"/>).
  ///
  /// <para><b>Layout.</b> A mutable struct, embedded as a single field of the treenumerator so there is
  /// no extra object indirection on the hot path. It is never copied -- only ever accessed as the
  /// driver's <c>_Path</c> field (so method calls bind <c>ref this</c> and its mutations persist), and
  /// every <c>ref</c> it returns points into the heap-allocated deques, never into the struct
  /// itself.</para>
  /// </summary>
  internal struct DepthFirstPath<TNode, TChildEnumerator> : IDisposable
    where TChildEnumerator : IChildEnumerator<TNode>
  {
    public DepthFirstPath(Func<NodeContext<TNode>, TChildEnumerator> childEnumeratorFactory)
    {
      _ChildEnumeratorFactory = childEnumeratorFactory;
      _AcceptedNodes = new RefSemiDeque<DepthFirstNodeState<TNode>>();
      _Enumerators = new RefSemiDeque<TChildEnumerator>();
      _RootNodesSeen = 0;
      _DepthOfLastVisitedNode = -1;
    }

    private readonly Func<NodeContext<TNode>, TChildEnumerator> _ChildEnumeratorFactory;

    // Accepted nodes on the path (SkipNode'd nodes are absent).
    private readonly RefSemiDeque<DepthFirstNodeState<TNode>> _AcceptedNodes;
    // One child enumerator per descended level, including skipped levels. Defines raw depth.
    private readonly RefSemiDeque<TChildEnumerator> _Enumerators;

    private int _RootNodesSeen;
    // Raw depth of the most recently emitted VisitingNode; lets a backtracked-to level tell whether it
    // already took its return visit. A single int, so skipped nodes need no per-level visit bit.
    private int _DepthOfLastVisitedNode;

    // Raw depth of the active (deepest) level.
    public int Depth => _Enumerators.Count - 1;

    public bool IsEmpty => _Enumerators.Count == 0;

    public bool HasAcceptedNodes => _AcceptedNodes.Count > 0;

    // The active level's child enumerator, by ref so the driver advances it in place. THE I/O SEAM.
    public ref TChildEnumerator TopEnumerator
    {
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      get => ref _Enumerators.GetLast();
    }

    // Schedule a freshly pulled child as a new accepted level; returns its state so the driver can
    // publish it without a second lookup.
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ref DepthFirstNodeState<TNode> PushChild(TNode node, int siblingIndex)
      => ref PushLevel(node, new NodePosition(siblingIndex, Depth + 1));

    // Schedule the next root as a new accepted level; returns its state for publishing.
    public ref DepthFirstNodeState<TNode> PushRoot(TNode node)
      => ref PushLevel(node, new NodePosition(_RootNodesSeen++, 0));

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private ref DepthFirstNodeState<TNode> PushLevel(TNode node, NodePosition position)
    {
      _AcceptedNodes.AddLast(new DepthFirstNodeState<TNode>(node, position));
      _Enumerators.AddLast(_ChildEnumeratorFactory(new NodeContext<TNode>(node, position)));
      return ref _AcceptedNodes.GetLast();
    }

    // SkipNode: swallow the just-scheduled node (no visit) but keep its enumerator resident so its
    // children promote into its slot. This is what makes the two deques diverge.
    public void SkipCurrentNode() => _AcceptedNodes.RemoveLast();

    // SkipDescendants: accept the node but prune its children by disposing its unused enumerator.
    public void DisposeCurrentEnumerator() => _Enumerators.GetLast().Dispose();

    // Emit the active accepted node's next visit (its first, or a between/after-children return visit);
    // returns its state so the driver can publish it without a second lookup.
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ref DepthFirstNodeState<TNode> TakeNextVisit()
    {
      ref var node = ref _AcceptedNodes.GetLast();
      node.VisitCount++;
      _DepthOfLastVisitedNode = node.Position.Depth;
      return ref node;
    }

    // Pop the active (exhausted) level: remove its accepted node if it has one (a skipped level has
    // none) and dispose + remove its child enumerator.
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void PopFinishedLevel()
    {
      if (_AcceptedNodes.Count > 0 && _AcceptedNodes.GetLast().Position.Depth == Depth)
        _AcceptedNodes.RemoveLast();

      _Enumerators.RemoveLast().Dispose();
    }

    // The level we returned to already took its return visit (its node was the last one visited), so it
    // owes nothing now -- the driver should descend into its next child instead.
    public bool TopLevelAlreadyVisited => Depth == _DepthOfLastVisitedNode;

    // The active level's accepted top is shallower than the raw depth, so this level's node was removed
    // by SkipNode -- only its enumerator remains, to promote children.
    public bool TopLevelWasSkipped => _AcceptedNodes.GetLast().Position.Depth < Depth;

    // SkipSiblings: silence the remaining siblings of the just-scheduled node by disposing every
    // enumerator that would still yield them (its skipped ancestors up to its nearest accepted one).
    // Returns true if the node was an effective root, so the driver ends root enumeration.
    public bool SkipRemainingSiblings()
    {
      var wasEffectiveRoot = _AcceptedNodes.Count == 1;

      var parentDepth = wasEffectiveRoot ? 0 : _AcceptedNodes.GetFromBack(1).Position.Depth;
      var depthDelta = _Enumerators.Count - parentDepth;

      for (int i = 1; i < depthDelta; i++)
        _Enumerators.GetFromBack(i).Dispose();

      return wasEffectiveRoot;
    }

    public void Dispose()
    {
      if (_Enumerators == null)
        return;

      while (_Enumerators.Count > 0)
        _Enumerators.RemoveLast().Dispose();
    }
  }
}
