using Arborist.Linq;
using Arborist.Tests.Utils;
using Arborist.Treenumerables;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using System.Linq;

namespace Arborist.Linq.Tests
{
  [TestClass]
  public class PruneTests
  {
    [TestMethod]
    public void Prune_BreadthFirstTraversal_AfterLevelOne()
    {
      // Arrange
      var tree = "a(b(c,d),e(f,g))";

      var treenumerable = TreeStringParser.ParseTreeString(tree);

      // Act
      var actual =
        treenumerable
        .Prune(visit => visit.Depth > 1)
        .ToBreadthFirstMoveNext()
        .Do(visit => Debug.WriteLine(visit))
        .ToArray();

      // Assert
      var expected = new MoveNextResult<string>[]
      {
        (TreenumeratorState.SchedulingNode, "a", 0, 0, 0),
        (TreenumeratorState.VisitingNode,   "a", 1, 0, 0),
        (TreenumeratorState.SchedulingNode, "b", 0, 0, 1),
        (TreenumeratorState.VisitingNode,   "a", 2, 0, 0),
        (TreenumeratorState.SchedulingNode, "e", 0, 1, 1),
        (TreenumeratorState.VisitingNode,   "a", 3, 0, 0),
        (TreenumeratorState.VisitingNode,   "b", 1, 0, 1),
        (TreenumeratorState.SchedulingNode, "c", 0, 0, 2),
        (TreenumeratorState.SchedulingNode, "d", 0, 1, 2),
        (TreenumeratorState.VisitingNode,   "b", 2, 0, 1),
        (TreenumeratorState.VisitingNode,   "e", 1, 1, 1),
        (TreenumeratorState.SchedulingNode, "f", 0, 0, 2),
        (TreenumeratorState.SchedulingNode, "g", 0, 1, 2),
        (TreenumeratorState.VisitingNode,   "e", 2, 1, 1),
      };

      CollectionAssert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void Prune_BreadthFirstTraversal_AtLevelOne()
    {
      // Arrange
      var tree = "a(b(c,d),e(f,g))";

      var treenumerable = TreeStringParser.ParseTreeString(tree);

      // Act
      var actual =
        treenumerable
        .Prune(visit => visit.Depth == 1)
        .ToBreadthFirstMoveNext()
        .Do(visit => Debug.WriteLine(visit))
        .ToArray();

      // Assert
      var expected = new MoveNextResult<string>[]
      {
        (TreenumeratorState.SchedulingNode, "a", 0, 0, 0),
        (TreenumeratorState.VisitingNode,   "a", 1, 0, 0),
        (TreenumeratorState.SchedulingNode, "b", 0, 0, 1),
        (TreenumeratorState.SchedulingNode, "e", 0, 1, 1),
        (TreenumeratorState.VisitingNode,   "a", 2, 0, 0),
      };

      CollectionAssert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void Prune_BreadthFirstTraversal_PruneBefore_RootNode()
    {
      // Arrange
      var tree = "a(b(c,d),e(f,g))";

      var treenumerable = TreeStringParser.ParseTreeString(tree);

      // Act
      var actual =
        treenumerable
        .Prune(visit => visit.Depth == 0)
        .ToBreadthFirstMoveNext()
        .Do(visit => Debug.WriteLine(visit))
        .ToArray();

      // Assert
      var expected = new MoveNextResult<string>[]
      {
        (TreenumeratorState.SchedulingNode, "a", 0, 0, 0),
      };
    }

    [TestMethod]
    public void Prune_DepthFirstTraversal_PruneBefore_MiddleChild()
    {
      // Arrange
      var tree = "a(b,c,d)";

      var treenumerable = TreeStringParser.ParseTreeString(tree);

      // Act
      var actual =
        treenumerable
        .Prune(visit => visit.Node == "c")
        .ToDepthFirstMoveNext()
        .Do(visit => Debug.WriteLine(visit))
        .ToArray();

      // Assert
      var expected = new MoveNextResult<string>[]
      {
        (TreenumeratorState.SchedulingNode, "a", 0, 0, 0),
        (TreenumeratorState.VisitingNode,   "a", 1, 0, 0),
        (TreenumeratorState.SchedulingNode, "b", 0, 0, 1),
        (TreenumeratorState.VisitingNode,   "b", 1, 0, 1),
        (TreenumeratorState.VisitingNode,   "b", 2, 0, 1),
        (TreenumeratorState.VisitingNode,   "a", 2, 0, 0),
        (TreenumeratorState.SchedulingNode, "c", 0, 1, 1),
        (TreenumeratorState.SchedulingNode, "d", 0, 2, 1),
        (TreenumeratorState.VisitingNode,   "d", 1, 1, 1),
        (TreenumeratorState.VisitingNode,   "d", 2, 1, 1),
        (TreenumeratorState.VisitingNode,   "a", 3, 0, 0),
      };

      CollectionAssert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void Prune_DepthFirstTraversal_PruneBefore_RootNode()
    {
      // Arrange
      var tree = "a(b,c,d)";

      var treenumerable = TreeStringParser.ParseTreeString(tree);

      // Act
      var actual =
        treenumerable
        .Prune(visit => visit.Node == "a")
        .ToDepthFirstMoveNext()
        .Do(visit => Debug.WriteLine(visit))
        .ToArray();

      // Assert
      var expected = new MoveNextResult<string>[]
      {
        (TreenumeratorState.SchedulingNode, "a", 0, 0, 0),
      };

      CollectionAssert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void Prune_DepthFirstTraversal_PruneBefore_FirstRootNode()
    {
      // Arrange
      var tree = "a,b";

      var treenumerable = TreeStringParser.ParseTreeString(tree);

      // Act
      var actual =
        treenumerable
        .Prune(visit => visit.Node == "a")
        .ToDepthFirstMoveNext()
        .Do(visit => Debug.WriteLine(visit))
        .ToArray();

      // Assert
      var expected = new MoveNextResult<string>[]
      {
        (TreenumeratorState.SchedulingNode, "a", 0, 0, 0),
        (TreenumeratorState.SchedulingNode, "b", 0, 1, 0),
        (TreenumeratorState.VisitingNode,   "b", 1, 0, 0),
        (TreenumeratorState.VisitingNode,   "b", 2, 0, 0),
      };

      CollectionAssert.AreEqual(expected, actual);
    }
  }
}
