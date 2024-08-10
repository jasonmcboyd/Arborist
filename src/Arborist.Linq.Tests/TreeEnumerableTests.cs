using Arborist.Core;
using Arborist.SimpleSerializer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Arborist.Linq.Tests
{
  [TestClass]
  public class TreeEnumerableTests
  {
    public static IEnumerable<object[]> GetTestData()
    {
      yield return new object[] { "",                   "",                "" };
      yield return new object[] { "a",                  "a:",              "a" };
      yield return new object[] { "a(b(e),c,d(f))",     "a|bcd|e::f:",     "a(b(e)cd(f))" };
      yield return new object[] { "a(b)",               "a|b:",            "a(b)" };
      yield return new object[] { "a(b,c(g),d,e(h),f)", "a|bcdef|:g::h::", "a(bc(g)de(h)f)" };
      yield return new object[] { "a(b,c)",             "a|bc:",           "a(bc)" };
      yield return new object[] { "a(f),b,c,d,e",       "abcde|f::::",     "a(f)bcde" };
      yield return new object[] { "a,b",                "ab:",             "ab" };
      yield return new object[] { "a,b,c,d,e(f)",       "abcde|::::f:",    "abcde(f)" };
    }

    public static string GetTestDisplayName(MethodInfo methodInfo, object[] data)
    {
      return string.IsNullOrEmpty(data[0].ToString()) ? "<empty-string>" : data[0].ToString();
    }

    [TestMethod]
    [DynamicData(nameof(GetTestData), DynamicDataSourceType.Method, DynamicDataDisplayName = nameof(GetTestDisplayName))]
    public void BreadthFirstTreeEnumerable(
      string treeString,
      string expectedBreadthFirst,
      string expectedDepthFirst)
    {
      Test(treeString, expectedBreadthFirst, TreeTraversalStrategy.BreadthFirst);
    }

    [TestMethod]
    [DynamicData(nameof(GetTestData), DynamicDataSourceType.Method, DynamicDataDisplayName = nameof(GetTestDisplayName))]
    public void DepthFirstTreeEnumerable(
      string treeString,
      string expectedBreadthFirst,
      string expectedDepthFirst)
    {
      Test(treeString, expectedDepthFirst, TreeTraversalStrategy.DepthFirst);
    }

    private void Test(
      string treeString,
      string expected,
      TreeTraversalStrategy treeTraversalStrategy)
    {
      // Arrange
      var treenumerable = TreeSerializer.Deserialize(treeString);

      // Act
      var enumerable =
        treeTraversalStrategy == TreeTraversalStrategy.BreadthFirst
        ? treenumerable.ToBreadthFirstTreeEnumerable().Select(token => token.ToString())
        : treenumerable.ToDepthFirstTreeEnumerable().Select(token => token.ToString());

      var actual = string.Join("", enumerable);

      // Assert
      Assert.AreEqual(expected, actual);
    }
  }
}
