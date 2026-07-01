using Copse.Core;
using System;

namespace Copse.Linq.Treenumerables
{
  public interface ISelectTreenumerable<TResult> : ITreenumerable<TResult>
  {
    ISelectTreenumerable<TOuterResult> Compose<TOuterResult>(Func<NodeContext<TResult>, TOuterResult> _outerSelector);
  }
}
