using Arborist.Core;
using Arborist.Linq;
using Arborist.TestUtils;
using Arborist.SimpleSerializer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using System.Linq;
using System;

namespace Arborist.Linq.Tests
{
  [TestClass]
  public class PruneBeforeTests
  {
    [TestMethod]
    public void PruneBefore_BreadthFirstTraversal_LevelTwo()
    {
      // Arrange
      var tree = "a(b(c,d),e(f,g))";

      var treenumerable = TreeSerializer.Deserialize(tree);

      // Act
      var actual =
        treenumerable
        .PruneBefore(visit => visit.OriginalPosition.Depth == 2)
        .GetBreadthFirstTraversal()
        .Do(visit => Debug.WriteLine(visit))
        .ToArray();

      // Assert
      var expected = new[]
      {
        (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0), (0, 0)),
        (TreenumeratorMode.VisitingNode,   "a", 1, (0, 0), (0, 0)),
        (TreenumeratorMode.SchedulingNode, "b", 0, (0, 1), (0, 1)),
        (TreenumeratorMode.VisitingNode,   "a", 2, (0, 0), (0, 0)),
        (TreenumeratorMode.SchedulingNode, "e", 0, (1, 1), (1, 1)),
        (TreenumeratorMode.VisitingNode,   "a", 3, (0, 0), (0, 0)),
        (TreenumeratorMode.VisitingNode,   "b", 1, (0, 1), (0, 1)),
        (TreenumeratorMode.VisitingNode,   "b", 2, (0, 1), (0, 1)),
        (TreenumeratorMode.VisitingNode,   "e", 1, (1, 1), (1, 1)),
        (TreenumeratorMode.VisitingNode,   "e", 2, (1, 1), (1, 1)),
      }
      .ToNodeVisitArray();

      CollectionAssert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void PruneBefore_BreadthFirstTraversal_LevelOne()
    {
      // Arrange
      var tree = "a(b(c,d),e(f,g))";

      var treenumerable = TreeSerializer.Deserialize(tree);

      // Act
      var actual =
        treenumerable
        .PruneBefore(visit => visit.OriginalPosition.Depth == 1)
        .GetBreadthFirstTraversal()
        .Do(visit => Debug.WriteLine(visit))
        .ToArray();

      // Assert
      var expected = new[]
      {
        (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0), (0, 0)),
        (TreenumeratorMode.VisitingNode,   "a", 1, (0, 0), (0, 0)),
        (TreenumeratorMode.VisitingNode,   "a", 2, (0, 0), (0, 0)),
      };

      CollectionAssert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void PruneBefore_BreadthFirstTraversal_LevelZero()
    {
      // Arrange
      var tree = "a(b(c,d),e(f,g))";

      var treenumerable = TreeSerializer.Deserialize(tree);

      // Act
      var actual =
        treenumerable
        .PruneBefore(visit => visit.OriginalPosition.Depth == 0)
        .GetBreadthFirstTraversal()
        .Do(visit => Debug.WriteLine(visit))
        .ToArray();

      // Assert
      var expected = new[]
      {
        (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0), (0, 0)),
      };
    }

    [TestMethod]
    public void PruneBefore_DepthFirstTraversal_LevelOne_SiblingOne()
    {
      // Arrange
      var tree = "a(b,c,d)";

      var treenumerable = TreeSerializer.Deserialize(tree);

      // Act
      var actual =
        treenumerable
        .PruneBefore(visit => visit.Node == "c")
        .GetDepthFirstTraversal()
        .Do(visit => Debug.WriteLine(visit))
        .ToArray();

      // Assert
      var expected = new[]
      {
        (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0), (0, 0)),
        (TreenumeratorMode.VisitingNode,   "a", 1, (0, 0), (0, 0)),
        (TreenumeratorMode.SchedulingNode, "b", 0, (0, 1), (0, 1)),
        (TreenumeratorMode.VisitingNode,   "b", 1, (0, 1), (0, 1)),
        (TreenumeratorMode.VisitingNode,   "b", 2, (0, 1), (0, 1)),
        (TreenumeratorMode.VisitingNode,   "a", 2, (0, 0), (0, 0)),
        (TreenumeratorMode.SchedulingNode, "d", 0, (1, 1), (1, 1)),
        (TreenumeratorMode.VisitingNode,   "d", 1, (1, 1), (1, 1)),
        (TreenumeratorMode.VisitingNode,   "d", 2, (1, 1), (1, 1)),
        (TreenumeratorMode.VisitingNode,   "a", 3, (0, 0), (0, 0)),
      };

      CollectionAssert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void PruneBefore_DepthFirstTraversal_LevelZero()
    {
      // Arrange
      var tree = "a(b,c,d)";

      var treenumerable = TreeSerializer.Deserialize(tree);

      // Act
      var actual =
        treenumerable
        .PruneBefore(visit => visit.Node == "a")
        .GetDepthFirstTraversal()
        .Do(visit => Debug.WriteLine(visit))
        .ToArray();

      // Assert
      var expected = Array.Empty<NodeVisit<string>>();

      CollectionAssert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void PruneBefore_DepthFirstTraversal_LevelZero_SiblingZero()
    {
      // Arrange
      var tree = "a,b";

      var treenumerable = TreeSerializer.Deserialize(tree);

      // Act
      var actual =
        treenumerable
        .PruneBefore(visit => visit.Node == "a")
        .GetDepthFirstTraversal()
        .Do(visit => Debug.WriteLine(visit))
        .ToArray();

      // Assert
      var expected = new[]
      {
        (TreenumeratorMode.SchedulingNode, "b", 0, (0, 0), (0, 0)),
        (TreenumeratorMode.VisitingNode,   "b", 1, (0, 0), (0, 0)),
        (TreenumeratorMode.VisitingNode,   "b", 2, (0, 0), (0, 0)),
      };

      CollectionAssert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void PruneBefore_DepthFirstTraversal_LevelZero_SiblingOne()
    {
      // Arrange
      var tree = "a,b";

      var treenumerable = TreeSerializer.Deserialize(tree);

      // Act
      var actual =
        treenumerable
        .PruneBefore(visit => visit.Node == "b")
        .GetDepthFirstTraversal()
        .Do(visit => Debug.WriteLine(visit))
        .ToArray();

      // Assert
      var expected = new[]
      {
        (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0), (0, 0)),
        (TreenumeratorMode.VisitingNode,   "a", 1, (0, 0), (0, 0)),
        (TreenumeratorMode.VisitingNode,   "a", 2, (0, 0), (0, 0)),
      };

      CollectionAssert.AreEqual(expected, actual);
    }
  }
}

