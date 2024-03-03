using Arborist.Core;
using Arborist.TestUtils;
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
              SchedulingPredicate = visit => SchedulingStrategy.TraverseSubtree,
              Description = "Traverse all",
              ExpectedBreadthFirstResults = new MoveNextResult<string>[]
              {
              },
              ExpectedDepthFirstResults = new MoveNextResult<string>[]
              {
              }
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
              SchedulingPredicate = visit => SchedulingStrategy.TraverseSubtree,
              Description = "Traverse all",
              ExpectedBreadthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, (0, 0), (0, 0)),
                (TreenumeratorState.VisitingNode,   "a", 1, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "b", 0, (0, 1), (0, 1)),
                (TreenumeratorState.VisitingNode,   "a", 2, (0, 0), (0, 0)),
                (TreenumeratorState.VisitingNode,   "b", 1, (0, 1), (0, 1)),
                (TreenumeratorState.SchedulingNode, "c", 0, (0, 2), (0, 2)),
                (TreenumeratorState.VisitingNode,   "b", 2, (0, 1), (0, 1)),
                (TreenumeratorState.VisitingNode,   "c", 1, (0, 2), (0, 2)),
                (TreenumeratorState.SchedulingNode, "d", 0, (0, 3), (0, 3)),
                (TreenumeratorState.VisitingNode,   "c", 2, (0, 2), (0, 2)),
                (TreenumeratorState.VisitingNode,   "d", 1, (0, 3), (0, 3)),
                (TreenumeratorState.SchedulingNode, "e", 0, (0, 4), (0, 4)),
                (TreenumeratorState.VisitingNode,   "d", 2, (0, 3), (0, 3)),
                (TreenumeratorState.VisitingNode,   "e", 1, (0, 4), (0, 4)),
              },
              ExpectedDepthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, (0, 0), (0, 0)),
                (TreenumeratorState.VisitingNode,   "a", 1, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "b", 0, (0, 1), (0, 1)),
                (TreenumeratorState.VisitingNode,   "b", 1, (0, 1), (0, 1)),
                (TreenumeratorState.SchedulingNode, "c", 0, (0, 2), (0, 2)),
                (TreenumeratorState.VisitingNode,   "c", 1, (0, 2), (0, 2)),
                (TreenumeratorState.SchedulingNode, "d", 0, (0, 3), (0, 3)),
                (TreenumeratorState.VisitingNode,   "d", 1, (0, 3), (0, 3)),
                (TreenumeratorState.SchedulingNode, "e", 0, (0, 4), (0, 4)),
                (TreenumeratorState.VisitingNode,   "e", 1, (0, 4), (0, 4)),
                (TreenumeratorState.VisitingNode,   "d", 2, (0, 3), (0, 3)),
                (TreenumeratorState.VisitingNode,   "c", 2, (0, 2), (0, 2)),
                (TreenumeratorState.VisitingNode,   "b", 2, (0, 1), (0, 1)),
                (TreenumeratorState.VisitingNode,   "a", 2, (0, 0), (0, 0)),
              }
            },
            // Skip nodes
            new TestScenario
            {
              SchedulingPredicate = visit => SchedulingStrategy.SkipNode,
              Description = "Skip all nodes",
              ExpectedBreadthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "b", 0, (0, 1), (0, 0)),
                (TreenumeratorState.SchedulingNode, "c", 0, (0, 2), (0, 0)),
                (TreenumeratorState.SchedulingNode, "d", 0, (0, 3), (0, 0)),
                (TreenumeratorState.SchedulingNode, "e", 0, (0, 4), (0, 0)),
              },
              ExpectedDepthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "b", 0, (0, 1), (0, 0)),
                (TreenumeratorState.SchedulingNode, "c", 0, (0, 2), (0, 0)),
                (TreenumeratorState.SchedulingNode, "d", 0, (0, 3), (0, 0)),
                (TreenumeratorState.SchedulingNode, "e", 0, (0, 4), (0, 0)),
              }
            },
            new TestScenario
            {
              SchedulingPredicate = visit => visit.OriginalPosition.Depth > 0 && visit.OriginalPosition.Depth < 4 ? SchedulingStrategy.SkipNode : SchedulingStrategy.TraverseSubtree,
              Description = "Skip levels 1, 2, 3 node",
              ExpectedBreadthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, (0, 0), (0, 0)),
                (TreenumeratorState.VisitingNode,   "a", 1, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "b", 0, (0, 1), (0, 1)),
                (TreenumeratorState.SchedulingNode, "c", 0, (0, 2), (0, 1)),
                (TreenumeratorState.SchedulingNode, "d", 0, (0, 3), (0, 1)),
                (TreenumeratorState.SchedulingNode, "e", 0, (0, 4), (0, 1)),
                (TreenumeratorState.VisitingNode,   "a", 2, (0, 0), (0, 0)),
                (TreenumeratorState.VisitingNode,   "e", 1, (0, 4), (0, 1)),
              },
              ExpectedDepthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, (0, 0), (0, 0)),
                (TreenumeratorState.VisitingNode,   "a", 1, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "b", 0, (0, 1), (0, 1)),
                (TreenumeratorState.SchedulingNode, "c", 0, (0, 2), (0, 1)),
                (TreenumeratorState.SchedulingNode, "d", 0, (0, 3), (0, 1)),
                (TreenumeratorState.SchedulingNode, "e", 0, (0, 4), (0, 1)),
                (TreenumeratorState.VisitingNode,   "e", 1, (0, 4), (0, 1)),
                (TreenumeratorState.VisitingNode,   "a", 2, (0, 0), (0, 0)),
              }
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
              SchedulingPredicate = visit => SchedulingStrategy.TraverseSubtree,
              Description = "Traverse all",
              ExpectedBreadthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, (0, 0), (0, 0)),
                (TreenumeratorState.VisitingNode,   "a", 1, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "b", 0, (0, 1), (0, 1)),
                (TreenumeratorState.VisitingNode,   "a", 2, (0, 0), (0, 0)),
                (TreenumeratorState.VisitingNode,   "b", 1, (0, 1), (0, 1)),
                (TreenumeratorState.SchedulingNode, "c", 0, (0, 2), (0, 2)),
                (TreenumeratorState.VisitingNode,   "b", 2, (0, 1), (0, 1)),
                (TreenumeratorState.VisitingNode,   "c", 1, (0, 2), (0, 2)),
              },
              ExpectedDepthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, (0, 0), (0, 0)),
                (TreenumeratorState.VisitingNode,   "a", 1, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "b", 0, (0, 1), (0, 1)),
                (TreenumeratorState.VisitingNode,   "b", 1, (0, 1), (0, 1)),
                (TreenumeratorState.SchedulingNode, "c", 0, (0, 2), (0, 2)),
                (TreenumeratorState.VisitingNode,   "c", 1, (0, 2), (0, 2)),
                (TreenumeratorState.VisitingNode,   "b", 2, (0, 1), (0, 1)),
                (TreenumeratorState.VisitingNode,   "a", 2, (0, 0), (0, 0)),
              }
            },
            // Skip nodes
            new TestScenario
            {
              SchedulingPredicate = visit => SchedulingStrategy.SkipNode,
              Description = "Skip all nodes",
              ExpectedBreadthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "b", 0, (0, 1), (0, 0)),
                (TreenumeratorState.SchedulingNode, "c", 0, (0, 2), (0, 0)),
              },
              ExpectedDepthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "b", 0, (0, 1), (0, 0)),
                (TreenumeratorState.SchedulingNode, "c", 0, (0, 2), (0, 0)),
              }
            },
            new TestScenario
            {
              SchedulingPredicate = visit => visit.OriginalPosition.Depth == 0 ? SchedulingStrategy.SkipNode : SchedulingStrategy.TraverseSubtree,
              Description = "Skip level 0 node",
              ExpectedBreadthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "b", 0, (0, 1), (0, 0)),
                (TreenumeratorState.VisitingNode,   "b", 1, (0, 1), (0, 0)),
                (TreenumeratorState.SchedulingNode, "c", 0, (0, 2), (0, 1)),
                (TreenumeratorState.VisitingNode,   "b", 2, (0, 1), (0, 0)),
                (TreenumeratorState.VisitingNode,   "c", 1, (0, 2), (0, 1)),
              },
              ExpectedDepthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "b", 0, (0, 1), (0, 0)),
                (TreenumeratorState.VisitingNode,   "b", 1, (0, 1), (0, 0)),
                (TreenumeratorState.SchedulingNode, "c", 0, (0, 2), (0, 1)),
                (TreenumeratorState.VisitingNode,   "c", 1, (0, 2), (0, 1)),
                (TreenumeratorState.VisitingNode,   "b", 2, (0, 1), (0, 0)),
              }
            },
            new TestScenario
            {
              SchedulingPredicate = visit => visit.OriginalPosition.Depth == 1 ? SchedulingStrategy.SkipNode : SchedulingStrategy.TraverseSubtree,
              Description = "Skip level 1 node",
              ExpectedBreadthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, (0, 0), (0, 0)),
                (TreenumeratorState.VisitingNode,   "a", 1, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "b", 0, (0, 1), (0, 1)),
                (TreenumeratorState.SchedulingNode, "c", 0, (0, 2), (0, 1)),
                (TreenumeratorState.VisitingNode,   "a", 2, (0, 0), (0, 0)),
                (TreenumeratorState.VisitingNode,   "c", 1, (0, 2), (0, 1)),
              },
              ExpectedDepthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, (0, 0), (0, 0)),
                (TreenumeratorState.VisitingNode,   "a", 1, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "b", 0, (0, 1), (0, 1)),
                (TreenumeratorState.SchedulingNode, "c", 0, (0, 2), (0, 1)),
                (TreenumeratorState.VisitingNode,   "c", 1, (0, 2), (0, 1)),
                (TreenumeratorState.VisitingNode,   "a", 2, (0, 0), (0, 0)),
              }
            },
            new TestScenario
            {
              SchedulingPredicate = visit => visit.OriginalPosition.Depth == 2 ? SchedulingStrategy.SkipNode : SchedulingStrategy.TraverseSubtree,
              Description = "Skip level 2 node",
              ExpectedBreadthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, (0, 0), (0, 0)),
                (TreenumeratorState.VisitingNode,   "a", 1, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "b", 0, (0, 1), (0, 1)),
                (TreenumeratorState.VisitingNode,   "a", 2, (0, 0), (0, 0)),
                (TreenumeratorState.VisitingNode,   "b", 1, (0, 1), (0, 1)),
                (TreenumeratorState.SchedulingNode, "c", 0, (0, 2), (0, 2)),
                (TreenumeratorState.VisitingNode,   "b", 2, (0, 1), (0, 1)),
              },
              ExpectedDepthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, (0, 0), (0, 0)),
                (TreenumeratorState.VisitingNode,   "a", 1, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "b", 0, (0, 1), (0, 1)),
                (TreenumeratorState.VisitingNode,   "b", 1, (0, 1), (0, 1)),
                (TreenumeratorState.SchedulingNode, "c", 0, (0, 2), (0, 2)),
                (TreenumeratorState.VisitingNode,   "b", 2, (0, 1), (0, 1)),
                (TreenumeratorState.VisitingNode,   "a", 2, (0, 0), (0, 0)),
              }
            },
            new TestScenario
            {
              SchedulingPredicate = visit => visit.OriginalPosition.Depth == 0 || visit.OriginalPosition.Depth == 2 ? SchedulingStrategy.SkipNode : SchedulingStrategy.TraverseSubtree,
              Description = "Skip level 0 and level 2 nodes",
              ExpectedBreadthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "b", 0, (0, 1), (0, 0)),
                (TreenumeratorState.VisitingNode,   "b", 1, (0, 1), (0, 0)),
                (TreenumeratorState.SchedulingNode, "c", 0, (0, 2), (0, 1)),
                (TreenumeratorState.VisitingNode,   "b", 2, (0, 1), (0, 0)),
              },
              ExpectedDepthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "b", 0, (0, 1), (0, 0)),
                (TreenumeratorState.VisitingNode,   "b", 1, (0, 1), (0, 0)),
                (TreenumeratorState.SchedulingNode, "c", 0, (0, 2), (0, 1)),
                (TreenumeratorState.VisitingNode,   "b", 2, (0, 1), (0, 0)),
              }
            },
            // Skip subtree
            new TestScenario
            {
              SchedulingPredicate = visit => visit.OriginalPosition.Depth == 0 ? SchedulingStrategy.SkipSubtree : SchedulingStrategy.TraverseSubtree,
              Description = "Skip level 0, sibling 0 subtree",
              ExpectedBreadthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, (0, 0), (0, 0)),
              },
              ExpectedDepthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, (0, 0), (0, 0)),
              }
            },
            new TestScenario
            {
              SchedulingPredicate = visit => visit.OriginalPosition.Depth == 1 ? SchedulingStrategy.SkipSubtree : SchedulingStrategy.TraverseSubtree,
              Description = "Skip level 1, sibling 0 subtree",
              ExpectedBreadthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, (0, 0), (0, 0)),
                (TreenumeratorState.VisitingNode,   "a", 1, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "b", 0, (0, 1), (0, 1)),
                (TreenumeratorState.VisitingNode,   "a", 2, (0, 0), (0, 0)),
              },
              ExpectedDepthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, (0, 0), (0, 0)),
                (TreenumeratorState.VisitingNode,   "a", 1, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "b", 0, (0, 1), (0, 1)),
                (TreenumeratorState.VisitingNode,   "a", 2, (0, 0), (0, 0)),
              }
            },
            new TestScenario
            {
              SchedulingPredicate = visit => visit.OriginalPosition.Depth == 2 ? SchedulingStrategy.SkipSubtree : SchedulingStrategy.TraverseSubtree,
              Description = "Skip level 2, sibling 0 subtree",
              ExpectedBreadthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, (0, 0), (0, 0)),
                (TreenumeratorState.VisitingNode,   "a", 1, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "b", 0, (0, 1), (0, 1)),
                (TreenumeratorState.VisitingNode,   "a", 2, (0, 0), (0, 0)),
                (TreenumeratorState.VisitingNode,   "b", 1, (0, 1), (0, 1)),
                (TreenumeratorState.SchedulingNode, "c", 0, (0, 2), (0, 2)),
                (TreenumeratorState.VisitingNode,   "b", 2, (0, 1), (0, 1)),
              },
              ExpectedDepthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, (0, 0), (0, 0)),
                (TreenumeratorState.VisitingNode,   "a", 1, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "b", 0, (0, 1), (0, 1)),
                (TreenumeratorState.VisitingNode,   "b", 1, (0, 1), (0, 1)),
                (TreenumeratorState.SchedulingNode, "c", 0, (0, 2), (0, 2)),
                (TreenumeratorState.VisitingNode,   "b", 2, (0, 1), (0, 1)),
                (TreenumeratorState.VisitingNode,   "a", 2, (0, 0), (0, 0)),
              }
            },
            // Skip descendant subtrees
            new TestScenario
            {
              SchedulingPredicate = visit => visit.OriginalPosition.Depth == 0 ? SchedulingStrategy.SkipDescendants : SchedulingStrategy.TraverseSubtree,
              Description = "Skip level 0, sibling 0 descedant subtrees",
              ExpectedBreadthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, (0, 0), (0, 0)),
                (TreenumeratorState.VisitingNode,   "a", 1, (0, 0), (0, 0)),
              },
              ExpectedDepthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, (0, 0), (0, 0)),
                (TreenumeratorState.VisitingNode,   "a", 1, (0, 0), (0, 0)),
              }
            },
            new TestScenario
            {
              SchedulingPredicate = visit => visit.OriginalPosition.Depth == 1 ? SchedulingStrategy.SkipDescendants : SchedulingStrategy.TraverseSubtree,
              Description = "Skip level 1, sibling 0 descedant subtrees",
              ExpectedBreadthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, (0, 0), (0, 0)),
                (TreenumeratorState.VisitingNode,   "a", 1, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "b", 0, (0, 1), (0, 1)),
                (TreenumeratorState.VisitingNode,   "a", 2, (0, 0), (0, 0)),
                (TreenumeratorState.VisitingNode,   "b", 1, (0, 1), (0, 1)),
              },
              ExpectedDepthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, (0, 0), (0, 0)),
                (TreenumeratorState.VisitingNode,   "a", 1, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "b", 0, (0, 1), (0, 1)),
                (TreenumeratorState.VisitingNode,   "b", 1, (0, 1), (0, 1)),
                (TreenumeratorState.VisitingNode,   "a", 2, (0, 0), (0, 0)),
              }
            },
            new TestScenario
            {
              SchedulingPredicate = visit => visit.OriginalPosition.Depth == 2 ? SchedulingStrategy.SkipDescendants : SchedulingStrategy.TraverseSubtree,
              Description = "Skip level 2, sibling 0 descedant subtrees",
              ExpectedBreadthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, (0, 0), (0, 0)),
                (TreenumeratorState.VisitingNode,   "a", 1, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "b", 0, (0, 1), (0, 1)),
                (TreenumeratorState.VisitingNode,   "a", 2, (0, 0), (0, 0)),
                (TreenumeratorState.VisitingNode,   "b", 1, (0, 1), (0, 1)),
                (TreenumeratorState.SchedulingNode, "c", 0, (0, 2), (0, 2)),
                (TreenumeratorState.VisitingNode,   "b", 2, (0, 1), (0, 1)),
                (TreenumeratorState.VisitingNode,   "c", 1, (0, 2), (0, 2)),
              },
              ExpectedDepthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, (0, 0), (0, 0)),
                (TreenumeratorState.VisitingNode,   "a", 1, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "b", 0, (0, 1), (0, 1)),
                (TreenumeratorState.VisitingNode,   "b", 1, (0, 1), (0, 1)),
                (TreenumeratorState.SchedulingNode, "c", 0, (0, 2), (0, 2)),
                (TreenumeratorState.VisitingNode,   "c", 1, (0, 2), (0, 2)),
                (TreenumeratorState.VisitingNode,   "b", 2, (0, 1), (0, 1)),
                (TreenumeratorState.VisitingNode,   "a", 2, (0, 0), (0, 0)),
              }
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
              SchedulingPredicate = visit => SchedulingStrategy.TraverseSubtree,
              Description = "Traverse all",
              ExpectedBreadthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, (0, 0), (0, 0)),
                (TreenumeratorState.VisitingNode,   "a", 1, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "b", 0, (0, 1), (0, 1)),
                (TreenumeratorState.VisitingNode,   "a", 2, (0, 0), (0, 0)),
                (TreenumeratorState.VisitingNode,   "b", 1, (0, 1), (0, 1)),
                (TreenumeratorState.SchedulingNode, "c", 0, (0, 2), (0, 2)),
                (TreenumeratorState.VisitingNode,   "b", 2, (0, 1), (0, 1)),
                (TreenumeratorState.SchedulingNode, "d", 0, (1, 2), (1, 2)),
                (TreenumeratorState.VisitingNode,   "b", 3, (0, 1), (0, 1)),
                (TreenumeratorState.VisitingNode,   "c", 1, (0, 2), (0, 2)),
                (TreenumeratorState.VisitingNode,   "d", 1, (1, 2), (1, 2)),
              },
              ExpectedDepthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, (0, 0), (0, 0)),
                (TreenumeratorState.VisitingNode,   "a", 1, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "b", 0, (0, 1), (0, 1)),
                (TreenumeratorState.VisitingNode,   "b", 1, (0, 1), (0, 1)),
                (TreenumeratorState.SchedulingNode, "c", 0, (0, 2), (0, 2)),
                (TreenumeratorState.VisitingNode,   "c", 1, (0, 2), (0, 2)),
                (TreenumeratorState.VisitingNode,   "b", 2, (0, 1), (0, 1)),
                (TreenumeratorState.SchedulingNode, "d", 0, (1, 2), (1, 2)),
                (TreenumeratorState.VisitingNode,   "d", 1, (1, 2), (1, 2)),
                (TreenumeratorState.VisitingNode,   "b", 3, (0, 1), (0, 1)),
                (TreenumeratorState.VisitingNode,   "a", 2, (0, 0), (0, 0)),
              }
            },
            // Skip nodes
            new TestScenario
            {
              SchedulingPredicate = visit => visit.Node == "b" ? SchedulingStrategy.SkipNode : SchedulingStrategy.TraverseSubtree,
              Description = "Skip level 1 node",
              ExpectedBreadthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, (0, 0), (0, 0)),
                (TreenumeratorState.VisitingNode,   "a", 1, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "b", 0, (0, 1), (0, 1)),
                (TreenumeratorState.SchedulingNode, "c", 0, (0, 2), (0, 1)),
                (TreenumeratorState.VisitingNode,   "a", 2, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "d", 0, (1, 2), (1, 1)),
                (TreenumeratorState.VisitingNode,   "a", 3, (0, 0), (0, 0)),
                (TreenumeratorState.VisitingNode,   "c", 1, (0, 2), (0, 1)),
                (TreenumeratorState.VisitingNode,   "d", 1, (1, 2), (1, 1)),
              },
              ExpectedDepthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, (0, 0), (0, 0)),
                (TreenumeratorState.VisitingNode,   "a", 1, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "b", 0, (0, 1), (0, 1)),
                (TreenumeratorState.SchedulingNode, "c", 0, (0, 2), (0, 1)),
                (TreenumeratorState.VisitingNode,   "c", 1, (0, 2), (0, 1)),
                (TreenumeratorState.VisitingNode,   "a", 2, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "d", 0, (1, 2), (1, 1)),
                (TreenumeratorState.VisitingNode,   "d", 1, (1, 2), (1, 1)),
                (TreenumeratorState.VisitingNode,   "a", 3, (0, 0), (0, 0)),
              }
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
              SchedulingPredicate = visit => SchedulingStrategy.TraverseSubtree,
              Description = "Traverse all",
              ExpectedBreadthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, (0, 0), (0, 0)),
                (TreenumeratorState.VisitingNode,   "a", 1, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "b", 0, (0, 1), (0, 1)),
                (TreenumeratorState.VisitingNode,   "a", 2, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "c", 0, (1, 1), (1, 1)),
                (TreenumeratorState.VisitingNode,   "a", 3, (0, 0), (0, 0)),
                (TreenumeratorState.VisitingNode,   "b", 1, (0, 1), (0, 1)),
                (TreenumeratorState.SchedulingNode, "d", 0, (0, 2), (0, 2)),
                (TreenumeratorState.VisitingNode,   "b", 2, (0, 1), (0, 1)),
                (TreenumeratorState.SchedulingNode, "e", 0, (1, 2), (1, 2)),
                (TreenumeratorState.VisitingNode,   "b", 3, (0, 1), (0, 1)),
                (TreenumeratorState.VisitingNode,   "c", 1, (1, 1), (1, 1)),
                (TreenumeratorState.SchedulingNode, "f", 0, (0, 2), (0, 2)),
                (TreenumeratorState.VisitingNode,   "c", 2, (1, 1), (1, 1)),
                (TreenumeratorState.SchedulingNode, "g", 0, (1, 2), (1, 2)),
                (TreenumeratorState.VisitingNode,   "c", 3, (1, 1), (1, 1)),
                (TreenumeratorState.VisitingNode,   "d", 1, (0, 2), (0, 2)),
                (TreenumeratorState.VisitingNode,   "e", 1, (1, 2), (1, 2)),
                (TreenumeratorState.VisitingNode,   "f", 1, (0, 2), (0, 2)),
                (TreenumeratorState.VisitingNode,   "g", 1, (1, 2), (1, 2)),
              },
              ExpectedDepthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, (0, 0), (0, 0)),
                (TreenumeratorState.VisitingNode,   "a", 1, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "b", 0, (0, 1), (0, 1)),
                (TreenumeratorState.VisitingNode,   "b", 1, (0, 1), (0, 1)),
                (TreenumeratorState.SchedulingNode, "d", 0, (0, 2), (0, 2)),
                (TreenumeratorState.VisitingNode,   "d", 1, (0, 2), (0, 2)),
                (TreenumeratorState.VisitingNode,   "b", 2, (0, 1), (0, 1)),
                (TreenumeratorState.SchedulingNode, "e", 0, (1, 2), (1, 2)),
                (TreenumeratorState.VisitingNode,   "e", 1, (1, 2), (1, 2)),
                (TreenumeratorState.VisitingNode,   "b", 3, (0, 1), (0, 1)),
                (TreenumeratorState.VisitingNode,   "a", 2, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "c", 0, (1, 1), (1, 1)),
                (TreenumeratorState.VisitingNode,   "c", 1, (1, 1), (1, 1)),
                (TreenumeratorState.SchedulingNode, "f", 0, (0, 2), (0, 2)),
                (TreenumeratorState.VisitingNode,   "f", 1, (0, 2), (0, 2)),
                (TreenumeratorState.VisitingNode,   "c", 2, (1, 1), (1, 1)),
                (TreenumeratorState.SchedulingNode, "g", 0, (1, 2), (1, 2)),
                (TreenumeratorState.VisitingNode,   "g", 1, (1, 2), (1, 2)),
                (TreenumeratorState.VisitingNode,   "c", 3, (1, 1), (1, 1)),
                (TreenumeratorState.VisitingNode,   "a", 3, (0, 0), (0, 0)),
              }
            },
            // Skip nodes
            new TestScenario
            {
              SchedulingPredicate = visit => visit.Node == "c" ? SchedulingStrategy.SkipNode : SchedulingStrategy.TraverseSubtree,
              Description = "Skip level 1, sibling 1 node",
              ExpectedBreadthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, (0, 0), (0, 0)),
                (TreenumeratorState.VisitingNode,   "a", 1, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "b", 0, (0, 1), (0, 1)),
                (TreenumeratorState.VisitingNode,   "a", 2, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "c", 0, (1, 1), (1, 1)),
                (TreenumeratorState.SchedulingNode, "f", 0, (0, 2), (1, 1)),
                (TreenumeratorState.VisitingNode,   "a", 3, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "g", 0, (1, 2), (2, 1)),
                (TreenumeratorState.VisitingNode,   "a", 4, (0, 0), (0, 0)),
                (TreenumeratorState.VisitingNode,   "b", 1, (0, 1), (0, 1)),
                (TreenumeratorState.SchedulingNode, "d", 0, (0, 2), (0, 2)),
                (TreenumeratorState.VisitingNode,   "b", 2, (0, 1), (0, 1)),
                (TreenumeratorState.SchedulingNode, "e", 0, (1, 2), (1, 2)),
                (TreenumeratorState.VisitingNode,   "b", 3, (0, 1), (0, 1)),
                (TreenumeratorState.VisitingNode,   "f", 1, (0, 2), (1, 1)),
                (TreenumeratorState.VisitingNode,   "g", 1, (1, 2), (2, 1)),
                (TreenumeratorState.VisitingNode,   "d", 1, (0, 2), (0, 2)),
                (TreenumeratorState.VisitingNode,   "e", 1, (1, 2), (1, 2)),
              },
              ExpectedDepthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, (0, 0), (0, 0)),
                (TreenumeratorState.VisitingNode,   "a", 1, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "b", 0, (0, 1), (0, 1)),
                (TreenumeratorState.VisitingNode,   "b", 1, (0, 1), (0, 1)),
                (TreenumeratorState.SchedulingNode, "d", 0, (0, 2), (0, 2)),
                (TreenumeratorState.VisitingNode,   "d", 1, (0, 2), (0, 2)),
                (TreenumeratorState.VisitingNode,   "b", 2, (0, 1), (0, 1)),
                (TreenumeratorState.SchedulingNode, "e", 0, (1, 2), (1, 2)),
                (TreenumeratorState.VisitingNode,   "e", 1, (1, 2), (1, 2)),
                (TreenumeratorState.VisitingNode,   "b", 3, (0, 1), (0, 1)),
                (TreenumeratorState.VisitingNode,   "a", 2, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "c", 0, (1, 1), (1, 1)),
                (TreenumeratorState.SchedulingNode, "f", 0, (0, 2), (1, 1)),
                (TreenumeratorState.VisitingNode,   "f", 1, (0, 2), (1, 1)),
                (TreenumeratorState.VisitingNode,   "a", 3, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "g", 0, (1, 2), (2, 1)),
                (TreenumeratorState.VisitingNode,   "g", 1, (1, 2), (2, 1)),
                (TreenumeratorState.VisitingNode,   "a", 4, (0, 0), (0, 0)),
              }
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
              SchedulingPredicate = visit => SchedulingStrategy.TraverseSubtree,
              Description = "Traverse all",
              ExpectedBreadthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, (0, 0), (0, 0)),
                (TreenumeratorState.VisitingNode,   "a", 1, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "b", 0, (0, 1), (0, 1)),
                (TreenumeratorState.VisitingNode,   "a", 2, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "c", 0, (1, 1), (1, 1)),
                (TreenumeratorState.VisitingNode,   "a", 3, (0, 0), (0, 0)),
                (TreenumeratorState.VisitingNode,   "b", 1, (0, 1), (0, 1)),
                (TreenumeratorState.VisitingNode,   "c", 1, (1, 1), (1, 1)),
                (TreenumeratorState.SchedulingNode, "d", 0, (0, 2), (0, 2)),
                (TreenumeratorState.VisitingNode,   "c", 2, (1, 1), (1, 1)),
                (TreenumeratorState.SchedulingNode, "e", 0, (1, 2), (1, 2)),
                (TreenumeratorState.VisitingNode,   "c", 3, (1, 1), (1, 1)),
                (TreenumeratorState.VisitingNode,   "d", 1, (0, 2), (0, 2)),
                (TreenumeratorState.VisitingNode,   "e", 1, (1, 2), (1, 2)),
                (TreenumeratorState.SchedulingNode, "f", 0, (0, 3), (0, 3)),
                (TreenumeratorState.VisitingNode,   "e", 2, (1, 2), (1, 2)),
                (TreenumeratorState.SchedulingNode, "g", 0, (1, 3), (1, 3)),
                (TreenumeratorState.VisitingNode,   "e", 3, (1, 2), (1, 2)),
                (TreenumeratorState.VisitingNode,   "f", 1, (0, 3), (0, 3)),
                (TreenumeratorState.VisitingNode,   "g", 1, (1, 3), (1, 3)),
                (TreenumeratorState.SchedulingNode, "h", 0, (0, 4), (0, 4)),
                (TreenumeratorState.VisitingNode,   "g", 2, (1, 3), (1, 3)),
                (TreenumeratorState.SchedulingNode, "i", 0, (1, 4), (1, 4)),
                (TreenumeratorState.VisitingNode,   "g", 3, (1, 3), (1, 3)),
                (TreenumeratorState.VisitingNode,   "h", 1, (0, 4), (0, 4)),
                (TreenumeratorState.VisitingNode,   "i", 1, (1, 4), (1, 4)),
              },
              ExpectedDepthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, (0, 0), (0, 0)),
                (TreenumeratorState.VisitingNode,   "a", 1, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "b", 0, (0, 1), (0, 1)),
                (TreenumeratorState.VisitingNode,   "b", 1, (0, 1), (0, 1)),
                (TreenumeratorState.VisitingNode,   "a", 2, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "c", 0, (1, 1), (1, 1)),
                (TreenumeratorState.VisitingNode,   "c", 1, (1, 1), (1, 1)),
                (TreenumeratorState.SchedulingNode, "d", 0, (0, 2), (0, 2)),
                (TreenumeratorState.VisitingNode,   "d", 1, (0, 2), (0, 2)),
                (TreenumeratorState.VisitingNode,   "c", 2, (1, 1), (1, 1)),
                (TreenumeratorState.SchedulingNode, "e", 0, (1, 2), (1, 2)),
                (TreenumeratorState.VisitingNode,   "e", 1, (1, 2), (1, 2)),
                (TreenumeratorState.SchedulingNode, "f", 0, (0, 3), (0, 3)),
                (TreenumeratorState.VisitingNode,   "f", 1, (0, 3), (0, 3)),
                (TreenumeratorState.VisitingNode,   "e", 2, (1, 2), (1, 2)),
                (TreenumeratorState.SchedulingNode, "g", 0, (1, 3), (1, 3)),
                (TreenumeratorState.VisitingNode,   "g", 1, (1, 3), (1, 3)),
                (TreenumeratorState.SchedulingNode, "h", 0, (0, 4), (0, 4)),
                (TreenumeratorState.VisitingNode,   "h", 1, (0, 4), (0, 4)),
                (TreenumeratorState.VisitingNode,   "g", 2, (1, 3), (1, 3)),
                (TreenumeratorState.SchedulingNode, "i", 0, (1, 4), (1, 4)),
                (TreenumeratorState.VisitingNode,   "i", 1, (1, 4), (1, 4)),
                (TreenumeratorState.VisitingNode,   "g", 3, (1, 3), (1, 3)),
                (TreenumeratorState.VisitingNode,   "e", 3, (1, 2), (1, 2)),
                (TreenumeratorState.VisitingNode,   "c", 3, (1, 1), (1, 1)),
                (TreenumeratorState.VisitingNode,   "a", 3, (0, 0), (0, 0)),
              }
            },
            // Skip nodes
            new TestScenario
            {
              SchedulingPredicate = visit => SchedulingStrategy.SkipNode,
              Description = "Skip all nodes",
              ExpectedBreadthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "b", 0, (0, 1), (0, 0)),
                (TreenumeratorState.SchedulingNode, "c", 0, (1, 1), (0, 0)),
                (TreenumeratorState.SchedulingNode, "d", 0, (0, 2), (0, 0)),
                (TreenumeratorState.SchedulingNode, "e", 0, (1, 2), (0, 0)),
                (TreenumeratorState.SchedulingNode, "f", 0, (0, 3), (0, 0)),
                (TreenumeratorState.SchedulingNode, "g", 0, (1, 3), (0, 0)),
                (TreenumeratorState.SchedulingNode, "h", 0, (0, 4), (0, 0)),
                (TreenumeratorState.SchedulingNode, "i", 0, (1, 4), (0, 0)),
              },
              ExpectedDepthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "b", 0, (0, 1), (0, 0)),
                (TreenumeratorState.SchedulingNode, "c", 0, (1, 1), (0, 0)),
                (TreenumeratorState.SchedulingNode, "d", 0, (0, 2), (0, 0)),
                (TreenumeratorState.SchedulingNode, "e", 0, (1, 2), (0, 0)),
                (TreenumeratorState.SchedulingNode, "f", 0, (0, 3), (0, 0)),
                (TreenumeratorState.SchedulingNode, "g", 0, (1, 3), (0, 0)),
                (TreenumeratorState.SchedulingNode, "h", 0, (0, 4), (0, 0)),
                (TreenumeratorState.SchedulingNode, "i", 0, (1, 4), (0, 0)),
              }
            },
            new TestScenario
            {
              SchedulingPredicate =
                visit =>
                  (visit.OriginalPosition.Depth == 1 || visit.OriginalPosition.Depth == 3) && visit.OriginalPosition.SiblingIndex == 1
                  ? SchedulingStrategy.SkipNode
                  : SchedulingStrategy.TraverseSubtree,
              Description = "Skip levels 1 & 3, sibling 1 node",
              ExpectedBreadthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, (0, 0), (0, 0)),
                (TreenumeratorState.VisitingNode,   "a", 1, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "b", 0, (0, 1), (0, 1)),
                (TreenumeratorState.VisitingNode,   "a", 2, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "c", 0, (1, 1), (1, 1)),
                (TreenumeratorState.SchedulingNode, "d", 0, (0, 2), (1, 1)),
                (TreenumeratorState.VisitingNode,   "a", 3, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "e", 0, (1, 2), (2, 1)),
                (TreenumeratorState.VisitingNode,   "a", 4, (0, 0), (0, 0)),
                (TreenumeratorState.VisitingNode,   "b", 1, (0, 1), (0, 1)),
                (TreenumeratorState.VisitingNode,   "d", 1, (0, 2), (1, 1)),
                (TreenumeratorState.VisitingNode,   "e", 1, (1, 2), (2, 1)),
                (TreenumeratorState.SchedulingNode, "f", 0, (0, 3), (0, 2)),
                (TreenumeratorState.VisitingNode,   "e", 2, (1, 2), (2, 1)),
                (TreenumeratorState.SchedulingNode, "g", 0, (1, 3), (1, 2)),
                (TreenumeratorState.SchedulingNode, "h", 0, (0, 4), (1, 2)),
                (TreenumeratorState.VisitingNode,   "e", 3, (1, 2), (2, 1)),
                (TreenumeratorState.SchedulingNode, "i", 0, (1, 4), (2, 2)),
                (TreenumeratorState.VisitingNode,   "e", 4, (1, 2), (2, 1)),
                (TreenumeratorState.VisitingNode,   "f", 1, (0, 3), (0, 2)),
                (TreenumeratorState.VisitingNode,   "h", 1, (0, 4), (1, 2)),
                (TreenumeratorState.VisitingNode,   "i", 1, (1, 4), (2, 2)),
              },
              ExpectedDepthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, (0, 0), (0, 0)),
                (TreenumeratorState.VisitingNode,   "a", 1, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "b", 0, (0, 1), (0, 1)),
                (TreenumeratorState.VisitingNode,   "b", 1, (0, 1), (0, 1)),
                (TreenumeratorState.VisitingNode,   "a", 2, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "c", 0, (1, 1), (1, 1)),
                (TreenumeratorState.SchedulingNode, "d", 0, (0, 2), (1, 1)),
                (TreenumeratorState.VisitingNode,   "d", 1, (0, 2), (1, 1)),
                (TreenumeratorState.VisitingNode,   "a", 3, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "e", 0, (1, 2), (2, 1)),
                (TreenumeratorState.VisitingNode,   "e", 1, (1, 2), (2, 1)),
                (TreenumeratorState.SchedulingNode, "f", 0, (0, 3), (0, 2)),
                (TreenumeratorState.VisitingNode,   "f", 1, (0, 3), (0, 2)),
                (TreenumeratorState.VisitingNode,   "e", 2, (1, 2), (2, 1)),
                (TreenumeratorState.SchedulingNode, "g", 0, (1, 3), (1, 2)),
                (TreenumeratorState.SchedulingNode, "h", 0, (0, 4), (1, 2)),
                (TreenumeratorState.VisitingNode,   "h", 1, (0, 4), (1, 2)),
                (TreenumeratorState.VisitingNode,   "e", 3, (1, 2), (2, 1)),
                (TreenumeratorState.SchedulingNode, "i", 0, (1, 4), (2, 2)),
                (TreenumeratorState.VisitingNode,   "i", 1, (1, 4), (2, 2)),
                (TreenumeratorState.VisitingNode,   "e", 4, (1, 2), (2, 1)),
                (TreenumeratorState.VisitingNode,   "a", 4, (0, 0), (0, 0)),
              }
            },
            new TestScenario
            {
              SchedulingPredicate =
                visit =>
                  visit.OriginalPosition.Depth == 0 || visit.OriginalPosition.SiblingIndex == 1
                  ? SchedulingStrategy.SkipNode
                  : SchedulingStrategy.TraverseSubtree,
              Description = "Skip root and all sibling 1 nodes",
              ExpectedBreadthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "b", 0, (0, 1), (0, 0)),
                (TreenumeratorState.SchedulingNode, "c", 0, (1, 1), (1, 0)),
                (TreenumeratorState.SchedulingNode, "d", 0, (0, 2), (1, 0)),
                (TreenumeratorState.SchedulingNode, "e", 0, (1, 2), (2, 0)),
                (TreenumeratorState.SchedulingNode, "f", 0, (0, 3), (2, 0)),
                (TreenumeratorState.SchedulingNode, "g", 0, (1, 3), (3, 0)),
                (TreenumeratorState.SchedulingNode, "h", 0, (0, 4), (3, 0)),
                (TreenumeratorState.SchedulingNode, "i", 0, (1, 4), (4, 0)),
                (TreenumeratorState.VisitingNode,   "b", 1, (0, 1), (0, 0)),
                (TreenumeratorState.VisitingNode,   "d", 1, (0, 2), (1, 0)),
                (TreenumeratorState.VisitingNode,   "f", 1, (0, 3), (2, 0)),
                (TreenumeratorState.VisitingNode,   "h", 1, (0, 4), (3, 0)),
              },
              ExpectedDepthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "b", 0, (0, 1), (0, 0)),
                (TreenumeratorState.VisitingNode,   "b", 1, (0, 1), (0, 0)),
                (TreenumeratorState.SchedulingNode, "c", 0, (1, 1), (1, 0)),
                (TreenumeratorState.SchedulingNode, "d", 0, (0, 2), (1, 0)),
                (TreenumeratorState.VisitingNode,   "d", 1, (0, 2), (1, 0)),
                (TreenumeratorState.SchedulingNode, "e", 0, (1, 2), (2, 0)),
                (TreenumeratorState.SchedulingNode, "f", 0, (0, 3), (2, 0)),
                (TreenumeratorState.VisitingNode,   "f", 1, (0, 3), (2, 0)),
                (TreenumeratorState.SchedulingNode, "g", 0, (1, 3), (3, 0)),
                (TreenumeratorState.SchedulingNode, "h", 0, (0, 4), (3, 0)),
                (TreenumeratorState.VisitingNode,   "h", 1, (0, 4), (3, 0)),
                (TreenumeratorState.SchedulingNode, "i", 0, (1, 4), (4, 0)),
              }
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
              SchedulingPredicate = visit => SchedulingStrategy.TraverseSubtree,
              Description = "Traverse all",
              ExpectedBreadthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, (0, 0), (0, 0)),
                (TreenumeratorState.VisitingNode,   "a", 1, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "b", 0, (0, 1), (0, 1)),
                (TreenumeratorState.VisitingNode,   "a", 2, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "c", 0, (1, 1), (1, 1)),
                (TreenumeratorState.VisitingNode,   "a", 3, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "d", 0, (2, 1), (2, 1)),
                (TreenumeratorState.VisitingNode,   "a", 4, (0, 0), (0, 0)),
                (TreenumeratorState.VisitingNode,   "b", 1, (0, 1), (0, 1)),
                (TreenumeratorState.VisitingNode,   "c", 1, (1, 1), (1, 1)),
                (TreenumeratorState.SchedulingNode, "e", 0, (0, 2), (0, 2)),
                (TreenumeratorState.VisitingNode,   "c", 2, (1, 1), (1, 1)),
                (TreenumeratorState.SchedulingNode, "f", 0, (1, 2), (1, 2)),
                (TreenumeratorState.VisitingNode,   "c", 3, (1, 1), (1, 1)),
                (TreenumeratorState.SchedulingNode, "g", 0, (2, 2), (2, 2)),
                (TreenumeratorState.VisitingNode,   "c", 4, (1, 1), (1, 1)),
                (TreenumeratorState.VisitingNode,   "d", 1, (2, 1), (2, 1)),
                (TreenumeratorState.VisitingNode,   "e", 1, (0, 2), (0, 2)),
                (TreenumeratorState.VisitingNode,   "f", 1, (1, 2), (1, 2)),
                (TreenumeratorState.VisitingNode,   "g", 1, (2, 2), (2, 2)),
              },
              ExpectedDepthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, (0, 0), (0, 0)),
                (TreenumeratorState.VisitingNode,   "a", 1, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "b", 0, (0, 1), (0, 1)),
                (TreenumeratorState.VisitingNode,   "b", 1, (0, 1), (0, 1)),
                (TreenumeratorState.VisitingNode,   "a", 2, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "c", 0, (1, 1), (1, 1)),
                (TreenumeratorState.VisitingNode,   "c", 1, (1, 1), (1, 1)),
                (TreenumeratorState.SchedulingNode, "e", 0, (0, 2), (0, 2)),
                (TreenumeratorState.VisitingNode,   "e", 1, (0, 2), (0, 2)),
                (TreenumeratorState.VisitingNode,   "c", 2, (1, 1), (1, 1)),
                (TreenumeratorState.SchedulingNode, "f", 0, (1, 2), (1, 2)),
                (TreenumeratorState.VisitingNode,   "f", 1, (1, 2), (1, 2)),
                (TreenumeratorState.VisitingNode,   "c", 3, (1, 1), (1, 1)),
                (TreenumeratorState.SchedulingNode, "g", 0, (2, 2), (2, 2)),
                (TreenumeratorState.VisitingNode,   "g", 1, (2, 2), (2, 2)),
                (TreenumeratorState.VisitingNode,   "c", 4, (1, 1), (1, 1)),
                (TreenumeratorState.VisitingNode,   "a", 3, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "d", 0, (2, 1), (2, 1)),
                (TreenumeratorState.VisitingNode,   "d", 1, (2, 1), (2, 1)),
                (TreenumeratorState.VisitingNode,   "a", 4, (0, 0), (0, 0)),
              }
            },
            // Skip nodes
            new TestScenario
            {
              SchedulingPredicate = visit => SchedulingStrategy.SkipNode,
              Description = "Skip all nodes",
              ExpectedBreadthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "b", 0, (0, 1), (0, 0)),
                (TreenumeratorState.SchedulingNode, "c", 0, (1, 1), (0, 0)),
                (TreenumeratorState.SchedulingNode, "e", 0, (0, 2), (0, 0)),
                (TreenumeratorState.SchedulingNode, "f", 0, (1, 2), (0, 0)),
                (TreenumeratorState.SchedulingNode, "g", 0, (2, 2), (0, 0)),
                (TreenumeratorState.SchedulingNode, "d", 0, (2, 1), (0, 0)),
              },
              ExpectedDepthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "b", 0, (0, 1), (0, 0)),
                (TreenumeratorState.SchedulingNode, "c", 0, (1, 1), (0, 0)),
                (TreenumeratorState.SchedulingNode, "e", 0, (0, 2), (0, 0)),
                (TreenumeratorState.SchedulingNode, "f", 0, (1, 2), (0, 0)),
                (TreenumeratorState.SchedulingNode, "g", 0, (2, 2), (0, 0)),
                (TreenumeratorState.SchedulingNode, "d", 0, (2, 1), (0, 0)),
              }
            },
            new TestScenario
            {
              SchedulingPredicate = visit => visit.OriginalPosition.Depth == 1 && visit.OriginalPosition.SiblingIndex == 1 ? SchedulingStrategy.SkipNode : SchedulingStrategy.TraverseSubtree,
              Description = "Skip level 1, sibling 1 node",
              ExpectedBreadthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, (0, 0), (0, 0)),
                (TreenumeratorState.VisitingNode,   "a", 1, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "b", 0, (0, 1), (0, 1)),
                (TreenumeratorState.VisitingNode,   "a", 2, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "c", 0, (1, 1), (1, 1)),
                (TreenumeratorState.SchedulingNode, "e", 0, (0, 2), (1, 1)),
                (TreenumeratorState.VisitingNode,   "a", 3, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "f", 0, (1, 2), (2, 1)),
                (TreenumeratorState.VisitingNode,   "a", 4, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "g", 0, (2, 2), (3, 1)),
                (TreenumeratorState.VisitingNode,   "a", 5, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "d", 0, (2, 1), (4, 1)),
                (TreenumeratorState.VisitingNode,   "a", 6, (0, 0), (0, 0)),
                (TreenumeratorState.VisitingNode,   "b", 1, (0, 1), (0, 1)),
                (TreenumeratorState.VisitingNode,   "e", 1, (0, 2), (1, 1)),
                (TreenumeratorState.VisitingNode,   "f", 1, (1, 2), (2, 1)),
                (TreenumeratorState.VisitingNode,   "g", 1, (2, 2), (3, 1)),
                (TreenumeratorState.VisitingNode,   "d", 1, (2, 1), (4, 1)),
              },
              ExpectedDepthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, (0, 0), (0, 0)),
                (TreenumeratorState.VisitingNode,   "a", 1, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "b", 0, (0, 1), (0, 1)),
                (TreenumeratorState.VisitingNode,   "b", 1, (0, 1), (0, 1)),
                (TreenumeratorState.VisitingNode,   "a", 2, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "c", 0, (1, 1), (1, 1)),
                (TreenumeratorState.SchedulingNode, "e", 0, (0, 2), (1, 1)),
                (TreenumeratorState.VisitingNode,   "e", 1, (0, 2), (1, 1)),
                (TreenumeratorState.VisitingNode,   "a", 3, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "f", 0, (1, 2), (2, 1)),
                (TreenumeratorState.VisitingNode,   "f", 1, (1, 2), (2, 1)),
                (TreenumeratorState.VisitingNode,   "a", 4, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "g", 0, (2, 2), (3, 1)),
                (TreenumeratorState.VisitingNode,   "g", 1, (2, 2), (3, 1)),
                (TreenumeratorState.VisitingNode,   "a", 5, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "d", 0, (2, 1), (4, 1)),
                (TreenumeratorState.VisitingNode,   "d", 1, (2, 1), (4, 1)),
                (TreenumeratorState.VisitingNode,   "a", 6, (0, 0), (0, 0)),
              }
            },
            // Skip subtree
            new TestScenario
            {
              SchedulingPredicate = visit => visit.OriginalPosition.Depth == 1 && visit.OriginalPosition.SiblingIndex == 1 ? SchedulingStrategy.SkipSubtree : SchedulingStrategy.TraverseSubtree,
              Description = "Skip level 1, sibling 1 subtree",
              ExpectedBreadthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, (0, 0), (0, 0)),
                (TreenumeratorState.VisitingNode,   "a", 1, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "b", 0, (0, 1), (0, 1)),
                (TreenumeratorState.VisitingNode,   "a", 2, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "c", 0, (1, 1), (1, 1)),
                (TreenumeratorState.SchedulingNode, "d", 0, (2, 1), (1, 1)),
                (TreenumeratorState.VisitingNode,   "a", 3, (0, 0), (0, 0)),
                (TreenumeratorState.VisitingNode,   "b", 1, (0, 1), (0, 1)),
                (TreenumeratorState.VisitingNode,   "d", 1, (2, 1), (1, 1)),
              },
              ExpectedDepthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, (0, 0), (0, 0)),
                (TreenumeratorState.VisitingNode,   "a", 1, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "b", 0, (0, 1), (0, 1)),
                (TreenumeratorState.VisitingNode,   "b", 1, (0, 1), (0, 1)),
                (TreenumeratorState.VisitingNode,   "a", 2, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "c", 0, (1, 1), (1, 1)),
                (TreenumeratorState.SchedulingNode, "d", 0, (2, 1), (1, 1)),
                (TreenumeratorState.VisitingNode,   "d", 1, (2, 1), (1, 1)),
                (TreenumeratorState.VisitingNode,   "a", 3, (0, 0), (0, 0)),
              }
            },
            // Skip descendant subtrees
            new TestScenario
            {
              SchedulingPredicate = visit => visit.OriginalPosition.Depth == 1 && visit.OriginalPosition.SiblingIndex == 1 ? SchedulingStrategy.SkipDescendants : SchedulingStrategy.TraverseSubtree,
              Description = "Skip level 1, sibling 1 descendant subtree",
              ExpectedBreadthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, (0, 0), (0, 0)),
                (TreenumeratorState.VisitingNode,   "a", 1, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "b", 0, (0, 1), (0, 1)),
                (TreenumeratorState.VisitingNode,   "a", 2, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "c", 0, (1, 1), (1, 1)),
                (TreenumeratorState.VisitingNode,   "a", 3, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "d", 0, (2, 1), (2, 1)),
                (TreenumeratorState.VisitingNode,   "a", 4, (0, 0), (0, 0)),
                (TreenumeratorState.VisitingNode,   "b", 1, (0, 1), (0, 1)),
                (TreenumeratorState.VisitingNode,   "c", 1, (1, 1), (1, 1)),
                (TreenumeratorState.VisitingNode,   "d", 1, (2, 1), (2, 1)),
              },
              ExpectedDepthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, (0, 0), (0, 0)),
                (TreenumeratorState.VisitingNode,   "a", 1, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "b", 0, (0, 1), (0, 1)),
                (TreenumeratorState.VisitingNode,   "b", 1, (0, 1), (0, 1)),
                (TreenumeratorState.VisitingNode,   "a", 2, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "c", 0, (1, 1), (1, 1)),
                (TreenumeratorState.VisitingNode,   "c", 1, (1, 1), (1, 1)),
                (TreenumeratorState.VisitingNode,   "a", 3, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "d", 0, (2, 1), (2, 1)),
                (TreenumeratorState.VisitingNode,   "d", 1, (2, 1), (2, 1)),
                (TreenumeratorState.VisitingNode,   "a", 4, (0, 0), (0, 0)),
              }
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
              SchedulingPredicate = visit => SchedulingStrategy.TraverseSubtree,
              Description = "Traverse all",
              ExpectedBreadthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "d", 0, (1, 0), (1, 0)),
                (TreenumeratorState.VisitingNode,   "a", 1, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "b", 0, (0, 1), (0, 1)),
                (TreenumeratorState.VisitingNode,   "a", 2, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "c", 0, (1, 1), (1, 1)),
                (TreenumeratorState.VisitingNode,   "a", 3, (0, 0), (0, 0)),
                (TreenumeratorState.VisitingNode,   "d", 1, (1, 0), (1, 0)),
                (TreenumeratorState.SchedulingNode, "e", 0, (0, 1), (0, 1)),
                (TreenumeratorState.VisitingNode,   "d", 2, (1, 0), (1, 0)),
                (TreenumeratorState.SchedulingNode, "f", 0, (1, 1), (1, 1)),
                (TreenumeratorState.VisitingNode,   "d", 3, (1, 0), (1, 0)),
                (TreenumeratorState.VisitingNode,   "b", 1, (0, 1), (0, 1)),
                (TreenumeratorState.VisitingNode,   "c", 1, (1, 1), (1, 1)),
                (TreenumeratorState.VisitingNode,   "e", 1, (0, 1), (0, 1)),
                (TreenumeratorState.VisitingNode,   "f", 1, (1, 1), (1, 1)),
              },
              ExpectedDepthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, (0, 0), (0, 0)),
                (TreenumeratorState.VisitingNode,   "a", 1, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "b", 0, (0, 1), (0, 1)),
                (TreenumeratorState.VisitingNode,   "b", 1, (0, 1), (0, 1)),
                (TreenumeratorState.VisitingNode,   "a", 2, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "c", 0, (1, 1), (1, 1)),
                (TreenumeratorState.VisitingNode,   "c", 1, (1, 1), (1, 1)),
                (TreenumeratorState.VisitingNode,   "a", 3, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "d", 0, (1, 0), (1, 0)),
                (TreenumeratorState.VisitingNode,   "d", 1, (1, 0), (1, 0)),
                (TreenumeratorState.SchedulingNode, "e", 0, (0, 1), (0, 1)),
                (TreenumeratorState.VisitingNode,   "e", 1, (0, 1), (0, 1)),
                (TreenumeratorState.VisitingNode,   "d", 2, (1, 0), (1, 0)),
                (TreenumeratorState.SchedulingNode, "f", 0, (1, 1), (1, 1)),
                (TreenumeratorState.VisitingNode,   "f", 1, (1, 1), (1, 1)),
                (TreenumeratorState.VisitingNode,   "d", 3, (1, 0), (1, 0)),
              }
            },
            // Skip nodes
            new TestScenario
            {
              SchedulingPredicate = visit => SchedulingStrategy.SkipNode,
              Description = "Skip all nodes",
              ExpectedBreadthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "b", 0, (0, 1), (0, 0)),
                (TreenumeratorState.SchedulingNode, "c", 0, (1, 1), (0, 0)),
                (TreenumeratorState.SchedulingNode, "d", 0, (1, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "e", 0, (0, 1), (0, 0)),
                (TreenumeratorState.SchedulingNode, "f", 0, (1, 1), (0, 0)),
              },
              ExpectedDepthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "b", 0, (0, 1), (0, 0)),
                (TreenumeratorState.SchedulingNode, "c", 0, (1, 1), (0, 0)),
                (TreenumeratorState.SchedulingNode, "d", 0, (1, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "e", 0, (0, 1), (0, 0)),
                (TreenumeratorState.SchedulingNode, "f", 0, (1, 1), (0, 0)),
              }
            },
            new TestScenario
            {
              SchedulingPredicate = visit => visit.Node == "d" ? SchedulingStrategy.SkipNode : SchedulingStrategy.TraverseSubtree,
              Description = "Skip level 0, sibling 1 node",
              ExpectedBreadthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "d", 0, (1, 0), (1, 0)),
                (TreenumeratorState.SchedulingNode, "e", 0, (0, 1), (1, 0)),
                (TreenumeratorState.SchedulingNode, "f", 0, (1, 1), (2, 0)),
                (TreenumeratorState.VisitingNode,   "a", 1, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "b", 0, (0, 1), (0, 1)),
                (TreenumeratorState.VisitingNode,   "a", 2, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "c", 0, (1, 1), (1, 1)),
                (TreenumeratorState.VisitingNode,   "a", 3, (0, 0), (0, 0)),
                (TreenumeratorState.VisitingNode,   "e", 1, (0, 1), (1, 0)),
                (TreenumeratorState.VisitingNode,   "f", 1, (1, 1), (2, 0)),
                (TreenumeratorState.VisitingNode,   "b", 1, (0, 1), (0, 1)),
                (TreenumeratorState.VisitingNode,   "c", 1, (1, 1), (1, 1)),
              },
              ExpectedDepthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, (0, 0), (0, 0)),
                (TreenumeratorState.VisitingNode,   "a", 1, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "b", 0, (0, 1), (0, 1)),
                (TreenumeratorState.VisitingNode,   "b", 1, (0, 1), (0, 1)),
                (TreenumeratorState.VisitingNode,   "a", 2, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "c", 0, (1, 1), (1, 1)),
                (TreenumeratorState.VisitingNode,   "c", 1, (1, 1), (1, 1)),
                (TreenumeratorState.VisitingNode,   "a", 3, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "d", 0, (1, 0), (1, 0)),
                (TreenumeratorState.SchedulingNode, "e", 0, (0, 1), (1, 0)),
                (TreenumeratorState.VisitingNode,   "e", 1, (0, 1), (1, 0)),
                (TreenumeratorState.SchedulingNode, "f", 0, (1, 1), (2, 0)),
                (TreenumeratorState.VisitingNode,   "f", 1, (1, 1), (2, 0)),
              }
            },
            new TestScenario
            {
              SchedulingPredicate = visit => visit.Node == "c" ? SchedulingStrategy.SkipNode : SchedulingStrategy.TraverseSubtree,
              Description = "Skip level 1, sibling 1 node",
              ExpectedBreadthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "d", 0, (1, 0), (1, 0)),
                (TreenumeratorState.VisitingNode,   "a", 1, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "b", 0, (0, 1), (0, 1)),
                (TreenumeratorState.VisitingNode,   "a", 2, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "c", 0, (1, 1), (1, 1)),
                (TreenumeratorState.VisitingNode,   "d", 1, (1, 0), (1, 0)),
                (TreenumeratorState.SchedulingNode, "e", 0, (0, 1), (0, 1)),
                (TreenumeratorState.VisitingNode,   "d", 2, (1, 0), (1, 0)),
                (TreenumeratorState.SchedulingNode, "f", 0, (1, 1), (1, 1)),
                (TreenumeratorState.VisitingNode,   "d", 3, (1, 0), (1, 0)),
                (TreenumeratorState.VisitingNode,   "b", 1, (0, 1), (0, 1)),
                (TreenumeratorState.VisitingNode,   "e", 1, (0, 1), (0, 1)),
                (TreenumeratorState.VisitingNode,   "f", 1, (1, 1), (1, 1)),
              },
              ExpectedDepthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, (0, 0), (0, 0)),
                (TreenumeratorState.VisitingNode,   "a", 1, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "b", 0, (0, 1), (0, 1)),
                (TreenumeratorState.VisitingNode,   "b", 1, (0, 1), (0, 1)),
                (TreenumeratorState.VisitingNode,   "a", 2, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "c", 0, (1, 1), (1, 1)),
                (TreenumeratorState.SchedulingNode, "d", 0, (1, 0), (1, 0)),
                (TreenumeratorState.VisitingNode,   "d", 1, (1, 0), (1, 0)),
                (TreenumeratorState.SchedulingNode, "e", 0, (0, 1), (0, 1)),
                (TreenumeratorState.VisitingNode,   "e", 1, (0, 1), (0, 1)),
                (TreenumeratorState.VisitingNode,   "d", 2, (1, 0), (1, 0)),
                (TreenumeratorState.SchedulingNode, "f", 0, (1, 1), (1, 1)),
                (TreenumeratorState.VisitingNode,   "f", 1, (1, 1), (1, 1)),
                (TreenumeratorState.VisitingNode,   "d", 3, (1, 0), (1, 0)),
              }
            },
            // Skip subtree
            new TestScenario
            {
              SchedulingPredicate = visit => visit.Node == "d" ? SchedulingStrategy.SkipSubtree : SchedulingStrategy.TraverseSubtree,
              Description = "Skip level 0, sibling 1 subtree",
              ExpectedBreadthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "d", 0, (1, 0), (1, 0)),
                (TreenumeratorState.VisitingNode,   "a", 1, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "b", 0, (0, 1), (0, 1)),
                (TreenumeratorState.VisitingNode,   "a", 2, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "c", 0, (1, 1), (1, 1)),
                (TreenumeratorState.VisitingNode,   "a", 3, (0, 0), (0, 0)),
                (TreenumeratorState.VisitingNode,   "b", 1, (0, 1), (0, 1)),
                (TreenumeratorState.VisitingNode,   "c", 1, (1, 1), (1, 1)),
              },
              ExpectedDepthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, (0, 0), (0, 0)),
                (TreenumeratorState.VisitingNode,   "a", 1, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "b", 0, (0, 1), (0, 1)),
                (TreenumeratorState.VisitingNode,   "b", 1, (0, 1), (0, 1)),
                (TreenumeratorState.VisitingNode,   "a", 2, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "c", 0, (1, 1), (1, 1)),
                (TreenumeratorState.VisitingNode,   "c", 1, (1, 1), (1, 1)),
                (TreenumeratorState.VisitingNode,   "a", 3, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "d", 0, (1, 0), (1, 0)),
              }
            },
            new TestScenario
            {
              SchedulingPredicate = visit => visit.Node == "c" ? SchedulingStrategy.SkipSubtree : SchedulingStrategy.TraverseSubtree,
              Description = "Skip level 1, sibling 1 subtree",
              ExpectedBreadthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "d", 0, (1, 0), (1, 0)),
                (TreenumeratorState.VisitingNode,   "a", 1, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "b", 0, (0, 1), (0, 1)),
                (TreenumeratorState.VisitingNode,   "a", 2, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "c", 0, (1, 1), (1, 1)),
                (TreenumeratorState.VisitingNode,   "d", 1, (1, 0), (1, 0)),
                (TreenumeratorState.SchedulingNode, "e", 0, (0, 1), (0, 1)),
                (TreenumeratorState.VisitingNode,   "d", 2, (1, 0), (1, 0)),
                (TreenumeratorState.SchedulingNode, "f", 0, (1, 1), (1, 1)),
                (TreenumeratorState.VisitingNode,   "d", 3, (1, 0), (1, 0)),
                (TreenumeratorState.VisitingNode,   "b", 1, (0, 1), (0, 1)),
                (TreenumeratorState.VisitingNode,   "e", 1, (0, 1), (0, 1)),
                (TreenumeratorState.VisitingNode,   "f", 1, (1, 1), (1, 1)),
              },
              ExpectedDepthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, (0, 0), (0, 0)),
                (TreenumeratorState.VisitingNode,   "a", 1, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "b", 0, (0, 1), (0, 1)),
                (TreenumeratorState.VisitingNode,   "b", 1, (0, 1), (0, 1)),
                (TreenumeratorState.VisitingNode,   "a", 2, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "c", 0, (1, 1), (1, 1)),
                (TreenumeratorState.SchedulingNode, "d", 0, (1, 0), (1, 0)),
                (TreenumeratorState.VisitingNode,   "d", 1, (1, 0), (1, 0)),
                (TreenumeratorState.SchedulingNode, "e", 0, (0, 1), (0, 1)),
                (TreenumeratorState.VisitingNode,   "e", 1, (0, 1), (0, 1)),
                (TreenumeratorState.VisitingNode,   "d", 2, (1, 0), (1, 0)),
                (TreenumeratorState.SchedulingNode, "f", 0, (1, 1), (1, 1)),
                (TreenumeratorState.VisitingNode,   "f", 1, (1, 1), (1, 1)),
                (TreenumeratorState.VisitingNode,   "d", 3, (1, 0), (1, 0)),
              }
            },
            // Skip descendant subtrees
            new TestScenario
            {
              SchedulingPredicate = visit => visit.Node == "d" ? SchedulingStrategy.SkipDescendants : SchedulingStrategy.TraverseSubtree,
              Description = "Skip level 0, sibling 1 descendant subtrees",
              ExpectedBreadthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "d", 0, (1, 0), (1, 0)),
                (TreenumeratorState.VisitingNode,   "a", 1, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "b", 0, (0, 1), (0, 1)),
                (TreenumeratorState.VisitingNode,   "a", 2, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "c", 0, (1, 1), (1, 1)),
                (TreenumeratorState.VisitingNode,   "a", 3, (0, 0), (0, 0)),
                (TreenumeratorState.VisitingNode,   "d", 1, (1, 0), (1, 0)),
                (TreenumeratorState.VisitingNode,   "b", 1, (0, 1), (0, 1)),
                (TreenumeratorState.VisitingNode,   "c", 1, (1, 1), (1, 1)),
              },
              ExpectedDepthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, (0, 0), (0, 0)),
                (TreenumeratorState.VisitingNode,   "a", 1, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "b", 0, (0, 1), (0, 1)),
                (TreenumeratorState.VisitingNode,   "b", 1, (0, 1), (0, 1)),
                (TreenumeratorState.VisitingNode,   "a", 2, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "c", 0, (1, 1), (1, 1)),
                (TreenumeratorState.VisitingNode,   "c", 1, (1, 1), (1, 1)),
                (TreenumeratorState.VisitingNode,   "a", 3, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "d", 0, (1, 0), (1, 0)),
                (TreenumeratorState.VisitingNode,   "d", 1, (1, 0), (1, 0)),
              }
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
              SchedulingPredicate = visit => SchedulingStrategy.TraverseSubtree,
              Description = "Traverse all",
              ExpectedBreadthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, (0, 0), (0, 0)),
                (TreenumeratorState.VisitingNode,   "a", 1, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "b", 0, (0, 1), (0, 1)),
                (TreenumeratorState.VisitingNode,   "a", 2, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "c", 0, (1, 1), (1, 1)),
                (TreenumeratorState.VisitingNode,   "a", 3, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "d", 0, (2, 1), (2, 1)),
                (TreenumeratorState.VisitingNode,   "a", 4, (0, 0), (0, 0)),
                (TreenumeratorState.VisitingNode,   "b", 1, (0, 1), (0, 1)),
                (TreenumeratorState.VisitingNode,   "c", 1, (1, 1), (1, 1)),
                (TreenumeratorState.VisitingNode,   "d", 1, (2, 1), (2, 1)),
              },
              ExpectedDepthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, (0, 0), (0, 0)),
                (TreenumeratorState.VisitingNode,   "a", 1, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "b", 0, (0, 1), (0, 1)),
                (TreenumeratorState.VisitingNode,   "b", 1, (0, 1), (0, 1)),
                (TreenumeratorState.VisitingNode,   "a", 2, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "c", 0, (1, 1), (1, 1)),
                (TreenumeratorState.VisitingNode,   "c", 1, (1, 1), (1, 1)),
                (TreenumeratorState.VisitingNode,   "a", 3, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "d", 0, (2, 1), (2, 1)),
                (TreenumeratorState.VisitingNode,   "d", 1, (2, 1), (2, 1)),
                (TreenumeratorState.VisitingNode,   "a", 4, (0, 0), (0, 0)),
              }
            },
            // Skip nodes
            new TestScenario
            {
              SchedulingPredicate = visit => SchedulingStrategy.SkipNode,
              Description = "Skip all nodes",
              ExpectedBreadthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "b", 0, (0, 1), (0, 0)),
                (TreenumeratorState.SchedulingNode, "c", 0, (1, 1), (0, 0)),
                (TreenumeratorState.SchedulingNode, "d", 0, (2, 1), (0, 0)),
              },
              ExpectedDepthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "b", 0, (0, 1), (0, 0)),
                (TreenumeratorState.SchedulingNode, "c", 0, (1, 1), (0, 0)),
                (TreenumeratorState.SchedulingNode, "d", 0, (2, 1), (0, 0)),
              }
            },
            new TestScenario
            {
              SchedulingPredicate = visit => visit.OriginalPosition.Depth == 0 ? SchedulingStrategy.SkipNode : SchedulingStrategy.TraverseSubtree,
              Description = "Skip level 0 nodes",
              ExpectedBreadthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "b", 0, (0, 1), (0, 0)),
                (TreenumeratorState.SchedulingNode, "c", 0, (1, 1), (1, 0)),
                (TreenumeratorState.SchedulingNode, "d", 0, (2, 1), (2, 0)),
                (TreenumeratorState.VisitingNode,   "b", 1, (0, 1), (0, 0)),
                (TreenumeratorState.VisitingNode,   "c", 1, (1, 1), (1, 0)),
                (TreenumeratorState.VisitingNode,   "d", 1, (2, 1), (2, 0)),
              },
              ExpectedDepthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "b", 0, (0, 1), (0, 0)),
                (TreenumeratorState.VisitingNode,   "b", 1, (0, 1), (0, 0)),
                (TreenumeratorState.SchedulingNode, "c", 0, (1, 1), (1, 0)),
                (TreenumeratorState.VisitingNode,   "c", 1, (1, 1), (1, 0)),
                (TreenumeratorState.SchedulingNode, "d", 0, (2, 1), (2, 0)),
                (TreenumeratorState.VisitingNode,   "d", 1, (2, 1), (2, 0)),
              }
            },
            new TestScenario
            {
              SchedulingPredicate = visit => visit.OriginalPosition.Depth == 1 ? SchedulingStrategy.SkipNode : SchedulingStrategy.TraverseSubtree,
              Description = "Skip level 1 nodes",
              ExpectedBreadthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, (0, 0), (0, 0)),
                (TreenumeratorState.VisitingNode,   "a", 1, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "b", 0, (0, 1), (0, 1)),
                (TreenumeratorState.SchedulingNode, "c", 0, (1, 1), (0, 1)),
                (TreenumeratorState.SchedulingNode, "d", 0, (2, 1), (0, 1)),
                (TreenumeratorState.VisitingNode,   "a", 2, (0, 0), (0, 0)),
              },
              ExpectedDepthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, (0, 0), (0, 0)),
                (TreenumeratorState.VisitingNode,   "a", 1, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "b", 0, (0, 1), (0, 1)),
                (TreenumeratorState.SchedulingNode, "c", 0, (1, 1), (0, 1)),
                (TreenumeratorState.SchedulingNode, "d", 0, (2, 1), (0, 1)),
                (TreenumeratorState.VisitingNode,   "a", 2, (0, 0), (0, 0)),
              }
            },
            new TestScenario
            {
              SchedulingPredicate = visit => visit.Node == "b" ? SchedulingStrategy.SkipNode : SchedulingStrategy.TraverseSubtree,
              Description = "Skip level 1, sibling 0 node",
              ExpectedBreadthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, (0, 0), (0, 0)),
                (TreenumeratorState.VisitingNode,   "a", 1, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "b", 0, (0, 1), (0, 1)),
                (TreenumeratorState.SchedulingNode, "c", 0, (1, 1), (0, 1)),
                (TreenumeratorState.VisitingNode,   "a", 2, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "d", 0, (2, 1), (1, 1)),
                (TreenumeratorState.VisitingNode,   "a", 3, (0, 0), (0, 0)),
                (TreenumeratorState.VisitingNode,   "c", 1, (1, 1), (0, 1)),
                (TreenumeratorState.VisitingNode,   "d", 1, (2, 1), (1, 1)),
              },
              ExpectedDepthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, (0, 0), (0, 0)),
                (TreenumeratorState.VisitingNode,   "a", 1, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "b", 0, (0, 1), (0, 1)),
                (TreenumeratorState.SchedulingNode, "c", 0, (1, 1), (0, 1)),
                (TreenumeratorState.VisitingNode,   "c", 1, (1, 1), (0, 1)),
                (TreenumeratorState.VisitingNode,   "a", 2, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "d", 0, (2, 1), (1, 1)),
                (TreenumeratorState.VisitingNode,   "d", 1, (2, 1), (1, 1)),
                (TreenumeratorState.VisitingNode,   "a", 3, (0, 0), (0, 0)),
              }
            },
            new TestScenario
            {
              SchedulingPredicate = visit => visit.Node == "c" ? SchedulingStrategy.SkipNode : SchedulingStrategy.TraverseSubtree,
              Description = "Skip level 1, sibling 1 node",
              ExpectedBreadthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, (0, 0), (0, 0)),
                (TreenumeratorState.VisitingNode,   "a", 1, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "b", 0, (0, 1), (0, 1)),
                (TreenumeratorState.VisitingNode,   "a", 2, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "c", 0, (1, 1), (1, 1)),
                (TreenumeratorState.SchedulingNode, "d", 0, (2, 1), (1, 1)),
                (TreenumeratorState.VisitingNode,   "a", 3, (0, 0), (0, 0)),
                (TreenumeratorState.VisitingNode,   "b", 1, (0, 1), (0, 1)),
                (TreenumeratorState.VisitingNode,   "d", 1, (2, 1), (1, 1)),
              },
              ExpectedDepthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, (0, 0), (0, 0)),
                (TreenumeratorState.VisitingNode,   "a", 1, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "b", 0, (0, 1), (0, 1)),
                (TreenumeratorState.VisitingNode,   "b", 1, (0, 1), (0, 1)),
                (TreenumeratorState.VisitingNode,   "a", 2, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "c", 0, (1, 1), (1, 1)),
                (TreenumeratorState.SchedulingNode, "d", 0, (2, 1), (1, 1)),
                (TreenumeratorState.VisitingNode,   "d", 1, (2, 1), (1, 1)),
                (TreenumeratorState.VisitingNode,   "a", 3, (0, 0), (0, 0)),
              }
            },
            new TestScenario
            {
              SchedulingPredicate = visit => visit.Node == "d" ? SchedulingStrategy.SkipNode : SchedulingStrategy.TraverseSubtree,
              Description = "Skip level 1, sibling 2 node",
              ExpectedBreadthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, (0, 0), (0, 0)),
                (TreenumeratorState.VisitingNode,   "a", 1, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "b", 0, (0, 1), (0, 1)),
                (TreenumeratorState.VisitingNode,   "a", 2, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "c", 0, (1, 1), (1, 1)),
                (TreenumeratorState.VisitingNode,   "a", 3, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "d", 0, (2, 1), (2, 1)),
                (TreenumeratorState.VisitingNode,   "b", 1, (0, 1), (0, 1)),
                (TreenumeratorState.VisitingNode,   "c", 1, (1, 1), (1, 1)),
              },
              ExpectedDepthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, (0, 0), (0, 0)),
                (TreenumeratorState.VisitingNode,   "a", 1, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "b", 0, (0, 1), (0, 1)),
                (TreenumeratorState.VisitingNode,   "b", 1, (0, 1), (0, 1)),
                (TreenumeratorState.VisitingNode,   "a", 2, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "c", 0, (1, 1), (1, 1)),
                (TreenumeratorState.VisitingNode,   "c", 1, (1, 1), (1, 1)),
                (TreenumeratorState.VisitingNode,   "a", 3, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "d", 0, (2, 1), (2, 1)),
              }
            },
            // Skip subtree
            new TestScenario
            {
              SchedulingPredicate = visit => visit.OriginalPosition.Depth == 0 ? SchedulingStrategy.SkipSubtree : SchedulingStrategy.TraverseSubtree,
              Description = "Skip level 0 subtrees",
              ExpectedBreadthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, (0, 0), (0, 0)),
              },
              ExpectedDepthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, (0, 0), (0, 0)),
              }
            },
            new TestScenario
            {
              SchedulingPredicate = visit => visit.OriginalPosition.Depth == 1 ? SchedulingStrategy.SkipSubtree : SchedulingStrategy.TraverseSubtree,
              Description = "Skip level 1 subtree",
              ExpectedBreadthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, (0, 0), (0, 0)),
                (TreenumeratorState.VisitingNode,   "a", 1, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "b", 0, (0, 1), (0, 1)),
                (TreenumeratorState.SchedulingNode, "c", 0, (1, 1), (0, 1)),
                (TreenumeratorState.SchedulingNode, "d", 0, (2, 1), (0, 1)),
                (TreenumeratorState.VisitingNode,   "a", 2, (0, 0), (0, 0)),
              },
              ExpectedDepthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, (0, 0), (0, 0)),
                (TreenumeratorState.VisitingNode,   "a", 1, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "b", 0, (0, 1), (0, 1)),
                (TreenumeratorState.SchedulingNode, "c", 0, (1, 1), (0, 1)),
                (TreenumeratorState.SchedulingNode, "d", 0, (2, 1), (0, 1)),
                (TreenumeratorState.VisitingNode,   "a", 2, (0, 0), (0, 0)),
              }
            },
            new TestScenario
            {
              SchedulingPredicate = visit => visit.Node == "b" ? SchedulingStrategy.SkipSubtree : SchedulingStrategy.TraverseSubtree,
              Description = "Skip level 1, sibling 0 subtree",
              ExpectedBreadthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, (0, 0), (0, 0)),
                (TreenumeratorState.VisitingNode,   "a", 1, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "b", 0, (0, 1), (0, 1)),
                (TreenumeratorState.SchedulingNode, "c", 0, (1, 1), (0, 1)),
                (TreenumeratorState.VisitingNode,   "a", 2, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "d", 0, (2, 1), (1, 1)),
                (TreenumeratorState.VisitingNode,   "a", 3, (0, 0), (0, 0)),
                (TreenumeratorState.VisitingNode,   "c", 1, (1, 1), (0, 1)),
                (TreenumeratorState.VisitingNode,   "d", 1, (2, 1), (1, 1)),
              },
              ExpectedDepthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, (0, 0), (0, 0)),
                (TreenumeratorState.VisitingNode,   "a", 1, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "b", 0, (0, 1), (0, 1)),
                (TreenumeratorState.SchedulingNode, "c", 0, (1, 1), (0, 1)),
                (TreenumeratorState.VisitingNode,   "c", 1, (1, 1), (0, 1)),
                (TreenumeratorState.VisitingNode,   "a", 2, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "d", 0, (2, 1), (1, 1)),
                (TreenumeratorState.VisitingNode,   "d", 1, (2, 1), (1, 1)),
                (TreenumeratorState.VisitingNode,   "a", 3, (0, 0), (0, 0)),
              }
            },
            new TestScenario
            {
              SchedulingPredicate = visit => visit.Node == "c" ? SchedulingStrategy.SkipSubtree : SchedulingStrategy.TraverseSubtree,
              Description = "Skip level 1, sibling 1 subtree",
              ExpectedBreadthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, (0, 0), (0, 0)),
                (TreenumeratorState.VisitingNode,   "a", 1, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "b", 0, (0, 1), (0, 1)),
                (TreenumeratorState.VisitingNode,   "a", 2, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "c", 0, (1, 1), (1, 1)),
                (TreenumeratorState.SchedulingNode, "d", 0, (2, 1), (1, 1)),
                (TreenumeratorState.VisitingNode,   "a", 3, (0, 0), (0, 0)),
                (TreenumeratorState.VisitingNode,   "b", 1, (0, 1), (0, 1)),
                (TreenumeratorState.VisitingNode,   "d", 1, (2, 1), (1, 1)),
              },
              ExpectedDepthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, (0, 0), (0, 0)),
                (TreenumeratorState.VisitingNode,   "a", 1, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "b", 0, (0, 1), (0, 1)),
                (TreenumeratorState.VisitingNode,   "b", 1, (0, 1), (0, 1)),
                (TreenumeratorState.VisitingNode,   "a", 2, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "c", 0, (1, 1), (1, 1)),
                (TreenumeratorState.SchedulingNode, "d", 0, (2, 1), (1, 1)),
                (TreenumeratorState.VisitingNode,   "d", 1, (2, 1), (1, 1)),
                (TreenumeratorState.VisitingNode,   "a", 3, (0, 0), (0, 0)),
              }
            },
            new TestScenario
            {
              SchedulingPredicate = visit => visit.Node == "d" ? SchedulingStrategy.SkipSubtree : SchedulingStrategy.TraverseSubtree,
              Description = "Skip level 1, sibling 2 subtree",
              ExpectedBreadthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, (0, 0), (0, 0)),
                (TreenumeratorState.VisitingNode,   "a", 1, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "b", 0, (0, 1), (0, 1)),
                (TreenumeratorState.VisitingNode,   "a", 2, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "c", 0, (1, 1), (1, 1)),
                (TreenumeratorState.VisitingNode,   "a", 3, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "d", 0, (2, 1), (2, 1)),
                (TreenumeratorState.VisitingNode,   "b", 1, (0, 1), (0, 1)),
                (TreenumeratorState.VisitingNode,   "c", 1, (1, 1), (1, 1)),
              },
              ExpectedDepthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, (0, 0), (0, 0)),
                (TreenumeratorState.VisitingNode,   "a", 1, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "b", 0, (0, 1), (0, 1)),
                (TreenumeratorState.VisitingNode,   "b", 1, (0, 1), (0, 1)),
                (TreenumeratorState.VisitingNode,   "a", 2, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "c", 0, (1, 1), (1, 1)),
                (TreenumeratorState.VisitingNode,   "c", 1, (1, 1), (1, 1)),
                (TreenumeratorState.VisitingNode,   "a", 3, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "d", 0, (2, 1), (2, 1)),
              }
            },
            // Skip descendant subtree
            new TestScenario
            {
              SchedulingPredicate = visit => visit.OriginalPosition.Depth == 0 ? SchedulingStrategy.SkipDescendants : SchedulingStrategy.TraverseSubtree,
              Description = "Skip level 0 descendant subtrees",
              ExpectedBreadthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, (0, 0), (0, 0)),
                (TreenumeratorState.VisitingNode,   "a", 1, (0, 0), (0, 0)),
              },
              ExpectedDepthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, (0, 0), (0, 0)),
                (TreenumeratorState.VisitingNode,   "a", 1, (0, 0), (0, 0)),
              }
            },
            new TestScenario
            {
              SchedulingPredicate = visit => visit.OriginalPosition.Depth == 1 ? SchedulingStrategy.SkipDescendants : SchedulingStrategy.TraverseSubtree,
              Description = "Skip level 1 descendant subtrees",
              ExpectedBreadthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, (0, 0), (0, 0)),
                (TreenumeratorState.VisitingNode,   "a", 1, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "b", 0, (0, 1), (0, 1)),
                (TreenumeratorState.VisitingNode,   "a", 2, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "c", 0, (1, 1), (1, 1)),
                (TreenumeratorState.VisitingNode,   "a", 3, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "d", 0, (2, 1), (2, 1)),
                (TreenumeratorState.VisitingNode,   "a", 4, (0, 0), (0, 0)),
                (TreenumeratorState.VisitingNode,   "b", 1, (0, 1), (0, 1)),
                (TreenumeratorState.VisitingNode,   "c", 1, (1, 1), (1, 1)),
                (TreenumeratorState.VisitingNode,   "d", 1, (2, 1), (2, 1)),
              },
              ExpectedDepthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, (0, 0), (0, 0)),
                (TreenumeratorState.VisitingNode,   "a", 1, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "b", 0, (0, 1), (0, 1)),
                (TreenumeratorState.VisitingNode,   "b", 1, (0, 1), (0, 1)),
                (TreenumeratorState.VisitingNode,   "a", 2, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "c", 0, (1, 1), (1, 1)),
                (TreenumeratorState.VisitingNode,   "c", 1, (1, 1), (1, 1)),
                (TreenumeratorState.VisitingNode,   "a", 3, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "d", 0, (2, 1), (2, 1)),
                (TreenumeratorState.VisitingNode,   "d", 1, (2, 1), (2, 1)),
                (TreenumeratorState.VisitingNode,   "a", 4, (0, 0), (0, 0)),
              }
            },
            new TestScenario
            {
              SchedulingPredicate = visit => visit.Node == "b" ? SchedulingStrategy.SkipDescendants : SchedulingStrategy.TraverseSubtree,
              Description = "Skip level 1, sibling 0 descendant subtrees",
              ExpectedBreadthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, (0, 0), (0, 0)),
                (TreenumeratorState.VisitingNode,   "a", 1, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "b", 0, (0, 1), (0, 1)),
                (TreenumeratorState.VisitingNode,   "a", 2, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "c", 0, (1, 1), (1, 1)),
                (TreenumeratorState.VisitingNode,   "a", 3, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "d", 0, (2, 1), (2, 1)),
                (TreenumeratorState.VisitingNode,   "a", 4, (0, 0), (0, 0)),
                (TreenumeratorState.VisitingNode,   "b", 1, (0, 1), (0, 1)),
                (TreenumeratorState.VisitingNode,   "c", 1, (1, 1), (1, 1)),
                (TreenumeratorState.VisitingNode,   "d", 1, (2, 1), (2, 1)),
              },
              ExpectedDepthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, (0, 0), (0, 0)),
                (TreenumeratorState.VisitingNode,   "a", 1, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "b", 0, (0, 1), (0, 1)),
                (TreenumeratorState.VisitingNode,   "b", 1, (0, 1), (0, 1)),
                (TreenumeratorState.VisitingNode,   "a", 2, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "c", 0, (1, 1), (1, 1)),
                (TreenumeratorState.VisitingNode,   "c", 1, (1, 1), (1, 1)),
                (TreenumeratorState.VisitingNode,   "a", 3, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "d", 0, (2, 1), (2, 1)),
                (TreenumeratorState.VisitingNode,   "d", 1, (2, 1), (2, 1)),
                (TreenumeratorState.VisitingNode,   "a", 4, (0, 0), (0, 0)),
              }
            },
            new TestScenario
            {
              SchedulingPredicate = visit => visit.Node == "c" ? SchedulingStrategy.SkipDescendants : SchedulingStrategy.TraverseSubtree,
              Description = "Skip level 1, sibling 1 descendant subtrees",
              ExpectedBreadthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, (0, 0), (0, 0)),
                (TreenumeratorState.VisitingNode,   "a", 1, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "b", 0, (0, 1), (0, 1)),
                (TreenumeratorState.VisitingNode,   "a", 2, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "c", 0, (1, 1), (1, 1)),
                (TreenumeratorState.VisitingNode,   "a", 3, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "d", 0, (2, 1), (2, 1)),
                (TreenumeratorState.VisitingNode,   "a", 4, (0, 0), (0, 0)),
                (TreenumeratorState.VisitingNode,   "b", 1, (0, 1), (0, 1)),
                (TreenumeratorState.VisitingNode,   "c", 1, (1, 1), (1, 1)),
                (TreenumeratorState.VisitingNode,   "d", 1, (2, 1), (2, 1)),
              },
              ExpectedDepthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, (0, 0), (0, 0)),
                (TreenumeratorState.VisitingNode,   "a", 1, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "b", 0, (0, 1), (0, 1)),
                (TreenumeratorState.VisitingNode,   "b", 1, (0, 1), (0, 1)),
                (TreenumeratorState.VisitingNode,   "a", 2, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "c", 0, (1, 1), (1, 1)),
                (TreenumeratorState.VisitingNode,   "c", 1, (1, 1), (1, 1)),
                (TreenumeratorState.VisitingNode,   "a", 3, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "d", 0, (2, 1), (2, 1)),
                (TreenumeratorState.VisitingNode,   "d", 1, (2, 1), (2, 1)),
                (TreenumeratorState.VisitingNode,   "a", 4, (0, 0), (0, 0)),
              }
            },
            new TestScenario
            {
              SchedulingPredicate = visit => visit.Node == "d" ? SchedulingStrategy.SkipDescendants : SchedulingStrategy.TraverseSubtree,
              Description = "Skip level 1, sibling 2 descendant subtrees",
              ExpectedBreadthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, (0, 0), (0, 0)),
                (TreenumeratorState.VisitingNode,   "a", 1, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "b", 0, (0, 1), (0, 1)),
                (TreenumeratorState.VisitingNode,   "a", 2, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "c", 0, (1, 1), (1, 1)),
                (TreenumeratorState.VisitingNode,   "a", 3, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "d", 0, (2, 1), (2, 1)),
                (TreenumeratorState.VisitingNode,   "a", 4, (0, 0), (0, 0)),
                (TreenumeratorState.VisitingNode,   "b", 1, (0, 1), (0, 1)),
                (TreenumeratorState.VisitingNode,   "c", 1, (1, 1), (1, 1)),
                (TreenumeratorState.VisitingNode,   "d", 1, (2, 1), (2, 1)),
              },
              ExpectedDepthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, (0, 0), (0, 0)),
                (TreenumeratorState.VisitingNode,   "a", 1, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "b", 0, (0, 1), (0, 1)),
                (TreenumeratorState.VisitingNode,   "b", 1, (0, 1), (0, 1)),
                (TreenumeratorState.VisitingNode,   "a", 2, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "c", 0, (1, 1), (1, 1)),
                (TreenumeratorState.VisitingNode,   "c", 1, (1, 1), (1, 1)),
                (TreenumeratorState.VisitingNode,   "a", 3, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "d", 0, (2, 1), (2, 1)),
                (TreenumeratorState.VisitingNode,   "d", 1, (2, 1), (2, 1)),
                (TreenumeratorState.VisitingNode,   "a", 4, (0, 0), (0, 0)),
              }
            },
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
              SchedulingPredicate = visit => SchedulingStrategy.TraverseSubtree,
              Description = "Traverse all",
              ExpectedBreadthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "b", 0, (1, 0), (1, 0)),
                (TreenumeratorState.VisitingNode,   "a", 1, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "c", 0, (0, 1), (0, 1)),
                (TreenumeratorState.VisitingNode,   "a", 2, (0, 0), (0, 0)),
                (TreenumeratorState.VisitingNode,   "b", 1, (1, 0), (1, 0)),
                (TreenumeratorState.SchedulingNode, "d", 0, (0, 1), (0, 1)),
                (TreenumeratorState.VisitingNode,   "b", 2, (1, 0), (1, 0)),
                (TreenumeratorState.VisitingNode,   "c", 1, (0, 1), (0, 1)),
                (TreenumeratorState.VisitingNode,   "d", 1, (0, 1), (0, 1)),
              },
              ExpectedDepthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, (0, 0), (0, 0)),
                (TreenumeratorState.VisitingNode,   "a", 1, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "c", 0, (0, 1), (0, 1)),
                (TreenumeratorState.VisitingNode,   "c", 1, (0, 1), (0, 1)),
                (TreenumeratorState.VisitingNode,   "a", 2, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "b", 0, (1, 0), (1, 0)),
                (TreenumeratorState.VisitingNode,   "b", 1, (1, 0), (1, 0)),
                (TreenumeratorState.SchedulingNode, "d", 0, (0, 1), (0, 1)),
                (TreenumeratorState.VisitingNode,   "d", 1, (0, 1), (0, 1)),
                (TreenumeratorState.VisitingNode,   "b", 2, (1, 0), (1, 0)),
              }
            },
            // Skip nodes
            new TestScenario
            {
              SchedulingPredicate = visit => SchedulingStrategy.SkipNode,
              Description = "Skip all nodes",
              ExpectedBreadthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "c", 0, (0, 1), (0, 0)),
                (TreenumeratorState.SchedulingNode, "b", 0, (1, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "d", 0, (0, 1), (0, 0)),
              },
              ExpectedDepthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "c", 0, (0, 1), (0, 0)),
                (TreenumeratorState.SchedulingNode, "b", 0, (1, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "d", 0, (0, 1), (0, 0)),
              }
            },
            new TestScenario
            {
              SchedulingPredicate = visit => visit.Node == "a" ? SchedulingStrategy.SkipNode : SchedulingStrategy.TraverseSubtree,
              Description = "Skip level 0, sibling 0 node",
              ExpectedBreadthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "c", 0, (0, 1), (0, 0)),
                (TreenumeratorState.SchedulingNode, "b", 0, (1, 0), (1, 0)),
                (TreenumeratorState.VisitingNode,   "c", 1, (0, 1), (0, 0)),
                (TreenumeratorState.VisitingNode,   "b", 1, (1, 0), (1, 0)),
                (TreenumeratorState.SchedulingNode, "d", 0, (0, 1), (0, 1)),
                (TreenumeratorState.VisitingNode,   "b", 2, (1, 0), (1, 0)),
                (TreenumeratorState.VisitingNode,   "d", 1, (0, 1), (0, 1)),
              },
              ExpectedDepthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "c", 0, (0, 1), (0, 0)),
                (TreenumeratorState.VisitingNode,   "c", 1, (0, 1), (0, 0)),
                (TreenumeratorState.SchedulingNode, "b", 0, (1, 0), (1, 0)),
                (TreenumeratorState.VisitingNode,   "b", 1, (1, 0), (1, 0)),
                (TreenumeratorState.SchedulingNode, "d", 0, (0, 1), (0, 1)),
                (TreenumeratorState.VisitingNode,   "d", 1, (0, 1), (0, 1)),
                (TreenumeratorState.VisitingNode,   "b", 2, (1, 0), (1, 0)),
              }
            },
            new TestScenario
            {
              SchedulingPredicate = visit => visit.Node == "b" ? SchedulingStrategy.SkipNode : SchedulingStrategy.TraverseSubtree,
              Description = "Skip level 0, sibling 1 node",
              ExpectedBreadthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "b", 0, (1, 0), (1, 0)),
                (TreenumeratorState.SchedulingNode, "d", 0, (0, 1), (1, 0)),
                (TreenumeratorState.VisitingNode,   "a", 1, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "c", 0, (0, 1), (0, 1)),
                (TreenumeratorState.VisitingNode,   "a", 2, (0, 0), (0, 0)),
                (TreenumeratorState.VisitingNode,   "d", 1, (0, 1), (1, 0)),
                (TreenumeratorState.VisitingNode,   "c", 1, (0, 1), (0, 1)),
              },
              ExpectedDepthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, (0, 0), (0, 0)),
                (TreenumeratorState.VisitingNode,   "a", 1, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "c", 0, (0, 1), (0, 1)),
                (TreenumeratorState.VisitingNode,   "c", 1, (0, 1), (0, 1)),
                (TreenumeratorState.VisitingNode,   "a", 2, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "b", 0, (1, 0), (1, 0)),
                (TreenumeratorState.SchedulingNode, "d", 0, (0, 1), (1, 0)),
                (TreenumeratorState.VisitingNode,   "d", 1, (0, 1), (1, 0)),
              }
            },
            new TestScenario
            {
              SchedulingPredicate = visit => visit.Node != "d" ? SchedulingStrategy.SkipNode : SchedulingStrategy.TraverseSubtree,
              Description = "Skip all but level 1, sibling 1 node",
              ExpectedBreadthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "c", 0, (0, 1), (0, 0)),
                (TreenumeratorState.SchedulingNode, "b", 0, (1, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "d", 0, (0, 1), (0, 0)),
                (TreenumeratorState.VisitingNode,   "d", 1, (0, 1), (0, 0)),
              },
              ExpectedDepthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "c", 0, (0, 1), (0, 0)),
                (TreenumeratorState.SchedulingNode, "b", 0, (1, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "d", 0, (0, 1), (0, 0)),
                (TreenumeratorState.VisitingNode,   "d", 1, (0, 1), (0, 0)),
              }
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
              SchedulingPredicate = visit => SchedulingStrategy.TraverseSubtree,
              Description = "Traverse all",
              ExpectedBreadthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "b", 0, (1, 0), (1, 0)),
                (TreenumeratorState.VisitingNode,   "a", 1, (0, 0), (0, 0)),
                (TreenumeratorState.VisitingNode,   "b", 1, (1, 0), (1, 0)),
                (TreenumeratorState.SchedulingNode, "c", 0, (0, 1), (0, 1)),
                (TreenumeratorState.VisitingNode,   "b", 2, (1, 0), (1, 0)),
                (TreenumeratorState.SchedulingNode, "d", 0, (1, 1), (1, 1)),
                (TreenumeratorState.VisitingNode,   "b", 3, (1, 0), (1, 0)),
                (TreenumeratorState.VisitingNode,   "c", 1, (0, 1), (0, 1)),
                (TreenumeratorState.VisitingNode,   "d", 1, (1, 1), (1, 1)),
              },
              ExpectedDepthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, (0, 0), (0, 0)),
                (TreenumeratorState.VisitingNode,   "a", 1, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "b", 0, (1, 0), (1, 0)),
                (TreenumeratorState.VisitingNode,   "b", 1, (1, 0), (1, 0)),
                (TreenumeratorState.SchedulingNode, "c", 0, (0, 1), (0, 1)),
                (TreenumeratorState.VisitingNode,   "c", 1, (0, 1), (0, 1)),
                (TreenumeratorState.VisitingNode,   "b", 2, (1, 0), (1, 0)),
                (TreenumeratorState.SchedulingNode, "d", 0, (1, 1), (1, 1)),
                (TreenumeratorState.VisitingNode,   "d", 1, (1, 1), (1, 1)),
                (TreenumeratorState.VisitingNode,   "b", 3, (1, 0), (1, 0)),
              }
            },
            // Skip nodes
            new TestScenario
            {
              SchedulingPredicate = visit => SchedulingStrategy.SkipNode,
              Description = "Skip all nodes",
              ExpectedBreadthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "b", 0, (1, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "c", 0, (0, 1), (0, 0)),
                (TreenumeratorState.SchedulingNode, "d", 0, (1, 1), (0, 0)),
              },
              ExpectedDepthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "b", 0, (1, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "c", 0, (0, 1), (0, 0)),
                (TreenumeratorState.SchedulingNode, "d", 0, (1, 1), (0, 0)),
              }
            },
            new TestScenario
            {
              SchedulingPredicate = visit => visit.Node == "b" ? SchedulingStrategy.SkipNode : SchedulingStrategy.TraverseSubtree,
              Description = "Skip level 0, sibling 1 node",
              ExpectedBreadthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "b", 0, (1, 0), (1, 0)),
                (TreenumeratorState.SchedulingNode, "c", 0, (0, 1), (1, 0)),
                (TreenumeratorState.SchedulingNode, "d", 0, (1, 1), (2, 0)),
                (TreenumeratorState.VisitingNode,   "a", 1, (0, 0), (0, 0)),
                (TreenumeratorState.VisitingNode,   "c", 1, (0, 1), (1, 0)),
                (TreenumeratorState.VisitingNode,   "d", 1, (1, 1), (2, 0)),
              },
              ExpectedDepthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, (0, 0), (0, 0)),
                (TreenumeratorState.VisitingNode,   "a", 1, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "b", 0, (1, 0), (1, 0)),
                (TreenumeratorState.SchedulingNode, "c", 0, (0, 1), (1, 0)),
                (TreenumeratorState.VisitingNode,   "c", 1, (0, 1), (1, 0)),
                (TreenumeratorState.SchedulingNode, "d", 0, (1, 1), (2, 0)),
                (TreenumeratorState.VisitingNode,   "d", 1, (1, 1), (2, 0)),
              }
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
              SchedulingPredicate = visit => SchedulingStrategy.TraverseSubtree,
              Description = "Traverse all",
              ExpectedBreadthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "b", 0, (1, 0), (1, 0)),
                (TreenumeratorState.SchedulingNode, "c", 0, (2, 0), (2, 0)),
                (TreenumeratorState.VisitingNode,   "a", 1, (0, 0), (0, 0)),
                (TreenumeratorState.VisitingNode,   "b", 1, (1, 0), (1, 0)),
                (TreenumeratorState.SchedulingNode, "d", 0, (0, 1), (0, 1)),
                (TreenumeratorState.VisitingNode,   "b", 2, (1, 0), (1, 0)),
                (TreenumeratorState.SchedulingNode, "e", 0, (1, 1), (1, 1)),
                (TreenumeratorState.VisitingNode,   "b", 3, (1, 0), (1, 0)),
                (TreenumeratorState.VisitingNode,   "c", 1, (2, 0), (2, 0)),
                (TreenumeratorState.VisitingNode,   "d", 1, (0, 1), (0, 1)),
                (TreenumeratorState.VisitingNode,   "e", 1, (1, 1), (1, 1)),
              },
              ExpectedDepthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, (0, 0), (0, 0)),
                (TreenumeratorState.VisitingNode,   "a", 1, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "b", 0, (1, 0), (1, 0)),
                (TreenumeratorState.VisitingNode,   "b", 1, (1, 0), (1, 0)),
                (TreenumeratorState.SchedulingNode, "d", 0, (0, 1), (0, 1)),
                (TreenumeratorState.VisitingNode,   "d", 1, (0, 1), (0, 1)),
                (TreenumeratorState.VisitingNode,   "b", 2, (1, 0), (1, 0)),
                (TreenumeratorState.SchedulingNode, "e", 0, (1, 1), (1, 1)),
                (TreenumeratorState.VisitingNode,   "e", 1, (1, 1), (1, 1)),
                (TreenumeratorState.VisitingNode,   "b", 3, (1, 0), (1, 0)),
                (TreenumeratorState.SchedulingNode, "c", 0, (2, 0), (2, 0)),
                (TreenumeratorState.VisitingNode,   "c", 1, (2, 0), (2, 0)),
              }
            },
            // Skip nodes
            new TestScenario
            {
              SchedulingPredicate = visit => visit.Node == "b" ? SchedulingStrategy.SkipNode : SchedulingStrategy.TraverseSubtree,
              Description = "Skip level 0, sibling 1 node",
              ExpectedBreadthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "b", 0, (1, 0), (1, 0)),
                (TreenumeratorState.SchedulingNode, "d", 0, (0, 1), (1, 0)),
                (TreenumeratorState.SchedulingNode, "e", 0, (1, 1), (2, 0)),
                (TreenumeratorState.SchedulingNode, "c", 0, (2, 0), (3, 0)),
                (TreenumeratorState.VisitingNode,   "a", 1, (0, 0), (0, 0)),
                (TreenumeratorState.VisitingNode,   "d", 1, (0, 1), (1, 0)),
                (TreenumeratorState.VisitingNode,   "e", 1, (1, 1), (2, 0)),
                (TreenumeratorState.VisitingNode,   "c", 1, (2, 0), (3, 0)),
              },
              ExpectedDepthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, (0, 0), (0, 0)),
                (TreenumeratorState.VisitingNode,   "a", 1, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "b", 0, (1, 0), (1, 0)),
                (TreenumeratorState.SchedulingNode, "d", 0, (0, 1), (1, 0)),
                (TreenumeratorState.VisitingNode,   "d", 1, (0, 1), (1, 0)),
                (TreenumeratorState.SchedulingNode, "e", 0, (1, 1), (2, 0)),
                (TreenumeratorState.VisitingNode,   "e", 1, (1, 1), (2, 0)),
                (TreenumeratorState.SchedulingNode, "c", 0, (2, 0), (3, 0)),
                (TreenumeratorState.VisitingNode,   "c", 1, (2, 0), (3, 0)),
              }
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
              SchedulingPredicate = visit => SchedulingStrategy.TraverseSubtree,
              Description = "Traverse all",
              ExpectedBreadthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "b", 0, (1, 0), (1, 0)),
                (TreenumeratorState.SchedulingNode, "c", 0, (2, 0), (2, 0)),
                (TreenumeratorState.VisitingNode,   "a", 1, (0, 0), (0, 0)),
                (TreenumeratorState.VisitingNode,   "b", 1, (1, 0), (1, 0)),
                (TreenumeratorState.VisitingNode,   "c", 1, (2, 0), (2, 0)),
              },
              ExpectedDepthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, (0, 0), (0, 0)),
                (TreenumeratorState.VisitingNode,   "a", 1, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "b", 0, (1, 0), (1, 0)),
                (TreenumeratorState.VisitingNode,   "b", 1, (1, 0), (1, 0)),
                (TreenumeratorState.SchedulingNode, "c", 0, (2, 0), (2, 0)),
                (TreenumeratorState.VisitingNode,   "c", 1, (2, 0), (2, 0)),
              }
            },
            // Skip nodes
            new TestScenario
            {
              SchedulingPredicate = visit => SchedulingStrategy.SkipNode,
              Description = "Skip all nodes",
              ExpectedBreadthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "b", 0, (1, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "c", 0, (2, 0), (0, 0)),
              },
              ExpectedDepthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "b", 0, (1, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "c", 0, (2, 0), (0, 0)),
              }
            },
            new TestScenario
            {
              SchedulingPredicate = visit => visit.Node == "a" ? SchedulingStrategy.SkipNode : SchedulingStrategy.TraverseSubtree,
              Description = "Skip level 0, sibling 0 node",
              ExpectedBreadthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "b", 0, (1, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "c", 0, (2, 0), (1, 0)),
                (TreenumeratorState.VisitingNode,   "b", 1, (1, 0), (0, 0)),
                (TreenumeratorState.VisitingNode,   "c", 1, (2, 0), (1, 0)),
              },
              ExpectedDepthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "b", 0, (1, 0), (0, 0)),
                (TreenumeratorState.VisitingNode,   "b", 1, (1, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "c", 0, (2, 0), (1, 0)),
                (TreenumeratorState.VisitingNode,   "c", 1, (2, 0), (1, 0)),
              }
            },
            new TestScenario
            {
              SchedulingPredicate = visit => visit.Node == "b" ? SchedulingStrategy.SkipNode : SchedulingStrategy.TraverseSubtree,
              Description = "Skip level 0, sibling 1 node",
              ExpectedBreadthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "b", 0, (1, 0), (1, 0)),
                (TreenumeratorState.SchedulingNode, "c", 0, (2, 0), (1, 0)),
                (TreenumeratorState.VisitingNode,   "a", 1, (0, 0), (0, 0)),
                (TreenumeratorState.VisitingNode,   "c", 1, (2, 0), (1, 0)),
              },
              ExpectedDepthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, (0, 0), (0, 0)),
                (TreenumeratorState.VisitingNode,   "a", 1, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "b", 0, (1, 0), (1, 0)),
                (TreenumeratorState.SchedulingNode, "c", 0, (2, 0), (1, 0)),
                (TreenumeratorState.VisitingNode,   "c", 1, (2, 0), (1, 0)),
              }
            },
            new TestScenario
            {
              SchedulingPredicate = visit => visit.Node == "c" ? SchedulingStrategy.SkipNode : SchedulingStrategy.TraverseSubtree,
              Description = "Skip level 0, sibling 2 node",
              ExpectedBreadthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "b", 0, (1, 0), (1, 0)),
                (TreenumeratorState.SchedulingNode, "c", 0, (2, 0), (2, 0)),
                (TreenumeratorState.VisitingNode,   "a", 1, (0, 0), (0, 0)),
                (TreenumeratorState.VisitingNode,   "b", 1, (1, 0), (1, 0)),
              },
              ExpectedDepthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, (0, 0), (0, 0)),
                (TreenumeratorState.VisitingNode,   "a", 1, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "b", 0, (1, 0), (1, 0)),
                (TreenumeratorState.VisitingNode,   "b", 1, (1, 0), (1, 0)),
                (TreenumeratorState.SchedulingNode, "c", 0, (2, 0), (2, 0)),
              }
            },
            // Skip subtree
            new TestScenario
            {
              SchedulingPredicate = visit => visit.Node == "a" ? SchedulingStrategy.SkipSubtree : SchedulingStrategy.TraverseSubtree,
              Description = "Skip level 0, sibling 0 subtree",
              ExpectedBreadthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "b", 0, (1, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "c", 0, (2, 0), (1, 0)),
                (TreenumeratorState.VisitingNode,   "b", 1, (1, 0), (0, 0)),
                (TreenumeratorState.VisitingNode,   "c", 1, (2, 0), (1, 0)),
              },
              ExpectedDepthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "b", 0, (1, 0), (0, 0)),
                (TreenumeratorState.VisitingNode,   "b", 1, (1, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "c", 0, (2, 0), (1, 0)),
                (TreenumeratorState.VisitingNode,   "c", 1, (2, 0), (1, 0)),
              }
            },
            new TestScenario
            {
              SchedulingPredicate = visit => visit.Node == "b" ? SchedulingStrategy.SkipSubtree : SchedulingStrategy.TraverseSubtree,
              Description = "Skip level 0, sibling 1 subtree",
              ExpectedBreadthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "b", 0, (1, 0), (1, 0)),
                (TreenumeratorState.SchedulingNode, "c", 0, (2, 0), (1, 0)),
                (TreenumeratorState.VisitingNode,   "a", 1, (0, 0), (0, 0)),
                (TreenumeratorState.VisitingNode,   "c", 1, (2, 0), (1, 0)),
              },
              ExpectedDepthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, (0, 0), (0, 0)),
                (TreenumeratorState.VisitingNode,   "a", 1, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "b", 0, (1, 0), (1, 0)),
                (TreenumeratorState.SchedulingNode, "c", 0, (2, 0), (1, 0)),
                (TreenumeratorState.VisitingNode,   "c", 1, (2, 0), (1, 0)),
              }
            },
            new TestScenario
            {
              SchedulingPredicate = visit => visit.Node == "c" ? SchedulingStrategy.SkipSubtree : SchedulingStrategy.TraverseSubtree,
              Description = "Skip level 0, sibling 2 subtree",
              ExpectedBreadthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "b", 0, (1, 0), (1, 0)),
                (TreenumeratorState.SchedulingNode, "c", 0, (2, 0), (2, 0)),
                (TreenumeratorState.VisitingNode,   "a", 1, (0, 0), (0, 0)),
                (TreenumeratorState.VisitingNode,   "b", 1, (1, 0), (1, 0)),
              },
              ExpectedDepthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, (0, 0), (0, 0)),
                (TreenumeratorState.VisitingNode,   "a", 1, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "b", 0, (1, 0), (1, 0)),
                (TreenumeratorState.VisitingNode,   "b", 1, (1, 0), (1, 0)),
                (TreenumeratorState.SchedulingNode, "c", 0, (2, 0), (2, 0)),
              }
            },
            new TestScenario
            {
              SchedulingPredicate = visit => SchedulingStrategy.SkipSubtree,
              Description = "Skip level 0 subtrees",
              ExpectedBreadthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "b", 0, (1, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "c", 0, (2, 0), (0, 0)),
              },
              ExpectedDepthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "b", 0, (1, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "c", 0, (2, 0), (0, 0)),
              }
            },
            // Skip descendant subtrees
            new TestScenario
            {
              SchedulingPredicate = visit => visit.Node == "a" ? SchedulingStrategy.SkipDescendants : SchedulingStrategy.TraverseSubtree,
              Description = "Skip level 0, sibling 0 descendant subtrees",
              ExpectedBreadthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "b", 0, (1, 0), (1, 0)),
                (TreenumeratorState.SchedulingNode, "c", 0, (2, 0), (2, 0)),
                (TreenumeratorState.VisitingNode,   "a", 1, (0, 0), (0, 0)),
                (TreenumeratorState.VisitingNode,   "b", 1, (1, 0), (1, 0)),
                (TreenumeratorState.VisitingNode,   "c", 1, (2, 0), (2, 0)),
              },
              ExpectedDepthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, (0, 0), (0, 0)),
                (TreenumeratorState.VisitingNode,   "a", 1, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "b", 0, (1, 0), (1, 0)),
                (TreenumeratorState.VisitingNode,   "b", 1, (1, 0), (1, 0)),
                (TreenumeratorState.SchedulingNode, "c", 0, (2, 0), (2, 0)),
                (TreenumeratorState.VisitingNode,   "c", 1, (2, 0), (2, 0)),
              }
            },
            new TestScenario
            {
              SchedulingPredicate = visit => visit.Node == "b" ? SchedulingStrategy.SkipDescendants : SchedulingStrategy.TraverseSubtree,
              Description = "Skip level 0, sibling 1 descendant subtrees",
              ExpectedBreadthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "b", 0, (1, 0), (1, 0)),
                (TreenumeratorState.SchedulingNode, "c", 0, (2, 0), (2, 0)),
                (TreenumeratorState.VisitingNode,   "a", 1, (0, 0), (0, 0)),
                (TreenumeratorState.VisitingNode,   "b", 1, (1, 0), (1, 0)),
                (TreenumeratorState.VisitingNode,   "c", 1, (2, 0), (2, 0)),
              },
              ExpectedDepthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, (0, 0), (0, 0)),
                (TreenumeratorState.VisitingNode,   "a", 1, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "b", 0, (1, 0), (1, 0)),
                (TreenumeratorState.VisitingNode,   "b", 1, (1, 0), (1, 0)),
                (TreenumeratorState.SchedulingNode, "c", 0, (2, 0), (2, 0)),
                (TreenumeratorState.VisitingNode,   "c", 1, (2, 0), (2, 0)),
              }
            },
            new TestScenario
            {
              SchedulingPredicate = visit => visit.Node == "c" ? SchedulingStrategy.SkipDescendants : SchedulingStrategy.TraverseSubtree,
              Description = "Skip level 0, sibling 2  descendant subtrees",
              ExpectedBreadthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "b", 0, (1, 0), (1, 0)),
                (TreenumeratorState.SchedulingNode, "c", 0, (2, 0), (2, 0)),
                (TreenumeratorState.VisitingNode,   "a", 1, (0, 0), (0, 0)),
                (TreenumeratorState.VisitingNode,   "b", 1, (1, 0), (1, 0)),
                (TreenumeratorState.VisitingNode,   "c", 1, (2, 0), (2, 0)),
              },
              ExpectedDepthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, (0, 0), (0, 0)),
                (TreenumeratorState.VisitingNode,   "a", 1, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "b", 0, (1, 0), (1, 0)),
                (TreenumeratorState.VisitingNode,   "b", 1, (1, 0), (1, 0)),
                (TreenumeratorState.SchedulingNode, "c", 0, (2, 0), (2, 0)),
                (TreenumeratorState.VisitingNode,   "c", 1, (2, 0), (2, 0)),
              }
            },
            new TestScenario
            {
              SchedulingPredicate = visit => SchedulingStrategy.SkipDescendants,
              Description = "Skip level 0, descendant subtrees",
              ExpectedBreadthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "b", 0, (1, 0), (1, 0)),
                (TreenumeratorState.SchedulingNode, "c", 0, (2, 0), (2, 0)),
                (TreenumeratorState.VisitingNode,   "a", 1, (0, 0), (0, 0)),
                (TreenumeratorState.VisitingNode,   "b", 1, (1, 0), (1, 0)),
                (TreenumeratorState.VisitingNode,   "c", 1, (2, 0), (2, 0)),
              },
              ExpectedDepthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, (0, 0), (0, 0)),
                (TreenumeratorState.VisitingNode,   "a", 1, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "b", 0, (1, 0), (1, 0)),
                (TreenumeratorState.VisitingNode,   "b", 1, (1, 0), (1, 0)),
                (TreenumeratorState.SchedulingNode, "c", 0, (2, 0), (2, 0)),
                (TreenumeratorState.VisitingNode,   "c", 1, (2, 0), (2, 0)),
              }
            },
          }
        },
      };
  }
}
