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
      var root =
        TreeNode.Create('a',
          TreeNode.Create('b', 'c', 'd'),
          TreeNode.Create('e', 'f', 'g'));

      var treenumerable =
        TestTreenumerableFactory
        .Create<TreeNode<char>, char>(root)
        .Prune(visit => visit.Depth > 1);

      // Act
      var actual =
        treenumerable
        .ToBreadthFirstMoveNext()
        .Do(x => Debug.WriteLine(x))
        .ToArray();

      // Assert
      var expected = new MoveNextResult<char>[]
      {
        (TreenumeratorState.SchedulingNode, 'a', 0, 0, 0),
        (TreenumeratorState.VisitingNode,   'a', 1, 0, 0),
        (TreenumeratorState.SchedulingNode, 'b', 0, 0, 1),
        (TreenumeratorState.VisitingNode,   'a', 2, 0, 0),
        (TreenumeratorState.SchedulingNode, 'e', 0, 1, 1),
        (TreenumeratorState.VisitingNode,   'a', 3, 0, 0),

        (TreenumeratorState.VisitingNode,   'b', 1, 0, 1),
        (TreenumeratorState.SchedulingNode, 'c', 0, 0, 2),
        (TreenumeratorState.SchedulingNode, 'd', 0, 1, 2),
        (TreenumeratorState.VisitingNode,   'b', 2, 0, 1),

        (TreenumeratorState.VisitingNode,   'e', 1, 1, 1),
        (TreenumeratorState.SchedulingNode, 'f', 0, 0, 2),
        (TreenumeratorState.SchedulingNode, 'g', 0, 1, 2),
        (TreenumeratorState.VisitingNode,   'e', 2, 1, 1),
      };

      CollectionAssert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void Prune_BreadthFirstTraversal_AtLevelOne()
    {
      // Arrange
      var root =
        TreeNode.Create('a',
          TreeNode.Create('b', 'c', 'd'),
          TreeNode.Create('e', 'f', 'g'));

      var treenumerable =
        TestTreenumerableFactory
        .Create<TreeNode<char>, char>(root)
        .Prune(x => x.Depth == 1);

      // Act
      var actual =
        treenumerable
        .ToBreadthFirstMoveNext()
        .Do(x => Debug.WriteLine(x))
        .ToArray();

      // Assert
      var expected = new MoveNextResult<char>[]
      {
        (TreenumeratorState.SchedulingNode, 'a', 0, 0, 0),
        (TreenumeratorState.VisitingNode,   'a', 1, 0, 0),
        (TreenumeratorState.SchedulingNode, 'b', 0, 0, 1),
        (TreenumeratorState.SchedulingNode, 'e', 0, 1, 1),
        (TreenumeratorState.VisitingNode,   'a', 2, 0, 0),
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
        .Prune(x => x.Depth == 0);

      // Act
      var actual =
        treenumerable
        .ToBreadthFirstMoveNext()
        .Do(x => Debug.WriteLine(x))
        .ToArray();

      // Assert
      var expected = new MoveNextResult<char>[]
      {
        (TreenumeratorState.SchedulingNode, 'a', 0, 0, 0),
      };
    }

    [TestMethod]
    public void Prune_DepthFirstTraversal_PruneBefore_MiddleChild()
    {
      // Arrange
      var root =
        TreeNode.Create('a', 'b', 'c', 'd');

      var treenumerable =
        TestTreenumerableFactory
        .Create<TreeNode<char>, char>(root)
        .Prune(x => x.Node == 'c');

      // Act
      var actual =
        treenumerable
        .ToDepthFirstMoveNext()
        .Do(x => Debug.WriteLine(x))
        .ToArray();

      // Assert
      var expected = new MoveNextResult<char>[]
      {
        (TreenumeratorState.SchedulingNode, 'a', 1, 0, 0),
        (TreenumeratorState.VisitingNode,   'a', 2, 0, 0),
          (TreenumeratorState.SchedulingNode, 'b', 1, 0, 1),
          (TreenumeratorState.VisitingNode,   'b', 2, 0, 1),
          (TreenumeratorState.VisitingNode,   'b', 3, 0, 1),
        (TreenumeratorState.VisitingNode,   'a', 3, 0, 0),
          (TreenumeratorState.SchedulingNode, 'c', 1, 1, 1),
          (TreenumeratorState.SchedulingNode, 'd', 1, 1, 1),
          (TreenumeratorState.VisitingNode,   'd', 2, 1, 1),
          (TreenumeratorState.VisitingNode,   'd', 3, 1, 1),
        (TreenumeratorState.VisitingNode,   'a', 4, 0, 0),
      };

      CollectionAssert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void Prune_DepthFirstTraversal_PruneBefore_RootNode()
    {
      // Arrange
      var root =
        TreeNode.Create('a', 'b', 'c', 'd');

      var treenumerable =
        TestTreenumerableFactory
        .Create<TreeNode<char>, char>(root)
        .Prune(x => x.Node == 'a');

      // Act
      var actual =
        treenumerable
        .ToDepthFirstMoveNext()
        .Do(x => Debug.WriteLine(x))
        .ToArray();

      // Assert
      var expected = new MoveNextResult<char>[]
      {
        (TreenumeratorState.SchedulingNode, 'a', 0, 0, 0),
      };

      CollectionAssert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void Prune_DepthFirstTraversal_PruneBefore_FirstRootNode()
    {
      // Arrange
      var roots = new[]
      {
        TreeNode.Create('a'),
        TreeNode.Create('b'),
      };

      var treenumerable =
        TestTreenumerableFactory
        .Create<TreeNode<char>, char>(roots)
        .Prune(x => x.Node == 'a');

      // Act
      var actual =
        treenumerable
        .ToDepthFirstMoveNext()
        .Do(x => Debug.WriteLine(x))
        .ToArray();

      // Assert
      var expected = new MoveNextResult<char>[]
      {
        (TreenumeratorState.SchedulingNode, 'a', 0, 0, 0),
        (TreenumeratorState.SchedulingNode, 'b', 0, 1, 0),
        (TreenumeratorState.VisitingNode,   'b', 0, 2, 0),
        (TreenumeratorState.VisitingNode,   'b', 0, 3, 0),
      };

      CollectionAssert.AreEqual(expected, actual);
    }
  }
}