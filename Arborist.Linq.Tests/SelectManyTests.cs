using Arborist.Treenumerables.Nodes;
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
      var subtree = IndexableTreeNode.Create(1, 2, 3).ToTreenumerable();

      var root = IndexableTreeNode.Create(subtree, subtree, subtree);

      var treenumerable = root.ToTreenumerable();

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
