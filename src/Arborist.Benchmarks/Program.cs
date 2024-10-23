using Arborist.Benchmarks;
using Arborist.Benchmarks.Trees;
using Arborist.Linq;
using Arborist.Trees;
using BenchmarkDotNet.Running;
using System;

BenchmarkRunner.Run<TraversalBenchmarks>();

//Console.WriteLine(
//Treenumerables
//.GetTree(19, TreeShape.Deep)
//.CountNodes());

//Console.WriteLine(
//Treenumerables
//.GetTree(19, TreeShape.Wide)
//.CountNodes());
