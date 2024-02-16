using Arborist.Tests.Utils;
using Arborist.Treenumerables;
using Arborist.Treenumerables.Nodes;
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
      var root = IndexableTreeNode.Create('a', 'b', 'c');

      var treenumerable = root.ToTreenumerable();

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
        IndexableTreeNode.Create('a',
          IndexableTreeNode.Create('b', 'c', 'd'),
          IndexableTreeNode.Create('e', 'f', 'g', 'h'));

      var treenumerable = root.ToTreenumerable();

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
        IndexableTreeNode.Create('a',
          IndexableTreeNode.Create('b', 'c', 'd'),
          IndexableTreeNode.Create('e', 'f', 'g', 'h')),
        IndexableTreeNode.Create('i', 'j', 'k')
      };

      var treenumerable = roots.ToTreenumerable();

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
