﻿using Arborist.Core;
using Arborist.Linq;
using Arborist.Nodes;
using Arborist.SimpleSerializer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Arborist.TestUtils
{
  public static class TestMethods
  {
    public static void TraversalTest(
      string serializedTree,
      Func<ITreenumerable<string>, ITreenumerable<string>> operation,
      Func<NodeVisit<string>, TraversalStrategy> traversalStrategySelector,
      NodeVisit<string>[] expectedTraversal,
      bool isDepthFirstTest,
      bool enumerableTreenumeratorTest)
    {
      // Arrange
      ITreenumerable<string> treenumerable;

      if (enumerableTreenumeratorTest)
      {
        var rootNodes = EnumerableTreeNode.Create(TreeSerializer.DeserializeRoots(serializedTree));
        treenumerable = rootNodes.ToTreenumerable().Select(visit => visit.Node);
      }
      else
      {
        IEnumerable<INodeContainerWithIndexableChildren<string>> roots = TreeSerializer.DeserializeRoots(serializedTree);
        treenumerable = roots.ToTreenumerable().Select(visit => visit.Node);
      }

      Debug.WriteLine("--------- Tree ---------");
      Debug.WriteLine(serializedTree);

      Debug.WriteLine($"{Environment.NewLine}---- Expected Values ----");
      NodeVisitsDebugWriter.WriteNodeVisitHeader();
      foreach (var nodeVisit in expectedTraversal)
        Debug.WriteLine(nodeVisit);

      if (operation != null)
        treenumerable = operation(treenumerable);

      var traversal =
        isDepthFirstTest
        ? treenumerable.GetDepthFirstTraversal(traversalStrategySelector)
        : treenumerable.GetBreadthFirstTraversal(traversalStrategySelector);

      // Act
      Debug.WriteLine($"{Environment.NewLine}----- Actual Values -----");
      NodeVisitsDebugWriter.WriteNodeVisitHeader();
      var actualTraversal =
        traversal
        .Do(visit => Debug.WriteLine(visit))
        .ToArray();

      var diff = NodeVisitDiffer.Diff(expectedTraversal, actualTraversal);

      Debug.WriteLine($"{Environment.NewLine}---- Diffed Values ----");
      foreach (var diffResult in diff)
        Debug.WriteLine(diffResult);

      // Assert
      CollectionAssert.AreEqual(expectedTraversal, actualTraversal);
    }
  }
}
