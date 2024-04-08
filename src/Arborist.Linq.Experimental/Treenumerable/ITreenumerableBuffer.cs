using Arborist.Core;
using System;

namespace Arborist.Linq.Experimental.Treenumerables
{
  public interface ITreenumerableBuffer<TNode> : ITreenumerable<TNode>, IDisposable
  {
  }
}
