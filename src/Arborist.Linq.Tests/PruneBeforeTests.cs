using Arborist.Core;
using Arborist.TestUtils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Arborist.Linq.Tests
{
  [TestClass]
  public class PruneBeforeTests
  {
    static PruneBeforeTests()
    {
      _TreenumerableTestDataFactory = new TreenumerableTestDataFactory(TestTrees);
    }

    private static TreenumerableTestDataFactory _TreenumerableTestDataFactory;

    public static IEnumerable<object[]> GetTestData()
      => _TreenumerableTestDataFactory.GetTestData();

    public static string GetTestDisplayName(MethodInfo methodInfo, object[] data)
      => _TreenumerableTestDataFactory.GetTestDisplayName(methodInfo, data);

    private static TreeTestDefinition[] TestTrees =>
      new TreeTestDefinition[]
      {
        // Empty Tree
        new TreeTestDefinition
        {
          TreeString = "",
          TestScenarios = new List<TestScenario>
          {
            // No skipping
            new TestScenario
            {
              TraversalStrategySelector = visit => TraversalStrategy.TraverseSubtree,
              TreenumerableMap = treenumerable => treenumerable.PruneBefore(_ => true),
              Description = "Prune before all, traverse all",
              ExpectedBreadthFirstResults = Array.Empty<NodeVisit<string>>(),
              ExpectedDepthFirstResults = Array.Empty<NodeVisit<string>>()
            },
          }
        },

        // Three levels, one node per level
        new TreeTestDefinition
        {
          TreeString = "a(b(c))",
          TestScenarios = new List<TestScenario>
          {
            // Skip node
            new TestScenario
            {
              TraversalStrategySelector = visit => visit.OriginalPosition.Depth == 0 ? TraversalStrategy.SkipNode : TraversalStrategy.TraverseSubtree,
              TreenumerableMap = treenumerable => treenumerable.PruneBefore(visit => visit.OriginalPosition.Depth == 2),
              Description = "Prune before level 2, skip root node",
              ExpectedBreadthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0), (0, 0)),
                (TreenumeratorMode.SchedulingNode, "b", 0, (0, 1), (0, 0)),
                (TreenumeratorMode.VisitingNode,   "b", 1, (0, 1), (0, 0)),
              }.ToNodeVisitArray(),
              ExpectedDepthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0), (0, 0)),
                (TreenumeratorMode.SchedulingNode, "b", 0, (0, 1), (0, 0)),
                (TreenumeratorMode.VisitingNode,   "b", 1, (0, 1), (0, 0)),
              }.ToNodeVisitArray()
            },
            new TestScenario
            {
              TraversalStrategySelector = visit => visit.OriginalPosition.Depth == 0 ? TraversalStrategy.SkipNode : TraversalStrategy.TraverseSubtree,
              TreenumerableMap = treenumerable => treenumerable.PruneBefore(visit => visit.OriginalPosition.Depth == 1),
              Description = "Prune before level 1, skip root node",
              ExpectedBreadthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0), (0, 0)),
              }.ToNodeVisitArray(),
              ExpectedDepthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0), (0, 0)),
              }.ToNodeVisitArray()
            },
          }
        },

        // Two-level complete ternary tree.
        new TreeTestDefinition
        {
          TreeString = "a(b,c,d)",
          TestScenarios = new List<TestScenario>
          {
            // No skipping
            new TestScenario
            {
              TraversalStrategySelector = visit => TraversalStrategy.TraverseSubtree,
              TreenumerableMap = treenumerable => treenumerable.PruneBefore(visit => visit.OriginalPosition.Depth == 1),
              Description = "Prune before level 1, traverse all",
              ExpectedBreadthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0), (0, 0)),
                (TreenumeratorMode.VisitingNode,   "a", 1, (0, 0), (0, 0)),
              }.ToNodeVisitArray(),
              ExpectedDepthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0), (0, 0)),
                (TreenumeratorMode.VisitingNode,   "a", 1, (0, 0), (0, 0)),
              }.ToNodeVisitArray()
            },
          }
        },

        // Three root nodes, no children.
        new TreeTestDefinition
        {
          TreeString = "a,b,c",
          TestScenarios = new List<TestScenario>
          {
            // No skipping
            new TestScenario
            {
              TraversalStrategySelector = visit => TraversalStrategy.TraverseSubtree,
              TreenumerableMap = treenumerable => treenumerable.PruneBefore(_ => true),
              Description = "Prune before all, traverse all",
              ExpectedBreadthFirstResults = Array.Empty<NodeVisit<string>>(),
              ExpectedDepthFirstResults = Array.Empty<NodeVisit<string>>()
            },
            new TestScenario
            {
              TraversalStrategySelector = visit => TraversalStrategy.TraverseSubtree,
              TreenumerableMap = treenumerable => treenumerable.PruneBefore(visit => visit.OriginalPosition.SiblingIndex == 1),
              Description = "Prune before sibling 1, traverse all",
              ExpectedBreadthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0), (0, 0)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (2, 0), (1, 0)),
                (TreenumeratorMode.VisitingNode,   "a", 1, (0, 0), (0, 0)),
                (TreenumeratorMode.VisitingNode,   "c", 1, (2, 0), (1, 0)),
              }.ToNodeVisitArray(),
              ExpectedDepthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0), (0, 0)),
                (TreenumeratorMode.VisitingNode,   "a", 1, (0, 0), (0, 0)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (2, 0), (1, 0)),
                (TreenumeratorMode.VisitingNode,   "c", 1, (2, 0), (1, 0)),
              }.ToNodeVisitArray()
            },
            // Skip subtree
            new TestScenario
            {
              TraversalStrategySelector = visit => TraversalStrategy.SkipSubtree,
              TreenumerableMap = treenumerable => treenumerable.PruneBefore(_ => true),
              Description = "Prune before all, Skip all subtrees",
              ExpectedBreadthFirstResults = Array.Empty<NodeVisit<string>>(),
              ExpectedDepthFirstResults = Array.Empty<NodeVisit<string>>()
            },
            new TestScenario
            {
              TraversalStrategySelector = visit => visit.OriginalPosition.SiblingIndex == 0 ? TraversalStrategy.SkipSubtree : TraversalStrategy.TraverseSubtree,
              TreenumerableMap = treenumerable => treenumerable.PruneBefore(visit => visit.OriginalPosition.SiblingIndex == 1),
              Description = "Prune before sibling 1, skip sibling 0 subtree",
              ExpectedBreadthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0), (0, 0)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (2, 0), (0, 0)),
                (TreenumeratorMode.VisitingNode,   "c", 1, (2, 0), (0, 0)),
              }.ToNodeVisitArray(),
              ExpectedDepthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0), (0, 0)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (2, 0), (0, 0)),
                (TreenumeratorMode.VisitingNode,   "c", 1, (2, 0), (0, 0)),
              }.ToNodeVisitArray()
            },
          }
        },
      };

    [TestMethod]
    [DynamicData(nameof(GetTestData), DynamicDataSourceType.Method, DynamicDataDisplayName = nameof(GetTestDisplayName))]
    public void PruneBefore_BreadthFirst_EnumerableNodes(
      string treeString,
      string testDescription,
      int testIndex,
      int testScenarioIndex)
    {
      var testScenario = _TreenumerableTestDataFactory.GetTestScenario(testIndex, testScenarioIndex);

      TestMethods
      .TraversalTest(
        treeString,
        testScenario.TreenumerableMap,
        testScenario.TraversalStrategySelector,
        testScenario.ExpectedBreadthFirstResults,
        false,
        true);
    }

    [TestMethod]
    [DynamicData(nameof(GetTestData), DynamicDataSourceType.Method, DynamicDataDisplayName = nameof(GetTestDisplayName))]
    public void PruneBefore_DepthFirst_EnumerableNodes(
      string treeString,
      string testDescription,
      int testIndex,
      int testScenarioIndex)
    {
      var testScenario = _TreenumerableTestDataFactory.GetTestScenario(testIndex, testScenarioIndex);

      TestMethods
      .TraversalTest(
        treeString,
        testScenario.TreenumerableMap,
        testScenario.TraversalStrategySelector,
        testScenario.ExpectedDepthFirstResults,
        true,
        true);
    }

    [TestMethod]
    [DynamicData(nameof(GetTestData), DynamicDataSourceType.Method, DynamicDataDisplayName = nameof(GetTestDisplayName))]
    public void PruneBefore_BreadthFirst_IndexableNodes(
      string treeString,
      string testDescription,
      int testIndex,
      int testScenarioIndex)
    {
      var testScenario = _TreenumerableTestDataFactory.GetTestScenario(testIndex, testScenarioIndex);

      TestMethods
      .TraversalTest(
        treeString,
        testScenario.TreenumerableMap,
        testScenario.TraversalStrategySelector,
        testScenario.ExpectedBreadthFirstResults,
        false,
        false);
    }

    [TestMethod]
    [DynamicData(nameof(GetTestData), DynamicDataSourceType.Method, DynamicDataDisplayName = nameof(GetTestDisplayName))]
    public void PruneBefore_DepthFirst_IndexableNodes(
      string treeString,
      string testDescription,
      int testIndex,
      int testScenarioIndex)
    {
      var testScenario = _TreenumerableTestDataFactory.GetTestScenario(testIndex, testScenarioIndex);

      TestMethods
      .TraversalTest(
        treeString,
        testScenario.TreenumerableMap,
        testScenario.TraversalStrategySelector,
        testScenario.ExpectedDepthFirstResults,
        true,
        false);
    }
  }
}

