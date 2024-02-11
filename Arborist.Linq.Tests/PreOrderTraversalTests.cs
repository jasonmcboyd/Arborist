using Arborist.Linq;
using Arborist.Tests.Utils;
using Arborist.Treenumerables.SimpleSerializer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace Arborist.Linq.Tests
{
  [TestClass]
  public class PreOrderTraversalTests
  {
    public static IEnumerable<object[]> GetTestData()
    {
      yield return new object[]
      {
        "a",
        new[] { "a" }
      };
      yield return new object[]
      {
        "a,b,c",
        new[] { "a", "b", "c" }
      };
      yield return new object[]
      {
        "a(b,c)",
        new[] { "a", "b", "c" }
      };
      yield return new object[]
      {
        "a(b(e,f,g),c(h,i,j))",
        new[] { "a", "b", "e", "f", "g", "c", "h", "i", "j" }
      };
      yield return new object[]
      {
        "a(b(c))",
        new[] { "a", "b", "c" }
      };
      yield return new object[]
      {
        "a(b,c),d(e,f)",
        new[] { "a", "b", "c", "d", "e", "f" }
      };
      yield return new object[]
      {
        "a,b(c),d(e(f))",
        new[] { "a", "b", "c", "d", "e", "f" }
      };
    }

    public static string GetTestDisplayName(MethodInfo methodInfo, object[] data)
    {
      return data[0].ToString();
    }

    [TestMethod]
    [DynamicData(nameof(GetTestData), DynamicDataSourceType.Method, DynamicDataDisplayName = nameof(GetTestDisplayName))]
    public void PreOrderTraversal(
      string treeString,
      string[] expected)
    {
      // Arrange
      var treenumerable = TreeSerializer.Deserialize(treeString);

      // Act
      var actual =
        treenumerable
        .PreOrderTraversal()
        .Do(visit => Debug.WriteLine(visit))
        .ToArray();

      // Assert
      CollectionAssert.AreEqual(expected, actual);
    }
  }
}
