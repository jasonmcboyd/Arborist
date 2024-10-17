namespace Arborist.Treenumerables
{
  public delegate bool MoveNextChildDelegate<TChildEnumerator, TNode>(ref TChildEnumerator childEnumerator, out NodeAndSiblingIndex<TNode> childNodeAndSiblingIndex);
  public delegate void DisposeChildEnumeratorDelegate<TChildEnumerator>(ref TChildEnumerator childEnumerator);
}
