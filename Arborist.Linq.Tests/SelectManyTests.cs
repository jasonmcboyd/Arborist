using Arborist.Linq;
using Arborist.Tests.Utils;
using Arborist.Treenumerables;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Arborist.Linq.Tests
{
  [TestClass]
  public class SelectManyTests
  {
    [TestMethod]
    public void SelectMany()
    {
      // Arrange
      var subtree = TestTreenumerableFactory.Create<TreeNode<int>, int>(TreeNode.Create(1, 2, 3));

      var root = TreeNode.Create(subtree, subtree, subtree);

      var treenumerable = TestTreenumerableFactory.Create<TreeNode<ITreenumerable<int>>, ITreenumerable<int>>(root);

      // Act
      var actual =
        treenumerable
        .SelectMany()
        .PreOrderTraversal()
        .ToArray();

      // Assert
      var expected = new[] { 1, 2, 3 };

      CollectionAssert.AreEqual(expected, actual);
    }
  }
}
