# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with this repository.

## Project Overview

### Summary

**Arborist** is a high-performance .NET tree traversal and manipulation library providing efficient, memory-conscious algorithms for:
- Tree traversal (Depth-First and Breadth-First)
- LINQ-style operations on trees (Select, Where, SelectMany, Union, etc.)
- Tree construction, filtering, pruning, and aggregation

### Core Functionality

#### Tree Traversal Engine:

- Two traversal strategies: depth-first and breadth-first
- Dynamic tree pruning during traversal using NodeTraversalStrategies flags
- Two-phase node visits (scheduling vs. visiting) for flexible processing
- Generic design adapts to any tree structure via custom child enumerators

#### LINQ-Style Operations (45+ methods):

- Query: Where(), Select(), SelectMany(), CountNodes(), GetLeaves(), GetLevels()
- Aggregation: RootfixAggregate(), LeaffixAggregate(), cumulative scans
- Set operations: Union(), Intersection(), Subtract()
- Transformation: InvertTree(), Materialize(), pretty printing

#### Performance Optimizations:

- Custom RefSemiDeque<T> data structure with ref semantics for zero-copy state management
- Lazy evaluation - operations compose without materializing intermediate trees
- Struct-based nodes (SimpleNode<T>) to minimize allocations

## Build & Test Commands

```bash
# Build the solution
dotnet build src/Arborist.sln

# Run all tests
dotnet test src/Arborist.sln

# Run specific test project
dotnet test src/Arborist.Tests
dotnet test src/Arborist.Linq.Tests

# Run benchmarks
dotnet run --project src/Arborist.Benchmarks
```

## Architecture

### Design Philosophy

`ITreenumerable<T>` is a **tree monad**, analogous to how `IEnumerable<T>` is a list monad. This means it should obey the monadic laws (left identity, right identity, associativity). Since these laws cannot be strictly enforced by the type system alone, care must be taken when implementing new operations to preserve monadic behavior.

The library **never performs node equality comparisons**. This is a deliberate design choice that frees consumers from implementing `IEquatable<T>`, `IEqualityComparer<T>`, or overriding `Equals`/`GetHashCode` on their node types. Any type can be used as a node value without additional boilerplate.

### Project Structure

- **Arborist.Core** - Core interfaces (`ITreenumerable<T>`, `ITreenumerator<T>`) and enums
- **Arborist** - Base traversal implementations (DFS/BFS treenumerators)
- **Arborist.Linq** - LINQ-style tree operations and extensions
- **Arborist.Linq.Experimental** - Experimental features (in progress)
- **Arborist.Trees** - Sample tree implementations (Collatz, Triangle, etc.)
- **Arborist.SimpleSerializer** - Tree serialization for testing/debugging
- **Arborist.TestUtils** - Test utilities and helpers

### Key Abstractions

- **ITreenumerable<T>** - Factory interface for creating treenumerators (analogous to `IEnumerable<T>`)
- **ITreenumerator<T>** - Stateful traversal engine (analogous to `IEnumerator<T>`)
- **NodeTraversalStrategies** - Flags enum controlling traversal: `SkipNode`, `SkipDescendants`, `SkipSiblings`
- **TreenumeratorMode** - `SchedulingNode` (pre-order) vs `VisitingNode` (post-order)
- **NodePosition** - Tracks (sibling index, depth) in tree
- **NodeContext<T>** - Bundles node value with its position
- **NodeVisit<T>** - Complete visit information including mode and visit count

### Traversal Implementations

- `DepthFirstTreenumerator<TValue, TNode, TChildEnumerator>` - Stack-based DFS
- `BreadthFirstTreenumerator<TValue, TNode, TChildEnumerator>` - Queue+stack-based BFS
- `RefSemiDeque<T>` - Custom high-performance dual-ended queue for O(1) operations

### DFT vs BFT Scheduling and Visiting Behavior

Understanding the difference between DFT and BFT scheduling/visiting is critical when implementing treenumerator wrappers (like `Where`, `Select`, etc.):

**Key Invariant:** DFT and BFT produce the exact same scheduling and visiting nodes, just in a different order. Both strategies visit a parent node between scheduling each of its children.

**Depth-First Traversal (DFT):**
- Schedules a child, **visits it completely** (including all its descendants), then returns to the parent before scheduling the next sibling
- Children are visited immediately after being scheduled
- Stack-based: uses a single stack to track the current path from root to the current node

**Breadth-First Traversal (BFT):**
- Schedules all children at the current level, with parent visits interleaved between each schedule
- Children are NOT visited until ALL nodes at the current level have been scheduled
- Queue+stack-based: stack holds nodes being scheduled, queue holds nodes awaiting visits

**Example Tree:**
```
    a
   / \
  b   c
```

**DFT MoveNext() sequence:**
```
S a (schedule a)
V a (visit a, about to enumerate children)
S b (schedule b)
V b (visit b, no children)
V a (visit a again, between children)
S c (schedule c)
V c (visit c, no children)
V a (visit a, done with children)
```

**BFT MoveNext() sequence:**
```
S a (schedule a)
V a (visit a, about to enumerate children)
S b (schedule b)
V a (visit a, between children)
S c (schedule c)
V a (visit a, done scheduling children)
V b (visit b)
V c (visit c)
```

Note that both produce: S a, S b, S c, V a (×3), V b (×1), V c (×1) — just in different orders.

**Why This Matters for Wrapper Implementations:**

When implementing operators that filter or transform nodes (like `Where`), the wrapper must account for these different patterns. For example, when a node is filtered and its children are "promoted" to become children of the grandparent:

- In DFT: The child is visited immediately after scheduling, so the wrapper naturally sees schedule→visit pairs
- In BFT: All promoted children are scheduled (with inner parent visits between them) before any are visited. The wrapper must track which parent visits were already emitted to avoid duplicates (see `WhereBreadthFirstTreenumerator._PendingParentVisit` and `_ExtraParentVisitsEmitted`)

## Code Conventions

### Naming (from .editorconfig)

- **Interfaces**: Prefix with `I` (e.g., `ITreenumerable<T>`)
- **Types**: PascalCase (e.g., `DepthFirstTreenumerator`)
- **Private fields**: Leading underscore + PascalCase (e.g., `_RootsEnumerator`)
- **Parameters**: camelCase (e.g., `nodeTraversalStrategies`)
- **Public members**: PascalCase (e.g., `Position`, `VisitCount`)

### Patterns

- Heavy use of `ref` for performance optimization
- All treenumerators implement `IDisposable`
- Generic type parameters follow pattern: `TValue` (node value), `TNode` (node type), `TChildEnumerator` (child enumeration)

## Testing

- **Framework**: MSTest with `[DynamicData]` for data-driven tests
- **Test data**: `TreeTraversalTestData.cs` contains comprehensive test trees
- **Utilities**: `NodeVisitDiffer` for comparing expected vs actual results
- **Serialization**: Text-based tree format for test fixtures

## Target Frameworks

Multi-targets .NET Framework 4.8 and .NET 8.0.
