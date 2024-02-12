using Nito.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Arborist.Treenumerables.Treenumerators
{
  internal sealed class BreadthFirstTreenumerator<TNode>
    : TreenumeratorBase<TNode>
  {
    public BreadthFirstTreenumerator(IEnumerable<INodeWithEnumerableChildren<TNode>> rootNodes)
    {
      INodeWithEnumerableChildren<TNode> sentinalNode = new SentinalNode(rootNodes);
      var sentinalNodeVisit = NodeVisit.Create(sentinalNode, 1, (0, -1), default, false);
      _CurrentLevel.AddToFront(sentinalNodeVisit);
      Current = sentinalNodeVisit.WithNode(sentinalNode.Value);
    }

    private Deque<NodeVisit<INodeWithEnumerableChildren<TNode>>> _CurrentLevel =
      new Deque<NodeVisit<INodeWithEnumerableChildren<TNode>>>();

    private Deque<NodeVisit<INodeWithEnumerableChildren<TNode>>> _NextLevel =
      new Deque<NodeVisit<INodeWithEnumerableChildren<TNode>>>();

    private Stack<IEnumerator<INodeWithEnumerableChildren<TNode>>> _ChildrenStack =
      new Stack<IEnumerator<INodeWithEnumerableChildren<TNode>>>();

    protected override bool OnMoveNext(SchedulingStrategy schedulingStrategy)
    {
      if (State == TreenumeratorState.EnumerationFinished)
        return false;

      if (State == TreenumeratorState.EnumerationNotStarted)
        State = TreenumeratorState.VisitingNode;

      while (true)
      {
        if (_CurrentLevel.Count == 0)
          (_CurrentLevel, _NextLevel) = (_NextLevel, _CurrentLevel);

        if (_CurrentLevel.Count == 0)
        {
          State = TreenumeratorState.EnumerationFinished;
          return false;
        }

        if (State == TreenumeratorState.SchedulingNode)
        {
          var onScheduling = OnScheduling(schedulingStrategy);

          if (onScheduling.HasValue)
          {
            return onScheduling.Value;
          }
          else
          {
            State = TreenumeratorState.VisitingNode;
            continue;
          }
        }

        var onVisiting = OnVisiting();

        if (onVisiting.HasValue)
          return onVisiting.Value;
      }
    }

    private bool? OnScheduling(SchedulingStrategy schedulingStrategy)
    {
      var previousVisit = _CurrentLevel.RemoveFromFront();

      if (schedulingStrategy == SchedulingStrategy.SkipDescendantSubtrees)
      {
        // TODO: This is a temporary hack.
        INodeWithEnumerableChildren<TNode> node =
          new SentinalNode(
            previousVisit.Node.Value,
            Enumerable.Empty<INodeWithEnumerableChildren<TNode>>());

        previousVisit = previousVisit.WithNode(node);
      }

      if (schedulingStrategy == SchedulingStrategy.SkipNode)
        previousVisit = previousVisit.Skip();

      if (schedulingStrategy != SchedulingStrategy.SkipSubtree)
      {
        if (_CurrentLevel[0].Skipped)
          //&& (_CurrentLevel[0].Depth != -1 || previousVisit.Skipped))
          _CurrentLevel.AddToFront(previousVisit);
        else
          _NextLevel.AddToBack(previousVisit);
      }

      if (_CurrentLevel.Count > 0)
      {
        var nextVisit = _CurrentLevel.RemoveFromFront().IncrementVisitCount();
        _CurrentLevel.AddToFront(nextVisit);

        Current = nextVisit.WithNode(nextVisit.Node.Value);

        State = TreenumeratorState.VisitingNode;

        return
          Current.OriginalPosition.Depth == -1
          ? (bool?)null
          : true;
      }

      if (MoveNextChild())
        return true;

      return null;
    }

    private bool? OnVisiting()
    {
      var previousVisit = _CurrentLevel[0];

      if (previousVisit.VisitCount == 0)
      {
        IncrementVisit();
        return true;
      }

      if (previousVisit.VisitCount == 1
        && Current.OriginalPosition.Depth == previousVisit.OriginalPosition.Depth)
        _ChildrenStack.Push(previousVisit.Node.Children.GetEnumerator());

      if (Current.OriginalPosition.Depth > previousVisit.OriginalPosition.Depth)
      {
        IncrementVisit();
        return
          Current.OriginalPosition.Depth == -1
          ? (bool?)null
          : true;
      }

      if (Current.OriginalPosition.Depth < previousVisit.OriginalPosition.Depth)
      {
        IncrementVisit();
        return true;
      }

      if (MoveNextChild())
        return true;

      if (previousVisit.VisitCount == 1)
      {
        IncrementVisit();
        _CurrentLevel.RemoveFromFront();
        return
          Current.OriginalPosition.Depth == -1
          ? (bool?)null
          : true;
      }

      _CurrentLevel.RemoveFromFront();

      return null;
    }

    private void IncrementVisit()
    {
      var previousVisit = _CurrentLevel[0].IncrementVisitCount();

      _CurrentLevel[0] = previousVisit;

      Current = previousVisit.WithNode(previousVisit.Node.Value);

      State = TreenumeratorState.VisitingNode;
    }

    private bool MoveNextChild()
    {
      var children = _ChildrenStack.Pop();

      if (children.MoveNext())
      {
        var siblingIndex = Current.VisitCount - 1; ;
        var depth = Current.OriginalPosition.Depth + 1;

        var childNode = NodeVisit.Create(children.Current, 0, (siblingIndex, depth), default, false);
        _CurrentLevel.AddToFront(childNode);
        Current = childNode.WithNode(childNode.Node.Value);
        State = TreenumeratorState.SchedulingNode;
        _ChildrenStack.Push(children);
        return true;
      }

      children.Dispose();

      return false;
    }

    public override void Dispose()
    {
      while (_ChildrenStack.Count > 0)
        _ChildrenStack.Pop().Dispose();
    }

    private class SentinalNode : INodeWithEnumerableChildren<TNode>
    {
      public SentinalNode(
        IEnumerable<INodeWithEnumerableChildren<TNode>> rootNodes)
        : this(default, rootNodes)
      {
      }

      public SentinalNode(
        TNode value,
        IEnumerable<INodeWithEnumerableChildren<TNode>> rootNodes)
      {
        Value = value;
        Children = rootNodes;
      }

      public TNode Value { get; private set; }

      public IEnumerable<INodeWithEnumerableChildren<TNode>> Children { get; private set; }
    }
  }
}
