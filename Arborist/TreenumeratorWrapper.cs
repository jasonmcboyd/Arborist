namespace Arborist
{
  public abstract class TreenumeratorWrapper<TInner, TNode>
    : TreenumeratorBase<TNode>
  {
    public TreenumeratorWrapper(ITreenumerator<TInner> innerTreenumerator)
    {
      InnerTreenumerator = innerTreenumerator;
    }

    protected ITreenumerator<TInner> InnerTreenumerator { get; }

    public override void Dispose()
    {
      InnerTreenumerator?.Dispose();
    }
  }

  public abstract class TreenumeratorWrapper<TNode> : TreenumeratorWrapper<TNode, TNode>
  {
    protected TreenumeratorWrapper(ITreenumerator<TNode> InnerTreenumerator)
      : base(InnerTreenumerator)
    {
    }
  }
}
