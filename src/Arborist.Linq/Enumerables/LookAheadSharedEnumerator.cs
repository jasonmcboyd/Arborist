using System;
using System.Collections;
using System.Collections.Generic;

namespace Arborist.Linq.Enumerables
{
  internal class LookAheadSharedEnumerator<T> : IEnumerator<T>
  {
    public LookAheadSharedEnumerator(
      IEnumerable<T> enumerable,
      LookAheadSharedEnumerable<T> sharedEnumerable)
    {
      _Enumerator = enumerable.GetEnumerator();
      _SharedEnumerable = sharedEnumerable;
      _SharedEnumerable.EnumeratorRequested += OnEnumeratorRequested;
    }

    private readonly IEnumerator<T> _Enumerator;
    private readonly LookAheadSharedEnumerable<T> _SharedEnumerable;
    private bool _HasCachedValue;
    private T _CachedValue;

    public int RefCount { get; private set; }

    public T Current { get; private set; }

    object IEnumerator.Current => Current;

    private void OnEnumeratorRequested(object sender, EventArgs eventArgs) => RefCount++;

    public bool MoveNext()
    {
      if (_HasCachedValue)
      {
        _HasCachedValue = false;
        Current = _CachedValue;
        _CachedValue = default;
        return true;
      }

      if (!_Enumerator.MoveNext())
        return false;

      Current = _Enumerator.Current;
      return true;
    }

    public bool PeekNext(out T value)
    {
      if (_HasCachedValue)
      {
        value = _CachedValue;
        return true;
      }

      else if (_Enumerator.MoveNext())
      {
        _HasCachedValue = true;
        _CachedValue = _Enumerator.Current;
        value = _CachedValue;
        return true;
      }

      value = default;
      return false;
    }

    public void Reset()
    {
    }

    public void Dispose()
    {
      RefCount--;

      if (RefCount <= 0)
      {
        _Enumerator.Dispose();
        _SharedEnumerable.EnumeratorRequested -= OnEnumeratorRequested;
      }
    }
  }
}
