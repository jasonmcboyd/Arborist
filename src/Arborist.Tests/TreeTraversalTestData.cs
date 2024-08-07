﻿using Arborist.Core;
using Arborist.TestUtils;
using System;
using System.Collections.Generic;

namespace Arborist.Tests
{
  public static class TreeTraversalTestData
  {
    public static TreeTestDefinition[] TestTrees =>
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
              NodeTraversalStrategySelector = visit => NodeTraversalStrategy.TraverseSubtree,
              Description = "Traverse all",
              ExpectedBreadthFirstResults = Array.Empty<NodeVisit<string>>(),
              ExpectedDepthFirstResults = Array.Empty<NodeVisit<string>>()
            },
          }
        },

        // Five-level unary tree.
        new TreeTestDefinition
        {
          TreeString = "a(b(c(d(e))))",
          TestScenarios = new List<TestScenario>()
          {
            // No skipping
            new TestScenario
            {
              NodeTraversalStrategySelector = visit => NodeTraversalStrategy.TraverseSubtree,
              Description = "Traverse all",
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
                (TreenumeratorMode.SchedulingNode, "d", 0, (0, 3)),
                (TreenumeratorMode.VisitingNode,   "c", 2, (0, 2)),
                (TreenumeratorMode.VisitingNode,   "d", 1, (0, 3)),
                (TreenumeratorMode.SchedulingNode, "e", 0, (0, 4)),
                (TreenumeratorMode.VisitingNode,   "d", 2, (0, 3)),
                (TreenumeratorMode.VisitingNode,   "e", 1, (0, 4)),
              }.ToNodeVisitArray(),
              ExpectedDepthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0)),
                (TreenumeratorMode.VisitingNode,   "a", 1, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "b", 0, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "b", 1, (0, 1)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (0, 2)),
                (TreenumeratorMode.VisitingNode,   "c", 1, (0, 2)),
                (TreenumeratorMode.SchedulingNode, "d", 0, (0, 3)),
                (TreenumeratorMode.VisitingNode,   "d", 1, (0, 3)),
                (TreenumeratorMode.SchedulingNode, "e", 0, (0, 4)),
                (TreenumeratorMode.VisitingNode,   "e", 1, (0, 4)),
                (TreenumeratorMode.VisitingNode,   "d", 2, (0, 3)),
                (TreenumeratorMode.VisitingNode,   "c", 2, (0, 2)),
                (TreenumeratorMode.VisitingNode,   "b", 2, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "a", 2, (0, 0)),
              }.ToNodeVisitArray()
            },
            // Skip nodes
            new TestScenario
            {
              NodeTraversalStrategySelector = visit => NodeTraversalStrategy.SkipNode,
              Description = "Skip all nodes",
              ExpectedBreadthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "b", 0, (0, 1)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (0, 2)),
                (TreenumeratorMode.SchedulingNode, "d", 0, (0, 3)),
                (TreenumeratorMode.SchedulingNode, "e", 0, (0, 4)),
              }.ToNodeVisitArray(),
              ExpectedDepthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "b", 0, (0, 1)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (0, 2)),
                (TreenumeratorMode.SchedulingNode, "d", 0, (0, 3)),
                (TreenumeratorMode.SchedulingNode, "e", 0, (0, 4)),
              }.ToNodeVisitArray()
            },
            new TestScenario
            {
              NodeTraversalStrategySelector = visit => visit.Position.Depth > 0 && visit.Position.Depth < 4 ? NodeTraversalStrategy.SkipNode : NodeTraversalStrategy.TraverseSubtree,
              Description = "Skip levels 1, 2, 3 node",
              ExpectedBreadthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0)),
                (TreenumeratorMode.VisitingNode,   "a", 1, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "b", 0, (0, 1)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (0, 2)),
                (TreenumeratorMode.SchedulingNode, "d", 0, (0, 3)),
                (TreenumeratorMode.SchedulingNode, "e", 0, (0, 4)),
                (TreenumeratorMode.VisitingNode,   "a", 2, (0, 0)),
                (TreenumeratorMode.VisitingNode,   "e", 1, (0, 4)),
              }.ToNodeVisitArray(),
              ExpectedDepthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0)),
                (TreenumeratorMode.VisitingNode,   "a", 1, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "b", 0, (0, 1)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (0, 2)),
                (TreenumeratorMode.SchedulingNode, "d", 0, (0, 3)),
                (TreenumeratorMode.SchedulingNode, "e", 0, (0, 4)),
                (TreenumeratorMode.VisitingNode,   "e", 1, (0, 4)),
                (TreenumeratorMode.VisitingNode,   "a", 2, (0, 0)),
              }.ToNodeVisitArray()
            },
          }
        },

        // Three-level unary tree.
        new TreeTestDefinition
        {
          TreeString = "a(b(c))",
          TestScenarios = new List<TestScenario>()
          {
            // No skipping
            new TestScenario
            {
              NodeTraversalStrategySelector = visit => NodeTraversalStrategy.TraverseSubtree,
              Description = "Traverse all",
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
              }.ToNodeVisitArray()
            },
            // Skip nodes
            new TestScenario
            {
              NodeTraversalStrategySelector = visit => NodeTraversalStrategy.SkipNode,
              Description = "Skip all nodes",
              ExpectedBreadthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "b", 0, (0, 1)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (0, 2)),
              }.ToNodeVisitArray(),
              ExpectedDepthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "b", 0, (0, 1)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (0, 2)),
              }.ToNodeVisitArray()
            },
            new TestScenario
            {
              NodeTraversalStrategySelector = visit => visit.Position.Depth == 0 ? NodeTraversalStrategy.SkipNode : NodeTraversalStrategy.TraverseSubtree,
              Description = "Skip level 0 node",
              ExpectedBreadthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "b", 0, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "b", 1, (0, 1)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (0, 2)),
                (TreenumeratorMode.VisitingNode,   "b", 2, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "c", 1, (0, 2)),
              }.ToNodeVisitArray(),
              ExpectedDepthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "b", 0, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "b", 1, (0, 1)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (0, 2)),
                (TreenumeratorMode.VisitingNode,   "c", 1, (0, 2)),
                (TreenumeratorMode.VisitingNode,   "b", 2, (0, 1)),
              }.ToNodeVisitArray()
            },
            new TestScenario
            {
              NodeTraversalStrategySelector = visit => visit.Position.Depth == 1 ? NodeTraversalStrategy.SkipNode : NodeTraversalStrategy.TraverseSubtree,
              Description = "Skip level 1 node",
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
              NodeTraversalStrategySelector = visit => visit.Position.Depth == 2 ? NodeTraversalStrategy.SkipNode : NodeTraversalStrategy.TraverseSubtree,
              Description = "Skip level 2 node",
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
              NodeTraversalStrategySelector = visit => visit.Position.Depth == 0 || visit.Position.Depth == 2 ? NodeTraversalStrategy.SkipNode : NodeTraversalStrategy.TraverseSubtree,
              Description = "Skip level 0 and level 2 nodes",
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
            // Skip subtree
            new TestScenario
            {
              NodeTraversalStrategySelector = visit => visit.Position.Depth == 0 ? NodeTraversalStrategy.SkipSubtree : NodeTraversalStrategy.TraverseSubtree,
              Description = "Skip level 0 subtree",
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
              NodeTraversalStrategySelector = visit => visit.Position.Depth == 1 ? NodeTraversalStrategy.SkipSubtree : NodeTraversalStrategy.TraverseSubtree,
              Description = "Skip level 1 subtree",
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
            new TestScenario
            {
              NodeTraversalStrategySelector = visit => visit.Position.Depth == 2 ? NodeTraversalStrategy.SkipSubtree : NodeTraversalStrategy.TraverseSubtree,
              Description = "Skip level 2 subtree",
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
            // Skip descendant subtrees
            new TestScenario
            {
              NodeTraversalStrategySelector = visit => visit.Position.Depth == 0 ? NodeTraversalStrategy.SkipDescendants : NodeTraversalStrategy.TraverseSubtree,
              Description = "Skip level 0 descedant subtrees",
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
              NodeTraversalStrategySelector = visit => visit.Position.Depth == 1 ? NodeTraversalStrategy.SkipDescendants : NodeTraversalStrategy.TraverseSubtree,
              Description = "Skip level 1 descedant subtrees",
              ExpectedBreadthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0)),
                (TreenumeratorMode.VisitingNode,   "a", 1, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "b", 0, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "a", 2, (0, 0)),
                (TreenumeratorMode.VisitingNode,   "b", 1, (0, 1)),
              }.ToNodeVisitArray(),
              ExpectedDepthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0)),
                (TreenumeratorMode.VisitingNode,   "a", 1, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "b", 0, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "b", 1, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "a", 2, (0, 0)),
              }.ToNodeVisitArray()
            },
            new TestScenario
            {
              NodeTraversalStrategySelector = visit => visit.Position.Depth == 2 ? NodeTraversalStrategy.SkipDescendants : NodeTraversalStrategy.TraverseSubtree,
              Description = "Skip level 2 descedant subtrees",
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
              }.ToNodeVisitArray()
            },
          }
        },

        new TreeTestDefinition
        {
          TreeString = "a(b(e),c(f),d(g))",
          TestScenarios = new List<TestScenario>
          {
            // No skipping
            new TestScenario
            {
              NodeTraversalStrategySelector = visit => NodeTraversalStrategy.TraverseSubtree,
              Description = "Traverse all",
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
                (TreenumeratorMode.VisitingNode,   "b", 1, (0, 1)),
                (TreenumeratorMode.SchedulingNode, "e", 0, (0, 2)),
                (TreenumeratorMode.VisitingNode,   "b", 2, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "c", 1, (1, 1)),
                (TreenumeratorMode.SchedulingNode, "f", 0, (0, 2)),
                (TreenumeratorMode.VisitingNode,   "c", 2, (1, 1)),
                (TreenumeratorMode.VisitingNode,   "d", 1, (2, 1)),
                (TreenumeratorMode.SchedulingNode, "g", 0, (0, 2)),
                (TreenumeratorMode.VisitingNode,   "d", 2, (2, 1)),
                (TreenumeratorMode.VisitingNode,   "e", 1, (0, 2)),
                (TreenumeratorMode.VisitingNode,   "f", 1, (0, 2)),
                (TreenumeratorMode.VisitingNode,   "g", 1, (0, 2)),
              }.ToNodeVisitArray(),
              ExpectedDepthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0)),
                (TreenumeratorMode.VisitingNode,   "a", 1, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "b", 0, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "b", 1, (0, 1)),
                (TreenumeratorMode.SchedulingNode, "e", 0, (0, 2)),
                (TreenumeratorMode.VisitingNode,   "e", 1, (0, 2)),
                (TreenumeratorMode.VisitingNode,   "b", 2, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "a", 2, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (1, 1)),
                (TreenumeratorMode.VisitingNode,   "c", 1, (1, 1)),
                (TreenumeratorMode.SchedulingNode, "f", 0, (0, 2)),
                (TreenumeratorMode.VisitingNode,   "f", 1, (0, 2)),
                (TreenumeratorMode.VisitingNode,   "c", 2, (1, 1)),
                (TreenumeratorMode.VisitingNode,   "a", 3, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "d", 0, (2, 1)),
                (TreenumeratorMode.VisitingNode,   "d", 1, (2, 1)),
                (TreenumeratorMode.SchedulingNode, "g", 0, (0, 2)),
                (TreenumeratorMode.VisitingNode,   "g", 1, (0, 2)),
                (TreenumeratorMode.VisitingNode,   "d", 2, (2, 1)),
                (TreenumeratorMode.VisitingNode,   "a", 4, (0, 0)),
              }.ToNodeVisitArray()
            },

            // Skip node
            new TestScenario
            {
              NodeTraversalStrategySelector = visit =>
                visit.Position.Depth == 0
                ? NodeTraversalStrategy.SkipNode
                : visit.Position.Depth == 1  
                ? NodeTraversalStrategy.SkipSubtree
                : NodeTraversalStrategy.TraverseSubtree,
              Description = "Skip level 1 subtrees, skip level 0 nodes",
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
              NodeTraversalStrategySelector = visit =>
                visit.Position.Depth == 0
                ? NodeTraversalStrategy.SkipNode
                : visit.Position.Depth == 2  
                ? NodeTraversalStrategy.SkipSubtree
                : NodeTraversalStrategy.TraverseSubtree,
              Description = "Skip level 2 subtrees, skip level 0 nodes",
              ExpectedBreadthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "b", 0, (0, 1)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (1, 1)),
                (TreenumeratorMode.SchedulingNode, "d", 0, (2, 1)),
                (TreenumeratorMode.VisitingNode,   "b", 1, (0, 1)),
                (TreenumeratorMode.SchedulingNode, "e", 0, (0, 2)),
                (TreenumeratorMode.VisitingNode,   "b", 2, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "c", 1, (1, 1)),
                (TreenumeratorMode.SchedulingNode, "f", 0, (0, 2)),
                (TreenumeratorMode.VisitingNode,   "c", 2, (1, 1)),
                (TreenumeratorMode.VisitingNode,   "d", 1, (2, 1)),
                (TreenumeratorMode.SchedulingNode, "g", 0, (0, 2)),
                (TreenumeratorMode.VisitingNode,   "d", 2, (2, 1)),
              }.ToNodeVisitArray(),
              ExpectedDepthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "b", 0, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "b", 1, (0, 1)),
                (TreenumeratorMode.SchedulingNode, "e", 0, (0, 2)),
                (TreenumeratorMode.VisitingNode,   "b", 2, (0, 1)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (1, 1)),
                (TreenumeratorMode.VisitingNode,   "c", 1, (1, 1)),
                (TreenumeratorMode.SchedulingNode, "f", 0, (0, 2)),
                (TreenumeratorMode.VisitingNode,   "c", 2, (1, 1)),
                (TreenumeratorMode.SchedulingNode, "d", 0, (2, 1)),
                (TreenumeratorMode.VisitingNode,   "d", 1, (2, 1)),
                (TreenumeratorMode.SchedulingNode, "g", 0, (0, 2)),
                (TreenumeratorMode.VisitingNode,   "d", 2, (2, 1)),
              }.ToNodeVisitArray()
            },
            new TestScenario
            {
              NodeTraversalStrategySelector = visit => visit.Position.Depth > 0 ? NodeTraversalStrategy.SkipNode : NodeTraversalStrategy.TraverseSubtree,
              Description = "Skip level 1 & 2 nodes",
              ExpectedBreadthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0)),
                (TreenumeratorMode.VisitingNode,   "a", 1, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "b", 0, (0, 1)),
                (TreenumeratorMode.SchedulingNode, "e", 0, (0, 2)),
                (TreenumeratorMode.VisitingNode,   "a", 2, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (1, 1)),
                (TreenumeratorMode.SchedulingNode, "f", 0, (0, 2)),
                (TreenumeratorMode.VisitingNode,   "a", 3, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "d", 0, (2, 1)),
                (TreenumeratorMode.SchedulingNode, "g", 0, (0, 2)),
                (TreenumeratorMode.VisitingNode,   "a", 4, (0, 0)),
              }.ToNodeVisitArray(),
              ExpectedDepthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0)),
                (TreenumeratorMode.VisitingNode,   "a", 1, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "b", 0, (0, 1)),
                (TreenumeratorMode.SchedulingNode, "e", 0, (0, 2)),
                (TreenumeratorMode.VisitingNode,   "a", 2, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (1, 1)),
                (TreenumeratorMode.SchedulingNode, "f", 0, (0, 2)),
                (TreenumeratorMode.VisitingNode,   "a", 3, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "d", 0, (2, 1)),
                (TreenumeratorMode.SchedulingNode, "g", 0, (0, 2)),
                (TreenumeratorMode.VisitingNode,   "a", 4, (0, 0)),
              }.ToNodeVisitArray()
            },
            new TestScenario
            {
              NodeTraversalStrategySelector = visit =>
                visit.Position.Depth == 1
                ? NodeTraversalStrategy.SkipNode
                : visit.Position.Depth == 2
                ? NodeTraversalStrategy.SkipSubtree
                : NodeTraversalStrategy.TraverseSubtree,
              Description = "Skip level 2 subtrees, skip level 1 nodes",
              ExpectedBreadthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0)),
                (TreenumeratorMode.VisitingNode,   "a", 1, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "b", 0, (0, 1)),
                (TreenumeratorMode.SchedulingNode, "e", 0, (0, 2)),
                (TreenumeratorMode.VisitingNode,   "a", 2, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (1, 1)),
                (TreenumeratorMode.SchedulingNode, "f", 0, (0, 2)),
                (TreenumeratorMode.VisitingNode,   "a", 3, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "d", 0, (2, 1)),
                (TreenumeratorMode.SchedulingNode, "g", 0, (0, 2)),
                (TreenumeratorMode.VisitingNode,   "a", 4, (0, 0)),
              }.ToNodeVisitArray(),
              ExpectedDepthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0)),
                (TreenumeratorMode.VisitingNode,   "a", 1, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "b", 0, (0, 1)),
                (TreenumeratorMode.SchedulingNode, "e", 0, (0, 2)),
                (TreenumeratorMode.VisitingNode,   "a", 2, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (1, 1)),
                (TreenumeratorMode.SchedulingNode, "f", 0, (0, 2)),
                (TreenumeratorMode.VisitingNode,   "a", 3, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "d", 0, (2, 1)),
                (TreenumeratorMode.SchedulingNode, "g", 0, (0, 2)),
                (TreenumeratorMode.VisitingNode,   "a", 4, (0, 0)),
              }.ToNodeVisitArray()
            },
            new TestScenario
            {
              NodeTraversalStrategySelector = visit =>
                visit.Position.Depth == 1
                ? NodeTraversalStrategy.SkipNode
                : visit.Position.Depth == 2 && visit.Node != "g"
                ? NodeTraversalStrategy.SkipSubtree
                : NodeTraversalStrategy.TraverseSubtree,
              Description = "Skip level 2, siblings 0 & 1 subtrees, skip level 1 nodes",
              ExpectedBreadthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0)),
                (TreenumeratorMode.VisitingNode,   "a", 1, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "b", 0, (0, 1)),
                (TreenumeratorMode.SchedulingNode, "e", 0, (0, 2)),
                (TreenumeratorMode.VisitingNode,   "a", 2, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (1, 1)),
                (TreenumeratorMode.SchedulingNode, "f", 0, (0, 2)),
                (TreenumeratorMode.VisitingNode,   "a", 3, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "d", 0, (2, 1)),
                (TreenumeratorMode.SchedulingNode, "g", 0, (0, 2)),
                (TreenumeratorMode.VisitingNode,   "a", 4, (0, 0)),
                (TreenumeratorMode.VisitingNode,   "g", 1, (0, 2)),
              }.ToNodeVisitArray(),
              ExpectedDepthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0)),
                (TreenumeratorMode.VisitingNode,   "a", 1, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "b", 0, (0, 1)),
                (TreenumeratorMode.SchedulingNode, "e", 0, (0, 2)),
                (TreenumeratorMode.VisitingNode,   "a", 2, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (1, 1)),
                (TreenumeratorMode.SchedulingNode, "f", 0, (0, 2)),
                (TreenumeratorMode.VisitingNode,   "a", 3, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "d", 0, (2, 1)),
                (TreenumeratorMode.SchedulingNode, "g", 0, (0, 2)),
                (TreenumeratorMode.VisitingNode,   "g", 1, (0, 2)),
                (TreenumeratorMode.VisitingNode,   "a", 4, (0, 0)),
              }.ToNodeVisitArray()
            },
          }
        },

        // Three level skinny tree.
        new TreeTestDefinition
        {
          TreeString = "a(b(c,d))",
          TestScenarios = new List<TestScenario>
          {
            // Traverse all
            new TestScenario
            {
              NodeTraversalStrategySelector = visit => NodeTraversalStrategy.TraverseSubtree,
              Description = "Traverse all",
              ExpectedBreadthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0)),
                (TreenumeratorMode.VisitingNode,   "a", 1, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "b", 0, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "a", 2, (0, 0)),
                (TreenumeratorMode.VisitingNode,   "b", 1, (0, 1)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (0, 2)),
                (TreenumeratorMode.VisitingNode,   "b", 2, (0, 1)),
                (TreenumeratorMode.SchedulingNode, "d", 0, (1, 2)),
                (TreenumeratorMode.VisitingNode,   "b", 3, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "c", 1, (0, 2)),
                (TreenumeratorMode.VisitingNode,   "d", 1, (1, 2)),
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
                (TreenumeratorMode.SchedulingNode, "d", 0, (1, 2)),
                (TreenumeratorMode.VisitingNode,   "d", 1, (1, 2)),
                (TreenumeratorMode.VisitingNode,   "b", 3, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "a", 2, (0, 0)),
              }.ToNodeVisitArray()
            },
            // Skip nodes
            new TestScenario
            {
              NodeTraversalStrategySelector = visit => visit.Node == "b" ? NodeTraversalStrategy.SkipNode : NodeTraversalStrategy.TraverseSubtree,
              Description = "Skip level 1 node",
              ExpectedBreadthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0)),
                (TreenumeratorMode.VisitingNode,   "a", 1, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "b", 0, (0, 1)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (0, 2)),
                (TreenumeratorMode.VisitingNode,   "a", 2, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "d", 0, (1, 2)),
                (TreenumeratorMode.VisitingNode,   "a", 3, (0, 0)),
                (TreenumeratorMode.VisitingNode,   "c", 1, (0, 2)),
                (TreenumeratorMode.VisitingNode,   "d", 1, (1, 2)),
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
              }.ToNodeVisitArray()
            },
            new TestScenario
            {
              NodeTraversalStrategySelector = visit => visit.Position.Depth == 2 ? NodeTraversalStrategy.SkipNode : NodeTraversalStrategy.TraverseSubtree,
              Description = "Skip level 2 nodes",
              ExpectedBreadthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0)),
                (TreenumeratorMode.VisitingNode,   "a", 1, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "b", 0, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "a", 2, (0, 0)),
                (TreenumeratorMode.VisitingNode,   "b", 1, (0, 1)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (0, 2)),
                (TreenumeratorMode.VisitingNode,   "b", 2, (0, 1)),
                (TreenumeratorMode.SchedulingNode, "d", 0, (1, 2)),
                (TreenumeratorMode.VisitingNode,   "b", 3, (0, 1)),
              }.ToNodeVisitArray(),
              ExpectedDepthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0)),
                (TreenumeratorMode.VisitingNode,   "a", 1, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "b", 0, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "b", 1, (0, 1)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (0, 2)),
                (TreenumeratorMode.VisitingNode,   "b", 2, (0, 1)),
                (TreenumeratorMode.SchedulingNode, "d", 0, (1, 2)),
                (TreenumeratorMode.VisitingNode,   "b", 3, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "a", 2, (0, 0)),
              }.ToNodeVisitArray()
            },
          }
        },

        // Three-level complete binary trees.
        new TreeTestDefinition
        {
          TreeString = "a(b(d,e),c(f,g))",
          TestScenarios = new List<TestScenario>
          {
            // No skipping
            new TestScenario
            {
              NodeTraversalStrategySelector = visit => NodeTraversalStrategy.TraverseSubtree,
              Description = "Traverse all",
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
                (TreenumeratorMode.VisitingNode,   "c", 1, (1, 1)),
                (TreenumeratorMode.SchedulingNode, "f", 0, (0, 2)),
                (TreenumeratorMode.VisitingNode,   "c", 2, (1, 1)),
                (TreenumeratorMode.SchedulingNode, "g", 0, (1, 2)),
                (TreenumeratorMode.VisitingNode,   "c", 3, (1, 1)),
                (TreenumeratorMode.VisitingNode,   "d", 1, (0, 2)),
                (TreenumeratorMode.VisitingNode,   "e", 1, (1, 2)),
                (TreenumeratorMode.VisitingNode,   "f", 1, (0, 2)),
                (TreenumeratorMode.VisitingNode,   "g", 1, (1, 2)),
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
                (TreenumeratorMode.VisitingNode,   "a", 2, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (1, 1)),
                (TreenumeratorMode.VisitingNode,   "c", 1, (1, 1)),
                (TreenumeratorMode.SchedulingNode, "f", 0, (0, 2)),
                (TreenumeratorMode.VisitingNode,   "f", 1, (0, 2)),
                (TreenumeratorMode.VisitingNode,   "c", 2, (1, 1)),
                (TreenumeratorMode.SchedulingNode, "g", 0, (1, 2)),
                (TreenumeratorMode.VisitingNode,   "g", 1, (1, 2)),
                (TreenumeratorMode.VisitingNode,   "c", 3, (1, 1)),
                (TreenumeratorMode.VisitingNode,   "a", 3, (0, 0)),
              }.ToNodeVisitArray()
            },
            // Skip nodes
            new TestScenario
            {
              NodeTraversalStrategySelector = visit => visit.Node == "c" ? NodeTraversalStrategy.SkipNode : NodeTraversalStrategy.TraverseSubtree,
              Description = "Skip level 1, sibling 1 node",
              ExpectedBreadthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0)),
                (TreenumeratorMode.VisitingNode,   "a", 1, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "b", 0, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "a", 2, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (1, 1)),
                (TreenumeratorMode.SchedulingNode, "f", 0, (0, 2)),
                (TreenumeratorMode.VisitingNode,   "a", 3, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "g", 0, (1, 2)),
                (TreenumeratorMode.VisitingNode,   "a", 4, (0, 0)),
                (TreenumeratorMode.VisitingNode,   "b", 1, (0, 1)),
                (TreenumeratorMode.SchedulingNode, "d", 0, (0, 2)),
                (TreenumeratorMode.VisitingNode,   "b", 2, (0, 1)),
                (TreenumeratorMode.SchedulingNode, "e", 0, (1, 2)),
                (TreenumeratorMode.VisitingNode,   "b", 3, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "f", 1, (0, 2)),
                (TreenumeratorMode.VisitingNode,   "g", 1, (1, 2)),
                (TreenumeratorMode.VisitingNode,   "d", 1, (0, 2)),
                (TreenumeratorMode.VisitingNode,   "e", 1, (1, 2)),
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
                (TreenumeratorMode.VisitingNode,   "a", 2, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (1, 1)),
                (TreenumeratorMode.SchedulingNode, "f", 0, (0, 2)),
                (TreenumeratorMode.VisitingNode,   "f", 1, (0, 2)),
                (TreenumeratorMode.VisitingNode,   "a", 3, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "g", 0, (1, 2)),
                (TreenumeratorMode.VisitingNode,   "g", 1, (1, 2)),
                (TreenumeratorMode.VisitingNode,   "a", 4, (0, 0)),
              }.ToNodeVisitArray()
            },
          }
        },

        // Five-level right-heavy binary tree.
        new TreeTestDefinition
        {
          TreeString = "a(b,c(d,e(f,g(h,i))))",
          TestScenarios = new List<TestScenario>
          {
            // No skipping
            new TestScenario
            {
              NodeTraversalStrategySelector = visit => NodeTraversalStrategy.TraverseSubtree,
              Description = "Traverse all",
              ExpectedBreadthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0)),
                (TreenumeratorMode.VisitingNode,   "a", 1, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "b", 0, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "a", 2, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (1, 1)),
                (TreenumeratorMode.VisitingNode,   "a", 3, (0, 0)),
                (TreenumeratorMode.VisitingNode,   "b", 1, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "c", 1, (1, 1)),
                (TreenumeratorMode.SchedulingNode, "d", 0, (0, 2)),
                (TreenumeratorMode.VisitingNode,   "c", 2, (1, 1)),
                (TreenumeratorMode.SchedulingNode, "e", 0, (1, 2)),
                (TreenumeratorMode.VisitingNode,   "c", 3, (1, 1)),
                (TreenumeratorMode.VisitingNode,   "d", 1, (0, 2)),
                (TreenumeratorMode.VisitingNode,   "e", 1, (1, 2)),
                (TreenumeratorMode.SchedulingNode, "f", 0, (0, 3)),
                (TreenumeratorMode.VisitingNode,   "e", 2, (1, 2)),
                (TreenumeratorMode.SchedulingNode, "g", 0, (1, 3)),
                (TreenumeratorMode.VisitingNode,   "e", 3, (1, 2)),
                (TreenumeratorMode.VisitingNode,   "f", 1, (0, 3)),
                (TreenumeratorMode.VisitingNode,   "g", 1, (1, 3)),
                (TreenumeratorMode.SchedulingNode, "h", 0, (0, 4)),
                (TreenumeratorMode.VisitingNode,   "g", 2, (1, 3)),
                (TreenumeratorMode.SchedulingNode, "i", 0, (1, 4)),
                (TreenumeratorMode.VisitingNode,   "g", 3, (1, 3)),
                (TreenumeratorMode.VisitingNode,   "h", 1, (0, 4)),
                (TreenumeratorMode.VisitingNode,   "i", 1, (1, 4)),
              }.ToNodeVisitArray(),
              ExpectedDepthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0)),
                (TreenumeratorMode.VisitingNode,   "a", 1, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "b", 0, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "b", 1, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "a", 2, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (1, 1)),
                (TreenumeratorMode.VisitingNode,   "c", 1, (1, 1)),
                (TreenumeratorMode.SchedulingNode, "d", 0, (0, 2)),
                (TreenumeratorMode.VisitingNode,   "d", 1, (0, 2)),
                (TreenumeratorMode.VisitingNode,   "c", 2, (1, 1)),
                (TreenumeratorMode.SchedulingNode, "e", 0, (1, 2)),
                (TreenumeratorMode.VisitingNode,   "e", 1, (1, 2)),
                (TreenumeratorMode.SchedulingNode, "f", 0, (0, 3)),
                (TreenumeratorMode.VisitingNode,   "f", 1, (0, 3)),
                (TreenumeratorMode.VisitingNode,   "e", 2, (1, 2)),
                (TreenumeratorMode.SchedulingNode, "g", 0, (1, 3)),
                (TreenumeratorMode.VisitingNode,   "g", 1, (1, 3)),
                (TreenumeratorMode.SchedulingNode, "h", 0, (0, 4)),
                (TreenumeratorMode.VisitingNode,   "h", 1, (0, 4)),
                (TreenumeratorMode.VisitingNode,   "g", 2, (1, 3)),
                (TreenumeratorMode.SchedulingNode, "i", 0, (1, 4)),
                (TreenumeratorMode.VisitingNode,   "i", 1, (1, 4)),
                (TreenumeratorMode.VisitingNode,   "g", 3, (1, 3)),
                (TreenumeratorMode.VisitingNode,   "e", 3, (1, 2)),
                (TreenumeratorMode.VisitingNode,   "c", 3, (1, 1)),
                (TreenumeratorMode.VisitingNode,   "a", 3, (0, 0)),
              }.ToNodeVisitArray()
            },
            // Skip nodes
            new TestScenario
            {
              NodeTraversalStrategySelector = visit => NodeTraversalStrategy.SkipNode,
              Description = "Skip all nodes",
              ExpectedBreadthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "b", 0, (0, 1)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (1, 1)),
                (TreenumeratorMode.SchedulingNode, "d", 0, (0, 2)),
                (TreenumeratorMode.SchedulingNode, "e", 0, (1, 2)),
                (TreenumeratorMode.SchedulingNode, "f", 0, (0, 3)),
                (TreenumeratorMode.SchedulingNode, "g", 0, (1, 3)),
                (TreenumeratorMode.SchedulingNode, "h", 0, (0, 4)),
                (TreenumeratorMode.SchedulingNode, "i", 0, (1, 4)),
              }.ToNodeVisitArray(),
              ExpectedDepthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "b", 0, (0, 1)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (1, 1)),
                (TreenumeratorMode.SchedulingNode, "d", 0, (0, 2)),
                (TreenumeratorMode.SchedulingNode, "e", 0, (1, 2)),
                (TreenumeratorMode.SchedulingNode, "f", 0, (0, 3)),
                (TreenumeratorMode.SchedulingNode, "g", 0, (1, 3)),
                (TreenumeratorMode.SchedulingNode, "h", 0, (0, 4)),
                (TreenumeratorMode.SchedulingNode, "i", 0, (1, 4)),
              }.ToNodeVisitArray()
            },
            new TestScenario
            {
              NodeTraversalStrategySelector =
                visit =>
                  (visit.Position.Depth == 1 || visit.Position.Depth == 3) && visit.Position.SiblingIndex == 1
                  ? NodeTraversalStrategy.SkipNode
                  : NodeTraversalStrategy.TraverseSubtree,
              Description = "Skip levels 1 & 3, sibling 1 node",
              ExpectedBreadthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0)),
                (TreenumeratorMode.VisitingNode,   "a", 1, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "b", 0, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "a", 2, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (1, 1)),
                (TreenumeratorMode.SchedulingNode, "d", 0, (0, 2)),
                (TreenumeratorMode.VisitingNode,   "a", 3, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "e", 0, (1, 2)),
                (TreenumeratorMode.VisitingNode,   "a", 4, (0, 0)),
                (TreenumeratorMode.VisitingNode,   "b", 1, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "d", 1, (0, 2)),
                (TreenumeratorMode.VisitingNode,   "e", 1, (1, 2)),
                (TreenumeratorMode.SchedulingNode, "f", 0, (0, 3)),
                (TreenumeratorMode.VisitingNode,   "e", 2, (1, 2)),
                (TreenumeratorMode.SchedulingNode, "g", 0, (1, 3)),
                (TreenumeratorMode.SchedulingNode, "h", 0, (0, 4)),
                (TreenumeratorMode.VisitingNode,   "e", 3, (1, 2)),
                (TreenumeratorMode.SchedulingNode, "i", 0, (1, 4)),
                (TreenumeratorMode.VisitingNode,   "e", 4, (1, 2)),
                (TreenumeratorMode.VisitingNode,   "f", 1, (0, 3)),
                (TreenumeratorMode.VisitingNode,   "h", 1, (0, 4)),
                (TreenumeratorMode.VisitingNode,   "i", 1, (1, 4)),
              }.ToNodeVisitArray(),
              ExpectedDepthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0)),
                (TreenumeratorMode.VisitingNode,   "a", 1, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "b", 0, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "b", 1, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "a", 2, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (1, 1)),
                (TreenumeratorMode.SchedulingNode, "d", 0, (0, 2)),
                (TreenumeratorMode.VisitingNode,   "d", 1, (0, 2)),
                (TreenumeratorMode.VisitingNode,   "a", 3, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "e", 0, (1, 2)),
                (TreenumeratorMode.VisitingNode,   "e", 1, (1, 2)),
                (TreenumeratorMode.SchedulingNode, "f", 0, (0, 3)),
                (TreenumeratorMode.VisitingNode,   "f", 1, (0, 3)),
                (TreenumeratorMode.VisitingNode,   "e", 2, (1, 2)),
                (TreenumeratorMode.SchedulingNode, "g", 0, (1, 3)),
                (TreenumeratorMode.SchedulingNode, "h", 0, (0, 4)),
                (TreenumeratorMode.VisitingNode,   "h", 1, (0, 4)),
                (TreenumeratorMode.VisitingNode,   "e", 3, (1, 2)),
                (TreenumeratorMode.SchedulingNode, "i", 0, (1, 4)),
                (TreenumeratorMode.VisitingNode,   "i", 1, (1, 4)),
                (TreenumeratorMode.VisitingNode,   "e", 4, (1, 2)),
                (TreenumeratorMode.VisitingNode,   "a", 4, (0, 0)),
              }.ToNodeVisitArray()
            },
            new TestScenario
            {
              NodeTraversalStrategySelector =
                visit =>
                  visit.Position.Depth == 0 || visit.Position.SiblingIndex == 1
                  ? NodeTraversalStrategy.SkipNode
                  : NodeTraversalStrategy.TraverseSubtree,
              Description = "Skip root and all sibling 1 nodes",
              ExpectedBreadthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "b", 0, (0, 1)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (1, 1)),
                (TreenumeratorMode.SchedulingNode, "d", 0, (0, 2)),
                (TreenumeratorMode.SchedulingNode, "e", 0, (1, 2)),
                (TreenumeratorMode.SchedulingNode, "f", 0, (0, 3)),
                (TreenumeratorMode.SchedulingNode, "g", 0, (1, 3)),
                (TreenumeratorMode.SchedulingNode, "h", 0, (0, 4)),
                (TreenumeratorMode.SchedulingNode, "i", 0, (1, 4)),
                (TreenumeratorMode.VisitingNode,   "b", 1, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "d", 1, (0, 2)),
                (TreenumeratorMode.VisitingNode,   "f", 1, (0, 3)),
                (TreenumeratorMode.VisitingNode,   "h", 1, (0, 4)),
              }.ToNodeVisitArray(),
              ExpectedDepthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "b", 0, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "b", 1, (0, 1)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (1, 1)),
                (TreenumeratorMode.SchedulingNode, "d", 0, (0, 2)),
                (TreenumeratorMode.VisitingNode,   "d", 1, (0, 2)),
                (TreenumeratorMode.SchedulingNode, "e", 0, (1, 2)),
                (TreenumeratorMode.SchedulingNode, "f", 0, (0, 3)),
                (TreenumeratorMode.VisitingNode,   "f", 1, (0, 3)),
                (TreenumeratorMode.SchedulingNode, "g", 0, (1, 3)),
                (TreenumeratorMode.SchedulingNode, "h", 0, (0, 4)),
                (TreenumeratorMode.VisitingNode,   "h", 1, (0, 4)),
                (TreenumeratorMode.SchedulingNode, "i", 0, (1, 4)),
              }.ToNodeVisitArray()
            },
          }
        },

        // Three-level ternary arrow fletching.
        new TreeTestDefinition
        {
          TreeString = "a(b,c(e,f,g),d)",
          TestScenarios = new List<TestScenario>
          {
            // No skipping
            new TestScenario
            {
              NodeTraversalStrategySelector = visit => NodeTraversalStrategy.TraverseSubtree,
              Description = "Traverse all",
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
                (TreenumeratorMode.VisitingNode,   "b", 1, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "c", 1, (1, 1)),
                (TreenumeratorMode.SchedulingNode, "e", 0, (0, 2)),
                (TreenumeratorMode.VisitingNode,   "c", 2, (1, 1)),
                (TreenumeratorMode.SchedulingNode, "f", 0, (1, 2)),
                (TreenumeratorMode.VisitingNode,   "c", 3, (1, 1)),
                (TreenumeratorMode.SchedulingNode, "g", 0, (2, 2)),
                (TreenumeratorMode.VisitingNode,   "c", 4, (1, 1)),
                (TreenumeratorMode.VisitingNode,   "d", 1, (2, 1)),
                (TreenumeratorMode.VisitingNode,   "e", 1, (0, 2)),
                (TreenumeratorMode.VisitingNode,   "f", 1, (1, 2)),
                (TreenumeratorMode.VisitingNode,   "g", 1, (2, 2)),
              }.ToNodeVisitArray(),
              ExpectedDepthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0)),
                (TreenumeratorMode.VisitingNode,   "a", 1, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "b", 0, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "b", 1, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "a", 2, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (1, 1)),
                (TreenumeratorMode.VisitingNode,   "c", 1, (1, 1)),
                (TreenumeratorMode.SchedulingNode, "e", 0, (0, 2)),
                (TreenumeratorMode.VisitingNode,   "e", 1, (0, 2)),
                (TreenumeratorMode.VisitingNode,   "c", 2, (1, 1)),
                (TreenumeratorMode.SchedulingNode, "f", 0, (1, 2)),
                (TreenumeratorMode.VisitingNode,   "f", 1, (1, 2)),
                (TreenumeratorMode.VisitingNode,   "c", 3, (1, 1)),
                (TreenumeratorMode.SchedulingNode, "g", 0, (2, 2)),
                (TreenumeratorMode.VisitingNode,   "g", 1, (2, 2)),
                (TreenumeratorMode.VisitingNode,   "c", 4, (1, 1)),
                (TreenumeratorMode.VisitingNode,   "a", 3, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "d", 0, (2, 1)),
                (TreenumeratorMode.VisitingNode,   "d", 1, (2, 1)),
                (TreenumeratorMode.VisitingNode,   "a", 4, (0, 0)),
              }.ToNodeVisitArray()
            },
            // Skip nodes
            new TestScenario
            {
              NodeTraversalStrategySelector = visit => NodeTraversalStrategy.SkipNode,
              Description = "Skip all nodes",
              ExpectedBreadthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "b", 0, (0, 1)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (1, 1)),
                (TreenumeratorMode.SchedulingNode, "e", 0, (0, 2)),
                (TreenumeratorMode.SchedulingNode, "f", 0, (1, 2)),
                (TreenumeratorMode.SchedulingNode, "g", 0, (2, 2)),
                (TreenumeratorMode.SchedulingNode, "d", 0, (2, 1)),
              }.ToNodeVisitArray(),
              ExpectedDepthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "b", 0, (0, 1)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (1, 1)),
                (TreenumeratorMode.SchedulingNode, "e", 0, (0, 2)),
                (TreenumeratorMode.SchedulingNode, "f", 0, (1, 2)),
                (TreenumeratorMode.SchedulingNode, "g", 0, (2, 2)),
                (TreenumeratorMode.SchedulingNode, "d", 0, (2, 1)),
              }.ToNodeVisitArray()
            },
            new TestScenario
            {
              NodeTraversalStrategySelector = visit => visit.Position.Depth == 1 && visit.Position.SiblingIndex == 1 ? NodeTraversalStrategy.SkipNode : NodeTraversalStrategy.TraverseSubtree,
              Description = "Skip level 1, sibling 1 node",
              ExpectedBreadthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0)),
                (TreenumeratorMode.VisitingNode,   "a", 1, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "b", 0, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "a", 2, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (1, 1)),
                (TreenumeratorMode.SchedulingNode, "e", 0, (0, 2)),
                (TreenumeratorMode.VisitingNode,   "a", 3, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "f", 0, (1, 2)),
                (TreenumeratorMode.VisitingNode,   "a", 4, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "g", 0, (2, 2)),
                (TreenumeratorMode.VisitingNode,   "a", 5, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "d", 0, (2, 1)),
                (TreenumeratorMode.VisitingNode,   "a", 6, (0, 0)),
                (TreenumeratorMode.VisitingNode,   "b", 1, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "e", 1, (0, 2)),
                (TreenumeratorMode.VisitingNode,   "f", 1, (1, 2)),
                (TreenumeratorMode.VisitingNode,   "g", 1, (2, 2)),
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
                (TreenumeratorMode.SchedulingNode, "e", 0, (0, 2)),
                (TreenumeratorMode.VisitingNode,   "e", 1, (0, 2)),
                (TreenumeratorMode.VisitingNode,   "a", 3, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "f", 0, (1, 2)),
                (TreenumeratorMode.VisitingNode,   "f", 1, (1, 2)),
                (TreenumeratorMode.VisitingNode,   "a", 4, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "g", 0, (2, 2)),
                (TreenumeratorMode.VisitingNode,   "g", 1, (2, 2)),
                (TreenumeratorMode.VisitingNode,   "a", 5, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "d", 0, (2, 1)),
                (TreenumeratorMode.VisitingNode,   "d", 1, (2, 1)),
                (TreenumeratorMode.VisitingNode,   "a", 6, (0, 0)),
              }.ToNodeVisitArray()
            },
            // Skip subtree
            new TestScenario
            {
              NodeTraversalStrategySelector = visit => visit.Position.Depth == 1 && visit.Position.SiblingIndex == 1 ? NodeTraversalStrategy.SkipSubtree : NodeTraversalStrategy.TraverseSubtree,
              Description = "Skip level 1, sibling 1 subtree",
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
                (TreenumeratorMode.VisitingNode,   "a", 3, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "d", 0, (2, 1)),
                (TreenumeratorMode.VisitingNode,   "d", 1, (2, 1)),
                (TreenumeratorMode.VisitingNode,   "a", 4, (0, 0)),
              }.ToNodeVisitArray()
            },
            // Skip descendant subtrees
            new TestScenario
            {
              NodeTraversalStrategySelector = visit => visit.Position.Depth == 1 && visit.Position.SiblingIndex == 1 ? NodeTraversalStrategy.SkipDescendants : NodeTraversalStrategy.TraverseSubtree,
              Description = "Skip level 1, sibling 1 descendant subtree",
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
                (TreenumeratorMode.VisitingNode,   "b", 1, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "c", 1, (1, 1)),
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
                (TreenumeratorMode.VisitingNode,   "c", 1, (1, 1)),
                (TreenumeratorMode.VisitingNode,   "a", 3, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "d", 0, (2, 1)),
                (TreenumeratorMode.VisitingNode,   "d", 1, (2, 1)),
                (TreenumeratorMode.VisitingNode,   "a", 4, (0, 0)),
              }.ToNodeVisitArray()
            },
          }
        },

        // Two two-level complete binary trees.
        new TreeTestDefinition
        {
          TreeString = "a(b,c),d(e,f)",
          TestScenarios = new List<TestScenario>
          {
            // No skipping
            new TestScenario
            {
              NodeTraversalStrategySelector = visit => NodeTraversalStrategy.TraverseSubtree,
              Description = "Traverse all",
              ExpectedBreadthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "d", 0, (1, 0)),
                (TreenumeratorMode.VisitingNode,   "a", 1, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "b", 0, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "a", 2, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (1, 1)),
                (TreenumeratorMode.VisitingNode,   "a", 3, (0, 0)),
                (TreenumeratorMode.VisitingNode,   "d", 1, (1, 0)),
                (TreenumeratorMode.SchedulingNode, "e", 0, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "d", 2, (1, 0)),
                (TreenumeratorMode.SchedulingNode, "f", 0, (1, 1)),
                (TreenumeratorMode.VisitingNode,   "d", 3, (1, 0)),
                (TreenumeratorMode.VisitingNode,   "b", 1, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "c", 1, (1, 1)),
                (TreenumeratorMode.VisitingNode,   "e", 1, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "f", 1, (1, 1)),
              }.ToNodeVisitArray(),
              ExpectedDepthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0)),
                (TreenumeratorMode.VisitingNode,   "a", 1, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "b", 0, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "b", 1, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "a", 2, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (1, 1)),
                (TreenumeratorMode.VisitingNode,   "c", 1, (1, 1)),
                (TreenumeratorMode.VisitingNode,   "a", 3, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "d", 0, (1, 0)),
                (TreenumeratorMode.VisitingNode,   "d", 1, (1, 0)),
                (TreenumeratorMode.SchedulingNode, "e", 0, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "e", 1, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "d", 2, (1, 0)),
                (TreenumeratorMode.SchedulingNode, "f", 0, (1, 1)),
                (TreenumeratorMode.VisitingNode,   "f", 1, (1, 1)),
                (TreenumeratorMode.VisitingNode,   "d", 3, (1, 0)),
              }.ToNodeVisitArray()
            },
            // Skip nodes
            new TestScenario
            {
              NodeTraversalStrategySelector = visit => NodeTraversalStrategy.SkipNode,
              Description = "Skip all nodes",
              ExpectedBreadthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "b", 0, (0, 1)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (1, 1)),
                (TreenumeratorMode.SchedulingNode, "d", 0, (1, 0)),
                (TreenumeratorMode.SchedulingNode, "e", 0, (0, 1)),
                (TreenumeratorMode.SchedulingNode, "f", 0, (1, 1)),
              }.ToNodeVisitArray(),
              ExpectedDepthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "b", 0, (0, 1)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (1, 1)),
                (TreenumeratorMode.SchedulingNode, "d", 0, (1, 0)),
                (TreenumeratorMode.SchedulingNode, "e", 0, (0, 1)),
                (TreenumeratorMode.SchedulingNode, "f", 0, (1, 1)),
              }.ToNodeVisitArray()
            },
            new TestScenario
            {
              NodeTraversalStrategySelector = visit => visit.Node == "d" ? NodeTraversalStrategy.SkipNode : NodeTraversalStrategy.TraverseSubtree,
              Description = "Skip level 0, sibling 1 node",
              ExpectedBreadthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "d", 0, (1, 0)),
                (TreenumeratorMode.SchedulingNode, "e", 0, (0, 1)),
                (TreenumeratorMode.SchedulingNode, "f", 0, (1, 1)),
                (TreenumeratorMode.VisitingNode,   "a", 1, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "b", 0, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "a", 2, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (1, 1)),
                (TreenumeratorMode.VisitingNode,   "a", 3, (0, 0)),
                (TreenumeratorMode.VisitingNode,   "e", 1, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "f", 1, (1, 1)),
                (TreenumeratorMode.VisitingNode,   "b", 1, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "c", 1, (1, 1)),
              }.ToNodeVisitArray(),
              ExpectedDepthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0)),
                (TreenumeratorMode.VisitingNode,   "a", 1, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "b", 0, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "b", 1, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "a", 2, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (1, 1)),
                (TreenumeratorMode.VisitingNode,   "c", 1, (1, 1)),
                (TreenumeratorMode.VisitingNode,   "a", 3, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "d", 0, (1, 0)),
                (TreenumeratorMode.SchedulingNode, "e", 0, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "e", 1, (0, 1)),
                (TreenumeratorMode.SchedulingNode, "f", 0, (1, 1)),
                (TreenumeratorMode.VisitingNode,   "f", 1, (1, 1)),
              }.ToNodeVisitArray()
            },
            new TestScenario
            {
              NodeTraversalStrategySelector = visit =>
                visit.Position.Depth == 1 && visit.Position.SiblingIndex == 1
                ? NodeTraversalStrategy.SkipNode
                : NodeTraversalStrategy.TraverseSubtree,
              Description = "Skip level 1, sibling 1 node",
              ExpectedBreadthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "d", 0, (1, 0)),
                (TreenumeratorMode.VisitingNode,   "a", 1, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "b", 0, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "a", 2, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (1, 1)),
                (TreenumeratorMode.VisitingNode,   "a", 3, (0, 0)),
                (TreenumeratorMode.VisitingNode,   "d", 1, (1, 0)),
                (TreenumeratorMode.SchedulingNode, "e", 0, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "d", 2, (1, 0)),
                (TreenumeratorMode.SchedulingNode, "f", 0, (1, 1)),
                (TreenumeratorMode.VisitingNode,   "d", 3, (1, 0)),
                (TreenumeratorMode.VisitingNode,   "b", 1, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "e", 1, (0, 1)),
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
                (TreenumeratorMode.SchedulingNode, "d", 0, (1, 0)),
                (TreenumeratorMode.VisitingNode,   "d", 1, (1, 0)),
                (TreenumeratorMode.SchedulingNode, "e", 0, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "e", 1, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "d", 2, (1, 0)),
                (TreenumeratorMode.SchedulingNode, "f", 0, (1, 1)),
                (TreenumeratorMode.VisitingNode,   "d", 3, (1, 0)),
              }.ToNodeVisitArray()
            },
            // Skip subtree
            new TestScenario
            {
              NodeTraversalStrategySelector = visit => visit.Node == "d" ? NodeTraversalStrategy.SkipSubtree : NodeTraversalStrategy.TraverseSubtree,
              Description = "Skip level 0, sibling 1 subtree",
              ExpectedBreadthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "d", 0, (1, 0)),
                (TreenumeratorMode.VisitingNode,   "a", 1, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "b", 0, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "a", 2, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (1, 1)),
                (TreenumeratorMode.VisitingNode,   "a", 3, (0, 0)),
                (TreenumeratorMode.VisitingNode,   "b", 1, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "c", 1, (1, 1)),
              }.ToNodeVisitArray(),
              ExpectedDepthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0)),
                (TreenumeratorMode.VisitingNode,   "a", 1, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "b", 0, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "b", 1, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "a", 2, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (1, 1)),
                (TreenumeratorMode.VisitingNode,   "c", 1, (1, 1)),
                (TreenumeratorMode.VisitingNode,   "a", 3, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "d", 0, (1, 0)),
              }.ToNodeVisitArray()
            },
            new TestScenario
            {
              NodeTraversalStrategySelector = visit =>
                visit.Position.Depth == 1 && visit.Position.SiblingIndex == 1
                ? NodeTraversalStrategy.SkipSubtree
                : NodeTraversalStrategy.TraverseSubtree,
              Description = "Skip level 1, sibling 1 subtree",
              ExpectedBreadthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "d", 0, (1, 0)),
                (TreenumeratorMode.VisitingNode,   "a", 1, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "b", 0, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "a", 2, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (1, 1)),
                (TreenumeratorMode.VisitingNode,   "a", 3, (0, 0)),
                (TreenumeratorMode.VisitingNode,   "d", 1, (1, 0)),
                (TreenumeratorMode.SchedulingNode, "e", 0, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "d", 2, (1, 0)),
                (TreenumeratorMode.SchedulingNode, "f", 0, (1, 1)),
                (TreenumeratorMode.VisitingNode,   "d", 3, (1, 0)),
                (TreenumeratorMode.VisitingNode,   "b", 1, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "e", 1, (0, 1)),
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
                (TreenumeratorMode.SchedulingNode, "d", 0, (1, 0)),
                (TreenumeratorMode.VisitingNode,   "d", 1, (1, 0)),
                (TreenumeratorMode.SchedulingNode, "e", 0, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "e", 1, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "d", 2, (1, 0)),
                (TreenumeratorMode.SchedulingNode, "f", 0, (1, 1)),
                (TreenumeratorMode.VisitingNode,   "d", 3, (1, 0)),
              }.ToNodeVisitArray()
            },
            // Skip descendant subtrees
            new TestScenario
            {
              NodeTraversalStrategySelector = visit => visit.Node == "d" ? NodeTraversalStrategy.SkipDescendants : NodeTraversalStrategy.TraverseSubtree,
              Description = "Skip level 0, sibling 1 descendant subtrees",
              ExpectedBreadthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "d", 0, (1, 0)),
                (TreenumeratorMode.VisitingNode,   "a", 1, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "b", 0, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "a", 2, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (1, 1)),
                (TreenumeratorMode.VisitingNode,   "a", 3, (0, 0)),
                (TreenumeratorMode.VisitingNode,   "d", 1, (1, 0)),
                (TreenumeratorMode.VisitingNode,   "b", 1, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "c", 1, (1, 1)),
              }.ToNodeVisitArray(),
              ExpectedDepthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0)),
                (TreenumeratorMode.VisitingNode,   "a", 1, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "b", 0, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "b", 1, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "a", 2, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (1, 1)),
                (TreenumeratorMode.VisitingNode,   "c", 1, (1, 1)),
                (TreenumeratorMode.VisitingNode,   "a", 3, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "d", 0, (1, 0)),
                (TreenumeratorMode.VisitingNode,   "d", 1, (1, 0)),
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
              NodeTraversalStrategySelector = visit => NodeTraversalStrategy.TraverseSubtree,
              Description = "Traverse all",
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
                (TreenumeratorMode.VisitingNode,   "b", 1, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "c", 1, (1, 1)),
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
                (TreenumeratorMode.VisitingNode,   "c", 1, (1, 1)),
                (TreenumeratorMode.VisitingNode,   "a", 3, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "d", 0, (2, 1)),
                (TreenumeratorMode.VisitingNode,   "d", 1, (2, 1)),
                (TreenumeratorMode.VisitingNode,   "a", 4, (0, 0)),
              }.ToNodeVisitArray()
            },
            // Skip nodes
            new TestScenario
            {
              NodeTraversalStrategySelector = visit => NodeTraversalStrategy.SkipNode,
              Description = "Skip all nodes",
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
              NodeTraversalStrategySelector = visit => visit.Position.Depth == 0 ? NodeTraversalStrategy.SkipNode : NodeTraversalStrategy.TraverseSubtree,
              Description = "Skip level 0 nodes",
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
              NodeTraversalStrategySelector = visit => visit.Position.Depth == 1 ? NodeTraversalStrategy.SkipNode : NodeTraversalStrategy.TraverseSubtree,
              Description = "Skip level 1 nodes",
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
              NodeTraversalStrategySelector = visit => visit.Node == "b" ? NodeTraversalStrategy.SkipNode : NodeTraversalStrategy.TraverseSubtree,
              Description = "Skip level 1, sibling 0 node",
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
                (TreenumeratorMode.VisitingNode,   "c", 1, (1, 1)),
                (TreenumeratorMode.VisitingNode,   "d", 1, (2, 1)),
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
              NodeTraversalStrategySelector = visit => visit.Node == "c" ? NodeTraversalStrategy.SkipNode : NodeTraversalStrategy.TraverseSubtree,
              Description = "Skip level 1, sibling 1 node",
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
                (TreenumeratorMode.VisitingNode,   "a", 3, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "d", 0, (2, 1)),
                (TreenumeratorMode.VisitingNode,   "d", 1, (2, 1)),
                (TreenumeratorMode.VisitingNode,   "a", 4, (0, 0)),
              }.ToNodeVisitArray()
            },
            new TestScenario
            {
              NodeTraversalStrategySelector = visit => visit.Node == "d" ? NodeTraversalStrategy.SkipNode : NodeTraversalStrategy.TraverseSubtree,
              Description = "Skip level 1, sibling 2 node",
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
                (TreenumeratorMode.VisitingNode,   "b", 1, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "c", 1, (1, 1)),
              }.ToNodeVisitArray(),
              ExpectedDepthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0)),
                (TreenumeratorMode.VisitingNode,   "a", 1, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "b", 0, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "b", 1, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "a", 2, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (1, 1)),
                (TreenumeratorMode.VisitingNode,   "c", 1, (1, 1)),
                (TreenumeratorMode.VisitingNode,   "a", 3, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "d", 0, (2, 1)),
                (TreenumeratorMode.VisitingNode,   "a", 4, (0, 0)),
              }.ToNodeVisitArray()
            },
            // Skip subtree
            new TestScenario
            {
              NodeTraversalStrategySelector = visit => visit.Position.Depth == 0 ? NodeTraversalStrategy.SkipSubtree : NodeTraversalStrategy.TraverseSubtree,
              Description = "Skip level 0 subtrees",
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
              NodeTraversalStrategySelector = visit => visit.Position.Depth == 1 ? NodeTraversalStrategy.SkipSubtree : NodeTraversalStrategy.TraverseSubtree,
              Description = "Skip level 1 subtree",
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
              NodeTraversalStrategySelector = visit => visit.Node == "b" ? NodeTraversalStrategy.SkipSubtree : NodeTraversalStrategy.TraverseSubtree,
              Description = "Skip level 1, sibling 0 subtree",
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
                (TreenumeratorMode.VisitingNode,   "c", 1, (1, 1)),
                (TreenumeratorMode.VisitingNode,   "d", 1, (2, 1)),
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
              NodeTraversalStrategySelector = visit => visit.Node == "c" ? NodeTraversalStrategy.SkipSubtree : NodeTraversalStrategy.TraverseSubtree,
              Description = "Skip level 1, sibling 1 subtree",
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
                (TreenumeratorMode.VisitingNode,   "a", 3, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "d", 0, (2, 1)),
                (TreenumeratorMode.VisitingNode,   "d", 1, (2, 1)),
                (TreenumeratorMode.VisitingNode,   "a", 4, (0, 0)),
              }.ToNodeVisitArray()
            },
            new TestScenario
            {
              NodeTraversalStrategySelector = visit => visit.Node == "d" ? NodeTraversalStrategy.SkipSubtree : NodeTraversalStrategy.TraverseSubtree,
              Description = "Skip level 1, sibling 2 subtree",
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
                (TreenumeratorMode.VisitingNode,   "b", 1, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "c", 1, (1, 1)),
              }.ToNodeVisitArray(),
              ExpectedDepthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0)),
                (TreenumeratorMode.VisitingNode,   "a", 1, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "b", 0, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "b", 1, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "a", 2, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (1, 1)),
                (TreenumeratorMode.VisitingNode,   "c", 1, (1, 1)),
                (TreenumeratorMode.VisitingNode,   "a", 3, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "d", 0, (2, 1)),
                (TreenumeratorMode.VisitingNode,   "a", 4, (0, 0)),
              }.ToNodeVisitArray()
            },
            // Skip descendant subtree
            new TestScenario
            {
              NodeTraversalStrategySelector = visit => visit.Position.Depth == 0 ? NodeTraversalStrategy.SkipDescendants : NodeTraversalStrategy.TraverseSubtree,
              Description = "Skip level 0 descendant subtrees",
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
              NodeTraversalStrategySelector = visit => visit.Position.Depth == 1 ? NodeTraversalStrategy.SkipDescendants : NodeTraversalStrategy.TraverseSubtree,
              Description = "Skip level 1 descendant subtrees",
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
                (TreenumeratorMode.VisitingNode,   "b", 1, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "c", 1, (1, 1)),
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
                (TreenumeratorMode.VisitingNode,   "c", 1, (1, 1)),
                (TreenumeratorMode.VisitingNode,   "a", 3, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "d", 0, (2, 1)),
                (TreenumeratorMode.VisitingNode,   "d", 1, (2, 1)),
                (TreenumeratorMode.VisitingNode,   "a", 4, (0, 0)),
              }.ToNodeVisitArray()
            },
            new TestScenario
            {
              NodeTraversalStrategySelector = visit => visit.Node == "b" ? NodeTraversalStrategy.SkipDescendants : NodeTraversalStrategy.TraverseSubtree,
              Description = "Skip level 1, sibling 0 descendant subtrees",
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
                (TreenumeratorMode.VisitingNode,   "b", 1, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "c", 1, (1, 1)),
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
                (TreenumeratorMode.VisitingNode,   "c", 1, (1, 1)),
                (TreenumeratorMode.VisitingNode,   "a", 3, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "d", 0, (2, 1)),
                (TreenumeratorMode.VisitingNode,   "d", 1, (2, 1)),
                (TreenumeratorMode.VisitingNode,   "a", 4, (0, 0)),
              }.ToNodeVisitArray()
            },
            new TestScenario
            {
              NodeTraversalStrategySelector = visit => visit.Node == "c" ? NodeTraversalStrategy.SkipDescendants : NodeTraversalStrategy.TraverseSubtree,
              Description = "Skip level 1, sibling 1 descendant subtrees",
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
                (TreenumeratorMode.VisitingNode,   "b", 1, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "c", 1, (1, 1)),
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
                (TreenumeratorMode.VisitingNode,   "c", 1, (1, 1)),
                (TreenumeratorMode.VisitingNode,   "a", 3, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "d", 0, (2, 1)),
                (TreenumeratorMode.VisitingNode,   "d", 1, (2, 1)),
                (TreenumeratorMode.VisitingNode,   "a", 4, (0, 0)),
              }.ToNodeVisitArray()
            },
            new TestScenario
            {
              NodeTraversalStrategySelector = visit => visit.Node == "d" ? NodeTraversalStrategy.SkipDescendants : NodeTraversalStrategy.TraverseSubtree,
              Description = "Skip level 1, sibling 2 descendant subtrees",
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
                (TreenumeratorMode.VisitingNode,   "b", 1, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "c", 1, (1, 1)),
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
                (TreenumeratorMode.VisitingNode,   "c", 1, (1, 1)),
                (TreenumeratorMode.VisitingNode,   "a", 3, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "d", 0, (2, 1)),
                (TreenumeratorMode.VisitingNode,   "d", 1, (2, 1)),
                (TreenumeratorMode.VisitingNode,   "a", 4, (0, 0)),
              }.ToNodeVisitArray()
            },
          }
        },

        new TreeTestDefinition
        {
          TreeString = "a(b(c,d,e))",
          TestScenarios = new List<TestScenario>
          {
            // No skipping
            new TestScenario
            {
              NodeTraversalStrategySelector = visit => NodeTraversalStrategy.TraverseSubtree,
              Description = "Traverse all",
              ExpectedBreadthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0)),
                (TreenumeratorMode.VisitingNode,   "a", 1, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "b", 0, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "a", 2, (0, 0)),
                (TreenumeratorMode.VisitingNode,   "b", 1, (0, 1)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (0, 2)),
                (TreenumeratorMode.VisitingNode,   "b", 2, (0, 1)),
                (TreenumeratorMode.SchedulingNode, "d", 0, (1, 2)),
                (TreenumeratorMode.VisitingNode,   "b", 3, (0, 1)),
                (TreenumeratorMode.SchedulingNode, "e", 0, (2, 2)),
                (TreenumeratorMode.VisitingNode,   "b", 4, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "c", 1, (0, 2)),
                (TreenumeratorMode.VisitingNode,   "d", 1, (1, 2)),
                (TreenumeratorMode.VisitingNode,   "e", 1, (2, 2)),
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
                (TreenumeratorMode.SchedulingNode, "d", 0, (1, 2)),
                (TreenumeratorMode.VisitingNode,   "d", 1, (1, 2)),
                (TreenumeratorMode.VisitingNode,   "b", 3, (0, 1)),
                (TreenumeratorMode.SchedulingNode, "e", 0, (2, 2)),
                (TreenumeratorMode.VisitingNode,   "e", 1, (2, 2)),
                (TreenumeratorMode.VisitingNode,   "b", 4, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "a", 2, (0, 0)),
              }.ToNodeVisitArray()
            },
            // Skip nodes
            new TestScenario
            {
              NodeTraversalStrategySelector = visit => visit.Position.Depth == 0 ? NodeTraversalStrategy.TraverseSubtree : NodeTraversalStrategy.SkipNode,
              Description = "Skip all but level 0 nodes",
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
            // Skip subtree
            // Skip descendant subtree
          }
        },

        // Two two-level unary tree.
        new TreeTestDefinition
        {
          TreeString = "a(c),b(d)",
          TestScenarios = new List<TestScenario>()
          {
            // No skipping
            new TestScenario
            {
              NodeTraversalStrategySelector = visit => NodeTraversalStrategy.TraverseSubtree,
              Description = "Traverse all",
              ExpectedBreadthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "b", 0, (1, 0)),
                (TreenumeratorMode.VisitingNode,   "a", 1, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "a", 2, (0, 0)),
                (TreenumeratorMode.VisitingNode,   "b", 1, (1, 0)),
                (TreenumeratorMode.SchedulingNode, "d", 0, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "b", 2, (1, 0)),
                (TreenumeratorMode.VisitingNode,   "c", 1, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "d", 1, (0, 1)),
              }.ToNodeVisitArray(),
              ExpectedDepthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0)),
                (TreenumeratorMode.VisitingNode,   "a", 1, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "c", 1, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "a", 2, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "b", 0, (1, 0)),
                (TreenumeratorMode.VisitingNode,   "b", 1, (1, 0)),
                (TreenumeratorMode.SchedulingNode, "d", 0, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "d", 1, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "b", 2, (1, 0)),
              }.ToNodeVisitArray()
            },
            // Skip nodes
            new TestScenario
            {
              NodeTraversalStrategySelector = visit => NodeTraversalStrategy.SkipNode,
              Description = "Skip all nodes",
              ExpectedBreadthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (0, 1)),
                (TreenumeratorMode.SchedulingNode, "b", 0, (1, 0)),
                (TreenumeratorMode.SchedulingNode, "d", 0, (0, 1)),
              }.ToNodeVisitArray(),
              ExpectedDepthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (0, 1)),
                (TreenumeratorMode.SchedulingNode, "b", 0, (1, 0)),
                (TreenumeratorMode.SchedulingNode, "d", 0, (0, 1)),
              }.ToNodeVisitArray()
            },
            new TestScenario
            {
              NodeTraversalStrategySelector = visit => visit.Position.Depth == 0 ? NodeTraversalStrategy.SkipNode : NodeTraversalStrategy.SkipSubtree,
              Description = "Skip level 0 nodes, skip level 1 subtrees",
              ExpectedBreadthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (0, 1)),
                (TreenumeratorMode.SchedulingNode, "b", 0, (1, 0)),
                (TreenumeratorMode.SchedulingNode, "d", 0, (0, 1)),
              }.ToNodeVisitArray(),
              ExpectedDepthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (0, 1)),
                (TreenumeratorMode.SchedulingNode, "b", 0, (1, 0)),
                (TreenumeratorMode.SchedulingNode, "d", 0, (0, 1)),
              }.ToNodeVisitArray()
            },
            new TestScenario
            {
              NodeTraversalStrategySelector = visit => visit.Node == "a" ? NodeTraversalStrategy.SkipNode : NodeTraversalStrategy.TraverseSubtree,
              Description = "Skip level 0, sibling 0 node",
              ExpectedBreadthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (0, 1)),
                (TreenumeratorMode.SchedulingNode, "b", 0, (1, 0)),
                (TreenumeratorMode.VisitingNode,   "c", 1, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "b", 1, (1, 0)),
                (TreenumeratorMode.SchedulingNode, "d", 0, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "b", 2, (1, 0)),
                (TreenumeratorMode.VisitingNode,   "d", 1, (0, 1)),
              }.ToNodeVisitArray(),
              ExpectedDepthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "c", 1, (0, 1)),
                (TreenumeratorMode.SchedulingNode, "b", 0, (1, 0)),
                (TreenumeratorMode.VisitingNode,   "b", 1, (1, 0)),
                (TreenumeratorMode.SchedulingNode, "d", 0, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "d", 1, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "b", 2, (1, 0)),
              }.ToNodeVisitArray()
            },
            new TestScenario
            {
              NodeTraversalStrategySelector = visit => visit.Position.Depth == 1 ? NodeTraversalStrategy.SkipNode : NodeTraversalStrategy.TraverseSubtree,
              Description = "Skip level 1 nodes",
              ExpectedBreadthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "b", 0, (1, 0)),
                (TreenumeratorMode.VisitingNode,   "a", 1, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "a", 2, (0, 0)),
                (TreenumeratorMode.VisitingNode,   "b", 1, (1, 0)),
                (TreenumeratorMode.SchedulingNode, "d", 0, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "b", 2, (1, 0)),
              }.ToNodeVisitArray(),
              ExpectedDepthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0)),
                (TreenumeratorMode.VisitingNode,   "a", 1, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "a", 2, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "b", 0, (1, 0)),
                (TreenumeratorMode.VisitingNode,   "b", 1, (1, 0)),
                (TreenumeratorMode.SchedulingNode, "d", 0, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "b", 2, (1, 0)),
              }.ToNodeVisitArray()
            },
            new TestScenario
            {
              NodeTraversalStrategySelector = visit => visit.Node == "b" ? NodeTraversalStrategy.SkipNode : NodeTraversalStrategy.TraverseSubtree,
              Description = "Skip level 0, sibling 1 node",
              ExpectedBreadthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "b", 0, (1, 0)),
                (TreenumeratorMode.SchedulingNode, "d", 0, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "a", 1, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "a", 2, (0, 0)),
                (TreenumeratorMode.VisitingNode,   "d", 1, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "c", 1, (0, 1)),
              }.ToNodeVisitArray(),
              ExpectedDepthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0)),
                (TreenumeratorMode.VisitingNode,   "a", 1, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "c", 1, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "a", 2, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "b", 0, (1, 0)),
                (TreenumeratorMode.SchedulingNode, "d", 0, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "d", 1, (0, 1)),
              }.ToNodeVisitArray()
            },
            new TestScenario
            {
              NodeTraversalStrategySelector = visit => visit.Node != "d" ? NodeTraversalStrategy.SkipNode : NodeTraversalStrategy.TraverseSubtree,
              Description = "Skip all but level 1, sibling 1 node",
              ExpectedBreadthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (0, 1)),
                (TreenumeratorMode.SchedulingNode, "b", 0, (1, 0)),
                (TreenumeratorMode.SchedulingNode, "d", 0, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "d", 1, (0, 1)),
              }.ToNodeVisitArray(),
              ExpectedDepthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (0, 1)),
                (TreenumeratorMode.SchedulingNode, "b", 0, (1, 0)),
                (TreenumeratorMode.SchedulingNode, "d", 0, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "d", 1, (0, 1)),
              }.ToNodeVisitArray()
            },
          }
        },

        // Two trees, single node and two-level complete binary tree.
        new TreeTestDefinition
        {
          TreeString = "a,b(c,d)",
          TestScenarios = new List<TestScenario>
          {
            // No skipping
            new TestScenario
            {
              NodeTraversalStrategySelector = visit => NodeTraversalStrategy.TraverseSubtree,
              Description = "Traverse all",
              ExpectedBreadthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "b", 0, (1, 0)),
                (TreenumeratorMode.VisitingNode,   "a", 1, (0, 0)),
                (TreenumeratorMode.VisitingNode,   "b", 1, (1, 0)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "b", 2, (1, 0)),
                (TreenumeratorMode.SchedulingNode, "d", 0, (1, 1)),
                (TreenumeratorMode.VisitingNode,   "b", 3, (1, 0)),
                (TreenumeratorMode.VisitingNode,   "c", 1, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "d", 1, (1, 1)),
              }.ToNodeVisitArray(),
              ExpectedDepthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0)),
                (TreenumeratorMode.VisitingNode,   "a", 1, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "b", 0, (1, 0)),
                (TreenumeratorMode.VisitingNode,   "b", 1, (1, 0)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "c", 1, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "b", 2, (1, 0)),
                (TreenumeratorMode.SchedulingNode, "d", 0, (1, 1)),
                (TreenumeratorMode.VisitingNode,   "d", 1, (1, 1)),
                (TreenumeratorMode.VisitingNode,   "b", 3, (1, 0)),
              }.ToNodeVisitArray()
            },
            // Skip nodes
            new TestScenario
            {
              NodeTraversalStrategySelector = visit => NodeTraversalStrategy.SkipNode,
              Description = "Skip all nodes",
              ExpectedBreadthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "b", 0, (1, 0)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (0, 1)),
                (TreenumeratorMode.SchedulingNode, "d", 0, (1, 1)),
              }.ToNodeVisitArray(),
              ExpectedDepthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "b", 0, (1, 0)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (0, 1)),
                (TreenumeratorMode.SchedulingNode, "d", 0, (1, 1)),
              }.ToNodeVisitArray()
            },
            new TestScenario
            {
              NodeTraversalStrategySelector = visit => visit.Node == "b" ? NodeTraversalStrategy.SkipNode : NodeTraversalStrategy.TraverseSubtree,
              Description = "Skip level 0, sibling 1 node",
              ExpectedBreadthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "b", 0, (1, 0)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (0, 1)),
                (TreenumeratorMode.SchedulingNode, "d", 0, (1, 1)),
                (TreenumeratorMode.VisitingNode,   "a", 1, (0, 0)),
                (TreenumeratorMode.VisitingNode,   "c", 1, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "d", 1, (1, 1)),
              }.ToNodeVisitArray(),
              ExpectedDepthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0)),
                (TreenumeratorMode.VisitingNode,   "a", 1, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "b", 0, (1, 0)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "c", 1, (0, 1)),
                (TreenumeratorMode.SchedulingNode, "d", 0, (1, 1)),
                (TreenumeratorMode.VisitingNode,   "d", 1, (1, 1)),
              }.ToNodeVisitArray()
            },
          }
        },

        // Three root nodes, second root node has two children.
        new TreeTestDefinition
        {
          TreeString = "a,b(d,e),c",
          TestScenarios = new List<TestScenario>
          {
            // No skipping
            new TestScenario
            {
              NodeTraversalStrategySelector = visit => NodeTraversalStrategy.TraverseSubtree,
              Description = "Traverse all",
              ExpectedBreadthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "b", 0, (1, 0)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (2, 0)),
                (TreenumeratorMode.VisitingNode,   "a", 1, (0, 0)),
                (TreenumeratorMode.VisitingNode,   "b", 1, (1, 0)),
                (TreenumeratorMode.SchedulingNode, "d", 0, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "b", 2, (1, 0)),
                (TreenumeratorMode.SchedulingNode, "e", 0, (1, 1)),
                (TreenumeratorMode.VisitingNode,   "b", 3, (1, 0)),
                (TreenumeratorMode.VisitingNode,   "c", 1, (2, 0)),
                (TreenumeratorMode.VisitingNode,   "d", 1, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "e", 1, (1, 1)),
              }.ToNodeVisitArray(),
              ExpectedDepthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0)),
                (TreenumeratorMode.VisitingNode,   "a", 1, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "b", 0, (1, 0)),
                (TreenumeratorMode.VisitingNode,   "b", 1, (1, 0)),
                (TreenumeratorMode.SchedulingNode, "d", 0, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "d", 1, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "b", 2, (1, 0)),
                (TreenumeratorMode.SchedulingNode, "e", 0, (1, 1)),
                (TreenumeratorMode.VisitingNode,   "e", 1, (1, 1)),
                (TreenumeratorMode.VisitingNode,   "b", 3, (1, 0)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (2, 0)),
                (TreenumeratorMode.VisitingNode,   "c", 1, (2, 0)),
              }.ToNodeVisitArray()
            },
            // Skip nodes
            new TestScenario
            {
              NodeTraversalStrategySelector = visit => visit.Node == "b" ? NodeTraversalStrategy.SkipNode : NodeTraversalStrategy.TraverseSubtree,
              Description = "Skip level 0, sibling 1 node",
              ExpectedBreadthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "b", 0, (1, 0)),
                (TreenumeratorMode.SchedulingNode, "d", 0, (0, 1)),
                (TreenumeratorMode.SchedulingNode, "e", 0, (1, 1)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (2, 0)),
                (TreenumeratorMode.VisitingNode,   "a", 1, (0, 0)),
                (TreenumeratorMode.VisitingNode,   "d", 1, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "e", 1, (1, 1)),
                (TreenumeratorMode.VisitingNode,   "c", 1, (2, 0)),
              }.ToNodeVisitArray(),
              ExpectedDepthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0)),
                (TreenumeratorMode.VisitingNode,   "a", 1, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "b", 0, (1, 0)),
                (TreenumeratorMode.SchedulingNode, "d", 0, (0, 1)),
                (TreenumeratorMode.VisitingNode,   "d", 1, (0, 1)),
                (TreenumeratorMode.SchedulingNode, "e", 0, (1, 1)),
                (TreenumeratorMode.VisitingNode,   "e", 1, (1, 1)),
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
              NodeTraversalStrategySelector = visit => NodeTraversalStrategy.TraverseSubtree,
              Description = "Traverse all",
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
              NodeTraversalStrategySelector = visit => NodeTraversalStrategy.SkipNode,
              Description = "Skip all nodes",
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
              NodeTraversalStrategySelector = visit => visit.Node == "a" ? NodeTraversalStrategy.SkipNode : NodeTraversalStrategy.TraverseSubtree,
              Description = "Skip level 0, sibling 0 node",
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
            new TestScenario
            {
              NodeTraversalStrategySelector = visit => visit.Node == "b" ? NodeTraversalStrategy.SkipNode : NodeTraversalStrategy.TraverseSubtree,
              Description = "Skip level 0, sibling 1 node",
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
              NodeTraversalStrategySelector = visit => visit.Node == "c" ? NodeTraversalStrategy.SkipNode : NodeTraversalStrategy.TraverseSubtree,
              Description = "Skip level 0, sibling 2 node",
              ExpectedBreadthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "b", 0, (1, 0)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (2, 0)),
                (TreenumeratorMode.VisitingNode,   "a", 1, (0, 0)),
                (TreenumeratorMode.VisitingNode,   "b", 1, (1, 0)),
              }.ToNodeVisitArray(),
              ExpectedDepthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0)),
                (TreenumeratorMode.VisitingNode,   "a", 1, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "b", 0, (1, 0)),
                (TreenumeratorMode.VisitingNode,   "b", 1, (1, 0)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (2, 0)),
              }.ToNodeVisitArray()
            },
            // Skip subtree
            new TestScenario
            {
              NodeTraversalStrategySelector = visit => visit.Node == "a" ? NodeTraversalStrategy.SkipSubtree : NodeTraversalStrategy.TraverseSubtree,
              Description = "Skip level 0, sibling 0 subtree",
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
            new TestScenario
            {
              NodeTraversalStrategySelector = visit => visit.Node == "b" ? NodeTraversalStrategy.SkipSubtree : NodeTraversalStrategy.TraverseSubtree,
              Description = "Skip level 0, sibling 1 subtree",
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
              NodeTraversalStrategySelector = visit => visit.Node == "c" ? NodeTraversalStrategy.SkipSubtree : NodeTraversalStrategy.TraverseSubtree,
              Description = "Skip level 0, sibling 2 subtree",
              ExpectedBreadthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "b", 0, (1, 0)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (2, 0)),
                (TreenumeratorMode.VisitingNode,   "a", 1, (0, 0)),
                (TreenumeratorMode.VisitingNode,   "b", 1, (1, 0)),
              }.ToNodeVisitArray(),
              ExpectedDepthFirstResults = new[]
              {
                (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0)),
                (TreenumeratorMode.VisitingNode,   "a", 1, (0, 0)),
                (TreenumeratorMode.SchedulingNode, "b", 0, (1, 0)),
                (TreenumeratorMode.VisitingNode,   "b", 1, (1, 0)),
                (TreenumeratorMode.SchedulingNode, "c", 0, (2, 0)),
              }.ToNodeVisitArray()
            },
            new TestScenario
            {
              NodeTraversalStrategySelector = visit => NodeTraversalStrategy.SkipSubtree,
              Description = "Skip level 0 subtrees",
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
            // Skip descendant subtrees
            new TestScenario
            {
              NodeTraversalStrategySelector = visit => visit.Node == "a" ? NodeTraversalStrategy.SkipDescendants : NodeTraversalStrategy.TraverseSubtree,
              Description = "Skip level 0, sibling 0 descendant subtrees",
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
            new TestScenario
            {
              NodeTraversalStrategySelector = visit => visit.Node == "b" ? NodeTraversalStrategy.SkipDescendants : NodeTraversalStrategy.TraverseSubtree,
              Description = "Skip level 0, sibling 1 descendant subtrees",
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
            new TestScenario
            {
              NodeTraversalStrategySelector = visit => visit.Node == "c" ? NodeTraversalStrategy.SkipDescendants : NodeTraversalStrategy.TraverseSubtree,
              Description = "Skip level 0, sibling 2  descendant subtrees",
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
            new TestScenario
            {
              NodeTraversalStrategySelector = visit => NodeTraversalStrategy.SkipDescendants,
              Description = "Skip level 0, descendant subtrees",
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
          }
        },
      };
  }
}
