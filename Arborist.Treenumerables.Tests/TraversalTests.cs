using Arborist.Linq;
using Arborist.Tests.Utils;
using Arborist.Treenumerables.SimpleSerializer;
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
    static TraversalTests()
    {
      _TreenumerableTestDataFactory = new TreenumerableTestDataFactory(TreeTraversalTestData.TestTrees); 
    }

    private static TreenumerableTestDataFactory _TreenumerableTestDataFactory;

    public static IEnumerable<object[]> GetTestData()
      => _TreenumerableTestDataFactory.GetTestData();

    public static string GetTestDisplayName(MethodInfo methodInfo, object[] data)
      => _TreenumerableTestDataFactory.GetTestDisplayName(methodInfo, data);

    [TestMethod]
    [DynamicData(nameof(GetTestData), DynamicDataSourceType.Method, DynamicDataDisplayName = nameof(GetTestDisplayName))]
    public void DepthFirst(
      string treeString,
      string testDescription,
      int testTreeIndex,
      int testScenarioIndex)
    {
      // Arrange
      var treenumerable = TreeSerializer.Deserialize(treeString);
      var testScenario = TreeTraversalTestData.TestTrees[testTreeIndex].TestScenarios[testScenarioIndex];

      var expected = testScenario.ExpectedDepthFirstResults;

      Debug.WriteLine("-----Expected Values-----");
      MoveNextResultsDebugWriter.WriteMoveNextResults(expected);

      // Act
      var actual =
        treenumerable
        .ToDepthFirstMoveNext(testScenario.SchedulingPredicate)
        .ToArray();

      Debug.WriteLine("\r\n-----Actual Values-----");
      MoveNextResultsDebugWriter.WriteMoveNextResults(expected);

      var diff = MoveNextResultDiffer.Diff(expected, actual);

      Debug.WriteLine("\r\n-----Diffed Values-----");
      foreach (var diffResult in diff)
        Debug.WriteLine(diffResult);

      // Assert
      CollectionAssert.AreEqual(expected, actual);
    }

    [TestMethod]
    [DynamicData(nameof(GetTestData), DynamicDataSourceType.Method, DynamicDataDisplayName = nameof(GetTestDisplayName))]
    public void Treenumerable_DepthFirst(
      string treeString,
      string testDescription,
      int testTreeIndex,
      int testScenarioIndex)
    {
      // Arrange
      var rootNodes = EnumerableTreeNode.Create(TreeSerializer.DeserializeRoots(treeString));
      var treenumerable = rootNodes.ToTreenumerable().Select(visit => visit.Node.Value);
      var testScenario = TreeTraversalTestData.TestTrees[testTreeIndex].TestScenarios[testScenarioIndex];

      var expected = testScenario.ExpectedDepthFirstResults;

      Debug.WriteLine("-----Expected Values-----");
      MoveNextResultsDebugWriter.WriteMoveNextResults(expected);

      // Act
      Debug.WriteLine("\r\n-----Actual Values-----");
      var actual =
        treenumerable
        .ToDepthFirstMoveNext(testScenario.SchedulingPredicate)
        .Do(x => Debug.WriteLine(x))
        .ToArray();

      var diff = MoveNextResultDiffer.Diff(expected, actual);

      Debug.WriteLine("\r\n-----Diffed Values-----");
      foreach (var diffResult in diff)
        Debug.WriteLine(diffResult);

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
      var treenumerable = TreeSerializer.Deserialize(treeString);
      var testScenario = TreeTraversalTestData.TestTrees[testTreeIndex].TestScenarios[testScenarioIndex];

      var expected = testScenario.ExpectedBreadthFirstResults;

      Debug.WriteLine("-----Expected Values-----");
      MoveNextResultsDebugWriter.WriteMoveNextResults(expected);

      // Act
      var actual =
        treenumerable
        .ToBreadthFirstMoveNext(testScenario.SchedulingPredicate)
        .ToArray();

      Debug.WriteLine("\r\n-----Actual Values-----");
      MoveNextResultsDebugWriter.WriteMoveNextResults(expected);

      var diff = MoveNextResultDiffer.Diff(expected, actual);

      Debug.WriteLine("\r\n-----Diffed Values-----");
      foreach (var diffResult in diff)
        Debug.WriteLine(diffResult);

      // Assert
      CollectionAssert.AreEqual(expected, actual);
    }

    [TestMethod]
    [DynamicData(nameof(GetTestData), DynamicDataSourceType.Method, DynamicDataDisplayName = nameof(GetTestDisplayName))]
    public void Treenumerable_BreadthFirst(
      string treeString,
      string testDescription,
      int testTreeIndex,
      int testScenarioIndex)
    {
      // Arrange
      var rootNodes = EnumerableTreeNode.Create(TreeSerializer.DeserializeRoots(treeString));
      var treenumerable = rootNodes.ToTreenumerable().Select(visit => visit.Node.Value);
      var testScenario = TreeTraversalTestData.TestTrees[testTreeIndex].TestScenarios[testScenarioIndex];

      var expected = testScenario.ExpectedBreadthFirstResults;

      Debug.WriteLine("-----Expected Values-----");
      MoveNextResultsDebugWriter.WriteMoveNextResults(expected);

      // Act
      var actual =
        treenumerable
        .ToBreadthFirstMoveNext(testScenario.SchedulingPredicate)
        .ToArray();

      Debug.WriteLine("\r\n-----Actual Values-----");
      MoveNextResultsDebugWriter.WriteMoveNextResults(expected);

      var diff = MoveNextResultDiffer.Diff(expected, actual);

      Debug.WriteLine("\r\n-----Diffed Values-----");
      foreach (var diffResult in diff)
        Debug.WriteLine(diffResult);

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
      var treenumerable = TreeSerializer.Deserialize(treeString);
      var testScenario = TreeTraversalTestData.TestTrees[testTreeIndex].TestScenarios[testScenarioIndex];

      MoveNextResult<string>[] Sort(IEnumerable<MoveNextResult<string>> nodes) =>
        nodes
        .OrderBy(x => (x.State, x.OriginalPosition, x.Node))
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
