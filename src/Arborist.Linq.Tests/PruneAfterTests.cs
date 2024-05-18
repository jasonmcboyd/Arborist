using Arborist.Core;
using Arborist.TestUtils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Arborist.Linq.Tests
{
  [TestClass]
  public class PruneAfterTests
  {
    static PruneAfterTests()
    {
      _TreenumerableTestDataFactory = new TreenumerableTestDataFactory(TestTrees); 
    }

    private static readonly TreenumerableTestDataFactory _TreenumerableTestDataFactory;

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
              TreenumerableMap = treenumerable => treenumerable.PruneAfter(_ => true),
              Description = "Prune after all, traverse all",
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
              TraversalStrategySelector = visit => visit.Position.Depth == 0 ? TraversalStrategy.SkipNode : TraversalStrategy.TraverseSubtree,
              TreenumerableMap = treenumerable => treenumerable.PruneAfter(visit => visit.Position.Depth == 1),
              Description = "Prune after level 1, skip root node",
              ExpectedBreadthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "b", 0, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "b", 1, (0, 1)),
              }.ToNodeVisitArray(),
              ExpectedDepthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "b", 0, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "b", 1, (0, 1)),
              }.ToNodeVisitArray()
            },
          }
        },

        new TreeTestDefinition
        {
          TreeString = "a(b(e),c(f),d(g))",
          TestScenarios = new List<TestScenario>
          {
            // Skip node
            new TestScenario
            {
              TraversalStrategySelector = visit => visit.Position.Depth == 0 ? TraversalStrategy.SkipNode : TraversalStrategy.TraverseSubtree,
              TreenumerableMap = treenumerable => treenumerable.PruneAfter(visit => visit.Position.Depth == 0),
              Description = "prune after level 0, skip level 0 nodes",
              ExpectedBreadthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0)),
              }.ToNodeVisitArray(),
              ExpectedDepthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0)),
              }.ToNodeVisitArray()
            },
            new TestScenario
            {
              TraversalStrategySelector = visit => visit.Position.Depth == 1 ? TraversalStrategy.SkipNode : TraversalStrategy.TraverseSubtree,
              TreenumerableMap = treenumerable => treenumerable.PruneAfter(visit => visit.Position.Depth == 1),
              Description = "prune after level 1, skip level 1 nodes",
              ExpectedBreadthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0)),
                (TreenumeratorMode.VisitingNode,   "a", 1, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "b", 0, (0, 1)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (1, 1)),
                (TreenumeratorMode.SchedulingNode, "d", 0, (2, 1)),
              }.ToNodeVisitArray(),
              ExpectedDepthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0)),
                (TreenumeratorMode.VisitingNode,   "a", 1, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "b", 0, (0, 1)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (1, 1)),
                (TreenumeratorMode.SchedulingNode, "d", 0, (2, 1)),
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
              TreenumerableMap = treenumerable => treenumerable.PruneAfter(_ => true),
              Description = "Prune after all, traverse all",
              ExpectedBreadthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0)),
                (TreenumeratorMode.VisitingNode,   "a", 1, (0, 0)),
              }.ToNodeVisitArray(),
              ExpectedDepthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0)),
                (TreenumeratorMode.VisitingNode,   "a", 1, (0, 0)),
              }.ToNodeVisitArray()
            },
            // Skip descendants
            new TestScenario
            {
              TraversalStrategySelector = visit => visit.Position.Depth == 0 ? TraversalStrategy.SkipDescendants : TraversalStrategy.TraverseSubtree,
              TreenumerableMap = treenumerable => treenumerable.PruneAfter(_ => false),
              Description = "Prune after none, skip level 0 descendants",
              ExpectedBreadthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0)),
                (TreenumeratorMode.VisitingNode,   "a", 1, (0, 0)),
              }.ToNodeVisitArray(),
              ExpectedDepthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0)),
                (TreenumeratorMode.VisitingNode,   "a", 1, (0, 0)),
              }.ToNodeVisitArray()
            },
            // Skip nodes
            new TestScenario
            {
              TraversalStrategySelector = visit => visit.Node == "a" ? TraversalStrategy.SkipNode : TraversalStrategy.TraverseSubtree,
              TreenumerableMap = treenumerable => treenumerable.PruneAfter(visit => visit.Position.Depth != 0),
              Description = "Prune after all, skip a node",
              ExpectedBreadthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "b", 0, (0, 1)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (1, 1)),
                (TreenumeratorMode.SchedulingNode, "d", 0, (2, 1)),
                (TreenumeratorMode.VisitingNode,   "b", 1, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "c", 1, (1, 1)),
                (TreenumeratorMode.VisitingNode,   "d", 1, (2, 1)),
              }.ToNodeVisitArray(),
              ExpectedDepthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "b", 0, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "b", 1, (0, 1)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (1, 1)),
                (TreenumeratorMode.VisitingNode,   "c", 1, (1, 1)),
                (TreenumeratorMode.SchedulingNode, "d", 0, (2, 1)),
                (TreenumeratorMode.VisitingNode,   "d", 1, (2, 1)),
              }.ToNodeVisitArray()
            },
            // Skip subtree
            new TestScenario
            {
              TraversalStrategySelector = visit => visit.Node == "c" ? TraversalStrategy.SkipSubtree : TraversalStrategy.TraverseSubtree,
              TreenumerableMap = treenumerable => treenumerable.PruneAfter(_ => false),
              Description = "Prune after none, skip c subtree",
              ExpectedBreadthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0)),
                (TreenumeratorMode.VisitingNode,   "a", 1, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "b", 0, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "a", 2, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (1, 1)),
                (TreenumeratorMode.SchedulingNode, "d", 0, (2, 1)),
                (TreenumeratorMode.VisitingNode,   "a", 3, (0, 0)),
                (TreenumeratorMode.VisitingNode,   "b", 1, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "d", 1, (2, 1)),
              }.ToNodeVisitArray(),
              ExpectedDepthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0)),
                (TreenumeratorMode.VisitingNode,   "a", 1, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "b", 0, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "b", 1, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "a", 2, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (1, 1)),
                (TreenumeratorMode.SchedulingNode, "d", 0, (2, 1)),
                (TreenumeratorMode.VisitingNode,   "d", 1, (2, 1)),
                (TreenumeratorMode.VisitingNode,   "a", 3, (0, 0)),
              }.ToNodeVisitArray()
            },
          }
        },

        new TreeTestDefinition
        {
          TreeString = "a(d(g)),b(e(h)),c(f(i))",
          TestScenarios = new List<TestScenario>
          {
            // Skip node
            new TestScenario
            {
              TraversalStrategySelector = visit => visit.Position.Depth == 0 ? TraversalStrategy.SkipNode : TraversalStrategy.TraverseSubtree,
              TreenumerableMap = treenumerable => treenumerable.PruneAfter(visit => visit.Position.Depth == 1),
              Description = "Prune after level 1, skip level 0 nodes",
              ExpectedBreadthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "d", 0, (0, 1)),
                (TreenumeratorMode.SchedulingNode, "b", 0, (1, 0)),
                (TreenumeratorMode.SchedulingNode, "e", 0, (0, 1)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (2, 0)),
                (TreenumeratorMode.SchedulingNode, "f", 0, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "d", 1, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "e", 1, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "f", 1, (0, 1)),
              }.ToNodeVisitArray(),
              ExpectedDepthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "d", 0, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "d", 1, (0, 1)),
                (TreenumeratorMode.SchedulingNode, "b", 0, (1, 0)),
                (TreenumeratorMode.SchedulingNode, "e", 0, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "e", 1, (0, 1)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (2, 0)),
                (TreenumeratorMode.SchedulingNode, "f", 0, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "f", 1, (0, 1)),
              }.ToNodeVisitArray()
            },
          }
        },

        new TreeTestDefinition
        {
          TreeString = "a(d),b(e),c(f)",
          TestScenarios = new List<TestScenario>
          {
            // Skip node
            new TestScenario
            {
              TraversalStrategySelector = visit => TraversalStrategy.SkipNode,
              TreenumerableMap = treenumerable => treenumerable.PruneAfter(visit => visit.Position.Depth == 0),
              Description = "Prune after level 0, skip all nodes",
              ExpectedBreadthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "b", 0, (1, 0)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (2, 0)),
              }.ToNodeVisitArray(),
              ExpectedDepthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "b", 0, (1, 0)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (2, 0)),
              }.ToNodeVisitArray()
            },
            new TestScenario
            {
              TraversalStrategySelector = visit => visit.Node == "b" ? TraversalStrategy.SkipNode : TraversalStrategy.TraverseSubtree,
              TreenumerableMap = treenumerable => treenumerable.PruneAfter(visit => visit.Position.Depth == 0),
              Description = "Prune after level 0, skip level 0 sibling 1 node",
              ExpectedBreadthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "b", 0, (1, 0)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (2, 0)),
                (TreenumeratorMode.VisitingNode,   "a", 1, (0, 0)),
                (TreenumeratorMode.VisitingNode,   "c", 1, (2, 0)),
              }.ToNodeVisitArray(),
              ExpectedDepthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0)),
                (TreenumeratorMode.VisitingNode,   "a", 1, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "b", 0, (1, 0)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (2, 0)),
                (TreenumeratorMode.VisitingNode,   "c", 1, (2, 0)),
              }.ToNodeVisitArray()
            },
            new TestScenario
            {
              TraversalStrategySelector = visit => visit.Position.Depth == 0 ? TraversalStrategy.SkipNode : TraversalStrategy.TraverseSubtree,
              TreenumerableMap = treenumerable => treenumerable.PruneAfter(visit => visit.Position.Depth == 0),
              Description = "Prune after level 0, skip level 0 nodes",
              ExpectedBreadthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "b", 0, (1, 0)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (2, 0)),
              }.ToNodeVisitArray(),
              ExpectedDepthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "b", 0, (1, 0)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (2, 0)),
              }.ToNodeVisitArray()
            },
          }
        },

        new TreeTestDefinition
        {
          TreeString = "a,b(d),c",
          TestScenarios = new List<TestScenario>
          {
            // Skip node
            new TestScenario
            {
              TraversalStrategySelector = visit => visit.Node == "b" ? TraversalStrategy.SkipNode : TraversalStrategy.TraverseSubtree,
              TreenumerableMap = treenumerable => treenumerable.PruneAfter(_ => false),
              Description = "Prune after none, skip level 0 sibling 1 node",
              ExpectedBreadthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "b", 0, (1, 0)),
                (TreenumeratorMode.SchedulingNode, "d", 0, (0, 1)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (2, 0)),
                (TreenumeratorMode.VisitingNode,   "a", 1, (0, 0)),
                (TreenumeratorMode.VisitingNode,   "d", 1, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "c", 1, (2, 0)),
              }.ToNodeVisitArray(),
              ExpectedDepthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0)),
                (TreenumeratorMode.VisitingNode,   "a", 1, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "b", 0, (1, 0)),
                (TreenumeratorMode.SchedulingNode, "d", 0, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "d", 1, (0, 1)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (2, 0)),
                (TreenumeratorMode.VisitingNode,   "c", 1, (2, 0)),
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
              TreenumerableMap = treenumerable => treenumerable.PruneAfter(_ => true),
              Description = "Prune after all, traverse all",
              ExpectedBreadthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "b", 0, (1, 0)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (2, 0)),
                (TreenumeratorMode.VisitingNode,   "a", 1, (0, 0)),
                (TreenumeratorMode.VisitingNode,   "b", 1, (1, 0)),
                (TreenumeratorMode.VisitingNode,   "c", 1, (2, 0)),
              }.ToNodeVisitArray(),
              ExpectedDepthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0)),
                (TreenumeratorMode.VisitingNode,   "a", 1, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "b", 0, (1, 0)),
                (TreenumeratorMode.VisitingNode,   "b", 1, (1, 0)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (2, 0)),
                (TreenumeratorMode.VisitingNode,   "c", 1, (2, 0)),
              }.ToNodeVisitArray()
            },
            // Skip nodes
                        new TestScenario
            {
              TraversalStrategySelector = visit => visit.Node == "a" ? TraversalStrategy.SkipNode : TraversalStrategy.TraverseSubtree,
              TreenumerableMap = treenumerable => treenumerable.PruneAfter(visit => visit.Node == "b"),
              Description = "Prune after sibling 1, skip sibling 0 node",
              ExpectedBreadthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "b", 0, (1, 0)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (2, 0)),
                (TreenumeratorMode.VisitingNode,   "b", 1, (1, 0)),
                (TreenumeratorMode.VisitingNode,   "c", 1, (2, 0)),
              }.ToNodeVisitArray(),
              ExpectedDepthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "b", 0, (1, 0)),
                (TreenumeratorMode.VisitingNode,   "b", 1, (1, 0)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (2, 0)),
                (TreenumeratorMode.VisitingNode,   "c", 1, (2, 0)),
              }.ToNodeVisitArray()
            },

            // Skip subtree
            new TestScenario
            {
              TraversalStrategySelector = visit => TraversalStrategy.SkipSubtree,
              TreenumerableMap = treenumerable => treenumerable.PruneAfter(_ => true),
              Description = "Prune after all, Skip all subtrees",
              ExpectedBreadthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "b", 0, (1, 0)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (2, 0)),
              }.ToNodeVisitArray(),
              ExpectedDepthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "b", 0, (1, 0)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (2, 0)),
              }.ToNodeVisitArray()
            },

            // Skip node
            new TestScenario
            {
              TraversalStrategySelector = visit => TraversalStrategy.SkipNode,
              TreenumerableMap = treenumerable => treenumerable.PruneAfter(_ => false),
              Description = "Prune after none, Skip all nodes",
              ExpectedBreadthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "b", 0, (1, 0)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (2, 0)),
              }.ToNodeVisitArray(),
              ExpectedDepthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "b", 0, (1, 0)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (2, 0)),
              }.ToNodeVisitArray()
            },
          }
        },
      };

    [TestMethod]
    [DynamicData(nameof(GetTestData), DynamicDataSourceType.Method, DynamicDataDisplayName = nameof(GetTestDisplayName))]
    public void PruneAfter_BreadthFirst_EnumerableNodes(
      string treeString,
      string testDescription,
      int testIndex,
      int testScenarioIndex)
    {
      var testScenario = _TreenumerableTestDataFactory.GetTestScenario(testIndex, testScenarioIndex);

      TestMethods
      .TraversalTest(
        treeString,
        testDescription,
        testScenario.TreenumerableMap,
        testScenario.TraversalStrategySelector,
        testScenario.ExpectedBreadthFirstResults,
        false,
        true);
    }

    [TestMethod]
    [DynamicData(nameof(GetTestData), DynamicDataSourceType.Method, DynamicDataDisplayName = nameof(GetTestDisplayName))]
    public void PruneAfter_DepthFirst_EnumerableNodes(
      string treeString,
      string testDescription,
      int testIndex,
      int testScenarioIndex)
    {
      var testScenario = _TreenumerableTestDataFactory.GetTestScenario(testIndex, testScenarioIndex);

      TestMethods
      .TraversalTest(
        treeString,
        testDescription,
        testScenario.TreenumerableMap,
        testScenario.TraversalStrategySelector,
        testScenario.ExpectedDepthFirstResults,
        true,
        true);
    }

    [TestMethod]
    [DynamicData(nameof(GetTestData), DynamicDataSourceType.Method, DynamicDataDisplayName = nameof(GetTestDisplayName))]
    public void PruneAfter_BreadthFirst_IndexableNodes(
      string treeString,
      string testDescription,
      int testIndex,
      int testScenarioIndex)
    {
      var testScenario = _TreenumerableTestDataFactory.GetTestScenario(testIndex, testScenarioIndex);

      TestMethods
      .TraversalTest(
        treeString,
        testDescription,
        testScenario.TreenumerableMap,
        testScenario.TraversalStrategySelector,
        testScenario.ExpectedBreadthFirstResults,
        false,
        false);
    }

    [TestMethod]
    [DynamicData(nameof(GetTestData), DynamicDataSourceType.Method, DynamicDataDisplayName = nameof(GetTestDisplayName))]
    public void PruneAfter_DepthFirst_IndexableNodes(
      string treeString,
      string testDescription,
      int testIndex,
      int testScenarioIndex)
    {
      var testScenario = _TreenumerableTestDataFactory.GetTestScenario(testIndex, testScenarioIndex);

      TestMethods
      .TraversalTest(
        treeString,
        testDescription,
        testScenario.TreenumerableMap,
        testScenario.TraversalStrategySelector,
        testScenario.ExpectedDepthFirstResults,
        true,
        false);
    }
  }
}

