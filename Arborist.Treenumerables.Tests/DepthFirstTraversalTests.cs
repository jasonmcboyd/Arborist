using Arborist.Tests.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using System.Linq;

namespace Arborist.Treenumerables.Tests
{
  [TestClass]
  public class DepthFirstTraversalTests
  {
    [TestMethod]
    public void DepthFirstTraversal_TwoLevels_MultipleRoots_NoSkipping()
    {
      // Arrange
      var roots = new[]
      {
        TreeNode.Create('a', 'b', 'c'),
        TreeNode.Create('d', 'e', 'f')
      };

      var treenumerable = TestTreenumerableFactory.Create<TreeNode<char>, char>(roots);

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
        ('b', 1, 0, 1),
        ('b', 2, 0, 1),
        ('b', 3, 0, 1),
        ('a', 3, 0, 0),
        ('c', 1, 1, 1),
        ('c', 2, 1, 1),
        ('c', 3, 1, 1),
        ('a', 4, 0, 0),

        ('d', 1, 1, 0),
        ('d', 2, 1, 0),
        ('e', 1, 0, 1),
        ('e', 2, 0, 1),
        ('e', 3, 0, 1),
        ('d', 3, 1, 0),
        ('f', 1, 1, 1),
        ('f', 2, 1, 1),
        ('f', 3, 1, 1),
        ('d', 4, 1, 0),
      };

      CollectionAssert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void DepthFirstTraversal_OneLevel_MultipleRoots_NoSkipping()
    {
      // Arrange
      var roots = new[]
      {
        TreeNode.Create('a'),
        TreeNode.Create('b')
      };

      var treenumerable = TestTreenumerableFactory.Create<TreeNode<char>, char>(roots);

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
        ('a', 3, 0, 0),
        ('b', 1, 1, 0),
        ('b', 2, 1, 0),
        ('b', 3, 1, 0),
      };

      CollectionAssert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void DepthFirstTraversal_TwoLevels_NoSkipping()
    {
      // Arrange
      var root = TreeNode.Create('a', 'b', 'c');

      var treenumerable = TestTreenumerableFactory.Create<TreeNode<char>, char>(root);

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
        ('b', 1, 0, 1),
        ('b', 2, 0, 1),
        ('b', 3, 0, 1),
        ('a', 3, 0, 0),
        ('c', 1, 1, 1),
        ('c', 2, 1, 1),
        ('c', 3, 1, 1),
        ('a', 4, 0, 0),
      };

      CollectionAssert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void DepthFirstTraversal_TwoLevels_SkipSubtreeOnDepthOfZero()
    {
      // Arrange
      var root = TreeNode.Create('a', 'b', 'c');

      var treenumerable = TestTreenumerableFactory.Create<TreeNode<char>, char>(root);

      // Act
      var actual =
        treenumerable
        .ToDepthFirstMoveNext(visit =>
          visit.Depth == 0
          ? ChildStrategy.SkipSubtree
          : ChildStrategy.ScheduleForTraversal)
        .Do(x => Debug.WriteLine(x))
        .ToArray();

      // Assert
      var expected = new MoveNextResult<char>[]
      {
        ('a', 1, 0, 0),
      };

      CollectionAssert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void DepthFirstTraversal_TwoLevels_SkipNodeOnDepthOfZero()
    {
      // Arrange
      var root = TreeNode.Create('a', 'b', 'c');

      var treenumerable = TestTreenumerableFactory.Create<TreeNode<char>, char>(root);

      // Act
      var actual =
        treenumerable
        .ToDepthFirstMoveNext(visit =>
          visit.Node == 'a'
          ? ChildStrategy.SkipNode
          : ChildStrategy.ScheduleForTraversal)
        .Do(x => Debug.WriteLine(x))
        .ToArray();

      // Assert
      var expected = new MoveNextResult<char>[]
      {
        ('a', 1, 0, 0),
        ('b', 1, 0, 1),
        ('b', 2, 0, 1),
        ('b', 3, 0, 1),
        ('c', 1, 1, 1),
        ('c', 2, 1, 1),
        ('c', 3, 1, 1),
      };

      CollectionAssert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void DepthFirstTraversal_FiveLevels_ThreeBranches_SkipMiddleBranchNodes()
    {
      // Arrange
      var root =
        TreeNode.Create("a",
          TreeNode.Create("b",
            TreeNode.Create("bb")),
          TreeNode.Create("c",
            TreeNode.Create("cc",
              TreeNode.Create("ccc",
                TreeNode.Create("cccc")))),
          TreeNode.Create("d",
            TreeNode.Create("dd")));

      var treenumerable = TestTreenumerableFactory.Create<TreeNode<string>, string>(root);

      // Act
      var actual =
        treenumerable
        .ToDepthFirstMoveNext(visit =>
          visit.Node == "c" || visit.Node == "cc" || visit.Node == "ccc"
          ? ChildStrategy.SkipNode
          : ChildStrategy.ScheduleForTraversal)
        .Do(x => Debug.WriteLine(x))
        .ToArray();

      // Assert
      var expected = new MoveNextResult<string>[]
      {
        ("a", 1, 0, 0),
        ("a", 2, 0, 0),

          ("b", 1, 0, 1),
          ("b", 2, 0, 1),
            ("bb", 1, 0, 2),
            ("bb", 2, 0, 2),
            ("bb", 3, 0, 2),
          ("b", 3, 0, 1),
        
        ("a", 3, 0, 0),

          ("c", 1, 1, 1),
          ("cc", 1, 0, 2),
          ("ccc", 1, 0, 3),
          ("cccc", 1, 0, 4),
          ("cccc", 2, 0, 4),
          ("cccc", 3, 0, 4),

        ("a", 4, 0, 0),

          ("d", 1, 2, 1),
          ("d", 2, 2, 1),
            ("dd", 1, 0, 2),
            ("dd", 2, 0, 2),
            ("dd", 3, 0, 2),
          ("d", 3, 2, 1),

        ("a", 5, 0, 0),
      };

      CollectionAssert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void DepthFirstTraversal_ThreeLevels_SkipSubtreesAtDepthOfOne()
    {
      // Arrange
      var root =
        TreeNode.Create('a',
          TreeNode.Create('b', 'd', 'e'),
          TreeNode.Create('c', 'f', 'g'));

      var treenumerable = TestTreenumerableFactory.Create<TreeNode<char>, char>(root);

      // Act
      var actual =
        treenumerable
        .ToDepthFirstMoveNext(visit =>
          visit.Depth == 1
          ? ChildStrategy.SkipSubtree
          : ChildStrategy.ScheduleForTraversal)
        .Do(x => Debug.WriteLine(x))
        .ToArray();

      // Assert
      var expected = new MoveNextResult<char>[]
      {
        ('a', 1, 0, 0),
        ('a', 2, 0, 0),
        ('b', 1, 0, 1),
        ('a', 3, 0, 0),
        ('c', 1, 1, 1),
        ('a', 4, 0, 0),
      };

      CollectionAssert.AreEqual(expected, actual);
    }
  }
}
