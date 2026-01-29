# Benchmark Categories Reference

This file documents the category structure for organizing benchmarks.

## How It Works

Use `[BenchmarkCategory]` attributes on benchmark classes to enable filtering. You can specify multiple categories per class.

```csharp
[MemoryDiagnoser]
[BenchmarkCategory("Traversal", "DepthFirst")]
public class DepthFirstTreenumerator { }
```

## Suggested Categories

Apply these categories to your benchmark classes for better organization:

### By Feature Area

| Benchmark Class             | Suggested Categories                  |
|-----------------------------|---------------------------------------|
| DepthFirstTreenumerator     | "Traversal", "DepthFirst"             |
| BreadthFirstTreenumerator   | "Traversal", "BreadthFirst"           |
| LevelOrderTraversal         | "Traversal", "LevelOrder"             |
| PostOrderTraversal          | "Traversal", "PostOrder"              |
| PreOrderTraversal           | "Traversal", "PreOrder"               |
| AllNodes                    | "LINQ", "Query"                       |
| AnyNodes                    | "LINQ", "Query"                       |
| CountNodes                  | "LINQ", "Query"                       |
| DepthFirstWhere             | "LINQ", "Filter", "DepthFirst"        |
| BreadthFirstWhere           | "LINQ", "Filter", "BreadthFirst"      |
| Select                      | "LINQ", "Projection"                  |
| GetLeaves                   | "LINQ", "Query"                       |
| PruneAfter                  | "LINQ", "Pruning"                     |
| PruneBefore                 | "LINQ", "Pruning"                     |
| SkipAllNodes                | "LINQ", "Skip"                        |
| EnumerableToTree            | "Conversion"                          |
| RefSemiDeque                | "DataStructures"                      |

## Usage Examples

### Local Development

Run only the benchmark you're working on:
```bash
# Run specific class
dotnet run -c Release -- --filter '*DepthFirstTreenumerator*'

# Run all traversal benchmarks
dotnet run -c Release -- --filter '*' --category Traversal

# Run all LINQ benchmarks
dotnet run -c Release -- --filter '*' --category LINQ

# Run depth-first related benchmarks
dotnet run -c Release -- --filter '*' --category DepthFirst

# Run multiple categories
dotnet run -c Release -- --filter '*' --category Traversal --category LINQ
```

### List Available Benchmarks

See what benchmarks are available without running them:
```bash
dotnet run -c Release -- --list flat
dotnet run -c Release -- --list tree
```

### Interactive Mode

Let BenchmarkDotNet prompt you to choose:
```bash
dotnet run -c Release
```

### CI Environment

CI runs ALL benchmarks automatically:
```bash
# GitHub Actions runs this (all benchmarks, accurate measurements)
dotnet run -c Release -- --filter '*'
```

## Adding Categories to Existing Benchmarks

Update each benchmark class to include appropriate categories:

```csharp
using BenchmarkDotNet.Attributes;

[MemoryDiagnoser]
[BenchmarkCategory("YourCategory", "OptionalSecondCategory")]
public class YourBenchmark
{
    [Benchmark]
    public void YourMethod() { }
}
```

## Performance Notes

- **Local (ShortRun)**: Fast iterations, less accurate (~15-30 seconds per benchmark)
- **CI (Default)**: Accurate measurements, longer runs (~1-2 minutes per benchmark)

The environment is auto-detected via `GITHUB_ACTIONS` or `CI` environment variables.
