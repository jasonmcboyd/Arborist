using Arborist.Core;
using Arborist.TestUtils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
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
            // Traverse all
            new TestScenario
            {
              NodeTraversalStrategiesSelector = nc => NodeTraversalStrategies.TraverseAll,
              TreenumerableMap = treenumerable => treenumerable.Where(_ => true),
              Description = "Where all, traverse all",
              ExpectedBreadthFirstResults = Array.Empty<NodeVisit<string>>(),
              ExpectedDepthFirstResults = Array.Empty<NodeVisit<string>>()
            },
          }
        },

        new TreeTestDefinition
        {
          TreeString = "a(b(c))",
          TestScenarios = new List<TestScenario>
          {
            // Skip node
            new TestScenario
            {
              NodeTraversalStrategiesSelector = nc => NodeTraversalStrategies.TraverseAll,
              TreenumerableMap = treenumerable => treenumerable.Where(nc => nc.Position.Depth != 0),
              Description = "Where not level 0",
              ExpectedBreadthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "b", 0, (0, 0)),
                (TreenumeratorMode.VisitingNode,   "b", 1, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "b", 2, (0, 0)),
                (TreenumeratorMode.VisitingNode,   "c", 1, (0, 1)),
              }.ToNodeVisitArray(),
              ExpectedDepthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "b", 0, (0, 0)),
                (TreenumeratorMode.VisitingNode,   "b", 1, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "c", 1, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "b", 2, (0, 0)),
              }.ToNodeVisitArray(),
            },
            new TestScenario
            {
              NodeTraversalStrategiesSelector = nc => nc.Position.Depth < 2 ? NodeTraversalStrategies.SkipNode : NodeTraversalStrategies.TraverseAll,
              TreenumerableMap = treenumerable => treenumerable.Where(nc => true),
              Description = "Where all, skip level 0 and 1 nodes",
              ExpectedBreadthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "b", 0, (0, 1)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (0, 2)),
                (TreenumeratorMode.VisitingNode,   "c", 1, (0, 2)),
              }.ToNodeVisitArray(),
              ExpectedDepthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "b", 0, (0, 1)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (0, 2)),
                (TreenumeratorMode.VisitingNode,   "c", 1, (0, 2)),
              }.ToNodeVisitArray()
            },
            new TestScenario
            {
              NodeTraversalStrategiesSelector =
                nc =>
                  nc.Position.Depth != 1
                  ? NodeTraversalStrategies.SkipNode
                  : NodeTraversalStrategies.TraverseAll,
              TreenumerableMap = treenumerable => treenumerable.Where(nc => true),
              Description = "Where all, skip level 0 and 2 nodes",
              ExpectedBreadthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "b", 0, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "b", 1, (0, 1)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (0, 2)),
                (TreenumeratorMode.VisitingNode,   "b", 2, (0, 1)),
              }.ToNodeVisitArray(),
              ExpectedDepthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "b", 0, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "b", 1, (0, 1)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (0, 2)),
                (TreenumeratorMode.VisitingNode,   "b", 2, (0, 1)),
              }.ToNodeVisitArray()
            },
            new TestScenario
            {
              NodeTraversalStrategiesSelector = nc => nc.Position.Depth == 0 ? NodeTraversalStrategies.SkipNode : NodeTraversalStrategies.TraverseAll,
              TreenumerableMap = treenumerable => treenumerable.Where(nc => nc.Position.Depth < 2),
              Description = "Where before level 2, skip level 0",
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
            new TestScenario
            {
              NodeTraversalStrategiesSelector = nc => nc.Position.Depth == 0 ? NodeTraversalStrategies.SkipNode : NodeTraversalStrategies.TraverseAll,
              TreenumerableMap = treenumerable => treenumerable.Where(nc => nc.Position.Depth < 1),
              Description = "Where before level 1, skip level 0",
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
              NodeTraversalStrategiesSelector = nc => nc.Position.Depth == 0 ? NodeTraversalStrategies.SkipNode : NodeTraversalStrategies.TraverseAll,
              TreenumerableMap = treenumerable => treenumerable.Where(nc => nc.Position.Depth != 0),
              Description = "Where not level 0, skip level 0",
              ExpectedBreadthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "b", 0, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "c", 1, (0, 1)),
              }.ToNodeVisitArray(),
              ExpectedDepthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "b", 0, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "c", 1, (0, 1)),
              }.ToNodeVisitArray()
            },
            new TestScenario
            {
              NodeTraversalStrategiesSelector = nc => nc.Position.Depth == 0 ? NodeTraversalStrategies.SkipNode : NodeTraversalStrategies.TraverseAll,
              TreenumerableMap = treenumerable => treenumerable.Where(nc => nc.Position.Depth != 1),
              Description = "Where not level 1, skip level 0",
              ExpectedBreadthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "c", 1, (0, 1)),
              }.ToNodeVisitArray(),
              ExpectedDepthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "c", 1, (0, 1)),
              }.ToNodeVisitArray()
            },
            new TestScenario
            {
              NodeTraversalStrategiesSelector =
                nc =>
                  nc.Node == "c"
                  ? NodeTraversalStrategies.SkipNode
                  : NodeTraversalStrategies.TraverseAll,
              TreenumerableMap = treenumerable => treenumerable.Where(nc => true),
              Description = "Where all, skip node c",
              ExpectedBreadthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0)),
                (TreenumeratorMode.VisitingNode,   "a", 1, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "b", 0, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "a", 2, (0, 0)),
                (TreenumeratorMode.VisitingNode,   "b", 1, (0, 1)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (0, 2)),
                (TreenumeratorMode.VisitingNode,   "b", 2, (0, 1)),
              }.ToNodeVisitArray(),
              ExpectedDepthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0)),
                (TreenumeratorMode.VisitingNode,   "a", 1, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "b", 0, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "b", 1, (0, 1)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (0, 2)),
                (TreenumeratorMode.VisitingNode,   "b", 2, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "a", 2, (0, 0)),
              }.ToNodeVisitArray()
            },
            new TestScenario
            {
              NodeTraversalStrategiesSelector =
                nc =>
                  nc.Node == "c"
                  ? NodeTraversalStrategies.SkipNode
                  : NodeTraversalStrategies.TraverseAll,
              TreenumerableMap = treenumerable => treenumerable.Where(nc => nc.Node != "b"),
              Description = "Where not node b, skip node c",
              ExpectedBreadthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0)),
                (TreenumeratorMode.VisitingNode,   "a", 1, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "a", 2, (0, 0)),
              }.ToNodeVisitArray(),
              ExpectedDepthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0)),
                (TreenumeratorMode.VisitingNode,   "a", 1, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "a", 2, (0, 0)),
              }.ToNodeVisitArray()
            },

            // Skip siblings
            new TestScenario
            {
              NodeTraversalStrategiesSelector =
                nc =>
                  nc.Node == "a"
                  ? NodeTraversalStrategies.SkipSiblings
                  : NodeTraversalStrategies.TraverseAll,
              TreenumerableMap = treenumerable => treenumerable.Where(_ => true),
              Description = "Where all, skip node a siblings",
              ExpectedBreadthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0)),
                (TreenumeratorMode.VisitingNode,   "a", 1, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "b", 0, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "a", 2, (0, 0)),
                (TreenumeratorMode.VisitingNode,   "b", 1, (0, 1)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (0, 2)),
                (TreenumeratorMode.VisitingNode,   "b", 2, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "c", 1, (0, 2)),
              }.ToNodeVisitArray(),
              ExpectedDepthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0)),
                (TreenumeratorMode.VisitingNode,   "a", 1, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "b", 0, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "b", 1, (0, 1)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (0, 2)),
                (TreenumeratorMode.VisitingNode,   "c", 1, (0, 2)),
                (TreenumeratorMode.VisitingNode,   "b", 2, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "a", 2, (0, 0)),
              }.ToNodeVisitArray(),
            },

            // Mixed
            new TestScenario
            {
              NodeTraversalStrategiesSelector =
                nc =>
                  nc.Node == "a"
                  ? NodeTraversalStrategies.SkipSiblings
                  : nc.Node == "b"
                  ? NodeTraversalStrategies.SkipNode
                  : NodeTraversalStrategies.TraverseAll,
              TreenumerableMap = treenumerable => treenumerable.Where(_ => true),
              Description = "Where all, skip node a siblings, skip node b node",
              ExpectedBreadthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0)),
                (TreenumeratorMode.VisitingNode,   "a", 1, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "b", 0, (0, 1)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (0, 2)),
                (TreenumeratorMode.VisitingNode,   "a", 2, (0, 0)),
                (TreenumeratorMode.VisitingNode,   "c", 1, (0, 2)),
              }.ToNodeVisitArray(),
              ExpectedDepthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0)),
                (TreenumeratorMode.VisitingNode,   "a", 1, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "b", 0, (0, 1)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (0, 2)),
                (TreenumeratorMode.VisitingNode,   "c", 1, (0, 2)),
                (TreenumeratorMode.VisitingNode,   "a", 2, (0, 0)),
              }.ToNodeVisitArray(),
            },
            new TestScenario
            {
              NodeTraversalStrategiesSelector =
                nc =>
                  nc.Node == "b"
                  ? NodeTraversalStrategies.SkipNodeAndSiblings
                  : NodeTraversalStrategies.TraverseAll,
              TreenumerableMap = treenumerable => treenumerable.Where(nc => nc.Node != "c"),
              Description = "Where not node c, skip node b node and siblings",
              ExpectedBreadthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0)),
                (TreenumeratorMode.VisitingNode,   "a", 1, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "b", 0, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "a", 2, (0, 0)),
              }.ToNodeVisitArray(),
              ExpectedDepthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0)),
                (TreenumeratorMode.VisitingNode,   "a", 1, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "b", 0, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "a", 2, (0, 0)),
              }.ToNodeVisitArray()
            },
          }
        },

        new TreeTestDefinition
        {
          TreeString = "a(b,c,d)",
          TestScenarios = new List<TestScenario>
          {
            // Traverse all
            new TestScenario
            {
              NodeTraversalStrategiesSelector = nc => NodeTraversalStrategies.TraverseAll,
              TreenumerableMap = treenumerable => treenumerable.Where(nc => nc.Position.Depth < 1),
              Description = "Where before level 1, traverse all",
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
            new TestScenario
            {
              NodeTraversalStrategiesSelector = nc => NodeTraversalStrategies.TraverseAll,
              TreenumerableMap = treenumerable => treenumerable.Where(nc => nc.Node != "b"),
              Description = "Where not level 1 sibling 0, traverse all",
              ExpectedBreadthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0)),
                (TreenumeratorMode.VisitingNode,   "a", 1, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "a", 2, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "d", 0, (1, 1)),
                (TreenumeratorMode.VisitingNode,   "a", 3, (0, 0)),
                (TreenumeratorMode.VisitingNode,   "c", 1, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "d", 1, (1, 1)),
              }.ToNodeVisitArray(),
              ExpectedDepthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0)),
                (TreenumeratorMode.VisitingNode,   "a", 1, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "c", 1, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "a", 2, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "d", 0, (1, 1)),
                (TreenumeratorMode.VisitingNode,   "d", 1, (1, 1)),
                (TreenumeratorMode.VisitingNode,   "a", 3, (0, 0)),
              }.ToNodeVisitArray()
            },
            new TestScenario
            {
              NodeTraversalStrategiesSelector = nc => NodeTraversalStrategies.TraverseAll,
              TreenumerableMap = treenumerable => treenumerable.Where(nc => nc.Position.Depth == 1),
              Description = "Where level 1",
              ExpectedBreadthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "b", 0, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (1, 0)),
                (TreenumeratorMode.SchedulingNode, "d", 0, (2, 0)),
                (TreenumeratorMode.VisitingNode,   "b", 1, (0, 0)),
                (TreenumeratorMode.VisitingNode,   "c", 1, (1, 0)),
                (TreenumeratorMode.VisitingNode,   "d", 1, (2, 0)),
              }.ToNodeVisitArray(),
              ExpectedDepthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "b", 0, (0, 0)),
                (TreenumeratorMode.VisitingNode,   "b", 1, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (1, 0)),
                (TreenumeratorMode.VisitingNode,   "c", 1, (1, 0)),
                (TreenumeratorMode.SchedulingNode, "d", 0, (2, 0)),
                (TreenumeratorMode.VisitingNode,   "d", 1, (2, 0)),
              }.ToNodeVisitArray()
            },
            new TestScenario
            {
              NodeTraversalStrategiesSelector = nc => NodeTraversalStrategies.TraverseAll,
              TreenumerableMap = treenumerable => treenumerable.Where(nc => nc.Node != "c"),
              Description = "Where not level 1 sibling 1, traverse all",
              ExpectedBreadthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0)),
                (TreenumeratorMode.VisitingNode,   "a", 1, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "b", 0, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "a", 2, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "d", 0, (1, 1)),
                (TreenumeratorMode.VisitingNode,   "a", 3, (0, 0)),
                (TreenumeratorMode.VisitingNode,   "b", 1, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "d", 1, (1, 1)),
              }.ToNodeVisitArray(),
              ExpectedDepthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0)),
                (TreenumeratorMode.VisitingNode,   "a", 1, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "b", 0, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "b", 1, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "a", 2, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "d", 0, (1, 1)),
                (TreenumeratorMode.VisitingNode,   "d", 1, (1, 1)),
                (TreenumeratorMode.VisitingNode,   "a", 3, (0, 0)),
              }.ToNodeVisitArray()
            },

            // Skip descendants
            new TestScenario
            {
              NodeTraversalStrategiesSelector = nc => NodeTraversalStrategies.SkipDescendants,
              TreenumerableMap = treenumerable => treenumerable.Where(nc => nc.Position.Depth == 1),
              Description = "Where level 1, skip descendants",
              ExpectedBreadthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "b", 0, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (1, 0)),
                (TreenumeratorMode.SchedulingNode, "d", 0, (2, 0)),
                (TreenumeratorMode.VisitingNode,   "b", 1, (0, 0)),
                (TreenumeratorMode.VisitingNode,   "c", 1, (1, 0)),
                (TreenumeratorMode.VisitingNode,   "d", 1, (2, 0)),
              }.ToNodeVisitArray(),
              ExpectedDepthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "b", 0, (0, 0)),
                (TreenumeratorMode.VisitingNode,   "b", 1, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (1, 0)),
                (TreenumeratorMode.VisitingNode,   "c", 1, (1, 0)),
                (TreenumeratorMode.SchedulingNode, "d", 0, (2, 0)),
                (TreenumeratorMode.VisitingNode,   "d", 1, (2, 0)),
              }.ToNodeVisitArray()
            },

            // Skip node
            new TestScenario
            {
              NodeTraversalStrategiesSelector = nc => NodeTraversalStrategies.SkipNode,
              TreenumerableMap = treenumerable => treenumerable.Where(nc => true),
              Description = "Where all, skip all nodes",
              ExpectedBreadthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "b", 0, (0, 1)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (1, 1)),
                (TreenumeratorMode.SchedulingNode, "d", 0, (2, 1)),
              }.ToNodeVisitArray(),
              ExpectedDepthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "b", 0, (0, 1)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (1, 1)),
                (TreenumeratorMode.SchedulingNode, "d", 0, (2, 1)),
              }.ToNodeVisitArray()
            },
            new TestScenario
            {
              NodeTraversalStrategiesSelector =
                nc =>
                  nc.Node == "b"
                  ? NodeTraversalStrategies.SkipNode
                  : NodeTraversalStrategies.TraverseAll,
              TreenumerableMap = treenumerable => treenumerable.Where(nc => true),
              Description = "Where all, skip node b",
              ExpectedBreadthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0)),
                (TreenumeratorMode.VisitingNode,   "a", 1, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "b", 0, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "a", 2, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (1, 1)),
                (TreenumeratorMode.VisitingNode,   "a", 3, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "d", 0, (1, 1)),
                (TreenumeratorMode.VisitingNode,   "a", 4, (0, 0)),
                (TreenumeratorMode.VisitingNode,   "c", 1, (1, 1)),
                (TreenumeratorMode.VisitingNode,   "d", 1, (1, 1)),
              }.ToNodeVisitArray(),
              ExpectedDepthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0)),
                (TreenumeratorMode.VisitingNode,   "a", 1, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "b", 0, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "a", 2, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (1, 1)),
                (TreenumeratorMode.VisitingNode,   "c", 1, (1, 1)),
                (TreenumeratorMode.VisitingNode,   "a", 3, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "d", 0, (2, 1)),
                (TreenumeratorMode.VisitingNode,   "d", 1, (2, 1)),
                (TreenumeratorMode.VisitingNode,   "a", 4, (0, 0)),
              }.ToNodeVisitArray()
            },
            new TestScenario
            {
              NodeTraversalStrategiesSelector =
                nc =>
                  nc.Node == "c"
                  ? NodeTraversalStrategies.SkipNode
                  : NodeTraversalStrategies.TraverseAll,
              TreenumerableMap = treenumerable => treenumerable.Where(nc => nc.Node != "d"),
              Description = "Where not d, skip node c",
              ExpectedBreadthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0)),
                (TreenumeratorMode.VisitingNode,   "a", 1, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "b", 0, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "a", 2, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (1, 1)),
                (TreenumeratorMode.VisitingNode,   "a", 3, (0, 0)),
                (TreenumeratorMode.VisitingNode,   "b", 1, (0, 1)),
              }.ToNodeVisitArray(),
              ExpectedDepthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0)),
                (TreenumeratorMode.VisitingNode,   "a", 1, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "b", 0, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "b", 1, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "a", 2, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (1, 1)),
                (TreenumeratorMode.VisitingNode,   "a", 3, (0, 0)),
              }.ToNodeVisitArray()
            },
            new TestScenario
            {
              NodeTraversalStrategiesSelector =
                nc =>
                  nc.Node == "c"
                  ? NodeTraversalStrategies.SkipNode
                  : NodeTraversalStrategies.TraverseAll,
              TreenumerableMap = treenumerable => treenumerable.Where(nc => nc.Node != "b"),
              Description = "Where not node b, skip node c",
              ExpectedBreadthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0)),
                (TreenumeratorMode.VisitingNode,   "a", 1, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "a", 2, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "d", 0, (1, 1)),
                (TreenumeratorMode.VisitingNode,   "a", 3, (0, 0)),
                (TreenumeratorMode.VisitingNode,   "d", 1, (1, 1)),
              }.ToNodeVisitArray(),
              ExpectedDepthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0)),
                (TreenumeratorMode.VisitingNode,   "a", 1, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "a", 2, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "d", 0, (1, 1)),
                (TreenumeratorMode.VisitingNode,   "d", 1, (1, 1)),
                (TreenumeratorMode.VisitingNode,   "a", 3, (0, 0)),
              }.ToNodeVisitArray()
            },
            new TestScenario
            {
              NodeTraversalStrategiesSelector = nc => nc.Position.Depth == 1 ? NodeTraversalStrategies.SkipNode : NodeTraversalStrategies.TraverseAll,
              TreenumerableMap = treenumerable => treenumerable.Where(nc => nc.Node != "b"),
              Description = "Where not level 1 sibling 0, skip level 1 nodes",
              ExpectedBreadthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0)),
                (TreenumeratorMode.VisitingNode,   "a", 1, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "a", 2, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "d", 0, (1, 1)),
                (TreenumeratorMode.VisitingNode,   "a", 3, (0, 0)),
              }.ToNodeVisitArray(),
              ExpectedDepthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0)),
                (TreenumeratorMode.VisitingNode,   "a", 1, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "a", 2, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "d", 0, (1, 1)),
                (TreenumeratorMode.VisitingNode,   "a", 3, (0, 0)),
              }.ToNodeVisitArray()
            },
            new TestScenario
            {
              NodeTraversalStrategiesSelector = nc => nc.Position.Depth == 0 ? NodeTraversalStrategies.SkipNode : NodeTraversalStrategies.TraverseAll,
              TreenumerableMap = treenumerable => treenumerable.Where(nc => nc.Node != "c"),
              Description = "Where not level 1 sibling 1, skip level 0 nodes",
              ExpectedBreadthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "b", 0, (0, 1)),
                (TreenumeratorMode.SchedulingNode, "d", 0, (1, 1)),
                (TreenumeratorMode.VisitingNode,   "b", 1, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "d", 1, (1, 1)),
              }.ToNodeVisitArray(),
              ExpectedDepthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "b", 0, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "b", 1, (0, 1)),
                (TreenumeratorMode.SchedulingNode, "d", 0, (1, 1)),
                (TreenumeratorMode.VisitingNode,   "d", 1, (1, 1)),
              }.ToNodeVisitArray()
            },

            // Skip subtree
            new TestScenario
            {
              NodeTraversalStrategiesSelector = nc => nc.Position.Depth == 1 && nc.Position.SiblingIndex == 0 ? NodeTraversalStrategies.SkipNodeAndDescendants : NodeTraversalStrategies.TraverseAll,
              TreenumerableMap = treenumerable => treenumerable.Where(nc => nc.Position.Depth != 1 || nc.Position.SiblingIndex != 0),
              Description = "Where not level 1 sibling 0, skip level 1 sibling 0 subtree",
              ExpectedBreadthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0)),
                (TreenumeratorMode.VisitingNode,   "a", 1, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "a", 2, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "d", 0, (1, 1)),
                (TreenumeratorMode.VisitingNode,   "a", 3, (0, 0)),
                (TreenumeratorMode.VisitingNode,   "d", 1, (1, 1)),
              }.ToNodeVisitArray(),
              ExpectedDepthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0)),
                (TreenumeratorMode.VisitingNode,   "a", 1, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "a", 2, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "d", 0, (1, 1)),
                (TreenumeratorMode.VisitingNode,   "d", 1, (1, 1)),
                (TreenumeratorMode.VisitingNode,   "a", 3, (0, 0)),
              }.ToNodeVisitArray()
            },
          }
        },

        new TreeTestDefinition
        {
          TreeString = "a(b(c,d,e))",
          TestScenarios = new List<TestScenario>
          {
            // Traverse all
            new TestScenario
            {
              NodeTraversalStrategiesSelector = nc => NodeTraversalStrategies.TraverseAll,
              TreenumerableMap = treenumerable => treenumerable.Where(nc => nc.Node != "b" && nc.Node != "d"),
              Description = "Where not b and d, traverse all",
              ExpectedBreadthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0)),
                (TreenumeratorMode.VisitingNode,   "a", 1, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "a", 2, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "e", 0, (1, 1)),
                (TreenumeratorMode.VisitingNode,   "a", 3, (0, 0)),
                (TreenumeratorMode.VisitingNode,   "c", 1, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "e", 1, (1, 1)),
              }.ToNodeVisitArray(),
              ExpectedDepthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0)),
                (TreenumeratorMode.VisitingNode,   "a", 1, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "c", 1, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "a", 2, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "e", 0, (1, 1)),
                (TreenumeratorMode.VisitingNode,   "e", 1, (1, 1)),
                (TreenumeratorMode.VisitingNode,   "a", 3, (0, 0)),
              }.ToNodeVisitArray()
            },

            // Skip node
            new TestScenario
            {
              NodeTraversalStrategiesSelector = nc => nc.Position.Depth == 0 ? NodeTraversalStrategies.TraverseAll : NodeTraversalStrategies.SkipNode,
              TreenumerableMap = treenumerable => treenumerable.Where(nc => true),
              Description = "Where all, skip level > 0 nodes",
              ExpectedBreadthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0)),
                (TreenumeratorMode.VisitingNode,   "a", 1, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "b", 0, (0, 1)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (0, 2)),
                (TreenumeratorMode.VisitingNode,   "a", 2, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "d", 0, (1, 2)),
                (TreenumeratorMode.VisitingNode,   "a", 3, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "e", 0, (2, 2)),
                (TreenumeratorMode.VisitingNode,   "a", 4, (0, 0)),
              }.ToNodeVisitArray(),
              ExpectedDepthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0)),
                (TreenumeratorMode.VisitingNode,   "a", 1, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "b", 0, (0, 1)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (0, 2)),
                (TreenumeratorMode.VisitingNode,   "a", 2, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "d", 0, (1, 2)),
                (TreenumeratorMode.VisitingNode,   "a", 3, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "e", 0, (2, 2)),
                (TreenumeratorMode.VisitingNode,   "a", 4, (0, 0)),
              }.ToNodeVisitArray()
            },
            new TestScenario
            {
              NodeTraversalStrategiesSelector =
                nc =>
                  nc.Position.Depth == 2
                  ? NodeTraversalStrategies.TraverseAll
                  : NodeTraversalStrategies.SkipNode,
              TreenumerableMap = treenumerable => treenumerable.Where(nc => true),
              Description = "Where all, skip levels 0 and 1 ",
              ExpectedBreadthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "b", 0, (0, 1)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (0, 2)),
                (TreenumeratorMode.SchedulingNode, "d", 0, (1, 2)),
                (TreenumeratorMode.SchedulingNode, "e", 0, (2, 2)),
                (TreenumeratorMode.VisitingNode,   "c", 1, (0, 2)),
                (TreenumeratorMode.VisitingNode,   "d", 1, (1, 2)),
                (TreenumeratorMode.VisitingNode,   "e", 1, (2, 2)),
              }.ToNodeVisitArray(),
              ExpectedDepthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "b", 0, (0, 1)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (0, 2)),
                (TreenumeratorMode.VisitingNode,   "c", 1, (0, 2)),
                (TreenumeratorMode.SchedulingNode, "d", 0, (1, 2)),
                (TreenumeratorMode.VisitingNode,   "d", 1, (1, 2)),
                (TreenumeratorMode.SchedulingNode, "e", 0, (2, 2)),
                (TreenumeratorMode.VisitingNode,   "e", 1, (2, 2)),
              }.ToNodeVisitArray()
            },
            new TestScenario
            {
              NodeTraversalStrategiesSelector =
                nc =>
                  nc.Node == "b"
                  ? NodeTraversalStrategies.SkipNode
                  : NodeTraversalStrategies.TraverseAll,
              TreenumerableMap = treenumerable => treenumerable.Where(nc => true),
              Description = "Where all, skip node b",
              ExpectedBreadthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0)),
                (TreenumeratorMode.VisitingNode,   "a", 1, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "b", 0, (0, 1)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (0, 2)),
                (TreenumeratorMode.VisitingNode,   "a", 2, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "d", 0, (1, 2)),
                (TreenumeratorMode.VisitingNode,   "a", 3, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "e", 0, (2, 2)),
                (TreenumeratorMode.VisitingNode,   "a", 4, (0, 0)),
                (TreenumeratorMode.VisitingNode,   "c", 1, (0, 2)),
                (TreenumeratorMode.VisitingNode,   "d", 1, (1, 2)),
                (TreenumeratorMode.VisitingNode,   "e", 1, (2, 2)),
              }.ToNodeVisitArray(),
              ExpectedDepthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0)),
                (TreenumeratorMode.VisitingNode,   "a", 1, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "b", 0, (0, 1)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (0, 2)),
                (TreenumeratorMode.VisitingNode,   "c", 1, (0, 2)),
                (TreenumeratorMode.VisitingNode,   "a", 2, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "d", 0, (1, 2)),
                (TreenumeratorMode.VisitingNode,   "d", 1, (1, 2)),
                (TreenumeratorMode.VisitingNode,   "a", 3, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "e", 0, (2, 2)),
                (TreenumeratorMode.VisitingNode,   "e", 1, (2, 2)),
                (TreenumeratorMode.VisitingNode,   "a", 4, (0, 0)),
              }.ToNodeVisitArray()
            },
            new TestScenario
            {
              NodeTraversalStrategiesSelector =
                nc =>
                  nc.Position.Depth == 2
                  ? NodeTraversalStrategies.TraverseAll
                  : NodeTraversalStrategies.SkipNode,
              TreenumerableMap = treenumerable => treenumerable.Where(nc => nc.Node != "d"),
              Description = "Where not d, skip levels 0 and 1 ",
              ExpectedBreadthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "b", 0, (0, 1)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (0, 2)),
                (TreenumeratorMode.SchedulingNode, "e", 0, (1, 2)),
                (TreenumeratorMode.VisitingNode,   "c", 1, (0, 2)),
                (TreenumeratorMode.VisitingNode,   "e", 1, (1, 2)),
              }.ToNodeVisitArray(),
              ExpectedDepthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "b", 0, (0, 1)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (0, 2)),
                (TreenumeratorMode.VisitingNode,   "c", 1, (0, 2)),
                (TreenumeratorMode.SchedulingNode, "e", 0, (1, 2)),
                (TreenumeratorMode.VisitingNode,   "e", 1, (1, 2)),
              }.ToNodeVisitArray()
            },
            new TestScenario
            {
              NodeTraversalStrategiesSelector =
                nc =>
                  nc.Node == "c"
                  ? NodeTraversalStrategies.SkipNode
                  : NodeTraversalStrategies.TraverseAll,
              TreenumerableMap = treenumerable => treenumerable.Where(nc => nc.Node != "b" && nc.Node != "d"),
              Description = "Where not b and d, skip node c",
              ExpectedBreadthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0)),
                (TreenumeratorMode.VisitingNode,   "a", 1, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "a", 2, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "e", 0, (1, 1)),
                (TreenumeratorMode.VisitingNode,   "a", 3, (0, 0)),
                (TreenumeratorMode.VisitingNode,   "e", 1, (1, 1)),
              }.ToNodeVisitArray(),
              ExpectedDepthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0)),
                (TreenumeratorMode.VisitingNode,   "a", 1, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "a", 2, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "e", 0, (1, 1)),
                (TreenumeratorMode.VisitingNode,   "e", 1, (1, 1)),
                (TreenumeratorMode.VisitingNode,   "a", 3, (0, 0)),
              }.ToNodeVisitArray()
            },
            new TestScenario
            {
              NodeTraversalStrategiesSelector =
                nc =>
                  nc.Node == "d"
                  ? NodeTraversalStrategies.SkipNode
                  : NodeTraversalStrategies.TraverseAll,
              TreenumerableMap = treenumerable => treenumerable.Where(nc => nc.Node != "b" && nc.Node != "e"),
              Description = "Where not b and e, skip node d",
              ExpectedBreadthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0)),
                (TreenumeratorMode.VisitingNode,   "a", 1, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "a", 2, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "d", 0, (1, 1)),
                (TreenumeratorMode.VisitingNode,   "a", 3, (0, 0)),
                (TreenumeratorMode.VisitingNode,   "c", 1, (0, 1)),
              }.ToNodeVisitArray(),
              ExpectedDepthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0)),
                (TreenumeratorMode.VisitingNode,   "a", 1, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "c", 1, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "a", 2, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "d", 0, (1, 1)),
                (TreenumeratorMode.VisitingNode,   "a", 3, (0, 0)),
              }.ToNodeVisitArray()
            },
            new TestScenario
            {
              NodeTraversalStrategiesSelector =
                nc => NodeTraversalStrategies.TraverseAll,
              TreenumerableMap = treenumerable => treenumerable.Where(nc => nc.Node != "b"),
              Description = "Where not node b",
              ExpectedBreadthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0)),
                (TreenumeratorMode.VisitingNode,   "a", 1, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "a", 2, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "d", 0, (1, 1)),
                (TreenumeratorMode.VisitingNode,   "a", 3, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "e", 0, (2, 1)),
                (TreenumeratorMode.VisitingNode,   "a", 4, (0, 0)),
                (TreenumeratorMode.VisitingNode,   "c", 1, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "d", 1, (1, 1)),
                (TreenumeratorMode.VisitingNode,   "e", 1, (2, 1)),
              }.ToNodeVisitArray(),
              ExpectedDepthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0)),
                (TreenumeratorMode.VisitingNode,   "a", 1, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "c", 1, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "a", 2, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "d", 0, (1, 1)),
                (TreenumeratorMode.VisitingNode,   "d", 1, (1, 1)),
                (TreenumeratorMode.VisitingNode,   "a", 3, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "e", 0, (2, 1)),
                (TreenumeratorMode.VisitingNode,   "e", 1, (2, 1)),
                (TreenumeratorMode.VisitingNode,   "a", 4, (0, 0)),
              }.ToNodeVisitArray()
            },
            new TestScenario
            {
              NodeTraversalStrategiesSelector =
                nc =>
                  nc.Node == "b"
                  ? NodeTraversalStrategies.SkipNode
                  : NodeTraversalStrategies.TraverseAll,
              TreenumerableMap = treenumerable => treenumerable.Where(nc => nc.Node != "d"),
              Description = "Where not d, skip node b",
              ExpectedBreadthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0)),
                (TreenumeratorMode.VisitingNode,   "a", 1, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "b", 0, (0, 1)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (0, 2)),
                (TreenumeratorMode.VisitingNode,   "a", 2, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "e", 0, (1, 2)),
                (TreenumeratorMode.VisitingNode,   "a", 3, (0, 0)),
                (TreenumeratorMode.VisitingNode,   "c", 1, (0, 2)),
                (TreenumeratorMode.VisitingNode,   "e", 1, (1, 2)),
              }.ToNodeVisitArray(),
              ExpectedDepthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0)),
                (TreenumeratorMode.VisitingNode,   "a", 1, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "b", 0, (0, 1)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (0, 2)),
                (TreenumeratorMode.VisitingNode,   "c", 1, (0, 2)),
                (TreenumeratorMode.VisitingNode,   "a", 2, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "e", 0, (1, 2)),
                (TreenumeratorMode.VisitingNode,   "e", 1, (1, 2)),
                (TreenumeratorMode.VisitingNode,   "a", 3, (0, 0)),
              }.ToNodeVisitArray()
            },
            new TestScenario
            {
              NodeTraversalStrategiesSelector =
                nc =>
                  nc.Node == "b"
                  ? NodeTraversalStrategies.SkipNode
                  : NodeTraversalStrategies.TraverseAll,
              TreenumerableMap =
                treenumerable =>
                  treenumerable
                  .Where(nc => nc.Node != "d")
                  .Where(nc => nc.Node != "e"),
              Description = "Where not d composed with where not e, skip node b",
              ExpectedBreadthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0)),
                (TreenumeratorMode.VisitingNode,   "a", 1, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "b", 0, (0, 1)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (0, 2)),
                (TreenumeratorMode.VisitingNode,   "a", 2, (0, 0)),
                (TreenumeratorMode.VisitingNode,   "c", 1, (0, 2)),
              }.ToNodeVisitArray(),
              ExpectedDepthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0)),
                (TreenumeratorMode.VisitingNode,   "a", 1, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "b", 0, (0, 1)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (0, 2)),
                (TreenumeratorMode.VisitingNode,   "c", 1, (0, 2)),
                (TreenumeratorMode.VisitingNode,   "a", 2, (0, 0)),
              }.ToNodeVisitArray()
            },
            new TestScenario
            {
              NodeTraversalStrategiesSelector =
                nc =>
                  nc.Node == "b"
                  ? NodeTraversalStrategies.SkipNode
                  : NodeTraversalStrategies.TraverseAll,
              TreenumerableMap =
                treenumerable =>
                  treenumerable
                  .Where(nc => nc.Node != "e")
                  .Where(nc => nc.Node != "d"),
              Description = "Where not e composed with where not d, skip node b",
              ExpectedBreadthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0)),
                (TreenumeratorMode.VisitingNode,   "a", 1, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "b", 0, (0, 1)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (0, 2)),
                (TreenumeratorMode.VisitingNode,   "a", 2, (0, 0)),
                (TreenumeratorMode.VisitingNode,   "c", 1, (0, 2)),
              }.ToNodeVisitArray(),
              ExpectedDepthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0)),
                (TreenumeratorMode.VisitingNode,   "a", 1, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "b", 0, (0, 1)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (0, 2)),
                (TreenumeratorMode.VisitingNode,   "c", 1, (0, 2)),
                (TreenumeratorMode.VisitingNode,   "a", 2, (0, 0)),
              }.ToNodeVisitArray()
            },
            new TestScenario
            {
              NodeTraversalStrategiesSelector =
                nc =>
                  nc.Node == "b"
                  ? NodeTraversalStrategies.SkipNode
                  : NodeTraversalStrategies.TraverseAll,
              TreenumerableMap =
                treenumerable =>
                  treenumerable
                  .Where(nc => nc.Node != "d" && nc.Node != "e"),
              Description = "Where not d and e, skip node b",
              ExpectedBreadthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0)),
                (TreenumeratorMode.VisitingNode,   "a", 1, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "b", 0, (0, 1)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (0, 2)),
                (TreenumeratorMode.VisitingNode,   "a", 2, (0, 0)),
                (TreenumeratorMode.VisitingNode,   "c", 1, (0, 2)),
              }.ToNodeVisitArray(),
              ExpectedDepthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0)),
                (TreenumeratorMode.VisitingNode,   "a", 1, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "b", 0, (0, 1)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (0, 2)),
                (TreenumeratorMode.VisitingNode,   "c", 1, (0, 2)),
                (TreenumeratorMode.VisitingNode,   "a", 2, (0, 0)),
              }.ToNodeVisitArray()
            },
            new TestScenario
            {
              NodeTraversalStrategiesSelector =
                nc =>
                  nc.Node == "b" || nc.Node == "e"
                  ? NodeTraversalStrategies.SkipNode
                  : NodeTraversalStrategies.TraverseAll,
              TreenumerableMap = treenumerable => treenumerable.Where(nc => nc.Node != "d"),
              Description = "Where not d, skip nodes b and e",
              ExpectedBreadthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0)),
                (TreenumeratorMode.VisitingNode,   "a", 1, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "b", 0, (0, 1)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (0, 2)),
                (TreenumeratorMode.VisitingNode,   "a", 2, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "e", 0, (1, 2)),
                (TreenumeratorMode.VisitingNode,   "a", 3, (0, 0)),
                (TreenumeratorMode.VisitingNode,   "c", 1, (0, 2)),
              }.ToNodeVisitArray(),
              ExpectedDepthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0)),
                (TreenumeratorMode.VisitingNode,   "a", 1, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "b", 0, (0, 1)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (0, 2)),
                (TreenumeratorMode.VisitingNode,   "c", 1, (0, 2)),
                (TreenumeratorMode.VisitingNode,   "a", 2, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "e", 0, (1, 2)),
                (TreenumeratorMode.VisitingNode,   "a", 3, (0, 0)),
              }.ToNodeVisitArray()
            },
          }
        },

        new TreeTestDefinition
        {
          TreeString = "a(c,d),b(e,f)",
          TestScenarios = new List<TestScenario>
          {
            // Skip nodes
            new TestScenario
            {
              NodeTraversalStrategiesSelector =
                nc =>
                  nc.Node != "a"
                  ? NodeTraversalStrategies.SkipNode
                  : NodeTraversalStrategies.TraverseAll,
              TreenumerableMap = treenumerable => treenumerable.Where(_ => true),
              Description = "Where all, skip all nodes except a",
              ExpectedBreadthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "b", 0, (1, 0)),
                (TreenumeratorMode.SchedulingNode, "e", 0, (0, 1)),
                (TreenumeratorMode.SchedulingNode, "f", 0, (1, 1)),
                (TreenumeratorMode.VisitingNode,   "a", 1, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "a", 2, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "d", 0, (1, 1)),
                (TreenumeratorMode.VisitingNode,   "a", 3, (0, 0)),
              }.ToNodeVisitArray(),
              ExpectedDepthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0)),
                (TreenumeratorMode.VisitingNode,   "a", 1, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "a", 2, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "d", 0, (1, 1)),
                (TreenumeratorMode.VisitingNode,   "a", 3, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "b", 0, (1, 0)),
                (TreenumeratorMode.SchedulingNode, "e", 0, (0, 1)),
                (TreenumeratorMode.SchedulingNode, "f", 0, (1, 1)),
              }.ToNodeVisitArray()
            },
          }
        },

        new TreeTestDefinition
        {
          TreeString = "a(b(d(e)),c)",
          TestScenarios = new List<TestScenario>
          {
            // Skip node
            new TestScenario
            {
              NodeTraversalStrategiesSelector =
                nc =>
                  nc.Node == "a" || nc.Node == "b" || nc.Node == "e"
                  ? NodeTraversalStrategies.SkipNode
                  : NodeTraversalStrategies.TraverseAll,
              TreenumerableMap = treenumerable => treenumerable.Where(nc => true),
              Description = "Where all, skip a, b, and e",
              ExpectedBreadthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "b", 0, (0, 1)),
                (TreenumeratorMode.SchedulingNode, "d", 0, (0, 2)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (1, 1)),
                (TreenumeratorMode.VisitingNode,   "d", 1, (0, 2)),
                (TreenumeratorMode.SchedulingNode, "e", 0, (0, 3)),
                (TreenumeratorMode.VisitingNode,   "d", 2, (0, 2)),
                (TreenumeratorMode.VisitingNode,   "c", 1, (1, 1)),
              }.ToNodeVisitArray(),
              ExpectedDepthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "b", 0, (0, 1)),
                (TreenumeratorMode.SchedulingNode, "d", 0, (0, 2)),
                (TreenumeratorMode.VisitingNode,   "d", 1, (0, 2)),
                (TreenumeratorMode.SchedulingNode, "e", 0, (0, 3)),
                (TreenumeratorMode.VisitingNode,   "d", 2, (0, 2)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (1, 1)),
                (TreenumeratorMode.VisitingNode,   "c", 1, (1, 1)),
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
              NodeTraversalStrategiesSelector = nc => nc.Position.Depth == 0 ? NodeTraversalStrategies.SkipNode : NodeTraversalStrategies.TraverseAll,
              TreenumerableMap = treenumerable => treenumerable.Where(nc => nc.Position.Depth < 2),
              Description = "Where before level 2, skip level 0 nodes",
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
            new TestScenario
            {
              NodeTraversalStrategiesSelector =
                nc =>
                  nc.Position.Depth == 1
                  ? NodeTraversalStrategies.SkipNode
                  : NodeTraversalStrategies.TraverseAll,
              TreenumerableMap = treenumerable => treenumerable.Where(nc => nc.Position.Depth < 2),
              Description = "Where before level 2, skip level 1 nodes",
              ExpectedBreadthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "b", 0, (1, 0)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (2, 0)),
                (TreenumeratorMode.VisitingNode,   "a", 1, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "d", 0, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "a", 2, (0, 0)),
                (TreenumeratorMode.VisitingNode,   "b", 1, (1, 0)),
                (TreenumeratorMode.SchedulingNode, "e", 0, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "b", 2, (1, 0)),
                (TreenumeratorMode.VisitingNode,   "c", 1, (2, 0)),
                (TreenumeratorMode.SchedulingNode, "f", 0, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "c", 2, (2, 0)),
              }.ToNodeVisitArray(),
              ExpectedDepthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0)),
                (TreenumeratorMode.VisitingNode,   "a", 1, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "d", 0, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "a", 2, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "b", 0, (1, 0)),
                (TreenumeratorMode.VisitingNode,   "b", 1, (1, 0)),
                (TreenumeratorMode.SchedulingNode, "e", 0, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "b", 2, (1, 0)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (2, 0)),
                (TreenumeratorMode.VisitingNode,   "c", 1, (2, 0)),
                (TreenumeratorMode.SchedulingNode, "f", 0, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "c", 2, (2, 0)),
              }.ToNodeVisitArray()
            },
          }
        },

        new TreeTestDefinition
        {
          TreeString = "a(d),b(e),c(f)",
          TestScenarios = new List<TestScenario>
          {
            // Traverse all
            new TestScenario
            {
              NodeTraversalStrategiesSelector = nc => NodeTraversalStrategies.TraverseAll,
              TreenumerableMap = treenumerable => treenumerable.Where(nc => nc.Position.Depth < 1),
              Description = "Where before level 1",
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

            // Skip node
            new TestScenario
            {
              NodeTraversalStrategiesSelector = nc => nc.Position.Depth == 0 ? NodeTraversalStrategies.SkipNode : NodeTraversalStrategies.TraverseAll,
              TreenumerableMap = treenumerable => treenumerable.Where(nc => nc.Position.Depth < 1),
              Description = "Where before level 1, skip level 0 nodes",
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
              NodeTraversalStrategiesSelector = nc => nc.Position.Depth == 0 ? NodeTraversalStrategies.SkipNode : NodeTraversalStrategies.TraverseAll,
              TreenumerableMap = treenumerable => treenumerable.Where(nc => nc.Node != "b"),
              Description = "Where not level 0 sibling 1, skip level 0 nodes",
              ExpectedBreadthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "d", 0, (0, 1)),
                (TreenumeratorMode.SchedulingNode, "e", 0, (1, 0)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (2, 0)),
                (TreenumeratorMode.SchedulingNode, "f", 0, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "d", 1, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "f", 1, (0, 1)),
              }.ToNodeVisitArray(),
              ExpectedDepthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "d", 0, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "d", 1, (0, 1)),
                (TreenumeratorMode.SchedulingNode, "e", 0, (1, 0)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (2, 0)),
                (TreenumeratorMode.SchedulingNode, "f", 0, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "f", 1, (0, 1)),
              }.ToNodeVisitArray()
            },
            new TestScenario
            {
              NodeTraversalStrategiesSelector = nc => nc.Node == "a" ? NodeTraversalStrategies.SkipNode : NodeTraversalStrategies.TraverseAll,
              TreenumerableMap = treenumerable => treenumerable.Where(nc => nc.Node != "b"),
              Description = "Where not level 0 sibling 1, skip level 0 sibling 0 node",
              ExpectedBreadthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "d", 0, (0, 1)),
                (TreenumeratorMode.SchedulingNode, "e", 0, (1, 0)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (2, 0)),
                (TreenumeratorMode.VisitingNode,   "d", 1, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "e", 1, (1, 0)),
                (TreenumeratorMode.VisitingNode,   "c", 1, (2, 0)),
                (TreenumeratorMode.SchedulingNode, "f", 0, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "c", 2, (2, 0)),
                (TreenumeratorMode.VisitingNode,   "f", 1, (0, 1)),
              }.ToNodeVisitArray(),
              ExpectedDepthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "d", 0, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "d", 1, (0, 1)),
                (TreenumeratorMode.SchedulingNode, "e", 0, (1, 0)),
                (TreenumeratorMode.VisitingNode,   "e", 1, (1, 0)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (2, 0)),
                (TreenumeratorMode.VisitingNode,   "c", 1, (2, 0)),
                (TreenumeratorMode.SchedulingNode, "f", 0, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "f", 1, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "c", 2, (2, 0)),
              }.ToNodeVisitArray()
            },
            new TestScenario
            {
              NodeTraversalStrategiesSelector = nc => NodeTraversalStrategies.TraverseAll,
              TreenumerableMap = treenumerable => treenumerable.Where(nc => nc.Position.Depth == 1),
              Description = "Where level 1",
              ExpectedBreadthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "d", 0, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "e", 0, (1, 0)),
                (TreenumeratorMode.SchedulingNode, "f", 0, (2, 0)),
                (TreenumeratorMode.VisitingNode,   "d", 1, (0, 0)),
                (TreenumeratorMode.VisitingNode,   "e", 1, (1, 0)),
                (TreenumeratorMode.VisitingNode,   "f", 1, (2, 0)),
              }.ToNodeVisitArray(),
              ExpectedDepthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "d", 0, (0, 0)),
                (TreenumeratorMode.VisitingNode,   "d", 1, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "e", 0, (1, 0)),
                (TreenumeratorMode.VisitingNode,   "e", 1, (1, 0)),
                (TreenumeratorMode.SchedulingNode, "f", 0, (2, 0)),
                (TreenumeratorMode.VisitingNode,   "f", 1, (2, 0)),
              }.ToNodeVisitArray()
            },

            // Skip subtree
            new TestScenario
            {
              NodeTraversalStrategiesSelector = nc => nc.Node == "b" ? NodeTraversalStrategies.SkipNodeAndDescendants : NodeTraversalStrategies.TraverseAll,
              TreenumerableMap = treenumerable => treenumerable.Where(nc => nc.Position.Depth < 1),
              Description = "Where before level 1, skip level 0, sibling 1 subtree",
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
              NodeTraversalStrategiesSelector = nc => NodeTraversalStrategies.SkipNodeAndDescendants,
              TreenumerableMap = treenumerable => treenumerable.Where(nc => nc.Position.Depth < 1),
              Description = "Where before level 1, skip all subtrees",
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
          TreeString = "a,b(c,d)",
          TestScenarios = new List<TestScenario>
          {
            // Traverse all
            new TestScenario
            {
              NodeTraversalStrategiesSelector = nc => NodeTraversalStrategies.TraverseAll,
              TreenumerableMap = treenumerable => treenumerable.Where(nc => nc.Position.SiblingIndex != 0),
              Description = "Where not sibling 0, traverse all",
              ExpectedBreadthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "b", 0, (0, 0)),
                (TreenumeratorMode.VisitingNode,   "b", 1, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "d", 0, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "b", 2, (0, 0)),
                (TreenumeratorMode.VisitingNode,   "d", 1, (0, 1)),
              }.ToNodeVisitArray(),
              ExpectedDepthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "b", 0, (0, 0)),
                (TreenumeratorMode.VisitingNode,   "b", 1, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "d", 0, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "d", 1, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "b", 2, (0, 0)),
              }.ToNodeVisitArray()
            },
          }
        },

        new TreeTestDefinition
        {
          TreeString = "a,b(d),c",
          TestScenarios = new List<TestScenario>
          {
            // Traverse all
            new TestScenario
            {
              NodeTraversalStrategiesSelector = nc => NodeTraversalStrategies.TraverseAll,
              TreenumerableMap = treenumerable => treenumerable.Where(nc => nc.Node != "b"),
              Description = "Where not level 0 sibling 1, traverse all",
              ExpectedBreadthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "d", 0, (1, 0)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (2, 0)),
                (TreenumeratorMode.VisitingNode,   "a", 1, (0, 0)),
                (TreenumeratorMode.VisitingNode,   "d", 1, (1, 0)),
                (TreenumeratorMode.VisitingNode,   "c", 1, (2, 0)),
              }.ToNodeVisitArray(),
              ExpectedDepthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0)),
                (TreenumeratorMode.VisitingNode,   "a", 1, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "d", 0, (1, 0)),
                (TreenumeratorMode.VisitingNode,   "d", 1, (1, 0)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (2, 0)),
                (TreenumeratorMode.VisitingNode,   "c", 1, (2, 0)),
              }.ToNodeVisitArray()
            },

            // Skip node and siblings
            new TestScenario
            {
              NodeTraversalStrategiesSelector =
                nc =>
                  nc.Node == "b"
                  ? NodeTraversalStrategies.SkipNodeAndSiblings
                  : NodeTraversalStrategies.TraverseAll,
              TreenumerableMap = treenumerable => treenumerable.Where(nc => nc.Node != "a" && nc.Node != "c"),
              Description = "Where not a and c, skip node b node and siblings",
              ExpectedBreadthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "b", 0, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "d", 0, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "d", 1, (0, 1)),
              }.ToNodeVisitArray(),
              ExpectedDepthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "b", 0, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "d", 0, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "d", 1, (0, 1)),
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
              NodeTraversalStrategiesSelector = nc => nc.Position.Depth == 1 ? NodeTraversalStrategies.SkipNode : NodeTraversalStrategies.TraverseAll,
              TreenumerableMap = treenumerable => treenumerable.Where(nc => nc.Position.Depth < 2),
              Description = "Where before level 2, skip level 1 nodes",
              ExpectedBreadthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0)),
                (TreenumeratorMode.VisitingNode,   "a", 1, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "b", 0, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "a", 2, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (1, 1)),
                (TreenumeratorMode.VisitingNode,   "a", 3, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "d", 0, (2, 1)),
                (TreenumeratorMode.VisitingNode,   "a", 4, (0, 0)),
              }.ToNodeVisitArray(),
              ExpectedDepthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0)),
                (TreenumeratorMode.VisitingNode,   "a", 1, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "b", 0, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "a", 2, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (1, 1)),
                (TreenumeratorMode.VisitingNode,   "a", 3, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "d", 0, (2, 1)),
                (TreenumeratorMode.VisitingNode,   "a", 4, (0, 0)),
              }.ToNodeVisitArray()
            },
            new TestScenario
            {
              NodeTraversalStrategiesSelector = nc => nc.Position.Depth == 0 ? NodeTraversalStrategies.SkipNode : NodeTraversalStrategies.TraverseAll,
              TreenumerableMap = treenumerable => treenumerable.Where(nc => nc.Position.Depth < 2),
              Description = "Where before level 2, skip level 0 nodes",
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
            new TestScenario
            {
              NodeTraversalStrategiesSelector = nc => nc.Position.Depth == 0 ? NodeTraversalStrategies.SkipNode : NodeTraversalStrategies.TraverseAll,
              TreenumerableMap = treenumerable => treenumerable.Where(nc => nc.Position.Depth < 1),
              Description = "Where before level 1, skip level 0 nodes",
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
              NodeTraversalStrategiesSelector =
                nc =>
                  nc.Position.Depth == 1
                  ? NodeTraversalStrategies.SkipNode
                  : NodeTraversalStrategies.TraverseAll,
              TreenumerableMap = treenumerable => treenumerable.Where(nc => nc.Position != new NodePosition(1, 1)),
              Description = "Where not level 1 sibling 1, skip level 1 nodes",
              ExpectedBreadthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0)),
                (TreenumeratorMode.VisitingNode,   "a", 1, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "b", 0, (0, 1)),
                (TreenumeratorMode.SchedulingNode, "e", 0, (0, 2)),
                (TreenumeratorMode.VisitingNode,   "a", 2, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "f", 0, (1, 1)),
                (TreenumeratorMode.VisitingNode,   "a", 3, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "d", 0, (2, 1)),
                (TreenumeratorMode.SchedulingNode, "g", 0, (0, 2)),
                (TreenumeratorMode.VisitingNode,   "a", 4, (0, 0)),
                (TreenumeratorMode.VisitingNode,   "e", 1, (0, 2)),
                (TreenumeratorMode.VisitingNode,   "g", 1, (0, 2)),
              }.ToNodeVisitArray(),
              ExpectedDepthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0)),
                (TreenumeratorMode.VisitingNode,   "a", 1, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "b", 0, (0, 1)),
                (TreenumeratorMode.SchedulingNode, "e", 0, (0, 2)),
                (TreenumeratorMode.VisitingNode,   "e", 1, (0, 2)),
                (TreenumeratorMode.VisitingNode,   "a", 2, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "f", 0, (1, 1)),
                (TreenumeratorMode.VisitingNode,   "a", 3, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "d", 0, (2, 1)),
                (TreenumeratorMode.SchedulingNode, "g", 0, (0, 2)),
                (TreenumeratorMode.VisitingNode,   "g", 1, (0, 2)),
                (TreenumeratorMode.VisitingNode,   "a", 4, (0, 0)),
              }.ToNodeVisitArray()
            },
          }
        },

        new TreeTestDefinition
        {
          TreeString = "a,b(d,e,f),c",
          TestScenarios = new List<TestScenario>
          {
            new TestScenario
            {
              NodeTraversalStrategiesSelector = nc => nc.Node == "b" ? NodeTraversalStrategies.SkipNode : NodeTraversalStrategies.TraverseAll,
              TreenumerableMap = treenumerable => treenumerable.Where(nc => nc.Position.Depth < 1),
              Description = "Where before level 1, skip node b",
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
              NodeTraversalStrategiesSelector = nc => nc.Node == "b" ? NodeTraversalStrategies.SkipNode : NodeTraversalStrategies.TraverseAll,
              TreenumerableMap = treenumerable => treenumerable.Where(nc => nc.Node != "a"),
              Description = "Where not level 0, sibling 0, skip node b",
              ExpectedBreadthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "b", 0, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "d", 0, (0, 1)),
                (TreenumeratorMode.SchedulingNode, "e", 0, (1, 1)),
                (TreenumeratorMode.SchedulingNode, "f", 0, (2, 1)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (1, 0)),
                (TreenumeratorMode.VisitingNode,   "d", 1, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "e", 1, (1, 1)),
                (TreenumeratorMode.VisitingNode,   "f", 1, (2, 1)),
                (TreenumeratorMode.VisitingNode,   "c", 1, (1, 0)),
              }.ToNodeVisitArray(),
              ExpectedDepthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "b", 0, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "d", 0, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "d", 1, (0, 1)),
                (TreenumeratorMode.SchedulingNode, "e", 0, (1, 1)),
                (TreenumeratorMode.VisitingNode,   "e", 1, (1, 1)),
                (TreenumeratorMode.SchedulingNode, "f", 0, (2, 1)),
                (TreenumeratorMode.VisitingNode,   "f", 1, (2, 1)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (1, 0)),
                (TreenumeratorMode.VisitingNode,   "c", 1, (1, 0)),
              }.ToNodeVisitArray()
            },
            new TestScenario
            {
              NodeTraversalStrategiesSelector = nc => NodeTraversalStrategies.TraverseAll,
              TreenumerableMap = treenumerable => treenumerable.Where(nc => nc.Node != "b"),
              Description = "Where not level 0 sibling 1, traverse all",
              ExpectedBreadthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "d", 0, (1, 0)),
                (TreenumeratorMode.SchedulingNode, "e", 0, (2, 0)),
                (TreenumeratorMode.SchedulingNode, "f", 0, (3, 0)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (4, 0)),
                (TreenumeratorMode.VisitingNode,   "a", 1, (0, 0)),
                (TreenumeratorMode.VisitingNode,   "d", 1, (1, 0)),
                (TreenumeratorMode.VisitingNode,   "e", 1, (2, 0)),
                (TreenumeratorMode.VisitingNode,   "f", 1, (3, 0)),
                (TreenumeratorMode.VisitingNode,   "c", 1, (4, 0)),
              }.ToNodeVisitArray(),
              ExpectedDepthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0)),
                (TreenumeratorMode.VisitingNode,   "a", 1, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "d", 0, (1, 0)),
                (TreenumeratorMode.VisitingNode,   "d", 1, (1, 0)),
                (TreenumeratorMode.SchedulingNode, "e", 0, (2, 0)),
                (TreenumeratorMode.VisitingNode,   "e", 1, (2, 0)),
                (TreenumeratorMode.SchedulingNode, "f", 0, (3, 0)),
                (TreenumeratorMode.VisitingNode,   "f", 1, (3, 0)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (4, 0)),
                (TreenumeratorMode.VisitingNode,   "c", 1, (4, 0)),
              }.ToNodeVisitArray()
            },
            new TestScenario
            {
              NodeTraversalStrategiesSelector = nc => nc.Node == "b" || nc.Node == "e" ? NodeTraversalStrategies.SkipNode : NodeTraversalStrategies.TraverseAll,
              TreenumerableMap = treenumerable => treenumerable.Where(nc => nc.Position.SiblingIndex != 0),
              Description = "Where not sibling 0, skip node b and e",
              ExpectedBreadthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "b", 0, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "e", 0, (0, 1)),
                (TreenumeratorMode.SchedulingNode, "f", 0, (1, 1)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (1, 0)),
                (TreenumeratorMode.VisitingNode,   "f", 1, (1, 1)),
                (TreenumeratorMode.VisitingNode,   "c", 1, (1, 0)),
              }.ToNodeVisitArray(),
              ExpectedDepthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "b", 0, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "e", 0, (0, 1)),
                (TreenumeratorMode.SchedulingNode, "f", 0, (1, 1)),
                (TreenumeratorMode.VisitingNode,   "f", 1, (1, 1)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (1, 0)),
                (TreenumeratorMode.VisitingNode,   "c", 1, (1, 0)),
              }.ToNodeVisitArray()
            },
            new TestScenario
            {
              NodeTraversalStrategiesSelector = nc => nc.Node == "b" ? NodeTraversalStrategies.SkipNode : NodeTraversalStrategies.TraverseAll,
              TreenumerableMap = treenumerable => treenumerable.Where(nc => nc.Position.SiblingIndex != 0),
              Description = "Where not sibling 0, skip node b",
              ExpectedBreadthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "b", 0, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "e", 0, (0, 1)),
                (TreenumeratorMode.SchedulingNode, "f", 0, (1, 1)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (1, 0)),
                (TreenumeratorMode.VisitingNode,   "e", 1, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "f", 1, (1, 1)),
                (TreenumeratorMode.VisitingNode,   "c", 1, (1, 0)),
              }.ToNodeVisitArray(),
              ExpectedDepthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "b", 0, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "e", 0, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "e", 1, (0, 1)),
                (TreenumeratorMode.SchedulingNode, "f", 0, (1, 1)),
                (TreenumeratorMode.VisitingNode,   "f", 1, (1, 1)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (1, 0)),
                (TreenumeratorMode.VisitingNode,   "c", 1, (1, 0)),
              }.ToNodeVisitArray()
            },
            new TestScenario
            {
              NodeTraversalStrategiesSelector = nc => nc.Node == "b" ? NodeTraversalStrategies.SkipNode : NodeTraversalStrategies.TraverseAll,
              TreenumerableMap = treenumerable => treenumerable.Where(nc => nc.Node != "d"),
              Description = "Where not level 1 sibling 0, skip node b",
              ExpectedBreadthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "b", 0, (1, 0)),
                (TreenumeratorMode.SchedulingNode, "e", 0, (0, 1)),
                (TreenumeratorMode.SchedulingNode, "f", 0, (1, 1)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (2, 0)),
                (TreenumeratorMode.VisitingNode,   "a", 1, (0, 0)),
                (TreenumeratorMode.VisitingNode,   "e", 1, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "f", 1, (1, 1)),
                (TreenumeratorMode.VisitingNode,   "c", 1, (2, 0)),
              }.ToNodeVisitArray(),
              ExpectedDepthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0)),
                (TreenumeratorMode.VisitingNode,   "a", 1, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "b", 0, (1, 0)),
                (TreenumeratorMode.SchedulingNode, "e", 0, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "e", 1, (0, 1)),
                (TreenumeratorMode.SchedulingNode, "f", 0, (1, 1)),
                (TreenumeratorMode.VisitingNode,   "f", 1, (1, 1)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (2, 0)),
                (TreenumeratorMode.VisitingNode,   "c", 1, (2, 0)),
              }.ToNodeVisitArray()
            },
          }
        },

        new TreeTestDefinition
        {
          TreeString = "a(b(d,e,f),c)",
          TestScenarios = new List<TestScenario>
          {
            // Traverse all
            new TestScenario
            {
              NodeTraversalStrategiesSelector = nc => NodeTraversalStrategies.TraverseAll,
              TreenumerableMap = treenumerable => treenumerable.Where(_ => true),
              Description = "Where all, traverse all",
              ExpectedBreadthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0)),
                (TreenumeratorMode.VisitingNode,   "a", 1, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "b", 0, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "a", 2, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (1, 1)),
                (TreenumeratorMode.VisitingNode,   "a", 3, (0, 0)),
                (TreenumeratorMode.VisitingNode,   "b", 1, (0, 1)),
                (TreenumeratorMode.SchedulingNode, "d", 0, (0, 2)),
                (TreenumeratorMode.VisitingNode,   "b", 2, (0, 1)),
                (TreenumeratorMode.SchedulingNode, "e", 0, (1, 2)),
                (TreenumeratorMode.VisitingNode,   "b", 3, (0, 1)),
                (TreenumeratorMode.SchedulingNode, "f", 0, (2, 2)),
                (TreenumeratorMode.VisitingNode,   "b", 4, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "c", 1, (1, 1)),
                (TreenumeratorMode.VisitingNode,   "d", 1, (0, 2)),
                (TreenumeratorMode.VisitingNode,   "e", 1, (1, 2)),
                (TreenumeratorMode.VisitingNode,   "f", 1, (2, 2)),
              }.ToNodeVisitArray(),
              ExpectedDepthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0)),
                (TreenumeratorMode.VisitingNode,   "a", 1, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "b", 0, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "b", 1, (0, 1)),
                (TreenumeratorMode.SchedulingNode, "d", 0, (0, 2)),
                (TreenumeratorMode.VisitingNode,   "d", 1, (0, 2)),
                (TreenumeratorMode.VisitingNode,   "b", 2, (0, 1)),
                (TreenumeratorMode.SchedulingNode, "e", 0, (1, 2)),
                (TreenumeratorMode.VisitingNode,   "e", 1, (1, 2)),
                (TreenumeratorMode.VisitingNode,   "b", 3, (0, 1)),
                (TreenumeratorMode.SchedulingNode, "f", 0, (2, 2)),
                (TreenumeratorMode.VisitingNode,   "f", 1, (2, 2)),
                (TreenumeratorMode.VisitingNode,   "b", 4, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "a", 2, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (1, 1)),
                (TreenumeratorMode.VisitingNode,   "c", 1, (1, 1)),
                (TreenumeratorMode.VisitingNode,   "a", 3, (0, 0)),
              }.ToNodeVisitArray()
            },
            new TestScenario
            {
              NodeTraversalStrategiesSelector = nc => NodeTraversalStrategies.TraverseAll,
              TreenumerableMap = treenumerable => treenumerable.Where(nc => nc.Node != "d" && nc.Node != "e"),
              Description = "Where not node d and e",
              ExpectedBreadthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0)),
                (TreenumeratorMode.VisitingNode,   "a", 1, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "b", 0, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "a", 2, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (1, 1)),
                (TreenumeratorMode.VisitingNode,   "a", 3, (0, 0)),
                (TreenumeratorMode.VisitingNode,   "b", 1, (0, 1)),
                (TreenumeratorMode.SchedulingNode, "f", 0, (0, 2)),
                (TreenumeratorMode.VisitingNode,   "b", 2, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "c", 1, (1, 1)),
                (TreenumeratorMode.VisitingNode,   "f", 1, (0, 2)),
              }.ToNodeVisitArray(),
              ExpectedDepthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0)),
                (TreenumeratorMode.VisitingNode,   "a", 1, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "b", 0, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "b", 1, (0, 1)),
                (TreenumeratorMode.SchedulingNode, "f", 0, (0, 2)),
                (TreenumeratorMode.VisitingNode,   "f", 1, (0, 2)),
                (TreenumeratorMode.VisitingNode,   "b", 2, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "a", 2, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (1, 1)),
                (TreenumeratorMode.VisitingNode,   "c", 1, (1, 1)),
                (TreenumeratorMode.VisitingNode,   "a", 3, (0, 0)),
              }.ToNodeVisitArray()
            },

            // Skip node
            new TestScenario
            {
              NodeTraversalStrategiesSelector =
                nc =>
                  nc.Node == "b"
                  ? NodeTraversalStrategies.SkipNode
                  :NodeTraversalStrategies.TraverseAll,
              TreenumerableMap = treenumerable => treenumerable.Where(nc => nc.Node != "f" && nc.Node != "c"),
              Description = "Where not nodes f and c, skip node b",
              ExpectedBreadthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0)),
                (TreenumeratorMode.VisitingNode,   "a", 1, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "b", 0, (0, 1)),
                (TreenumeratorMode.SchedulingNode, "d", 0, (0, 2)),
                (TreenumeratorMode.VisitingNode,   "a", 2, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "e", 0, (1, 2)),
                (TreenumeratorMode.VisitingNode,   "a", 3, (0, 0)),
                (TreenumeratorMode.VisitingNode,   "d", 1, (0, 2)),
                (TreenumeratorMode.VisitingNode,   "e", 1, (1, 2)),
              }.ToNodeVisitArray(),
              ExpectedDepthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0)),
                (TreenumeratorMode.VisitingNode,   "a", 1, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "b", 0, (0, 1)),
                (TreenumeratorMode.SchedulingNode, "d", 0, (0, 2)),
                (TreenumeratorMode.VisitingNode,   "d", 1, (0, 2)),
                (TreenumeratorMode.VisitingNode,   "a", 2, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "e", 0, (1, 2)),
                (TreenumeratorMode.VisitingNode,   "e", 1, (1, 2)),
                (TreenumeratorMode.VisitingNode,   "a", 3, (0, 0)),
              }.ToNodeVisitArray()
            },

            // Skip siblings
            new TestScenario
            {
              NodeTraversalStrategiesSelector =
                nc =>
                  nc.Node == "a"
                  ? NodeTraversalStrategies.SkipSiblings
                  : NodeTraversalStrategies.TraverseAll,
              TreenumerableMap = treenumerable => treenumerable.Where(nc => nc.Node != "d" && nc.Node != "e"),
              Description = "Where not node d and e, skip node a siblings",
              ExpectedBreadthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0)),
                (TreenumeratorMode.VisitingNode,   "a", 1, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "b", 0, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "a", 2, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (1, 1)),
                (TreenumeratorMode.VisitingNode,   "a", 3, (0, 0)),
                (TreenumeratorMode.VisitingNode,   "b", 1, (0, 1)),
                (TreenumeratorMode.SchedulingNode, "f", 0, (0, 2)),
                (TreenumeratorMode.VisitingNode,   "b", 2, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "c", 1, (1, 1)),
                (TreenumeratorMode.VisitingNode,   "f", 1, (0, 2)),
              }.ToNodeVisitArray(),
              ExpectedDepthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0)),
                (TreenumeratorMode.VisitingNode,   "a", 1, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "b", 0, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "b", 1, (0, 1)),
                (TreenumeratorMode.SchedulingNode, "f", 0, (0, 2)),
                (TreenumeratorMode.VisitingNode,   "f", 1, (0, 2)),
                (TreenumeratorMode.VisitingNode,   "b", 2, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "a", 2, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (1, 1)),
                (TreenumeratorMode.VisitingNode,   "c", 1, (1, 1)),
                (TreenumeratorMode.VisitingNode,   "a", 3, (0, 0)),
              }.ToNodeVisitArray()
            },
            new TestScenario
            {
              NodeTraversalStrategiesSelector =
                nc =>
                  nc.Node == "b"
                  ? NodeTraversalStrategies.SkipSiblings
                  : NodeTraversalStrategies.TraverseAll,
              TreenumerableMap = treenumerable => treenumerable.Where(nc => nc.Node != "d" && nc.Node != "e"),
              Description = "Where not node d and e, skip node b siblings",
              ExpectedBreadthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0)),
                (TreenumeratorMode.VisitingNode,   "a", 1, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "b", 0, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "a", 2, (0, 0)),
                (TreenumeratorMode.VisitingNode,   "b", 1, (0, 1)),
                (TreenumeratorMode.SchedulingNode, "f", 0, (0, 2)),
                (TreenumeratorMode.VisitingNode,   "b", 2, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "f", 1, (0, 2)),
              }.ToNodeVisitArray(),
              ExpectedDepthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0)),
                (TreenumeratorMode.VisitingNode,   "a", 1, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "b", 0, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "b", 1, (0, 1)),
                (TreenumeratorMode.SchedulingNode, "f", 0, (0, 2)),
                (TreenumeratorMode.VisitingNode,   "f", 1, (0, 2)),
                (TreenumeratorMode.VisitingNode,   "b", 2, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "a", 2, (0, 0)),
              }.ToNodeVisitArray()
            },
            new TestScenario
            {
              NodeTraversalStrategiesSelector =
                nc =>
                  nc.Node == "a"
                  ? NodeTraversalStrategies.SkipSiblings
                  : NodeTraversalStrategies.TraverseAll,
              TreenumerableMap = treenumerable => treenumerable.Where(nc => nc.Node != "d" && nc.Node != "e"),
              Description = "Where not node d and e, skip a siblings",
              ExpectedBreadthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0)),
                (TreenumeratorMode.VisitingNode,   "a", 1, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "b", 0, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "a", 2, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (1, 1)),
                (TreenumeratorMode.VisitingNode,   "a", 3, (0, 0)),
                (TreenumeratorMode.VisitingNode,   "b", 1, (0, 1)),
                (TreenumeratorMode.SchedulingNode, "f", 0, (0, 2)),
                (TreenumeratorMode.VisitingNode,   "b", 2, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "c", 1, (1, 1)),
                (TreenumeratorMode.VisitingNode,   "f", 1, (0, 2)),
              }.ToNodeVisitArray(),
              ExpectedDepthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0)),
                (TreenumeratorMode.VisitingNode,   "a", 1, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "b", 0, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "b", 1, (0, 1)),
                (TreenumeratorMode.SchedulingNode, "f", 0, (0, 2)),
                (TreenumeratorMode.VisitingNode,   "f", 1, (0, 2)),
                (TreenumeratorMode.VisitingNode,   "b", 2, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "a", 2, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (1, 1)),
                (TreenumeratorMode.VisitingNode,   "c", 1, (1, 1)),
                (TreenumeratorMode.VisitingNode,   "a", 3, (0, 0)),
              }.ToNodeVisitArray()
            },

            // Mixed
            new TestScenario
            {
              NodeTraversalStrategiesSelector =
                nc =>
                  nc.Node == "b"
                  ? NodeTraversalStrategies.SkipNodeAndSiblings
                  : NodeTraversalStrategies.TraverseAll,
              TreenumerableMap = treenumerable => treenumerable.Where(nc => nc.Node != "d" && nc.Node != "e"),
              Description = "Where not node d and e, skip node b node and siblings",
              ExpectedBreadthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0)),
                (TreenumeratorMode.VisitingNode,   "a", 1, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "b", 0, (0, 1)),
                (TreenumeratorMode.SchedulingNode, "f", 0, (0, 2)),
                (TreenumeratorMode.VisitingNode,   "a", 2, (0, 0)),
                (TreenumeratorMode.VisitingNode,   "f", 1, (0, 2)),
              }.ToNodeVisitArray(),
              ExpectedDepthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0)),
                (TreenumeratorMode.VisitingNode,   "a", 1, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "b", 0, (0, 1)),
                (TreenumeratorMode.SchedulingNode, "f", 0, (0, 2)),
                (TreenumeratorMode.VisitingNode,   "f", 1, (0, 2)),
                (TreenumeratorMode.VisitingNode,   "a", 2, (0, 0)),
              }.ToNodeVisitArray()
            },
          }
        },

        new TreeTestDefinition
        {
          TreeString = "a(c,d),b(e,f)",
          TestScenarios = new List<TestScenario>
          {
            new TestScenario
            {
              NodeTraversalStrategiesSelector = nc => nc.Node == "a" ? NodeTraversalStrategies.SkipNode : NodeTraversalStrategies.TraverseAll,
              TreenumerableMap = treenumerable => treenumerable.Where(_ => true),
              Description = "Where all nodes, skip node a",
              ExpectedBreadthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (0, 1)),
                (TreenumeratorMode.SchedulingNode, "d", 0, (1, 1)),
                (TreenumeratorMode.SchedulingNode, "b", 0, (1, 0)),
                (TreenumeratorMode.VisitingNode,   "c", 1, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "d", 1, (1, 1)),
                (TreenumeratorMode.VisitingNode,   "b", 1, (1, 0)),
                (TreenumeratorMode.SchedulingNode, "e", 0, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "b", 2, (1, 0)),
                (TreenumeratorMode.SchedulingNode, "f", 0, (1, 1)),
                (TreenumeratorMode.VisitingNode,   "b", 3, (1, 0)),
                (TreenumeratorMode.VisitingNode,   "e", 1, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "f", 1, (1, 1)),
              }.ToNodeVisitArray(),
              ExpectedDepthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "c", 1, (0, 1)),
                (TreenumeratorMode.SchedulingNode, "d", 0, (1, 1)),
                (TreenumeratorMode.VisitingNode,   "d", 1, (1, 1)),
                (TreenumeratorMode.SchedulingNode, "b", 0, (1, 0)),
                (TreenumeratorMode.VisitingNode,   "b", 1, (1, 0)),
                (TreenumeratorMode.SchedulingNode, "e", 0, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "e", 1, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "b", 2, (1, 0)),
                (TreenumeratorMode.SchedulingNode, "f", 0, (1, 1)),
                (TreenumeratorMode.VisitingNode,   "f", 1, (1, 1)),
                (TreenumeratorMode.VisitingNode,   "b", 3, (1, 0)),
              }.ToNodeVisitArray()
            },
          }
        },

        new TreeTestDefinition
        {
          TreeString = "a,b,c",
          TestScenarios = new List<TestScenario>
          {
            // Traverse all
            new TestScenario
            {
              NodeTraversalStrategiesSelector = nc => NodeTraversalStrategies.TraverseAll,
              TreenumerableMap = treenumerable => treenumerable.Where(_ => true),
              Description = "Where all, traverse all",
              ExpectedBreadthFirstResults = new[] {
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
            new TestScenario
            {
              NodeTraversalStrategiesSelector = nc => NodeTraversalStrategies.TraverseAll,
              TreenumerableMap = treenumerable => treenumerable.Where(_ => false),
              Description = "Where none, traverse all",
              ExpectedBreadthFirstResults = Array.Empty<NodeVisit<string>>(),
              ExpectedDepthFirstResults = Array.Empty<NodeVisit<string>>()
            },
            new TestScenario
            {
              NodeTraversalStrategiesSelector = nc => NodeTraversalStrategies.TraverseAll,
              TreenumerableMap = treenumerable => treenumerable.Where(nc => nc.Position.SiblingIndex != 0),
              Description = "Where not sibling 0, traverse all",
              ExpectedBreadthFirstResults = new[] {
                (TreenumeratorMode.SchedulingNode, "b", 0, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (1, 0)),
                (TreenumeratorMode.VisitingNode,   "b", 1, (0, 0)),
                (TreenumeratorMode.VisitingNode,   "c", 1, (1, 0)),
              }.ToNodeVisitArray(),
              ExpectedDepthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "b", 0, (0, 0)),
                (TreenumeratorMode.VisitingNode,   "b", 1, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (1, 0)),
                (TreenumeratorMode.VisitingNode,   "c", 1, (1, 0)),
              }.ToNodeVisitArray()
            },
            new TestScenario
            {
              NodeTraversalStrategiesSelector = nc => NodeTraversalStrategies.TraverseAll,
              TreenumerableMap = treenumerable => treenumerable.Where(nc => nc.Position.SiblingIndex != 1),
              Description = "Where not sibling 1, traverse all",
              ExpectedBreadthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (1, 0)),
                (TreenumeratorMode.VisitingNode,   "a", 1, (0, 0)),
                (TreenumeratorMode.VisitingNode,   "c", 1, (1, 0)),
              }.ToNodeVisitArray(),
              ExpectedDepthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0)),
                (TreenumeratorMode.VisitingNode,   "a", 1, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (1, 0)),
                (TreenumeratorMode.VisitingNode,   "c", 1, (1, 0)),
              }.ToNodeVisitArray()
            },

            // Skip subtree
            new TestScenario
            {
              NodeTraversalStrategiesSelector = nc => NodeTraversalStrategies.SkipNodeAndDescendants,
              TreenumerableMap = treenumerable => treenumerable.Where(_ => false),
              Description = "Where none, Skip all subtrees",
              ExpectedBreadthFirstResults = Array.Empty<NodeVisit<string>>(),
              ExpectedDepthFirstResults = Array.Empty<NodeVisit<string>>()
            },
            new TestScenario
            {
              NodeTraversalStrategiesSelector = nc => nc.Position.SiblingIndex == 0 ? NodeTraversalStrategies.SkipNodeAndDescendants : NodeTraversalStrategies.TraverseAll,
              TreenumerableMap = treenumerable => treenumerable.Where(nc => nc.Position.SiblingIndex != 1),
              Description = "Where not sibling 1, skip sibling 0 subtree",
              ExpectedBreadthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (1, 0)),
                (TreenumeratorMode.VisitingNode,   "c", 1, (1, 0)),
              }.ToNodeVisitArray(),
              ExpectedDepthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (1, 0)),
                (TreenumeratorMode.VisitingNode,   "c", 1, (1, 0)),
              }.ToNodeVisitArray()
            },
            // Skip node
            new TestScenario
            {
              NodeTraversalStrategiesSelector = nc => nc.Node == "a" ? NodeTraversalStrategies.SkipNode : NodeTraversalStrategies.TraverseAll,
              TreenumerableMap = treenumerable => treenumerable.Where(nc => nc.Node != "b"),
              Description = "Where not sibling 1, skip sibling 0",
              ExpectedBreadthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (1, 0)),
                (TreenumeratorMode.VisitingNode,   "c", 1, (1, 0)),
              }.ToNodeVisitArray(),
              ExpectedDepthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (1, 0)),
                (TreenumeratorMode.VisitingNode,   "c", 1, (1, 0)),
              }.ToNodeVisitArray()
            },
            new TestScenario
            {
              NodeTraversalStrategiesSelector = nc => nc.Node == "a" ? NodeTraversalStrategies.SkipNode : NodeTraversalStrategies.TraverseAll,
              TreenumerableMap = treenumerable => treenumerable.Where(nc => nc.Position.SiblingIndex == 0),
              Description = "Where not siblings 1 and 2, skip sibling 0 node",
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
              NodeTraversalStrategiesSelector = nc => nc.Node == "b" ? NodeTraversalStrategies.SkipNode : NodeTraversalStrategies.TraverseAll,
              TreenumerableMap = treenumerable => treenumerable.Where(nc => nc.Node != "a"),
              Description = "Where not sibling 0, skip sibling 1",
              ExpectedBreadthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "b", 0, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (1, 0)),
                (TreenumeratorMode.VisitingNode,   "c", 1, (1, 0)),
              }.ToNodeVisitArray(),
              ExpectedDepthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "b", 0, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (1, 0)),
                (TreenumeratorMode.VisitingNode,   "c", 1, (1, 0)),
              }.ToNodeVisitArray()
            },
            new TestScenario
            {
              NodeTraversalStrategiesSelector = nc => NodeTraversalStrategies.SkipNode,
              TreenumerableMap = treenumerable => treenumerable.Where(nc => true),
              Description = "Where all, skip all nodes",
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
    public void Where_BreadthFirst(
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
        testScenario.NodeTraversalStrategiesSelector,
        testScenario.ExpectedBreadthFirstResults,
        TreeTraversalStrategy.BreadthFirst);
    }

    [TestMethod]
    [DynamicData(nameof(GetTestData), DynamicDataSourceType.Method, DynamicDataDisplayName = nameof(GetTestDisplayName))]
    public void Where_DepthFirst(
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
        testScenario.NodeTraversalStrategiesSelector,
        testScenario.ExpectedDepthFirstResults,
        TreeTraversalStrategy.DepthFirst);
    }
  }
}

