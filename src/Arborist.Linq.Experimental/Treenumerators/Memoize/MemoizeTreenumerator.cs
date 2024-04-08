using Arborist.Core;
using System;

namespace Arborist.Linq.Experimental.Treenumerators
{
  internal class MemoizeTreenumerator<TNode> : TreenumeratorBase<TNode>
  {
    public MemoizeTreenumerator(Func<int, MoveNextResult<TNode>> generator)
    {
      _Generator = generator;
    }

    private Func<int, MoveNextResult<TNode>> _Generator;

    private int _Index = 0;
    private bool _GeneratorExhausted;

    protected override bool OnMoveNext(TraversalStrategy traversalStrategy)
    {
      throw new NotImplementedException();
      //if (_GeneratorExhausted)
      //  return false;

      //var result = _Generator(_Index);

      //_Index++;

      //if (result.HadNext)
      //{
      //  Current = result.Current;
      //  return true;
      //}

      //_GeneratorExhausted = true;

      //_Generator = null;
      
      //return false;
    }

    public override void Dispose()
    {
    }
  }
}
