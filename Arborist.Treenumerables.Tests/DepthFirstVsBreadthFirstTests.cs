using Arborist.Tests.Utils;
using Arborist.Treenumerables.Serialization;
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
        "a(b,c),d(e,f)",
        "a(b(d,e),c(f,g))"
      };

      var filterStrategies = new[]
      {
        SchedulingStrategy.SkipNode,
        SchedulingStrategy.SkipSubtree,
        SchedulingStrategy.SkipDescendantSubtrees,
      };

      foreach (var treeString in treeStrings)
      {
        yield return new[] { treeString, null, null };

        foreach (var str in treeString.Split(new[] { ',', '(', ')' }, StringSplitOptions.RemoveEmptyEntries ))
          foreach (var strategy in filterStrategies)
            yield return new object[] { treeString, strategy, str }; 
      }
    }

    [TestMethod]
    [DynamicData(nameof(GetData), DynamicDataSourceType.Method)]
    public void Test(string treeString, SchedulingStrategy? filterStrategy, string filterCharacter)
    {
      var treenumerable = TreeSerializer.Deserialize(treeString);

      MoveNextResult<string>[] Sort(IEnumerable<MoveNextResult<string>> nodes) =>
        nodes
        .OrderBy(x => (x.State, x.Depth, x.SiblingIndex, x.Node))
        .ToArray();

      var visitStrategy =
        new Func<NodeVisit<string>, SchedulingStrategy>(
          visit =>
            filterCharacter == null || filterCharacter != visit.Node
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
