using Arborist.Core;
using Arborist.SimpleSerializer;
using Arborist.TestUtils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace Arborist.Linq.Tests
{
  [TestClass]
  public class Where2Tests
  {
    public struct NodeAndTraversalStrategy
    {
      public NodeAndTraversalStrategy(string node, NodeTraversalStrategies nodeTraversalStrategy)
      {
        if (node == null)
          throw new ArgumentNullException(nameof(node));

        Node = node;
        NodeTraversalStrategy = nodeTraversalStrategy;
      }

      public string Node { get; set; }
      public NodeTraversalStrategies NodeTraversalStrategy { get; set; }
    }

    public static IEnumerable<object[]> GetTestData()
    {
      var treeStrings = new[]
      {
        // c
        "a(b(c))",
        //"a(b,c)",
        //"a,b(c)",
        //"a,b,c",

        // d
        //"a(b,c,d)",
        //"a,b(c,d)",
        //"a,b(d),c",

        // e
        //"a(b(d(e)),c)",
        //"a(b(c,d,e))",
        //"a(d),b,c(e)",

        // f
        //"a(c,d),b(e,f)",
        //"a(d),b(e),c(f)",
        //"a(b(d,e,f),c)",
        //"a,b(d),c(e(f))",

        // g
        //"a(b(e),c(f),d(g))",
        //"a(b(d,e),c(f(g)))"
        //"a(b(e),c(f),d(g))",
        //"a(b(d,e),c(f(g)))"

        // h
        //"a(d(f,g,h)),b,c(e)"

        // i
        //"a(b(d,e,f),c(g,h,i))",
        //"a(d(g)),b(e(h)),c(f(i))",
      };

      var nodeTraversalStrategies =
        Enum.GetValues(typeof(NodeTraversalStrategies))
        .Cast<NodeTraversalStrategies>()
        .Where(nodeTraversalStrategy => nodeTraversalStrategy != NodeTraversalStrategies.TraverseAll)
        .ToArray();

      foreach (var treeString in treeStrings)
      {
        var allTreeNodes =
          TreeSerializer
          .Deserialize(treeString)
          .PreOrderTraversal()
          .ToArray();

        var allTreeNodeAndTraversalStrategyPairs =
          allTreeNodes
          .SelectMany(node => nodeTraversalStrategies.Select(nodeTraversalStrategy => new NodeAndTraversalStrategy(node, nodeTraversalStrategy)))
          .ToArray();

        // Get combinations of 0, 1, or 2 nodes that will not satisfy where clause in tests.
        var treeNodeCombinations = GetTreeNodeCombinationsUpToCount(treeString, 2).ToArray();

        // Get combinations of 0, 1, or 2 node / node traversal strategy pairs for use in the tests.
        var treeNodeAndTraversalStrategyCombinations = Combinatorics.GetCombinationsUpToCount<NodeAndTraversalStrategy>(allTreeNodeAndTraversalStrategyPairs.AsSpan(), 2).ToArray();

        foreach (var nodeCombinations in treeNodeCombinations)
        {
          foreach (var composeOperations in new[] { true, false })
          {
            foreach (var nodeAndTraversalStrategyPairCombination in treeNodeAndTraversalStrategyCombinations)
            {
              var expectedTreeString = GetExpectedTreeString(treeString, nodeCombinations);
              var nodeAndTraversalStrategyPairs = nodeAndTraversalStrategyPairCombination.ToArray();

              yield return new object[]
              {
                treeString,
                expectedTreeString,
                nodeCombinations,
                composeOperations,
                nodeAndTraversalStrategyPairs
              };
            }
          }
        }
      }
    }

    private static IEnumerable<string[]> GetTreeNodeCombinationsUpToCount(string treeString, int count)
    {
      var nodes =
        TreeSerializer
        .Deserialize(treeString)
        .PreOrderTraversal()
        .ToArray()
        .AsSpan();

      return
        Combinatorics
        .GetCombinationsUpToCount<string>(nodes, count)
        .Select(combination => combination.Select(node => node.ToString()).ToArray());
    }

    private static string GetExpectedTreeString(string treeString, IEnumerable<string> whereNotNodes)
    {
      var tree = TreeSerializer.Deserialize(treeString);

      var expectedTree = tree;

      foreach (var node in whereNotNodes)
        expectedTree = expectedTree.Where(nc => nc.Node != node).Hide();

      return TreeSerializer.Serialize(expectedTree);
    }

    public static string GetTestDisplayName(MethodInfo methodInfo, object[] data)
    {
      var result = $"{data[0]} -> {data[1]} ";

      var nodeAndTraversalStrategyPairs = (NodeAndTraversalStrategy[])data[4];

      for (int i = 0; i < nodeAndTraversalStrategyPairs.Length; i++)
      {
        if (i > 0)
          result += ", ";

        result += $"{nodeAndTraversalStrategyPairs[i].Node}: {nodeAndTraversalStrategyPairs[i].NodeTraversalStrategy}";
      }

      result += $" ({data[3].ToString().Substring(0, 1)})";

      return result;
    }

    //[TestMethod]
    //[DynamicData(nameof(GetTestData), DynamicDataSourceType.Method, DynamicDataDisplayName = nameof(GetTestDisplayName))]
    //public void Where2Test_BreadthFirst(
    //  string treeString,
    //  string expectedTreeString,
    //  string[] skippedNodes,
    //  bool composeOperations,
    //  string testNodesString,
    //  string nodeTraversalStrategyString)
    //{
    //  var testNodes = DeserializeTestNodes(testNodesString);
    //  var nodeTraversalStrategy = DeserializeNodeTraversalStrategies(nodeTraversalStrategyString);

    //  Where2Test(
    //    treeString,
    //    expectedTreeString,
    //    skippedNodes,
    //    composeOperations,
    //    testNodes,
    //    nodeTraversalStrategy,
    //    TreeTraversalStrategy.BreadthFirst);
    //}

    [TestMethod]
    [DynamicData(nameof(GetTestData), DynamicDataSourceType.Method, DynamicDataDisplayName = nameof(GetTestDisplayName))]
    public void Where2Test_DepthFirst(
      string treeString,
      string expectedTreeString,
      string[] skippedNodes,
      bool composeOperations,
      NodeAndTraversalStrategy[] nodeAndTraversalStrategyPairs)
    {
      Where2Test(
        treeString,
        expectedTreeString,
        skippedNodes,
        composeOperations,
        nodeAndTraversalStrategyPairs,
        TreeTraversalStrategy.DepthFirst);
    }

    public void Where2Test(
      string treeString,
      string expectedTreeString,
      string[] skippedNodes,
      bool composeOperations,
      NodeAndTraversalStrategy[] nodeAndTraversalStrategyPairs,
      TreeTraversalStrategy treeTraversalStrategy)
    {
      // Arrange
      var treenumerable = TreeSerializer.Deserialize(treeString);

      if (nodeAndTraversalStrategyPairs.Any(x => x.Node == null || x.NodeTraversalStrategy == NodeTraversalStrategies.TraverseAll))
        throw new InternalTestFailureException();

      ITreenumerable<string> sut;

      if (composeOperations)
      {
        sut = treenumerable;

        foreach (var node in skippedNodes)
          sut = sut.Where(nc => nc.Node != node);
      }
      else
      {
        sut =
          treenumerable
          .Where(nc => !skippedNodes.Contains(nc.Node));
      }

      Func<NodeContext<string>, NodeTraversalStrategies> nodeTraversalStrategySelector =
        nodeContext =>
        {
          var testNodeIndex = -1;

          for (int i = 0; i < nodeAndTraversalStrategyPairs.Length; i++)
          {
            if (nodeAndTraversalStrategyPairs[i].Node == nodeContext.Node)
            {
              testNodeIndex = i;
              break;
            }
          }

          if (testNodeIndex == -1)
            return NodeTraversalStrategies.TraverseAll;
          else
            return nodeAndTraversalStrategyPairs[testNodeIndex].NodeTraversalStrategy;
        };

      var expected =
        TreeSerializer
        .Deserialize(expectedTreeString)
        .GetTraversal(treeTraversalStrategy, nodeTraversalStrategySelector)
        .ToArray();

      Debug.WriteLine("-----Expected Values-----");
      foreach (var value in expected)
        Debug.WriteLine(value);

      // Act
      Debug.WriteLine($"{Environment.NewLine}-----Actual Values-----");
      var actual =
        sut
        .GetTraversal(
          treeTraversalStrategy,
          nodeTraversalStrategySelector)
        .Do(visit => Debug.WriteLine(visit))
        .ToArray();

      var diff = NodeVisitDiffer.Diff(expected, actual);

      Debug.WriteLine($"{Environment.NewLine}-----Diffed Values-----");
      foreach (var diffResult in diff)
        Debug.WriteLine(diffResult);

      // Assert
      CollectionAssert.AreEqual(expected, actual);
    }
  }
}
