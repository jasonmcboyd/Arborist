namespace Arborist.Nodes
{
  public interface INode<out TValue, out TChildEnumerator>
  {
    TValue Value { get; }
    TChildEnumerator GetChildEnumerator();
  }
}
