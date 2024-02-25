using Arborist.Core;
using System;

namespace Arborist.Linq.Treenumerables
{
  public interface ITreenumerableBuffer<TNode> : ITreenumerable<TNode>, IDisposable
  {
  }
}
