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

### The Where Operation

The `Where` operation is one of the most complex operations in this library. Unlike `IEnumerable.Where()` which simply filters elements from a flat sequence, tree filtering must handle the structural implications of removing nodes from a hierarchy.

#### Core Concept: Child Promotion

When a node is filtered out, its children are **promoted** to become children of the filtered node's parent:

```
Original tree:     a              Filtered tree (remove b):    a
                  / \                                         /|\
                 b   c                                       d e c
                / \
               d   e
```

Node `b` is removed, but its children `d` and `e` become direct children of `a`. This is fundamentally different from `SkipDescendants` (which would remove `d` and `e` too).

#### Position Recalculation

Filtered nodes require recalculating positions for all remaining nodes:

1. **Depth compression**: Each filtered ancestor reduces the effective depth by 1
2. **Sibling index reassignment**: Remaining siblings get new sequential indices (0, 1, 2, ...)

```
Original:  a(b[0], c[1], d[2])     After filtering c:  a(b[0], d[1])
                                   (d's sibling index changes from 2 to 1)
```

#### Implementation Files

- **Extension method**: `Arborist.Linq/Treenumerable/Treenumerable.Where.cs`
- **DFT implementation**: `Arborist.Linq/Treenumerators/Filter/WhereDepthFirstTreenumerator.cs`
- **BFT implementation**: `Arborist.Linq/Treenumerators/Filter/WhereBreadthFirstTreenumerator.cs`

---

### WhereDepthFirstTreenumerator

DFT Where uses two parallel stacks to track the path from root to current node:

```csharp
private readonly RefSemiDeque<InternalNodeVisit> _NodeVisits;        // Accepted nodes
private readonly RefSemiDeque<InternalNodeVisit> _SkippedNodeVisits;  // Filtered ancestors
```

#### Internal State Structure

```csharp
private struct InternalNodeVisit
{
    public TNode Node;
    public NodePosition OriginalPosition;    // Position in inner (unfiltered) tree
    public NodePosition Position;            // Position in filtered tree
    public int VisitCount;
    public int CurrentChildIndex;            // How many children have been scheduled
}
```

#### Sibling Index Calculation

The parent's `CurrentChildIndex` tracks how many filtered children have been accepted. Each accepted child:
1. Gets the current `CurrentChildIndex` as its sibling index
2. Increments `CurrentChildIndex` for the next sibling

This ensures promoted children get correct sequential indices:

```
Tree: a(b(c, d), e)    Filter: remove b
When scheduling c: parent is a (b is on skipped stack), a.CurrentChildIndex = 0 → c gets index 0
When scheduling d: a.CurrentChildIndex = 1 → d gets index 1
When scheduling e: a.CurrentChildIndex = 2 → e gets index 2
Result: a(c[0], d[1], e[2])
```

#### Effective Depth Calculation

```csharp
effectiveDepth = _NodeVisits.Count + _SkippedNodeVisits.Count - 1
```

The depth in the filtered tree equals the total stack depth (accepted + filtered nodes) minus the sentinel.

#### The Sentinel Node

DFT initializes with a sentinel node (visit count = 1) that acts as a virtual root above all actual roots. This simplifies stack management—there's always a "parent" to reference.

#### Key Algorithm: OnScheduling

When a node is scheduled by the inner treenumerator:

1. **Pop deeper nodes**: If current depth ≤ previous depth, pop nodes from both stacks that are deeper
2. **Increment parent visit count**: When backtracking, the parent gets another visit
3. **Check predicate**: If node fails, push to `_SkippedNodeVisits` and return false (skip)
4. **Calculate position**: Use effective depth and parent's `CurrentChildIndex`
5. **Push to accepted stack**: Track this node for its descendants

#### Key Algorithm: OnVisiting

1. **Pop deeper nodes**: Remove nodes deeper than current visiting depth
2. **Skip if not accepted**: If node has no visit count, it was filtered during scheduling
3. **Increment visit count**: Track how many times this node has been visited

---

### WhereBreadthFirstTreenumerator

BFT Where is significantly more complex due to the deferred visiting pattern. It uses a queue for accepted nodes and a stack for filtered ancestors:

```csharp
private readonly RefSemiDeque<NodeTraversalStatus> _NodePositionAndVisitCounts;  // Queue
private readonly RefSemiDeque<NodeVisit<TNode>> _SkippedStack;                   // Stack
```

#### The Core Challenge: Deferred Parent Visits

In BFT, all children at a level are scheduled before any are visited. When a child is promoted (parent filtered), the wrapper must emit an extra parent visit for the grandparent—but the inner BFT may later emit its own parent visit for the filtered parent. This creates potential duplicate visits.

**Example showing the problem:**

