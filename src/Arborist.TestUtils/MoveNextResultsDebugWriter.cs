using Arborist.Core;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Arborist.TestUtils
{
  public static class MoveNextResultsDebugWriter
  {
    public static void WriteMoveNextResults<TNode>(MoveNextResult<TNode>[] moveNextResults)
    {

      var results = new List<string[]>()
      {
        new string[] { " ", "S", "N", "VC", "OP", "P" }
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
          TreenumeratorModeMap.ToChar(result.Mode).ToString(),
          result.Node.ToString(),
          result.VisitCount.ToString(),
          result.OriginalPosition.ToString(),
          result.Position.ToString()
        };
    }

    private static string[] CreateMoveNextResultDiffs<TNode>(
      string prefix,
      MoveNextResult<TNode> expected,
      MoveNextResult<TNode> actual)
    {
      var modeDiff = CreateValueDiffString(expected.Mode, actual.Mode);
      var nodeDiff = CreateValueDiffString(expected.Node, actual.Node);
      var visitCountDiff = CreateValueDiffString(expected.VisitCount, actual.VisitCount);
      var originalPositionDiff = CreateValueDiffString(expected.OriginalPosition, actual.OriginalPosition);
      var positionDiff = CreateValueDiffString(expected.Position, actual.Position);

      return new string[] { prefix, modeDiff, nodeDiff, visitCountDiff, originalPositionDiff };
    }

    private static string CreateValueDiffString<TValue>(TValue expected, TValue actual)
    {
      if (expected.Equals(actual))
        return PropertyValueToString(expected);

      return $"{PropertyValueToString(expected)}|{PropertyValueToString(actual)}";
    }

    private static string PropertyValueToString<TValue>(TValue value)
    {
      if (value is TreenumeratorMode mode)
        return TreenumeratorModeMap.ToChar(mode).ToString();

      return value.ToString();
    }
  }
}
