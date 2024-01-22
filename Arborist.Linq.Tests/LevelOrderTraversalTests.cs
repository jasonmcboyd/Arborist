using Arborist.Tests.Utils;
using Arborist.Treenumerables;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Arborist.Linq.Tests
{
  [TestClass]
  public class LevelOrderTraversalTests
  {
    [TestMethod]
    public void LevelOrderTraversal_TwoLevels()
    {
      // Arrange
      var root = TreeNode.Create('a', 'b', 'c');

      var treenumerable = TestTreenumerableFactory.Create<TreeNode<char>, char>(root);

      // Act
      var actual =
        treenumerable
        .LevelOrderTraversal()
        .ToArray();

      // Assert
      var expected = new[] { 'a', 'b', 'c' };

      CollectionAssert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void LevelOrderTraversal_MultipleLevels()
    {
      // Arrange
      var root =
        TreeNode.Create('a',
          TreeNode.Create('b', 'c', 'd'),
          TreeNode.Create('e', 'f', 'g', 'h'));

      var treenumerable = TestTreenumerableFactory.Create<TreeNode<char>, char>(root);

      // Act
      var actual =
        treenumerable
        .LevelOrderTraversal()
        .ToArray();

      // Assert
      var expected = new[] { 'a', 'b', 'e', 'c', 'd', 'f', 'g', 'h' };

      CollectionAssert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void LevelOrderTraversal_MultipleRoots_MultipleLevels()
    {
      // Arrange
      var roots = new[]
      {
        TreeNode.Create('a',
          TreeNode.Create('b', 'c', 'd'),
          TreeNode.Create('e', 'f', 'g', 'h')),
        TreeNode.Create('i', 'j', 'k')
      };

      var treenumerable = TestTreenumerableFactory.Create<TreeNode<char>, char>(roots);

      // Act
      var actual =
        treenumerable
        .LevelOrderTraversal()
        .ToArray();

      // Assert
      var expected = new[] { 'a', 'i', 'b', 'e', 'j', 'k', 'c', 'd', 'f', 'g', 'h' };

      CollectionAssert.AreEqual(expected, actual);
    }
  }
}