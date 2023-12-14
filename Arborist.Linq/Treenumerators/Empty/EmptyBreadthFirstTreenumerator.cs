using System;

namespace Arborist.Linq.Treenumerators
{
  internal class EmptyBreadthFirstTreenumerator<TNode> : ITreenumerator<TNode>
  {
    private EmptyBreadthFirstTreenumerator()
    {
    }

    private static readonly EmptyBreadthFirstTreenumerator<TNode> _Instance = new EmptyBreadthFirstTreenumerator<TNode>();
    public static EmptyBreadthFirstTreenumerator<TNode> Instance => _Instance;

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
