using Arborist.Tests.Utils;
using Arborist.Treenumerables;
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
      var root = TreeNode.Create('a', 'b', 'c');

      var treenumerable = TestTreenumerableFactory.Create<TreeNode<char>, char>(root);

      // Act
      var actual =
        treenumerable
        .PostOrderTraversal()
        .Do(x => Debug.WriteLine(x))
        .ToArray();

      // Assert
      var expected = new[] { 'b', 'c', 'a' };

      Assert.IsTrue(Enumerable.SequenceEqual(actual, expected));
    }

    [TestMethod]
    public void PostOrderTraversal_MultipleLevels()
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
        .PostOrderTraversal()
        .Do(x => Debug.WriteLine(x))
        .ToArray();

      // Assert
      var expected = new[] { 'c', 'd', 'b', 'f', 'g', 'h', 'e', 'a' };

      Assert.IsTrue(Enumerable.SequenceEqual(actual, expected));
    }

    [TestMethod]
    public void PostOrderTraversal_MultipleRoots_MultipleLevels()
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
        .PostOrderTraversal()
        .ToArray();

      // Assert
      var expected = new[] { 'c', 'd', 'b', 'f', 'g', 'h', 'e', 'a', 'j', 'k', 'i' };

      Assert.IsTrue(Enumerable.SequenceEqual(actual, expected));
    }
  }
}