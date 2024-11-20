using Arborist;
using Arborist.Benchmarks;
using Arborist.Benchmarks.Trees;
using Arborist.Core;
using Arborist.Linq;
using Arborist.Trees;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Running;
using System;
using System.Linq;

var config =
  ManualConfig
  .Create(DefaultConfig.Instance)
  .WithOptions(ConfigOptions.JoinSummary)
  .WithOptions(ConfigOptions.DisableLogFile);

BenchmarkRunner.Run([
  //BenchmarkConverter.TypeToBenchmarks(typeof(TraversalBenchmarks), config),
  BenchmarkConverter.TypeToBenchmarks(typeof(DepthFirstTreenumerator), config),
  //BenchmarkConverter.TypeToBenchmarks(typeof(DepthFirstWhere), config),
]);

//var count =
//  new TriangleTree()
//  //.Where(nodeContext => (nodeContext.Position.Depth + nodeContext.Position.SiblingIndex) % 2 == 0)
//  .GetDepthFirstTraversal(
//    nc => nc.Position.Depth == 1
//    ? NodeTraversalStrategies.SkipDescendants
//    : NodeTraversalStrategies.TraverseAll)
//  .Count();

//Console.WriteLine(count);
