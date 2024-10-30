using Arborist.Benchmarks;
using Arborist.Benchmarks.Trees;
using Arborist.Linq;
using Arborist.Trees;
using BenchmarkDotNet.Running;
using System;
using System.Linq;

BenchmarkRunner.Run<TraversalBenchmarks>();

//var count =
//  new TriangleTree()
//  .PruneAfter(nodeContext => nodeContext.Position.Depth == (1 << 14))
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
