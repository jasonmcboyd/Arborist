using System;

namespace Arborist
{
  public abstract class StackTreenumeratorWrapper<TInner, TBranch, TNode>
    : StackTreenumeratorBase<TBranch, TNode>
  {
    public StackTreenumeratorWrapper(
      ITreenumerator<TInner> innerTreenumerator,
      Func<TBranch, TNode> selector)
      : base(selector)
    {
      InnerTreenumerator = innerTreenumerator;
    }

    protected ITreenumerator<TInner> InnerTreenumerator { get; }

    public override void Dispose()
    {
      InnerTreenumerator?.Dispose();
    }
  }

  public abstract class StackTreenumeratorWrapper<TNode>
    : StackTreenumeratorWrapper<TNode, TNode, TNode>
  {
    protected StackTreenumeratorWrapper(ITreenumerator<TNode> innerTreenumerator)
      : base(innerTreenumerator, node => node)
    {
    }
  }
}
