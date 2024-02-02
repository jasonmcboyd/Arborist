using Arborist.Tests.Utils;
using System.Collections.Generic;

namespace Arborist.Treenumerables.Tests
{
  public static class TestDataFactory
  {
    public static IEnumerable<(string, string, int, int)> GetTestInput()
    {
      for (int i = 0; i < TestTrees.Length; i++)
      {
        var testTree = TestTrees[i];

        for (int j = 0; j < testTree.TestScenarios.Count; j++)
        {
          var testScenario = testTree.TestScenarios[j];

          yield return (testTree.TreeString, testScenario.Description, i, j);
        }
      }
    }

    public static TestTree[] TestTrees =>
      new TestTree[]
      {
        // Empty Tree
        new TestTree
        {
          TreeString = "",
          TestScenarios = new List<TestScenario>
          {
            // No skipping
            new TestScenario
            {
              SchedulingPredicate = visit => SchedulingStrategy.ScheduleForTraversal,
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

        // Three root nodes, no children.
        new TestTree
        {
          TreeString = "a,b,c",
          TestScenarios = new List<TestScenario>
          {
            // No skipping
            new TestScenario
            {
              SchedulingPredicate = visit => SchedulingStrategy.ScheduleForTraversal,
              Description = "Traverse all",
              ExpectedBreadthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, 0, 0),
                (TreenumeratorState.SchedulingNode, "b", 0, 1, 0),
                (TreenumeratorState.SchedulingNode, "c", 0, 2, 0),
                (TreenumeratorState.VisitingNode,   "a", 1, 0, 0),
                (TreenumeratorState.VisitingNode,   "a", 2, 0, 0),
                (TreenumeratorState.VisitingNode,   "b", 1, 1, 0),
                (TreenumeratorState.VisitingNode,   "b", 2, 1, 0),
                (TreenumeratorState.VisitingNode,   "c", 1, 2, 0),
                (TreenumeratorState.VisitingNode,   "c", 2, 2, 0),
              },
              ExpectedDepthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, 0, 0),
                (TreenumeratorState.VisitingNode,   "a", 1, 0, 0),
                (TreenumeratorState.VisitingNode,   "a", 2, 0, 0),
                (TreenumeratorState.SchedulingNode, "b", 0, 1, 0),
                (TreenumeratorState.VisitingNode,   "b", 1, 1, 0),
                (TreenumeratorState.VisitingNode,   "b", 2, 1, 0),
                (TreenumeratorState.SchedulingNode, "c", 0, 2, 0),
                (TreenumeratorState.VisitingNode,   "c", 1, 2, 0),
                (TreenumeratorState.VisitingNode,   "c", 2, 2, 0),
              }
            },
            // Skip nodes
            new TestScenario
            {
              SchedulingPredicate = visit => SchedulingStrategy.SkipNode,
              Description = "Skip all nodes",
              ExpectedBreadthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, 0, 0),
                (TreenumeratorState.VisitingNode,   "a", 1, 0, 0),
                (TreenumeratorState.VisitingNode,   "a", 2, 0, 0),
                (TreenumeratorState.SchedulingNode, "b", 0, 1, 0),
                (TreenumeratorState.VisitingNode,   "b", 1, 1, 0),
                (TreenumeratorState.VisitingNode,   "b", 2, 1, 0),
                (TreenumeratorState.SchedulingNode, "c", 0, 2, 0),
                (TreenumeratorState.VisitingNode,   "c", 1, 2, 0),
                (TreenumeratorState.VisitingNode,   "c", 2, 2, 0),
              },
              ExpectedDepthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, 0, 0),
                (TreenumeratorState.VisitingNode,   "a", 1, 0, 0),
                (TreenumeratorState.VisitingNode,   "a", 2, 0, 0),
                (TreenumeratorState.SchedulingNode, "b", 0, 1, 0),
                (TreenumeratorState.VisitingNode,   "b", 1, 1, 0),
                (TreenumeratorState.VisitingNode,   "b", 2, 1, 0),
                (TreenumeratorState.SchedulingNode, "c", 0, 2, 0),
                (TreenumeratorState.VisitingNode,   "c", 1, 2, 0),
                (TreenumeratorState.VisitingNode,   "c", 2, 2, 0),
              }
            },
            new TestScenario
            {
              SchedulingPredicate = visit => visit.Node == "a" ? SchedulingStrategy.SkipNode : SchedulingStrategy.ScheduleForTraversal,
              Description = "Skip level 0, sibling 0 node",
              ExpectedBreadthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, 0, 0),
                (TreenumeratorState.VisitingNode,   "a", 1, 0, 0),
                (TreenumeratorState.VisitingNode,   "a", 2, 0, 0),
                (TreenumeratorState.SchedulingNode, "b", 0, 1, 0),
                (TreenumeratorState.SchedulingNode, "c", 0, 2, 0),
                (TreenumeratorState.VisitingNode,   "b", 1, 1, 0),
                (TreenumeratorState.VisitingNode,   "b", 2, 1, 0),
                (TreenumeratorState.VisitingNode,   "c", 1, 2, 0),
                (TreenumeratorState.VisitingNode,   "c", 2, 2, 0),
              },
              ExpectedDepthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, 0, 0),
                (TreenumeratorState.VisitingNode,   "a", 1, 0, 0),
                (TreenumeratorState.VisitingNode,   "a", 2, 0, 0),
                (TreenumeratorState.SchedulingNode, "b", 0, 1, 0),
                (TreenumeratorState.VisitingNode,   "b", 1, 1, 0),
                (TreenumeratorState.VisitingNode,   "b", 2, 1, 0),
                (TreenumeratorState.SchedulingNode, "c", 0, 2, 0),
                (TreenumeratorState.VisitingNode,   "c", 1, 2, 0),
                (TreenumeratorState.VisitingNode,   "c", 2, 2, 0),
              }
            },
            new TestScenario
            {
              SchedulingPredicate = visit => visit.Node == "b" ? SchedulingStrategy.SkipNode : SchedulingStrategy.ScheduleForTraversal,
              Description = "Skip level 0, sibling 1 node",
              ExpectedBreadthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, 0, 0),
                (TreenumeratorState.SchedulingNode, "b", 0, 1, 0),
                (TreenumeratorState.VisitingNode,   "b", 1, 1, 0),
                (TreenumeratorState.VisitingNode,   "b", 2, 1, 0),
                (TreenumeratorState.SchedulingNode, "c", 0, 2, 0),
                (TreenumeratorState.VisitingNode,   "a", 1, 0, 0),
                (TreenumeratorState.VisitingNode,   "a", 2, 0, 0),
                (TreenumeratorState.VisitingNode,   "c", 1, 2, 0),
                (TreenumeratorState.VisitingNode,   "c", 2, 2, 0),
              },
              ExpectedDepthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, 0, 0),
                (TreenumeratorState.VisitingNode,   "a", 1, 0, 0),
                (TreenumeratorState.VisitingNode,   "a", 2, 0, 0),
                (TreenumeratorState.SchedulingNode, "b", 0, 1, 0),
                (TreenumeratorState.VisitingNode,   "b", 1, 1, 0),
                (TreenumeratorState.VisitingNode,   "b", 2, 1, 0),
                (TreenumeratorState.SchedulingNode, "c", 0, 2, 0),
                (TreenumeratorState.VisitingNode,   "c", 1, 2, 0),
                (TreenumeratorState.VisitingNode,   "c", 2, 2, 0),
              }
            },
            new TestScenario
            {
              SchedulingPredicate = visit => visit.Node == "c" ? SchedulingStrategy.SkipNode : SchedulingStrategy.ScheduleForTraversal,
              Description = "Skip level 0, sibling 2 node",
              ExpectedBreadthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, 0, 0),
                (TreenumeratorState.SchedulingNode, "b", 0, 1, 0),
                (TreenumeratorState.SchedulingNode, "c", 0, 2, 0),
                (TreenumeratorState.VisitingNode,   "c", 1, 2, 0),
                (TreenumeratorState.VisitingNode,   "c", 2, 2, 0),
                (TreenumeratorState.VisitingNode,   "a", 1, 0, 0),
                (TreenumeratorState.VisitingNode,   "a", 2, 0, 0),
                (TreenumeratorState.VisitingNode,   "b", 1, 1, 0),
                (TreenumeratorState.VisitingNode,   "b", 2, 1, 0),
              },
              ExpectedDepthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, 0, 0),
                (TreenumeratorState.VisitingNode,   "a", 1, 0, 0),
                (TreenumeratorState.VisitingNode,   "a", 2, 0, 0),
                (TreenumeratorState.SchedulingNode, "b", 0, 1, 0),
                (TreenumeratorState.VisitingNode,   "b", 1, 1, 0),
                (TreenumeratorState.VisitingNode,   "b", 2, 1, 0),
                (TreenumeratorState.SchedulingNode, "c", 0, 2, 0),
                (TreenumeratorState.VisitingNode,   "c", 1, 2, 0),
                (TreenumeratorState.VisitingNode,   "c", 2, 2, 0),
              }
            },
            // Skip subtree
            new TestScenario
            {
              SchedulingPredicate = visit => visit.Node == "a" ? SchedulingStrategy.SkipSubtree : SchedulingStrategy.ScheduleForTraversal,
              Description = "Skip level 0, sibling 0 subtree",
              ExpectedBreadthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, 0, 0),
                (TreenumeratorState.SchedulingNode, "b", 0, 1, 0),
                (TreenumeratorState.SchedulingNode, "c", 0, 2, 0),
                (TreenumeratorState.VisitingNode,   "b", 1, 1, 0),
                (TreenumeratorState.VisitingNode,   "b", 2, 1, 0),
                (TreenumeratorState.VisitingNode,   "c", 1, 2, 0),
                (TreenumeratorState.VisitingNode,   "c", 2, 2, 0),
              },
              ExpectedDepthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, 0, 0),
                (TreenumeratorState.SchedulingNode, "b", 0, 1, 0),
                (TreenumeratorState.VisitingNode,   "b", 1, 1, 0),
                (TreenumeratorState.VisitingNode,   "b", 2, 1, 0),
                (TreenumeratorState.SchedulingNode, "c", 0, 2, 0),
                (TreenumeratorState.VisitingNode,   "c", 1, 2, 0),
                (TreenumeratorState.VisitingNode,   "c", 2, 2, 0),
              }
            },
            new TestScenario
            {
              SchedulingPredicate = visit => visit.Node == "b" ? SchedulingStrategy.SkipSubtree : SchedulingStrategy.ScheduleForTraversal,
              Description = "Skip level 0, sibling 1 subtree",
              ExpectedBreadthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, 0, 0),
                (TreenumeratorState.SchedulingNode, "b", 0, 1, 0),
                (TreenumeratorState.SchedulingNode, "c", 0, 2, 0),
                (TreenumeratorState.VisitingNode,   "a", 1, 0, 0),
                (TreenumeratorState.VisitingNode,   "a", 2, 0, 0),
                (TreenumeratorState.VisitingNode,   "c", 1, 2, 0),
                (TreenumeratorState.VisitingNode,   "c", 2, 2, 0),
              },
              ExpectedDepthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, 0, 0),
                (TreenumeratorState.VisitingNode,   "a", 1, 0, 0),
                (TreenumeratorState.VisitingNode,   "a", 2, 0, 0),
                (TreenumeratorState.SchedulingNode, "b", 0, 1, 0),
                (TreenumeratorState.SchedulingNode, "c", 0, 2, 0),
                (TreenumeratorState.VisitingNode,   "c", 1, 2, 0),
                (TreenumeratorState.VisitingNode,   "c", 2, 2, 0),
              }
            },
            new TestScenario
            {
              SchedulingPredicate = visit => visit.Node == "c" ? SchedulingStrategy.SkipSubtree : SchedulingStrategy.ScheduleForTraversal,
              Description = "Skip level 0, sibling 2 subtree",
              ExpectedBreadthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, 0, 0),
                (TreenumeratorState.SchedulingNode, "b", 0, 1, 0),
                (TreenumeratorState.SchedulingNode, "c", 0, 2, 0),
                (TreenumeratorState.VisitingNode,   "a", 1, 0, 0),
                (TreenumeratorState.VisitingNode,   "a", 2, 0, 0),
                (TreenumeratorState.VisitingNode,   "b", 1, 1, 0),
                (TreenumeratorState.VisitingNode,   "b", 2, 1, 0),
              },
              ExpectedDepthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, 0, 0),
                (TreenumeratorState.VisitingNode,   "a", 1, 0, 0),
                (TreenumeratorState.VisitingNode,   "a", 2, 0, 0),
                (TreenumeratorState.SchedulingNode, "b", 0, 1, 0),
                (TreenumeratorState.VisitingNode,   "b", 1, 1, 0),
                (TreenumeratorState.VisitingNode,   "b", 2, 1, 0),
                (TreenumeratorState.SchedulingNode, "c", 0, 2, 0),
              }
            },
            // Skip descendant subtrees
            new TestScenario
            {
              SchedulingPredicate = visit => visit.Node == "a" ? SchedulingStrategy.SkipDescendantSubtrees : SchedulingStrategy.ScheduleForTraversal,
              Description = "Skip level 0, sibling 0 descendant subtrees",
              ExpectedBreadthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, 0, 0),
                (TreenumeratorState.SchedulingNode, "b", 0, 1, 0),
                (TreenumeratorState.SchedulingNode, "c", 0, 2, 0),
                (TreenumeratorState.VisitingNode,   "a", 1, 0, 0),
                (TreenumeratorState.VisitingNode,   "a", 2, 0, 0),
                (TreenumeratorState.VisitingNode,   "b", 1, 1, 0),
                (TreenumeratorState.VisitingNode,   "b", 2, 1, 0),
                (TreenumeratorState.VisitingNode,   "c", 1, 2, 0),
                (TreenumeratorState.VisitingNode,   "c", 2, 2, 0),
              },
              ExpectedDepthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, 0, 0),
                (TreenumeratorState.VisitingNode,   "a", 1, 0, 0),
                (TreenumeratorState.VisitingNode,   "a", 2, 0, 0),
                (TreenumeratorState.SchedulingNode, "b", 0, 1, 0),
                (TreenumeratorState.VisitingNode,   "b", 1, 1, 0),
                (TreenumeratorState.VisitingNode,   "b", 2, 1, 0),
                (TreenumeratorState.SchedulingNode, "c", 0, 2, 0),
                (TreenumeratorState.VisitingNode,   "c", 1, 2, 0),
                (TreenumeratorState.VisitingNode,   "c", 2, 2, 0),
              }
            },
            new TestScenario
            {
              SchedulingPredicate = visit => visit.Node == "b" ? SchedulingStrategy.SkipDescendantSubtrees : SchedulingStrategy.ScheduleForTraversal,
              Description = "Skip level 0, sibling 1 descendant subtrees",
              ExpectedBreadthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, 0, 0),
                (TreenumeratorState.SchedulingNode, "b", 0, 1, 0),
                (TreenumeratorState.SchedulingNode, "c", 0, 2, 0),
                (TreenumeratorState.VisitingNode,   "a", 1, 0, 0),
                (TreenumeratorState.VisitingNode,   "a", 2, 0, 0),
                (TreenumeratorState.VisitingNode,   "b", 1, 1, 0),
                (TreenumeratorState.VisitingNode,   "b", 2, 1, 0),
                (TreenumeratorState.VisitingNode,   "c", 1, 2, 0),
                (TreenumeratorState.VisitingNode,   "c", 2, 2, 0),
              },
              ExpectedDepthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, 0, 0),
                (TreenumeratorState.VisitingNode,   "a", 1, 0, 0),
                (TreenumeratorState.VisitingNode,   "a", 2, 0, 0),
                (TreenumeratorState.SchedulingNode, "b", 0, 1, 0),
                (TreenumeratorState.VisitingNode,   "b", 1, 1, 0),
                (TreenumeratorState.VisitingNode,   "b", 2, 1, 0),
                (TreenumeratorState.SchedulingNode, "c", 0, 2, 0),
                (TreenumeratorState.VisitingNode,   "c", 1, 2, 0),
                (TreenumeratorState.VisitingNode,   "c", 2, 2, 0),
              }
            },
            new TestScenario
            {
              SchedulingPredicate = visit => visit.Node == "c" ? SchedulingStrategy.SkipDescendantSubtrees : SchedulingStrategy.ScheduleForTraversal,
              Description = "Skip level 0, sibling 2  descendant subtrees",
              ExpectedBreadthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, 0, 0),
                (TreenumeratorState.SchedulingNode, "b", 0, 1, 0),
                (TreenumeratorState.SchedulingNode, "c", 0, 2, 0),
                (TreenumeratorState.VisitingNode,   "a", 1, 0, 0),
                (TreenumeratorState.VisitingNode,   "a", 2, 0, 0),
                (TreenumeratorState.VisitingNode,   "b", 1, 1, 0),
                (TreenumeratorState.VisitingNode,   "b", 2, 1, 0),
                (TreenumeratorState.VisitingNode,   "c", 1, 2, 0),
                (TreenumeratorState.VisitingNode,   "c", 2, 2, 0),
              },
              ExpectedDepthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, 0, 0),
                (TreenumeratorState.VisitingNode,   "a", 1, 0, 0),
                (TreenumeratorState.VisitingNode,   "a", 2, 0, 0),
                (TreenumeratorState.SchedulingNode, "b", 0, 1, 0),
                (TreenumeratorState.VisitingNode,   "b", 1, 1, 0),
                (TreenumeratorState.VisitingNode,   "b", 2, 1, 0),
                (TreenumeratorState.SchedulingNode, "c", 0, 2, 0),
                (TreenumeratorState.VisitingNode,   "c", 1, 2, 0),
                (TreenumeratorState.VisitingNode,   "c", 2, 2, 0),
              }
            },
          }
        },

        // Two-level complete ternary tree.
        new TestTree
        {
          TreeString = "a(b,c,d)",
          TestScenarios = new List<TestScenario>
          {
            // No skipping
            new TestScenario
            {
              SchedulingPredicate = visit => SchedulingStrategy.ScheduleForTraversal,
              Description = "Traverse all",
              ExpectedBreadthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, 0, 0),
                (TreenumeratorState.VisitingNode,   "a", 1, 0, 0),
                (TreenumeratorState.SchedulingNode, "b", 0, 0, 1),
                (TreenumeratorState.VisitingNode,   "a", 2, 0, 0),
                (TreenumeratorState.SchedulingNode, "c", 0, 1, 1),
                (TreenumeratorState.VisitingNode,   "a", 3, 0, 0),
                (TreenumeratorState.SchedulingNode, "d", 0, 2, 1),
                (TreenumeratorState.VisitingNode,   "a", 4, 0, 0),
                (TreenumeratorState.VisitingNode,   "b", 1, 0, 1),
                (TreenumeratorState.VisitingNode,   "b", 2, 0, 1),
                (TreenumeratorState.VisitingNode,   "c", 1, 1, 1),
                (TreenumeratorState.VisitingNode,   "c", 2, 1, 1),
                (TreenumeratorState.VisitingNode,   "d", 1, 2, 1),
                (TreenumeratorState.VisitingNode,   "d", 2, 2, 1),
              },
              ExpectedDepthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, 0, 0),
                (TreenumeratorState.VisitingNode,   "a", 1, 0, 0),
                (TreenumeratorState.SchedulingNode, "b", 0, 0, 1),
                (TreenumeratorState.VisitingNode,   "b", 1, 0, 1),
                (TreenumeratorState.VisitingNode,   "b", 2, 0, 1),
                (TreenumeratorState.VisitingNode,   "a", 2, 0, 0),
                (TreenumeratorState.SchedulingNode, "c", 0, 1, 1),
                (TreenumeratorState.VisitingNode,   "c", 1, 1, 1),
                (TreenumeratorState.VisitingNode,   "c", 2, 1, 1),
                (TreenumeratorState.VisitingNode,   "a", 3, 0, 0),
                (TreenumeratorState.SchedulingNode, "d", 0, 2, 1),
                (TreenumeratorState.VisitingNode,   "d", 1, 2, 1),
                (TreenumeratorState.VisitingNode,   "d", 2, 2, 1),
                (TreenumeratorState.VisitingNode,   "a", 4, 0, 0),
              }
            },
            // Skip nodes
            new TestScenario
            {
              SchedulingPredicate = visit => SchedulingStrategy.SkipNode,
              Description = "Skip all nodes",
              ExpectedBreadthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, 0, 0),
                (TreenumeratorState.VisitingNode,   "a", 1, 0, 0),
                (TreenumeratorState.SchedulingNode, "b", 0, 0, 1),
                (TreenumeratorState.VisitingNode,   "b", 1, 0, 1),
                (TreenumeratorState.VisitingNode,   "b", 2, 0, 1),
                (TreenumeratorState.VisitingNode,   "a", 2, 0, 0),
                (TreenumeratorState.SchedulingNode, "c", 0, 1, 1),
                (TreenumeratorState.VisitingNode,   "c", 1, 1, 1),
                (TreenumeratorState.VisitingNode,   "c", 2, 1, 1),
                (TreenumeratorState.VisitingNode,   "a", 3, 0, 0),
                (TreenumeratorState.SchedulingNode, "d", 0, 2, 1),
                (TreenumeratorState.VisitingNode,   "d", 1, 2, 1),
                (TreenumeratorState.VisitingNode,   "d", 2, 2, 1),
                (TreenumeratorState.VisitingNode,   "a", 4, 0, 0),
              },
              ExpectedDepthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, 0, 0),
                (TreenumeratorState.VisitingNode,   "a", 1, 0, 0),
                (TreenumeratorState.SchedulingNode, "b", 0, 0, 1),
                (TreenumeratorState.VisitingNode,   "b", 1, 0, 1),
                (TreenumeratorState.VisitingNode,   "b", 2, 0, 1),
                (TreenumeratorState.VisitingNode,   "a", 2, 0, 0),
                (TreenumeratorState.SchedulingNode, "c", 0, 1, 1),
                (TreenumeratorState.VisitingNode,   "c", 1, 1, 1),
                (TreenumeratorState.VisitingNode,   "c", 2, 1, 1),
                (TreenumeratorState.VisitingNode,   "a", 3, 0, 0),
                (TreenumeratorState.SchedulingNode, "d", 0, 2, 1),
                (TreenumeratorState.VisitingNode,   "d", 1, 2, 1),
                (TreenumeratorState.VisitingNode,   "d", 2, 2, 1),
                (TreenumeratorState.VisitingNode,   "a", 4, 0, 0),
              }
            },
            new TestScenario
            {
              SchedulingPredicate = visit => visit.Depth == 0 ? SchedulingStrategy.SkipNode : SchedulingStrategy.ScheduleForTraversal,
              Description = "Skip level 0 nodes",
              ExpectedBreadthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, 0, 0),
                (TreenumeratorState.VisitingNode,   "a", 1, 0, 0),
                (TreenumeratorState.SchedulingNode, "b", 0, 0, 1),
                (TreenumeratorState.VisitingNode,   "b", 1, 0, 1),
                (TreenumeratorState.VisitingNode,   "b", 2, 0, 1),
                (TreenumeratorState.VisitingNode,   "a", 2, 0, 0),
                (TreenumeratorState.SchedulingNode, "c", 0, 1, 1),
                (TreenumeratorState.VisitingNode,   "c", 1, 1, 1),
                (TreenumeratorState.VisitingNode,   "c", 2, 1, 1),
                (TreenumeratorState.VisitingNode,   "a", 3, 0, 0),
                (TreenumeratorState.SchedulingNode, "d", 0, 2, 1),
                (TreenumeratorState.VisitingNode,   "d", 1, 2, 1),
                (TreenumeratorState.VisitingNode,   "d", 2, 2, 1),
                (TreenumeratorState.VisitingNode,   "a", 4, 0, 0),
              },
              ExpectedDepthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, 0, 0),
                (TreenumeratorState.VisitingNode,   "a", 1, 0, 0),
                (TreenumeratorState.SchedulingNode, "b", 0, 0, 1),
                (TreenumeratorState.VisitingNode,   "b", 1, 0, 1),
                (TreenumeratorState.VisitingNode,   "b", 2, 0, 1),
                (TreenumeratorState.VisitingNode,   "a", 2, 0, 0),
                (TreenumeratorState.SchedulingNode, "c", 0, 1, 1),
                (TreenumeratorState.VisitingNode,   "c", 1, 1, 1),
                (TreenumeratorState.VisitingNode,   "c", 2, 1, 1),
                (TreenumeratorState.VisitingNode,   "a", 3, 0, 0),
                (TreenumeratorState.SchedulingNode, "d", 0, 2, 1),
                (TreenumeratorState.VisitingNode,   "d", 1, 2, 1),
                (TreenumeratorState.VisitingNode,   "d", 2, 2, 1),
                (TreenumeratorState.VisitingNode,   "a", 4, 0, 0),
              }
            },
            new TestScenario
            {
              SchedulingPredicate = visit => visit.Depth == 1 ? SchedulingStrategy.SkipNode : SchedulingStrategy.ScheduleForTraversal,
              Description = "Skip level 1 nodes",
              ExpectedBreadthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, 0, 0),
                (TreenumeratorState.VisitingNode,   "a", 1, 0, 0),
                (TreenumeratorState.SchedulingNode, "b", 0, 0, 1),
                (TreenumeratorState.VisitingNode,   "a", 2, 0, 0),
                (TreenumeratorState.SchedulingNode, "c", 0, 1, 1),
                (TreenumeratorState.VisitingNode,   "a", 3, 0, 0),
                (TreenumeratorState.SchedulingNode, "d", 0, 2, 1),
                (TreenumeratorState.VisitingNode,   "a", 4, 0, 0),
                (TreenumeratorState.VisitingNode,   "b", 1, 0, 1),
                (TreenumeratorState.VisitingNode,   "b", 2, 0, 1),
                (TreenumeratorState.VisitingNode,   "c", 1, 1, 1),
                (TreenumeratorState.VisitingNode,   "c", 2, 1, 1),
                (TreenumeratorState.VisitingNode,   "d", 1, 2, 1),
                (TreenumeratorState.VisitingNode,   "d", 2, 2, 1),
              },
              ExpectedDepthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, 0, 0),
                (TreenumeratorState.VisitingNode,   "a", 1, 0, 0),
                (TreenumeratorState.SchedulingNode, "b", 0, 0, 1),
                (TreenumeratorState.VisitingNode,   "b", 1, 0, 1),
                (TreenumeratorState.VisitingNode,   "b", 2, 0, 1),
                (TreenumeratorState.VisitingNode,   "a", 2, 0, 0),
                (TreenumeratorState.SchedulingNode, "c", 0, 1, 1),
                (TreenumeratorState.VisitingNode,   "c", 1, 1, 1),
                (TreenumeratorState.VisitingNode,   "c", 2, 1, 1),
                (TreenumeratorState.VisitingNode,   "a", 3, 0, 0),
                (TreenumeratorState.SchedulingNode, "d", 0, 2, 1),
                (TreenumeratorState.VisitingNode,   "d", 1, 2, 1),
                (TreenumeratorState.VisitingNode,   "d", 2, 2, 1),
                (TreenumeratorState.VisitingNode,   "a", 4, 0, 0),
              }
            },
            new TestScenario
            {
              SchedulingPredicate = visit => visit.Node == "b" ? SchedulingStrategy.SkipNode : SchedulingStrategy.ScheduleForTraversal,
              Description = "Skip level 1, sibling 0 node",
              ExpectedBreadthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, 0, 0),
                (TreenumeratorState.VisitingNode,   "a", 1, 0, 0),
                (TreenumeratorState.SchedulingNode, "b", 0, 0, 1),
                (TreenumeratorState.VisitingNode,   "a", 2, 0, 0),
                (TreenumeratorState.SchedulingNode, "c", 0, 1, 1),
                (TreenumeratorState.VisitingNode,   "a", 3, 0, 0),
                (TreenumeratorState.SchedulingNode, "d", 0, 2, 1),
                (TreenumeratorState.VisitingNode,   "a", 4, 0, 0),
                (TreenumeratorState.VisitingNode,   "b", 1, 0, 1),
                (TreenumeratorState.VisitingNode,   "b", 2, 0, 1),
                (TreenumeratorState.VisitingNode,   "c", 1, 1, 1),
                (TreenumeratorState.VisitingNode,   "c", 2, 1, 1),
                (TreenumeratorState.VisitingNode,   "d", 1, 2, 1),
                (TreenumeratorState.VisitingNode,   "d", 2, 2, 1),
              },
              ExpectedDepthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, 0, 0),
                (TreenumeratorState.VisitingNode,   "a", 1, 0, 0),
                (TreenumeratorState.SchedulingNode, "b", 0, 0, 1),
                (TreenumeratorState.VisitingNode,   "b", 1, 0, 1),
                (TreenumeratorState.VisitingNode,   "b", 2, 0, 1),
                (TreenumeratorState.VisitingNode,   "a", 2, 0, 0),
                (TreenumeratorState.SchedulingNode, "c", 0, 1, 1),
                (TreenumeratorState.VisitingNode,   "c", 1, 1, 1),
                (TreenumeratorState.VisitingNode,   "c", 2, 1, 1),
                (TreenumeratorState.VisitingNode,   "a", 3, 0, 0),
                (TreenumeratorState.SchedulingNode, "d", 0, 2, 1),
                (TreenumeratorState.VisitingNode,   "d", 1, 2, 1),
                (TreenumeratorState.VisitingNode,   "d", 2, 2, 1),
                (TreenumeratorState.VisitingNode,   "a", 4, 0, 0),
              }
            },
            new TestScenario
            {
              SchedulingPredicate = visit => visit.Node == "c" ? SchedulingStrategy.SkipNode : SchedulingStrategy.ScheduleForTraversal,
              Description = "Skip level 1, sibling 1 node",
              ExpectedBreadthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, 0, 0),
                (TreenumeratorState.VisitingNode,   "a", 1, 0, 0),
                (TreenumeratorState.SchedulingNode, "b", 0, 0, 1),
                (TreenumeratorState.VisitingNode,   "a", 2, 0, 0),
                (TreenumeratorState.SchedulingNode, "c", 0, 1, 1),
                (TreenumeratorState.VisitingNode,   "a", 3, 0, 0),
                (TreenumeratorState.SchedulingNode, "d", 0, 2, 1),
                (TreenumeratorState.VisitingNode,   "a", 4, 0, 0),
                (TreenumeratorState.VisitingNode,   "b", 1, 0, 1),
                (TreenumeratorState.VisitingNode,   "b", 2, 0, 1),
                (TreenumeratorState.VisitingNode,   "c", 1, 1, 1),
                (TreenumeratorState.VisitingNode,   "c", 2, 1, 1),
                (TreenumeratorState.VisitingNode,   "d", 1, 2, 1),
                (TreenumeratorState.VisitingNode,   "d", 2, 2, 1),
              },
              ExpectedDepthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, 0, 0),
                (TreenumeratorState.VisitingNode,   "a", 1, 0, 0),
                (TreenumeratorState.SchedulingNode, "b", 0, 0, 1),
                (TreenumeratorState.VisitingNode,   "b", 1, 0, 1),
                (TreenumeratorState.VisitingNode,   "b", 2, 0, 1),
                (TreenumeratorState.VisitingNode,   "a", 2, 0, 0),
                (TreenumeratorState.SchedulingNode, "c", 0, 1, 1),
                (TreenumeratorState.VisitingNode,   "c", 1, 1, 1),
                (TreenumeratorState.VisitingNode,   "c", 2, 1, 1),
                (TreenumeratorState.VisitingNode,   "a", 3, 0, 0),
                (TreenumeratorState.SchedulingNode, "d", 0, 2, 1),
                (TreenumeratorState.VisitingNode,   "d", 1, 2, 1),
                (TreenumeratorState.VisitingNode,   "d", 2, 2, 1),
                (TreenumeratorState.VisitingNode,   "a", 4, 0, 0),
              }
            },
            new TestScenario
            {
              SchedulingPredicate = visit => visit.Node == "d" ? SchedulingStrategy.SkipNode : SchedulingStrategy.ScheduleForTraversal,
              Description = "Skip level 1, sibling 2 node",
              ExpectedBreadthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, 0, 0),
                (TreenumeratorState.VisitingNode,   "a", 1, 0, 0),
                (TreenumeratorState.SchedulingNode, "b", 0, 0, 1),
                (TreenumeratorState.VisitingNode,   "a", 2, 0, 0),
                (TreenumeratorState.SchedulingNode, "c", 0, 1, 1),
                (TreenumeratorState.VisitingNode,   "a", 3, 0, 0),
                (TreenumeratorState.SchedulingNode, "d", 0, 2, 1),
                (TreenumeratorState.VisitingNode,   "a", 4, 0, 0),
                (TreenumeratorState.VisitingNode,   "b", 1, 0, 1),
                (TreenumeratorState.VisitingNode,   "b", 2, 0, 1),
                (TreenumeratorState.VisitingNode,   "c", 1, 1, 1),
                (TreenumeratorState.VisitingNode,   "c", 2, 1, 1),
                (TreenumeratorState.VisitingNode,   "d", 1, 2, 1),
                (TreenumeratorState.VisitingNode,   "d", 2, 2, 1),
              },
              ExpectedDepthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, 0, 0),
                (TreenumeratorState.VisitingNode,   "a", 1, 0, 0),
                (TreenumeratorState.SchedulingNode, "b", 0, 0, 1),
                (TreenumeratorState.VisitingNode,   "b", 1, 0, 1),
                (TreenumeratorState.VisitingNode,   "b", 2, 0, 1),
                (TreenumeratorState.VisitingNode,   "a", 2, 0, 0),
                (TreenumeratorState.SchedulingNode, "c", 0, 1, 1),
                (TreenumeratorState.VisitingNode,   "c", 1, 1, 1),
                (TreenumeratorState.VisitingNode,   "c", 2, 1, 1),
                (TreenumeratorState.VisitingNode,   "a", 3, 0, 0),
                (TreenumeratorState.SchedulingNode, "d", 0, 2, 1),
                (TreenumeratorState.VisitingNode,   "d", 1, 2, 1),
                (TreenumeratorState.VisitingNode,   "d", 2, 2, 1),
                (TreenumeratorState.VisitingNode,   "a", 4, 0, 0),
              }
            },
            // Skip subtree
            new TestScenario
            {
              SchedulingPredicate = visit => visit.Depth == 0 ? SchedulingStrategy.SkipSubtree : SchedulingStrategy.ScheduleForTraversal,
              Description = "Skip level 0 subtrees",
              ExpectedBreadthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, 0, 0),
              },
              ExpectedDepthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, 0, 0),
              }
            },
            new TestScenario
            {
              SchedulingPredicate = visit => visit.Depth == 1 ? SchedulingStrategy.SkipSubtree : SchedulingStrategy.ScheduleForTraversal,
              Description = "Skip level 1 subtree",
              ExpectedBreadthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, 0, 0),
                (TreenumeratorState.VisitingNode,   "a", 1, 0, 0),
                (TreenumeratorState.SchedulingNode, "b", 0, 0, 1),
                (TreenumeratorState.VisitingNode,   "a", 2, 0, 0),
                (TreenumeratorState.SchedulingNode, "c", 0, 1, 1),
                (TreenumeratorState.VisitingNode,   "a", 3, 0, 0),
                (TreenumeratorState.SchedulingNode, "d", 0, 2, 1),
                (TreenumeratorState.VisitingNode,   "a", 4, 0, 0),
              },
              ExpectedDepthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, 0, 0),
                (TreenumeratorState.VisitingNode,   "a", 1, 0, 0),
                (TreenumeratorState.SchedulingNode, "b", 0, 0, 1),
                (TreenumeratorState.VisitingNode,   "a", 2, 0, 0),
                (TreenumeratorState.SchedulingNode, "c", 0, 1, 1),
                (TreenumeratorState.VisitingNode,   "a", 3, 0, 0),
                (TreenumeratorState.SchedulingNode, "d", 0, 2, 1),
                (TreenumeratorState.VisitingNode,   "a", 4, 0, 0),
              }
            },
            new TestScenario
            {
              SchedulingPredicate = visit => visit.Node == "b" ? SchedulingStrategy.SkipSubtree : SchedulingStrategy.ScheduleForTraversal,
              Description = "Skip level 1, sibling 0 subtree",
              ExpectedBreadthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, 0, 0),
                (TreenumeratorState.VisitingNode,   "a", 1, 0, 0),
                (TreenumeratorState.SchedulingNode, "b", 0, 0, 1),
                (TreenumeratorState.VisitingNode,   "a", 2, 0, 0),
                (TreenumeratorState.SchedulingNode, "c", 0, 1, 1),
                (TreenumeratorState.VisitingNode,   "a", 3, 0, 0),
                (TreenumeratorState.SchedulingNode, "d", 0, 2, 1),
                (TreenumeratorState.VisitingNode,   "a", 4, 0, 0),
                (TreenumeratorState.VisitingNode,   "c", 1, 1, 1),
                (TreenumeratorState.VisitingNode,   "c", 2, 1, 1),
                (TreenumeratorState.VisitingNode,   "d", 1, 2, 1),
                (TreenumeratorState.VisitingNode,   "d", 2, 2, 1),
              },
              ExpectedDepthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, 0, 0),
                (TreenumeratorState.VisitingNode,   "a", 1, 0, 0),
                (TreenumeratorState.SchedulingNode, "b", 0, 0, 1),
                (TreenumeratorState.VisitingNode,   "a", 2, 0, 0),
                (TreenumeratorState.SchedulingNode, "c", 0, 1, 1),
                (TreenumeratorState.VisitingNode,   "c", 1, 1, 1),
                (TreenumeratorState.VisitingNode,   "c", 2, 1, 1),
                (TreenumeratorState.VisitingNode,   "a", 3, 0, 0),
                (TreenumeratorState.SchedulingNode, "d", 0, 2, 1),
                (TreenumeratorState.VisitingNode,   "d", 1, 2, 1),
                (TreenumeratorState.VisitingNode,   "d", 2, 2, 1),
                (TreenumeratorState.VisitingNode,   "a", 4, 0, 0),
              }
            },
            new TestScenario
            {
              SchedulingPredicate = visit => visit.Node == "c" ? SchedulingStrategy.SkipSubtree : SchedulingStrategy.ScheduleForTraversal,
              Description = "Skip level 1, sibling 1 subtree",
              ExpectedBreadthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, 0, 0),
                (TreenumeratorState.VisitingNode,   "a", 1, 0, 0),
                (TreenumeratorState.SchedulingNode, "b", 0, 0, 1),
                (TreenumeratorState.VisitingNode,   "a", 2, 0, 0),
                (TreenumeratorState.SchedulingNode, "c", 0, 1, 1),
                (TreenumeratorState.VisitingNode,   "a", 3, 0, 0),
                (TreenumeratorState.SchedulingNode, "d", 0, 2, 1),
                (TreenumeratorState.VisitingNode,   "a", 4, 0, 0),
                (TreenumeratorState.VisitingNode,   "b", 1, 0, 1),
                (TreenumeratorState.VisitingNode,   "b", 2, 0, 1),
                (TreenumeratorState.VisitingNode,   "d", 1, 2, 1),
                (TreenumeratorState.VisitingNode,   "d", 2, 2, 1),
              },
              ExpectedDepthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, 0, 0),
                (TreenumeratorState.VisitingNode,   "a", 1, 0, 0),
                (TreenumeratorState.SchedulingNode, "b", 0, 0, 1),
                (TreenumeratorState.VisitingNode,   "b", 1, 0, 1),
                (TreenumeratorState.VisitingNode,   "b", 2, 0, 1),
                (TreenumeratorState.VisitingNode,   "a", 2, 0, 0),
                (TreenumeratorState.SchedulingNode, "c", 0, 1, 1),
                (TreenumeratorState.VisitingNode,   "a", 3, 0, 0),
                (TreenumeratorState.SchedulingNode, "d", 0, 2, 1),
                (TreenumeratorState.VisitingNode,   "d", 1, 2, 1),
                (TreenumeratorState.VisitingNode,   "d", 2, 2, 1),
                (TreenumeratorState.VisitingNode,   "a", 4, 0, 0),
              }
            },
            new TestScenario
            {
              SchedulingPredicate = visit => visit.Node == "d" ? SchedulingStrategy.SkipSubtree : SchedulingStrategy.ScheduleForTraversal,
              Description = "Skip level 1, sibling 2 subtree",
              ExpectedBreadthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, 0, 0),
                (TreenumeratorState.VisitingNode,   "a", 1, 0, 0),
                (TreenumeratorState.SchedulingNode, "b", 0, 0, 1),
                (TreenumeratorState.VisitingNode,   "a", 2, 0, 0),
                (TreenumeratorState.SchedulingNode, "c", 0, 1, 1),
                (TreenumeratorState.VisitingNode,   "a", 3, 0, 0),
                (TreenumeratorState.SchedulingNode, "d", 0, 2, 1),
                (TreenumeratorState.VisitingNode,   "a", 4, 0, 0),
                (TreenumeratorState.VisitingNode,   "b", 1, 0, 1),
                (TreenumeratorState.VisitingNode,   "b", 2, 0, 1),
                (TreenumeratorState.VisitingNode,   "c", 1, 1, 1),
                (TreenumeratorState.VisitingNode,   "c", 2, 1, 1),
              },
              ExpectedDepthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, 0, 0),
                (TreenumeratorState.VisitingNode,   "a", 1, 0, 0),
                (TreenumeratorState.SchedulingNode, "b", 0, 0, 1),
                (TreenumeratorState.VisitingNode,   "b", 1, 0, 1),
                (TreenumeratorState.VisitingNode,   "b", 2, 0, 1),
                (TreenumeratorState.VisitingNode,   "a", 2, 0, 0),
                (TreenumeratorState.SchedulingNode, "c", 0, 1, 1),
                (TreenumeratorState.VisitingNode,   "c", 1, 1, 1),
                (TreenumeratorState.VisitingNode,   "c", 2, 1, 1),
                (TreenumeratorState.VisitingNode,   "a", 3, 0, 0),
                (TreenumeratorState.SchedulingNode, "d", 0, 2, 1),
                (TreenumeratorState.VisitingNode,   "a", 4, 0, 0),
              }
            },
            // Skip descendant subtree
            new TestScenario
            {
              SchedulingPredicate = visit => visit.Depth == 0 ? SchedulingStrategy.SkipDescendantSubtrees : SchedulingStrategy.ScheduleForTraversal,
              Description = "Skip level 0 descendant subtrees",
              ExpectedBreadthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, 0, 0),
                (TreenumeratorState.VisitingNode,   "a", 1, 0, 0),
                (TreenumeratorState.VisitingNode,   "a", 2, 0, 0),
              },
              ExpectedDepthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, 0, 0),
                (TreenumeratorState.VisitingNode,   "a", 1, 0, 0),
                (TreenumeratorState.VisitingNode,   "a", 2, 0, 0),
              }
            },
            new TestScenario
            {
              SchedulingPredicate = visit => visit.Depth == 1 ? SchedulingStrategy.SkipDescendantSubtrees : SchedulingStrategy.ScheduleForTraversal,
              Description = "Skip level 1 descendant subtrees",
              ExpectedBreadthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, 0, 0),
                (TreenumeratorState.VisitingNode,   "a", 1, 0, 0),
                (TreenumeratorState.SchedulingNode, "b", 0, 0, 1),
                (TreenumeratorState.VisitingNode,   "a", 2, 0, 0),
                (TreenumeratorState.SchedulingNode, "c", 0, 1, 1),
                (TreenumeratorState.VisitingNode,   "a", 3, 0, 0),
                (TreenumeratorState.SchedulingNode, "d", 0, 2, 1),
                (TreenumeratorState.VisitingNode,   "a", 4, 0, 0),
                (TreenumeratorState.VisitingNode,   "b", 1, 0, 1),
                (TreenumeratorState.VisitingNode,   "b", 2, 0, 1),
                (TreenumeratorState.VisitingNode,   "c", 1, 1, 1),
                (TreenumeratorState.VisitingNode,   "c", 2, 1, 1),
                (TreenumeratorState.VisitingNode,   "d", 1, 2, 1),
                (TreenumeratorState.VisitingNode,   "d", 2, 2, 1),
              },
              ExpectedDepthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, 0, 0),
                (TreenumeratorState.VisitingNode,   "a", 1, 0, 0),
                (TreenumeratorState.SchedulingNode, "b", 0, 0, 1),
                (TreenumeratorState.VisitingNode,   "b", 1, 0, 1),
                (TreenumeratorState.VisitingNode,   "b", 2, 0, 1),
                (TreenumeratorState.VisitingNode,   "a", 2, 0, 0),
                (TreenumeratorState.SchedulingNode, "c", 0, 1, 1),
                (TreenumeratorState.VisitingNode,   "c", 1, 1, 1),
                (TreenumeratorState.VisitingNode,   "c", 2, 1, 1),
                (TreenumeratorState.VisitingNode,   "a", 3, 0, 0),
                (TreenumeratorState.SchedulingNode, "d", 0, 2, 1),
                (TreenumeratorState.VisitingNode,   "d", 1, 2, 1),
                (TreenumeratorState.VisitingNode,   "d", 2, 2, 1),
                (TreenumeratorState.VisitingNode,   "a", 4, 0, 0),
              }
            },
            new TestScenario
            {
              SchedulingPredicate = visit => visit.Node == "b" ? SchedulingStrategy.SkipDescendantSubtrees : SchedulingStrategy.ScheduleForTraversal,
              Description = "Skip level 1, sibling 0 descendant subtrees",
              ExpectedBreadthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, 0, 0),
                (TreenumeratorState.VisitingNode,   "a", 1, 0, 0),
                (TreenumeratorState.SchedulingNode, "b", 0, 0, 1),
                (TreenumeratorState.VisitingNode,   "a", 2, 0, 0),
                (TreenumeratorState.SchedulingNode, "c", 0, 1, 1),
                (TreenumeratorState.VisitingNode,   "a", 3, 0, 0),
                (TreenumeratorState.SchedulingNode, "d", 0, 2, 1),
                (TreenumeratorState.VisitingNode,   "a", 4, 0, 0),
                (TreenumeratorState.VisitingNode,   "b", 1, 0, 1),
                (TreenumeratorState.VisitingNode,   "b", 2, 0, 1),
                (TreenumeratorState.VisitingNode,   "c", 1, 1, 1),
                (TreenumeratorState.VisitingNode,   "c", 2, 1, 1),
                (TreenumeratorState.VisitingNode,   "d", 1, 2, 1),
                (TreenumeratorState.VisitingNode,   "d", 2, 2, 1),
              },
              ExpectedDepthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, 0, 0),
                (TreenumeratorState.VisitingNode,   "a", 1, 0, 0),
                (TreenumeratorState.SchedulingNode, "b", 0, 0, 1),
                (TreenumeratorState.VisitingNode,   "b", 1, 0, 1),
                (TreenumeratorState.VisitingNode,   "b", 2, 0, 1),
                (TreenumeratorState.VisitingNode,   "a", 2, 0, 0),
                (TreenumeratorState.SchedulingNode, "c", 0, 1, 1),
                (TreenumeratorState.VisitingNode,   "c", 1, 1, 1),
                (TreenumeratorState.VisitingNode,   "c", 2, 1, 1),
                (TreenumeratorState.VisitingNode,   "a", 3, 0, 0),
                (TreenumeratorState.SchedulingNode, "d", 0, 2, 1),
                (TreenumeratorState.VisitingNode,   "d", 1, 2, 1),
                (TreenumeratorState.VisitingNode,   "d", 2, 2, 1),
                (TreenumeratorState.VisitingNode,   "a", 4, 0, 0),
              }
            },
            new TestScenario
            {
              SchedulingPredicate = visit => visit.Node == "c" ? SchedulingStrategy.SkipDescendantSubtrees : SchedulingStrategy.ScheduleForTraversal,
              Description = "Skip level 1, sibling 1 descendant subtrees",
              ExpectedBreadthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, 0, 0),
                (TreenumeratorState.VisitingNode,   "a", 1, 0, 0),
                (TreenumeratorState.SchedulingNode, "b", 0, 0, 1),
                (TreenumeratorState.VisitingNode,   "a", 2, 0, 0),
                (TreenumeratorState.SchedulingNode, "c", 0, 1, 1),
                (TreenumeratorState.VisitingNode,   "a", 3, 0, 0),
                (TreenumeratorState.SchedulingNode, "d", 0, 2, 1),
                (TreenumeratorState.VisitingNode,   "a", 4, 0, 0),
                (TreenumeratorState.VisitingNode,   "b", 1, 0, 1),
                (TreenumeratorState.VisitingNode,   "b", 2, 0, 1),
                (TreenumeratorState.VisitingNode,   "c", 1, 1, 1),
                (TreenumeratorState.VisitingNode,   "c", 2, 1, 1),
                (TreenumeratorState.VisitingNode,   "d", 1, 2, 1),
                (TreenumeratorState.VisitingNode,   "d", 2, 2, 1),
              },
              ExpectedDepthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, 0, 0),
                (TreenumeratorState.VisitingNode,   "a", 1, 0, 0),
                (TreenumeratorState.SchedulingNode, "b", 0, 0, 1),
                (TreenumeratorState.VisitingNode,   "b", 1, 0, 1),
                (TreenumeratorState.VisitingNode,   "b", 2, 0, 1),
                (TreenumeratorState.VisitingNode,   "a", 2, 0, 0),
                (TreenumeratorState.SchedulingNode, "c", 0, 1, 1),
                (TreenumeratorState.VisitingNode,   "c", 1, 1, 1),
                (TreenumeratorState.VisitingNode,   "c", 2, 1, 1),
                (TreenumeratorState.VisitingNode,   "a", 3, 0, 0),
                (TreenumeratorState.SchedulingNode, "d", 0, 2, 1),
                (TreenumeratorState.VisitingNode,   "d", 1, 2, 1),
                (TreenumeratorState.VisitingNode,   "d", 2, 2, 1),
                (TreenumeratorState.VisitingNode,   "a", 4, 0, 0),
              }
            },
            new TestScenario
            {
              SchedulingPredicate = visit => visit.Node == "d" ? SchedulingStrategy.SkipDescendantSubtrees : SchedulingStrategy.ScheduleForTraversal,
              Description = "Skip level 1, sibling 2 descendant subtrees",
              ExpectedBreadthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, 0, 0),
                (TreenumeratorState.VisitingNode,   "a", 1, 0, 0),
                (TreenumeratorState.SchedulingNode, "b", 0, 0, 1),
                (TreenumeratorState.VisitingNode,   "a", 2, 0, 0),
                (TreenumeratorState.SchedulingNode, "c", 0, 1, 1),
                (TreenumeratorState.VisitingNode,   "a", 3, 0, 0),
                (TreenumeratorState.SchedulingNode, "d", 0, 2, 1),
                (TreenumeratorState.VisitingNode,   "a", 4, 0, 0),
                (TreenumeratorState.VisitingNode,   "b", 1, 0, 1),
                (TreenumeratorState.VisitingNode,   "b", 2, 0, 1),
                (TreenumeratorState.VisitingNode,   "c", 1, 1, 1),
                (TreenumeratorState.VisitingNode,   "c", 2, 1, 1),
                (TreenumeratorState.VisitingNode,   "d", 1, 2, 1),
                (TreenumeratorState.VisitingNode,   "d", 2, 2, 1),
              },
              ExpectedDepthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, 0, 0),
                (TreenumeratorState.VisitingNode,   "a", 1, 0, 0),
                (TreenumeratorState.SchedulingNode, "b", 0, 0, 1),
                (TreenumeratorState.VisitingNode,   "b", 1, 0, 1),
                (TreenumeratorState.VisitingNode,   "b", 2, 0, 1),
                (TreenumeratorState.VisitingNode,   "a", 2, 0, 0),
                (TreenumeratorState.SchedulingNode, "c", 0, 1, 1),
                (TreenumeratorState.VisitingNode,   "c", 1, 1, 1),
                (TreenumeratorState.VisitingNode,   "c", 2, 1, 1),
                (TreenumeratorState.VisitingNode,   "a", 3, 0, 0),
                (TreenumeratorState.SchedulingNode, "d", 0, 2, 1),
                (TreenumeratorState.VisitingNode,   "d", 1, 2, 1),
                (TreenumeratorState.VisitingNode,   "d", 2, 2, 1),
                (TreenumeratorState.VisitingNode,   "a", 4, 0, 0),
              }
            },
          }
        },

        // Three-level unary tree.
        new TestTree
        {
          TreeString = "a(b(c))",
          TestScenarios = new List<TestScenario>()
          {
            // No skipping
            new TestScenario
            {
              SchedulingPredicate = visit => SchedulingStrategy.ScheduleForTraversal,
              Description = "Traverse all",
              ExpectedBreadthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, 0, 0),
                (TreenumeratorState.VisitingNode,   "a", 1, 0, 0),
                (TreenumeratorState.SchedulingNode, "b", 0, 0, 1),
                (TreenumeratorState.VisitingNode,   "a", 2, 0, 0),
                (TreenumeratorState.VisitingNode,   "b", 1, 0, 1),
                (TreenumeratorState.SchedulingNode, "c", 0, 0, 2),
                (TreenumeratorState.VisitingNode,   "b", 2, 0, 1),
                (TreenumeratorState.VisitingNode,   "c", 1, 0, 2),
                (TreenumeratorState.VisitingNode,   "c", 2, 0, 2),
              },
              ExpectedDepthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, 0, 0),
                (TreenumeratorState.VisitingNode,   "a", 1, 0, 0),
                (TreenumeratorState.SchedulingNode, "b", 0, 0, 1),
                (TreenumeratorState.VisitingNode,   "b", 1, 0, 1),
                (TreenumeratorState.SchedulingNode, "c", 0, 0, 2),
                (TreenumeratorState.VisitingNode,   "c", 1, 0, 2),
                (TreenumeratorState.VisitingNode,   "c", 2, 0, 2),
                (TreenumeratorState.VisitingNode,   "b", 2, 0, 1),
                (TreenumeratorState.VisitingNode,   "a", 2, 0, 0),
              }
            },
            // Skip nodes
            new TestScenario
            {
              SchedulingPredicate = visit => SchedulingStrategy.SkipNode,
              Description = "Skip all nodes",
              ExpectedBreadthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, 0, 0),
                (TreenumeratorState.VisitingNode,   "a", 1, 0, 0),
                (TreenumeratorState.SchedulingNode, "b", 0, 0, 1),
                (TreenumeratorState.VisitingNode,   "b", 1, 0, 1),
                (TreenumeratorState.SchedulingNode, "c", 0, 0, 2),
                (TreenumeratorState.VisitingNode,   "c", 1, 0, 2),
                (TreenumeratorState.VisitingNode,   "c", 2, 0, 2),
                (TreenumeratorState.VisitingNode,   "b", 2, 0, 1),
                (TreenumeratorState.VisitingNode,   "a", 2, 0, 0),
              },
              ExpectedDepthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, 0, 0),
                (TreenumeratorState.VisitingNode,   "a", 1, 0, 0),
                (TreenumeratorState.SchedulingNode, "b", 0, 0, 1),
                (TreenumeratorState.VisitingNode,   "b", 1, 0, 1),
                (TreenumeratorState.SchedulingNode, "c", 0, 0, 2),
                (TreenumeratorState.VisitingNode,   "c", 1, 0, 2),
                (TreenumeratorState.VisitingNode,   "c", 2, 0, 2),
                (TreenumeratorState.VisitingNode,   "b", 2, 0, 1),
                (TreenumeratorState.VisitingNode,   "a", 2, 0, 0),
              }
            },
            new TestScenario
            {
              SchedulingPredicate = visit => visit.Depth == 0 ? SchedulingStrategy.SkipNode : SchedulingStrategy.ScheduleForTraversal,
              Description = "Skip level 0, sibling 0 node",
              ExpectedBreadthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, 0, 0),
                (TreenumeratorState.VisitingNode,   "a", 1, 0, 0),
                (TreenumeratorState.SchedulingNode, "b", 0, 0, 1),
                (TreenumeratorState.VisitingNode,   "b", 1, 0, 1),
                (TreenumeratorState.SchedulingNode, "c", 0, 0, 2),
                (TreenumeratorState.VisitingNode,   "b", 2, 0, 1),
                (TreenumeratorState.VisitingNode,   "a", 2, 0, 0),
                (TreenumeratorState.VisitingNode,   "c", 1, 0, 2),
                (TreenumeratorState.VisitingNode,   "c", 2, 0, 2),
              },
              ExpectedDepthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, 0, 0),
                (TreenumeratorState.VisitingNode,   "a", 1, 0, 0),
                (TreenumeratorState.SchedulingNode, "b", 0, 0, 1),
                (TreenumeratorState.VisitingNode,   "b", 1, 0, 1),
                (TreenumeratorState.SchedulingNode, "c", 0, 0, 2),
                (TreenumeratorState.VisitingNode,   "c", 1, 0, 2),
                (TreenumeratorState.VisitingNode,   "c", 2, 0, 2),
                (TreenumeratorState.VisitingNode,   "b", 2, 0, 1),
                (TreenumeratorState.VisitingNode,   "a", 2, 0, 0),
              }
            },
            new TestScenario
            {
              SchedulingPredicate = visit => visit.Depth == 1 ? SchedulingStrategy.SkipNode : SchedulingStrategy.ScheduleForTraversal,
              Description = "Skip level 1, sibling 0 node",
              ExpectedBreadthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, 0, 0),
                (TreenumeratorState.VisitingNode,   "a", 1, 0, 0),
                (TreenumeratorState.SchedulingNode, "b", 0, 0, 1),
                (TreenumeratorState.VisitingNode,   "a", 2, 0, 0),
                (TreenumeratorState.VisitingNode,   "b", 1, 0, 1),
                (TreenumeratorState.SchedulingNode, "c", 0, 0, 2),
                (TreenumeratorState.VisitingNode,   "c", 1, 0, 2),
                (TreenumeratorState.VisitingNode,   "c", 2, 0, 2),
                (TreenumeratorState.VisitingNode,   "b", 2, 0, 1),
              },
              ExpectedDepthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, 0, 0),
                (TreenumeratorState.VisitingNode,   "a", 1, 0, 0),
                (TreenumeratorState.SchedulingNode, "b", 0, 0, 1),
                (TreenumeratorState.VisitingNode,   "b", 1, 0, 1),
                (TreenumeratorState.SchedulingNode, "c", 0, 0, 2),
                (TreenumeratorState.VisitingNode,   "c", 1, 0, 2),
                (TreenumeratorState.VisitingNode,   "c", 2, 0, 2),
                (TreenumeratorState.VisitingNode,   "b", 2, 0, 1),
                (TreenumeratorState.VisitingNode,   "a", 2, 0, 0),
              }
            },
            new TestScenario
            {
              SchedulingPredicate = visit => visit.Depth == 2 ? SchedulingStrategy.SkipNode : SchedulingStrategy.ScheduleForTraversal,
              Description = "Skip level 2, sibling 0 node",
              ExpectedBreadthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, 0, 0),
                (TreenumeratorState.VisitingNode,   "a", 1, 0, 0),
                (TreenumeratorState.SchedulingNode, "b", 0, 0, 1),
                (TreenumeratorState.VisitingNode,   "a", 2, 0, 0),
                (TreenumeratorState.VisitingNode,   "b", 1, 0, 1),
                (TreenumeratorState.SchedulingNode, "c", 0, 0, 2),
                (TreenumeratorState.VisitingNode,   "b", 2, 0, 1),
                (TreenumeratorState.VisitingNode,   "c", 1, 0, 2),
                (TreenumeratorState.VisitingNode,   "c", 2, 0, 2),
              },
              ExpectedDepthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, 0, 0),
                (TreenumeratorState.VisitingNode,   "a", 1, 0, 0),
                (TreenumeratorState.SchedulingNode, "b", 0, 0, 1),
                (TreenumeratorState.VisitingNode,   "b", 1, 0, 1),
                (TreenumeratorState.SchedulingNode, "c", 0, 0, 2),
                (TreenumeratorState.VisitingNode,   "c", 1, 0, 2),
                (TreenumeratorState.VisitingNode,   "c", 2, 0, 2),
                (TreenumeratorState.VisitingNode,   "b", 2, 0, 1),
                (TreenumeratorState.VisitingNode,   "a", 2, 0, 0),
              }
            },
            new TestScenario
            {
              SchedulingPredicate = visit => visit.Depth == 0 || visit.Depth == 2 ? SchedulingStrategy.SkipNode : SchedulingStrategy.ScheduleForTraversal,
              Description = "Skip level 0 and level 2, sibling 0 node",
              ExpectedBreadthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, 0, 0),
                (TreenumeratorState.VisitingNode,   "a", 1, 0, 0),
                (TreenumeratorState.SchedulingNode, "b", 0, 0, 1),
                (TreenumeratorState.VisitingNode,   "b", 1, 0, 1),
                (TreenumeratorState.SchedulingNode, "c", 0, 0, 2),
                (TreenumeratorState.VisitingNode,   "b", 2, 0, 1),
                (TreenumeratorState.VisitingNode,   "a", 2, 0, 0),
                (TreenumeratorState.VisitingNode,   "c", 1, 0, 2),
                (TreenumeratorState.VisitingNode,   "c", 2, 0, 2),
              },
              ExpectedDepthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, 0, 0),
                (TreenumeratorState.VisitingNode,   "a", 1, 0, 0),
                (TreenumeratorState.SchedulingNode, "b", 0, 0, 1),
                (TreenumeratorState.VisitingNode,   "b", 1, 0, 1),
                (TreenumeratorState.SchedulingNode, "c", 0, 0, 2),
                (TreenumeratorState.VisitingNode,   "c", 1, 0, 2),
                (TreenumeratorState.VisitingNode,   "c", 2, 0, 2),
                (TreenumeratorState.VisitingNode,   "b", 2, 0, 1),
                (TreenumeratorState.VisitingNode,   "a", 2, 0, 0),
              }
            },
            // Skip subtree
            new TestScenario
            {
              SchedulingPredicate = visit => visit.Depth == 0 ? SchedulingStrategy.SkipSubtree : SchedulingStrategy.ScheduleForTraversal,
              Description = "Skip level 0, sibling 0 subtree",
              ExpectedBreadthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, 0, 0),
              },
              ExpectedDepthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, 0, 0),
              }
            },
            new TestScenario
            {
              SchedulingPredicate = visit => visit.Depth == 1 ? SchedulingStrategy.SkipSubtree : SchedulingStrategy.ScheduleForTraversal,
              Description = "Skip level 1, sibling 0 subtree",
              ExpectedBreadthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, 0, 0),
                (TreenumeratorState.VisitingNode,   "a", 1, 0, 0),
                (TreenumeratorState.SchedulingNode, "b", 0, 0, 1),
                (TreenumeratorState.VisitingNode,   "a", 2, 0, 0),
              },
              ExpectedDepthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, 0, 0),
                (TreenumeratorState.VisitingNode,   "a", 1, 0, 0),
                (TreenumeratorState.SchedulingNode, "b", 0, 0, 1),
                (TreenumeratorState.VisitingNode,   "a", 2, 0, 0),
              }
            },
            new TestScenario
            {
              SchedulingPredicate = visit => visit.Depth == 2 ? SchedulingStrategy.SkipSubtree : SchedulingStrategy.ScheduleForTraversal,
              Description = "Skip level 2, sibling 0 subtree",
              ExpectedBreadthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, 0, 0),
                (TreenumeratorState.VisitingNode,   "a", 1, 0, 0),
                (TreenumeratorState.SchedulingNode, "b", 0, 0, 1),
                (TreenumeratorState.VisitingNode,   "a", 2, 0, 0),
                (TreenumeratorState.VisitingNode,   "b", 1, 0, 1),
                (TreenumeratorState.SchedulingNode, "c", 0, 0, 2),
                (TreenumeratorState.VisitingNode,   "b", 2, 0, 1),
              },
              ExpectedDepthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, 0, 0),
                (TreenumeratorState.VisitingNode,   "a", 1, 0, 0),
                (TreenumeratorState.SchedulingNode, "b", 0, 0, 1),
                (TreenumeratorState.VisitingNode,   "b", 1, 0, 1),
                (TreenumeratorState.SchedulingNode, "c", 0, 0, 2),
                (TreenumeratorState.VisitingNode,   "b", 2, 0, 1),
                (TreenumeratorState.VisitingNode,   "a", 2, 0, 0),
              }
            },
            // Skip descendant subtrees
            new TestScenario
            {
              SchedulingPredicate = visit => visit.Depth == 0 ? SchedulingStrategy.SkipDescendantSubtrees : SchedulingStrategy.ScheduleForTraversal,
              Description = "Skip level 0, sibling 0 descedant subtrees",
              ExpectedBreadthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, 0, 0),
                (TreenumeratorState.VisitingNode,   "a", 1, 0, 0),
                (TreenumeratorState.VisitingNode,   "a", 2, 0, 0),
              },
              ExpectedDepthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, 0, 0),
                (TreenumeratorState.VisitingNode,   "a", 1, 0, 0),
                (TreenumeratorState.VisitingNode,   "a", 2, 0, 0),
              }
            },
            new TestScenario
            {
              SchedulingPredicate = visit => visit.Depth == 1 ? SchedulingStrategy.SkipDescendantSubtrees : SchedulingStrategy.ScheduleForTraversal,
              Description = "Skip level 1, sibling 0 descedant subtrees",
              ExpectedBreadthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, 0, 0),
                (TreenumeratorState.VisitingNode,   "a", 1, 0, 0),
                (TreenumeratorState.SchedulingNode, "b", 0, 0, 1),
                (TreenumeratorState.VisitingNode,   "a", 2, 0, 0),
                (TreenumeratorState.VisitingNode,   "b", 1, 0, 1),
                (TreenumeratorState.VisitingNode,   "b", 2, 0, 1),
              },
              ExpectedDepthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, 0, 0),
                (TreenumeratorState.VisitingNode,   "a", 1, 0, 0),
                (TreenumeratorState.SchedulingNode, "b", 0, 0, 1),
                (TreenumeratorState.VisitingNode,   "b", 1, 0, 1),
                (TreenumeratorState.VisitingNode,   "b", 2, 0, 1),
                (TreenumeratorState.VisitingNode,   "a", 2, 0, 0),
              }
            },
            new TestScenario
            {
              SchedulingPredicate = visit => visit.Depth == 2 ? SchedulingStrategy.SkipDescendantSubtrees : SchedulingStrategy.ScheduleForTraversal,
              Description = "Skip level 2, sibling 0 descedant subtrees",
              ExpectedBreadthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, 0, 0),
                (TreenumeratorState.VisitingNode,   "a", 1, 0, 0),
                (TreenumeratorState.SchedulingNode, "b", 0, 0, 1),
                (TreenumeratorState.VisitingNode,   "a", 2, 0, 0),
                (TreenumeratorState.VisitingNode,   "b", 1, 0, 1),
                (TreenumeratorState.SchedulingNode, "c", 0, 0, 2),
                (TreenumeratorState.VisitingNode,   "b", 2, 0, 1),
                (TreenumeratorState.VisitingNode,   "c", 1, 0, 2),
                (TreenumeratorState.VisitingNode,   "c", 2, 0, 2),
              },
              ExpectedDepthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, 0, 0),
                (TreenumeratorState.VisitingNode,   "a", 1, 0, 0),
                (TreenumeratorState.SchedulingNode, "b", 0, 0, 1),
                (TreenumeratorState.VisitingNode,   "b", 1, 0, 1),
                (TreenumeratorState.SchedulingNode, "c", 0, 0, 2),
                (TreenumeratorState.VisitingNode,   "c", 1, 0, 2),
                (TreenumeratorState.VisitingNode,   "c", 2, 0, 2),
                (TreenumeratorState.VisitingNode,   "b", 2, 0, 1),
                (TreenumeratorState.VisitingNode,   "a", 2, 0, 0),
              }
            },
          }
        },

        // Five-level unary tree.
        new TestTree
        {
          TreeString = "a(b(c(d(e))))",
          TestScenarios = new List<TestScenario>()
          {
            // No skipping
            new TestScenario
            {
              SchedulingPredicate = visit => SchedulingStrategy.ScheduleForTraversal,
              Description = "Traverse all",
              ExpectedBreadthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, 0, 0),
                (TreenumeratorState.VisitingNode,   "a", 1, 0, 0),
                (TreenumeratorState.SchedulingNode, "b", 0, 0, 1),
                (TreenumeratorState.VisitingNode,   "a", 2, 0, 0),
                (TreenumeratorState.VisitingNode,   "b", 1, 0, 1),
                (TreenumeratorState.SchedulingNode, "c", 0, 0, 2),
                (TreenumeratorState.VisitingNode,   "b", 2, 0, 1),
                (TreenumeratorState.VisitingNode,   "c", 1, 0, 2),
                (TreenumeratorState.SchedulingNode, "d", 0, 0, 3),
                (TreenumeratorState.VisitingNode,   "c", 2, 0, 2),
                (TreenumeratorState.VisitingNode,   "d", 1, 0, 3),
                (TreenumeratorState.SchedulingNode, "e", 0, 0, 4),
                (TreenumeratorState.VisitingNode,   "d", 2, 0, 3),
                (TreenumeratorState.VisitingNode,   "e", 1, 0, 4),
                (TreenumeratorState.VisitingNode,   "e", 2, 0, 4),
              },
              ExpectedDepthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, 0, 0),
                (TreenumeratorState.VisitingNode,   "a", 1, 0, 0),
                (TreenumeratorState.SchedulingNode, "b", 0, 0, 1),
                (TreenumeratorState.VisitingNode,   "b", 1, 0, 1),
                (TreenumeratorState.SchedulingNode, "c", 0, 0, 2),
                (TreenumeratorState.VisitingNode,   "c", 1, 0, 2),
                (TreenumeratorState.SchedulingNode, "d", 0, 0, 3),
                (TreenumeratorState.VisitingNode,   "d", 1, 0, 3),
                (TreenumeratorState.SchedulingNode, "e", 0, 0, 4),
                (TreenumeratorState.VisitingNode,   "e", 1, 0, 4),
                (TreenumeratorState.VisitingNode,   "e", 2, 0, 4),
                (TreenumeratorState.VisitingNode,   "d", 2, 0, 3),
                (TreenumeratorState.VisitingNode,   "c", 2, 0, 2),
                (TreenumeratorState.VisitingNode,   "b", 2, 0, 1),
                (TreenumeratorState.VisitingNode,   "a", 2, 0, 0),
              }
            },
            // Skip nodes
            new TestScenario
            {
              SchedulingPredicate = visit => SchedulingStrategy.SkipNode,
              Description = "Skip all nodes",
              ExpectedBreadthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, 0, 0),
                (TreenumeratorState.VisitingNode,   "a", 1, 0, 0),
                (TreenumeratorState.SchedulingNode, "b", 0, 0, 1),
                (TreenumeratorState.VisitingNode,   "b", 1, 0, 1),
                (TreenumeratorState.SchedulingNode, "c", 0, 0, 2),
                (TreenumeratorState.VisitingNode,   "c", 1, 0, 2),
                (TreenumeratorState.SchedulingNode, "d", 0, 0, 3),
                (TreenumeratorState.VisitingNode,   "d", 1, 0, 3),
                (TreenumeratorState.SchedulingNode, "e", 0, 0, 4),
                (TreenumeratorState.VisitingNode,   "e", 1, 0, 4),
                (TreenumeratorState.VisitingNode,   "e", 2, 0, 4),
                (TreenumeratorState.VisitingNode,   "d", 2, 0, 3),
                (TreenumeratorState.VisitingNode,   "c", 2, 0, 2),
                (TreenumeratorState.VisitingNode,   "b", 2, 0, 1),
                (TreenumeratorState.VisitingNode,   "a", 2, 0, 0),
              },
              ExpectedDepthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, 0, 0),
                (TreenumeratorState.VisitingNode,   "a", 1, 0, 0),
                (TreenumeratorState.SchedulingNode, "b", 0, 0, 1),
                (TreenumeratorState.VisitingNode,   "b", 1, 0, 1),
                (TreenumeratorState.SchedulingNode, "c", 0, 0, 2),
                (TreenumeratorState.VisitingNode,   "c", 1, 0, 2),
                (TreenumeratorState.SchedulingNode, "d", 0, 0, 3),
                (TreenumeratorState.VisitingNode,   "d", 1, 0, 3),
                (TreenumeratorState.SchedulingNode, "e", 0, 0, 4),
                (TreenumeratorState.VisitingNode,   "e", 1, 0, 4),
                (TreenumeratorState.VisitingNode,   "e", 2, 0, 4),
                (TreenumeratorState.VisitingNode,   "d", 2, 0, 3),
                (TreenumeratorState.VisitingNode,   "c", 2, 0, 2),
                (TreenumeratorState.VisitingNode,   "b", 2, 0, 1),
                (TreenumeratorState.VisitingNode,   "a", 2, 0, 0),
              }
            },
            new TestScenario
            {
              SchedulingPredicate = visit => visit.Depth > 0 && visit.Depth < 4 ? SchedulingStrategy.SkipNode : SchedulingStrategy.ScheduleForTraversal,
              Description = "Skip levels 1, 2, 3 node",
              ExpectedBreadthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, 0, 0),
                (TreenumeratorState.VisitingNode,   "a", 1, 0, 0),
                (TreenumeratorState.SchedulingNode, "b", 0, 0, 1),
                (TreenumeratorState.VisitingNode,   "a", 2, 0, 0),
                (TreenumeratorState.VisitingNode,   "b", 1, 0, 1),
                (TreenumeratorState.SchedulingNode, "c", 0, 0, 2),
                (TreenumeratorState.VisitingNode,   "c", 1, 0, 2),
                (TreenumeratorState.SchedulingNode, "d", 0, 0, 3),
                (TreenumeratorState.VisitingNode,   "d", 1, 0, 3),
                (TreenumeratorState.SchedulingNode, "e", 0, 0, 4),
                (TreenumeratorState.VisitingNode,   "e", 1, 0, 4),
                (TreenumeratorState.VisitingNode,   "e", 2, 0, 4),
                (TreenumeratorState.VisitingNode,   "d", 2, 0, 3),
                (TreenumeratorState.VisitingNode,   "c", 2, 0, 2),
                (TreenumeratorState.VisitingNode,   "b", 2, 0, 1),
              },
              ExpectedDepthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, 0, 0),
                (TreenumeratorState.VisitingNode,   "a", 1, 0, 0),
                (TreenumeratorState.SchedulingNode, "b", 0, 0, 1),
                (TreenumeratorState.VisitingNode,   "b", 1, 0, 1),
                (TreenumeratorState.SchedulingNode, "c", 0, 0, 2),
                (TreenumeratorState.VisitingNode,   "c", 1, 0, 2),
                (TreenumeratorState.SchedulingNode, "d", 0, 0, 3),
                (TreenumeratorState.VisitingNode,   "d", 1, 0, 3),
                (TreenumeratorState.SchedulingNode, "e", 0, 0, 4),
                (TreenumeratorState.VisitingNode,   "e", 1, 0, 4),
                (TreenumeratorState.VisitingNode,   "e", 2, 0, 4),
                (TreenumeratorState.VisitingNode,   "d", 2, 0, 3),
                (TreenumeratorState.VisitingNode,   "c", 2, 0, 2),
                (TreenumeratorState.VisitingNode,   "b", 2, 0, 1),
                (TreenumeratorState.VisitingNode,   "a", 2, 0, 0),
              }
            },
          }
        },

        // Two two-level complete binary trees.
        new TestTree
        {
          TreeString = "a(b,c),d(e,f)",
          TestScenarios = new List<TestScenario>
          {
            // No skipping
            new TestScenario
            {
              SchedulingPredicate = visit => SchedulingStrategy.ScheduleForTraversal,
              Description = "Traverse all",
              ExpectedBreadthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, 0, 0),
                (TreenumeratorState.SchedulingNode, "d", 0, 1, 0),
                (TreenumeratorState.VisitingNode,   "a", 1, 0, 0),
                (TreenumeratorState.SchedulingNode, "b", 0, 0, 1),
                (TreenumeratorState.VisitingNode,   "a", 2, 0, 0),
                (TreenumeratorState.SchedulingNode, "c", 0, 1, 1),
                (TreenumeratorState.VisitingNode,   "a", 3, 0, 0),
                (TreenumeratorState.VisitingNode,   "d", 1, 1, 0),
                (TreenumeratorState.SchedulingNode, "e", 0, 0, 1),
                (TreenumeratorState.VisitingNode,   "d", 2, 1, 0),
                (TreenumeratorState.SchedulingNode, "f", 0, 1, 1),
                (TreenumeratorState.VisitingNode,   "d", 3, 1, 0),
                (TreenumeratorState.VisitingNode,   "b", 1, 0, 1),
                (TreenumeratorState.VisitingNode,   "b", 2, 0, 1),
                (TreenumeratorState.VisitingNode,   "c", 1, 1, 1),
                (TreenumeratorState.VisitingNode,   "c", 2, 1, 1),
                (TreenumeratorState.VisitingNode,   "e", 1, 0, 1),
                (TreenumeratorState.VisitingNode,   "e", 2, 0, 1),
                (TreenumeratorState.VisitingNode,   "f", 1, 1, 1),
                (TreenumeratorState.VisitingNode,   "f", 2, 1, 1),
              },
              ExpectedDepthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, 0, 0),
                (TreenumeratorState.VisitingNode,   "a", 1, 0, 0),
                (TreenumeratorState.SchedulingNode, "b", 0, 0, 1),
                (TreenumeratorState.VisitingNode,   "b", 1, 0, 1),
                (TreenumeratorState.VisitingNode,   "b", 2, 0, 1),
                (TreenumeratorState.VisitingNode,   "a", 2, 0, 0),
                (TreenumeratorState.SchedulingNode, "c", 0, 1, 1),
                (TreenumeratorState.VisitingNode,   "c", 1, 1, 1),
                (TreenumeratorState.VisitingNode,   "c", 2, 1, 1),
                (TreenumeratorState.VisitingNode,   "a", 3, 0, 0),
                (TreenumeratorState.SchedulingNode, "d", 0, 1, 0),
                (TreenumeratorState.VisitingNode,   "d", 1, 1, 0),
                (TreenumeratorState.SchedulingNode, "e", 0, 0, 1),
                (TreenumeratorState.VisitingNode,   "e", 1, 0, 1),
                (TreenumeratorState.VisitingNode,   "e", 2, 0, 1),
                (TreenumeratorState.VisitingNode,   "d", 2, 1, 0),
                (TreenumeratorState.SchedulingNode, "f", 0, 1, 1),
                (TreenumeratorState.VisitingNode,   "f", 1, 1, 1),
                (TreenumeratorState.VisitingNode,   "f", 2, 1, 1),
                (TreenumeratorState.VisitingNode,   "d", 3, 1, 0),
              }
            },
            // Skip nodes
            new TestScenario
            {
              SchedulingPredicate = visit => SchedulingStrategy.SkipNode,
              Description = "Skip all nodes",
              ExpectedBreadthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, 0, 0),
                (TreenumeratorState.VisitingNode,   "a", 1, 0, 0),
                (TreenumeratorState.SchedulingNode, "b", 0, 0, 1),
                (TreenumeratorState.VisitingNode,   "b", 1, 0, 1),
                (TreenumeratorState.VisitingNode,   "b", 2, 0, 1),
                (TreenumeratorState.VisitingNode,   "a", 2, 0, 0),
                (TreenumeratorState.SchedulingNode, "c", 0, 1, 1),
                (TreenumeratorState.VisitingNode,   "c", 1, 1, 1),
                (TreenumeratorState.VisitingNode,   "c", 2, 1, 1),
                (TreenumeratorState.VisitingNode,   "a", 3, 0, 0),

                (TreenumeratorState.SchedulingNode, "d", 0, 1, 0),
                (TreenumeratorState.VisitingNode,   "d", 1, 1, 0),
                (TreenumeratorState.SchedulingNode, "e", 0, 0, 1),
                (TreenumeratorState.VisitingNode,   "e", 1, 0, 1),
                (TreenumeratorState.VisitingNode,   "e", 2, 0, 1),
                (TreenumeratorState.VisitingNode,   "d", 2, 1, 0),
                (TreenumeratorState.SchedulingNode, "f", 0, 1, 1),
                (TreenumeratorState.VisitingNode,   "f", 1, 1, 1),
                (TreenumeratorState.VisitingNode,   "f", 2, 1, 1),
                (TreenumeratorState.VisitingNode,   "d", 3, 1, 0),
              },
              ExpectedDepthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, 0, 0),
                (TreenumeratorState.VisitingNode,   "a", 1, 0, 0),
                (TreenumeratorState.SchedulingNode, "b", 0, 0, 1),
                (TreenumeratorState.VisitingNode,   "b", 1, 0, 1),
                (TreenumeratorState.VisitingNode,   "b", 2, 0, 1),
                (TreenumeratorState.VisitingNode,   "a", 2, 0, 0),
                (TreenumeratorState.SchedulingNode, "c", 0, 1, 1),
                (TreenumeratorState.VisitingNode,   "c", 1, 1, 1),
                (TreenumeratorState.VisitingNode,   "c", 2, 1, 1),
                (TreenumeratorState.VisitingNode,   "a", 3, 0, 0),
                (TreenumeratorState.SchedulingNode, "d", 0, 1, 0),
                (TreenumeratorState.VisitingNode,   "d", 1, 1, 0),
                (TreenumeratorState.SchedulingNode, "e", 0, 0, 1),
                (TreenumeratorState.VisitingNode,   "e", 1, 0, 1),
                (TreenumeratorState.VisitingNode,   "e", 2, 0, 1),
                (TreenumeratorState.VisitingNode,   "d", 2, 1, 0),
                (TreenumeratorState.SchedulingNode, "f", 0, 1, 1),
                (TreenumeratorState.VisitingNode,   "f", 1, 1, 1),
                (TreenumeratorState.VisitingNode,   "f", 2, 1, 1),
                (TreenumeratorState.VisitingNode,   "d", 3, 1, 0),
              }
            },
            new TestScenario
            {
              SchedulingPredicate = visit => visit.Node == "d" ? SchedulingStrategy.SkipNode : SchedulingStrategy.ScheduleForTraversal,
              Description = "Skip level 0, sibling 1 node",
              ExpectedBreadthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, 0, 0),
                (TreenumeratorState.SchedulingNode, "d", 0, 1, 0),
                (TreenumeratorState.VisitingNode,   "d", 1, 1, 0),
                (TreenumeratorState.SchedulingNode, "e", 0, 0, 1),
                (TreenumeratorState.VisitingNode,   "d", 2, 1, 0),
                (TreenumeratorState.SchedulingNode, "f", 0, 1, 1),
                (TreenumeratorState.VisitingNode,   "d", 3, 1, 0),
                (TreenumeratorState.VisitingNode,   "a", 1, 0, 0),
                (TreenumeratorState.SchedulingNode, "b", 0, 0, 1),
                (TreenumeratorState.VisitingNode,   "a", 2, 0, 0),
                (TreenumeratorState.SchedulingNode, "c", 0, 1, 1),
                (TreenumeratorState.VisitingNode,   "a", 3, 0, 0),
                (TreenumeratorState.VisitingNode,   "e", 1, 0, 1),
                (TreenumeratorState.VisitingNode,   "e", 2, 0, 1),
                (TreenumeratorState.VisitingNode,   "f", 1, 1, 1),
                (TreenumeratorState.VisitingNode,   "f", 2, 1, 1),
                (TreenumeratorState.VisitingNode,   "b", 1, 0, 1),
                (TreenumeratorState.VisitingNode,   "b", 2, 0, 1),
                (TreenumeratorState.VisitingNode,   "c", 1, 1, 1),
                (TreenumeratorState.VisitingNode,   "c", 2, 1, 1),
              },
              ExpectedDepthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, 0, 0),
                (TreenumeratorState.VisitingNode,   "a", 1, 0, 0),
                (TreenumeratorState.SchedulingNode, "b", 0, 0, 1),
                (TreenumeratorState.VisitingNode,   "b", 1, 0, 1),
                (TreenumeratorState.VisitingNode,   "b", 2, 0, 1),
                (TreenumeratorState.VisitingNode,   "a", 2, 0, 0),
                (TreenumeratorState.SchedulingNode, "c", 0, 1, 1),
                (TreenumeratorState.VisitingNode,   "c", 1, 1, 1),
                (TreenumeratorState.VisitingNode,   "c", 2, 1, 1),
                (TreenumeratorState.VisitingNode,   "a", 3, 0, 0),
                (TreenumeratorState.SchedulingNode, "d", 0, 1, 0),
                (TreenumeratorState.VisitingNode,   "d", 1, 1, 0),
                (TreenumeratorState.SchedulingNode, "e", 0, 0, 1),
                (TreenumeratorState.VisitingNode,   "e", 1, 0, 1),
                (TreenumeratorState.VisitingNode,   "e", 2, 0, 1),
                (TreenumeratorState.VisitingNode,   "d", 2, 1, 0),
                (TreenumeratorState.SchedulingNode, "f", 0, 1, 1),
                (TreenumeratorState.VisitingNode,   "f", 1, 1, 1),
                (TreenumeratorState.VisitingNode,   "f", 2, 1, 1),
                (TreenumeratorState.VisitingNode,   "d", 3, 1, 0),
              }
            },
            // Skip subtree
            new TestScenario
            {
              SchedulingPredicate = visit => visit.Node == "d" ? SchedulingStrategy.SkipSubtree : SchedulingStrategy.ScheduleForTraversal,
              Description = "Skip level 0, sibling 1 subtree",
              ExpectedBreadthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, 0, 0),
                (TreenumeratorState.SchedulingNode, "d", 0, 1, 0),
                (TreenumeratorState.VisitingNode,   "a", 1, 0, 0),
                (TreenumeratorState.SchedulingNode, "b", 0, 0, 1),
                (TreenumeratorState.VisitingNode,   "a", 2, 0, 0),
                (TreenumeratorState.SchedulingNode, "c", 0, 1, 1),
                (TreenumeratorState.VisitingNode,   "a", 3, 0, 0),
                (TreenumeratorState.VisitingNode,   "b", 1, 0, 1),
                (TreenumeratorState.VisitingNode,   "b", 2, 0, 1),
                (TreenumeratorState.VisitingNode,   "c", 1, 1, 1),
                (TreenumeratorState.VisitingNode,   "c", 2, 1, 1),
              },
              ExpectedDepthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, 0, 0),
                (TreenumeratorState.VisitingNode,   "a", 1, 0, 0),
                (TreenumeratorState.SchedulingNode, "b", 0, 0, 1),
                (TreenumeratorState.VisitingNode,   "b", 1, 0, 1),
                (TreenumeratorState.VisitingNode,   "b", 2, 0, 1),
                (TreenumeratorState.VisitingNode,   "a", 2, 0, 0),
                (TreenumeratorState.SchedulingNode, "c", 0, 1, 1),
                (TreenumeratorState.VisitingNode,   "c", 1, 1, 1),
                (TreenumeratorState.VisitingNode,   "c", 2, 1, 1),
                (TreenumeratorState.VisitingNode,   "a", 3, 0, 0),
                (TreenumeratorState.SchedulingNode, "d", 0, 1, 0),
              }
            },
            // Skip descendant subtrees
            new TestScenario
            {
              SchedulingPredicate = visit => visit.Node == "d" ? SchedulingStrategy.SkipDescendantSubtrees : SchedulingStrategy.ScheduleForTraversal,
              Description = "Skip level 0, sibling 1 descendant subtrees",
              ExpectedBreadthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, 0, 0),
                (TreenumeratorState.SchedulingNode, "d", 0, 1, 0),
                (TreenumeratorState.VisitingNode,   "a", 1, 0, 0),
                (TreenumeratorState.SchedulingNode, "b", 0, 0, 1),
                (TreenumeratorState.VisitingNode,   "a", 2, 0, 0),
                (TreenumeratorState.SchedulingNode, "c", 0, 1, 1),
                (TreenumeratorState.VisitingNode,   "a", 3, 0, 0),
                (TreenumeratorState.VisitingNode,   "d", 1, 1, 0),
                (TreenumeratorState.VisitingNode,   "d", 2, 1, 0),
                (TreenumeratorState.VisitingNode,   "b", 1, 0, 1),
                (TreenumeratorState.VisitingNode,   "b", 2, 0, 1),
                (TreenumeratorState.VisitingNode,   "c", 1, 1, 1),
                (TreenumeratorState.VisitingNode,   "c", 2, 1, 1),
              },
              ExpectedDepthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, 0, 0),
                (TreenumeratorState.VisitingNode,   "a", 1, 0, 0),
                (TreenumeratorState.SchedulingNode, "b", 0, 0, 1),
                (TreenumeratorState.VisitingNode,   "b", 1, 0, 1),
                (TreenumeratorState.VisitingNode,   "b", 2, 0, 1),
                (TreenumeratorState.VisitingNode,   "a", 2, 0, 0),
                (TreenumeratorState.SchedulingNode, "c", 0, 1, 1),
                (TreenumeratorState.VisitingNode,   "c", 1, 1, 1),
                (TreenumeratorState.VisitingNode,   "c", 2, 1, 1),
                (TreenumeratorState.VisitingNode,   "a", 3, 0, 0),
                (TreenumeratorState.SchedulingNode, "d", 0, 1, 0),
                (TreenumeratorState.VisitingNode,   "d", 1, 1, 0),
                (TreenumeratorState.VisitingNode,   "d", 2, 1, 0),
              }
            },
          }
        },

        // Three-level complete binary trees.
        new TestTree
        {
          TreeString = "a(b(d,e),c(f,g))",
          TestScenarios = new List<TestScenario>
          {
            // No skipping
            new TestScenario
            {
              SchedulingPredicate = visit => SchedulingStrategy.ScheduleForTraversal,
              Description = "Traverse all",
              ExpectedBreadthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, 0, 0),
                (TreenumeratorState.VisitingNode,   "a", 1, 0, 0),
                (TreenumeratorState.SchedulingNode, "b", 0, 0, 1),
                (TreenumeratorState.VisitingNode,   "a", 2, 0, 0),
                (TreenumeratorState.SchedulingNode, "c", 0, 1, 1),
                (TreenumeratorState.VisitingNode,   "a", 3, 0, 0),
                (TreenumeratorState.VisitingNode,   "b", 1, 0, 1),
                (TreenumeratorState.SchedulingNode, "d", 0, 0, 2),
                (TreenumeratorState.VisitingNode,   "b", 2, 0, 1),
                (TreenumeratorState.SchedulingNode, "e", 0, 1, 2),
                (TreenumeratorState.VisitingNode,   "b", 3, 0, 1),
                (TreenumeratorState.VisitingNode,   "c", 1, 1, 1),
                (TreenumeratorState.SchedulingNode, "f", 0, 0, 2),
                (TreenumeratorState.VisitingNode,   "c", 2, 1, 1),
                (TreenumeratorState.SchedulingNode, "g", 0, 1, 2),
                (TreenumeratorState.VisitingNode,   "c", 3, 1, 1),
                (TreenumeratorState.VisitingNode,   "d", 1, 0, 2),
                (TreenumeratorState.VisitingNode,   "d", 2, 0, 2),
                (TreenumeratorState.VisitingNode,   "e", 1, 1, 2),
                (TreenumeratorState.VisitingNode,   "e", 2, 1, 2),
                (TreenumeratorState.VisitingNode,   "f", 1, 0, 2),
                (TreenumeratorState.VisitingNode,   "f", 2, 0, 2),
                (TreenumeratorState.VisitingNode,   "g", 1, 1, 2),
                (TreenumeratorState.VisitingNode,   "g", 2, 1, 2),
              },
              ExpectedDepthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, 0, 0),
                (TreenumeratorState.VisitingNode,   "a", 1, 0, 0),
                (TreenumeratorState.SchedulingNode, "b", 0, 0, 1),
                (TreenumeratorState.VisitingNode,   "b", 1, 0, 1),
                (TreenumeratorState.SchedulingNode, "d", 0, 0, 2),
                (TreenumeratorState.VisitingNode,   "d", 1, 0, 2),
                (TreenumeratorState.VisitingNode,   "d", 2, 0, 2),
                (TreenumeratorState.VisitingNode,   "b", 2, 0, 1),
                (TreenumeratorState.SchedulingNode, "e", 0, 1, 2),
                (TreenumeratorState.VisitingNode,   "e", 1, 1, 2),
                (TreenumeratorState.VisitingNode,   "e", 2, 1, 2),
                (TreenumeratorState.VisitingNode,   "b", 3, 0, 1),
                (TreenumeratorState.VisitingNode,   "a", 2, 0, 0),
                (TreenumeratorState.SchedulingNode, "c", 0, 1, 1),
                (TreenumeratorState.VisitingNode,   "c", 1, 1, 1),
                (TreenumeratorState.SchedulingNode, "f", 0, 0, 2),
                (TreenumeratorState.VisitingNode,   "f", 1, 0, 2),
                (TreenumeratorState.VisitingNode,   "f", 2, 0, 2),
                (TreenumeratorState.VisitingNode,   "c", 2, 1, 1),
                (TreenumeratorState.SchedulingNode, "g", 0, 1, 2),
                (TreenumeratorState.VisitingNode,   "g", 1, 1, 2),
                (TreenumeratorState.VisitingNode,   "g", 2, 1, 2),
                (TreenumeratorState.VisitingNode,   "c", 3, 1, 1),
                (TreenumeratorState.VisitingNode,   "a", 3, 0, 0),
              }
            },
            // Skip nodes
            new TestScenario
            {
              SchedulingPredicate = visit => visit.Node == "c" ? SchedulingStrategy.SkipNode : SchedulingStrategy.ScheduleForTraversal,
              Description = "Skip level 1, sibling 1 node",
              ExpectedBreadthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, 0, 0),
                (TreenumeratorState.VisitingNode,   "a", 1, 0, 0),
                (TreenumeratorState.SchedulingNode, "b", 0, 0, 1),
                (TreenumeratorState.VisitingNode,   "a", 2, 0, 0),
                (TreenumeratorState.SchedulingNode, "c", 0, 1, 1),
                (TreenumeratorState.VisitingNode,   "a", 3, 0, 0),

                (TreenumeratorState.VisitingNode,   "b", 1, 0, 1),
                (TreenumeratorState.SchedulingNode, "d", 0, 0, 2),
                (TreenumeratorState.VisitingNode,   "b", 2, 0, 1),
                (TreenumeratorState.SchedulingNode, "e", 0, 1, 2),
                (TreenumeratorState.VisitingNode,   "b", 3, 0, 1),

                (TreenumeratorState.VisitingNode,   "c", 1, 1, 1),
                (TreenumeratorState.SchedulingNode, "f", 0, 0, 2),
                (TreenumeratorState.VisitingNode,   "f", 1, 0, 2),
                (TreenumeratorState.VisitingNode,   "f", 2, 0, 2),
                (TreenumeratorState.VisitingNode,   "c", 2, 1, 1),
                (TreenumeratorState.SchedulingNode, "g", 0, 1, 2),
                (TreenumeratorState.VisitingNode,   "g", 1, 1, 2),
                (TreenumeratorState.VisitingNode,   "g", 2, 1, 2),
                (TreenumeratorState.VisitingNode,   "c", 3, 1, 1),

                (TreenumeratorState.VisitingNode,   "d", 1, 0, 2),
                (TreenumeratorState.VisitingNode,   "d", 2, 0, 2),
                (TreenumeratorState.VisitingNode,   "e", 1, 1, 2),
                (TreenumeratorState.VisitingNode,   "e", 2, 1, 2),
              },
              ExpectedDepthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, 0, 0),
                (TreenumeratorState.VisitingNode,   "a", 1, 0, 0),
                (TreenumeratorState.SchedulingNode, "b", 0, 0, 1),
                (TreenumeratorState.VisitingNode,   "b", 1, 0, 1),
                (TreenumeratorState.SchedulingNode, "d", 0, 0, 2),
                (TreenumeratorState.VisitingNode,   "d", 1, 0, 2),
                (TreenumeratorState.VisitingNode,   "d", 2, 0, 2),
                (TreenumeratorState.VisitingNode,   "b", 2, 0, 1),
                (TreenumeratorState.SchedulingNode, "e", 0, 1, 2),
                (TreenumeratorState.VisitingNode,   "e", 1, 1, 2),
                (TreenumeratorState.VisitingNode,   "e", 2, 1, 2),
                (TreenumeratorState.VisitingNode,   "b", 3, 0, 1),
                (TreenumeratorState.VisitingNode,   "a", 2, 0, 0),
                (TreenumeratorState.SchedulingNode, "c", 0, 1, 1),
                (TreenumeratorState.VisitingNode,   "c", 1, 1, 1),
                (TreenumeratorState.SchedulingNode, "f", 0, 0, 2),
                (TreenumeratorState.VisitingNode,   "f", 1, 0, 2),
                (TreenumeratorState.VisitingNode,   "f", 2, 0, 2),
                (TreenumeratorState.VisitingNode,   "c", 2, 1, 1),
                (TreenumeratorState.SchedulingNode, "g", 0, 1, 2),
                (TreenumeratorState.VisitingNode,   "g", 1, 1, 2),
                (TreenumeratorState.VisitingNode,   "g", 2, 1, 2),
                (TreenumeratorState.VisitingNode,   "c", 3, 1, 1),
                (TreenumeratorState.VisitingNode,   "a", 3, 0, 0),
              }
            },
          }
        },

        // Two trees, single node and two-level complete binary tree.
        new TestTree
        {
          TreeString = "a,b(c,d)",
          TestScenarios = new List<TestScenario>
          {
            // No skipping
            new TestScenario
            {
              SchedulingPredicate = visit => SchedulingStrategy.ScheduleForTraversal,
              Description = "Traverse all",
              ExpectedBreadthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, 0, 0),
                (TreenumeratorState.SchedulingNode, "b", 0, 1, 0),
                (TreenumeratorState.VisitingNode,   "a", 1, 0, 0),
                (TreenumeratorState.VisitingNode,   "a", 2, 0, 0),
                (TreenumeratorState.VisitingNode,   "b", 1, 1, 0),
                (TreenumeratorState.SchedulingNode, "c", 0, 0, 1),
                (TreenumeratorState.VisitingNode,   "b", 2, 1, 0),
                (TreenumeratorState.SchedulingNode, "d", 0, 1, 1),
                (TreenumeratorState.VisitingNode,   "b", 3, 1, 0),
                (TreenumeratorState.VisitingNode,   "c", 1, 0, 1),
                (TreenumeratorState.VisitingNode,   "c", 2, 0, 1),
                (TreenumeratorState.VisitingNode,   "d", 1, 1, 1),
                (TreenumeratorState.VisitingNode,   "d", 2, 1, 1),
              },
              ExpectedDepthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, 0, 0),
                (TreenumeratorState.VisitingNode,   "a", 1, 0, 0),
                (TreenumeratorState.VisitingNode,   "a", 2, 0, 0),
                (TreenumeratorState.SchedulingNode, "b", 0, 1, 0),
                (TreenumeratorState.VisitingNode,   "b", 1, 1, 0),
                (TreenumeratorState.SchedulingNode, "c", 0, 0, 1),
                (TreenumeratorState.VisitingNode,   "c", 1, 0, 1),
                (TreenumeratorState.VisitingNode,   "c", 2, 0, 1),
                (TreenumeratorState.VisitingNode,   "b", 2, 1, 0),
                (TreenumeratorState.SchedulingNode, "d", 0, 1, 1),
                (TreenumeratorState.VisitingNode,   "d", 1, 1, 1),
                (TreenumeratorState.VisitingNode,   "d", 2, 1, 1),
                (TreenumeratorState.VisitingNode,   "b", 3, 1, 0),
              }
            },
            // Skip nodes
            new TestScenario
            {
              SchedulingPredicate = visit => SchedulingStrategy.SkipNode,
              Description = "Skip all nodes",
              ExpectedBreadthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, 0, 0),
                (TreenumeratorState.VisitingNode,   "a", 1, 0, 0),
                (TreenumeratorState.VisitingNode,   "a", 2, 0, 0),
                (TreenumeratorState.SchedulingNode, "b", 0, 1, 0),
                (TreenumeratorState.VisitingNode,   "b", 1, 1, 0),
                (TreenumeratorState.SchedulingNode, "c", 0, 0, 1),
                (TreenumeratorState.VisitingNode,   "c", 1, 0, 1),
                (TreenumeratorState.VisitingNode,   "c", 2, 0, 1),
                (TreenumeratorState.VisitingNode,   "b", 2, 1, 0),
                (TreenumeratorState.SchedulingNode, "d", 0, 1, 1),
                (TreenumeratorState.VisitingNode,   "d", 1, 1, 1),
                (TreenumeratorState.VisitingNode,   "d", 2, 1, 1),
                (TreenumeratorState.VisitingNode,   "b", 3, 1, 0),
              },
              ExpectedDepthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, 0, 0),
                (TreenumeratorState.VisitingNode,   "a", 1, 0, 0),
                (TreenumeratorState.VisitingNode,   "a", 2, 0, 0),
                (TreenumeratorState.SchedulingNode, "b", 0, 1, 0),
                (TreenumeratorState.VisitingNode,   "b", 1, 1, 0),
                (TreenumeratorState.SchedulingNode, "c", 0, 0, 1),
                (TreenumeratorState.VisitingNode,   "c", 1, 0, 1),
                (TreenumeratorState.VisitingNode,   "c", 2, 0, 1),
                (TreenumeratorState.VisitingNode,   "b", 2, 1, 0),
                (TreenumeratorState.SchedulingNode, "d", 0, 1, 1),
                (TreenumeratorState.VisitingNode,   "d", 1, 1, 1),
                (TreenumeratorState.VisitingNode,   "d", 2, 1, 1),
                (TreenumeratorState.VisitingNode,   "b", 3, 1, 0),
              }
            },
            new TestScenario
            {
              SchedulingPredicate = visit => visit.Node == "b" ? SchedulingStrategy.SkipNode : SchedulingStrategy.ScheduleForTraversal,
              Description = "Skip level 0, sibling 1 node",
              ExpectedBreadthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, 0, 0),
                (TreenumeratorState.VisitingNode,   "a", 1, 0, 0),
                (TreenumeratorState.VisitingNode,   "a", 2, 0, 0),
                (TreenumeratorState.SchedulingNode, "b", 0, 1, 0),
                (TreenumeratorState.VisitingNode,   "b", 1, 1, 0),
                (TreenumeratorState.SchedulingNode, "c", 0, 0, 1),
                (TreenumeratorState.VisitingNode,   "c", 1, 0, 1),
                (TreenumeratorState.VisitingNode,   "c", 2, 0, 1),
                (TreenumeratorState.VisitingNode,   "b", 2, 1, 0),
                (TreenumeratorState.SchedulingNode, "d", 0, 1, 1),
                (TreenumeratorState.VisitingNode,   "d", 1, 1, 1),
                (TreenumeratorState.VisitingNode,   "d", 2, 1, 1),
                (TreenumeratorState.VisitingNode,   "b", 3, 1, 0),
              },
              ExpectedDepthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, 0, 0),
                (TreenumeratorState.VisitingNode,   "a", 1, 0, 0),
                (TreenumeratorState.VisitingNode,   "a", 2, 0, 0),
                (TreenumeratorState.SchedulingNode, "b", 0, 1, 0),
                (TreenumeratorState.VisitingNode,   "b", 1, 1, 0),
                (TreenumeratorState.SchedulingNode, "c", 0, 0, 1),
                (TreenumeratorState.VisitingNode,   "c", 1, 0, 1),
                (TreenumeratorState.VisitingNode,   "c", 2, 0, 1),
                (TreenumeratorState.VisitingNode,   "b", 2, 1, 0),
                (TreenumeratorState.SchedulingNode, "d", 0, 1, 1),
                (TreenumeratorState.VisitingNode,   "d", 1, 1, 1),
                (TreenumeratorState.VisitingNode,   "d", 2, 1, 1),
                (TreenumeratorState.VisitingNode,   "b", 3, 1, 0),
              }
            },
          }
        },

        // Five-level right-heavy binary tree.
        new TestTree
        {
          TreeString = "a(b,c(d,e(f,g(h,i))))",
          TestScenarios = new List<TestScenario>
          {
            // No skipping
            new TestScenario
            {
              SchedulingPredicate = visit => SchedulingStrategy.ScheduleForTraversal,
              Description = "Traverse all",
              ExpectedBreadthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, 0, 0),
                (TreenumeratorState.VisitingNode,   "a", 1, 0, 0),
                (TreenumeratorState.SchedulingNode, "b", 0, 0, 1),
                (TreenumeratorState.VisitingNode,   "a", 2, 0, 0),
                (TreenumeratorState.SchedulingNode, "c", 0, 1, 1),
                (TreenumeratorState.VisitingNode,   "a", 3, 0, 0),
                (TreenumeratorState.VisitingNode,   "b", 1, 0, 1),
                (TreenumeratorState.VisitingNode,   "b", 2, 0, 1),
                (TreenumeratorState.VisitingNode,   "c", 1, 1, 1),
                (TreenumeratorState.SchedulingNode, "d", 0, 0, 2),
                (TreenumeratorState.VisitingNode,   "c", 2, 1, 1),
                (TreenumeratorState.SchedulingNode, "e", 0, 1, 2),
                (TreenumeratorState.VisitingNode,   "c", 3, 1, 1),
                (TreenumeratorState.VisitingNode,   "d", 1, 0, 2),
                (TreenumeratorState.VisitingNode,   "d", 2, 0, 2),
                (TreenumeratorState.VisitingNode,   "e", 1, 1, 2),
                (TreenumeratorState.SchedulingNode, "f", 0, 0, 3),
                (TreenumeratorState.VisitingNode,   "e", 2, 1, 2),
                (TreenumeratorState.SchedulingNode, "g", 0, 1, 3),
                (TreenumeratorState.VisitingNode,   "e", 3, 1, 2),
                (TreenumeratorState.VisitingNode,   "f", 1, 0, 3),
                (TreenumeratorState.VisitingNode,   "f", 2, 0, 3),
                (TreenumeratorState.VisitingNode,   "g", 1, 1, 3),
                (TreenumeratorState.SchedulingNode, "h", 0, 0, 4),
                (TreenumeratorState.VisitingNode,   "g", 2, 1, 3),
                (TreenumeratorState.SchedulingNode, "i", 0, 1, 4),
                (TreenumeratorState.VisitingNode,   "g", 3, 1, 3),
                (TreenumeratorState.VisitingNode,   "h", 1, 0, 4),
                (TreenumeratorState.VisitingNode,   "h", 2, 0, 4),
                (TreenumeratorState.VisitingNode,   "i", 1, 1, 4),
                (TreenumeratorState.VisitingNode,   "i", 2, 1, 4),
              },
              ExpectedDepthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, 0, 0),
                (TreenumeratorState.VisitingNode,   "a", 1, 0, 0),
                (TreenumeratorState.SchedulingNode, "b", 0, 0, 1),
                (TreenumeratorState.VisitingNode,   "b", 1, 0, 1),
                (TreenumeratorState.VisitingNode,   "b", 2, 0, 1),
                (TreenumeratorState.VisitingNode,   "a", 2, 0, 0),
                (TreenumeratorState.SchedulingNode, "c", 0, 1, 1),
                (TreenumeratorState.VisitingNode,   "c", 1, 1, 1),
                (TreenumeratorState.SchedulingNode, "d", 0, 0, 2),
                (TreenumeratorState.VisitingNode,   "d", 1, 0, 2),
                (TreenumeratorState.VisitingNode,   "d", 2, 0, 2),
                (TreenumeratorState.VisitingNode,   "c", 2, 1, 1),
                (TreenumeratorState.SchedulingNode, "e", 0, 1, 2),
                (TreenumeratorState.VisitingNode,   "e", 1, 1, 2),
                (TreenumeratorState.SchedulingNode, "f", 0, 0, 3),
                (TreenumeratorState.VisitingNode,   "f", 1, 0, 3),
                (TreenumeratorState.VisitingNode,   "f", 2, 0, 3),
                (TreenumeratorState.VisitingNode,   "e", 2, 1, 2),
                (TreenumeratorState.SchedulingNode, "g", 0, 1, 3),
                (TreenumeratorState.VisitingNode,   "g", 1, 1, 3),
                (TreenumeratorState.SchedulingNode, "h", 0, 0, 4),
                (TreenumeratorState.VisitingNode,   "h", 1, 0, 4),
                (TreenumeratorState.VisitingNode,   "h", 2, 0, 4),
                (TreenumeratorState.VisitingNode,   "g", 2, 1, 3),
                (TreenumeratorState.SchedulingNode, "i", 0, 1, 4),
                (TreenumeratorState.VisitingNode,   "i", 1, 1, 4),
                (TreenumeratorState.VisitingNode,   "i", 2, 1, 4),
                (TreenumeratorState.VisitingNode,   "g", 3, 1, 3),
                (TreenumeratorState.VisitingNode,   "e", 3, 1, 2),
                (TreenumeratorState.VisitingNode,   "c", 3, 1, 1),
                (TreenumeratorState.VisitingNode,   "a", 3, 0, 0),
              }
            },
            // Skip nodes
            new TestScenario
            {
              SchedulingPredicate = visit => SchedulingStrategy.SkipNode,
              Description = "Skip all nodes",
              ExpectedBreadthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, 0, 0),
                (TreenumeratorState.VisitingNode,   "a", 1, 0, 0),
                (TreenumeratorState.SchedulingNode, "b", 0, 0, 1),
                (TreenumeratorState.VisitingNode,   "b", 1, 0, 1),
                (TreenumeratorState.VisitingNode,   "b", 2, 0, 1),
                (TreenumeratorState.VisitingNode,   "a", 2, 0, 0),
                (TreenumeratorState.SchedulingNode, "c", 0, 1, 1),
                (TreenumeratorState.VisitingNode,   "c", 1, 1, 1),
                (TreenumeratorState.SchedulingNode, "d", 0, 0, 2),
                (TreenumeratorState.VisitingNode,   "d", 1, 0, 2),
                (TreenumeratorState.VisitingNode,   "d", 2, 0, 2),
                (TreenumeratorState.VisitingNode,   "c", 2, 1, 1),
                (TreenumeratorState.SchedulingNode, "e", 0, 1, 2),
                (TreenumeratorState.VisitingNode,   "e", 1, 1, 2),
                (TreenumeratorState.SchedulingNode, "f", 0, 0, 3),
                (TreenumeratorState.VisitingNode,   "f", 1, 0, 3),
                (TreenumeratorState.VisitingNode,   "f", 2, 0, 3),
                (TreenumeratorState.VisitingNode,   "e", 2, 1, 2),
                (TreenumeratorState.SchedulingNode, "g", 0, 1, 3),
                (TreenumeratorState.VisitingNode,   "g", 1, 1, 3),
                (TreenumeratorState.SchedulingNode, "h", 0, 0, 4),
                (TreenumeratorState.VisitingNode,   "h", 1, 0, 4),
                (TreenumeratorState.VisitingNode,   "h", 2, 0, 4),
                (TreenumeratorState.VisitingNode,   "g", 2, 1, 3),
                (TreenumeratorState.SchedulingNode, "i", 0, 1, 4),
                (TreenumeratorState.VisitingNode,   "i", 1, 1, 4),
                (TreenumeratorState.VisitingNode,   "i", 2, 1, 4),
                (TreenumeratorState.VisitingNode,   "g", 3, 1, 3),
                (TreenumeratorState.VisitingNode,   "e", 3, 1, 2),
                (TreenumeratorState.VisitingNode,   "c", 3, 1, 1),
                (TreenumeratorState.VisitingNode,   "a", 3, 0, 0),
              },
              ExpectedDepthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, 0, 0),
                (TreenumeratorState.VisitingNode,   "a", 1, 0, 0),
                (TreenumeratorState.SchedulingNode, "b", 0, 0, 1),
                (TreenumeratorState.VisitingNode,   "b", 1, 0, 1),
                (TreenumeratorState.VisitingNode,   "b", 2, 0, 1),
                (TreenumeratorState.VisitingNode,   "a", 2, 0, 0),
                (TreenumeratorState.SchedulingNode, "c", 0, 1, 1),
                (TreenumeratorState.VisitingNode,   "c", 1, 1, 1),
                (TreenumeratorState.SchedulingNode, "d", 0, 0, 2),
                (TreenumeratorState.VisitingNode,   "d", 1, 0, 2),
                (TreenumeratorState.VisitingNode,   "d", 2, 0, 2),
                (TreenumeratorState.VisitingNode,   "c", 2, 1, 1),
                (TreenumeratorState.SchedulingNode, "e", 0, 1, 2),
                (TreenumeratorState.VisitingNode,   "e", 1, 1, 2),
                (TreenumeratorState.SchedulingNode, "f", 0, 0, 3),
                (TreenumeratorState.VisitingNode,   "f", 1, 0, 3),
                (TreenumeratorState.VisitingNode,   "f", 2, 0, 3),
                (TreenumeratorState.VisitingNode,   "e", 2, 1, 2),
                (TreenumeratorState.SchedulingNode, "g", 0, 1, 3),
                (TreenumeratorState.VisitingNode,   "g", 1, 1, 3),
                (TreenumeratorState.SchedulingNode, "h", 0, 0, 4),
                (TreenumeratorState.VisitingNode,   "h", 1, 0, 4),
                (TreenumeratorState.VisitingNode,   "h", 2, 0, 4),
                (TreenumeratorState.VisitingNode,   "g", 2, 1, 3),
                (TreenumeratorState.SchedulingNode, "i", 0, 1, 4),
                (TreenumeratorState.VisitingNode,   "i", 1, 1, 4),
                (TreenumeratorState.VisitingNode,   "i", 2, 1, 4),
                (TreenumeratorState.VisitingNode,   "g", 3, 1, 3),
                (TreenumeratorState.VisitingNode,   "e", 3, 1, 2),
                (TreenumeratorState.VisitingNode,   "c", 3, 1, 1),
                (TreenumeratorState.VisitingNode,   "a", 3, 0, 0),
              }
            },
            new TestScenario
            {
              SchedulingPredicate =
                visit =>
                  (visit.Depth == 1 || visit.Depth == 3) && visit.SiblingIndex == 1
                  ? SchedulingStrategy.SkipNode
                  : SchedulingStrategy.ScheduleForTraversal,
              Description = "Skip levels 1 & 3, sibling 1 node",
              ExpectedBreadthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, 0, 0),
                (TreenumeratorState.VisitingNode,   "a", 1, 0, 0),
                (TreenumeratorState.SchedulingNode, "b", 0, 0, 1),
                (TreenumeratorState.VisitingNode,   "a", 2, 0, 0),
                (TreenumeratorState.SchedulingNode, "c", 0, 1, 1),
                (TreenumeratorState.VisitingNode,   "a", 3, 0, 0),
                (TreenumeratorState.VisitingNode,   "b", 1, 0, 1),
                (TreenumeratorState.VisitingNode,   "b", 2, 0, 1),
                (TreenumeratorState.VisitingNode,   "c", 1, 1, 1),
                (TreenumeratorState.SchedulingNode, "d", 0, 0, 2),
                (TreenumeratorState.VisitingNode,   "d", 1, 0, 2),
                (TreenumeratorState.VisitingNode,   "d", 2, 0, 2),
                (TreenumeratorState.VisitingNode,   "c", 2, 1, 1),
                (TreenumeratorState.SchedulingNode, "e", 0, 1, 2),
                (TreenumeratorState.VisitingNode,   "e", 1, 1, 2),
                (TreenumeratorState.SchedulingNode, "f", 0, 0, 3),
                (TreenumeratorState.VisitingNode,   "e", 2, 1, 2),
                (TreenumeratorState.SchedulingNode, "g", 0, 1, 3),
                (TreenumeratorState.VisitingNode,   "e", 3, 1, 2),
                (TreenumeratorState.VisitingNode,   "c", 3, 1, 1),
                (TreenumeratorState.VisitingNode,   "f", 1, 0, 3),
                (TreenumeratorState.VisitingNode,   "f", 2, 0, 3),
                (TreenumeratorState.VisitingNode,   "g", 1, 1, 3),
                (TreenumeratorState.SchedulingNode, "h", 0, 0, 4),
                (TreenumeratorState.VisitingNode,   "h", 1, 0, 4),
                (TreenumeratorState.VisitingNode,   "h", 2, 0, 4),
                (TreenumeratorState.VisitingNode,   "g", 2, 1, 3),
                (TreenumeratorState.SchedulingNode, "i", 0, 1, 4),
                (TreenumeratorState.VisitingNode,   "i", 1, 1, 4),
                (TreenumeratorState.VisitingNode,   "i", 2, 1, 4),
                (TreenumeratorState.VisitingNode,   "g", 3, 1, 3),
              },
              ExpectedDepthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, 0, 0),
                (TreenumeratorState.VisitingNode,   "a", 1, 0, 0),
                (TreenumeratorState.SchedulingNode, "b", 0, 0, 1),
                (TreenumeratorState.VisitingNode,   "b", 1, 0, 1),
                (TreenumeratorState.VisitingNode,   "b", 2, 0, 1),
                (TreenumeratorState.VisitingNode,   "a", 2, 0, 0),
                (TreenumeratorState.SchedulingNode, "c", 0, 1, 1),
                (TreenumeratorState.VisitingNode,   "c", 1, 1, 1),
                (TreenumeratorState.SchedulingNode, "d", 0, 0, 2),
                (TreenumeratorState.VisitingNode,   "d", 1, 0, 2),
                (TreenumeratorState.VisitingNode,   "d", 2, 0, 2),
                (TreenumeratorState.VisitingNode,   "c", 2, 1, 1),
                (TreenumeratorState.SchedulingNode, "e", 0, 1, 2),
                (TreenumeratorState.VisitingNode,   "e", 1, 1, 2),
                (TreenumeratorState.SchedulingNode, "f", 0, 0, 3),
                (TreenumeratorState.VisitingNode,   "f", 1, 0, 3),
                (TreenumeratorState.VisitingNode,   "f", 2, 0, 3),
                (TreenumeratorState.VisitingNode,   "e", 2, 1, 2),
                (TreenumeratorState.SchedulingNode, "g", 0, 1, 3),
                (TreenumeratorState.VisitingNode,   "g", 1, 1, 3),
                (TreenumeratorState.SchedulingNode, "h", 0, 0, 4),
                (TreenumeratorState.VisitingNode,   "h", 1, 0, 4),
                (TreenumeratorState.VisitingNode,   "h", 2, 0, 4),
                (TreenumeratorState.VisitingNode,   "g", 2, 1, 3),
                (TreenumeratorState.SchedulingNode, "i", 0, 1, 4),
                (TreenumeratorState.VisitingNode,   "i", 1, 1, 4),
                (TreenumeratorState.VisitingNode,   "i", 2, 1, 4),
                (TreenumeratorState.VisitingNode,   "g", 3, 1, 3),
                (TreenumeratorState.VisitingNode,   "e", 3, 1, 2),
                (TreenumeratorState.VisitingNode,   "c", 3, 1, 1),
                (TreenumeratorState.VisitingNode,   "a", 3, 0, 0),
              }
            },
            new TestScenario
            {
              SchedulingPredicate =
                visit =>
                  visit.Depth == 0 || visit.SiblingIndex == 1
                  ? SchedulingStrategy.SkipNode
                  : SchedulingStrategy.ScheduleForTraversal,
              Description = "Skip root and all sibling 1 nodes",
              ExpectedBreadthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, 0, 0),
                (TreenumeratorState.VisitingNode,   "a", 1, 0, 0),
                (TreenumeratorState.SchedulingNode, "b", 0, 0, 1),
                (TreenumeratorState.VisitingNode,   "b", 1, 0, 1),
                (TreenumeratorState.VisitingNode,   "b", 2, 0, 1),
                (TreenumeratorState.VisitingNode,   "a", 2, 0, 0),
                (TreenumeratorState.SchedulingNode, "c", 0, 1, 1),
                (TreenumeratorState.VisitingNode,   "c", 1, 1, 1),
                (TreenumeratorState.SchedulingNode, "d", 0, 0, 2),
                (TreenumeratorState.VisitingNode,   "d", 1, 0, 2),
                (TreenumeratorState.VisitingNode,   "d", 2, 0, 2),
                (TreenumeratorState.VisitingNode,   "c", 2, 1, 1),
                (TreenumeratorState.SchedulingNode, "e", 0, 1, 2),
                (TreenumeratorState.VisitingNode,   "e", 1, 1, 2),
                (TreenumeratorState.SchedulingNode, "f", 0, 0, 3),
                (TreenumeratorState.VisitingNode,   "f", 1, 0, 3),
                (TreenumeratorState.VisitingNode,   "f", 2, 0, 3),
                (TreenumeratorState.VisitingNode,   "e", 2, 1, 2),
                (TreenumeratorState.SchedulingNode, "g", 0, 1, 3),
                (TreenumeratorState.VisitingNode,   "g", 1, 1, 3),
                (TreenumeratorState.SchedulingNode, "h", 0, 0, 4),
                (TreenumeratorState.VisitingNode,   "h", 1, 0, 4),
                (TreenumeratorState.VisitingNode,   "h", 2, 0, 4),
                (TreenumeratorState.VisitingNode,   "g", 2, 1, 3),
                (TreenumeratorState.SchedulingNode, "i", 0, 1, 4),
                (TreenumeratorState.VisitingNode,   "i", 1, 1, 4),
                (TreenumeratorState.VisitingNode,   "i", 2, 1, 4),
                (TreenumeratorState.VisitingNode,   "g", 3, 1, 3),
                (TreenumeratorState.VisitingNode,   "e", 3, 1, 2),
                (TreenumeratorState.VisitingNode,   "c", 3, 1, 1),
                (TreenumeratorState.VisitingNode,   "a", 3, 0, 0),
              },
              ExpectedDepthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, 0, 0),
                (TreenumeratorState.VisitingNode,   "a", 1, 0, 0),
                (TreenumeratorState.SchedulingNode, "b", 0, 0, 1),
                (TreenumeratorState.VisitingNode,   "b", 1, 0, 1),
                (TreenumeratorState.VisitingNode,   "b", 2, 0, 1),
                (TreenumeratorState.VisitingNode,   "a", 2, 0, 0),
                (TreenumeratorState.SchedulingNode, "c", 0, 1, 1),
                (TreenumeratorState.VisitingNode,   "c", 1, 1, 1),
                (TreenumeratorState.SchedulingNode, "d", 0, 0, 2),
                (TreenumeratorState.VisitingNode,   "d", 1, 0, 2),
                (TreenumeratorState.VisitingNode,   "d", 2, 0, 2),
                (TreenumeratorState.VisitingNode,   "c", 2, 1, 1),
                (TreenumeratorState.SchedulingNode, "e", 0, 1, 2),
                (TreenumeratorState.VisitingNode,   "e", 1, 1, 2),
                (TreenumeratorState.SchedulingNode, "f", 0, 0, 3),
                (TreenumeratorState.VisitingNode,   "f", 1, 0, 3),
                (TreenumeratorState.VisitingNode,   "f", 2, 0, 3),
                (TreenumeratorState.VisitingNode,   "e", 2, 1, 2),
                (TreenumeratorState.SchedulingNode, "g", 0, 1, 3),
                (TreenumeratorState.VisitingNode,   "g", 1, 1, 3),
                (TreenumeratorState.SchedulingNode, "h", 0, 0, 4),
                (TreenumeratorState.VisitingNode,   "h", 1, 0, 4),
                (TreenumeratorState.VisitingNode,   "h", 2, 0, 4),
                (TreenumeratorState.VisitingNode,   "g", 2, 1, 3),
                (TreenumeratorState.SchedulingNode, "i", 0, 1, 4),
                (TreenumeratorState.VisitingNode,   "i", 1, 1, 4),
                (TreenumeratorState.VisitingNode,   "i", 2, 1, 4),
                (TreenumeratorState.VisitingNode,   "g", 3, 1, 3),
                (TreenumeratorState.VisitingNode,   "e", 3, 1, 2),
                (TreenumeratorState.VisitingNode,   "c", 3, 1, 1),
                (TreenumeratorState.VisitingNode,   "a", 3, 0, 0),
              }
            },
          }
        },

        // Three-level ternary arrow fletching.
        new TestTree
        {
          TreeString = "a(b,c(e,f,g),d)",
          TestScenarios = new List<TestScenario>
          {
            // No skipping
            new TestScenario
            {
              SchedulingPredicate = visit => SchedulingStrategy.ScheduleForTraversal,
              Description = "Traverse all",
              ExpectedBreadthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, 0, 0),
                (TreenumeratorState.VisitingNode,   "a", 1, 0, 0),
                (TreenumeratorState.SchedulingNode, "b", 0, 0, 1),
                (TreenumeratorState.VisitingNode,   "a", 2, 0, 0),
                (TreenumeratorState.SchedulingNode, "c", 0, 1, 1),
                (TreenumeratorState.VisitingNode,   "a", 3, 0, 0),
                (TreenumeratorState.SchedulingNode, "d", 0, 2, 1),
                (TreenumeratorState.VisitingNode,   "a", 4, 0, 0),
                (TreenumeratorState.VisitingNode,   "b", 1, 0, 1),
                (TreenumeratorState.VisitingNode,   "b", 2, 0, 1),
                (TreenumeratorState.VisitingNode,   "c", 1, 1, 1),
                (TreenumeratorState.SchedulingNode, "e", 0, 0, 2),
                (TreenumeratorState.VisitingNode,   "c", 2, 1, 1),
                (TreenumeratorState.SchedulingNode, "f", 0, 1, 2),
                (TreenumeratorState.VisitingNode,   "c", 3, 1, 1),
                (TreenumeratorState.SchedulingNode, "g", 0, 2, 2),
                (TreenumeratorState.VisitingNode,   "c", 4, 1, 1),
                (TreenumeratorState.VisitingNode,   "d", 1, 2, 1),
                (TreenumeratorState.VisitingNode,   "d", 2, 2, 1),
                (TreenumeratorState.VisitingNode,   "e", 1, 0, 2),
                (TreenumeratorState.VisitingNode,   "e", 2, 0, 2),
                (TreenumeratorState.VisitingNode,   "f", 1, 1, 2),
                (TreenumeratorState.VisitingNode,   "f", 2, 1, 2),
                (TreenumeratorState.VisitingNode,   "g", 1, 2, 2),
                (TreenumeratorState.VisitingNode,   "g", 2, 2, 2),
              },
              ExpectedDepthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, 0, 0),
                (TreenumeratorState.VisitingNode,   "a", 1, 0, 0),
                (TreenumeratorState.SchedulingNode, "b", 0, 0, 1),
                (TreenumeratorState.VisitingNode,   "b", 1, 0, 1),
                (TreenumeratorState.VisitingNode,   "b", 2, 0, 1),
                (TreenumeratorState.VisitingNode,   "a", 2, 0, 0),
                (TreenumeratorState.SchedulingNode, "c", 0, 1, 1),
                (TreenumeratorState.VisitingNode,   "c", 1, 1, 1),
                (TreenumeratorState.SchedulingNode, "e", 0, 0, 2),
                (TreenumeratorState.VisitingNode,   "e", 1, 0, 2),
                (TreenumeratorState.VisitingNode,   "e", 2, 0, 2),
                (TreenumeratorState.VisitingNode,   "c", 2, 1, 1),
                (TreenumeratorState.SchedulingNode, "f", 0, 1, 2),
                (TreenumeratorState.VisitingNode,   "f", 1, 1, 2),
                (TreenumeratorState.VisitingNode,   "f", 2, 1, 2),
                (TreenumeratorState.VisitingNode,   "c", 3, 1, 1),
                (TreenumeratorState.SchedulingNode, "g", 0, 2, 2),
                (TreenumeratorState.VisitingNode,   "g", 1, 2, 2),
                (TreenumeratorState.VisitingNode,   "g", 2, 2, 2),
                (TreenumeratorState.VisitingNode,   "c", 4, 1, 1),
                (TreenumeratorState.VisitingNode,   "a", 3, 0, 0),
                (TreenumeratorState.SchedulingNode, "d", 0, 2, 1),
                (TreenumeratorState.VisitingNode,   "d", 1, 2, 1),
                (TreenumeratorState.VisitingNode,   "d", 2, 2, 1),
                (TreenumeratorState.VisitingNode,   "a", 4, 0, 0),
              }
            },
            // Skip nodes
            new TestScenario
            {
              SchedulingPredicate = visit => SchedulingStrategy.SkipNode,
              Description = "Skip all nodes",
              ExpectedBreadthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, 0, 0),
                (TreenumeratorState.VisitingNode,   "a", 1, 0, 0),
                (TreenumeratorState.SchedulingNode, "b", 0, 0, 1),
                (TreenumeratorState.VisitingNode,   "b", 1, 0, 1),
                (TreenumeratorState.VisitingNode,   "b", 2, 0, 1),
                (TreenumeratorState.VisitingNode,   "a", 2, 0, 0),
                (TreenumeratorState.SchedulingNode, "c", 0, 1, 1),
                (TreenumeratorState.VisitingNode,   "c", 1, 1, 1),
                (TreenumeratorState.SchedulingNode, "e", 0, 0, 2),
                (TreenumeratorState.VisitingNode,   "e", 1, 0, 2),
                (TreenumeratorState.VisitingNode,   "e", 2, 0, 2),
                (TreenumeratorState.VisitingNode,   "c", 2, 1, 1),
                (TreenumeratorState.SchedulingNode, "f", 0, 1, 2),
                (TreenumeratorState.VisitingNode,   "f", 1, 1, 2),
                (TreenumeratorState.VisitingNode,   "f", 2, 1, 2),
                (TreenumeratorState.VisitingNode,   "c", 3, 1, 1),
                (TreenumeratorState.SchedulingNode, "g", 0, 2, 2),
                (TreenumeratorState.VisitingNode,   "g", 1, 2, 2),
                (TreenumeratorState.VisitingNode,   "g", 2, 2, 2),
                (TreenumeratorState.VisitingNode,   "c", 4, 1, 1),
                (TreenumeratorState.VisitingNode,   "a", 3, 0, 0),
                (TreenumeratorState.SchedulingNode, "d", 0, 2, 1),
                (TreenumeratorState.VisitingNode,   "d", 1, 2, 1),
                (TreenumeratorState.VisitingNode,   "d", 2, 2, 1),
                (TreenumeratorState.VisitingNode,   "a", 4, 0, 0),
              },
              ExpectedDepthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, 0, 0),
                (TreenumeratorState.VisitingNode,   "a", 1, 0, 0),
                (TreenumeratorState.SchedulingNode, "b", 0, 0, 1),
                (TreenumeratorState.VisitingNode,   "b", 1, 0, 1),
                (TreenumeratorState.VisitingNode,   "b", 2, 0, 1),
                (TreenumeratorState.VisitingNode,   "a", 2, 0, 0),
                (TreenumeratorState.SchedulingNode, "c", 0, 1, 1),
                (TreenumeratorState.VisitingNode,   "c", 1, 1, 1),
                (TreenumeratorState.SchedulingNode, "e", 0, 0, 2),
                (TreenumeratorState.VisitingNode,   "e", 1, 0, 2),
                (TreenumeratorState.VisitingNode,   "e", 2, 0, 2),
                (TreenumeratorState.VisitingNode,   "c", 2, 1, 1),
                (TreenumeratorState.SchedulingNode, "f", 0, 1, 2),
                (TreenumeratorState.VisitingNode,   "f", 1, 1, 2),
                (TreenumeratorState.VisitingNode,   "f", 2, 1, 2),
                (TreenumeratorState.VisitingNode,   "c", 3, 1, 1),
                (TreenumeratorState.SchedulingNode, "g", 0, 2, 2),
                (TreenumeratorState.VisitingNode,   "g", 1, 2, 2),
                (TreenumeratorState.VisitingNode,   "g", 2, 2, 2),
                (TreenumeratorState.VisitingNode,   "c", 4, 1, 1),
                (TreenumeratorState.VisitingNode,   "a", 3, 0, 0),
                (TreenumeratorState.SchedulingNode, "d", 0, 2, 1),
                (TreenumeratorState.VisitingNode,   "d", 1, 2, 1),
                (TreenumeratorState.VisitingNode,   "d", 2, 2, 1),
                (TreenumeratorState.VisitingNode,   "a", 4, 0, 0),
              }
            },
            new TestScenario
            {
              SchedulingPredicate = visit => visit.Depth == 1 && visit.SiblingIndex == 1 ? SchedulingStrategy.SkipNode : SchedulingStrategy.ScheduleForTraversal,
              Description = "Skip level 1, sibling 1 node",
              ExpectedBreadthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, 0, 0),
                (TreenumeratorState.VisitingNode,   "a", 1, 0, 0),
                (TreenumeratorState.SchedulingNode, "b", 0, 0, 1),
                (TreenumeratorState.VisitingNode,   "a", 2, 0, 0),
                (TreenumeratorState.SchedulingNode, "c", 0, 1, 1),
                (TreenumeratorState.VisitingNode,   "a", 3, 0, 0),
                (TreenumeratorState.SchedulingNode, "d", 0, 2, 1),
                (TreenumeratorState.VisitingNode,   "a", 4, 0, 0),
                (TreenumeratorState.VisitingNode,   "b", 1, 0, 1),
                (TreenumeratorState.VisitingNode,   "b", 2, 0, 1),
                (TreenumeratorState.VisitingNode,   "c", 1, 1, 1),
                (TreenumeratorState.SchedulingNode, "e", 0, 0, 2),
                (TreenumeratorState.VisitingNode,   "e", 1, 0, 2),
                (TreenumeratorState.VisitingNode,   "e", 2, 0, 2),
                (TreenumeratorState.VisitingNode,   "c", 2, 1, 1),
                (TreenumeratorState.SchedulingNode, "f", 0, 1, 2),
                (TreenumeratorState.VisitingNode,   "f", 1, 1, 2),
                (TreenumeratorState.VisitingNode,   "f", 2, 1, 2),
                (TreenumeratorState.VisitingNode,   "c", 3, 1, 1),
                (TreenumeratorState.SchedulingNode, "g", 0, 2, 2),
                (TreenumeratorState.VisitingNode,   "g", 1, 2, 2),
                (TreenumeratorState.VisitingNode,   "g", 2, 2, 2),
                (TreenumeratorState.VisitingNode,   "c", 4, 1, 1),
                (TreenumeratorState.VisitingNode,   "d", 1, 2, 1),
                (TreenumeratorState.VisitingNode,   "d", 2, 2, 1),
              },
              ExpectedDepthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, 0, 0),
                (TreenumeratorState.VisitingNode,   "a", 1, 0, 0),
                (TreenumeratorState.SchedulingNode, "b", 0, 0, 1),
                (TreenumeratorState.VisitingNode,   "b", 1, 0, 1),
                (TreenumeratorState.VisitingNode,   "b", 2, 0, 1),
                (TreenumeratorState.VisitingNode,   "a", 2, 0, 0),
                (TreenumeratorState.SchedulingNode, "c", 0, 1, 1),
                (TreenumeratorState.VisitingNode,   "c", 1, 1, 1),
                (TreenumeratorState.SchedulingNode, "e", 0, 0, 2),
                (TreenumeratorState.VisitingNode,   "e", 1, 0, 2),
                (TreenumeratorState.VisitingNode,   "e", 2, 0, 2),
                (TreenumeratorState.VisitingNode,   "c", 2, 1, 1),
                (TreenumeratorState.SchedulingNode, "f", 0, 1, 2),
                (TreenumeratorState.VisitingNode,   "f", 1, 1, 2),
                (TreenumeratorState.VisitingNode,   "f", 2, 1, 2),
                (TreenumeratorState.VisitingNode,   "c", 3, 1, 1),
                (TreenumeratorState.SchedulingNode, "g", 0, 2, 2),
                (TreenumeratorState.VisitingNode,   "g", 1, 2, 2),
                (TreenumeratorState.VisitingNode,   "g", 2, 2, 2),
                (TreenumeratorState.VisitingNode,   "c", 4, 1, 1),
                (TreenumeratorState.VisitingNode,   "a", 3, 0, 0),
                (TreenumeratorState.SchedulingNode, "d", 0, 2, 1),
                (TreenumeratorState.VisitingNode,   "d", 1, 2, 1),
                (TreenumeratorState.VisitingNode,   "d", 2, 2, 1),
                (TreenumeratorState.VisitingNode,   "a", 4, 0, 0),
              }
            },
            // Skip subtree
            new TestScenario
            {
              SchedulingPredicate = visit => visit.Depth == 1 && visit.SiblingIndex == 1 ? SchedulingStrategy.SkipSubtree : SchedulingStrategy.ScheduleForTraversal,
              Description = "Skip level 1, sibling 1 subtree",
              ExpectedBreadthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, 0, 0),
                (TreenumeratorState.VisitingNode,   "a", 1, 0, 0),
                (TreenumeratorState.SchedulingNode, "b", 0, 0, 1),
                (TreenumeratorState.VisitingNode,   "a", 2, 0, 0),
                (TreenumeratorState.SchedulingNode, "c", 0, 1, 1),
                (TreenumeratorState.VisitingNode,   "a", 3, 0, 0),
                (TreenumeratorState.SchedulingNode, "d", 0, 2, 1),
                (TreenumeratorState.VisitingNode,   "a", 4, 0, 0),
                (TreenumeratorState.VisitingNode,   "b", 1, 0, 1),
                (TreenumeratorState.VisitingNode,   "b", 2, 0, 1),
                (TreenumeratorState.VisitingNode,   "d", 1, 2, 1),
                (TreenumeratorState.VisitingNode,   "d", 2, 2, 1),
              },
              ExpectedDepthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, 0, 0),
                (TreenumeratorState.VisitingNode,   "a", 1, 0, 0),
                (TreenumeratorState.SchedulingNode, "b", 0, 0, 1),
                (TreenumeratorState.VisitingNode,   "b", 1, 0, 1),
                (TreenumeratorState.VisitingNode,   "b", 2, 0, 1),
                (TreenumeratorState.VisitingNode,   "a", 2, 0, 0),
                (TreenumeratorState.SchedulingNode, "c", 0, 1, 1),
                (TreenumeratorState.VisitingNode,   "a", 3, 0, 0),
                (TreenumeratorState.SchedulingNode, "d", 0, 2, 1),
                (TreenumeratorState.VisitingNode,   "d", 1, 2, 1),
                (TreenumeratorState.VisitingNode,   "d", 2, 2, 1),
                (TreenumeratorState.VisitingNode,   "a", 4, 0, 0),
              }
            },
            // Skip descendant subtrees
            new TestScenario
            {
              SchedulingPredicate = visit => visit.Depth == 1 && visit.SiblingIndex == 1 ? SchedulingStrategy.SkipDescendantSubtrees : SchedulingStrategy.ScheduleForTraversal,
              Description = "Skip level 1, sibling 1 descendant subtree",
              ExpectedBreadthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, 0, 0),
                (TreenumeratorState.VisitingNode,   "a", 1, 0, 0),
                (TreenumeratorState.SchedulingNode, "b", 0, 0, 1),
                (TreenumeratorState.VisitingNode,   "a", 2, 0, 0),
                (TreenumeratorState.SchedulingNode, "c", 0, 1, 1),
                (TreenumeratorState.VisitingNode,   "a", 3, 0, 0),
                (TreenumeratorState.SchedulingNode, "d", 0, 2, 1),
                (TreenumeratorState.VisitingNode,   "a", 4, 0, 0),
                (TreenumeratorState.VisitingNode,   "b", 1, 0, 1),
                (TreenumeratorState.VisitingNode,   "b", 2, 0, 1),
                (TreenumeratorState.VisitingNode,   "c", 1, 1, 1),
                (TreenumeratorState.VisitingNode,   "c", 2, 1, 1),
                (TreenumeratorState.VisitingNode,   "d", 1, 2, 1),
                (TreenumeratorState.VisitingNode,   "d", 2, 2, 1),
              },
              ExpectedDepthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, 0, 0),
                (TreenumeratorState.VisitingNode,   "a", 1, 0, 0),
                (TreenumeratorState.SchedulingNode, "b", 0, 0, 1),
                (TreenumeratorState.VisitingNode,   "b", 1, 0, 1),
                (TreenumeratorState.VisitingNode,   "b", 2, 0, 1),
                (TreenumeratorState.VisitingNode,   "a", 2, 0, 0),
                (TreenumeratorState.SchedulingNode, "c", 0, 1, 1),
                (TreenumeratorState.VisitingNode,   "c", 1, 1, 1),
                (TreenumeratorState.VisitingNode,   "c", 2, 1, 1),
                (TreenumeratorState.VisitingNode,   "a", 3, 0, 0),
                (TreenumeratorState.SchedulingNode, "d", 0, 2, 1),
                (TreenumeratorState.VisitingNode,   "d", 1, 2, 1),
                (TreenumeratorState.VisitingNode,   "d", 2, 2, 1),
                (TreenumeratorState.VisitingNode,   "a", 4, 0, 0),
              }
            },
          }
        }
      };
  }
}
