using Arborist.Core;
using Arborist.Linq.Treenumerators;
using System;

namespace Arborist.Linq
{
  public static partial class Treenumerable
  {
    // SPIKE wiring (spike-dft-extraction): exposes the clean-room DFT extraction treenumerator
    // without touching the production Where(). DFT only; BFT throws.
    public static ITreenumerable<TNode> WhereSpike<TNode>(
      this ITreenumerable<TNode> source,
      Func<NodeContext<TNode>, bool> predicate)
    {
      if (predicate == null)
        return source;

      return
        TreenumerableFactory.Create(
          () => throw new NotImplementedException("WhereSpike: BFT not implemented (DFT spike)."),
          () => new WhereDepthFirstTreenumerator2<TNode>(
            source.GetDepthFirstTreenumerator,
            predicate));
    }
  }
}
