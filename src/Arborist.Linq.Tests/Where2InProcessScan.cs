using Arborist.Core;
using Arborist.Linq;
using Arborist.SimpleSerializer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Arborist.Linq.Tests
{
  // Fast in-process equivalent of Where2Test_BreadthFirst for the currently-enabled tree(s):
  // loops Where2Tests.GetTestData() and runs the identical Where-wrapper-vs-materialized-oracle
  // BFT comparison per case, but in ONE test loop (no MSTest per-case overhead) and with
  // deserialization caching (the serializer is the slow part). Big trees (9-node i-group are
  // ~185k cases) run in seconds here vs minutes through DynamicData. Same coverage.
  [TestClass]
  public class Where2InProcessScan
  {
    [TestMethod]
    public void BreadthFirstMatchesOracle()
    {
      var deserializedByString = new Dictionary<string, ITreenumerable<string>>();
      ITreenumerable<string> Deserialize(string treeString)
      {
        if (!deserializedByString.TryGetValue(treeString, out var treenumerable))
        {
          treenumerable = TreeSerializer.Deserialize(treeString);
          deserializedByString[treeString] = treenumerable;
        }
        return treenumerable;
      }

      long total = 0;
      long failed = 0;
      var failures = new List<string>();

      foreach (var data in Where2Tests.GetTestData())
      {
        total++;

        var treeString = (string)data[0];
        var expectedTreeString = (string)data[1];
        var skippedNodes = (string[])data[2];
        var composeOperations = (bool)data[3];
        var pairs = (Where2Tests.NodeAndTraversalStrategy[])data[4];

        NodeTraversalStrategies Selector(NodeContext<string> nodeContext)
        {
          foreach (var pair in pairs)
            if (pair.Node == nodeContext.Node)
              return pair.NodeTraversalStrategy;
          return NodeTraversalStrategies.TraverseAll;
        }

        ITreenumerable<string> sut;
        if (composeOperations)
        {
          sut = Deserialize(treeString);
          foreach (var node in skippedNodes)
          {
            var skipped = node;
            sut = sut.Where(nodeContext => nodeContext.Node != skipped);
          }
        }
        else
        {
          sut = Deserialize(treeString).Where(nodeContext => !skippedNodes.Contains(nodeContext.Node));
        }

        var expected = Key(Deserialize(expectedTreeString).GetTraversal(TreeTraversalStrategy.BreadthFirst, Selector));
        // Take() bounds a hypothetical non-terminating wrapper regression into a length mismatch.
        var actual = Key(sut.GetTraversal(TreeTraversalStrategy.BreadthFirst, Selector)).Take(100_000);

        if (!expected.SequenceEqual(actual))
        {
          failed++;
          if (failures.Count < 40)
            failures.Add(Where2Tests.GetTestDisplayName(null, data));
        }
      }

      Assert.AreEqual(
        0L,
        failed,
        $"BFT Where wrapper diverged from the oracle on {failed} of {total} cases:{Environment.NewLine}"
        + string.Join(Environment.NewLine, failures));
    }

    private static IEnumerable<(TreenumeratorMode, int, int, int, string)> Key(IEnumerable<NodeVisit<string>> visits) =>
      visits.Select(visit => (visit.Mode, visit.Position.Depth, visit.Position.SiblingIndex, visit.VisitCount, visit.Node));
  }
}
