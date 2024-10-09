using Arborist.Benchmarks;
using BenchmarkDotNet.Running;

BenchmarkRunner.Run<TraversalBenchmarks>();

//var benchmark = new TreenumerableBenchmarks().DepthFirstTraversalDepthBig();
//var count = new TreenumerableBenchmarks().BreadthFirstTraversalDepthBig();
//Console.WriteLine($"Count: {count}");
//Console.WriteLine($"Count: {new TraversalBenchmarks().PruneBeforeBFT()}");
//Console.WriteLine($"Count: {new TraversalBenchmarks().PruneBeforeDFT()}");

