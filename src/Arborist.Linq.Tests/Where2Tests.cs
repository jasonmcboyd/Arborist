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
    public static IEnumerable<object[]> GetTestData()
    {
      var treeStrings = new[]
      {
        //"a,b,c",
        //"a(b,c)",
        //"a,b(c)",
        "a,b(d),c",
        //"a(b(c))",
        //"a(b,c),d(e,f)",
        //"a,b(c),d(e(f))",
        //"a(b(e,f,g),c)",
        //"a(b(e,f,g),c(h,i,j))",
        //"a(d),b,c(e)",
        //"a(d(f,g,h)),b,c(e)"
      };

      var nodeTraversalStrategies =
        Enum.GetValues(typeof(NodeTraversalStrategies))
        .Cast<NodeTraversalStrategies>()
        .Where(nodeTraversalStrategy => nodeTraversalStrategy != NodeTraversalStrategies.TraverseAll)
        .ToArray();

      foreach (var treeString in treeStrings)
      {
        var nodePairs = GetTreeNodePairs(treeString).ToArray();

        foreach (var nodePair in nodePairs)
        {
          var expectedTreeString = GetExpectedTreeString(treeString, nodePair[0], nodePair[1]);

          foreach (var composeOperations in new[] { true, false })
          {
            yield return new object[]
            {
              treeString,
              expectedTreeString,
              nodePair,
              composeOperations,
              "",
              ""
            };

            var expectedTreeNodes =
              TreeSerializer
              .Deserialize(treeString)
              .PreOrderTraversal()
              .ToArray();

            var cross =
              expectedTreeNodes
              .SelectMany(node => nodeTraversalStrategies.Select(nodeTraversalStrategy => (node, nodeTraversalStrategy)))
              .ToArray();

            for (int i = 0; i < cross.Length; i++)
            {
              var firstPair = cross[i];

              yield return new object[]
              {
                treeString,
                expectedTreeString,
                nodePair,
                composeOperations,
                firstPair.node,
                firstPair.nodeTraversalStrategy.ToString()
              };

              for (int j = i + 1; j < cross.Length; j++)
              {
                var secondPair = cross[j];

                if (firstPair.node == secondPair.node)
                  continue;

                yield return new object[]
                {
                  treeString,
                  expectedTreeString,
                  nodePair,
                  composeOperations,
                  SerializeTestNodes(firstPair.node, secondPair.node),
                  SerializeNodeTraversalStrategies(firstPair.nodeTraversalStrategy, secondPair.nodeTraversalStrategy)
                };
              }
            }
          }
        }
      }
    }

    private static IEnumerable<string[]> GetTreeNodePairs(string treeString)
    {
      var nodes = TreeSerializer.Deserialize(treeString).PreOrderTraversal().ToArray();

      for (int i = 0; i < nodes.Length - 1; i++)
        for (int j = i + 1; j < nodes.Length; j++)
          yield return new[] { nodes[i], nodes[j] };
    }

    private static string GetExpectedTreeString(string treeString, string node1, string node2)
    {
      var tree = TreeSerializer.Deserialize(treeString);

      var expectedTree =
        tree
        .Where(nc => nc.Node != node1)
        .Hide()
        .Where(nc => nc.Node != node2);

      return TreeSerializer.Serialize(expectedTree);
    }

    private static string SerializeNodeTraversalStrategies(params NodeTraversalStrategies[] nodeTraversalStrategies)
    {
      return string.Join("|", nodeTraversalStrategies.Select(nodeTraversalStrategy => nodeTraversalStrategy.ToString()));
    }

    private static NodeTraversalStrategies[] DeserializeNodeTraversalStrategies(string nodeTraversalStrategies)
    {
      if (string.IsNullOrEmpty(nodeTraversalStrategies))
        return Array.Empty<NodeTraversalStrategies>();

      return
        nodeTraversalStrategies
        .Split('|')
        .Select(nodeTraversalStrategy => Enum.Parse(typeof(NodeTraversalStrategies), nodeTraversalStrategy))
        .Cast<NodeTraversalStrategies>()
        .ToArray();
    }

    private static string SerializeTestNodes(params string[] testNodes)
    {
      return string.Join("|", testNodes);
    }

    private static string[] DeserializeTestNodes(string testNodes)
    {
      if (string.IsNullOrEmpty(testNodes))
        return Array.Empty<string>();

      return testNodes.Split('|');
    }

    public static string GetTestDisplayName(MethodInfo methodInfo, object[] data)
    {
      var result = $"{data[0]} -> {data[1]} ";

      var testNodes = DeserializeTestNodes(data[4].ToString());
      var nodeTraversalStrategy = DeserializeNodeTraversalStrategies(data[5].ToString());

      for (int i = 0; i < testNodes.Length; i++)
      {
        if (i == 1)
          result += ", ";

        result += $"{nodeTraversalStrategy[i]}: {testNodes[i]}";
      }

      result += $" ({data[3].ToString().Substring(0, 1)})";

      return result;
    }

    [TestMethod]
    [DynamicData(nameof(GetTestData), DynamicDataSourceType.Method, DynamicDataDisplayName = nameof(GetTestDisplayName))]
    public void Where2Test_BreadthFirst(
      string treeString,
      string expectedTreeString,
      string[] skippedNodes,
      bool composeOperations,
      string testNodesString,
      string nodeTraversalStrategyString)
    {
      var testNodes = DeserializeTestNodes(testNodesString);
      var nodeTraversalStrategy = DeserializeNodeTraversalStrategies(nodeTraversalStrategyString);

      Where2Test(
        treeString,
        expectedTreeString,
        skippedNodes,
        composeOperations,
        testNodes,
        nodeTraversalStrategy,
        TreeTraversalStrategy.BreadthFirst);
    }

    [TestMethod]
    [DynamicData(nameof(GetTestData), DynamicDataSourceType.Method, DynamicDataDisplayName = nameof(GetTestDisplayName))]
    public void Where2Test_DepthFirst(
      string treeString,
      string expectedTreeString,
      string[] skippedNodes,
      bool composeOperations,
      string testNodesString,
      string nodeTraversalStrategyString)
    {
      var testNodes = DeserializeTestNodes(testNodesString);
      var nodeTraversalStrategy = DeserializeNodeTraversalStrategies(nodeTraversalStrategyString);

      Where2Test(
        treeString,
        expectedTreeString,
        skippedNodes,
        composeOperations,
        testNodes,
        nodeTraversalStrategy,
        TreeTraversalStrategy.DepthFirst);
    }

    public void Where2Test(
      string treeString,
      string expectedTreeString,
      string[] skippedNodes,
      bool composeOperations,
      string[] testNodes,
      NodeTraversalStrategies[] nodeTraversalStrategy,
      TreeTraversalStrategy treeTraversalStrategy)
    {
      // Arrange
      var treenumerable = TreeSerializer.Deserialize(treeString);

      ITreenumerable<string> sut;

      if (composeOperations)
      {
        sut =
          treenumerable
          .Where(nc => nc.Node != skippedNodes[0])
          .Where(nc => nc.Node != skippedNodes[1]);
      }
      else
      {
        sut =
          treenumerable
          .Where(nc =>
            nc.Node != skippedNodes[0]
            && nc.Node != skippedNodes[1]);
      }


      Func<NodeContext<string>, NodeTraversalStrategies> nodeTraversalStrategySelector =
        nodeContext =>
        {
          var testNodeIndex = Array.IndexOf(testNodes, nodeContext.Node);

          if (testNodeIndex == -1)
            return NodeTraversalStrategies.TraverseAll;
          else
            return nodeTraversalStrategy[testNodeIndex];
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
