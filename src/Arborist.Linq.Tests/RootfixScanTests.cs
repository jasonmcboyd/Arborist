using Arborist.SimpleSerializer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace Arborist.Linq.Tests
{
  [TestClass]
  public class RootfixScanTests
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
        new[] { "a", "ab", "ac" }
      };
      yield return new object[]
      {
        "a(b(e,f,g),c(h,i,j))",
        new[] { "a", "ab", "abe", "abf", "abg", "c", "ch", "ci", "cj" }
      };
      yield return new object[]
      {
        "a(b(c))",
        new[] { "a", "ab", "abc" }
      };
      yield return new object[]
      {
        "a(b,c),d(e,f)",
        new[] { "a", "ab", "ac", "d", "de", "df" }
      };
      yield return new object[]
      {
        "a,b(c),d(e(f))",
        new[] { "a", "b", "bc", "d", "de", "def" }
      };
    }

    public static string GetTestDisplayName(MethodInfo methodInfo, object[] data)
    {
      return data[0].ToString();
    }

    [TestMethod]
    [DynamicData(nameof(GetTestData), DynamicDataSourceType.Method, DynamicDataDisplayName = nameof(GetTestDisplayName))]
    public void RootfixScan(
      string treeString,
      string[] expected)
    {
      // Arrange
      var treenumerable = TreeSerializer.Deserialize(treeString);

      // Act
      var actual =
        treenumerable
        .Do(visit => Debug.WriteLine((visit.Node, visit.VisitCount, visit.OriginalPosition.SiblingIndex, visit.OriginalPosition.Depth)))
        .RootfixScan((visit, accumulate) => visit.Node + accumulate.Node)
        .Do(visit => Debug.WriteLine((visit.Node, visit.VisitCount, visit.OriginalPosition.SiblingIndex, visit.OriginalPosition.Depth)))
        .PreOrderTraversal()
        .ToArray();

      // Assert
      CollectionAssert.AreEqual(expected, actual);
    }
  }
}
