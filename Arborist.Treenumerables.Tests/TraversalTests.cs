using Arborist.Tests.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace Arborist.Treenumerables.Tests
{
  [TestClass]
  public class TraversalTests
  {
    public static IEnumerable<object[]> GetTestData()
    {
      foreach (var tuple in TestDataFactory.GetTestInput())
        yield return new object[]
        {
          tuple.Item1,
          tuple.Item2,
          tuple.Item3,
          tuple.Item4
        };
    }

    public static string GetTestDisplayName(MethodInfo methodInfo, object[] data)
    {
      return $"{data[0]} -> {data[1]}";
    }

    [TestMethod]
    [DynamicData(nameof(GetTestData), DynamicDataSourceType.Method, DynamicDataDisplayName = nameof(GetTestDisplayName))]
    public void DepthFirst(
      string treeString,
      string testDescription,
      int testTreeIndex,
      int testScenarioIndex)
    {
      // Arrange
      var treenumerable = TreeStringParser.ParseTreeString(treeString);
      var testScenario = TestDataFactory.TestTrees[testTreeIndex].TestScenarios[testScenarioIndex];

      var expected = testScenario.ExpectedDepthFirstResults;

      Debug.WriteLine("-----Expected Values-----");
      foreach (var value in expected)
        Debug.WriteLine(value);

      // Act
      Debug.WriteLine("\r\n-----Actual Values-----");
      var actual =
        treenumerable
        .ToDepthFirstMoveNext(testScenario.SchedulingPredicate)
        .Do(x => Debug.WriteLine(x))
        .ToArray();

      // Assert
      CollectionAssert.AreEqual(expected, actual);
    }

    [TestMethod]
    [DynamicData(nameof(GetTestData), DynamicDataSourceType.Method, DynamicDataDisplayName = nameof(GetTestDisplayName))]
    public void BreadthFirst(
      string treeString,
      string testDescription,
      int testTreeIndex,
      int testScenarioIndex)
    {
      // Arrange
      var treenumerable = TreeStringParser.ParseTreeString(treeString);
      var testScenario = TestDataFactory.TestTrees[testTreeIndex].TestScenarios[testScenarioIndex];

      var expected = testScenario.ExpectedBreadthFirstResults;

      Debug.WriteLine("-----Expected Values-----");
      foreach (var value in expected)
        Debug.WriteLine(value);

      // Act
      Debug.WriteLine("\r\n-----Actual Values-----");
      var actual =
        treenumerable
        .ToBreadthFirstMoveNext(testScenario.SchedulingPredicate)
        .Do(x => Debug.WriteLine(x))
        .ToArray();

      // Assert
      CollectionAssert.AreEqual(expected, actual);
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
      var treenumerable = TreeStringParser.ParseTreeString(treeString);
      var testScenario = TestDataFactory.TestTrees[testTreeIndex].TestScenarios[testScenarioIndex];

      MoveNextResult<string>[] Sort(IEnumerable<MoveNextResult<string>> nodes) =>
        nodes
        .OrderBy(x => (x.State, x.Depth, x.SiblingIndex, x.Node))
        .ToArray();

      // Act
      Debug.WriteLine("-----Breadth First-----");
      var breadthFirst =
        treenumerable
        .ToBreadthFirstMoveNext(testScenario.SchedulingPredicate)
        .Do(x => Debug.WriteLine(x))
        .ToArray();

      Debug.WriteLine("\r\n-----Depth First------");
      var depthFirst =
        treenumerable
        .ToDepthFirstMoveNext(testScenario.SchedulingPredicate)
        .Do(x => Debug.WriteLine(x))
        .ToArray();

      // Assert
      CollectionAssert.AreEqual(Sort(breadthFirst), Sort(depthFirst));
    }
  }
}
