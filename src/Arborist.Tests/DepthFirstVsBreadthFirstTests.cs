using Arborist.Core;
using Arborist.Linq;
using Arborist.SimpleSerializer;
using Arborist.TestUtils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace Arborist.Tests
{
  [TestClass]
  public class DepthFirstVsBreadthFirstTests
  {
    // NOTE: must be a mutable struct with settable properties — MSTest's DynamicData
    // default-constructs and assigns properties when materializing rows, so a readonly
    // struct (or get-only properties) silently arrives with default values.
    public struct NodeStrategy
    {
      public NodeStrategy(string node, NodeTraversalStrategies strategy)
      {
        Node = node;
        Strategy = strategy;
      }

      public string Node { get; set; }
      public NodeTraversalStrategies Strategy { get; set; }

      public override string ToString() => $"{Node}:{Strategy}";
    }

    public static IEnumerable<object[]> GetData()
    {
      var treeStrings = new[]
      {
        "a,b,c",
        "a(b,c)",
        "a(b(c))",
        "a(b),c(d)",
        "a(b(c,d))",
        "a(b(c,d)),e(f(g,h))",
        "a(b,c),d(e,f)",
        "a(b(d,e),c(f,g))",
        "a(b(c)),d(e(f))",
        // Forests that expose SkipNode-parent + SkipSiblings-child interactions
        // (where the SkipNode'd parent sits on the stack and an unrelated subtree
        // is at the queue front).
        "a,b(d),c(e(f))",
        "b(d),c(f)",
        "b(d),e(f)",
      };

      var strategies =
        Enum
        .GetValues<NodeTraversalStrategies>()
        .Where(strategy => strategy != NodeTraversalStrategies.TraverseAll)
        .ToArray();

      foreach (var treeString in treeStrings)
      {
        var nodes =
          treeString
          .Split(new[] { ',', '(', ')' }, StringSplitOptions.RemoveEmptyEntries);

        // No strategy applied.
        yield return new object[] { treeString, Array.Empty<NodeStrategy>() };

        // One (node, strategy).
        foreach (var node in nodes)
          foreach (var strategy in strategies)
            yield return new object[] { treeString, new[] { new NodeStrategy(node, strategy) } };

        // Two (node, strategy) pairs on distinct nodes — needed to expose multi-node
        // interactions such as SkipNode on a parent combined with SkipSiblings on its
        // child.
        for (int i = 0; i < nodes.Length; i++)
          for (int j = i + 1; j < nodes.Length; j++)
            foreach (var strategy1 in strategies)
              foreach (var strategy2 in strategies)
                yield return new object[]
                {
                  treeString,
                  new[] { new NodeStrategy(nodes[i], strategy1), new NodeStrategy(nodes[j], strategy2) }
                };
      }
    }

    public static string GetDisplayName(MethodInfo methodInfo, object[] data)
    {
      var assignments = (NodeStrategy[])data[1];
      var suffix =
        assignments.Length == 0
        ? "(none)"
        : string.Join(", ", assignments.Select(assignment => assignment.ToString()));

      return $"{data[0]} [{suffix}]";
    }

    [TestMethod]
    [DynamicData(nameof(GetData), DynamicDataSourceType.Method, DynamicDataDisplayName = nameof(GetDisplayName))]
    public void Test(string treeString, NodeStrategy[] assignments)
    {
      var treenumerable =
        TreeSerializer
        .Deserialize(treeString)
        .Select(visit => visit.Node);

      NodeVisit<string>[] Sort(IEnumerable<NodeVisit<string>> nodes) =>
        nodes
        .OrderBy(nodeVisit => (nodeVisit.Mode, nodeVisit.Position.Depth, nodeVisit.Position.SiblingIndex, nodeVisit.Node))
        .ToArray();

      NodeTraversalStrategies Selector(NodeContext<string> nodeContext)
      {
        foreach (var assignment in assignments)
          if (assignment.Node == nodeContext.Node)
            return assignment.Strategy;

        return NodeTraversalStrategies.TraverseAll;
      }

      Debug.WriteLine("-----Breadth First-----");
      var breadthFirst =
        treenumerable
        .GetBreadthFirstTraversal(Selector)
        .Do(nodeVisit => Debug.WriteLine(nodeVisit))
        .ToArray();

      Debug.WriteLine($"{Environment.NewLine}-----Depth First------");
      var depthFirst =
        treenumerable
        .GetDepthFirstTraversal(Selector)
        .Do(nodeVisit => Debug.WriteLine(nodeVisit))
        .ToArray();

      CollectionAssert.AreEqual(Sort(breadthFirst), Sort(depthFirst));
    }
  }
}
