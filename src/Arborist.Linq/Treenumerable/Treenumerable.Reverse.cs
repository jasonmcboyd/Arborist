using Arborist.Core;
using Arborist.Linq.Extensions;
using Arborist.Nodes;
using System;

namespace Arborist.Linq
{
  public static partial class Treenumerable
  {
    public static ITreenumerable<TNode> Reverse<TNode>(
      this ITreenumerable<TNode> source)
    {
      //return source.ToPreorderTreeEnumerable().ToReverseTreeRoots().ToTreenumerable();
      throw new NotImplementedException();
    }
  }
}
