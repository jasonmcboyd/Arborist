namespace Arborist.Treenumerables
{
  public delegate TryMoveNextChildResult<TNode> TryMoveNextChildDelegate<TChildEnumerator, TNode>(ref TChildEnumerator childEnumerator);
  public delegate void DisposeChildEnumeratorDelegate<TChildEnumerator>(ref TChildEnumerator childEnumerator);
}
