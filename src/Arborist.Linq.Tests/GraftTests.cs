using Arborist.Tests.Utils;
using Arborist.Treenumerables.SimpleSerializer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using System.Linq;

namespace Arborist.Linq.Tests
{
  [TestClass]
  public class GraftTests
  {
    [TestMethod]
    public void Graft_MultipleNodes_GraftSingleNodeBetweenChildren()
    {
      // Arrange
      var tree = "a(b,c)";

      var treenumerable = TreeSerializer.Deserialize(tree);
      var scion = TreeSerializer.Deserialize("d");

      // Act
      var actual =
        treenumerable
        .Graft(
          scion,
          visit => visit.VisitCount == 2 && visit.OriginalPosition.Depth == 0)
        .ToDepthFirstMoveNext()
        .ToArray();

      // Assert
      var expected = new MoveNextResult<string>[]
      {
        (TreenumeratorState.SchedulingNode, "a", 1, (0, 0), (0, 0)),
        (TreenumeratorState.SchedulingNode, "b", 1, (0, 1), (0, 1)),
        (TreenumeratorState.VisitingNode,   "b", 2, (0, 1), (0, 1)),
        (TreenumeratorState.VisitingNode,   "a", 2, (0, 0), (0, 0)),
        (TreenumeratorState.SchedulingNode, "d", 1, (1, 1), (1, 1)),
        (TreenumeratorState.VisitingNode,   "d", 2, (1, 1), (1, 1)),
        (TreenumeratorState.VisitingNode,   "a", 3, (0, 0), (0, 0)),
        (TreenumeratorState.SchedulingNode, "c", 1, (2, 1), (2, 1)),
        (TreenumeratorState.VisitingNode,   "c", 2, (2, 1), (2, 1)),
        (TreenumeratorState.VisitingNode,   "a", 4, (0, 0), (0, 0)),
      };

      CollectionAssert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void Graft_MultipleNodes_GraftSingleNodeToLeaves()
    {
      // Arrange
      var tree = "a(b,c)";

      var treenumerable = TreeSerializer.Deserialize(tree);
      var scion = TreeSerializer.Deserialize("d");

      // Act
      var actual =
        treenumerable
        .Graft(
          scion,
          visit => visit.VisitCount == 1 && (visit.Node == "b" || visit.Node == "c"))
        .ToDepthFirstMoveNext()
        .ToArray();

      // Assert
      var expected = new MoveNextResult<string>[]
      {
        (TreenumeratorState.SchedulingNode, "a", 1, (0, 0), (0, 0)),
        (TreenumeratorState.SchedulingNode, "b", 1, (0, 1), (0, 1)),
        (TreenumeratorState.SchedulingNode, "d", 1, (0, 2), (0, 2)),
        (TreenumeratorState.VisitingNode,   "d", 2, (0, 2), (0, 2)),
        (TreenumeratorState.VisitingNode,   "b", 2, (0, 1), (0, 1)),
        (TreenumeratorState.VisitingNode,   "a", 2, (0, 0), (0, 0)),
        (TreenumeratorState.SchedulingNode, "c", 1, (1, 1), (1, 1)),
        (TreenumeratorState.VisitingNode,   "d", 1, (0, 2), (0, 2)),
        (TreenumeratorState.VisitingNode,   "d", 2, (0, 2), (0, 2)),
        (TreenumeratorState.VisitingNode,   "c", 2, (1, 1), (1, 1)),
        (TreenumeratorState.VisitingNode,   "a", 3, (0, 0), (0, 0)),
      };

      CollectionAssert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void Graft_MultipleNodes_PredicateAlwaysFalse()
    {
      // Arrange
      var tree = "a(b,c)";

      var treenumerable = TreeSerializer.Deserialize(tree);
      var scion = TreeSerializer.Deserialize("d");

      // Act
      var actual =
        treenumerable
        .Graft(scion, _ => false)
        .ToDepthFirstMoveNext()
        .ToArray();

      // Assert
      var expected = new MoveNextResult<string>[]
      {
        (TreenumeratorState.SchedulingNode, "a", 1, (0, 0), (0, 0)),
        (TreenumeratorState.SchedulingNode, "b", 1, (0, 1), (0, 1)),
        (TreenumeratorState.VisitingNode,   "b", 2, (0, 1), (0, 1)),
        (TreenumeratorState.VisitingNode,   "a", 2, (0, 0), (0, 0)),
        (TreenumeratorState.SchedulingNode, "c", 1, (1, 1), (1, 1)),
        (TreenumeratorState.VisitingNode,   "c", 2, (1, 1), (1, 1)),
        (TreenumeratorState.VisitingNode,   "a", 3, (0, 0), (0, 0)),
      };

      CollectionAssert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void Graft_SingleNode_GraftSingleNode()
    {
      // Arrange
      var tree = "a";

      var treenumerable = TreeSerializer.Deserialize(tree);
      var scion = TreeSerializer.Deserialize("b");

      // Act
      var actual =
        treenumerable
        .Graft(scion, visit => visit.VisitCount == 1)
        .ToDepthFirstMoveNext()
        .Do(x => Debug.WriteLine(x))
        .ToArray();

      // Assert
      var expected = new MoveNextResult<string>[]
      {
        (TreenumeratorState.SchedulingNode, "a", 1, (0, 0), (0, 0)),
        (TreenumeratorState.VisitingNode,   "a", 2, (0, 0), (0, 0)),
        (TreenumeratorState.SchedulingNode, "b", 1, (0, 1), (0, 1)),
        (TreenumeratorState.VisitingNode,   "b", 2, (0, 1), (0, 1)),
        (TreenumeratorState.VisitingNode,   "b", 3, (0, 1), (0, 1)),
        (TreenumeratorState.VisitingNode,   "a", 3, (0, 0), (0, 0)),
      };

      CollectionAssert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void Graft_SingleNode_GraftSingleNodeThreeTimes()
    {
      // Arrange
      var tree = "a";

      var treenumerable = TreeSerializer.Deserialize(tree);
      var scion = TreeSerializer.Deserialize("b");

      // Act
      var actual =
        treenumerable
        .Graft(scion, visit => visit.VisitCount < 4)
        .ToDepthFirstMoveNext()
        .ToArray();

      // Assert
      var expected = new MoveNextResult<string>[]
      {
        (TreenumeratorState.SchedulingNode, "a", 1, (0, 0), (0, 0)),
        (TreenumeratorState.SchedulingNode, "b", 1, (0, 1), (0, 1)),
        (TreenumeratorState.VisitingNode,   "b", 2, (0, 1), (0, 1)),
        (TreenumeratorState.VisitingNode,   "a", 2, (0, 0), (0, 0)),
        (TreenumeratorState.VisitingNode,   "b", 1, (1, 1), (1, 1)),
        (TreenumeratorState.VisitingNode,   "b", 2, (1, 1), (1, 1)),
        (TreenumeratorState.VisitingNode,   "a", 3, (0, 0), (0, 0)),
        (TreenumeratorState.VisitingNode,   "b", 1, (2, 1), (2, 1)),
        (TreenumeratorState.VisitingNode,   "b", 2, (2, 1), (2, 1)),
        (TreenumeratorState.VisitingNode,   "a", 4, (0, 0), (0, 0)),
      };

      CollectionAssert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void Graft_SingleNode_PredicateAlwaysFalse()
    {
      // Arrange
      var tree = "a";

      var treenumerable = TreeSerializer.Deserialize(tree);

      // Act
      var actual =
        treenumerable
        .Graft(treenumerable, _ => false)
        .ToDepthFirstMoveNext()
        .ToArray();

      // Assert
      var expected = new MoveNextResult<string>[]
      {
        (TreenumeratorState.SchedulingNode, "a", 1, (0, 0), (0, 0)),
        (TreenumeratorState.SchedulingNode, "a", 2, (0, 0), (0, 0)),
      };

      CollectionAssert.AreEqual(expected, actual);
    }
  }
}

