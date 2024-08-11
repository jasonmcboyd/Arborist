﻿using Arborist.Core;
using System.Collections;
using System.Collections.Generic;

namespace Arborist.Linq.TreeEnumerable.DepthFirstTree
{
  internal sealed class DepthFirstTreeEnumerator<TNode> : IEnumerator<DepthFirstTreeEnumerableToken<TNode>>
  {
    public DepthFirstTreeEnumerator(ITreenumerator<TNode> breadthFirstTreenumerator)
    {
      _Treenumerator = breadthFirstTreenumerator;
    }

    private readonly ITreenumerator<TNode> _Treenumerator;

    public DepthFirstTreeEnumerableToken<TNode> Current { get; private set; }

    object IEnumerator.Current => Current;

    private int _PreviousDepth = 0;
    private bool _HasCachedNode = false;
    private DepthFirstTreeEnumerableToken<TNode> _CachedNode;
    private int _CachedEndChildGroupTokenCount = 0;

    public bool MoveNext()
    {
      if (_CachedEndChildGroupTokenCount > 0)
      {
        _CachedEndChildGroupTokenCount--;
        Current = new DepthFirstTreeEnumerableToken<TNode>(DepthFirstTreeEnumerableTokenType.EndChildGroup);
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
        if (_Treenumerator.Mode != TreenumeratorMode.SchedulingNode)
          continue;

        var depth = _Treenumerator.Position.Depth;

        if (depth != _PreviousDepth)
        {
          OnDepthChanged(depth);
          return true;
        }

        Current = new DepthFirstTreeEnumerableToken<TNode>(_Treenumerator.Node);
        return true;
      }

      return OnTreenumeratorFinished();
    }

    private void OnDepthChanged(int newDepth)
    {
      if (newDepth > _PreviousDepth)
      {
        Current = new DepthFirstTreeEnumerableToken<TNode>(DepthFirstTreeEnumerableTokenType.StartChildGroup);
      }
      else
      {
        _CachedEndChildGroupTokenCount = _PreviousDepth - _Treenumerator.Position.Depth - 1;
        Current = new DepthFirstTreeEnumerableToken<TNode>(DepthFirstTreeEnumerableTokenType.EndChildGroup);
      }

      _HasCachedNode = true;
      _CachedNode = new DepthFirstTreeEnumerableToken<TNode>(_Treenumerator.Node);
      _PreviousDepth = newDepth;
    }

    private bool OnTreenumeratorFinished()
    {
      if (_PreviousDepth <= 0)
        return false;

      _CachedEndChildGroupTokenCount = _PreviousDepth - 1;
      Current = new DepthFirstTreeEnumerableToken<TNode>(DepthFirstTreeEnumerableTokenType.EndChildGroup);
      _PreviousDepth = 0;
      return true;
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
