using Arborist.SimpleSerializer;
using Arborist.TestUtils;
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
        "a"
      };
      yield return new object[]
      {
        "a,b,c",
        "a,b,c"
      };
      yield return new object[]
      {
        "a(b,c)",
        "a(ab,ac)"
      };
      yield return new object[]
      {
        "a(b(e,f,g),c(h,i,j))",
        "a(ab(abe,abf,abg),ac(ach,aci,acj))"
      };
      yield return new object[]
      {
        "a(b(c))",
        "a(ab(abc))"
      };
      yield return new object[]
      {
        "a(b,c),d(e,f)",
        "a(ab,ac),d(de,df)"
      };
      yield return new object[]
      {
        "a,b(c),d(e(f))",
        "a,b(bc),d(de(def))"
      };
    }

    public static string GetTestDisplayName(MethodInfo methodInfo, object[] data)
    {
      return data[0].ToString();
    }

    [TestMethod]
    [DynamicData(nameof(GetTestData), DynamicDataSourceType.Method, DynamicDataDisplayName = nameof(GetTestDisplayName))]
    public void RootfixScanTest_BreadthFirst(
      string treeString,
      string expectedTree)
    {
      RootfixScanTest(treeString, expectedTree, false);
    }

    [TestMethod]
    [DynamicData(nameof(GetTestData), DynamicDataSourceType.Method, DynamicDataDisplayName = nameof(GetTestDisplayName))]
    public void RootfixScanTest_DepthFirst(
      string treeString,
      string expectedTree)
    {
      RootfixScanTest(treeString, expectedTree, true);
    }

    public void RootfixScanTest(
      string treeString,
      string expectedTree,
      bool isDepthFirstTest)
    {
      // Arrange
      var treenumerable = TreeSerializer.Deserialize(treeString);

      var sut =
        treenumerable
        .RootfixScan((accumulate, visit) => accumulate.Node + visit.Node, "");

      var expected =
        isDepthFirstTest
        ? TreeSerializer.Deserialize(expectedTree).ToDepthFirstMoveNext().ToArray()
        : TreeSerializer.Deserialize(expectedTree).ToBreadthFirstMoveNext().ToArray();

      Debug.WriteLine("-----Expected Values-----");
      foreach (var value in expected)
        Debug.WriteLine(value);

      // Act
      Debug.WriteLine("\r\n-----Actual Values-----");
      var actual =
        (isDepthFirstTest
        ? sut.ToDepthFirstMoveNext()
        : sut.ToBreadthFirstMoveNext())
        .Do(visit => Debug.WriteLine(visit))
        .ToArray();

      var diff = MoveNextResultDiffer.Diff(expected, actual);

      Debug.WriteLine("\r\n-----Diffed Values-----");
      foreach (var diffResult in diff)
        Debug.WriteLine(diffResult);

      // Assert
      CollectionAssert.AreEqual(expected, actual);
    }
  }
}
