using Nito.Collections;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Arborist.Treenumerables.Treenumerators
{
  internal sealed class BreadthFirstTreenumerator<TNode>
    : TreenumeratorBase<TNode>
  {
    public BreadthFirstTreenumerator(
      IEnumerable<TNode> rootNodes,
      Func<TNode, IEnumerator<TNode>> childrenGetter)
    {
      _RootsEnumerator = rootNodes.GetEnumerator();
      _ChildrenGetter = childrenGetter;

      //var sentinalNodeVisit = NodeVisit.Create(default(TNode), 1, (0, -1), default, SchedulingStrategy.ScheduleForTraversal);
      //_CurrentLevel.AddToFront(sentinalNodeVisit);
      //Current = sentinalNodeVisit;
    }

    private readonly IEnumerator<TNode> _RootsEnumerator;

    private readonly VirtualNodeVisitPool<TNode> _NodePool;

    private Deque<VirtualNodeVisit<TNode>> _CurrentLevel =
      new Deque<VirtualNodeVisit<TNode>>();

    private Deque<VirtualNodeVisit<TNode>> _NextLevel =
      new Deque<VirtualNodeVisit<TNode>>();

    private Stack<IEnumerator<TNode>> _ChildrenStack =
      new Stack<IEnumerator<TNode>>();

    private readonly Func<TNode, IEnumerator<TNode>> _ChildrenGetter;

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
      throw new NotImplementedException();
      //var previousVisit = _CurrentLevel.RemoveFromFront();

      //previousVisit = previousVisit.WithSchedulingStrategy(schedulingStrategy);

      //if (schedulingStrategy != SchedulingStrategy.SkipSubtree)
      //{
      //  // TODO: Not sure which predicate to use here.
      //  if (_CurrentLevel[0].SchedulingStrategy == SchedulingStrategy.SkipNode)
      //    //&& (_CurrentLevel[0].Depth != -1 || previousVisit.Skipped))
      //    _CurrentLevel.AddToFront(previousVisit);
      //  else
      //    _NextLevel.AddToBack(previousVisit);
      //}

      //if (_CurrentLevel.Count > 0)
      //{
      //  var nextVisit = _CurrentLevel.RemoveFromFront().IncrementVisitCount();
      //  _CurrentLevel.AddToFront(nextVisit);

      //  Current = nextVisit.WithNode(nextVisit.Node);

      //  State = TreenumeratorState.VisitingNode;

      //  return
      //    Current.OriginalPosition.Depth == -1
      //    ? (bool?)null
      //    : true;
      //}

      //if (MoveNextChild())
      //  return true;

      //return null;
    }

    private bool? OnVisiting()
    {
      throw new NotImplementedException();
      //var previousVisit = _CurrentLevel[0];

      //if (previousVisit.VisitCount == 0)
      //{
      //  IncrementVisit();
      //  return true;
      //}

      //if (previousVisit.VisitCount == 1
      //  && Current.OriginalPosition.Depth == previousVisit.OriginalPosition.Depth)
      //  _ChildrenStack.Push(GetChildren(previousVisit));

      //if (Current.OriginalPosition.Depth > previousVisit.OriginalPosition.Depth)
      //{
      //  IncrementVisit();
      //  return
      //    Current.OriginalPosition.Depth == -1
      //    ? (bool?)null
      //    : true;
      //}

      //if (Current.OriginalPosition.Depth < previousVisit.OriginalPosition.Depth)
      //{
      //  IncrementVisit();
      //  return true;
      //}

      //if (MoveNextChild())
      //  return true;

      //if (previousVisit.VisitCount == 1)
      //{
      //  IncrementVisit();
      //  _CurrentLevel.RemoveFromFront();
      //  return
      //    Current.OriginalPosition.Depth == -1
      //    ? (bool?)null
      //    : true;
      //}

      //_CurrentLevel.RemoveFromFront();

      //return null;
    }

    private IEnumerator<TNode> GetChildren(VirtualNodeVisit<TNode> visit)
    {
      throw new NotImplementedException();
      //if (visit.OriginalPosition.Depth == -1)
      //  return _RootsEnumerator;

      //if (visit.SchedulingStrategy == SchedulingStrategy.SkipDescendantSubtrees)
      //  return Enumerable.Empty<TNode>().GetEnumerator();

      //return _ChildrenGetter(visit.Node);
    }

    private void IncrementVisit()
    {
      throw new NotImplementedException();
      //var previousVisit = _CurrentLevel[0].IncrementVisitCount();

      //_CurrentLevel[0] = previousVisit;

      //Current = previousVisit.WithNode(previousVisit.Node);

      //State = TreenumeratorState.VisitingNode;
    }

    private bool MoveNextChild()
    {
      throw new NotImplementedException();
      //var children = _ChildrenStack.Pop();

      //if (children.MoveNext())
      //{
      //  var siblingIndex = Current.VisitCount - 1; ;
      //  var depth = Current.OriginalPosition.Depth + 1;

      //  var childNode = NodeVisit.Create(children.Current, 0, (siblingIndex, depth), default, SchedulingStrategy.ScheduleForTraversal);
      //  _CurrentLevel.AddToFront(childNode);
      //  Current = childNode.WithNode(childNode.Node);
      //  State = TreenumeratorState.SchedulingNode;
      //  _ChildrenStack.Push(children);
      //  return true;
      //}

      //children.Dispose();

      //return false;
    }

    public override void Dispose()
    {
      _RootsEnumerator?.Dispose();
      //_NodePool?.Dispose();

      while (_ChildrenStack.Count > 0)
        _ChildrenStack.Pop().Dispose();
    }
  }
}
