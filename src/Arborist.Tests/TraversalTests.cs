using Arborist.Core;
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
      TraversalTest(treeString, testTreeIndex, testScenarioIndex, true, false);
    }

    [TestMethod]
    [DynamicData(nameof(GetTestData), DynamicDataSourceType.Method, DynamicDataDisplayName = nameof(GetTestDisplayName))]
    public void DepthFirst_EnumerableTreenumerable(
      string treeString,
      string testDescription,
      int testTreeIndex,
      int testScenarioIndex)
    {
      TraversalTest(treeString, testTreeIndex, testScenarioIndex, true, true);
    }

    [TestMethod]
    [DynamicData(nameof(GetTestData), DynamicDataSourceType.Method, DynamicDataDisplayName = nameof(GetTestDisplayName))]
    public void BreadthFirst_IndexableTreenumerable(
      string treeString,
      string testDescription,
      int testTreeIndex,
      int testScenarioIndex)
    {
      TraversalTest(treeString, testTreeIndex, testScenarioIndex, false, false);
    }

    [TestMethod]
    [DynamicData(nameof(GetTestData), DynamicDataSourceType.Method, DynamicDataDisplayName = nameof(GetTestDisplayName))]
    public void BreadthFirst_EnumerableTreenumerable(
      string treeString,
      string testDescription,
      int testTreeIndex,
      int testScenarioIndex)
    {
      TraversalTest(treeString, testTreeIndex, testScenarioIndex, false, true);
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

    private void TraversalTest(
      string treeString,
      int testTreeIndex,
      int testScenarioIndex,
      bool depthFirstTest,
      bool enumerableTreenumeratorTest)
    {
      // Arrange
      var testScenario = TreeTraversalTestData.TestTrees[testTreeIndex].TestScenarios[testScenarioIndex];

      ITreenumerable<string> treenumerable;

      if (enumerableTreenumeratorTest)
      {
        var rootNodes = EnumerableTreeNode.Create(TreeSerializer.DeserializeRoots(treeString));
        treenumerable = rootNodes.ToTreenumerable().Select(visit => visit.Node);
      }
      else
      {
        IEnumerable<INodeContainerWithIndexableChildren<string>> roots = TreeSerializer.DeserializeRoots(treeString);
        treenumerable = roots.ToTreenumerable().Select(visit => visit.Node);
      }

      var expected =
        depthFirstTest
        ? testScenario.ExpectedDepthFirstResults
        : testScenario.ExpectedBreadthFirstResults;

      Debug.WriteLine("--------- Tree ---------");
      Debug.WriteLine(treeString);

      Debug.WriteLine("\r\n-----Expected Values-----");
      MoveNextResultsDebugWriter.WriteMoveNextResults(expected);

      var moveNextEnumerable =
        depthFirstTest
        ? treenumerable.ToDepthFirstMoveNext(testScenario.SchedulingPredicate)
        : treenumerable.ToBreadthFirstMoveNext(testScenario.SchedulingPredicate);

      // Act
      Debug.WriteLine("\r\n-----Actual Values-----");
      var actual =
        moveNextEnumerable
        .Do(visit => Debug.WriteLine(visit))
        .ToArray();

      var diff = MoveNextResultDiffer.Diff(expected, actual);

      Debug.WriteLine("\r\n-----Diffed Values-----");
      foreach (var diffResult in diff)
        Debug.WriteLine(diffResult);

      // Assert
      CollectionAssert.AreEqual(expected, actual);
    }
  }
}
