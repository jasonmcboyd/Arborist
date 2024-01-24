using Arborist.Linq;
using Arborist.Tests.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace Arborist.Linq.Tests
{
  [TestClass]
  public class GetLeavesTests
  {
    public static IEnumerable<object[]> GetTestData()
    {
      yield return new object[] { "a", new[] { "a" } };
      yield return new object[] { "a,b,c", new[] { "a", "b", "c" } };
      yield return new object[] { "a(b,c)", new[] { "b", "c" } };
      yield return new object[] { "a(b(c))", new[] { "c" } };
      yield return new object[] { "a(b,c),d(e,f)", new[] { "b", "c", "e", "f" } };
      yield return new object[] { "a,b(c),d(e(f))", new[] { "a", "c", "f" } };
    }

    public static string GetTestDisplayName(MethodInfo methodInfo, object[] data)
    {
      return data[0].ToString();
    }

    [TestMethod]
    [DynamicData(nameof(GetTestData), DynamicDataSourceType.Method, DynamicDataDisplayName = nameof(GetTestDisplayName))]
    public void GetLeaves(
      string treeString,
      string[] expected)
    {
      // Arrange
      var treenumerable = TreeStringParser.ParseTreeString(treeString);

      // Act
      var actual =
        treenumerable
        .GetLeaves()
        .Do(node => Debug.WriteLine(node))
        .ToArray();

      // Assert
      CollectionAssert.AreEqual(expected, actual);
    }
  }
}
