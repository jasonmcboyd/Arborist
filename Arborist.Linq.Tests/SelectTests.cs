using Arborist.Tests.Utils;
using Arborist.Treenumerables;
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
      var root = TreeNode.Create(1, 2, 3);

      var treenumerable = TestTreenumerableFactory.Create<TreeNode<int>, int>(root);

      // Act
      var actual =
        treenumerable
        .Select(x => (char)('a' + x.Node))
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
      var root = TreeNode.Create(1, 2, 3);

      var treenumerable = TestTreenumerableFactory.Create<TreeNode<int>, int>(root);

      // Act
      var actual =
        treenumerable
        .Select(x => (char)('a' + x.Node))
        .LevelOrderTraversal()
        .ToArray();

      // Assert
      var expected = new[] { 'b', 'c', 'd' };

      CollectionAssert.AreEqual(expected, actual);
    }
  }
}
