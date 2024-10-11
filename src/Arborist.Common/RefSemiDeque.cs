using System;
using System.Collections.Generic;

namespace Arborist.Common
{
  public class RefSemiDeque<T>
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
    private int _TailOffset = 0;
    private int _HeadOffset = 0;

    public ref T GetFirst()
    {
      if (Count == 0)
        throw new InvalidOperationException("The stack is empty.");

      return ref _Partitions[0][_HeadOffset];
    }

    public void RemoveFirst()
    {
      if (Count == 0)
        throw new InvalidOperationException("The stack is empty.");

      _Partitions[0][_HeadOffset] = default;

      Count--;
      _HeadOffset++;

      if (Count == 0)
      {
        _HeadOffset = 0;
        _TailOffset = 0;
      }
      else if (_HeadOffset == _Partitions[0].Length)
      {
        Capacity -= _Partitions[0].Length;
        _CurrentPartitionIndex--;
        _Partitions.RemoveAt(0);
        _HeadOffset = 0;
      }
    }

    public void AddLast(T item)
    {
      if (CurrentPartition.Length == _TailOffset)
        AddPartitionOrMoveToNextPartition();

      CurrentPartition[_TailOffset] = item;
      _TailOffset++;
      Count++;
    }

    public T RemoveLast()
    {
      if (Count == 0)
        throw new InvalidOperationException("The stack is empty.");

      Count--;
      _TailOffset--;

      var item = CurrentPartition[_TailOffset];
      CurrentPartition[_TailOffset] = default;

      if (_CurrentPartitionIndex > 0 && _TailOffset == 0)
      {
        _CurrentPartitionIndex--;
        _TailOffset = CurrentPartition.Length;
      }

      return item;
    }

    public ref T GetLast() => ref GetFromBack(0);

    public ref T GetFromBack(int index)
    {
      if (index < 0 || index >= Count)
        throw new IndexOutOfRangeException();

      var (partition, offset) = GetPartitionAndOffset(index);

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
      _TailOffset = 0;
    }

    private PartitionAndOffset GetPartitionAndOffset(int index)
    {
      if (index < _TailOffset)
        return new PartitionAndOffset(_CurrentPartitionIndex, _TailOffset - 1 - index);

      index -= _TailOffset;

      var arrayIndex = _Partitions.Count - 2;
      var array = _Partitions[arrayIndex];

      while (array.Length <= index)
      {
        index -= array.Length;
        arrayIndex--;
        array = _Partitions[arrayIndex];
      }

      return new PartitionAndOffset(arrayIndex, array.Length - 1 - index);
    }

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
