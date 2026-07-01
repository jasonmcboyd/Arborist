using Copse.Core;
using System;

namespace Copse.Linq.Experimental.Treenumerables
{
  public interface ITreenumerableBuffer<TNode> : ITreenumerable<TNode>, IDisposable
  {
  }
}
