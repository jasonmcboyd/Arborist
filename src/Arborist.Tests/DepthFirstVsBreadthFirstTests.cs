using Arborist.Core;
using Arborist.Linq;
using Arborist.SimpleSerializer;
using Arborist.TestUtils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Arborist.Tests
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
        "a(b(c,d)),e(f(g,h))",
        "a(b,c),d(e,f)",
        "a(b(d,e),c(f,g))",
        "a(b(c)),d(e(f))",
      };

      var filterStrategies = new[]
      {
        TraversalStrategy.SkipNode,
        TraversalStrategy.SkipSubtree,
        TraversalStrategy.SkipDescendants,
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
    public void Test(string treeString, TraversalStrategy? filterStrategy, string filterCharacter)
    {
      var treenumerable =
        TreeSerializer
        .DeserializeRoots(treeString)
        .ToTreenumerable()
        .Select(visit => visit.Node);

      NodeVisit<string>[] Sort(IEnumerable<NodeVisit<string>> nodes) =>
        nodes
        .OrderBy(nodeVisit => (nodeVisit.Mode, nodeVisit.OriginalPosition.Depth, nodeVisit.OriginalPosition.SiblingIndex, nodeVisit.Node))
        .ToArray();

      var traversalStrategySelector =
        new Func<NodeVisit<string>, TraversalStrategy>(
          nodeVisit =>
            filterCharacter == null || filterCharacter != nodeVisit.Node
            ? TraversalStrategy.TraverseSubtree
            : filterStrategy.Value);

      Debug.WriteLine("-----Breadth First-----");
      var breadthFirst =
        treenumerable
        .GetBreadthFirstTraversal(traversalStrategySelector)
        .Do(nodeVisit => Debug.WriteLine(nodeVisit))
        .ToArray();

      Debug.WriteLine($"{Environment.NewLine}-----Depth First------");
      var depthFirst =
        treenumerable
        .GetDepthFirstTraversal(traversalStrategySelector)
        .Do(nodeVisit => Debug.WriteLine(nodeVisit))
        .ToArray();

      CollectionAssert.AreEqual(Sort(breadthFirst), Sort(depthFirst));
    }
  }
}
