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
  public class PruneAfterTests
  {
    static PruneAfterTests()
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
              Description = "Prune after all, traverse all",
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
              Description = "Prune after all, traverse all",
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
            // Skip subtree
            new TestScenario
            {
              SchedulingPredicate = visit => SchedulingStrategy.SkipSubtree,
              TreenumerableMap = treenumerable => treenumerable.PruneAfter(_ => true),
              Description = "Prune after all, Skip all subtrees",
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
              Description = "Prune after all, traverse all",
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
          }
        }
      };

    [TestMethod]
    [DynamicData(nameof(GetTestData), DynamicDataSourceType.Method, DynamicDataDisplayName = nameof(GetTestDisplayName))]
    public void PruneAfter_DepthFirst(
      string treeString,
      string testDescription,
      int testIndex,
      int testScenarioIndex)
    {
      // Arrange
      var testScenario = _TreenumerableTestDataFactory.GetTestScenario(testIndex, testScenarioIndex);

      var treenumerable = testScenario.TreenumerableMap(TreeSerializer.Deserialize(treeString));

      var expected = testScenario.ExpectedDepthFirstResults;

      Debug.WriteLine("-----Expected Values-----");
      foreach (var value in expected)
        Debug.WriteLine(value);

      // Act
      Debug.WriteLine($"{Environment.NewLine}-----Actual Values-----");
      var actual =
        treenumerable
        .ToDepthFirstMoveNext(testScenario.SchedulingPredicate)
        .Do(visit => Debug.WriteLine(visit))
        .ToArray();

      // Assert
      CollectionAssert.AreEqual(expected, actual);
    }

    [TestMethod]
    [DynamicData(nameof(GetTestData), DynamicDataSourceType.Method, DynamicDataDisplayName = nameof(GetTestDisplayName))]
    public void PruneAfter_BreadthFirst(
      string treeString,
      string testDescription,
      int testIndex,
      int testScenarioIndex)
    {
      // Arrange
      var testScenario = _TreenumerableTestDataFactory.GetTestScenario(testIndex, testScenarioIndex);

      var treenumerable = testScenario.TreenumerableMap(TreeSerializer.Deserialize(treeString));

      var expected = testScenario.ExpectedBreadthFirstResults;

      Debug.WriteLine("-----Expected Values-----");
      foreach (var value in expected)
        Debug.WriteLine(value);

      Debug.WriteLine($"{Environment.NewLine}-----Actual Before Values-----");
      treenumerable
      .ToBreadthFirstMoveNext(visit => SchedulingStrategy.TraverseSubtree)
      .Do(visit => Debug.WriteLine(visit))
      .ToArray();

      // Act
      Debug.WriteLine($"{Environment.NewLine}-----Actual Values-----");
      var actual =
        treenumerable
        .ToBreadthFirstMoveNext(testScenario.SchedulingPredicate)
        .Do(visit => Debug.WriteLine(visit))
        .ToArray();

      // Assert
      CollectionAssert.AreEqual(expected, actual);
    }
  }
}

