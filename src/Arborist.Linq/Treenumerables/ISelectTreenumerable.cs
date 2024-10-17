using Arborist.Core;
using System;

namespace Arborist.Linq.Treenumerables
{
  public interface ISelectTreenumerable<TResult> : ITreenumerable<TResult>
  {
    ISelectTreenumerable<TOuterResult> Compose<TOuterResult>(Func<NodeContext<TResult>, TOuterResult> _outerSelector);
  }
}
