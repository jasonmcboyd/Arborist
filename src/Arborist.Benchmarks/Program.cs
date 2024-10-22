using Arborist.Benchmarks;
using Arborist.Benchmarks.Trees;
using Arborist.Linq;
using Arborist.Trees;
using BenchmarkDotNet.Running;

BenchmarkRunner.Run<TraversalBenchmarks>();

//var profiler = new Profiler();

//Console.WriteLine($"Count: {profiler.LevelOrderTraversal_DeepTree()}");

//var tree =
//  new DeepTree()
//  .TakeTrees(20)
//  .CountNodes();

//Console.WriteLine(tree);

//benchmark.LevelOrderTraversal_DeepTree();
//benchmark.LevelOrderTraversal_TriangleTree();
//benchmark.LevelOrderTraversal_WideTree();
//benchmark.PreOrderTraversal_DeepTree();
//benchmark.PreOrderTraversal_TriangleTree();
//benchmark.PreOrderTraversal_WideTree();


