using Arborist.Linq;
using Arborist.Nodes;
using Arborist.SimpleSerializer;
using Arborist.TestUtils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace Arborist.Tests
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
    public void DepthFirst_IndexableTreenumerable(
      string treeString,
      string testDescription,
      int testTreeIndex,
      int testScenarioIndex)
    {
      // Arrange
      IEnumerable<INodeContainerWithIndexableChildren<string>> roots = TreeSerializer.DeserializeRoots(treeString);

      var treenumerable =
        roots
        .ToTreenumerable()
        .Select(visit => visit.Node);

      var testScenario = TreeTraversalTestData.TestTrees[testTreeIndex].TestScenarios[testScenarioIndex];

      var expected = testScenario.ExpectedDepthFirstResults;

      Debug.WriteLine("--------- Tree ---------");
      Debug.WriteLine($"\r\n{treeString}");

      Debug.WriteLine("\r\n-----Expected Values-----");
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
    public void DepthFirst_EnumerableTreenumerable(
      string treeString,
      string testDescription,
      int testTreeIndex,
      int testScenarioIndex)
    {
      // Arrange
      var rootNodes = EnumerableTreeNode.Create(TreeSerializer.DeserializeRoots(treeString));
      var treenumerable = rootNodes.ToTreenumerable().Select(visit => visit.Node);
      var testScenario = TreeTraversalTestData.TestTrees[testTreeIndex].TestScenarios[testScenarioIndex];

      var expected = testScenario.ExpectedDepthFirstResults;

      Debug.WriteLine("--------- Tree ---------");
      Debug.WriteLine(treeString);

      Debug.WriteLine("\r\n-----Expected Values-----");
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
    public void BreadthFirst_IndexableTreenumerable(
      string treeString,
      string testDescription,
      int testTreeIndex,
      int testScenarioIndex)
    {
      // Arrange
      IEnumerable<INodeContainerWithIndexableChildren<string>> roots = TreeSerializer.DeserializeRoots(treeString);

      var treenumerable =
        roots
        .ToTreenumerable()
        .Select(visit => visit.Node);

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
    public void BreadthFirst_EnumerableTreenumerable(
      string treeString,
      string testDescription,
      int testTreeIndex,
      int testScenarioIndex)
    {
      // Arrange
      var rootNodes = EnumerableTreeNode.Create(TreeSerializer.DeserializeRoots(treeString));
      var treenumerable = rootNodes.ToTreenumerable().Select(visit => visit.Node);
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
      var treenumerable =
        TreeSerializer
        .DeserializeRoots(treeString)
        .ToTreenumerable()
        .Select(visit => visit.Node);

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
