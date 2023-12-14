using Arborist.Linq;
using Arborist.Tests.Utils;
using Arborist.Treenumerables;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Arborist.Linq.Tests
{
  [TestClass]
  public class RepeatTests
  {
    [TestMethod]
    public void Repeat_SingleNode_Zero()
    {
      // Arrange
      var root = TreeNode.Create('a');

      var treenumerable = TestTreenumerableFactory.Create<TreeNode<char>, char>(root);

      // Act
      var actual =
        treenumerable
        .Repeat(0)
        .ToDepthFirstMoveNext()
        .ToArray();

      // Assert
      var expected =
        new MoveNextResult<char>[]
        {
          ('a', 1, 0, 0),
          ('a', 2, 0, 0)
        };

      CollectionAssert.AreEqual(actual, expected);
    }

    [TestMethod]
    public void Repeat_SingleNode_Two()
    {
      // Arrange
      var root = TreeNode.Create('a');

      var treenumerable = TestTreenumerableFactory.Create<TreeNode<char>, char>(root);

      // Act
      var actual =
        treenumerable
        .Repeat(2)
        .ToDepthFirstMoveNext()
        .ToArray();

      // Assert
      var expected =
        new MoveNextResult<char>[]
        {
          ('a', 1, 0, 0),
          ('a', 2, 0, 0),

          ('a', 1, 1, 0),
          ('a', 2, 1, 0),

          ('a', 1, 2, 0),
          ('a', 2, 2, 0)
        };

      CollectionAssert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void Repeat_SingleNode_NoCount()
    {
      // Arrange
      var root = TreeNode.Create('a');

      var treenumerable = TestTreenumerableFactory.Create<TreeNode<char>, char>(root);

      // Act
      var actual =
        treenumerable
        .Repeat()
        .ToDepthFirstMoveNext()
        .Take(6)
        .ToArray();

      // Assert
      var expected =
        new MoveNextResult<char>[]
        {
          ('a', 1, 0, 0),
          ('a', 2, 0, 0),

          ('a', 1, 1, 0),
          ('a', 2, 1, 0),

          ('a', 1, 2, 0),
          ('a', 2, 2, 0)
        };

      CollectionAssert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void Repeat_SingleNode_Two_SkipOnFirstMoveNext()
    {
      // Arrange
      var root = TreeNode.Create('a');

      var treenumerable =
        TestTreenumerableFactory
        .Create<TreeNode<char>, char>(root)
        .Repeat(2);

      var treenumerator = treenumerable.GetDepthFirstTreenumerator();

      // Act
      var result = treenumerator.MoveNext(true);

      treenumerator.Dispose();

      // Assert
      Assert.IsFalse(result);
    }

    [TestMethod]
    public void Repeat_ThreeNodes_Zero()
    {
      // Arrange
      var root = TreeNode.Create('a', 'b', 'c');

      var treenumerable = TestTreenumerableFactory.Create<TreeNode<char>, char>(root);

      // Act
      var actual =
        treenumerable
        .Repeat(0)
        .ToDepthFirstMoveNext()
        .ToArray();

      // Assert
      var expected =
        new MoveNextResult<char>[]
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
    public void Repeat_ThreeNodes_One()
    {
      // Arrange
      var root = TreeNode.Create('a', 'b', 'c');

      var treenumerable = TestTreenumerableFactory.Create<TreeNode<char>, char>(root);

      // Act
      var actual =
        treenumerable
        .Repeat(1)
        .ToDepthFirstMoveNext()
        .ToArray();

      // Assert
      var expected =
        new MoveNextResult<char>[]
        {
          ('a', 1, 0, 0),
          ('b', 1, 0, 1),
          ('b', 2, 0, 1),
          ('a', 2, 0, 0),
          ('c', 1, 1, 1),
          ('c', 2, 1, 1),
          ('a', 3, 0, 0),

          ('a', 1, 1, 0),
          ('b', 1, 0, 1),
          ('b', 2, 0, 1),
          ('a', 2, 1, 0),
          ('c', 1, 1, 1),
          ('c', 2, 1, 1),
          ('a', 3, 1, 0)
        };

      CollectionAssert.AreEqual(expected, actual);
    }

    
    [TestMethod]
    public void Repeat_TwoRoots_Zero()
    {
      // Arrange
      var roots =
        new[]
        {
          TreeNode.Create('a'),
          TreeNode.Create('b')
        };

      var treenumerable = TestTreenumerableFactory.Create<TreeNode<char>, char>(roots);

      // Act
      var actual =
        treenumerable
        .Repeat(0)
        .ToDepthFirstMoveNext()
        .ToArray();

      // Assert
      var expected =
        new MoveNextResult<char>[]
        {
          ('a', 1, 0, 0),
          ('a', 2, 0, 0),
          ('b', 1, 1, 0),
          ('b', 2, 1, 0)
        };

      CollectionAssert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void Repeat_TwoRoots_Two()
    {
      // Arrange
      var roots =
        new[]
        {
          TreeNode.Create('a'),
          TreeNode.Create('b')
        };

      var treenumerable = TestTreenumerableFactory.Create<TreeNode<char>, char>(roots);

      // Act
      var actual =
        treenumerable
        .Repeat(2)
        .ToDepthFirstMoveNext()
        .ToArray();

      // Assert
      var expected =
        new MoveNextResult<char>[]
        {
          ('a', 1, 0, 0),
          ('a', 2, 0, 0),
          ('b', 1, 1, 0),
          ('b', 2, 1, 0),

          ('a', 1, 2, 0),
          ('a', 2, 2, 0),
          ('b', 1, 3, 0),
          ('b', 2, 3, 0),

          ('a', 1, 4, 0),
          ('a', 2, 4, 0),
          ('b', 1, 5, 0),
          ('b', 2, 5, 0),
        };

      CollectionAssert.AreEqual(expected, actual);
    }
  }
}