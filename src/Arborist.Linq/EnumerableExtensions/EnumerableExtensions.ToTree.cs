﻿using Arborist.Core;
using Arborist.Linq.Treenumerables;
using System;
using System.Collections.Generic;

namespace Arborist.Linq
{
  public static partial class EnumerableExtensions
  {
    public static ITreenumerable<TNode> ToTree<TNode>(
      this IEnumerable<TNode> source,
      Func<NodeContext<TNode>, bool> childAdderStrategy)
    {
      return
        new EnumerableTreenumerable<TNode>(source, childAdderStrategy);
    }
  }
}
