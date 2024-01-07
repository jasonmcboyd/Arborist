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
        (TreenumeratorState.SchedulingNode, "a", 0, 0, 0),
        (TreenumeratorState.VisitingNode,   "a", 1, 0, 0),

          (TreenumeratorState.SchedulingNode, "b", 0, 0, 1),
          (TreenumeratorState.VisitingNode,   "b", 1, 0, 1),
            (TreenumeratorState.SchedulingNode, "bb", 0, 0, 2),
            (TreenumeratorState.VisitingNode,   "bb", 1, 0, 2),
            (TreenumeratorState.VisitingNode,   "bb", 2, 0, 2),
          (TreenumeratorState.VisitingNode,   "b", 2, 0, 1),

        (TreenumeratorState.VisitingNode,   "a", 2, 0, 0),

          (TreenumeratorState.SchedulingNode, "c", 0, 1, 1),
          (TreenumeratorState.SchedulingNode, "cc", 0, 0, 2),
          (TreenumeratorState.SchedulingNode, "ccc", 0, 0, 3),
          (TreenumeratorState.SchedulingNode, "cccc", 0, 0, 4),
          (TreenumeratorState.VisitingNode,   "cccc", 1, 1, 1),
          (TreenumeratorState.VisitingNode,   "cccc", 2, 1, 1),

        (TreenumeratorState.VisitingNode, "a", 3, 0, 0),

          (TreenumeratorState.SchedulingNode, "d", 0, 2, 1),
          (TreenumeratorState.VisitingNode,   "d", 1, 2, 1),
            (TreenumeratorState.SchedulingNode, "dd", 0, 0, 2),
            (TreenumeratorState.VisitingNode,   "dd", 1, 0, 2),
            (TreenumeratorState.VisitingNode,   "dd", 2, 0, 2),
          (TreenumeratorState.VisitingNode, "d", 2, 2, 1),

        (TreenumeratorState.VisitingNode, "a", 4, 0, 0),
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
        (TreenumeratorState.SchedulingNode, 'a', 0, 0, 0),
        (TreenumeratorState.VisitingNode,   'a', 1, 0, 0),
        (TreenumeratorState.VisitingNode,   'a', 2, 0, 0),
        (TreenumeratorState.SchedulingNode, 'b', 0, 1, 0),
        (TreenumeratorState.VisitingNode,   'b', 1, 1, 0),
        (TreenumeratorState.VisitingNode,   'b', 2, 1, 0),
      };

      CollectionAssert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void DepthFirstTraversal_OneLevel_MultipleRoots_SkipSubtreeOfFirstRoot()
    {
      // Arrange
      var roots = new[]
      {
        TreeNode.Create('a'),
        TreeNode.Create('b'),
        TreeNode.Create('c')
      };

      var treenumerable = TestTreenumerableFactory.Create<TreeNode<char>, char>(roots);

      // Act
      var actual =
        treenumerable
        .ToDepthFirstMoveNext(visit =>
          visit.Node == 'a'
          ? ChildStrategy.SkipSubtree
          : ChildStrategy.ScheduleForTraversal)
        .Do(x => Debug.WriteLine(x))
        .ToArray();

      // Assert
      var expected = new MoveNextResult<char>[]
      {
        (TreenumeratorState.SchedulingNode, 'a', 0, 0, 0),
        (TreenumeratorState.SchedulingNode, 'b', 0, 1, 0),
        (TreenumeratorState.VisitingNode,   'b', 1, 0, 0),
        (TreenumeratorState.VisitingNode,   'b', 2, 0, 0),
        (TreenumeratorState.SchedulingNode, 'c', 0, 2, 0),
        (TreenumeratorState.VisitingNode,   'c', 1, 1, 0),
        (TreenumeratorState.VisitingNode,   'c', 2, 1, 0),
      };

      CollectionAssert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void DepthFirstTraversal_OneLevel_MultipleRoots_SkipSubtreeOfSecondRoot()
    {
      // Arrange
      var roots = new[]
      {
        TreeNode.Create('a'),
        TreeNode.Create('b'),
        TreeNode.Create('c')
      };

      var treenumerable = TestTreenumerableFactory.Create<TreeNode<char>, char>(roots);

      // Act
      var actual =
        treenumerable
        .ToDepthFirstMoveNext(visit =>
          visit.Node == 'b'
          ? ChildStrategy.SkipSubtree
          : ChildStrategy.ScheduleForTraversal)
        .Do(x => Debug.WriteLine(x))
        .ToArray();

      // Assert
      var expected = new MoveNextResult<char>[]
      {
        (TreenumeratorState.SchedulingNode, 'a', 0, 0, 0),
        (TreenumeratorState.VisitingNode,   'a', 1, 0, 0),
        (TreenumeratorState.VisitingNode,   'a', 2, 0, 0),
        (TreenumeratorState.SchedulingNode, 'b', 0, 1, 0),
        (TreenumeratorState.SchedulingNode, 'c', 0, 2, 0),
        (TreenumeratorState.VisitingNode,   'c', 1, 1, 0),
        (TreenumeratorState.VisitingNode,   'c', 2, 1, 0),
      };

      CollectionAssert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void DepthFirstTraversal_ThreeLevels_OneNodePerLevel_SkipNodeAtLevelOne()
    {
      // Arrange
      var root =
        TreeNode.Create('a',
          TreeNode.Create('b', 'c'));

      var treenumerable = TestTreenumerableFactory.Create<TreeNode<char>, char>(root);

      // Act
      var actual =
        treenumerable
        .ToDepthFirstMoveNext(visit =>
          visit.Depth == 1
          ? ChildStrategy.SkipNode
          : ChildStrategy.ScheduleForTraversal)
        .Do(x => Debug.WriteLine(x))
        .ToArray();

      // Assert
      var expected = new MoveNextResult<char>[]
      {
        (TreenumeratorState.SchedulingNode, 'a', 0, 0, 0),
        (TreenumeratorState.VisitingNode  , 'a', 1, 0, 0),
        (TreenumeratorState.SchedulingNode, 'b', 0, 0, 1),
        (TreenumeratorState.SchedulingNode, 'c', 0, 0, 2),
        (TreenumeratorState.VisitingNode,   'c', 1, 0, 1),
        (TreenumeratorState.VisitingNode,   'c', 2, 0, 1),
        (TreenumeratorState.VisitingNode,   'a', 2, 0, 0),
      };

      CollectionAssert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void DepthFirstTraversal_ThreeLevels_OneNodePerLevel_SkipNodeAtLevelTwo()
    {
      // Arrange
      var root =
        TreeNode.Create('a',
          TreeNode.Create('b', 'c'));

      var treenumerable = TestTreenumerableFactory.Create<TreeNode<char>, char>(root);

      // Act
      var actual =
        treenumerable
        .ToDepthFirstMoveNext(visit =>
          visit.Depth == 2
          ? ChildStrategy.SkipNode
          : ChildStrategy.ScheduleForTraversal)
        .Do(x => Debug.WriteLine(x))
        .ToArray();

      // Assert
      var expected = new MoveNextResult<char>[]
      {
        (TreenumeratorState.SchedulingNode, 'a', 0, 0, 0),
        (TreenumeratorState.VisitingNode  , 'a', 1, 0, 0),
        (TreenumeratorState.SchedulingNode, 'b', 0, 0, 1),
        (TreenumeratorState.VisitingNode,   'b', 1, 0, 1),
        (TreenumeratorState.SchedulingNode, 'c', 0, 0, 2),
        (TreenumeratorState.VisitingNode,   'b', 2, 0, 1),
        (TreenumeratorState.VisitingNode,   'a', 2, 0, 0),
      };

      CollectionAssert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void DepthFirstTraversal_ThreeLevels_OneNodePerLevel_SkipNodeOnAtLevelZero()
    {
      // Arrange
      var root =
        TreeNode.Create('a',
          TreeNode.Create('b', 'c'));

      var treenumerable = TestTreenumerableFactory.Create<TreeNode<char>, char>(root);

      // Act
      var actual =
        treenumerable
        .ToDepthFirstMoveNext(visit =>
          visit.Depth == 0
          ? ChildStrategy.SkipNode
          : ChildStrategy.ScheduleForTraversal)
        .Do(x => Debug.WriteLine(x))
        .ToArray();

      // Assert
      var expected = new MoveNextResult<char>[]
      {
        (TreenumeratorState.SchedulingNode, 'a', 0, 0, 0),
        (TreenumeratorState.SchedulingNode, 'b', 0, 0, 1),
        (TreenumeratorState.VisitingNode,   'b', 1, 0, 0),
        (TreenumeratorState.SchedulingNode, 'c', 0, 0, 2),
        (TreenumeratorState.VisitingNode,   'c', 1, 0, 1),
        (TreenumeratorState.VisitingNode,   'c', 2, 0, 1),
        (TreenumeratorState.VisitingNode,   'b', 2, 0, 0),
      };

      CollectionAssert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void DepthFirstTraversal_ThreeLevels_SkipNodeAtLevelOne()
    {
      // Arrange
      var root =
        TreeNode.Create('a',
          TreeNode.Create('b', 'c', 'd', 'e'));

      var treenumerable = TestTreenumerableFactory.Create<TreeNode<char>, char>(root);

      // Act
      var actual =
        treenumerable
        .ToDepthFirstMoveNext(visit =>
          visit.Depth == 1
          ? ChildStrategy.SkipNode
          : ChildStrategy.ScheduleForTraversal)
        .Do(x => Debug.WriteLine(x))
        .ToArray();

      // Assert
      var expected = new MoveNextResult<char>[]
      {
        (TreenumeratorState.SchedulingNode, 'a', 0, 0, 0),
        (TreenumeratorState.VisitingNode,   'a', 1, 0, 0),
        (TreenumeratorState.SchedulingNode, 'b', 0, 0, 1),
        (TreenumeratorState.SchedulingNode, 'c', 0, 0, 2),
        (TreenumeratorState.VisitingNode,   'c', 1, 0, 1),
        (TreenumeratorState.VisitingNode,   'c', 2, 0, 1),
        (TreenumeratorState.VisitingNode,   'a', 2, 0, 0),
        (TreenumeratorState.SchedulingNode, 'd', 0, 1, 2),
        (TreenumeratorState.VisitingNode,   'd', 1, 1, 1),
        (TreenumeratorState.VisitingNode,   'd', 2, 1, 1),
        (TreenumeratorState.VisitingNode,   'a', 3, 0, 0),
        (TreenumeratorState.SchedulingNode, 'e', 0, 2, 2),
        (TreenumeratorState.VisitingNode,   'e', 1, 2, 1),
        (TreenumeratorState.VisitingNode,   'e', 2, 2, 1),
        (TreenumeratorState.VisitingNode,   'a', 4, 0, 0),
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
        (TreenumeratorState.SchedulingNode, 'a', 0, 0, 0),
        (TreenumeratorState.VisitingNode  , 'a', 1, 0, 0),
        (TreenumeratorState.SchedulingNode, 'b', 0, 0, 1),
        (TreenumeratorState.SchedulingNode, 'c', 0, 1, 1),
        (TreenumeratorState.VisitingNode,   'a', 2, 0, 0),
      };

      CollectionAssert.AreEqual(expected, actual);
    }

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
        (TreenumeratorState.SchedulingNode, 'a', 0, 0, 0),
        (TreenumeratorState.VisitingNode,   'a', 1, 0, 0),
          (TreenumeratorState.SchedulingNode, 'b', 0, 0, 1),
          (TreenumeratorState.VisitingNode,   'b', 1, 0, 1),
          (TreenumeratorState.VisitingNode,   'b', 2, 0, 1),
        (TreenumeratorState.VisitingNode,   'a', 2, 0, 0),
          (TreenumeratorState.SchedulingNode, 'c', 0, 1, 1),
          (TreenumeratorState.VisitingNode,   'c', 1, 1, 1),
          (TreenumeratorState.VisitingNode,   'c', 2, 1, 1),
        (TreenumeratorState.VisitingNode,   'a', 3, 0, 0),

        (TreenumeratorState.SchedulingNode, 'd', 0, 1, 0),
        (TreenumeratorState.VisitingNode,   'd', 1, 1, 0),
          (TreenumeratorState.SchedulingNode, 'e', 0, 0, 1),
          (TreenumeratorState.VisitingNode,   'e', 1, 0, 1),
          (TreenumeratorState.VisitingNode,   'e', 2, 0, 1),
        (TreenumeratorState.VisitingNode,   'd', 2, 1, 0),
          (TreenumeratorState.SchedulingNode, 'f', 0, 1, 1),
          (TreenumeratorState.VisitingNode,   'f', 1, 1, 1),
          (TreenumeratorState.VisitingNode,   'f', 2, 1, 1),
        (TreenumeratorState.VisitingNode,   'd', 3, 1, 0),
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
        (TreenumeratorState.SchedulingNode, 'a', 0, 0, 0),
        (TreenumeratorState.VisitingNode,   'a', 1, 0, 0),
          (TreenumeratorState.SchedulingNode, 'b', 0, 0, 1),
          (TreenumeratorState.VisitingNode,   'b', 1, 0, 1),
          (TreenumeratorState.VisitingNode,   'b', 2, 0, 1),
        (TreenumeratorState.VisitingNode,   'a', 2, 0, 0),
          (TreenumeratorState.SchedulingNode, 'c', 0, 1, 1),
          (TreenumeratorState.VisitingNode,   'c', 1, 1, 1),
          (TreenumeratorState.VisitingNode,   'c', 2, 1, 1),
        (TreenumeratorState.VisitingNode,   'a', 3, 0, 0),
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
          visit.Depth == 0
          ? ChildStrategy.SkipNode
          : ChildStrategy.ScheduleForTraversal)
        .Do(x => Debug.WriteLine(x))
        .ToArray();

      // Assert
      var expected = new MoveNextResult<char>[]
      {
        (TreenumeratorState.SchedulingNode, 'a', 0, 0, 0),
          (TreenumeratorState.SchedulingNode, 'b', 0, 0, 1),
          (TreenumeratorState.VisitingNode,   'b', 1, 0, 0),
          (TreenumeratorState.VisitingNode,   'b', 2, 0, 0),
          (TreenumeratorState.SchedulingNode, 'c', 0, 1, 1),
          (TreenumeratorState.VisitingNode,   'c', 1, 1, 0),
          (TreenumeratorState.VisitingNode,   'c', 2, 1, 0),
      };

      CollectionAssert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void DepthFirstTraversal_TwoLevels_SkipNodeOnNonzeroDepth()
    {
      // Arrange
      var root = TreeNode.Create('a', 'b', 'c');

      var treenumerable = TestTreenumerableFactory.Create<TreeNode<char>, char>(root);

      // Act
      var actual =
        treenumerable
        .ToDepthFirstMoveNext(visit =>
          visit.Depth != 0
          ? ChildStrategy.SkipNode
          : ChildStrategy.ScheduleForTraversal)
        .Do(x => Debug.WriteLine(x))
        .ToArray();

      // Assert
      var expected = new MoveNextResult<char>[]
      {
        (TreenumeratorState.SchedulingNode, 'a', 0, 0, 0),
        (TreenumeratorState.VisitingNode,   'a', 1, 0, 0),
        (TreenumeratorState.SchedulingNode, 'b', 0, 0, 1),
        (TreenumeratorState.SchedulingNode, 'c', 0, 1, 1),
        (TreenumeratorState.VisitingNode,   'a', 2, 0, 0),
      };

      CollectionAssert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void DepthFirstTraversal_TwoLevels_SkipSubtreeOfFirstChild()
    {
      // Arrange
      var root = TreeNode.Create('a', 'b', 'c', 'd');

      var treenumerable = TestTreenumerableFactory.Create<TreeNode<char>, char>(root);

      // Act
      var actual =
        treenumerable
        .ToDepthFirstMoveNext(visit =>
          visit.Node == 'b'
          ? ChildStrategy.SkipSubtree
          : ChildStrategy.ScheduleForTraversal)
        .Do(x => Debug.WriteLine(x))
        .ToArray();

      // Assert
      var expected = new MoveNextResult<char>[]
      {
        (TreenumeratorState.SchedulingNode, 'a', 0, 0, 0),
        (TreenumeratorState.VisitingNode,   'a', 1, 0, 0),
          (TreenumeratorState.SchedulingNode, 'b', 0, 0, 1),
          (TreenumeratorState.SchedulingNode, 'c', 0, 1, 1),
          (TreenumeratorState.VisitingNode,   'c', 1, 0, 1),
          (TreenumeratorState.VisitingNode,   'c', 2, 0, 1),
        (TreenumeratorState.VisitingNode,   'a', 2, 0, 0),
          (TreenumeratorState.SchedulingNode, 'd', 0, 2, 1),
          (TreenumeratorState.VisitingNode,   'd', 1, 1, 1),
          (TreenumeratorState.VisitingNode,   'd', 2, 1, 1),
        (TreenumeratorState.VisitingNode,   'a', 3, 0, 0),
      };

      CollectionAssert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void DepthFirstTraversal_TwoLevels_SkipSubtreeOfLastChild()
    {
      // Arrange
      var root = TreeNode.Create('a', 'b', 'c', 'd');

      var treenumerable = TestTreenumerableFactory.Create<TreeNode<char>, char>(root);

      // Act
      var actual =
        treenumerable
        .ToDepthFirstMoveNext(visit =>
          visit.Node == 'd'
          ? ChildStrategy.SkipSubtree
          : ChildStrategy.ScheduleForTraversal)
        .Do(x => Debug.WriteLine(x))
        .ToArray();

      // Assert
      var expected = new MoveNextResult<char>[]
      {
        (TreenumeratorState.SchedulingNode, 'a', 0, 0, 0),
        (TreenumeratorState.VisitingNode,   'a', 1, 0, 0),
          (TreenumeratorState.SchedulingNode, 'b', 0, 0, 1),
          (TreenumeratorState.VisitingNode,   'b', 1, 0, 1),
          (TreenumeratorState.VisitingNode,   'b', 2, 0, 1),
        (TreenumeratorState.VisitingNode,   'a', 2, 0, 0),
          (TreenumeratorState.SchedulingNode, 'c', 0, 1, 1),
          (TreenumeratorState.VisitingNode,   'c', 1, 1, 1),
          (TreenumeratorState.VisitingNode,   'c', 2, 1, 1),
        (TreenumeratorState.VisitingNode,   'a', 3, 0, 0),
          (TreenumeratorState.SchedulingNode, 'd', 0, 2, 1),
      };

      CollectionAssert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void DepthFirstTraversal_TwoLevels_SkipSubtreeOfMiddleChild()
    {
      // Arrange
      var root = TreeNode.Create('a', 'b', 'c', 'd');

      var treenumerable = TestTreenumerableFactory.Create<TreeNode<char>, char>(root);

      // Act
      var actual =
        treenumerable
        .ToDepthFirstMoveNext(visit =>
          visit.Node == 'c'
          ? ChildStrategy.SkipSubtree
          : ChildStrategy.ScheduleForTraversal)
        .Do(x => Debug.WriteLine(x))
        .ToArray();

      // Assert
      var expected = new MoveNextResult<char>[]
      {
        (TreenumeratorState.SchedulingNode, 'a', 0, 0, 0),
        (TreenumeratorState.VisitingNode,   'a', 1, 0, 0),
          (TreenumeratorState.SchedulingNode, 'b', 0, 0, 1),
          (TreenumeratorState.VisitingNode,   'b', 1, 0, 1),
          (TreenumeratorState.VisitingNode,   'b', 2, 0, 1),
        (TreenumeratorState.VisitingNode,   'a', 2, 0, 0),
          (TreenumeratorState.SchedulingNode, 'c', 0, 1, 1),
          (TreenumeratorState.SchedulingNode, 'd', 0, 2, 1),
          (TreenumeratorState.VisitingNode,   'd', 1, 1, 1),
          (TreenumeratorState.VisitingNode,   'd', 2, 1, 1),
        (TreenumeratorState.VisitingNode,   'a', 3, 0, 0),
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
        (TreenumeratorState.SchedulingNode, 'a', 0, 0, 0),
      };

      CollectionAssert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void DepthFirstTraversal_TwoLevels_SkipSubtreeOnNonzeroDepth()
    {
      // Arrange
      var root = TreeNode.Create('a', 'b', 'c');

      var treenumerable = TestTreenumerableFactory.Create<TreeNode<char>, char>(root);

      // Act
      var actual =
        treenumerable
        .ToDepthFirstMoveNext(visit =>
          visit.Depth != 0
          ? ChildStrategy.SkipSubtree
          : ChildStrategy.ScheduleForTraversal)
        .Do(x => Debug.WriteLine(x))
        .ToArray();

      // Assert
      var expected = new MoveNextResult<char>[]
      {
        (TreenumeratorState.SchedulingNode, 'a', 0, 0, 0),
        (TreenumeratorState.VisitingNode,   'a', 1, 0, 0),
        (TreenumeratorState.SchedulingNode, 'b', 0, 0, 1),
        (TreenumeratorState.SchedulingNode, 'c', 0, 1, 1),
        (TreenumeratorState.VisitingNode,   'a', 2, 0, 0),
      };

      CollectionAssert.AreEqual(expected, actual);
    }
  }
}
