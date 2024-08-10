using Arborist.Core;
using System.Collections;
using System.Collections.Generic;

namespace Arborist.Linq.TreeEnumerable.BreadthFirstTree
{
  internal sealed class BreadthFirstTreeEnumerator<TNode> : IEnumerator<BreadthFirstTreeEnumerableToken<TNode>>
  {
    public BreadthFirstTreeEnumerator(ITreenumerator<TNode> breadthFirstTreenumerator)
    {
      _Treenumerator = breadthFirstTreenumerator;
    }

    private readonly ITreenumerator<TNode> _Treenumerator;
    private int _CurrentLevelDepth = -1;
    private bool _EnumerationStarted = false;

    public BreadthFirstTreeEnumerableToken<TNode> Current { get; private set; }

    object IEnumerator.Current => Current;

    private bool _HasCachedGenerationSeparatorBeforeCachedFamilySeparators = false;
    private bool _HasCachedGenerationSeparatorAfterCachedFamilySeparators = false;
    private int _CachedFamilySeparatorCount = 0;
    private bool _HasCachedNode = false;
    private BreadthFirstTreeEnumerableToken<TNode> _CachedNode;

    private bool HasCachedGenerationSeparator =>
      _HasCachedGenerationSeparatorBeforeCachedFamilySeparators
      || _HasCachedGenerationSeparatorAfterCachedFamilySeparators;

    public bool MoveNext()
    {
      if (_CachedFamilySeparatorCount > 0)
      {
        _CachedFamilySeparatorCount--;
        Current = new BreadthFirstTreeEnumerableToken<TNode>(BreadthFirstTreeEnumerableTokenType.FamilySeparator);
        return true;
      }

      if (_HasCachedNode)
      {
        _HasCachedNode = false;
        Current = _CachedNode;
        return true;
      }

      while (_Treenumerator.MoveNext(NodeTraversalStrategy.TraverseSubtree))
      {
        if (!_EnumerationStarted)
        {
          OnEnumerationStarting();
          return true;
        }

        if (_Treenumerator.Mode == TreenumeratorMode.SchedulingNode)
        {
          OnSchedulingNode();
          return true;
        }

        OnVisitingNode();
      }

      if (_HasCachedGenerationSeparatorAfterCachedFamilySeparators && _CachedFamilySeparatorCount > 0)
      {
        _CachedFamilySeparatorCount--;
        Current = new BreadthFirstTreeEnumerableToken<TNode>(BreadthFirstTreeEnumerableTokenType.FamilySeparator);
        return true;
      }
      else
      {
        _HasCachedGenerationSeparatorAfterCachedFamilySeparators = false;
        _HasCachedGenerationSeparatorBeforeCachedFamilySeparators = false;
        _CachedFamilySeparatorCount = 0;
      }

      if (_EnumerationStarted && Current.Type != BreadthFirstTreeEnumerableTokenType.FamilySeparator)
      {
        Current = new BreadthFirstTreeEnumerableToken<TNode>(BreadthFirstTreeEnumerableTokenType.FamilySeparator);
        return true;
      }

      return false;
    }

    private void OnEnumerationStarting()
    {
      Current = new BreadthFirstTreeEnumerableToken<TNode>(_Treenumerator.Node);
      _EnumerationStarted = true;
    }

    private void OnSchedulingNode()
    {
      if (HasCachedGenerationSeparator)
      {
        _HasCachedGenerationSeparatorAfterCachedFamilySeparators = false;
        _HasCachedGenerationSeparatorBeforeCachedFamilySeparators = false;

        Current = new BreadthFirstTreeEnumerableToken<TNode>(BreadthFirstTreeEnumerableTokenType.GenerationSeparator);
        _HasCachedNode = true;
        _CachedNode = new BreadthFirstTreeEnumerableToken<TNode>(_Treenumerator.Node);
        return;
      }

      if (_CachedFamilySeparatorCount > 0)
      {
        _CachedFamilySeparatorCount--;
        Current = new BreadthFirstTreeEnumerableToken<TNode>(BreadthFirstTreeEnumerableTokenType.FamilySeparator);
        if (!_HasCachedNode)
        {
          _HasCachedNode = true;
          _CachedNode = new BreadthFirstTreeEnumerableToken<TNode>(_Treenumerator.Node);
        }
        return;
      }

      Current = new BreadthFirstTreeEnumerableToken<TNode>(_Treenumerator.Node);
    }

    private void OnVisitingNode()
    {
      if (_Treenumerator.VisitCount != 1)
        return;

      if (_Treenumerator.Position.Depth == _CurrentLevelDepth)
      {
        _CachedFamilySeparatorCount++;
      }
      else
      {
        if (_CachedFamilySeparatorCount == 0)
          _HasCachedGenerationSeparatorBeforeCachedFamilySeparators = true;
        else
          _HasCachedGenerationSeparatorAfterCachedFamilySeparators = true;

        _CurrentLevelDepth++;
      }
    }

    public void Reset()
    {
      // Do nothing.
    }

    public void Dispose()
    {
      _Treenumerator?.Dispose();
    }
  }
}
