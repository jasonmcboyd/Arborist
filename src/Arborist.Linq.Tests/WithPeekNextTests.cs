using Arborist.Linq;
using Arborist.Nodes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Arborist.Linq.Tests
{
  [TestClass]
  public class WithPeekNextTests
  {
    [TestMethod]
    public void WithPeekNext_GetDepthFirstTraversal()
    {
      // Arrange
      var root = IndexableTreeNode.Create('a', 'b', 'c');

      var treenumerable = root.ToTreenumerable();

      // Act
      var actual =
        treenumerable
        .WithPeekNext()
        .GetDepthFirstTraversal()
        .Select(visit => (
          visit.Node.Node,
          visit.VisitCount,
          visit.OriginalPosition.SiblingIndex,
          visit.OriginalPosition.Depth,
          visit.Node.HasNextVisit ? visit.Node.NextVisit.Node : (char?)null))
        .ToArray();

      // Assert
      var expected =
        new (char, int, int, int, char?)[]
        {
          ('a', 1, 0, 0, 'b'),
          ('b', 1, 0, 1, 'b'),
          ('b', 2, 0, 1, 'a'),
          ('a', 2, 0, 0, 'c'),
          ('c', 1, 1, 1, 'c'),
          ('c', 2, 1, 1, 'a'),
          ('a', 3, 0, 0, null)
        };

      CollectionAssert.AreEqual(expected, actual);
    }
  }
}
