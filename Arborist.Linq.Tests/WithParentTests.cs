using Arborist.Linq;
using Arborist.Tests.Utils;
using Arborist.Treenumerables;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Arborist.Linq.Tests
{
  [TestClass]
  public class WithParentTests
  {
    [TestMethod]
    public void PreOrderTraversal()
    {
      // Arrange
      var root =
        TreeNode.Create(1,
          TreeNode.Create(2, 3, 4, 5),
          TreeNode.Create(6, 7));

      var treenumerable = TestTreenumerableFactory.Create<TreeNode<int>, int>(root);

      // Act
      var actual =
        treenumerable
        .WithParent()
        .Select(step => (step.Node.Node, step.Node.ParentNode))
        .PreOrderTraversal()
        .ToArray();

      // Assert
      var expected =
        new[]
        {
          (1, default(int?)),
          (2, 1),
          (3, 2),
          (4, 2),
          (5, 2),
          (6, 1),
          (7, 6),
        };

      CollectionAssert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void LevelOrderTraversal()
    {
      // Arrange
      var root =
        TreeNode.Create(1,
          TreeNode.Create(2, 3, 4, 5),
          TreeNode.Create(6, 7));

      var treenumerable = TestTreenumerableFactory.Create<TreeNode<int>, int>(root);

      // Act
      var actual =
        treenumerable
        .WithParent()
        .Select(step => (step.Node.Node, step.Node.ParentNode))
        .LevelOrderTraversal()
        .ToArray();

      // Assert
      var expected =
        new[]
        {
          (1, default(int?)),
          (2, 1),
          (6, 1),
          (3, 2),
          (4, 2),
          (5, 2),
          (7, 6),
        };

      CollectionAssert.AreEqual(expected, actual);
    }
  }
}
