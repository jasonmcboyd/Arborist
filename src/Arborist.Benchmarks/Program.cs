using Arborist;
using Arborist.Benchmarks;
using Arborist.Benchmarks.Trees;
using Arborist.Core;
using Arborist.Linq;
using Arborist.Trees;
using BenchmarkDotNet.Running;
using System;
using System.Linq;

BenchmarkRunner.Run<TraversalBenchmarks>();

//var source = new CompleteBinaryTree();

//Func<NodeContext<int>, bool> predicate = _ => true;

//var result = 0;

//using (var treenumerator = source.GetDepthFirstTreenumerator())
//{
//  var strat = NodeTraversalStrategies.SkipNode;

//  while (treenumerator.MoveNext(strat))
//  {
//    if (predicate(new NodeContext<int>(treenumerator.Node, treenumerator.Position)))
//      result++;

//    strat = treenumerator.Position.Depth == 19 ? NodeTraversalStrategies.SkipDescendants : NodeTraversalStrategies.SkipNode;
//  }
//}

//Console.WriteLine($"Result: {result:#,#}");


//var count =
//  new CompleteBinaryTree()
//  .PruneBefore(nodeContext => nodeContext.Position.Depth == 22)
//  .LevelOrderTraversal()
//  .Count();

//Console.WriteLine($"Count: {count:#,#}");

//Console.WriteLine(
//Treenumerables
//.GetTree(19, TreeShape.Deep)
//.CountNodes());

//Console.WriteLine(
//Treenumerables
//.GetTree(19, TreeShape.Wide)
//.CountNodes());
