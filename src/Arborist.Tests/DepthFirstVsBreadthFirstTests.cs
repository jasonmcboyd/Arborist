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
        NodeTraversalStrategies.SkipNode,
        NodeTraversalStrategies.SkipNodeAndDescendants,
        NodeTraversalStrategies.SkipDescendants,
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
    public void Test(string treeString, NodeTraversalStrategies? filterStrategy, string filterCharacter)
    {
      var treenumerable =
        TreeSerializer
        .Deserialize(treeString)
        .Select(visit => visit.Node);

      NodeVisit<string>[] Sort(IEnumerable<NodeVisit<string>> nodes) =>
        nodes
        .OrderBy(nodeVisit => (nodeVisit.Mode, nodeVisit.Position.Depth, nodeVisit.Position.SiblingIndex, nodeVisit.Node))
        .ToArray();

      var nodeTraversalStrategiesSelector =
        new Func<NodeContext<string>, NodeTraversalStrategies>(
          nodeVisit =>
            filterCharacter == null || filterCharacter != nodeVisit.Node
            ? NodeTraversalStrategies.TraverseAll
            : filterStrategy.Value);

      Debug.WriteLine("-----Breadth First-----");
      var breadthFirst =
        treenumerable
        .GetBreadthFirstTraversal(nodeTraversalStrategiesSelector)
        .Do(nodeVisit => Debug.WriteLine(nodeVisit))
        .ToArray();

      Debug.WriteLine($"{Environment.NewLine}-----Depth First------");
      var depthFirst =
        treenumerable
        .GetDepthFirstTraversal(nodeTraversalStrategiesSelector)
        .Do(nodeVisit => Debug.WriteLine(nodeVisit))
        .ToArray();

      CollectionAssert.AreEqual(Sort(breadthFirst), Sort(depthFirst));
    }
  }
}
