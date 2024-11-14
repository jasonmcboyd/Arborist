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

//var count =
//  Enumerable.Range(0, 1 << 22)
//  .ToTree(_ => false)
//  .CountNodes();

//Console.WriteLine(count);

//var count =
//  Enumerable.Range(0, 1 << 20)
//  .ToTree(_ => true)
//  .CountNodes();

//Console.WriteLine(count.ToString());

//count = Enumerable.Range(0, 1 << 20).ToDegenerateTree().CountNodes();

//Console.WriteLine(count.ToString());
