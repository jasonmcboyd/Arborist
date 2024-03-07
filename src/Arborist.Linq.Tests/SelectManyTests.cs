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
  public class SelectManyTests
  {
    public static IEnumerable<object[]> GetTestData()
    {
      return new[]
      {
        new[]
        {
          "",
          "a",
          ""
        },
        new[]
        {
          "a",
          "b,c",
          "ab,ac"
        },
        new[]
        {
          "a",
          "b(c)",
          "ab(ac)"
        },
        new[]
        {
          "a(b(d),c)",
          "a(b(d),c)",
          "aa(ab(ad),ac,ba(bb(bd),bc,da(db(dd),dc)),ca(cb(cd),cc))"
        },
        new[]
        {
          "a(b(d),c)",
          "e",
          "ae(be(de),ce)"
        },
        new[]
        {
          "a(b)",
          "c",
          "ac(bc)"
        },
        new[]
        {
          "a(b)",
          "c(d)",
          "ac(ad,bc(bd))"
        },
        new[]
        {
          "a(b)",
          "c,d",
          "ac,ad(bc,bd)"
        },
        new[]
        {
          "a(b,c)",
          "d",
          "ad(bd,cd)"
        },
        new[]
        {
          "a(b,c)",
          "d(e)",
          "ad(ae,bd(be),cd(ce))"
        },
        new[]
        {
          "a(b,c)",
          "d(e,f)",
          "ad(ae,af,bd(be,bf),cd,(ce,cf))"
        },
        new[]
        {
          "a(b,c)",
          "d,e",
          "ad,ae(bd,be,cd,ce)"
        },
        new[]
        {
          "a(d),b,c(e)",
          "f",
          "af(df),bf,cf(ef)"
        },
        new[]
        {
          "a,b",
          "c",
          "ac,bc"
        },
        new[]
        {
          "a,b",
          "c(d)",
          "ac(ad),bc(bd)"
        },
        new[]
        {
          "a,b",
          "c,d",
          "ac,ad,bc,bd"
        },
        new[]
        {
          "a,b(c)",
          "a(b)",
          "aa(ab),ba(bb,ca(cb))"
        },
      };
    }

    public static string GetTestDisplayName(MethodInfo methodInfo, object[] data)
      => $"{data[0]} | {data[1]}";

    [TestMethod]
    [DynamicData(nameof(GetTestData), DynamicDataSourceType.Method, DynamicDataDisplayName = nameof(GetTestDisplayName))]
    public void SelectMany_BreadthFirst(
      string treeString,
      string innerTreeString,
      string expectedResults)
    {
      SelectManyTest(treeString, innerTreeString, expectedResults, false);
    }

    [TestMethod]
    [DynamicData(nameof(GetTestData), DynamicDataSourceType.Method, DynamicDataDisplayName = nameof(GetTestDisplayName))]
    public void SelectMany_DepthFirst(
      string treeString,
      string innerTreeString,
      string expectedResults)
    {
      SelectManyTest(treeString, innerTreeString, expectedResults, true);
    }

    private void SelectManyTest(
      string treeString,
      string innerTreeString,
      string expectedResults,
      bool isDepthFirstTest)
    {
      // Arrange
      var treenumerable = TreeSerializer.Deserialize(treeString);
      var innerTreenumerable = TreeSerializer.Deserialize(innerTreeString);

      var sut =
        treenumerable
        .SelectMany(x => innerTreenumerable.Select(y => x + y.Node));

      var expected =
        isDepthFirstTest
        ? TreeSerializer.Deserialize(expectedResults).ToDepthFirstMoveNext().ToArray()
        : TreeSerializer.Deserialize(expectedResults).ToBreadthFirstMoveNext().ToArray();

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
