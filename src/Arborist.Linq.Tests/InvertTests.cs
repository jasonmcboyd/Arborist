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
  public class InvertTests
  {
    public static IEnumerable<object[]> GetTestData()
    {
      return new[]
        {
          new [] { ""                , ""                 },
          new [] { "a"               , "a"                },
          new [] { "a(b(c,d))"       , "a(b(d,c))"        },
          new [] { "a(b(d),c(e))"    , "a(c(e),b(d))"     },
          new [] { "a(b(d),c)"       , "a(c,b(d))"        },
          new [] { "a(b(d,e),c(f,g))", "a(c(g,f),b(e,d))" },
          new [] { "a(b)"            , "a(b)"             },
          new [] { "a(b,c)"          , "a(c,b)"           },
          new [] { "a(c),b"          , "b,a(c)"           },
          new [] { "a(c),b(d)"       , "b(d),a(c)"        },
          new [] { "a(c,d),b(e,f)"   , "b(f,e),a(d,c)"    },
          new [] { "a,b(c)"          , "b(c),a"           },
          new [] { "a,b(c,d)"        , "b(d,c),a"         },
          new [] { "a,b,c"           , "c,b,a"            },
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
        .Invert()
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
