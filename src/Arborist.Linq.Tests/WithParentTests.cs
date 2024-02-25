using Arborist.Linq;
using Arborist.Nodes;
using Arborist.TestUtils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
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
        IndexableTreeNode.Create(1,
          IndexableTreeNode.Create(2, 3, 4, 5),
          IndexableTreeNode.Create(6, 7));

      var treenumerable = root.ToTreenumerable();

      // Act
      var actual =
        treenumerable
        .WithParent()
        .Select(step => (step.Node.Node, step.Node.ParentNode))
        .PreOrderTraversal()
        .Do(x => Debug.WriteLine(x))
        .ToArray();

      // Assert
      var expected =
        new[]
        {
          (1, 0),
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
        IndexableTreeNode.Create(1,
          IndexableTreeNode.Create(2, 3, 4, 5),
          IndexableTreeNode.Create(6, 7));

      var treenumerable = root.ToTreenumerable();

      // Act
      var actual =
        treenumerable
        .WithParent()
        .Select(step => (step.Node.Node, step.Node.HasParentNode ? step.Node.ParentNode : default))
        .LevelOrderTraversal()
        .Do(x => Debug.WriteLine(x))
        .ToArray();

      // Assert
      var expected =
        new[]
        {
          (1, 0),
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
