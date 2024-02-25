using Arborist.Nodes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Arborist.Linq.Tests
{
  [TestClass]
  public class SelectTests
  {
    [TestMethod]
    public void PreOrderTraversal_TwoLevels()
    {
      // Arrange
      var root = IndexableTreeNode.Create(1, 2, 3);

      var treenumerable = root.ToTreenumerable();

      // Act
      var actual =
        treenumerable
        .Select(visit => (char)('a' + visit.Node))
        .PreOrderTraversal()
        .ToArray();

      // Assert
      var expected = new[] { 'b', 'c', 'd' };

      CollectionAssert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void LevelOrderTraversal_TwoLevels()
    {
      // Arrange
      var root = IndexableTreeNode.Create(1, 2, 3);

      var treenumerable = root.ToTreenumerable();

      // Act
      var actual =
        treenumerable
        .Select(visit => (char)('a' + visit.Node))
        .LevelOrderTraversal()
        .ToArray();

      // Assert
      var expected = new[] { 'b', 'c', 'd' };

      CollectionAssert.AreEqual(expected, actual);
    }
  }
}
