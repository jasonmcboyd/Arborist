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
    public void Prune_BreadthFirstTraversal_PruneAfter_LevelOne()
    {
      // Arrange
      var root =
        TreeNode.Create('a',
          TreeNode.Create('b', 'c', 'd'),
          TreeNode.Create('e', 'f', 'g'));

      var treenumerable =
        TestTreenumerableFactory
        .Create<TreeNode<char>, char>(root)
        .Prune(
          visit => visit.Depth == 1 && visit.VisitCount == 1,
          PruneOption.PruneAfterNode);

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
        ('e', 1, 1, 1),
        ('e', 2, 1, 1),
      };

      CollectionAssert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void Prune_BreadthFirstTraversal_PruneBefore_LevelOne()
    {
      // Arrange
      var root =
        TreeNode.Create('a',
          TreeNode.Create('b', 'c', 'd'),
          TreeNode.Create('e', 'f', 'g'));

      var treenumerable =
        TestTreenumerableFactory
        .Create<TreeNode<char>, char>(root)
        .Do(x => Debug.WriteLine(x))
        .Prune(
          x => x.Depth == 1,
          PruneOption.PruneBeforeNode);

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
      };

      CollectionAssert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void Prune_BreadthFirstTraversal_PruneBefore_RootNode()
    {
      // Arrange
      var root =
        TreeNode.Create('a',
          TreeNode.Create('b', 'c', 'd'),
          TreeNode.Create('e', 'f', 'g'));

      var treenumerable =
        TestTreenumerableFactory
        .Create<TreeNode<char>, char>(root)
        .Prune(
          visit => visit.Depth == 0,
          PruneOption.PruneBeforeNode);

      // Act
      var actual =
        treenumerable
        .GetBreadthFirstTraversal()
        .Count();

      // Assert
      Assert.AreEqual(0, actual);
    }

    [TestMethod]
    public void Prune_DepthFirstTraversal_PruneBefore_LevelOne()
    {
      // Arrange
      var root =
        TreeNode.Create('a',
          TreeNode.Create('b', 'c', 'd'),
          TreeNode.Create('e', 'f', 'g'));

      var treenumerable =
        TestTreenumerableFactory
        .Create<TreeNode<char>, char>(root)
        .Prune(
          x => x.Depth == 1,
          PruneOption.PruneBeforeNode);

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
        ('a', 2, 0, 0),
      };

      CollectionAssert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void Prune_DepthFirstTraversal_PruneAfter_LevelOne()
    {
      // Arrange
      var root =
        TreeNode.Create('a',
          TreeNode.Create('b', 'c', 'd'),
          TreeNode.Create('e', 'f', 'g'));

      var treenumerable =
        TestTreenumerableFactory
        .Create<TreeNode<char>, char>(root)
        .Prune(
          x => x.Depth == 1,
          PruneOption.PruneAfterNode);

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
        ('e', 1, 1, 1),
        ('e', 2, 1, 1),
        ('a', 3, 0, 0),
      };

      CollectionAssert.AreEqual(expected, actual);
    }
  }
}