using Arborist.Linq;
using Arborist.Tests.Utils;
using Arborist.Treenumerables;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using System.Linq;

namespace Arborist.Linq.Tests
{
  [TestClass]
  public class WhereTests
  {
    [TestMethod]
    public void Where()
    {
      // Arrange
      var root =
        TreeNode.Create('a',
          TreeNode.Create('b', 'c', 'd'),
          TreeNode.Create('e', 'f', 'g'));

      var treenumerable =
        TestTreenumerableFactory
        .Create<TreeNode<char>, char>(root)
        .Where(x => x.Node != 'b' && x.Node != 'e');

      // Act
      var actual =
        treenumerable
        .GetDepthFirstTraversal()
        .Do(x => Debug.WriteLine($"{x.Node} : {x.SiblingIndex} : {x.Depth}"))
        .Select(x => (x.Node, x.SiblingIndex, x.Depth))
        .ToArray();

      // Assert
      var expected = new[]
      {
        ('a', 1, 0),
        ('c', 1, 1),
        ('c', 2, 1),
        ('a', 2, 0),
        ('d', 1, 1),
        ('d', 2, 1),
        ('a', 3, 0),
        ('f', 1, 1),
        ('f', 2, 1),
        ('a', 4, 0),
        ('g', 1, 1),
        ('g', 2, 1),
        ('a', 5, 0)
      };

      Assert.IsTrue(Enumerable.SequenceEqual(actual, expected));
    }
  }
}