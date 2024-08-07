namespace Arborist.Linq.TreeEnumerable
{
  public interface ITreeEnumerableToken<TNode, TTokenType>
  {
    TTokenType Type { get; }
    TNode Node { get; }
  }
}
