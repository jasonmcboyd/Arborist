using Arborist.Core;
using Arborist.Nodes;
using Arborist.TestUtils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Arborist.Linq.Tests
{
  [TestClass]
  public class SelectManyTests
  {
    static SelectManyTests()
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
              SchedulingPredicate = visit => SchedulingStrategy.TraverseSubtree,
              TreenumerableMap = treenumerable => treenumerable.PruneAfter(_ => true),
              Description = "Traverse all, prune after all",
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
        new TreeTestDefinition
        {
          TreeString = "a,b,c",
          TestScenarios = new List<TestScenario>
          {
            // No skipping
            new TestScenario
            {
              SchedulingPredicate = visit => SchedulingStrategy.TraverseSubtree,
              TreenumerableMap = treenumerable => treenumerable.PruneAfter(_ => true),
              Description = "Traverse all, prune after all",
              ExpectedBreadthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "b", 0, (1, 0), (1, 0)),
                (TreenumeratorState.SchedulingNode, "c", 0, (2, 0), (2, 0)),
                (TreenumeratorState.VisitingNode,   "a", 1, (0, 0), (0, 0)),
                (TreenumeratorState.VisitingNode,   "a", 2, (0, 0), (0, 0)),
                (TreenumeratorState.VisitingNode,   "b", 1, (1, 0), (1, 0)),
                (TreenumeratorState.VisitingNode,   "b", 2, (1, 0), (1, 0)),
                (TreenumeratorState.VisitingNode,   "c", 1, (2, 0), (2, 0)),
                (TreenumeratorState.VisitingNode,   "c", 2, (2, 0), (2, 0)),
              },
              ExpectedDepthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, (0, 0), (0, 0)),
                (TreenumeratorState.VisitingNode,   "a", 1, (0, 0), (0, 0)),
                (TreenumeratorState.VisitingNode,   "a", 2, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "b", 0, (1, 0), (1, 0)),
                (TreenumeratorState.VisitingNode,   "b", 1, (1, 0), (1, 0)),
                (TreenumeratorState.VisitingNode,   "b", 2, (1, 0), (1, 0)),
                (TreenumeratorState.SchedulingNode, "c", 0, (2, 0), (2, 0)),
                (TreenumeratorState.VisitingNode,   "c", 1, (2, 0), (2, 0)),
                (TreenumeratorState.VisitingNode,   "c", 2, (2, 0), (2, 0)),
              }
            },
            // Skip subtree
            new TestScenario
            {
              SchedulingPredicate = visit => SchedulingStrategy.SkipSubtree,
              TreenumerableMap = treenumerable => treenumerable.PruneAfter(_ => true),
              Description = "Skip all subtrees, prune after all",
              ExpectedBreadthFirstResults = new MoveNextResult<string>[]
              {
              },
              ExpectedDepthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, (0, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "b", 0, (1, 0), (0, 0)),
                (TreenumeratorState.SchedulingNode, "c", 0, (2, 0), (0, 0)),
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
              TreenumerableMap = treenumerable => treenumerable.PruneAfter(_ => true),
              Description = "Traverse all, prune after all",
              ExpectedBreadthFirstResults = new MoveNextResult<string>[]
              {
              },
              ExpectedDepthFirstResults = new MoveNextResult<string>[]
              {
                (TreenumeratorState.SchedulingNode, "a", 0, (0, 0), (0, 0)),
                (TreenumeratorState.VisitingNode,   "a", 1, (0, 0), (0, 0)),
                (TreenumeratorState.VisitingNode,   "a", 2, (0, 0), (0, 0)),
              }
            },
          }
        }
      };

    [TestMethod]
    public void SelectMany()
    {
      // Arrange
      var subtree = IndexableTreeNode.Create(1, 2, 3).ToTreenumerable();

      var root = IndexableTreeNode.Create(subtree, subtree, subtree);

      var treenumerable = root.ToTreenumerable();

      // Act
      var actual =
        treenumerable
        .SelectMany()
        .PreOrderTraversal()
        .ToArray();

      // Assert
      var expected = new[] { 1, 2, 3 };

      CollectionAssert.AreEqual(expected, actual);
    }
  }
}
