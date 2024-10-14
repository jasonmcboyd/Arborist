using Arborist.Benchmarks;
using BenchmarkDotNet.Running;

BenchmarkRunner.Run<TraversalBenchmarks>();

//var benchmark = new TreenumerableBenchmarks().DepthFirstTraversalDepthBig();
//var count = new TreenumerableBenchmarks().BreadthFirstTraversalDepthBig();
//Console.WriteLine($"Count: {count}");
//Console.WriteLine($"Count: {new TraversalBenchmarks().PruneBeforeBFT()}");
//Console.WriteLine($"Count: {new TraversalBenchmarks().PruneBeforeDFT()}");

//var profiler = new Profiler();

//var dftCount = profiler.DepthFirstTraversalBig();
//var bftCount = profiler.BreadthFirstTraversalBig();

//Console.WriteLine($"Depth First Traversal Count: {dftCount}");
//Console.WriteLine($"Breadth First Traversal Count: {bftCount}");

