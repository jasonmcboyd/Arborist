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
  public class SkipTreesTests
  {
    public static IEnumerable<object[]> GetTestData()
    {
      var testData = new[]
      {
        ("a,b,c", "a,b,c", 0),
        ("a,b,c", "b,c",   1),
        ("a,b,c", "c",     2),
        ("a,b,c", "",      3),
        ("a,b,c", "",      4),

        ("a(c,d),b(e,f)", "a(c,d),b(e,f)", 0),
        ("a(c,d),b(e,f)", "b(e,f)",        1),
        ("a(c,d),b(e,f)", "",              2),
      };

      foreach (var data in testData)
      {
        yield return new object[] { data.Item1, data.Item2, data.Item3, true };
        yield return new object[] { data.Item1, data.Item2, data.Item3, false };
      }
    }

    public static string GetTestDisplayName(MethodInfo methodInfo, object[] data)
    {
      return $"{data[0]} | {data[1]} | {data[2]} | {data[3]}";
    }

    [TestMethod]
    [DynamicData(nameof(GetTestData), DynamicDataSourceType.Method, DynamicDataDisplayName = nameof(GetTestDisplayName))]
    public void SkipTrees(
      string treeString,
      string expectedTreeString,
      int skipCount,
      bool isDepthFirst)
    {
      // Arrange
      var treenumerable = TreeSerializer.Deserialize(treeString).SkipTrees(skipCount);

      var expectedTreenumerable = TreeSerializer.Deserialize(expectedTreeString);

      var expected =
        isDepthFirst
        ? expectedTreenumerable.GetDepthFirstTraversal()
        : expectedTreenumerable.GetBreadthFirstTraversal();

      // Act
      var actual =
        (isDepthFirst
        ? treenumerable.GetDepthFirstTraversal()
        : treenumerable.GetBreadthFirstTraversal())
        .Do(visit => Debug.WriteLine(visit));

      // Assert
      CollectionAssert.AreEqual(expected.ToArray(), actual.ToArray());
    }
  }
}