```
Tree: a(b(c))    Filter: remove b

Inner BFT sequence:
  S a, V a, S b, V a, V b, S c, V b, V c

What we need to output:
  S a, V a, S c, V a, V c

The tricky part: When we schedule c (promoted to child of a), we must emit
"V a" BEFORE "V c". But the inner BFT will later emit "V b" which we skip,
followed by more visits we need to handle correctly.
```

#### The Pending Parent Visit Mechanism

BFT tracks extra parent visits with two fields:

```csharp
private bool _PendingParentVisit = false;        // Need to emit a parent visit next
private int _ExtraParentVisitsEmitted = 0;       // Count of extra visits emitted
```

**When scheduling a promoted child** (detected when there are filtered ancestors and current depth = deepest filtered ancestor's depth + 1):
- Set `_PendingParentVisit = true`

**On next MoveNext**:
- If `_PendingParentVisit` is true and conditions are met, emit parent visit first
- Increment `_ExtraParentVisitsEmitted`

**When visiting**:
- If encountering a parent visit that corresponds to a filtered node, check if we already emitted equivalent visits
- Use `_ExtraParentVisitsEmitted` to skip duplicates

#### Sibling Index Calculation

BFT calculates sibling indices differently depending on context:

```csharp
private NodePosition GetEffectivePosition()
{
    var effectiveDepth = InnerTreenumerator.Position.Depth - _SkippedStack.Count;

    int effectiveSiblingIndex;

    if (effectiveDepth == 0)
    {
        // Root level: use counter
        effectiveSiblingIndex = _SeenRootNodesCount;
    }
    else if (Mode == TreenumeratorMode.VisitingNode)
    {
        // Visiting: use parent's visit count - 1
        effectiveSiblingIndex = _NodePositionAndVisitCounts.GetFirst().VisitCount - 1;
    }
    else
    {
        // Scheduling: check previous node at same depth, or use saved position
        var previousNodePosition = _NodePositionAndVisitCounts.GetLast().Position;

        if (previousNodePosition.Depth == effectiveDepth)
            effectiveSiblingIndex = previousNodePosition.SiblingIndex + 1;
        else if (_LastRemovedSkippedNodePosition?.Depth == effectiveDepth)
            effectiveSiblingIndex = _LastRemovedSkippedNodePosition.Value.SiblingIndex + 1;
        else
            effectiveSiblingIndex = 0;
    }

    return new NodePosition(effectiveSiblingIndex, effectiveDepth);
}
```

**Key insight**: `_LastRemovedSkippedNodePosition` saves the position of recently-removed filtered nodes. This handles the case where a filtered sibling's position is needed to calculate the next sibling's index.

#### Predicate Inversion

Note that BFT inverts the predicate in the extension method:

```csharp
// In Treenumerable.Where.cs:
() => new WhereBreadthFirstTreenumerator<TNode>(
    source.GetBreadthFirstTreenumerator,
    nodeContext => !predicate(nodeContext),  // Inverted!
    NodeTraversalStrategies.SkipNode)
```

This is an implementation detail—the BFT implementation treats "true" as "skip this node."

---

### DFT vs BFT Where: Comparison

| Aspect | DFT | BFT |
|--------|-----|-----|
| **Data structures** | Two stacks (accepted + skipped) | Queue (accepted) + stack (skipped) |
| **Sibling index source** | Parent's `CurrentChildIndex` counter | Previous sibling position or saved position |
| **Parent visit handling** | Natural—visits interleave with scheduling | Complex—must track pending/extra visits |
| **Predicate** | Used directly | Inverted |
| **Depth calculation** | Sum of both stack counts - 1 | Inner depth - skipped stack count |
| **Complexity** | Moderate | High |

#### Why BFT Is Harder

In DFT, when you schedule a child and then visit it, the parent visit naturally occurs when you "return" to the parent level. The schedule→visit→parent-visit sequence is implicit in the stack unwinding.

In BFT, scheduling and visiting are decoupled. When a child is promoted:
1. You schedule it (need to emit parent visit for grandparent)
2. Other siblings may be scheduled (more parent visits interleaved)
3. Much later, you visit the child
4. The inner BFT emits parent visits for the filtered parent (which you must skip/suppress)

This temporal separation requires explicit tracking of "how many extra parent visits did I emit" vs "how many is the inner treenumerator trying to emit that I should skip."

---

### Implementing New Filter Operations

When implementing a new filtering operation similar to Where, consider:

1. **Do you need separate DFT/BFT implementations?** Usually yes, due to the different scheduling/visiting patterns.

2. **Track filtered ancestors**: You need to know the depth compression to calculate effective positions.

3. **Sibling index bookkeeping**: Either track via parent state (DFT) or via previous sibling position (BFT).

4. **Parent visit management**: DFT handles this naturally; BFT requires explicit tracking of pending/emitted visits.

5. **Test thoroughly**: Include tests for:
   - Filtering root nodes
   - Filtering leaf nodes
   - Filtering internal nodes (child promotion)
   - Multiple consecutive filtered ancestors
   - Mixed filtering patterns

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
