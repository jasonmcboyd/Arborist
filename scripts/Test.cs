using Arborist.Linq;
using Arborist.Tests.Utils;
using Arborist.Treenumerables.SimpleSerializer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using System.Linq;

namespace Arborist.Linq.Tests
{
  [TestClass]
  public class WhereTests
  {
    [TestMethod]
    public void Where_BreadthFirstTraversal_ThreeLevelChain_SkipFirst()
    {
      // Arrange
      var tree = "a(b(c))";

      var treenumerable = TreeSerializer.Deserialize(tree);

      // Act
      var actual =
        treenumerable
        .Where(visit => visit.OriginalPosition.Depth != 0)
        .ToBreadthFirstMoveNext()
        .Do(visit => Debug.WriteLine(visit))
        .ToArray();

      // Assert
      var expected = new MoveNextResult<string>[]
      {
        (TreenumeratorState.SchedulingNode, "b", 0, (0, 0), (0, 0)),
        (TreenumeratorState.VisitingNode,   "b", 1, (0, 0), (0, 0)),
        (TreenumeratorState.SchedulingNode, "c", 0, (0, 1), (0, 1)),
        (TreenumeratorState.VisitingNode,   "b", 2, (0, 0), (0, 0)),
        (TreenumeratorState.VisitingNode,   "c", 1, (0, 1), (0, 1)),
        (TreenumeratorState.VisitingNode,   "c", 2, (0, 1), (0, 1)),
      };

      CollectionAssert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void Where_BreadthFirstTraversal_ThreeLevelChain_SkipSecond()
    {
      // Arrange
      var tree = "a(b(c))";

      var treenumerable = TreeSerializer.Deserialize(tree);

      // Act
      var actual =
        treenumerable
        .Where(visit => visit.OriginalPosition.Depth != 1)
        .ToBreadthFirstMoveNext()
        .Do(visit => Debug.WriteLine(visit))
        .ToArray();

      // Assert
      var expected = new MoveNextResult<string>[]
      {
        (TreenumeratorState.SchedulingNode, "a", 0, (0, 0), (0, 0)),
        (TreenumeratorState.VisitingNode,   "a", 1, (0, 0), (0, 0)),
        (TreenumeratorState.SchedulingNode, "c", 0, (0, 1), (0, 1)),
        (TreenumeratorState.VisitingNode,   "a", 2, (0, 0), (0, 0)),
        (TreenumeratorState.VisitingNode,   "c", 1, (0, 1), (0, 1)),
        (TreenumeratorState.VisitingNode,   "c", 2, (0, 1), (0, 1)),
      };

      CollectionAssert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void Where_BreadthFirstTraversal_ThreeLevelChain_SkipThird()
    {
      // Arrange
      var tree = "a(b(c))";

      var treenumerable = TreeSerializer.Deserialize(tree);

      // Act
      var actual =
        treenumerable
        .Where(visit => visit.OriginalPosition.Depth != 2)
        .ToBreadthFirstMoveNext()
        .Do(visit => Debug.WriteLine(visit))
        .ToArray();

      // Assert
      var expected = new MoveNextResult<string>[]
      {
        (TreenumeratorState.SchedulingNode, "a", 0, (0, 0), (0, 0)),
        (TreenumeratorState.VisitingNode,   "a", 1, (0, 0), (0, 0)),
        (TreenumeratorState.SchedulingNode, "b", 0, (0, 1), (0, 1)),
        (TreenumeratorState.VisitingNode,   "a", 2, (0, 0), (0, 0)),
        (TreenumeratorState.VisitingNode,   "b", 1, (0, 1), (0, 1)),
        (TreenumeratorState.VisitingNode,   "b", 2, (0, 1), (0, 1)),
      };

      CollectionAssert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void Where_BreadthFirstTraversal_TwoLevels_DepthIsZero()
    {
      // Arrange
      var tree = "a(b,c)";

      var treenumerable = TreeSerializer.Deserialize(tree);

      // Act
      var actual =
        treenumerable
        .Where(visit => visit.OriginalPosition.Depth == 0)
        .ToBreadthFirstMoveNext()
        .Do(visit => Debug.WriteLine(visit))
        .ToArray();

      // Assert
      var expected = new MoveNextResult<string>[]
      {
        (TreenumeratorState.SchedulingNode, "a", 0, (0, 0), (0, 0)),
        (TreenumeratorState.VisitingNode,   "a", 1, (0, 0), (0, 0)),
        (TreenumeratorState.VisitingNode,   "a", 2, (0, 0), (0, 0)),
      };

      CollectionAssert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void Where_BreadthFirstTraversal_TwoLevels_DepthIsNotZero()
    {
      // Arrange
      var tree = "a(b,c)";

      var treenumerable = TreeSerializer.Deserialize(tree);

      // Act
      var actual =
        treenumerable
        .Where(visit => visit.OriginalPosition.Depth != 0)
        .ToBreadthFirstMoveNext()
        .Do(visit => Debug.WriteLine(visit))
        .ToArray();

      // Assert
      var expected = new MoveNextResult<string>[]
      {
        (TreenumeratorState.SchedulingNode, "b", 0, (0, 0), (0, 0)),
        (TreenumeratorState.SchedulingNode, "c", 0, (1, 0), (1, 0)),
        (TreenumeratorState.VisitingNode,   "b", 1, (0, 0), (0, 0)),
        (TreenumeratorState.VisitingNode,   "b", 2, (0, 0), (0, 0)),
        (TreenumeratorState.VisitingNode,   "c", 1, (1, 0), (1, 0)),
        (TreenumeratorState.VisitingNode,   "c", 2, (1, 0), (1, 0)),
      };

      CollectionAssert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void Where_BreadthFirstTraversal_TwoLevels_SkipsFirstChild()
    {
      // Arrange
      var tree = "a(b,c,d,)";

      var treenumerable = TreeSerializer.Deserialize(tree);

      // Act
      var actual =
        treenumerable
        .Where(visit => !(visit.OriginalPosition.Depth == 1 && visit.OriginalPosition.SiblingIndex == 0))
        .ToBreadthFirstMoveNext()
        .Do(visit => Debug.WriteLine(visit))
        .ToArray();

      // Assert
      var expected = new MoveNextResult<string>[]
      {
        (TreenumeratorState.SchedulingNode, "a", 0, (0, 0), (0, 0)),
        (TreenumeratorState.VisitingNode,   "a", 1, (0, 0), (0, 0)),
        (TreenumeratorState.SchedulingNode, "c", 0, (0, 1), (0, 1)),
        (TreenumeratorState.VisitingNode,   "a", 2, (0, 0), (0, 0)),
        (TreenumeratorState.SchedulingNode, "d", 0, (1, 1), (1, 1)),
        (TreenumeratorState.VisitingNode,   "a", 3, (0, 0), (0, 0)),
        (TreenumeratorState.VisitingNode,   "c", 1, (0, 1), (0, 1)),
        (TreenumeratorState.VisitingNode,   "c", 2, (0, 1), (0, 1)),
        (TreenumeratorState.VisitingNode,   "d", 1, (1, 1), (1, 1)),
        (TreenumeratorState.VisitingNode,   "d", 2, (1, 1), (1, 1)),
      };

      CollectionAssert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void Where_BreadthFirstTraversal_TwoLevels_SkipsSecondChild()
    {
      // Arrange
      var tree = "a(b,c,d,)";

      var treenumerable = TreeSerializer.Deserialize(tree);

      // Act
      var actual =
        treenumerable
        .Where(visit => !(visit.OriginalPosition.Depth == 1 && visit.OriginalPosition.SiblingIndex == 1))
        .ToBreadthFirstMoveNext()
        .Do(visit => Debug.WriteLine(visit))
        .ToArray();

      // Assert
      var expected = new MoveNextResult<string>[]
      {
        (TreenumeratorState.SchedulingNode, "a", 0, (0, 0), (0, 0)),
        (TreenumeratorState.VisitingNode,   "a", 1, (0, 0), (0, 0)),
        (TreenumeratorState.SchedulingNode, "b", 0, (0, 1), (0, 1)),
        (TreenumeratorState.VisitingNode,   "a", 2, (0, 0), (0, 0)),
        (TreenumeratorState.SchedulingNode, "d", 0, (1, 1), (1, 1)),
        (TreenumeratorState.VisitingNode,   "a", 3, (0, 0), (0, 0)),
        (TreenumeratorState.VisitingNode,   "b", 1, (0, 1), (0, 1)),
        (TreenumeratorState.VisitingNode,   "b", 2, (0, 1), (0, 1)),
        (TreenumeratorState.VisitingNode,   "d", 1, (1, 1), (1, 1)),
        (TreenumeratorState.VisitingNode,   "d", 2, (1, 1), (1, 1)),
      };

      CollectionAssert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void Where_BreadthFirstTraversal_TwoLevels_SkipsThirdChild()
    {
      // Arrange
      var tree = "a(b,c,d,)";

      var treenumerable = TreeSerializer.Deserialize(tree);

      // Act
      var actual =
        treenumerable
        .Where(visit => !(visit.OriginalPosition.Depth == 1 && visit.OriginalPosition.SiblingIndex == 2))
        .ToBreadthFirstMoveNext()
        .Do(visit => Debug.WriteLine(visit))
        .ToArray();

      // Assert
      var expected = new MoveNextResult<string>[]
      {
        (TreenumeratorState.SchedulingNode, "a", 0, (0, 0), (0, 0)),
        (TreenumeratorState.VisitingNode,   "a", 1, (0, 0), (0, 0)),
        (TreenumeratorState.SchedulingNode, "b", 0, (0, 1), (0, 1)),
        (TreenumeratorState.VisitingNode,   "a", 2, (0, 0), (0, 0)),
        (TreenumeratorState.SchedulingNode, "c", 0, (1, 1), (1, 1)),
        (TreenumeratorState.VisitingNode,   "a", 3, (0, 0), (0, 0)),
        (TreenumeratorState.VisitingNode,   "b", 1, (0, 1), (0, 1)),
        (TreenumeratorState.VisitingNode,   "b", 2, (0, 1), (0, 1)),
        (TreenumeratorState.VisitingNode,   "c", 1, (1, 1), (1, 1)),
        (TreenumeratorState.VisitingNode,   "c", 2, (1, 1), (1, 1)),
      };

      CollectionAssert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void Where_BreadthFirstTraversal_TwoLevels_PredicateAlwaysReturnsFalse()
    {
      // Arrange
      var tree = "a(b,c)";

      var treenumerable = TreeSerializer.Deserialize(tree);

      // Act
      var actual =
        treenumerable
        .Where(_ => false)
        .ToBreadthFirstMoveNext()
        .Do(visit => Debug.WriteLine(visit))
        .ToArray();

      // Assert
      var expected = new MoveNextResult<string>[]
      {
      };

      CollectionAssert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void Where_BreadthFirstTraversal_TwoLevels_PredicateAlwaysReturnsTrue()
    {
      // Arrange
      var tree = "a(b,c)";

      var treenumerable = TreeSerializer.Deserialize(tree);

      // Act
      var actual =
        treenumerable
        .Where(_ => true)
        .ToBreadthFirstMoveNext()
        .Do(visit => Debug.WriteLine(visit))
        .ToArray();

      // Assert
      var expected = new MoveNextResult<string>[]
      {
        (TreenumeratorState.SchedulingNode, "a", 0, (0, 0), (0, 0)),
        (TreenumeratorState.VisitingNode,   "a", 1, (0, 0), (0, 0)),
        (TreenumeratorState.SchedulingNode, "b", 0, (0, 1), (0, 1)),
        (TreenumeratorState.VisitingNode,   "a", 2, (0, 0), (0, 0)),
        (TreenumeratorState.SchedulingNode, "c", 0, (1, 1), (1, 1)),
        (TreenumeratorState.VisitingNode,   "a", 3, (0, 0), (0, 0)),
        (TreenumeratorState.VisitingNode,   "b", 1, (0, 1), (0, 1)),
        (TreenumeratorState.VisitingNode,   "b", 2, (0, 1), (0, 1)),
        (TreenumeratorState.VisitingNode,   "c", 1, (1, 1), (1, 1)),
        (TreenumeratorState.VisitingNode,   "c", 2, (1, 1), (1, 1)),
      };

      CollectionAssert.AreEqual(expected, actual);
    }

    // -------------------------------------------------------------------------------

    [TestMethod]
    public void Where_DepthFirstTraversal_ThreeLevelChain_SkipFirst()
    {
      // Arrange
      var tree = "a(b(c))";

      var treenumerable = TreeSerializer.Deserialize(tree);

      // Act
      var actual =
        treenumerable
        .Where(visit => visit.OriginalPosition.Depth != 0)
        .ToDepthFirstMoveNext()
        .Do(visit => Debug.WriteLine(visit))
        .ToArray();

      // Assert
      var expected = new MoveNextResult<string>[]
      {
        (TreenumeratorState.SchedulingNode, "b", 0, (0, 0), (0, 0)),
        (TreenumeratorState.VisitingNode,   "b", 1, (0, 0), (0, 0)),
        (TreenumeratorState.SchedulingNode, "c", 0, (0, 1), (0, 1)),
        (TreenumeratorState.VisitingNode,   "c", 1, (0, 1), (0, 1)),
        (TreenumeratorState.VisitingNode,   "c", 2, (0, 1), (0, 1)),
        (TreenumeratorState.VisitingNode,   "b", 2, (0, 0), (0, 0)),
      };

      CollectionAssert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void Where_DepthFirstTraversal_ThreeLevelChain_SkipSecond()
    {
      // Arrange
      var tree = "a(b(c))";

      var treenumerable = TreeSerializer.Deserialize(tree);

      // Act
      var actual =
        treenumerable
        .Where(visit => visit.OriginalPosition.Depth != 1)
        .ToDepthFirstMoveNext()
        .Do(visit => Debug.WriteLine(visit))
        .ToArray();

      // Assert
      var expected = new MoveNextResult<string>[]
      {
        (TreenumeratorState.SchedulingNode, "a", 0, (0, 0), (0, 0)),
        (TreenumeratorState.VisitingNode,   "a", 1, (0, 0), (0, 0)),
        (TreenumeratorState.SchedulingNode, "c", 0, (0, 1), (0, 1)),
        (TreenumeratorState.VisitingNode,   "c", 1, (0, 1), (0, 1)),
        (TreenumeratorState.VisitingNode,   "c", 2, (0, 1), (0, 1)),
        (TreenumeratorState.VisitingNode,   "a", 2, (0, 0), (0, 0)),
      };

      CollectionAssert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void Where_DepthFirstTraversal_ThreeLevelChain_SkipThird()
    {
      // Arrange
      var tree = "a(b(c))";

      var treenumerable = TreeSerializer.Deserialize(tree);

      // Act
      var actual =
        treenumerable
        .Where(visit => visit.OriginalPosition.Depth != 2)
        .ToDepthFirstMoveNext()
        .Do(visit => Debug.WriteLine(visit))
        .ToArray();

      // Assert
      var expected = new MoveNextResult<string>[]
      {
        (TreenumeratorState.SchedulingNode, "a", 0, (0, 0), (0, 0)),
        (TreenumeratorState.VisitingNode,   "a", 1, (0, 0), (0, 0)),
        (TreenumeratorState.SchedulingNode, "b", 0, (0, 1), (0, 1)),
        (TreenumeratorState.VisitingNode,   "b", 1, (0, 1), (0, 1)),
        (TreenumeratorState.VisitingNode,   "b", 2, (0, 1), (0, 1)),
        (TreenumeratorState.VisitingNode,   "a", 2, (0, 0), (0, 0)),
      };

      CollectionAssert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void Where_DepthFirstTraversal_TwoLevels_DepthIsZero()
    {
      // Arrange
      var tree = "a(b,c)";

      var treenumerable = TreeSerializer.Deserialize(tree);

      // Act
      var actual =
        treenumerable
        .Where(visit => visit.OriginalPosition.Depth == 0)
        .ToDepthFirstMoveNext()
        .Do(visit => Debug.WriteLine(visit))
        .ToArray();

      // Assert
      var expected = new MoveNextResult<string>[]
      {
        (TreenumeratorState.SchedulingNode, "a", 0, (0, 0), (0, 0)),
        (TreenumeratorState.VisitingNode,   "a", 1, (0, 0), (0, 0)),
        (TreenumeratorState.VisitingNode,   "a", 2, (0, 0), (0, 0)),
      };

      CollectionAssert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void Where_DepthFirstTraversal_TwoLevels_DepthIsNotZero()
    {
      // Arrange
      var tree = "a(b,c)";

      var treenumerable = TreeSerializer.Deserialize(tree);

      // Act
      var actual =
        treenumerable
        .Where(visit => visit.OriginalPosition.Depth != 0)
        .ToDepthFirstMoveNext()
        .Do(visit => Debug.WriteLine(visit))
        .ToArray();

      // Assert
      var expected = new MoveNextResult<string>[]
      {
        (TreenumeratorState.SchedulingNode, "b", 0, (0, 0), (0, 0)),
        (TreenumeratorState.VisitingNode,   "b", 1, (0, 0), (0, 0)),
        (TreenumeratorState.VisitingNode,   "b", 2, (0, 0), (0, 0)),
        (TreenumeratorState.SchedulingNode, "c", 0, (1, 0), (1, 0)),
        (TreenumeratorState.VisitingNode,   "c", 1, (1, 0), (1, 0)),
        (TreenumeratorState.VisitingNode,   "c", 2, (1, 0), (1, 0)),
      };

      CollectionAssert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void Where_DepthFirstTraversal_TwoLevels_SkipsFirstChild()
    {
      // Arrange
      var tree = "a(b,c,d)";

      var treenumerable = TreeSerializer.Deserialize(tree);

      // Act
      var actual =
        treenumerable
        .Where(visit => !(visit.OriginalPosition.Depth == 1 && visit.OriginalPosition.SiblingIndex == 0))
        .ToDepthFirstMoveNext()
        .Do(visit => Debug.WriteLine(visit))
        .ToArray();

      // Assert
      var expected = new MoveNextResult<string>[]
      {
        (TreenumeratorState.SchedulingNode, "a", 0, (0, 0), (0, 0)),
        (TreenumeratorState.VisitingNode,   "a", 1, (0, 0), (0, 0)),
        (TreenumeratorState.SchedulingNode, "c", 0, (0, 1), (0, 1)),
        (TreenumeratorState.VisitingNode,   "c", 1, (0, 1), (0, 1)),
        (TreenumeratorState.VisitingNode,   "c", 2, (0, 1), (0, 1)),
        (TreenumeratorState.VisitingNode,   "a", 2, (0, 0), (0, 0)),
        (TreenumeratorState.SchedulingNode, "d", 0, (1, 1), (1, 1)),
        (TreenumeratorState.VisitingNode,   "d", 1, (1, 1), (1, 1)),
        (TreenumeratorState.VisitingNode,   "d", 2, (1, 1), (1, 1)),
        (TreenumeratorState.VisitingNode,   "a", 3, (0, 0), (0, 0)),
      };

      CollectionAssert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void Where_DepthFirstTraversal_TwoLevels_SkipsSecondChild()
    {
      // Arrange
      var tree = "a(b,c,d)";

      var treenumerable = TreeSerializer.Deserialize(tree);

      // Act
      var actual =
        treenumerable
        .Where(visit => !(visit.OriginalPosition.Depth == 1 && visit.OriginalPosition.SiblingIndex == 1))
        .ToDepthFirstMoveNext()
        .Do(visit => Debug.WriteLine(visit))
        .ToArray();

      // Assert
      var expected = new MoveNextResult<string>[]
      {
        (TreenumeratorState.SchedulingNode, "a", 0, (0, 0), (0, 0)),
        (TreenumeratorState.VisitingNode,   "a", 1, (0, 0), (0, 0)),
        (TreenumeratorState.SchedulingNode, "b", 0, (0, 1), (0, 1)),
        (TreenumeratorState.VisitingNode,   "b", 1, (0, 1), (0, 1)),
        (TreenumeratorState.VisitingNode,   "b", 2, (0, 1), (0, 1)),
        (TreenumeratorState.VisitingNode,   "a", 2, (0, 0), (0, 0)),
        (TreenumeratorState.SchedulingNode, "d", 0, (1, 1), (1, 1)),
        (TreenumeratorState.VisitingNode,   "d", 1, (1, 1), (1, 1)),
        (TreenumeratorState.VisitingNode,   "d", 2, (1, 1), (1, 1)),
        (TreenumeratorState.VisitingNode,   "a", 3, (0, 0), (0, 0)),
      };

      CollectionAssert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void Where_DepthFirstTraversal_TwoLevels_LevelOne_SiblingTwo()
    {
      // Arrange
      var tree = "a(b,c,d)";

      var treenumerable = TreeSerializer.Deserialize(tree);

      // Act
      var actual =
        treenumerable
        .Where(visit => !(visit.OriginalPosition.Depth == 1 && visit.OriginalPosition.SiblingIndex == 2))
        .ToDepthFirstMoveNext()
        .Do(visit => Debug.WriteLine(visit))
        .ToArray();

      // Assert
      var expected = new MoveNextResult<string>[]
      {
        (TreenumeratorState.SchedulingNode, "a", 0, (0, 0), (0, 0)),
        (TreenumeratorState.VisitingNode,   "a", 1, (0, 0), (0, 0)),
        (TreenumeratorState.SchedulingNode, "b", 0, (0, 1), (0, 1)),
        (TreenumeratorState.VisitingNode,   "b", 1, (0, 1), (0, 1)),
        (TreenumeratorState.VisitingNode,   "b", 2, (0, 1), (0, 1)),
        (TreenumeratorState.VisitingNode,   "a", 2, (0, 0), (0, 0)),
        (TreenumeratorState.SchedulingNode, "c", 0, (1, 1), (1, 1)),
        (TreenumeratorState.VisitingNode,   "c", 1, (1, 1), (1, 1)),
        (TreenumeratorState.VisitingNode,   "c", 2, (1, 1), (1, 1)),
        (TreenumeratorState.VisitingNode,   "a", 3, (0, 0), (0, 0)),
      };

      CollectionAssert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void Where_DepthFirstTraversal_TwoLevels_PredicateAlwaysReturnsFalse()
    {
      // Arrange
      var tree = "a(b,c)";

      var treenumerable = TreeSerializer.Deserialize(tree);

      // Act
      var actual =
        treenumerable
        .Where(_ => false)
        .ToDepthFirstMoveNext()
        .Do(visit => Debug.WriteLine(visit))
        .ToArray();

      // Assert
      var expected = new MoveNextResult<string>[]
      {
      };

      CollectionAssert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void Where_DepthFirstTraversal_TwoLevels_PredicateAlwaysReturnsTrue()
    {
      // Arrange
      var tree = "a(b,c)";

      var treenumerable = TreeSerializer.Deserialize(tree);

      // Act
      var actual =
        treenumerable
        .Where(_ => true)
        .ToDepthFirstMoveNext()
        .Do(visit => Debug.WriteLine(visit))
        .ToArray();

      // Assert
      var expected = new MoveNextResult<string>[]
      {
        (TreenumeratorState.SchedulingNode, "a", 0, (0, 0), (0, 0)),
        (TreenumeratorState.VisitingNode,   "a", 1, (0, 0), (0, 0)),
        (TreenumeratorState.SchedulingNode, "b", 0, (0, 1), (0, 1)),
        (TreenumeratorState.VisitingNode,   "b", 1, (0, 1), (0, 1)),
        (TreenumeratorState.VisitingNode,   "b", 2, (0, 1), (0, 1)),
        (TreenumeratorState.VisitingNode,   "a", 2, (0, 0), (0, 0)),
        (TreenumeratorState.SchedulingNode, "c", 0, (1, 1), (1, 1)),
        (TreenumeratorState.VisitingNode,   "c", 1, (1, 1), (1, 1)),
        (TreenumeratorState.VisitingNode,   "c", 2, (1, 1), (1, 1)),
        (TreenumeratorState.VisitingNode,   "a", 3, (0, 0), (0, 0)),
      };

      CollectionAssert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void Where_DepthFirstTraversal_OneLevel_LevelZero_SiblingZero()
    {
      // Arrange
      var tree = "a,b";

      var treenumerable = TreeSerializer.Deserialize(tree);

      // Act
      var actual =
        treenumerable
        .Where(visit => visit.Node == "a")
        .ToDepthFirstMoveNext()
        .Do(visit => Debug.WriteLine(visit))
        .ToArray();

      // Assert
      var expected = new MoveNextResult<string>[]
      {
        (TreenumeratorState.SchedulingNode, "a", 0, (0, 0), (0, 0)),
        (TreenumeratorState.VisitingNode,   "a", 1, (0, 0), (0, 0)),
        (TreenumeratorState.VisitingNode,   "a", 2, (0, 0), (0, 0)),
      };

      CollectionAssert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void Where_DepthFirstTraversal_OneLevel_LevelZero_SiblingOne()
    {
      // Arrange
      var tree = "a,b";

      var treenumerable = TreeSerializer.Deserialize(tree);

      // Act
      var actual =
        treenumerable
        .Where(visit => visit.Node == "b")
        .ToDepthFirstMoveNext()
        .Do(visit => Debug.WriteLine(visit))
        .ToArray();

      // Assert
      var expected = new MoveNextResult<string>[]
      {
        (TreenumeratorState.SchedulingNode, "b", 0, (0, 0), (0, 0)),
        (TreenumeratorState.VisitingNode,   "b", 1, (0, 0), (0, 0)),
        (TreenumeratorState.VisitingNode,   "b", 2, (0, 0), (0, 0)),
      };

      CollectionAssert.AreEqual(expected, actual);
    }
  }
}

