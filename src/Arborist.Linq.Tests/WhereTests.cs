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
        .GetBreadthFirstTraversal()
        .Do(visit => Debug.WriteLine(visit))
        .ToArray();

      // Assert
      var expected = new[]
      {
        (TreenumeratorMode.SchedulingNode, "b", 0, (0, 0), (0, 0)),
        (TreenumeratorMode.VisitingNode,   "b", 1, (0, 0), (0, 0)),
        (TreenumeratorMode.SchedulingNode, "c", 0, (0, 1), (0, 1)),
        (TreenumeratorMode.VisitingNode,   "b", 2, (0, 0), (0, 0)),
        (TreenumeratorMode.VisitingNode,   "c", 1, (0, 1), (0, 1)),
        (TreenumeratorMode.VisitingNode,   "c", 2, (0, 1), (0, 1)),
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
        .GetBreadthFirstTraversal()
        .Do(visit => Debug.WriteLine(visit))
        .ToArray();

      // Assert
      var expected = new[]
      {
        (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0), (0, 0)),
        (TreenumeratorMode.VisitingNode,   "a", 1, (0, 0), (0, 0)),
        (TreenumeratorMode.SchedulingNode, "c", 0, (0, 1), (0, 1)),
        (TreenumeratorMode.VisitingNode,   "a", 2, (0, 0), (0, 0)),
        (TreenumeratorMode.VisitingNode,   "c", 1, (0, 1), (0, 1)),
        (TreenumeratorMode.VisitingNode,   "c", 2, (0, 1), (0, 1)),
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
        (TreenumeratorMode.VisitingNode,   "b", 1, (0, 1), (0, 1)),
        (TreenumeratorMode.VisitingNode,   "b", 2, (0, 1), (0, 1)),
      }
      .ToNodeVisitArray();

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
    public void Where_BreadthFirstTraversal_TwoLevels_DepthIsNotZero()
    {
      // Arrange
      var tree = "a(b,c)";

      var treenumerable = TreeSerializer.Deserialize(tree);

      // Act
      var actual =
        treenumerable
        .Where(visit => visit.OriginalPosition.Depth != 0)
        .GetBreadthFirstTraversal()
        .Do(visit => Debug.WriteLine(visit))
        .ToArray();

      // Assert
      var expected = new[]
      {
        (TreenumeratorMode.SchedulingNode, "b", 0, (0, 0), (0, 0)),
        (TreenumeratorMode.SchedulingNode, "c", 0, (1, 0), (1, 0)),
        (TreenumeratorMode.VisitingNode,   "b", 1, (0, 0), (0, 0)),
        (TreenumeratorMode.VisitingNode,   "b", 2, (0, 0), (0, 0)),
        (TreenumeratorMode.VisitingNode,   "c", 1, (1, 0), (1, 0)),
        (TreenumeratorMode.VisitingNode,   "c", 2, (1, 0), (1, 0)),
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
        .GetBreadthFirstTraversal()
        .Do(visit => Debug.WriteLine(visit))
        .ToArray();

      // Assert
      var expected = new[]
      {
        (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0), (0, 0)),
        (TreenumeratorMode.VisitingNode,   "a", 1, (0, 0), (0, 0)),
        (TreenumeratorMode.SchedulingNode, "c", 0, (0, 1), (0, 1)),
        (TreenumeratorMode.VisitingNode,   "a", 2, (0, 0), (0, 0)),
        (TreenumeratorMode.SchedulingNode, "d", 0, (1, 1), (1, 1)),
        (TreenumeratorMode.VisitingNode,   "a", 3, (0, 0), (0, 0)),
        (TreenumeratorMode.VisitingNode,   "c", 1, (0, 1), (0, 1)),
        (TreenumeratorMode.VisitingNode,   "c", 2, (0, 1), (0, 1)),
        (TreenumeratorMode.VisitingNode,   "d", 1, (1, 1), (1, 1)),
        (TreenumeratorMode.VisitingNode,   "d", 2, (1, 1), (1, 1)),
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
        (TreenumeratorMode.SchedulingNode, "d", 0, (1, 1), (1, 1)),
        (TreenumeratorMode.VisitingNode,   "a", 3, (0, 0), (0, 0)),
        (TreenumeratorMode.VisitingNode,   "b", 1, (0, 1), (0, 1)),
        (TreenumeratorMode.VisitingNode,   "b", 2, (0, 1), (0, 1)),
        (TreenumeratorMode.VisitingNode,   "d", 1, (1, 1), (1, 1)),
        (TreenumeratorMode.VisitingNode,   "d", 2, (1, 1), (1, 1)),
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
        (TreenumeratorMode.SchedulingNode, "c", 0, (1, 1), (1, 1)),
        (TreenumeratorMode.VisitingNode,   "a", 3, (0, 0), (0, 0)),
        (TreenumeratorMode.VisitingNode,   "b", 1, (0, 1), (0, 1)),
        (TreenumeratorMode.VisitingNode,   "b", 2, (0, 1), (0, 1)),
        (TreenumeratorMode.VisitingNode,   "c", 1, (1, 1), (1, 1)),
        (TreenumeratorMode.VisitingNode,   "c", 2, (1, 1), (1, 1)),
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
        .GetBreadthFirstTraversal()
        .Do(visit => Debug.WriteLine(visit))
        .ToArray();

      // Assert
      var expected = Array.Empty<NodeVisit<string>>();

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
        (TreenumeratorMode.SchedulingNode, "c", 0, (1, 1), (1, 1)),
        (TreenumeratorMode.VisitingNode,   "a", 3, (0, 0), (0, 0)),
        (TreenumeratorMode.VisitingNode,   "b", 1, (0, 1), (0, 1)),
        (TreenumeratorMode.VisitingNode,   "b", 2, (0, 1), (0, 1)),
        (TreenumeratorMode.VisitingNode,   "c", 1, (1, 1), (1, 1)),
        (TreenumeratorMode.VisitingNode,   "c", 2, (1, 1), (1, 1)),
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
        .GetDepthFirstTraversal()
        .Do(visit => Debug.WriteLine(visit))
        .ToArray();

      // Assert
      var expected = new[]
      {
        (TreenumeratorMode.SchedulingNode, "b", 0, (0, 0), (0, 0)),
        (TreenumeratorMode.VisitingNode,   "b", 1, (0, 0), (0, 0)),
        (TreenumeratorMode.SchedulingNode, "c", 0, (0, 1), (0, 1)),
        (TreenumeratorMode.VisitingNode,   "c", 1, (0, 1), (0, 1)),
        (TreenumeratorMode.VisitingNode,   "c", 2, (0, 1), (0, 1)),
        (TreenumeratorMode.VisitingNode,   "b", 2, (0, 0), (0, 0)),
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
        .GetDepthFirstTraversal()
        .Do(visit => Debug.WriteLine(visit))
        .ToArray();

      // Assert
      var expected = new[]
      {
        (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0), (0, 0)),
        (TreenumeratorMode.VisitingNode,   "a", 1, (0, 0), (0, 0)),
        (TreenumeratorMode.SchedulingNode, "c", 0, (0, 1), (0, 1)),
        (TreenumeratorMode.VisitingNode,   "c", 1, (0, 1), (0, 1)),
        (TreenumeratorMode.VisitingNode,   "c", 2, (0, 1), (0, 1)),
        (TreenumeratorMode.VisitingNode,   "a", 2, (0, 0), (0, 0)),
      }.ToNodeVisitArray();

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
      }.ToNodeVisitArray();

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
        .GetDepthFirstTraversal()
        .Do(visit => Debug.WriteLine(visit))
        .ToArray();

      // Assert
      var expected = new[]
      {
        (TreenumeratorMode.SchedulingNode, "b", 0, (0, 0), (0, 0)),
        (TreenumeratorMode.VisitingNode,   "b", 1, (0, 0), (0, 0)),
        (TreenumeratorMode.VisitingNode,   "b", 2, (0, 0), (0, 0)),
        (TreenumeratorMode.SchedulingNode, "c", 0, (1, 0), (1, 0)),
        (TreenumeratorMode.VisitingNode,   "c", 1, (1, 0), (1, 0)),
        (TreenumeratorMode.VisitingNode,   "c", 2, (1, 0), (1, 0)),
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
        .GetDepthFirstTraversal()
        .Do(visit => Debug.WriteLine(visit))
        .ToArray();

      // Assert
      var expected = new[]
      {
        (TreenumeratorMode.SchedulingNode, "a", 0, (0, 0), (0, 0)),
        (TreenumeratorMode.VisitingNode,   "a", 1, (0, 0), (0, 0)),
        (TreenumeratorMode.SchedulingNode, "c", 0, (0, 1), (0, 1)),
        (TreenumeratorMode.VisitingNode,   "c", 1, (0, 1), (0, 1)),
        (TreenumeratorMode.VisitingNode,   "c", 2, (0, 1), (0, 1)),
        (TreenumeratorMode.VisitingNode,   "a", 2, (0, 0), (0, 0)),
        (TreenumeratorMode.SchedulingNode, "d", 0, (1, 1), (1, 1)),
        (TreenumeratorMode.VisitingNode,   "d", 1, (1, 1), (1, 1)),
        (TreenumeratorMode.VisitingNode,   "d", 2, (1, 1), (1, 1)),
        (TreenumeratorMode.VisitingNode,   "a", 3, (0, 0), (0, 0)),
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
    public void Where_DepthFirstTraversal_TwoLevels_LevelOne_SiblingTwo()
    {
      // Arrange
      var tree = "a(b,c,d)";

      var treenumerable = TreeSerializer.Deserialize(tree);

      // Act
      var actual =
        treenumerable
        .Where(visit => !(visit.OriginalPosition.Depth == 1 && visit.OriginalPosition.SiblingIndex == 2))
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
        (TreenumeratorMode.SchedulingNode, "c", 0, (1, 1), (1, 1)),
        (TreenumeratorMode.VisitingNode,   "c", 1, (1, 1), (1, 1)),
        (TreenumeratorMode.VisitingNode,   "c", 2, (1, 1), (1, 1)),
        (TreenumeratorMode.VisitingNode,   "a", 3, (0, 0), (0, 0)),
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
        .GetDepthFirstTraversal()
        .Do(visit => Debug.WriteLine(visit))
        .ToArray();

      // Assert
      var expected = Array.Empty<NodeVisit<string>>();

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
        (TreenumeratorMode.SchedulingNode, "c", 0, (1, 1), (1, 1)),
        (TreenumeratorMode.VisitingNode,   "c", 1, (1, 1), (1, 1)),
        (TreenumeratorMode.VisitingNode,   "c", 2, (1, 1), (1, 1)),
        (TreenumeratorMode.VisitingNode,   "a", 3, (0, 0), (0, 0)),
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
  }
}

