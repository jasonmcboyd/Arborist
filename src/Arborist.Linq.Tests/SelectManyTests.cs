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
  public class SelectManyTests
  {
    public static IEnumerable<string[]> GetTestTrees()
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

    public static IEnumerable<object[]> GetTestData()
    {
      foreach (var testTrees in GetTestTrees())
      {
        yield return new object[] { testTrees[0], testTrees[1], testTrees[2], null, null };

        var expectedTree = TreeSerializer.Deserialize(testTrees[2]);

        foreach (var node in expectedTree.PreOrderTraversal())
          foreach (var nodeTraversalStrategies in _SkipNodeTraversalStrategies)
            yield return new object[] { testTrees[0], testTrees[1], testTrees[2], node, nodeTraversalStrategies };
      }
    }

    private static NodeTraversalStrategies[] _SkipNodeTraversalStrategies = new[]
    {
      NodeTraversalStrategies.SkipNode,
      NodeTraversalStrategies.SkipNodeAndDescendants,
      NodeTraversalStrategies.SkipDescendants,
    };

    public static string GetTestDisplayName(MethodInfo methodInfo, object[] data)
    {
      var result = $"{data[0]} | {data[1]}";

      if (data[3] != null)
      {
        result += $": {data[4]} {data[3]}";
      }

      return result;
    }

    [TestMethod]
    [DynamicData(nameof(GetTestData), DynamicDataSourceType.Method, DynamicDataDisplayName = nameof(GetTestDisplayName))]
    public void SelectMany_BreadthFirst(
      string treeString,
      string innerTreeString,
      string expectedResults,
      string nodeToSkip,
      NodeTraversalStrategies? nodeTraversalStrategies)
    {
      SelectManyTest(treeString, innerTreeString, expectedResults, TreeTraversalStrategy.BreadthFirst, nodeToSkip, nodeTraversalStrategies);
    }

    [TestMethod]
    [DynamicData(nameof(GetTestData), DynamicDataSourceType.Method, DynamicDataDisplayName = nameof(GetTestDisplayName))]
    public void SelectMany_DepthFirst(
      string treeString,
      string innerTreeString,
      string expectedResults,
      string nodeToSkip,
      NodeTraversalStrategies? nodeTraversalStrategies)
    {
      SelectManyTest(treeString, innerTreeString, expectedResults, TreeTraversalStrategy.DepthFirst, nodeToSkip, nodeTraversalStrategies);
    }

    private void SelectManyTest(
      string treeString,
      string innerTreeString,
      string expectedResults,
      TreeTraversalStrategy treeTraversalStrategy,
      string nodeToSkip,
      NodeTraversalStrategies? nodeTraversalStrategies)
    {
      // Arrange
      var treenumerable = TreeSerializer.Deserialize(treeString);
      var innerTreenumerable = TreeSerializer.Deserialize(innerTreeString);

      var sut =
        treenumerable
        .SelectMany(x => innerTreenumerable.Select(y => x + y.Node));

      var nodeVisitStrategySelector =
        new Func<NodeContext<string>, NodeTraversalStrategies>(
          nodeVisit =>
            nodeToSkip == null || nodeToSkip != nodeVisit.Node
            ? NodeTraversalStrategies.TraverseAll
            : nodeTraversalStrategies.Value);

      var expected =
        TreeSerializer
        .Deserialize(expectedResults)
        .GetTraversal(treeTraversalStrategy, nodeVisitStrategySelector)
        .ToArray();

      Debug.WriteLine("-----Expected Values-----");
      foreach (var value in expected)
        Debug.WriteLine(value);

      // Act
      Debug.WriteLine($"{Environment.NewLine}-----Actual Values-----");
      var actual =
        sut
        .GetTraversal(treeTraversalStrategy, nodeVisitStrategySelector)
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
