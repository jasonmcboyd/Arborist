using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Arborist.Tests.Utils
{
  public static class MoveNextResultsDebugWriter
  {
    public static void WriteMoveNextResults<TNode>(MoveNextResult<TNode>[] moveNextResults)
    {

      var results = new List<string[]>()
      {
        new string[] { " ", "S", "N", "VC", "OP" }
      };

      foreach (var moveNextResult in moveNextResults)
        results.Add(CreateMoveNextResultStringArray(" ", moveNextResult));

      var maxColumnWidths =
        results[0]
        .Select((_, column) => results.Max(result => result[column].Length))
        .ToArray();

      var horizontalRule = "".PadRight(maxColumnWidths.Sum() + 6, '-');

      WriteStringPartsToDebug(results[0], maxColumnWidths);
      Debug.WriteLine(horizontalRule);
      for (int i = 1; i < results.Count; i++)
        WriteStringPartsToDebug(results[i], maxColumnWidths);
    }

    private static void WriteStringPartsToDebug(string[] stringParts, int[] maxColumnWidths)
      => Debug.WriteLine(string.Join(" ", stringParts.Select((x, i) => x.PadRight(maxColumnWidths[i], ' '))));

    private static string[] CreateMoveNextResultStringArray<TNode>(string prefix, MoveNextResult<TNode> result)
    {
      return
        new string[]
        {
          prefix,
          TreenumeratorStateMap.ToChar(result.State).ToString(),
          result.Node.ToString(),
          result.VisitCount.ToString(),
          result.OriginalPosition.ToString()
        };
    }

    private static string[] CreateMoveNextResultDiffs<TNode>(
      string prefix,
      MoveNextResult<TNode> expected,
      MoveNextResult<TNode> actual)
    {
      var stateDiff = CreateValueDiffString(expected.State, actual.State);
      var nodeDiff = CreateValueDiffString(expected.Node, actual.Node);
      var visitCountDiff = CreateValueDiffString(expected.VisitCount, actual.VisitCount);
      var originalPositionDiff = CreateValueDiffString(expected.OriginalPosition, actual.OriginalPosition);

      return new string[] { prefix, stateDiff, nodeDiff, visitCountDiff, originalPositionDiff };
    }

    private static string CreateValueDiffString<TValue>(TValue expected, TValue actual)
    {
      if (expected.Equals(actual))
        return PropertyValueToString(expected);

      return $"{PropertyValueToString(expected)}|{PropertyValueToString(actual)}";
    }

    private static string PropertyValueToString<TValue>(TValue value)
    {
      if (value is TreenumeratorState state)
        return TreenumeratorStateMap.ToChar(state).ToString();

      return value.ToString();
    }
  }
}
