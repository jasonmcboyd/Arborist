using Arborist.Core;
using Arborist.SimpleSerializer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace Arborist.Linq.Tests
{
  [TestClass]
  public class MaterializeTests
  {
    private static readonly string[] Trees =
    {
      "a",
      "a(b(c))",
      "a(b,c)",
      "a,b,c",
      "a,b(c)",
      "a(b,c,d)",
      "a(b(d(e)),c)",
      "a(b(d,e,f),c(g,h,i))",
      "a(d(g)),b(e(h)),c(f(i))",
      "a,b(d),c(e(f))",
    };

    [TestMethod]
    public void Materialized_preserves_structure()
    {
      foreach (var tree in Trees)
      {
        var materialized = TreeSerializer.Deserialize(tree).Materialize();
        Assert.AreEqual(tree, materialized.Serialize(), $"structure mismatch for {tree}");
      }
    }

    [TestMethod]
    public void Materialized_matches_source_DepthFirst()
      => AssertSameTraversal(TreeTraversalStrategy.DepthFirst);

    [TestMethod]
    public void Materialized_matches_source_BreadthFirst()
      => AssertSameTraversal(TreeTraversalStrategy.BreadthFirst);

    private static void AssertSameTraversal(TreeTraversalStrategy strategy)
    {
      foreach (var tree in Trees)
      {
        var source = TreeSerializer.Deserialize(tree);
        var materialized = TreeSerializer.Deserialize(tree).Materialize();

        CollectionAssert.AreEqual(
          Collect(source, strategy),
          Collect(materialized, strategy),
          $"{strategy} traversal mismatch for {tree}");
      }
    }

    // Guards the IChildEnumerator contract that PreorderChildEnumerator must honor: the engine
    // signals SkipDescendants by Disposing the child enumerator, so a disposed enumerator must
    // yield no further children. (A no-op Dispose silently ignores all skip strategies.)
    [TestMethod]
    public void Materialized_honors_SkipDescendants()
    {
      foreach (var strategy in new[] { TreeTraversalStrategy.DepthFirst, TreeTraversalStrategy.BreadthFirst })
      {
        var tree = TreeSerializer.Deserialize("a(b,c)").Materialize();

        var scheduled =
          tree
          .GetTraversal(strategy, nc => nc.Node == "a" ? NodeTraversalStrategies.SkipDescendants : NodeTraversalStrategies.TraverseAll)
          .Where(visit => visit.Mode == TreenumeratorMode.SchedulingNode)
          .Select(visit => visit.Node)
          .ToList();

        CollectionAssert.AreEqual(new[] { "a" }, scheduled, $"SkipDescendants not honored ({strategy})");
      }
    }

    private static List<(TreenumeratorMode, int, int, int, string)> Collect(
      ITreenumerable<string> tree,
      TreeTraversalStrategy strategy)
    {
      var result = new List<(TreenumeratorMode, int, int, int, string)>();
      using (var t = tree.GetTreenumerator(strategy))
        while (t.MoveNext(NodeTraversalStrategies.TraverseAll))
          result.Add((t.Mode, t.Position.Depth, t.Position.SiblingIndex, t.VisitCount, t.Node));
      return result;
    }
  }
}
