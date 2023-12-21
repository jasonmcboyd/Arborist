//using Arborist.Tests.Utils;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using System.Diagnostics;
//using System.Linq;

//namespace Arborist.Treenumerables.Tests
//{
//  [TestClass]
//  public class BreadthFirstTraversalTests
//  {
//    [TestMethod]
//    public void BreadthFirstTraversal()
//    {
//      // Arrange
//      var root = TreeNode.Create('a', 'b', 'c');

//      var treenumerable = TestTreenumerableFactory.Create<TreeNode<char>, char>(root);

//      // Act
//      var actual =
//        treenumerable
//        .ToBreadthFirstMoveNext()
//        .ToArray();

//      // Assert
//      var expected = new MoveNextResult<char>[]
//      {
//        ('a', 1, 0, 0),
//        ('a', 2, 0, 0),
//        ('a', 3, 0, 0),
//        ('b', 1, 0, 1),
//        ('b', 2, 0, 1),
//        ('c', 1, 1, 1),
//        ('c', 2, 1, 1),
//      };

//      CollectionAssert.AreEqual(expected, actual);
//    }

//    [TestMethod]
//    public void BreadthFirstTraversal_SkipChildrenOnFirstMoveNext()
//    {
//      // Arrange
//      var root = TreeNode.Create('a', 'b', 'c');

//      var treenumerable = TestTreenumerableFactory.Create<TreeNode<char>, char>(root);

//      var enumerator = treenumerable.GetBreadthFirstTreenumerator();

//      // Act
//      var result = enumerator.MoveNext(true);
//      enumerator.Dispose();

//      // Assert
//      Assert.IsFalse(result);
//    }

//    [TestMethod]
//    public void BreadthFirstTraversal_SingleRootNode_SkipChildrenAfterRootNode()
//    {
//      // Arrange
//      var root = TreeNode.Create('a', 'b', 'c');

//      var treenumerable = TestTreenumerableFactory.Create<TreeNode<char>, char>(root);

//      // Act
//      var actual =
//        treenumerable
//        .ToBreadthFirstMoveNext(visit => visit.Node == 'a')
//        .ToArray();

//      // Assert
//      var expected = new MoveNextResult<char>[]
//      {
//        ('a', 1, 0, 0),
//        ('a', 2, 0, 0),
//      };

//      CollectionAssert.AreEqual(expected, actual);
//    }

//    [TestMethod]
//    public void BreadthFirstTraversal_ThreeLevels_SkipChildrenAfterRootsFirstChild()
//    {
//      // Arrange
//      var root =
//        TreeNode.Create('a',
//          TreeNode.Create('b', 'd', 'e'),
//          TreeNode.Create('c', 'f', 'g'));

//      var treenumerable = TestTreenumerableFactory.Create<TreeNode<char>, char>(root);

//      // Act
//      var actual =
//        treenumerable
//        .ToBreadthFirstMoveNext(visit => visit.Node == 'a' && visit.VisitCount == 2)
//        .ToArray();

//      // Assert
//      var expected = new MoveNextResult<char>[]
//      {
//        ('a', 1, 0, 0),
//        ('a', 2, 0, 0),
//        ('a', 3, 0, 0),
//        ('b', 1, 0, 1),
//        ('b', 2, 0, 1),
//        ('b', 3, 0, 1),
//        ('d', 1, 0, 2),
//        ('d', 2, 0, 2),
//        ('e', 1, 1, 2),
//        ('e', 2, 1, 2),
//      };

//      CollectionAssert.AreEqual(expected, actual);
//    }

//    [TestMethod]
//    public void BreadthFirstTraversal_ThreeLevels_SkipChildrenAfterLevelOne()
//    {
//      // Arrange
//      var root =
//        TreeNode.Create('a',
//          TreeNode.Create('b', 'd', 'e'),
//          TreeNode.Create('c', 'f', 'g'));

//      var treenumerable = TestTreenumerableFactory.Create<TreeNode<char>, char>(root);

//      // Act
//      var actual =
//        treenumerable
//        .ToBreadthFirstMoveNext(visit => visit.Depth == 1 && visit.VisitCount == 1)
//        .Do(visit => Debug.WriteLine(visit))
//        .ToArray();

//      // Assert
//      var expected = new MoveNextResult<char>[]
//      {
//        ('a', 1, 0, 0),
//        ('a', 2, 0, 0),
//        ('a', 3, 0, 0),
//        ('b', 1, 0, 1),
//        ('b', 2, 0, 1),
//        ('c', 1, 1, 1),
//        ('c', 2, 1, 1),
//      };

//      CollectionAssert.AreEqual(expected, actual);
//    }

//    [TestMethod]
//    public void BreadthFirstTraversal_MultipleRoots()
//    {
//      // Arrange
//      var roots = new[]
//      {
//        TreeNode.Create('a', 'b', 'c'),
//        TreeNode.Create('d', 'e', 'f')
//      };

//      var treenumerable = TestTreenumerableFactory.Create<TreeNode<char>, char>(roots);

//      // Act
//      var actual =
//        treenumerable
//        .ToBreadthFirstMoveNext()
//        .ToArray();

//      // Assert
//      var expected = new MoveNextResult<char>[]
//      {
//        ('a', 1, 0, 0),
//        ('a', 2, 0, 0),
//        ('a', 3, 0, 0),
//        ('d', 1, 1, 0),
//        ('d', 2, 1, 0),
//        ('d', 3, 1, 0),

//        ('b', 1, 0, 1),
//        ('b', 2, 0, 1),
//        ('c', 1, 1, 1),
//        ('c', 2, 1, 1),

//        ('e', 1, 0, 1),
//        ('e', 2, 0, 1),
//        ('f', 1, 1, 1),
//        ('f', 2, 1, 1),
//      };

//      CollectionAssert.AreEqual(expected, actual);
//    }

//    [TestMethod]
//    public void BreadthFirstTraversal_MultipleRoots_SingleLevel()
//    {
//      // Arrange
//      var roots = new[]
//      {
//        TreeNode.Create('a'),
//        TreeNode.Create('b')
//      };

//      var treenumerable = TestTreenumerableFactory.Create<TreeNode<char>, char>(roots);

//      // Act
//      var actual =
//        treenumerable
//        .ToBreadthFirstMoveNext()
//        .ToArray();

//      // Assert
//      var expected = new MoveNextResult<char>[]
//      {
//        ('a', 1, 0, 0),
//        ('a', 2, 0, 0),
//        ('b', 1, 1, 0),
//        ('b', 2, 1, 0),
//      };

//      CollectionAssert.AreEqual(expected, actual);
//    }
//  }
//}