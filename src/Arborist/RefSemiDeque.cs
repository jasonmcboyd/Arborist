using System;
using System.Collections;
using System.Collections.Generic;

namespace Arborist
{
  public class RefSemiDeque<T> : IEnumerable<T>
  {
    public RefSemiDeque() : this(8) { }

    public RefSemiDeque(int capacity)
    {
      Capacity = capacity;
      _Partitions = new List<T[]>()
      {
        new T[capacity]
      };
    }

    public int Capacity { get; private set; }
    public int Count { get; private set; }

    private List<T[]> _Partitions;
    private int _CurrentPartitionIndex = 0;
    private T[] CurrentPartition => _Partitions[_CurrentPartitionIndex];
    private int _TailPointerOffset = 0;
    private int _HeadPointerOffset = 0;

    public ref T GetFirst()
    {
      if (Count == 0)
        throw new InvalidOperationException("The stack is empty.");

      return ref _Partitions[0][_TailPointerOffset];
    }

    public ref T RemoveFirst()
    {
      if (Count == 0)
        throw new InvalidOperationException("The stack is empty.");

      ref var result = ref _Partitions[0][_TailPointerOffset];

      Count--;
      _TailPointerOffset++;

      if (Count == 0)
      {
        _TailPointerOffset = 0;
        _HeadPointerOffset = 0;
      }
      else if (_TailPointerOffset == _Partitions[0].Length)
      {
        _CurrentPartitionIndex--;
        var partition = _Partitions[0];
        _Partitions.RemoveAt(0);
        _Partitions.Add(partition);
        _TailPointerOffset = 0;
      }

      return ref result;
    }

    public void AddLast(T item)
    {
      if (CurrentPartition.Length == _HeadPointerOffset)
        AddPartitionOrMoveToNextPartition();

      CurrentPartition[_HeadPointerOffset] = item;
      _HeadPointerOffset++;
      Count++;
    }

    public ref T RemoveLast()
    {
      if (Count == 0)
        throw new InvalidOperationException("The stack is empty.");

      Count--;
      _HeadPointerOffset--;

      ref var item = ref CurrentPartition[_HeadPointerOffset];

      if (Count == 0)
      {
        _CurrentPartitionIndex = 0;
        _HeadPointerOffset = 0;
        _TailPointerOffset = 0;
      }
      else if (_HeadPointerOffset == 0)
      {
        _CurrentPartitionIndex--;
        _HeadPointerOffset = CurrentPartition.Length;
      }

      return ref item;
    }

    public ref T GetLast()
    {
      if (Count == 0)
        throw new InvalidOperationException("The stack is empty.");

      return ref CurrentPartition[_HeadPointerOffset - 1];
    }

    public ref T GetFromBack(int index)
    {
      if (Count == 0)
        throw new InvalidOperationException("The stack is empty.");

      if (index < 0 || index >= Count)
        throw new IndexOutOfRangeException();

      GetPartitionAndOffset(index, out var partition, out var offset);

      return ref _Partitions[partition][offset];
    }

    private void AddPartitionOrMoveToNextPartition()
    {
      if (_CurrentPartitionIndex == _Partitions.Count - 1)
      {
        var newPartition = new T[CurrentPartition.Length * 2];
        _Partitions.Add(newPartition);
        Capacity += newPartition.Length;
      }

      _CurrentPartitionIndex++;
      _HeadPointerOffset = 0;
    }

    private void GetPartitionAndOffset(int index, out int partition, out int offset)
    {
      if (index < _HeadPointerOffset)
      {
        partition = _CurrentPartitionIndex;
        offset = _HeadPointerOffset - 1 - index;
        return;
      }

      index -= _HeadPointerOffset;

      var arrayIndex = _Partitions.Count - 2;
      var array = _Partitions[arrayIndex];

      while (array.Length <= index)
      {
        index -= array.Length;
        arrayIndex--;
        array = _Partitions[arrayIndex];
      }

      partition = arrayIndex;
      offset = array.Length - 1 - index;
    }

    #region IEnumerable

    public IEnumerator<T> GetEnumerator()
    {
      if (Count == 0)
        yield break;

      if (CurrentPartition == _Partitions[0])
      {
        for (var offset = _TailPointerOffset; offset < _HeadPointerOffset; offset++)
          yield return CurrentPartition[offset];

        yield break;
      }

      for (var offset = _TailPointerOffset; offset < _Partitions[0].Length; offset++)
        yield return _Partitions[0][offset];

      for (var partition = 1; partition < _CurrentPartitionIndex - 2; partition++)
        for (var offset = 0; offset < _Partitions[partition].Length; offset++)
          yield return _Partitions[partition][offset];

      for (var offset = 0; offset < _HeadPointerOffset; offset++)
        yield return _Partitions[_CurrentPartitionIndex][offset];
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    #endregion IEnumerable

    private struct PartitionAndOffset
    {
      public PartitionAndOffset(int partition, int offset)
      {
        Partition = partition;
        Offset = offset;
      }

      public int Partition { get; }

      public int Offset { get; }

      public void Deconstruct(out int partition, out int offset)
      {
        partition = Partition;
        offset = Offset;
      }
    }
  }
}
