using Arborist.Tests.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Arborist.Treenumerables.Tests
{
  [TestClass]
  public class DepthFirstVsBreadthFirstTests
  {
    public static IEnumerable<object[]> GetData()
    {
      var treeStrings = new[]
      {
        "a,b,c",
        "a(b,c)",
        "a(b(c))",
        "a(b(c,d))",
        "a(b,c),d(e,f)"
      };

      var filterStrategies = new[]
      {
        SchedulingStrategy.SkipNode,
        SchedulingStrategy.SkipSubtree
      };

      foreach (var treeString in treeStrings)
      {
        yield return new[] { treeString, null, null };

        foreach (var c in treeString.Where(char.IsLetter))
          foreach (var strategy in filterStrategies)
            yield return new object[] { treeString, strategy, c }; 
      }
    }

    [TestMethod]
    [DynamicData(nameof(GetData), DynamicDataSourceType.Method)]
    public void Test(string treeString, SchedulingStrategy? filterStrategy, char? filterCharacter)
    {
      var treenumerable = TreeStringParser.ParseTreeString(treeString);

      MoveNextResult<char>[] Sort(IEnumerable<MoveNextResult<char>> nodes) =>
        nodes
        .OrderBy(x => (x.State, x.Depth, x.SiblingIndex, x.Node))
        .ToArray();

      var visitStrategy =
        new Func<NodeVisit<char>, SchedulingStrategy>(
          visit =>
            filterCharacter == null || filterCharacter.Value != visit.Node
            ? SchedulingStrategy.ScheduleForTraversal
            : filterStrategy.Value);

      Debug.WriteLine("-----Breadth First-----");
      var breadthFirst =
        treenumerable
        .ToBreadthFirstMoveNext(visitStrategy)
        .Do(x => Debug.WriteLine(x))
        .ToArray();

      Debug.WriteLine("\r\n-----Depth First------");
      var depthFirst =
        treenumerable
        .ToDepthFirstMoveNext(visitStrategy)
        .Do(x => Debug.WriteLine(x))
        .ToArray();

      CollectionAssert.AreEqual(Sort(breadthFirst), Sort(depthFirst));
    }
  }
}
