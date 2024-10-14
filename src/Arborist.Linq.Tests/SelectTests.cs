using Arborist.SimpleSerializer;
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
      var treenumerable =
        TreeSerializer
        .Deserialize("1,2,3", int.Parse);

      // Act
      var actual =
        treenumerable
        .Select(visit => (char)('a' + visit.Node))
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
      var treenumerable =
        TreeSerializer
        .Deserialize("1,2,3", int.Parse);

      // Act
      var actual =
        treenumerable
        .Select(visit => (char)('a' + visit.Node))
        .LevelOrderTraversal()
        .ToArray();

      // Assert
      var expected = new[] { 'b', 'c', 'd' };

      CollectionAssert.AreEqual(expected, actual);
    }
  }
}
