using Arborist.SimpleSerializer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Arborist.Linq.Tests
{
  [TestClass]
  public class LevelOrderTraversalTests
  {
    [TestMethod]
    public void LevelOrderTraversal_TwoLevels()
    {
      // Arrange
      var treenumerable = TreeSerializer.Deserialize("a(b,c)");

      // Act
      var actual =
        treenumerable
        .LevelOrderTraversal()
        .ToArray();

      // Assert
      var expected = new[] { "a", "b", "c" };

      CollectionAssert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void LevelOrderTraversal_MultipleLevels()
    {
      // Arrange
      var treenumerable = TreeSerializer.Deserialize("a(b(c,d),e(f,g,h))");

      // Act
      var actual =
        treenumerable
        .LevelOrderTraversal()
        .ToArray();

      // Assert
      var expected = new[] { "a", "b", "e", "c", "d", "f", "g", "h" };

      CollectionAssert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void LevelOrderTraversal_MultipleRoots_MultipleLevels()
    {
      // Arrange
      var treenumerable = TreeSerializer.Deserialize("a(b(c,d),e(f,g,h)),i(j,k)");

      // Act
      var actual =
        treenumerable
        .LevelOrderTraversal()
        .ToArray();

      // Assert
      var expected = new[] { "a", "i", "b", "e", "j", "k", "c", "d", "f", "g", "h" };

      CollectionAssert.AreEqual(expected, actual);
    }
  }
}
