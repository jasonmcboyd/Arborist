using Arborist.Nodes;
using Arborist.TestUtils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using System.Linq;

namespace Arborist.Linq.Tests
{
  [TestClass]
  public class PostOrderTraversalTests
  {
    [TestMethod]
    public void PostOrderTraversal_TwoLevels()
    {
      // Arrange
      var root = IndexableTreeNode.Create('a', 'b', 'c');

      var treenumerable = root.ToTreenumerable();

      // Act
      var actual =
        treenumerable
        .PostOrderTraversal()
        .Do(x => Debug.WriteLine(x))
        .ToArray();

      // Assert
      var expected = new[] { 'b', 'c', 'a' };

      CollectionAssert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void PostOrderTraversal_MultipleLevels()
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
        .PostOrderTraversal()
        .Do(x => Debug.WriteLine(x))
        .ToArray();

      // Assert
      var expected = new[] { 'c', 'd', 'b', 'f', 'g', 'h', 'e', 'a' };

      CollectionAssert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void PostOrderTraversal_MultipleRoots_MultipleLevels()
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
        .PostOrderTraversal()
        .ToArray();

      // Assert
      var expected = new[] { 'c', 'd', 'b', 'f', 'g', 'h', 'e', 'a', 'j', 'k', 'i' };

      CollectionAssert.AreEqual(expected, actual);
    }
  }
}
