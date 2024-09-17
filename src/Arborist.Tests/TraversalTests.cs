using Arborist.Core;
using Arborist.Linq;
using Arborist.Nodes;
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
  public class TraversalTests
  {
    static TraversalTests()
    {
      _TreenumerableTestDataFactory = new TreenumerableTestDataFactory(TreeTraversalTestData.TestTrees); 
    }

    private static TreenumerableTestDataFactory _TreenumerableTestDataFactory;

    public static IEnumerable<object[]> GetTestData()
      => _TreenumerableTestDataFactory.GetTestData();

    public static string GetTestDisplayName(MethodInfo methodInfo, object[] data)
      => _TreenumerableTestDataFactory.GetTestDisplayName(methodInfo, data);

    [TestMethod]
    [DynamicData(nameof(GetTestData), DynamicDataSourceType.Method, DynamicDataDisplayName = nameof(GetTestDisplayName))]
    public void DepthFirst_IndexableTreenumerable(
      string treeString,
      string testDescription,
      int testTreeIndex,
      int testScenarioIndex)
    {
      TraversalTest(treeString, testTreeIndex, testScenarioIndex, true, false);
    }

    [TestMethod]
    [DynamicData(nameof(GetTestData), DynamicDataSourceType.Method, DynamicDataDisplayName = nameof(GetTestDisplayName))]
    public void DepthFirst_EnumerableTreenumerable(
      string treeString,
      string testDescription,
      int testTreeIndex,
      int testScenarioIndex)
    {
      TraversalTest(treeString, testTreeIndex, testScenarioIndex, true, true);
    }

    [TestMethod]
    [DynamicData(nameof(GetTestData), DynamicDataSourceType.Method, DynamicDataDisplayName = nameof(GetTestDisplayName))]
    public void BreadthFirst_IndexableTreenumerable(
      string treeString,
      string testDescription,
      int testTreeIndex,
      int testScenarioIndex)
    {
      TraversalTest(treeString, testTreeIndex, testScenarioIndex, false, false);
    }

    [TestMethod]
    [DynamicData(nameof(GetTestData), DynamicDataSourceType.Method, DynamicDataDisplayName = nameof(GetTestDisplayName))]
    public void BreadthFirst_EnumerableTreenumerable(
      string treeString,
      string testDescription,
      int testTreeIndex,
      int testScenarioIndex)
    {
      TraversalTest(treeString, testTreeIndex, testScenarioIndex, false, true);
    }

    [TestMethod]
    [DynamicData(nameof(GetTestData), DynamicDataSourceType.Method, DynamicDataDisplayName = nameof(GetTestDisplayName))]
    public void DepthFirstVsBreadthFirstValues(
      string treeString,
      string testDescription,
      int testTreeIndex,
      int testScenarioIndex)
    {
      // Arrange
      var treenumerable =
        TreeSerializer
        .DeserializeRoots(treeString)
        .ToTreenumerable<string, NodeWithIndexableChildren<string>>()
        .Select(visit => visit.Node);

      var testScenario = TreeTraversalTestData.TestTrees[testTreeIndex].TestScenarios[testScenarioIndex];

      NodeVisit<string>[] Sort(IEnumerable<NodeVisit<string>> nodes) =>
        nodes
        .OrderBy(x => (x.Mode, x.Position.Depth, x.Position.SiblingIndex, x.Node))
        .ToArray();

      // Act
      Debug.WriteLine("-----Breadth First-----");
      var breadthFirst =
        treenumerable
        .GetBreadthFirstTraversal(testScenario.NodeTraversalStrategySelector)
        .Do(x => Debug.WriteLine(x))
        .ToArray();

      Debug.WriteLine($"{Environment.NewLine}-----Depth First------");
      var depthFirst =
        treenumerable
        .GetDepthFirstTraversal(testScenario.NodeTraversalStrategySelector)
        .Do(x => Debug.WriteLine(x))
        .ToArray();

      // Assert
      CollectionAssert.AreEqual(Sort(breadthFirst), Sort(depthFirst));
    }

    private void TraversalTest(
      string treeString,
      int testTreeIndex,
      int testScenarioIndex,
      bool depthFirstTest,
      bool enumerableTreenumeratorTest)
    {
      // Arrange
      var testScenario = TreeTraversalTestData.TestTrees[testTreeIndex].TestScenarios[testScenarioIndex];

      ITreenumerable<string> treenumerable;

      if (enumerableTreenumeratorTest)
      {
        var rootNodes =
          TreeSerializer
          .DeserializeRoots(treeString)
          .CreateNodeWithEnumerableChildren<string, NodeWithIndexableChildren<string>>();

        treenumerable =
          rootNodes
          .ToTreenumerable()
          .Select(visit => visit.Node);
      }
      else
      {
        var roots = TreeSerializer.DeserializeRoots(treeString);

        treenumerable =
          roots
          .ToTreenumerable<string, NodeWithIndexableChildren<string>>()
          .Select(visit => visit.Node);
      }

      var expected =
        depthFirstTest
        ? testScenario.ExpectedDepthFirstResults
        : testScenario.ExpectedBreadthFirstResults;

      Debug.WriteLine("--------- Tree ---------");
      Debug.WriteLine(treeString);

      Debug.WriteLine($"{Environment.NewLine}---- Expected Values ----");
      NodeVisitsDebugWriter.WriteNodeVisitHeader();
      foreach (var nodeVisit in expected)
        Debug.WriteLine(nodeVisit);

      var moveNextEnumerable =
        depthFirstTest
        ? treenumerable.GetDepthFirstTraversal(testScenario.NodeTraversalStrategySelector)
        : treenumerable.GetBreadthFirstTraversal(testScenario.NodeTraversalStrategySelector);

      // Act
      Debug.WriteLine($"{Environment.NewLine}----- Actual Values -----");
      NodeVisitsDebugWriter.WriteNodeVisitHeader();
      var actual =
        moveNextEnumerable
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
