using Arborist.Core;
using System;

namespace Arborist
{
  public abstract class TreenumeratorWrapper<TInner, TNode>
    : TreenumeratorBase<TNode>
  {
    public TreenumeratorWrapper(
      Func<ITreenumerator<TInner>> innerTreenumeratorFactory)
    {
      InnerTreenumerator = innerTreenumeratorFactory();
    }

    protected ITreenumerator<TInner> InnerTreenumerator { get; }

    protected override void OnDisposing()
    {
      base.OnDisposing();

      InnerTreenumerator?.Dispose();
    }
  }

  public abstract class TreenumeratorWrapper<TNode> : TreenumeratorWrapper<TNode, TNode>
  {
    protected TreenumeratorWrapper(
      Func<ITreenumerator<TNode>> innerTreenumeratorFactory)
      : base(innerTreenumeratorFactory)
    {
    }
  }
}
