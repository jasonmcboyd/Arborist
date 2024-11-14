using System;
using System.Collections;
using System.Collections.Generic;

namespace Arborist.Linq.Enumerables
{
  internal class LookAheadSharedEnumerable<T> : IEnumerable<T>
  {
    public LookAheadSharedEnumerable(IEnumerable<T> enumerable)
    {
      _Enumerable = enumerable;
    }

    private readonly IEnumerable<T> _Enumerable;
    private LookAheadSharedEnumerator<T> _SharedEnumerator;

    public event EventHandler EnumeratorRequested;

    public IEnumerator<T> GetEnumerator() => GetSharedEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public LookAheadSharedEnumerator<T> GetSharedEnumerator()
    {
      if (_SharedEnumerator == null)
        _SharedEnumerator = new LookAheadSharedEnumerator<T>(_Enumerable, this);

      EnumeratorRequested?.Invoke(this, EventArgs.Empty);

      return _SharedEnumerator;
    }
  }
}
