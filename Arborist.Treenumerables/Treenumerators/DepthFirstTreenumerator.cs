using System;
using System.Collections.Generic;

namespace Arborist.Treenumerables.Treenumerators
{
  internal sealed class DepthFirstTreenumerator<TNode>
    : TreenumeratorBase<TNode>
  {
    public DepthFirstTreenumerator(IEnumerable<INodeWithEnumerableChildren<TNode>> rootNodes)
    {
      _RootsEnumerator = rootNodes.GetEnumerator();
    }

    private readonly IEnumerator<INodeWithEnumerableChildren<TNode>> _RootsEnumerator;

    private int _RootsEnumeratorIndex = 0;

    private readonly Stack<NodeVisit<IEnumerator<INodeWithEnumerableChildren<TNode>>>> _Stack =
      new Stack<NodeVisit<IEnumerator<INodeWithEnumerableChildren<TNode>>>>();

    public int VisitCount
    {
      get
      {
        if (State == TreenumeratorState.EnumerationNotStarted)
          throw new InvalidOperationException("Enumeration has not begun.");

        if (State == TreenumeratorState.EnumerationFinished)
          throw new InvalidOperationException("Enumeration has completed.");

        return _Stack.Peek().VisitCount;
      }
    }

    public int Depth
    {
      get
      {
        if (State == TreenumeratorState.EnumerationNotStarted)
          throw new InvalidOperationException("Enumeration has not begun.");

        if (State == TreenumeratorState.EnumerationFinished)
          throw new InvalidOperationException("Enumeration has completed.");

        return _Stack.Peek().Depth;
      }
    }

    public int SiblingIndex
    {
      get
      {
        if (State == TreenumeratorState.EnumerationNotStarted)
          throw new InvalidOperationException("Enumeration has not begun.");

        if (State == TreenumeratorState.EnumerationFinished)
          throw new InvalidOperationException("Enumeration has completed.");

        return _Stack.Peek().SiblingIndex;
      }
    }

    public bool Skipped
    {
      get
      {
        if (State == TreenumeratorState.EnumerationNotStarted)
          throw new InvalidOperationException("Enumeration has not begun.");

        if (State == TreenumeratorState.EnumerationFinished)
          throw new InvalidOperationException("Enumeration has completed.");

        return _Stack.Peek().Skipped;
      }
    }

    private bool _SkippingDescendants = false;
    private bool _HasCachedChild = false;

    protected override bool OnMoveNext(SchedulingStrategy schedulingStrategy)
    {
      if (_HasCachedChild)
      {
        _HasCachedChild = false;

        Current = _Stack.Peek().WithNode(_Stack.Peek().Node.Current.Value);
        State = TreenumeratorState.SchedulingNode;
        return true;
      }

      if (State == TreenumeratorState.EnumerationFinished)
        return false;

      if (State == TreenumeratorState.EnumerationNotStarted)
        return OnStarting();

      if (State == TreenumeratorState.SchedulingNode)
        return OnScheduling(schedulingStrategy);

      return OnVisiting();
    }

    private bool OnStarting()
    {
      if (!_RootsEnumerator.MoveNext())
      {
        State = TreenumeratorState.EnumerationFinished;
        return false;
      }

      State = TreenumeratorState.SchedulingNode;

      var rootNode = NodeVisit.Create(_RootsEnumerator, 0, _RootsEnumeratorIndex++, 0, false);

      _Stack.Push(rootNode);

      Current = rootNode.WithNode(rootNode.Node.Current.Value);

      return true;
    }

    private bool OnScheduling(SchedulingStrategy schedulingStrategy)
    {
      var previousVisit = _Stack.Pop();

      if (schedulingStrategy == SchedulingStrategy.SkipSubtree)
      {
        if (_Stack.Count == 0)
        {
          if (!previousVisit.Node.MoveNext())
          {
            previousVisit.Node.Dispose();
            State = TreenumeratorState.EnumerationFinished;
            return false;
          }

          previousVisit = NodeVisit.Create(_RootsEnumerator, 0, _RootsEnumeratorIndex++, 0, false);

          _Stack.Push(previousVisit);
          Current = previousVisit.WithNode(previousVisit.Node.Current.Value);
          State = TreenumeratorState.SchedulingNode;
          return true;
        }

        var parentVisit = _Stack.Pop().IncrementVisitCount();
        _Stack.Push(parentVisit);
        Current = parentVisit.WithNode(parentVisit.Node.Current.Value);

        if (previousVisit.Node.MoveNext())
        {
          previousVisit = NodeVisit.Create(previousVisit.Node, 0, previousVisit.SiblingIndex + 1, previousVisit.Depth, false);
          _Stack.Push(previousVisit);
          _HasCachedChild = true;
        }
        else
        {
          previousVisit.Node.Dispose();
        }

        State = TreenumeratorState.VisitingNode;

        return true;
      }

      if (schedulingStrategy == SchedulingStrategy.SkipNode)
        previousVisit = previousVisit.Skip();

      if (schedulingStrategy == SchedulingStrategy.SkipDescendantSubtrees)
        _SkippingDescendants = true;

      previousVisit = previousVisit.IncrementVisitCount();
      _Stack.Push(previousVisit);
      Current = previousVisit.WithNode(previousVisit.Node.Current.Value);
      State = TreenumeratorState.VisitingNode;
      return true;
    }

    private bool OnVisiting()
    {
      var previousVisit = _Stack.Pop();

      if (previousVisit.VisitCount == 0)
      {
        previousVisit = previousVisit.IncrementVisitCount();
        _Stack.Push(previousVisit);
        Current = previousVisit.WithNode(previousVisit.Node.Current.Value);
        return true;
      }

      if (previousVisit.VisitCount == 1)
      {
        var children = previousVisit.Node.Current.Children.GetEnumerator();

        if (children?.MoveNext() != true || _SkippingDescendants)
        {
          previousVisit = previousVisit.IncrementVisitCount();
          _Stack.Push(previousVisit);
          Current = previousVisit.WithNode(previousVisit.Node.Current.Value);
          return true;
        }

        var childrenNode = NodeVisit.Create(children, 0, 0, previousVisit.Depth + 1, false);
        _Stack.Push(previousVisit);
        _Stack.Push(childrenNode);
        Current = childrenNode.WithNode(childrenNode.Node.Current.Value);
        State = TreenumeratorState.SchedulingNode;
        return true;
      }

      if (_Stack.Count == 0)
      {
        if (!previousVisit.Node.MoveNext())
        {
          previousVisit.Node.Dispose();
          return false;
        }

        previousVisit = NodeVisit.Create(_RootsEnumerator, 0, _RootsEnumeratorIndex++, 0, false);

        _Stack.Push(previousVisit);
        Current = previousVisit.WithNode(previousVisit.Node.Current.Value);
        State = TreenumeratorState.SchedulingNode;
        return true;
      }

      var parentVisit = _Stack.Pop().IncrementVisitCount();
      _Stack.Push(parentVisit);
      Current = parentVisit.WithNode(parentVisit.Node.Current.Value);

      if (previousVisit.Node.MoveNext())
      {
        previousVisit = NodeVisit.Create(previousVisit.Node, 0, previousVisit.SiblingIndex + 1, previousVisit.Depth, false);
        _Stack.Push(previousVisit);
        _HasCachedChild = true;
      }
      else
      {
        previousVisit.Node.Dispose();
      }

      return true;
    }

    public override void Dispose()
    {
      _RootsEnumerator?.Dispose();
    }
  }
}
