# Migration Guide: Updating Benchmark Classes

This guide shows you how to update your remaining benchmark classes to use the new flexible system.

## What Changed

**Before:**
- Manually commented/uncommented lines in Program.cs
- `[ShortRunJob]` on every benchmark class
- No easy way to run subsets of benchmarks

**After:**
- Automatic discovery - no Program.cs changes needed
- Job configuration controlled by environment (local vs CI)
- Category-based filtering for easy selective runs

## Quick Update Checklist

For each benchmark class file, make these two changes:

### 1. Remove `[ShortRunJob]`
### 2. Add `[BenchmarkCategory(...)]`

## Examples

### BreadthFirstTreenumerator.cs

**Change from:**
```csharp
[MemoryDiagnoser]
[ShortRunJob]
public class BreadthFirstTreenumerator
```

**Change to:**
```csharp
[MemoryDiagnoser]
[BenchmarkCategory("Traversal", "BreadthFirst")]
public class BreadthFirstTreenumerator
```

### CountNodes.cs

**Change from:**
```csharp
[MemoryDiagnoser]
[ShortRunJob]
public class CountNodes
```

**Change to:**
```csharp
[MemoryDiagnoser]
[BenchmarkCategory("LINQ", "Query")]
public class CountNodes
```

### DepthFirstWhere.cs

**Change from:**
```csharp
[MemoryDiagnoser]
[ShortRunJob]
public class DepthFirstWhere
```

**Change to:**
```csharp
[MemoryDiagnoser]
[BenchmarkCategory("LINQ", "Filter", "DepthFirst")]
public class DepthFirstWhere
```

### RefSemiDeque.cs

**Change from:**
```csharp
[MemoryDiagnoser]
[ShortRunJob]
public class RefSemiDeque
```

**Change to:**
```csharp
[MemoryDiagnoser]
[BenchmarkCategory("DataStructures")]
public class RefSemiDeque
```

## Complete Category Mapping

See [BENCHMARK_CATEGORIES.md](BENCHMARK_CATEGORIES.md) for the full list of suggested categories for all 17 benchmark classes.

## Files to Update

Run this to see which files need updating:

```bash
cd src/Arborist.Benchmarks
grep -l "ShortRunJob" Benchmarks/*.cs
```

You should update:
- ✅ DepthFirstTreenumerator.cs (already done)
- ✅ AllNodes.cs (already done)
- ⬜ BreadthFirstTreenumerator.cs
- ⬜ BreadthFirstWhere.cs
- ⬜ DepthFirstWhere.cs
- ⬜ CountNodes.cs
- ⬜ AnyNodes.cs
- ⬜ GetLeaves.cs
- ⬜ LevelOrderTraversal.cs
- ⬜ PostOrderTraversal.cs
- ⬜ PreOrderTraversal.cs
- ⬜ PruneAfter.cs
- ⬜ PruneBefore.cs
- ⬜ RefSemiDeque.cs
- ⬜ Select.cs
- ⬜ SkipAllNodes.cs
- ⬜ EnumerableToTree.cs

## Testing Your Changes

After updating, test that everything works:

```bash
# List all benchmarks (should show all 17 classes)
dotnet run -c Release -- --list flat

# Run a specific one you updated
dotnet run -c Release -- --filter '*BreadthFirstTreenumerator*'

# Run by category
dotnet run -c Release -- --filter '*' --category Traversal
```

## Optional: Bulk Update Script

If you want to update many files at once, you can use find/replace in your editor:

**Find:** `[ShortRunJob]`
**Replace:** (delete - just remove the line)

Then manually add appropriate `[BenchmarkCategory(...)]` attributes based on the table in BENCHMARK_CATEGORIES.md.
