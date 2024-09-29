using Arborist.Benchmarks;
using BenchmarkDotNet.Running;

//var summary = BenchmarkRunner.Run<TreenumerableBenchmarks>();

var benchmark = new TreenumerableBenchmarks().BreadthFirstTraversalDepth20();

