﻿using Arborist.Core;
using Arborist.Linq.Treenumerators;
using System;

namespace Arborist.Linq
{
  public static partial class Treenumerable
  {
    public static ITreenumerable<TNode> Do<TNode>(
      this ITreenumerable<TNode> source,
      Action<NodeVisit<TNode>> onNext)
    {
      // Treat a null onNext as a no-op, this allows me to simply return the
      // source treenumerable.
      if (onNext == null)
        return source;

      return
        TreenumerableFactory
        .Create(
          source,
          breadthFirstEnumerator => new DoTreenumerator<TNode>(breadthFirstEnumerator, onNext),
          depthFirstEnumerator => new DoTreenumerator<TNode>(depthFirstEnumerator, onNext));
    }
  }
}
