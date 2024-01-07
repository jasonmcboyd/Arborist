using Arborist.Linq;
using Arborist.Tests.Utils;
using Arborist.Treenumerables;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using System.Linq;

namespace Arborist.Linq.Tests
{
  [TestClass]
  public class PreOrderTraversalTests
  {
    [TestMethod]
    public void PreOrderTraversal_TwoLevels()
    {
      // Arrange
      var root = TreeNode.Create('a', 'b', 'c');

      var treenumerable = TestTreenumerableFactory.Create<TreeNode<char>, char>(root);

      // Act
      var actual =
        treenumerable
        .PreOrderTraversal()
        .Do(x => Debug.WriteLine(x))
        .ToArray();

      // Assert
      var expected = new[] { 'a', 'b', 'c' };

      Assert.IsTrue(Enumerable.SequenceEqual(actual, expected));
    }

    [TestMethod]
    public void PreOrderTraversal_MultipleLevels()
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
        .PreOrderTraversal()
        .ToArray();

      // Assert
      var expected = new[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h' };

      Assert.IsTrue(Enumerable.SequenceEqual(actual, expected));
    }

    [TestMethod]
    public void PreOrderTraversal_MultipleRoots_MultipleLevels()
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
        .PreOrderTraversal()
        .ToArray();

      // Assert
      var expected = new[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k' };

      Assert.IsTrue(Enumerable.SequenceEqual(actual, expected));
    }
  }
}