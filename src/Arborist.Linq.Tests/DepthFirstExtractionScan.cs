using Arborist.Core;
using Arborist.Linq;
using Arborist.SimpleSerializer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Arborist.Linq.Tests
{
  // SPIKE validation (spike-dft-extraction): exhaustively compares the clean-room DFT extraction
  // spike (WhereSpike) against the TRUSTED production .Where() over DEPTH-first traversal, for
  // EVERY subset of nodes-to-extract, across a corpus of stress trees. Production Where uses a
  // completely different algorithm (collapse + reconcile), so spike == production across the whole
  // predicate power set is strong evidence the spike is correct. Predicate-only (TraverseAll).
  [TestClass]
  public class DepthFirstExtractionScan
  {
    private static readonly string[] Trees =
    {
      "a(b(c))",
      "a(b,c)",
      "a,b(c)",
      "a,b,c",
      "a(b,c,d)",
      "a,b(c,d)",
      "a,b(d),c",
      "a(b(d(e)),c)",
      "a(b(c,d,e))",
      "a(d),b,c(e)",
      "a(c,d),b(e,f)",
      "a(d),b(e),c(f)",
      "a(b(d,e,f),c)",
      "a,b(d),c(e(f))",
      "a(b(e),c(f),d(g))",
      "a(b(d,e),c(f(g)))",
      "a(d(f,g,h)),b,c(e)",
      "a(b(d,e,f),c(g,h,i))",
      "a(d(g)),b(e(h)),c(f(i))",
    };

    [TestMethod]
    public void DepthFirstSpikeMatchesProductionWhere()
    {
      long total = 0;
      long failed = 0;
      var failures = new List<string>();

      foreach (var treeString in Trees)
      {
        var tree = TreeSerializer.Deserialize(treeString);
        var nodes = tree.GetDepthFirstTraversal()
          .Where(v => v.Mode == TreenumeratorMode.SchedulingNode)
          .Select(v => v.Node)
          .Distinct()
          .ToArray();

        // Exhaustive: every subset of nodes-to-extract (power set).
        for (int mask = 0; mask < (1 << nodes.Length); mask++)
        {
          var extract = new HashSet<string>();
          for (int i = 0; i < nodes.Length; i++)
            if ((mask & (1 << i)) != 0)
              extract.Add(nodes[i]);

          bool Keep(NodeContext<string> nc) => !extract.Contains(nc.Node);

          var expected = Key(tree.Where(Keep).GetDepthFirstTraversal(All)).ToList();
          var actual = Key(tree.WhereSpike(Keep).GetDepthFirstTraversal(All)).Take(100_000).ToList();

          total++;
          if (!expected.SequenceEqual(actual))
          {
            failed++;
            if (failures.Count < 25)
              failures.Add($"{treeString}  extract={{{string.Join(",", extract.OrderBy(x => x))}}}"
                + $"{Environment.NewLine}    expected: {Render(expected)}"
                + $"{Environment.NewLine}    actual:   {Render(actual)}");
          }
        }
      }

      // Coverage floor: the power set over the 19-tree corpus is 1,840 cases. Guard against a
      // silently-empty corpus making the comparison vacuous.
      Assert.IsTrue(total >= 1840, $"corpus too small: only {total} cases ran (expected >= 1840)");

      Assert.AreEqual(
        0L,
        failed,
        $"DFT spike diverged from production Where on {failed} of {total} cases:{Environment.NewLine}"
        + string.Join(Environment.NewLine, failures));
    }

    private static NodeTraversalStrategies All(NodeContext<string> _) => NodeTraversalStrategies.TraverseAll;

    private static IEnumerable<(TreenumeratorMode, int, int, int, string)> Key(IEnumerable<NodeVisit<string>> visits) =>
      visits.Select(v => (v.Mode, v.Position.Depth, v.Position.SiblingIndex, v.VisitCount, v.Node));

    private static string Render(IEnumerable<(TreenumeratorMode Mode, int Depth, int Sib, int Vc, string Node)> visits) =>
      string.Join(" ", visits.Select(v => $"{(v.Mode == TreenumeratorMode.SchedulingNode ? "S" : "V")}{v.Node}({v.Sib},{v.Depth}){v.Vc}"));
  }
}
