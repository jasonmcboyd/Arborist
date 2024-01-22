using Arborist.Linq.Treenumerators;
using System;

namespace Arborist.Linq
{
  public static partial class Treenumerable
  {
    public static ITreenumerable<IsLeafNode<TNode>> IsLeaf<TNode>(
      this ITreenumerable<TNode> source)
    {
      throw new NotImplementedException();
    }
  }
}
