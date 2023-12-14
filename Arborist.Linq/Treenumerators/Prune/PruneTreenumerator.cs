using System;

namespace Arborist.Linq.Treenumerators
{
  internal class PruneTreenumerator<TNode> : ITreenumerator<TNode>
  {
    public PruneTreenumerator(
      ITreenumerator<TNode> innerTreenumerator,
      Func<NodeVisit<TNode>, bool> predicate,
      PruneOptions pruneOptions)
    {
      _InnerTreenumerator = innerTreenumerator;
      _Predicate = predicate;
      _PruneOptions = pruneOptions;
    }

    private readonly ITreenumerator<TNode> _InnerTreenumerator;

    private readonly Func<NodeVisit<TNode>, bool> _Predicate;
    private readonly PruneOptions _PruneOptions;

    private bool _PruneCurrentBranch;

    public NodeVisit<TNode> Current => _InnerTreenumerator.Current;

    private bool _StartedEnumeration;

    public bool MoveNext(bool skipChildren)
    {
      //skipChildren = _PruneCurrentBranch || skipChildren;

      //_PruneCurrentBranch = false;

      if (_StartedEnumeration && _Predicate(_InnerTreenumerator.Current))
        skipChildren = true;

      _StartedEnumeration = true;

      if (!_InnerTreenumerator.MoveNext(skipChildren))
        return false;

      //_PruneCurrentBranch = true;

      return true;
    }

    public void Dispose()
    {
      _InnerTreenumerator?.Dispose();
    }
  }
}
