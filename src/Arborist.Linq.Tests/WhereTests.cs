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

namespace Arborist.Linq.Tests
{
  [TestClass]
  public class WhereTests
  {
    static WhereTests()
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
              TreenumerableMap = treenumerable => treenumerable.Where(_ => true),
              Description = "Where all, traverse all",
              ExpectedBreadthFirstResults = Array.Empty<NodeVisit<string>>(),
              ExpectedDepthFirstResults = Array.Empty<NodeVisit<string>>()
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
              TreenumerableMap = treenumerable => treenumerable.Where(_ => true),
              Description = "Where all, traverse all",
              ExpectedBreadthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0), (0, 0)),
                (TreenumeratorMode.VisitingNode,   "a", 1, (0, 0), (0, 0)),
                (TreenumeratorMode.SchedulingNode, "b", 0, (0, 1), (0, 1)),
                (TreenumeratorMode.VisitingNode,   "a", 2, (0, 0), (0, 0)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (1, 1), (1, 1)),
                (TreenumeratorMode.VisitingNode,   "a", 3, (0, 0), (0, 0)),
                (TreenumeratorMode.SchedulingNode, "d", 0, (2, 1), (2, 1)),
                (TreenumeratorMode.VisitingNode,   "a", 4, (0, 0), (0, 0)),
                (TreenumeratorMode.VisitingNode,   "b", 1, (0, 1), (0, 1)),
                (TreenumeratorMode.VisitingNode,   "c", 1, (1, 1), (1, 1)),
                (TreenumeratorMode.VisitingNode,   "d", 1, (2, 1), (2, 1)),
              }.ToNodeVisitArray(),
              ExpectedDepthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0), (0, 0)),
                (TreenumeratorMode.VisitingNode,   "a", 1, (0, 0), (0, 0)),
                (TreenumeratorMode.SchedulingNode, "b", 0, (0, 1), (0, 1)),
                (TreenumeratorMode.VisitingNode,   "b", 1, (0, 1), (0, 1)),
                (TreenumeratorMode.VisitingNode,   "a", 2, (0, 0), (0, 0)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (1, 1), (1, 1)),
                (TreenumeratorMode.VisitingNode,   "c", 1, (1, 1), (1, 1)),
                (TreenumeratorMode.VisitingNode,   "a", 3, (0, 0), (0, 0)),
                (TreenumeratorMode.SchedulingNode, "d", 0, (2, 1), (2, 1)),
                (TreenumeratorMode.VisitingNode,   "d", 1, (2, 1), (2, 1)),
                (TreenumeratorMode.VisitingNode,   "a", 4, (0, 0), (0, 0)),
              }.ToNodeVisitArray()
            },
            // No skipping
            new TestScenario
            {
              TraversalStrategySelector = visit => visit.Node == "c" ? TraversalStrategy.SkipSubtree : TraversalStrategy.TraverseSubtree,
              TreenumerableMap = treenumerable => treenumerable.Where(visit => visit.OriginalPosition.Depth == 1),
              Description = "Where level 1, traverse first and last",
              ExpectedBreadthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "b", 0, (0, 0), (0, 0)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (1, 0), (1, 0)),
                (TreenumeratorMode.SchedulingNode, "d", 0, (2, 0), (1, 0)),
                (TreenumeratorMode.VisitingNode,   "b", 1, (0, 0), (0, 0)),
                (TreenumeratorMode.VisitingNode,   "d", 1, (2, 0), (1, 0)),
              }.ToNodeVisitArray(),
              ExpectedDepthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "b", 0, (0, 0), (0, 0)),
                (TreenumeratorMode.VisitingNode,   "b", 1, (0, 0), (0, 0)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (1, 0), (1, 0)),
                (TreenumeratorMode.SchedulingNode, "d", 0, (2, 0), (1, 0)),
                (TreenumeratorMode.VisitingNode,   "d", 1, (2, 0), (1, 0)),
              }.ToNodeVisitArray()
            },
          }
        },

        new TreeTestDefinition
        {
          TreeString = "a,b(d),c",
          TestScenarios = new List<TestScenario>
          {
            // No skipping
            new TestScenario
            {
              TraversalStrategySelector = visit => TraversalStrategy.TraverseSubtree,
              TreenumerableMap = treenumerable => treenumerable.Where(visit => visit.Node != "b"),
              Description = "Where level 0 sibling 0, traverse all",
              ExpectedBreadthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0), (0, 0)),
                (TreenumeratorMode.SchedulingNode, "b", 0, (1, 0), (1, 0)),
                (TreenumeratorMode.SchedulingNode, "d", 0, (2, 0), (2, 0)),
                (TreenumeratorMode.VisitingNode,   "a", 1, (0, 0), (0, 0)),
                (TreenumeratorMode.VisitingNode,   "b", 1, (1, 0), (1, 0)),
                (TreenumeratorMode.VisitingNode,   "d", 1, (2, 0), (2, 0)),
              }.ToNodeVisitArray(),
              ExpectedDepthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0), (0, 0)),
                (TreenumeratorMode.VisitingNode,   "a", 1, (0, 0), (0, 0)),
                (TreenumeratorMode.SchedulingNode, "d", 0, (1, 0), (1, 0)),
                (TreenumeratorMode.VisitingNode,   "d", 1, (1, 0), (1, 0)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (2, 0), (2, 0)),
                (TreenumeratorMode.VisitingNode,   "c", 1, (2, 0), (2, 0)),
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
              TreenumerableMap = treenumerable => treenumerable.Where(_ => true),
              Description = "Where all, traverse all",
              ExpectedBreadthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0), (0, 0)),
                (TreenumeratorMode.SchedulingNode, "b", 0, (1, 0), (1, 0)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (2, 0), (2, 0)),
                (TreenumeratorMode.VisitingNode,   "a", 1, (0, 0), (0, 0)),
                (TreenumeratorMode.VisitingNode,   "b", 1, (1, 0), (1, 0)),
                (TreenumeratorMode.VisitingNode,   "c", 1, (2, 0), (2, 0)),
              }.ToNodeVisitArray(),
              ExpectedDepthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0), (0, 0)),
                (TreenumeratorMode.VisitingNode,   "a", 1, (0, 0), (0, 0)),
                (TreenumeratorMode.SchedulingNode, "b", 0, (1, 0), (1, 0)),
                (TreenumeratorMode.VisitingNode,   "b", 1, (1, 0), (1, 0)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (2, 0), (2, 0)),
                (TreenumeratorMode.VisitingNode,   "c", 1, (2, 0), (2, 0)),
              }.ToNodeVisitArray()
            },
            // Skip subtree
            new TestScenario
            {
              TraversalStrategySelector = visit => TraversalStrategy.SkipSubtree,
              TreenumerableMap = treenumerable => treenumerable.Where(_ => true),
              Description = "Where all, Skip all subtrees",
              ExpectedBreadthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0), (0, 0)),
                (TreenumeratorMode.SchedulingNode, "b", 0, (1, 0), (0, 0)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (2, 0), (0, 0)),
              }.ToNodeVisitArray(),
              ExpectedDepthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0), (0, 0)),
                (TreenumeratorMode.SchedulingNode, "b", 0, (1, 0), (0, 0)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (2, 0), (0, 0)),
              }.ToNodeVisitArray()
            },
            new TestScenario
            {
              TraversalStrategySelector = visit => visit.Node == "b" ? TraversalStrategy.SkipSubtree : TraversalStrategy.TraverseSubtree,
              TreenumerableMap = treenumerable => treenumerable.Where(visit => visit.Node != "a"),
              Description = "Where not a, Skip b subtree",
              ExpectedBreadthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "b", 0, (0, 0), (0, 0)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (1, 0), (0, 0)),
                (TreenumeratorMode.VisitingNode,   "c", 1, (1, 0), (0, 0)),
              }.ToNodeVisitArray(),
              ExpectedDepthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "b", 0, (0, 0), (0, 0)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (1, 0), (0, 0)),
                (TreenumeratorMode.VisitingNode,   "c", 1, (1, 0), (0, 0)),
              }.ToNodeVisitArray()
            },
          }
        },
      };

    [TestMethod]
    [DynamicData(nameof(GetTestData), DynamicDataSourceType.Method, DynamicDataDisplayName = nameof(GetTestDisplayName))]
    public void Where_BreadthFirst_EnumerableNodes(
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
    public void Where_DepthFirst_EnumerableNodes(
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
    public void Where_BreadthFirst_IndexableNodes(
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
    public void Where_DepthFirst_IndexableNodes(
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

