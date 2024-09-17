// See https://aka.ms/new-console-template for more information
using Arborist.Benchmarks;
using Arborist.Core;
using Arborist.Linq;
using Arborist.Trees;
using BenchmarkDotNet.Running;

Console.WriteLine("Hello, World!");

//var summary = BenchmarkRunner.Run<CountLeavesBenchmark>();

ITreenumerable<ulong> tree = new CompleteBinaryTree();

int count = tree.PruneAfter(x => x.Position.Depth == 22).GetLeaves().Count();

Console.WriteLine($"Count: {count}");

//Console.WriteLine(Enumerable.Range(2, 2 << 21).Select(x => new CollatzTreeNode((ulong)x)).Count());
//Console.WriteLine(Enumerable.Range(2, 2 << 21).Select(x => new CompleteBinaryTreeNode((ulong)x)).Count());
