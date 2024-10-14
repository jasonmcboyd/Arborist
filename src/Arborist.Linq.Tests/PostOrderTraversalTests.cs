using Arborist.SimpleSerializer;
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
      var treenumerable = TreeSerializer.Deserialize("a(b,c)");

      // Act
      var actual =
        treenumerable
        .PostOrderTraversal()
        .Do(x => Debug.WriteLine(x))
        .ToArray();

      // Assert
      var expected = new[] { "b", "c", "a" };

      CollectionAssert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void PostOrderTraversal_MultipleLevels()
    {
      // Arrange
      var treenumerable = TreeSerializer.Deserialize("a(b(c,d),e(f,g,h))");

      // Act
      var actual =
        treenumerable
        .PostOrderTraversal()
        .Do(x => Debug.WriteLine(x))
        .ToArray();

      // Assert
      var expected = new[] { "c", "d", "b", "f", "g", "h", "e", "a" };

      CollectionAssert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void PostOrderTraversal_MultipleRoots_MultipleLevels()
    {
      // Arrange
      var treenumerable = TreeSerializer.Deserialize("a(b(c,d),e(f,g,h)),i(j,k)");

      // Act
      var actual =
        treenumerable
        .PostOrderTraversal()
        .ToArray();

      // Assert
      var expected = new[] { "c", "d", "b", "f", "g", "h", "e", "a", "j", "k", "i" };

      CollectionAssert.AreEqual(expected, actual);
    }
  }
}
