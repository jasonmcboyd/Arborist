using Arborist.Benchmarks;
using BenchmarkDotNet.Running;

BenchmarkRunner.Run<TraversalBenchmarks>();

//var benchmark = new TreenumerableBenchmarks().DepthFirstTraversalDepthBig();
//var count = new TreenumerableBenchmarks().BreadthFirstTraversalDepthBig();
//Console.WriteLine($"Count: {count}");

