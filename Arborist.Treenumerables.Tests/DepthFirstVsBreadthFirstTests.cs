using Arborist.Tests.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Arborist.Treenumerables.Tests
{
  [TestClass]
  public class DepthFirstVsBreadthFirstTests
  {
    public class TestNode : INodeWithIndexableChildren<TestNode, char>
    {
      public TestNode this[int index] => Children[index];

      public int ChildCount => Children.Count;

      public readonly List<TestNode> Children = new List<TestNode>();

      public char Value { get; set; }
    }

    public IEnumerable<TestNode> ParseTreeString(string tree)
    {
      var parensCount = 0;

      var rootNode = new TestNode();

      var stack = new Stack<TestNode>();

      stack.Push(rootNode);

      foreach (var c in tree)
      {
        if (char.IsLetter(c))
        {
          var node = new TestNode { Value = c };

          stack.Peek().Children.Add(node);

          stack.Push(node);
        }
        else if (c == '(')
        {
          parensCount++;
        }
        else if (c == ')')
        {
          parensCount--;

          stack.Pop();
        }
        else if (c == ',')
        {
          stack.Pop();
        }
      }

      return rootNode.Children;
    }

    public static IEnumerable<object[]> GetData()
    {
      var treeStrings = new[]
      {
        "a,b,c",
        "a(b,c)",
        "a(b,(c))",
        "a(b(c,d))",
        "a(b,c),d(e,f)"
      };

      var filterStrategies = new[]
      {
        SchedulingStrategy.SkipNode,
        SchedulingStrategy.SkipSubtree
      };

      foreach (var treeString in treeStrings)
      {
        yield return new[] { treeString, null, null };

        foreach (var c in treeString.Where(char.IsLetter))
          foreach (var strategy in filterStrategies)
            yield return new object[] { treeString, strategy, c }; 
      }
    }

    [TestMethod]
    [DynamicData(nameof(GetData), DynamicDataSourceType.Method)]
    public void Test(string treeString, SchedulingStrategy? filterStrategy, char? filterCharacter)
    {
      var roots = ParseTreeString(treeString).ToArray();

      var treenumerable = new IndexableTreenumerable<TestNode, char>(roots);

      MoveNextResult<char>[] Sort(IEnumerable<MoveNextResult<char>> nodes) =>
        nodes
        .OrderBy(x => (x.State, x.Depth, x.SiblingIndex, x.Node))
        .ToArray();

      var visitStrategy =
        new Func<NodeVisit<char>, SchedulingStrategy>(
          visit =>
            filterCharacter == null || filterCharacter.Value != visit.Node
            ? SchedulingStrategy.ScheduleForTraversal
            : filterStrategy.Value);

      Debug.WriteLine("-----Breadth First-----");
      var breadthFirst =
        treenumerable
        .ToBreadthFirstMoveNext(visitStrategy)
        .Do(x => Debug.WriteLine(x))
        .ToArray();

      Debug.WriteLine("\r\n-----Depth First------");
      var depthFirst =
        treenumerable
        .ToDepthFirstMoveNext(visitStrategy)
        .Do(x => Debug.WriteLine(x))
        .ToArray();

      CollectionAssert.AreEqual(Sort(breadthFirst), Sort(depthFirst));
    }
  }
}
