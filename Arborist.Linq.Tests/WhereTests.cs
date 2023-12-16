using Arborist.Linq;
using Arborist.Tests.Utils;
using Arborist.Treenumerables;
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
      var root = TreeNode.Create('a', TreeNode.Create('b', 'c'));

      var treenumerable =
        TestTreenumerableFactory
        .Create<TreeNode<char>, char>(root)
        .Where(x => x.Depth != 0);

      // Act
      var actual =
        treenumerable
        .ToBreadthFirstMoveNext()
        .Do(x => Debug.WriteLine(x))
        .ToArray();

      // Assert
      var expected = new MoveNextResult<char>[]
      {
        ('b', 1, 0, 0),
        ('b', 2, 0, 0),
        ('c', 1, 0, 1),
        ('c', 2, 0, 1),
      };

      CollectionAssert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void Where_BreadthFirstTraversal_ThreeLevelChain_SkipSecond()
    {
      // Arrange
      var root = TreeNode.Create('a', TreeNode.Create('b', 'c'));

      var treenumerable =
        TestTreenumerableFactory
        .Create<TreeNode<char>, char>(root)
        .Where(x => x.Depth != 1);

      // Act
      var actual =
        treenumerable
        .ToBreadthFirstMoveNext()
        .Do(x => Debug.WriteLine(x))
        .ToArray();

      // Assert
      var expected = new MoveNextResult<char>[]
      {
        ('a', 1, 0, 0),
        ('a', 2, 0, 0),
        ('c', 1, 0, 1),
        ('c', 2, 0, 1),
      };

      CollectionAssert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void Where_BreadthFirstTraversal_ThreeLevelChain_SkipThird()
    {
      // Arrange
      var root = TreeNode.Create('a', TreeNode.Create('b', 'c'));

      var treenumerable =
        TestTreenumerableFactory
        .Create<TreeNode<char>, char>(root)
        .Where(x => x.Depth != 2);

      // Act
      var actual =
        treenumerable
        .ToBreadthFirstMoveNext()
        .Do(x => Debug.WriteLine(x))
        .ToArray();

      // Assert
      var expected = new MoveNextResult<char>[]
      {
        ('a', 1, 0, 0),
        ('a', 2, 0, 0),
        ('b', 1, 0, 1),
        ('b', 2, 0, 1),
      };

      CollectionAssert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void Where_BreadthFirstTraversal_TwoLevels_DepthIsZero()
    {
      // Arrange
      var root = TreeNode.Create('a', 'b', 'c');

      var treenumerable =
        TestTreenumerableFactory
        .Create<TreeNode<char>, char>(root)
        .Where(x => x.Depth == 0);

      // Act
      var actual =
        treenumerable
        .ToBreadthFirstMoveNext()
        .Do(x => Debug.WriteLine(x))
        .ToArray();

      // Assert
      var expected = new MoveNextResult<char>[]
      {
        ('a', 1, 0, 0),
        ('a', 2, 0, 0)
      };

      CollectionAssert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void Where_BreadthFirstTraversal_TwoLevels_DepthIsNotZero()
    {
      // Arrange
      var root = TreeNode.Create('a', 'b', 'c');

      var treenumerable =
        TestTreenumerableFactory
        .Create<TreeNode<char>, char>(root)
        .Where(x => x.Depth != 0);

      // Act
      var actual =
        treenumerable
        .ToBreadthFirstMoveNext()
        .Do(x => Debug.WriteLine(x))
        .ToArray();

      // Assert
      var expected = new MoveNextResult<char>[]
      {
        ('b', 1, 0, 0),
        ('b', 2, 0, 0),
        ('c', 1, 1, 0),
        ('c', 2, 1, 0)
      };

      CollectionAssert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void Where_BreadthFirstTraversal_TwoLevels_SkipsFirstChild()
    {
      // Arrange
      var root = TreeNode.Create('a', 'b', 'c', 'd');

      var treenumerable =
        TestTreenumerableFactory
        .Create<TreeNode<char>, char>(root)
        .Where(x => !(x.Depth == 1 && x.SiblingIndex == 0));

      // Act
      var actual =
        treenumerable
        .ToBreadthFirstMoveNext()
        .Do(x => Debug.WriteLine(x))
        .ToArray();

      // Assert
      var expected = new MoveNextResult<char>[]
      {
        ('a', 1, 0, 0),
        ('a', 2, 0, 0),
        ('a', 3, 0, 0),
        ('c', 1, 0, 1),
        ('c', 2, 0, 1),
        ('d', 1, 1, 1),
        ('d', 2, 1, 1),
      };

      CollectionAssert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void Where_BreadthFirstTraversal_TwoLevels_SkipsSecondChild()
    {
      // Arrange
      var root = TreeNode.Create('a', 'b', 'c', 'd');

      var treenumerable =
        TestTreenumerableFactory
        .Create<TreeNode<char>, char>(root)
        .Where(x => !(x.Depth == 1 && x.SiblingIndex == 1));

      // Act
      var actual =
        treenumerable
        .ToBreadthFirstMoveNext()
        .Do(x => Debug.WriteLine(x))
        .ToArray();

      // Assert
      var expected = new MoveNextResult<char>[]
      {
        ('a', 1, 0, 0),
        ('a', 2, 0, 0),
        ('a', 3, 0, 0),
        ('b', 1, 0, 1),
        ('b', 2, 0, 1),
        ('d', 1, 1, 1),
        ('d', 2, 1, 1),
      };

      CollectionAssert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void Where_BreadthFirstTraversal_TwoLevels_SkipsThirdChild()
    {
      // Arrange
      var root = TreeNode.Create('a', 'b', 'c', 'd');

      var treenumerable =
        TestTreenumerableFactory
        .Create<TreeNode<char>, char>(root)
        .Where(x => !(x.Depth == 1 && x.SiblingIndex == 2));

      // Act
      var actual =
        treenumerable
        .ToBreadthFirstMoveNext()
        .Do(x => Debug.WriteLine(x))
        .ToArray();

      // Assert
      var expected = new MoveNextResult<char>[]
      {
        ('a', 1, 0, 0),
        ('a', 2, 0, 0),
        ('a', 3, 0, 0),
        ('b', 1, 1, 1),
        ('b', 2, 1, 1),
        ('c', 1, 0, 1),
        ('c', 2, 0, 1),
      };

      CollectionAssert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void Where_BreadthFirstTraversal_TwoLevels_PredicateAlwaysReturnsFalse()
    {
      // Arrange
      var root = TreeNode.Create('a', 'b', 'c');

      var treenumerable =
        TestTreenumerableFactory
        .Create<TreeNode<char>, char>(root)
        .Where(_ => false);

      // Act
      var actual =
        treenumerable
        .ToBreadthFirstMoveNext()
        .Count();

      // Assert
      Assert.AreEqual(0, actual);
    }

    [TestMethod]
    public void Where_BreadthFirstTraversal_TwoLevels_PredicateAlwaysReturnsTrue()
    {
      // Arrange
      var root = TreeNode.Create('a', 'b', 'c');

      var treenumerable =
        TestTreenumerableFactory
        .Create<TreeNode<char>, char>(root)
        .Where(_ => true);

      // Act
      var actual =
        treenumerable
        .ToBreadthFirstMoveNext()
        .ToArray();

      // Assert
      var expected = new MoveNextResult<char>[]
      {
        ('a', 1, 0, 0),
        ('a', 2, 0, 0),
        ('a', 3, 0, 0),
        ('b', 1, 0, 1),
        ('b', 2, 0, 1),
        ('c', 1, 1, 1),
        ('c', 2, 1, 1),
      };

      CollectionAssert.AreEqual(expected, actual);
    }

    // -------------------------------------------------------------------------------

    [TestMethod]
    public void Where_DepthFirstTraversal_ThreeLevelChain_SkipFirst()
    {
      // Arrange
      var root = TreeNode.Create('a', TreeNode.Create('b', 'c'));

      var treenumerable =
        TestTreenumerableFactory
        .Create<TreeNode<char>, char>(root)
        .Where(x => x.Depth != 0);

      // Act
      var actual =
        treenumerable
        .ToDepthFirstMoveNext()
        .Do(x => Debug.WriteLine(x))
        .ToArray();

      // Assert
      var expected = new MoveNextResult<char>[]
      {
        ('b', 1, 0, 0),
        ('c', 1, 0, 1),
        ('c', 2, 0, 1),
        ('b', 2, 0, 0)
      };

      CollectionAssert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void Where_DepthFirstTraversal_ThreeLevelChain_SkipSecond()
    {
      // Arrange
      var root = TreeNode.Create('a', TreeNode.Create('b', 'c'));

      var treenumerable =
        TestTreenumerableFactory
        .Create<TreeNode<char>, char>(root)
        .Where(x => x.Depth != 1);

      // Act
      var actual =
        treenumerable
        .ToDepthFirstMoveNext()
        .Do(x => Debug.WriteLine(x))
        .ToArray();

      // Assert
      var expected = new MoveNextResult<char>[]
      {
        ('a', 1, 0, 0),
        ('c', 1, 0, 1),
        ('c', 2, 0, 1),
        ('a', 2, 0, 0)
      };

      CollectionAssert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void Where_DepthFirstTraversal_ThreeLevelChain_SkipThird()
    {
      // Arrange
      var root = TreeNode.Create('a', TreeNode.Create('b', 'c'));

      var treenumerable =
        TestTreenumerableFactory
        .Create<TreeNode<char>, char>(root)
        .Where(x => x.Depth != 2);

      // Act
      var actual =
        treenumerable
        .ToDepthFirstMoveNext()
        .Do(x => Debug.WriteLine(x))
        .ToArray();

      // Assert
      var expected = new MoveNextResult<char>[]
      {
        ('a', 1, 0, 0),
        ('b', 1, 0, 1),
        ('b', 2, 0, 1),
        ('a', 2, 0, 0)
      };

      CollectionAssert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void Where_DepthFirstTraversal_TwoLevels_DepthIsZero()
    {
      // Arrange
      var root = TreeNode.Create('a', 'b', 'c');

      var treenumerable =
        TestTreenumerableFactory
        .Create<TreeNode<char>, char>(root)
        .Where(x => x.Depth == 0);

      // Act
      var actual =
        treenumerable
        .ToDepthFirstMoveNext()
        .Do(x => Debug.WriteLine(x))
        .ToArray();

      // Assert
      var expected = new MoveNextResult<char>[]
      {
        ('a', 1, 0, 0),
        ('a', 2, 0, 0)
      };

      CollectionAssert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void Where_DepthFirstTraversal_TwoLevels_DepthIsNotZero()
    {
      // Arrange
      var root = TreeNode.Create('a', 'b', 'c');

      var treenumerable =
        TestTreenumerableFactory
        .Create<TreeNode<char>, char>(root)
        .Where(x => x.Depth != 0);

      // Act
      var actual =
        treenumerable
        .ToDepthFirstMoveNext()
        .Do(x => Debug.WriteLine(x))
        .ToArray();

      // Assert
      var expected = new MoveNextResult<char>[]
      {
        ('b', 1, 0, 0),
        ('b', 2, 0, 0),
        ('c', 1, 1, 0),
        ('c', 2, 1, 0)
      };

      CollectionAssert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void Where_DepthFirstTraversal_TwoLevels_SkipsFirstChild()
    {
      // Arrange
      var root = TreeNode.Create('a', 'b', 'c', 'd');

      var treenumerable =
        TestTreenumerableFactory
        .Create<TreeNode<char>, char>(root)
        .Where(x => !(x.Depth == 1 && x.SiblingIndex == 0));

      // Act
      var actual =
        treenumerable
        .ToDepthFirstMoveNext()
        .Do(x => Debug.WriteLine(x))
        .ToArray();

      // Assert
      var expected = new MoveNextResult<char>[]
      {
        ('a', 1, 0, 0),
        ('c', 1, 0, 1),
        ('c', 2, 0, 1),
        ('a', 2, 0, 0),
        ('d', 1, 1, 1),
        ('d', 2, 1, 1),
        ('a', 3, 0, 0)
      };

      CollectionAssert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void Where_DepthFirstTraversal_TwoLevels_SkipsSecondChild()
    {
      // Arrange
      var root = TreeNode.Create('a', 'b', 'c', 'd');

      var treenumerable =
        TestTreenumerableFactory
        .Create<TreeNode<char>, char>(root)
        .Where(x => !(x.Depth == 1 && x.SiblingIndex == 1));

      // Act
      var actual =
        treenumerable
        .ToDepthFirstMoveNext()
        .Do(x => Debug.WriteLine(x))
        .ToArray();

      // Assert
      var expected = new MoveNextResult<char>[]
      {
        ('a', 1, 0, 0),
        ('b', 1, 0, 1),
        ('b', 2, 0, 1),
        ('a', 2, 0, 0),
        ('d', 1, 1, 1),
        ('d', 2, 1, 1),
        ('a', 3, 0, 0)
      };

      CollectionAssert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void Where_DepthFirstTraversal_TwoLevels_SkipsThirdChild()
    {
      // Arrange
      var root = TreeNode.Create('a', 'b', 'c', 'd');

      var treenumerable =
        TestTreenumerableFactory
        .Create<TreeNode<char>, char>(root)
        .Where(x => !(x.Depth == 1 && x.SiblingIndex == 2));

      // Act
      var actual =
        treenumerable
        .ToDepthFirstMoveNext()
        .Do(x => Debug.WriteLine(x))
        .ToArray();

      // Assert
      var expected = new MoveNextResult<char>[]
      {
        ('a', 1, 0, 0),
        ('b', 1, 0, 1),
        ('b', 2, 0, 1),
        ('a', 2, 0, 0),
        ('c', 1, 1, 1),
        ('c', 2, 1, 1),
        ('a', 3, 0, 0)
      };

      CollectionAssert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void Where_DepthFirstTraversal_TwoLevels_PredicateAlwaysReturnsFalse()
    {
      // Arrange
      var root = TreeNode.Create('a', 'b', 'c');

      var treenumerable =
        TestTreenumerableFactory
        .Create<TreeNode<char>, char>(root)
        .Where(_ => false);

      // Act
      var actual =
        treenumerable
        .ToDepthFirstMoveNext()
        .Count();

      // Assert
      Assert.AreEqual(0, actual);
    }

    [TestMethod]
    public void Where_DepthFirstTraversal_TwoLevels_PredicateAlwaysReturnsTrue()
    {
      // Arrange
      var root = TreeNode.Create('a', 'b', 'c');

      var treenumerable =
        TestTreenumerableFactory
        .Create<TreeNode<char>, char>(root)
        .Where(_ => true);

      // Act
      var actual =
        treenumerable
        .ToDepthFirstMoveNext()
        .ToArray();

      // Assert
      var expected = new MoveNextResult<char>[]
      {
        ('a', 1, 0, 0),
        ('b', 1, 0, 1),
        ('b', 2, 0, 1),
        ('a', 2, 0, 0),
        ('c', 1, 1, 1),
        ('c', 2, 1, 1),
        ('a', 3, 0, 0)
      };

      CollectionAssert.AreEqual(expected, actual);
    }
  }
}