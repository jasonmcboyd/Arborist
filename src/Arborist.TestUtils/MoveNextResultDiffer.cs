using Arborist.Core;
using DiffPlex;
using DiffPlex.DiffBuilder;
using DiffPlex.DiffBuilder.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Arborist.TestUtils
{
  public static class MoveNextResultDiffer
  {
    public static IEnumerable<string> Diff<TNode>(MoveNextResult<TNode>[] expectedResults, MoveNextResult<TNode>[] actualResults)
    {
      if (expectedResults.Length != actualResults.Length)
        throw new Exception("The expected results and actual results should have the same number of items");

      var diffBuilder = new SideBySideDiffBuilder(new Differ());

      var expectedText = string.Join($"{Environment.NewLine}", expectedResults.Select(x => x.ToString()));
      var actualText = string.Join($"{Environment.NewLine}", actualResults.Select(x => x.ToString()));

      var diff = diffBuilder.BuildDiffModel(expectedText, actualText);

      var results = new List<string[]>
      {
        new string[] { " ", "S", "N", "VC", "OP", "P" }
      };

      if (diff.OldText.Lines.Count != diff.NewText.Lines.Count)
        throw new Exception("Unexpected line count differences, I had not planned for this scenario.");

      var expectedPointer = 0;
      var actualPointer = 0;

      for (int i = 0; i < diff.OldText.Lines.Count; i++)
      {
        var expectedDiff = diff.OldText.Lines[i];
        var actualDiff = diff.NewText.Lines[i];

        var expected =
          expectedPointer < expectedResults.Length
          ? expectedResults[expectedPointer]
          : null;

        var actual =
          actualPointer < actualResults.Length
          ? actualResults[actualPointer]
          : null;

        if (expectedDiff.Type == ChangeType.Unchanged)
        {
          results.Add(WriteMoveNextResultDiffs(" ", expected, actual));
          expectedPointer++;
          actualPointer++;
        }
        else if (expectedDiff.Type == ChangeType.Modified)
        {
          results.Add(WriteMoveNextResultDiffs("M", expected, actual));
          expectedPointer++;
          actualPointer++;
        }
        else if (expectedDiff.Type == ChangeType.Inserted)
        {
          results.Add(CreateMoveNextResultStringArray("+", expected));
          expectedPointer++;
        }
        else if (expectedDiff.Type == ChangeType.Imaginary)
        {
          results.Add(CreateMoveNextResultStringArray("I", actual));
          actualPointer++;
        }
        else if (expectedDiff.Type == ChangeType.Deleted)
        {
          if (actual == null)
          {
            results.Add(CreateMoveNextResultStringArray("-", expected));
            actualPointer++;
          }
          else
          {
            results.Add(CreateMoveNextResultStringArray("-", actual));
            expectedPointer++;
          }
        }
        else
        {
          throw new Exception("Unexpected ChangeType.");
        }
      }

      var maxColumnWidths =
        results[0]
        .Select((_, column) => results.Max(result => result[column].Length))
        .ToArray();

      var horizontalRule = "".PadRight(maxColumnWidths.Sum() + 6, '-');

      yield return string.Join(" ", results[0].Select((x, i) => x.PadRight(maxColumnWidths[i], ' ')));
      yield return horizontalRule;

      foreach (var result in results.Skip(1))
        yield return string.Join(" ", result.Select((x, i) => x.PadRight(maxColumnWidths[i], ' ')));
    }

    private static string[] CreateMoveNextResultStringArray<TNode>(string prefix, MoveNextResult<TNode> result)
    {
      return
        new string[]
        {
          prefix,
          TreenumeratorStateMap.ToChar(result.State).ToString(),
          result.Node.ToString(),
          result.VisitCount.ToString(),
          result.OriginalPosition.ToString(),
          result.Position.ToString()
        };
    }

    private static string[] WriteMoveNextResultDiffs<TNode>(
      string prefix,
      MoveNextResult<TNode> expected,
      MoveNextResult<TNode> actual)
    {
      var stateDiff = WriteValueDiffs(expected.State, actual.State);
      var nodeDiff = WriteValueDiffs(expected.Node, actual.Node);
      var visitCountDiff = WriteValueDiffs(expected.VisitCount, actual.VisitCount);
      var originalPositionDiff = WriteValueDiffs(expected.OriginalPosition, actual.OriginalPosition);
      var positionDiff = WriteValueDiffs(expected.Position, actual.Position);

      return new string[] { prefix, stateDiff, nodeDiff, visitCountDiff, originalPositionDiff, positionDiff };
    }

    private static string WriteValueDiffs<TValue>(TValue expected, TValue actual)
    {
      if (expected.Equals(actual))
        return PropertyToString(expected);

      return $"{PropertyToString(expected)}|{PropertyToString(actual)}";
    }

    private static string PropertyToString<TValue>(TValue value)
    {
      if (value is TreenumeratorState state)
        return TreenumeratorStateMap.ToChar(state).ToString();

      return value.ToString();
    }
  }
}
