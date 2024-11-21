using Arborist;
using Arborist.Benchmarks;
using Arborist.Benchmarks.Trees;
using Arborist.Core;
using Arborist.Linq;
using Arborist.Trees;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Order;
using BenchmarkDotNet.Running;
using System;
using System.Linq;

var config =
  ManualConfig
  .Create(DefaultConfig.Instance)
  .WithOptions(ConfigOptions.JoinSummary)
  .WithOptions(ConfigOptions.DisableLogFile)
  .WithOrderer(new DefaultOrderer(SummaryOrderPolicy.Declared));

BenchmarkRunner.Run([
  BenchmarkConverter.TypeToBenchmarks(typeof(AllNodes), config),
  BenchmarkConverter.TypeToBenchmarks(typeof(AnyNodes), config),
  BenchmarkConverter.TypeToBenchmarks(typeof(CountNodes), config),
  BenchmarkConverter.TypeToBenchmarks(typeof(DepthFirstTreenumerator), config),
  BenchmarkConverter.TypeToBenchmarks(typeof(DepthFirstWhere), config),
  BenchmarkConverter.TypeToBenchmarks(typeof(EnumerableToTree), config),
  BenchmarkConverter.TypeToBenchmarks(typeof(GetLeaves), config),
  BenchmarkConverter.TypeToBenchmarks(typeof(LevelOrderTraversal), config),
  BenchmarkConverter.TypeToBenchmarks(typeof(PostOrderTraversal), config),
  BenchmarkConverter.TypeToBenchmarks(typeof(PreorderTraversal), config),
  BenchmarkConverter.TypeToBenchmarks(typeof(PruneAfter), config),
  BenchmarkConverter.TypeToBenchmarks(typeof(PruneBefore), config),
  BenchmarkConverter.TypeToBenchmarks(typeof(RefSemiDeque), config),
  BenchmarkConverter.TypeToBenchmarks(typeof(Select), config),
  BenchmarkConverter.TypeToBenchmarks(typeof(SkipAllNodes), config),
]);

//var count =
//  new TriangleTree()
//  .GetDepthFirstTraversal(nc =>
//    nc.Position.Depth == 2896
//    ? NodeTraversalStrategies.SkipDescendants
//    : NodeTraversalStrategies.TraverseAll)
//  .Count();

//Console.WriteLine(count);

//var count1 =
//  new CompleteBinaryTree()
//    .GetDepthFirstTraversal(nc =>
//      nc.Position.Depth == 21
//      ? NodeTraversalStrategies.SkipDescendants
//      : NodeTraversalStrategies.TraverseAll)
//    .Count();

//Console.WriteLine(count1);
