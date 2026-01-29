# Quick reference for running benchmarks

Write-Host -ForegroundColor Green @"

Benchmark Quick Commands
========================

Run specific class:
  dotnet run -c Release -- --filter DepthFirstTreenumerator
  dotnet run -c Release -- --filter BreadthFirstWhere

Run by category:
  dotnet run -c Release -- --filter '*' --category Traversal
  dotnet run -c Release -- --filter '*' --category LINQ
  dotnet run -c Release -- --filter '*' --category DepthFirst

List benchmarks:
  dotnet run -c Release -- --list flat
  dotnet run -c Release -- --list tree

Interactive mode (easiest):
  dotnet run -c Release

Run all benchmarks (like CI):
  dotnet run -c Release -- --filter '*'

"@

Write-Host -ForegroundColor Cyan @"
Available categories:
  Traversal, DepthFirst, BreadthFirst, LevelOrder, PostOrder, PreOrder
  LINQ, Query, Filter, Projection, Pruning, Skip
  Conversion, DataStructures

"@

Write-Host "See BENCHMARK_CATEGORIES.md for full documentation."
