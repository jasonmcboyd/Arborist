using System.Collections.Generic;

namespace Arborist.Treenumerables.Treenumerators
{
  internal sealed class IndexableDepthFirstTreenumerator<TNode, TValue>
    : TreenumeratorBase<TValue>
    where TNode : INodeWithIndexableChildren<TNode, TValue>
  {
    public IndexableDepthFirstTreenumerator(IEnumerable<TNode> roots)
    {
      _RootsEnumerator = roots.GetEnumerator();
    }

    private readonly IEnumerator<TNode> _RootsEnumerator;

    private readonly Stack<DepthFirstTraversalStep> _CurrentBranch = new Stack<DepthFirstTraversalStep>();

    private int _RootsEnumerationIndex = -1;
    private bool _SkipChildrenOnMoveNext = false;

    private void PushCurrent(
      TNode node,
      int visitCount,
      int siblingIndex,
      int depth,
      TraversalAction traversalAction)
    {
      _CurrentBranch
        .Push(
          new DepthFirstTraversalStep(
            NodeVisit.Create(node, visitCount, siblingIndex, depth),
            traversalAction));

      Current = _CurrentBranch.Peek().NodeVisit.WithNode(node.Value);
    }

    protected override bool OnMoveNext(bool skipChildren)
    {
      var shouldSkipChildren = _SkipChildrenOnMoveNext || skipChildren;

      _SkipChildrenOnMoveNext = false;

      if (_CurrentBranch.Count == 0)
      {
        if (shouldSkipChildren || !_RootsEnumerator.MoveNext())
          return false;

        _RootsEnumerationIndex++;

        PushCurrent(_RootsEnumerator.Current, 1, _RootsEnumerationIndex, 0, TraversalAction.Descended);

        return true;
      }

      var currentDepthFirstTraversalStep = _CurrentBranch.Peek();

      var nextChildIndex = currentDepthFirstTraversalStep.NodeVisit.VisitCount - 1;

      if (!shouldSkipChildren && _CurrentBranch.Peek().NodeVisit.Node.ChildCount > nextChildIndex)
      {
        PushCurrent(
          _CurrentBranch.Peek().NodeVisit.Node[nextChildIndex],
          1,
          nextChildIndex,
          Current.Depth + 1,
          TraversalAction.Descended);

        return true;
      }

      if (currentDepthFirstTraversalStep.TraversalAction == TraversalAction.Descended)
      {
        var previousVisit = _CurrentBranch.Pop();

        PushCurrent(
          previousVisit.NodeVisit.Node,
          previousVisit.NodeVisit.VisitCount + 1,
          Current.SiblingIndex,
          Current.Depth,
          TraversalAction.Ascended);

        _SkipChildrenOnMoveNext = skipChildren;

        return true;
      }

      _CurrentBranch.Pop();

      if (_CurrentBranch.Count > 0)
      {
        var previousVisit = _CurrentBranch.Pop();

        PushCurrent(
          previousVisit.NodeVisit.Node,
          previousVisit.NodeVisit.VisitCount + 1,
          previousVisit.NodeVisit.SiblingIndex,
          previousVisit.NodeVisit.Depth,
          TraversalAction.Ascended);


        //Current = _CurrentBranch.Peek().NodeVisit;

        //Current = NodeVisit.Create(Current.Node, Current.VisitCount + 1, Current.SiblingIndex, Current.Depth);

        //currentDepthFirstTraversalStep = _CurrentBranch.Pop();

        //_CurrentBranch.Push(new DepthFirstTraversalStep(Current, TraversalAction.Ascended));

        return true;
      }

      if (_RootsEnumerator.MoveNext())
      {
        _RootsEnumerationIndex++;

        PushCurrent(_RootsEnumerator.Current, 1, _RootsEnumerationIndex, 0, TraversalAction.Descended);

        return true;
      }

      return false;
    }

    public override void Dispose()
    {
      _RootsEnumerator?.Dispose();
    }

    #region Private Types

    private enum TraversalAction
    {
      Ascended,
      Descended
    }

    private readonly struct DepthFirstTraversalStep
    {
      public DepthFirstTraversalStep(
        NodeVisit<TNode> nodeVisit,
        TraversalAction traversalAction)
      {
        NodeVisit = nodeVisit;
        TraversalAction = traversalAction;
      }

      public NodeVisit<TNode> NodeVisit { get; }
      public TraversalAction TraversalAction { get; }
    }

    #endregion Private Types
  }
}
