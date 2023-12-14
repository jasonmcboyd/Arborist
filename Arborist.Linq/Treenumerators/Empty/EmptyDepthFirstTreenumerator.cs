using System;

namespace Arborist.Linq.Treenumerators
{
  internal class EmptyDepthFirstTreenumerator<TNode> : ITreenumerator<TNode>
  {
    private EmptyDepthFirstTreenumerator()
    {
    }

    private static readonly EmptyDepthFirstTreenumerator<TNode> _Instance = new EmptyDepthFirstTreenumerator<TNode>();
    public static EmptyDepthFirstTreenumerator<TNode> Instance => _Instance;

    public NodeVisit<TNode> Current => throw new InvalidOperationException();

    public bool MoveNext(bool skipChildren)
    {
      return false;
    }

    public void Dispose()
    {
    }
  }
}
