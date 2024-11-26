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
      InitialCapacity = capacity;
      Capacity = capacity;
      _Partitions = new LinkedList<T[]>();
      _Partitions.AddLast(new T[capacity]);
      CurrentPartition = _Partitions.First;
    }

    private int InitialCapacity { get; }
    public int Capacity { get; private set; }
    public int Count { get; private set; }

    private LinkedList<T[]> _Partitions;
    private LinkedListNode<T[]> CurrentPartition { get; set; }
    private int _TailPointerOffset = 0;
    private int _HeadPointerOffset = 0;

    public ref T GetFirst()
    {
      if (Count == 0)
        throw new InvalidOperationException("The stack is empty.");

      return ref _Partitions.First.Value[_TailPointerOffset];
    }

    public ref T RemoveFirst()
    {
      if (Count == 0)
        throw new InvalidOperationException("The stack is empty.");

      ref var result = ref _Partitions.First.Value[_TailPointerOffset];

      Count--;
      _TailPointerOffset++;

      if (Count == 0)
      {
        _TailPointerOffset = 0;
        _HeadPointerOffset = 0;
      }
      else if (_TailPointerOffset == _Partitions.First.Value.Length)
      {
        var node = _Partitions.First;
        _Partitions.RemoveFirst();
        _Partitions.AddLast(node);
        _TailPointerOffset = 0;
      }

      return ref result;
    }

    public void AddLast(T item)
    {
      if (CurrentPartition.Value.Length == _HeadPointerOffset)
        AddPartitionOrMoveToNextPartition();

      CurrentPartition.Value[_HeadPointerOffset] = item;
      _HeadPointerOffset++;
      Count++;
    }

    public ref T RemoveLast()
    {
      if (Count == 0)
        throw new InvalidOperationException("The stack is empty.");

      Count--;
      _HeadPointerOffset--;

      ref var item = ref CurrentPartition.Value[_HeadPointerOffset];

      if (Count == 0)
      {
        CurrentPartition = _Partitions.First;
        _HeadPointerOffset = 0;
        _TailPointerOffset = 0;
      }
      else if (_HeadPointerOffset == 0)
      {
        CurrentPartition = CurrentPartition.Previous;
        _HeadPointerOffset = CurrentPartition.Value.Length;
      }

      return ref item;
    }

    public ref T GetLast()
    {
      if (Count == 0)
        throw new InvalidOperationException("The stack is empty.");

      return ref CurrentPartition.Value[_HeadPointerOffset - 1];
    }

    public ref T GetFromBack(int index)
    {
      if (Count == 0)
        throw new InvalidOperationException("The stack is empty.");

      if (index < 0 || index >= Count)
        throw new IndexOutOfRangeException();

      GetPartitionAndOffset(index, out var partition, out var offset);

      return ref partition[offset];
    }

    private void GetPartitionAndOffset(int index, out T[] partition, out int offset)
    {
      if (index < _HeadPointerOffset)
      {
        partition = CurrentPartition.Value;
        offset = _HeadPointerOffset - 1 - index;
        return;
      }

      index -= _HeadPointerOffset;

      var node = CurrentPartition.Previous;

      while (node.Value.Length <= index)
      {
        index -= node.Value.Length;
        node = node.Previous;
      }

      partition = node.Value;
      offset = node.Value.Length - 1 - index;
    }

    private void AddPartitionOrMoveToNextPartition()
    {
      if (CurrentPartition == _Partitions.Last)
      {
        var newPartitionSize = Capacity;
        var newPartition = new T[newPartitionSize];
        _Partitions.AddLast(newPartition);
        Capacity += newPartition.Length;
      }

      CurrentPartition = CurrentPartition.Next;
      _HeadPointerOffset = 0;
    }

    #region IEnumerable

    public IEnumerator<T> GetEnumerator()
    {
      if (Count == 0)
        yield break;

      if (CurrentPartition == _Partitions.First)
      {
        for (var offset = _TailPointerOffset; offset < _HeadPointerOffset; offset++)
          yield return CurrentPartition.Value[offset];

        yield break;
      }

      for (var offset = _TailPointerOffset; offset < _Partitions.First.Value.Length; offset++)
        yield return _Partitions.First.Value[offset];

      var node = _Partitions.First.Next;

      while (node != CurrentPartition)
      {
        for (var offset = 0; offset < node.Value.Length; offset++)
          yield return node.Value[offset];

        node = node.Next;
      }

      for (var offset = 0; offset < _HeadPointerOffset; offset++)
        yield return CurrentPartition.Value[offset];
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    #endregion IEnumerable
  }
}
