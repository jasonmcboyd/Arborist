using Arborist.Core;
using Arborist.SimpleSerializer;
using Arborist.TestUtils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace Arborist.Linq.Tests
{
  [TestClass]
  public class LeaffixScanTests
  {
    public static IEnumerable<object[]> GetTestData()
    {
      return new[]
        {
          new [] { ""                , ""                   },
          new [] { "a"               , "a"                  },
          new [] { "a(b(c,d))"       , "abcd(bcd(c,d))"     },
          new [] { "a(b(d),c(e))"    , "abdce(bd(d),ce(e))" },
          new [] { "a(b(d),c)"       , "abdc(bd(d),c)"      },
          new [] { "a(b)"            , "ab(b)"              },
          new [] { "a(b,c)"          , "abc(b,c)"           },
          new [] { "a(c),b"          , "ac(c),b"           },
          new [] { "a(c),b(d)"       , "ac(c),bd(d)"        },
          new [] { "a(c,d),b(e,f)"   , "acd(c,d),bef(e,f)"  },
          new [] { "a(d),b,c(e)"     , "ad(d),b,ce(e)"      },
          new [] { "a,b(c)"          , "a,bc(c)"            },
          new [] { "a,b(c,d)"        , "a,bcd(c,d)"         },
          new [] { "a,b,c"           , "a,b,c"              },
        };
    }

    public static string GetTestDisplayName(MethodInfo methodInfo, object[] data)
    {
      return
        data[0].ToString() == ""
        ? "<empty-string>"
        : data[0].ToString();
    }

    [TestMethod]
    [DynamicData(nameof(GetTestData), DynamicDataSourceType.Method, DynamicDataDisplayName = nameof(GetTestDisplayName))]
    public void EnumerableToTreeTest_BreadthFirst(
      string treeString,
      string expectedTreeString)
    {
      EnumerableToTreeTest(treeString, expectedTreeString, TreeTraversalStrategy.BreadthFirst);
    }

    [TestMethod]
    [DynamicData(nameof(GetTestData), DynamicDataSourceType.Method, DynamicDataDisplayName = nameof(GetTestDisplayName))]
    public void EnumerableToTreeTest_DepthFirst(
      string treeString,
      string expectedTreeString)
    {
      EnumerableToTreeTest(treeString, expectedTreeString, TreeTraversalStrategy.DepthFirst);
    }

    public void EnumerableToTreeTest(
      string treeString,
      string expectedTreeString,
      TreeTraversalStrategy treeTraversalStrategy)
    {
      // Arrange
      var sut = TreeSerializer.Deserialize(treeString);

      var expected =
        TreeSerializer
        .Deserialize(expectedTreeString)
        .GetTraversal(treeTraversalStrategy)
        .ToArray();

      Debug.WriteLine("-----Expected Values-----");
      foreach (var value in expected)
        Debug.WriteLine(value);

      // Act
      Debug.WriteLine($"{Environment.NewLine}-----Actual Values-----");
      var actual =
        sut
        .LeaffixScan(
          nodeContext => nodeContext.Node,
          (nodeContext, children) => $"{nodeContext.Node}{string.Join("", children)}")
        .GetTraversal(treeTraversalStrategy)
        .Do(visit => Debug.WriteLine(visit))
        .ToArray();

      var diff = NodeVisitDiffer.Diff(expected, actual);

      Debug.WriteLine($"{Environment.NewLine}-----Diffed Values-----");
      foreach (var diffResult in diff)
        Debug.WriteLine(diffResult);

      // Assert
      CollectionAssert.AreEqual(expected, actual);
    }
  }
}
