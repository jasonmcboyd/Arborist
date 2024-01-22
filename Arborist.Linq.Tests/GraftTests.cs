//using Arborist.Tests.Utils;
//using Arborist.Treenumerables;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using System.Diagnostics;
//using System.Linq;

//namespace Arborist.Linq.Tests
//{
//  [TestClass]
//  public class GraftTests
//  {
//    [TestMethod]
//    public void Graft_MultipleNodes_GraftSingleNodeBetweenChildren()
//    {
//      // Arrange
//      var root = TreeNode.Create('a', 'b', 'c');

//      var treenumerable = TestTreenumerableFactory.Create<TreeNode<char>, char>(root);
//      var scion = TestTreenumerableFactory.Create<TreeNode<char>, char>(TreeNode.Create('d'));

//      // Act
//      var actual =
//        treenumerable
//        .Graft(
//          scion,
//          visit => visit.VisitCount == 2 && visit.Depth == 0)
//        .ToDepthFirstMoveNext()
//        .ToArray();

//      // Assert
//      var expected = new MoveNextResult<char>[]
//      {
//        ('a', 1, 0, 0),
//          ('b', 1, 0, 1),
//          ('b', 2, 0, 1),
//        ('a', 2, 0, 0),
//          ('d', 1, 1, 1),
//          ('d', 2, 1, 1),
//        ('a', 3, 0, 0),
//          ('c', 1, 2, 1),
//          ('c', 2, 2, 1),
//        ('a', 4, 0, 0),
//      };

//      CollectionAssert.AreEqual(expected, actual);
//    }

//    [TestMethod]
//    public void Graft_MultipleNodes_GraftSingleNodeToLeaves()
//    {
//      // Arrange
//      var root = TreeNode.Create('a', 'b', 'c');

//      var treenumerable = TestTreenumerableFactory.Create<TreeNode<char>, char>(root);
//      var scion = TestTreenumerableFactory.Create<TreeNode<char>, char>(TreeNode.Create('d'));

//      // Act
//      var actual =
//        treenumerable
//        .Graft(scion, visit => visit.VisitCount == 1 && (visit.Node == 'b' || visit.Node == 'c'))
//        .ToDepthFirstMoveNext()
//        .ToArray();

//      // Assert
//      var expected = new MoveNextResult<char>[]
//      {
//        ('a', 1, 0, 0),
//          ('b', 1, 0, 1),
//            ('d', 1, 0, 2),
//            ('d', 2, 0, 2),
//          ('b', 2, 0, 1),
//        ('a', 2, 0, 0),
//          ('c', 1, 1, 1),
//            ('d', 1, 0, 2),
//            ('d', 2, 0, 2),
//          ('c', 2, 1, 1),
//        ('a', 3, 0, 0),
//      };

//      CollectionAssert.AreEqual(expected, actual);
//    }

//    [TestMethod]
//    public void Graft_MultipleNodes_PredicateAlwaysFalse()
//    {
//      // Arrange
//      var root = TreeNode.Create('a', 'b', 'c');

//      var treenumerable = TestTreenumerableFactory.Create<TreeNode<char>, char>(root);
//      var scion = TestTreenumerableFactory.Create<TreeNode<char>, char>(TreeNode.Create('d'));

//      // Act
//      var actual =
//        treenumerable
//        .Graft(scion, _ => false)
//        .ToDepthFirstMoveNext()
//        .ToArray();

//      // Assert
//      var expected = new MoveNextResult<char>[]
//      {
//        ('a', 1, 0, 0),
//          ('b', 1, 0, 1),
//          ('b', 2, 0, 1),
//        ('a', 2, 0, 0),
//          ('c', 1, 1, 1),
//          ('c', 2, 1, 1),
//        ('a', 3, 0, 0),
//      };

//      CollectionAssert.AreEqual(expected, actual);
//    }

//    [TestMethod]
//    public void Graft_SingleNode_GraftSingleNode()
//    {
//      // Arrange
//      var root = TreeNode.Create('a');

//      var treenumerable = TestTreenumerableFactory.Create<TreeNode<char>, char>(root);
//      var scion = TestTreenumerableFactory.Create<TreeNode<char>, char>(TreeNode.Create('b'));

//      // Act
//      var actual =
//        treenumerable
//        .Graft(scion, visit => visit.VisitCount == 1)
//        .ToDepthFirstMoveNext()
//        .Do(x => Debug.WriteLine(x))
//        .ToArray();

//      // Assert
//      var expected = new MoveNextResult<char>[]
//      {
//        ('a', 1, 0, 0),
//        ('a', 2, 0, 0),
//          ('b', 1, 0, 1),
//          ('b', 2, 0, 1),
//          ('b', 3, 0, 1),
//        ('a', 3, 0, 0),
//      };

//      CollectionAssert.AreEqual(expected, actual);
//    }

//    [TestMethod]
//    public void Graft_SingleNode_GraftSingleNodeThreeTimes()
//    {
//      // Arrange
//      var root = TreeNode.Create('a');

//      var treenumerable = TestTreenumerableFactory.Create<TreeNode<char>, char>(root);
//      var scion = TestTreenumerableFactory.Create<TreeNode<char>, char>(TreeNode.Create('b'));

//      // Act
//      var actual =
//        treenumerable
//        .Graft(scion, visit => visit.VisitCount < 4)
//        .ToDepthFirstMoveNext()
//        .ToArray();

//      // Assert
//      var expected = new MoveNextResult<char>[]
//      {
//        ('a', 1, 0, 0),
//          ('b', 1, 0, 1),
//          ('b', 2, 0, 1),
//        ('a', 2, 0, 0),
//          ('b', 1, 1, 1),
//          ('b', 2, 1, 1),
//        ('a', 3, 0, 0),
//          ('b', 1, 2, 1),
//          ('b', 2, 2, 1),
//        ('a', 4, 0, 0),
//      };

//      CollectionAssert.AreEqual(expected, actual);
//    }

//    [TestMethod]
//    public void Graft_SingleNode_PredicateAlwaysFalse()
//    {
//      // Arrange
//      var root = TreeNode.Create('a');

//      var treenumerable = TestTreenumerableFactory.Create<TreeNode<char>, char>(root);

//      // Act
//      var actual =
//        treenumerable
//        .Graft(treenumerable, _ => false)
//        .ToDepthFirstMoveNext()
//        .ToArray();

//      // Assert
//      var expected = new MoveNextResult<char>[]
//      {
//        ('a', 1, 0, 0),
//        ('a', 2, 0, 0),
//      };

//      CollectionAssert.AreEqual(expected, actual);
//    }
//  }
//}