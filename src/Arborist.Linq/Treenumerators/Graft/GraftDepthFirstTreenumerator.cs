using Arborist.Linq.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Arborist.Linq.Treenumerators
{
  internal class GraftDepthFirstTreenumerator<TInner, TNode>
    : TreenumeratorWrapper<TInner, TNode>
  {
    public GraftDepthFirstTreenumerator(
      ITreenumerator<TInner> innerTreenumerator,
      Func<NodeVisit<TInner>, TNode> selector,
      Func<NodeVisit<TInner>, ITreenumerable<TNode>> scionGenerator,
      Func<NodeVisit<TInner>, bool> predicate)
      : base(innerTreenumerator)
    {
      _ScionGenerator = scionGenerator;
      _Selector = selector;
      _Predicate = predicate;
    }

    private readonly Func<NodeVisit<TInner>, TNode> _Selector;
    private readonly Func<NodeVisit<TInner>, ITreenumerable<TNode>> _ScionGenerator;
    private readonly Func<NodeVisit<TInner>, bool> _Predicate;

    private readonly List<NodeVisit<TInner>> _InnerBranch = new List<NodeVisit<TInner>>();
    private readonly List<NodeVisit<TNode>> _ScionBranch = new List<NodeVisit<TNode>>();

    private ITreenumerator<TNode> _Scion;

    private bool _ReturnedFromScion = false;

    private int CalculateInnerSiblingIndexAfterMoveNext()
    {
      throw new NotImplementedException();
      //if (InnerTreenumerator.Node.OriginalPosition.Depth == 0)
      //  return InnerTreenumerator.Node.OriginalPosition.SiblingIndex;

      //if (InnerTreenumerator.Node.OriginalPosition.Depth == _InnerBranch.Last().OriginalPosition.Depth)
      //  return _InnerBranch.Last().OriginalPosition.SiblingIndex;

      //return _InnerBranch.Last().VisitCount - 1;
    }

    private bool OnTreenumeratorMoveNext<T>(
      ITreenumerator<T> treenumerator,
      List<NodeVisit<T>> branch,
      Func<NodeVisit<T>, NodeVisit<TNode>> selector,
      Func<int> siblingIndexCalculator,
      Func<int> depthCalculator,
      SchedulingStrategy schedulingStrategy)
    {
      // TODO:
      throw new NotImplementedException();
      //if (!treenumerator.MoveNext(schedulingStrategy))
      //{
      //  if (branch.Count > 0)
      //    branch.RemoveLast();

      //  return false;
      //}

      //var siblingIndex = siblingIndexCalculator();
      //var depth = depthCalculator();

      //var nextVisit =
      //  NodeVisit
      //  .Create(
      //    treenumerator.Current.Node,
      //    treenumerator.Current.VisitCount,
      //    siblingIndex,
      //    depth);

      //if (branch.Count == 0)
      //{
      //  branch.Add(nextVisit);
      //  Current = selector(nextVisit);
      //  return true;
      //}

      //var previousVisit = branch.Last();

      //if (previousVisit.Depth < nextVisit.Depth)
      //  branch.Add(nextVisit);
      //else if (previousVisit.Depth == nextVisit.Depth)
      //{
      //  if (nextVisit.Depth == previousVisit.Depth && nextVisit.VisitCount == previousVisit.VisitCount)
      //    nextVisit = nextVisit.WithVisitCount(previousVisit.VisitCount + 1);

      //  branch.ReplaceLast(nextVisit);
      //}
      //else
      //{
      //  branch.RemoveLast();

      //  if (nextVisit.Depth == branch.Last().Depth && nextVisit.VisitCount == branch.Last().VisitCount)
      //    nextVisit = nextVisit.WithVisitCount(branch.Last().VisitCount + 1);

      //  branch.ReplaceLast(nextVisit);
      //}

      //Current = selector(nextVisit);
      //return true;
    }

    private bool OnScionMoveNext(SchedulingStrategy schedulingStrategy)
    {
      throw new NotImplementedException();
      //return OnTreenumeratorMoveNext(
      //  _Scion,
      //  _ScionBranch,
      //  visit => visit,
      //  () => _InnerBranch.Last().VisitCount + _Scion.Node.OriginalPosition.SiblingIndex - 1,
      //  () => _InnerBranch.Last().OriginalPosition.Depth + _Scion.Node.OriginalPosition.Depth + 1,
      //  schedulingStrategy);
    }

    private bool OnInnerMoveNext(SchedulingStrategy schedulingStrategy)
    {
      throw new NotImplementedException();
      //return OnTreenumeratorMoveNext(
      //  InnerTreenumerator,
      //  _InnerBranch,
      //  visit => visit.WithNode(_Selector(visit)),
      //  () => CalculateInnerSiblingIndexAfterMoveNext(),
      //  () => InnerTreenumerator.Node.OriginalPosition.Depth,
      //  schedulingStrategy);
    }

    protected override bool OnMoveNext(SchedulingStrategy schedulingStrategy)
    {
      // TODO:
      throw new NotImplementedException();
      //if (_Scion == null
      //  && schedulingStrategy == SchedulingStrategy.ScheduleForTraversal
      //  && _InnerBranch.Count > 0
      //  && _Predicate(_InnerBranch.Last()))
      //  _Scion = _ScionGenerator(InnerTreenumerator.Current).GetDepthFirstTreenumerator();

      //if (_Scion != null)
      //{
      //  if (OnScionMoveNext(schedulingStrategy))
      //    return true;

      //  _Scion = null;

      //  var nextVisit = _InnerBranch.Last().IncrementVisitCount();

      //  _InnerBranch.ReplaceLast(nextVisit);

      //  Current = nextVisit.WithNode(_Selector(nextVisit));

      //  _ReturnedFromScion = true;

      //  return true;
      //}

      //if (!_ReturnedFromScion)
      //  return OnInnerMoveNext(schedulingStrategy);

      //_ReturnedFromScion = false;

      //var priorVisit = _InnerBranch.Last();

      //var onMoveNext = OnInnerMoveNext(schedulingStrategy);

      //if (priorVisit.Depth != Current.Depth)
      //  return onMoveNext;

      //_InnerBranch.RemoveLast();

      //return OnInnerMoveNext(schedulingStrategy);
    }
  }
}
