# Memoize — design spec (preorder, incremental, eager-skip)

> **Status: DECIDED, not yet implemented.** Decision recorded 2026-06-27. This supersedes the
> dead experimental `MemoizeTreenumerator` stub (`Arborist.Linq.Experimental`), which is
> abandoned — do **not** resurrect it. The broad capability-interface lattice
> ([TREE_CAPABILITY_INTERFACES.md](TREE_CAPABILITY_INTERFACES.md)) is *out of scope* here:
> this design deliberately needs **no** child-realization capability.

## What `Memoize` is

`Memoize()` turns a tree into a **re-traversable, shared snapshot of its current shape**. The
caller is declaring "I will enumerate this tree more than once; capture it now." Subsequent
enumerations replay from the cache instead of re-running the (possibly expensive) source, and
each enumeration may prune differently (follow one branch, then another).

```csharp
var memo = expensiveTree.Memoize();   // shape captured = whatever expensiveTree is right now
foreach over memo (DFS, follow branch A)   // pays to build only the preorder prefix it reaches
foreach over memo (DFS, follow branch B)   // reuses the shared buffer, extends it as needed
foreach over memo (BFS)                    // forces full build, then rides BFS over PreorderTree
```

## The four locked decisions

1. **No holes / eager-skip.** The cache is filled by driving the inner traversal with
   `NodeTraversalStrategies.TraverseAll`. A consumer's pruning is applied only at *replay*, as a
   view — it changes what a given enumeration *yields*, never what is *cached*. The buffer is
   therefore always a complete, contiguous prefix of the full pre-order stream.

2. **No child-realization.** Because there are never holes, we never need to realize a node's
   children out of order to fill one. `IChildVisitableTree` is not required. This is the whole
   reason the design is simple: the "hole problem" that motivated child-realization is
   *dissolved*, not solved.

3. **Finite-tree precondition.** Driving the inner `TraverseAll` means an unbounded tree never
   surfaces (DFT in particular dives down branch 0 forever and never buffers node 2). So
   **Memoize requires a finite source.** The caller bounds it first
   (`tree.PruneAfter(d).Memoize()`). Infinite-tree memoization is explicitly traded away — that
   was child-realization's domain, and we have dropped it on purpose. *(Optional later nicety: a
   node-count guard that throws instead of hanging on an unbounded source.)*

4. **Representation = preorder, one structure.** The memo is a `PreorderTree<TValue>`
   (`values[]` + `subtreeSizes[]`, `src/Arborist/Treenumerables/PreorderTree.cs`), built
   incrementally. Both DFS and BFS replay ride the existing engine over it. BFS over a preorder
   layout is correct and O(N) but not cache-sequential — that locality tax is **accepted**;
   a level-order/LOUDS variant is deferred to the *serialization* track, where sequential disk
   locality actually is non-negotiable. (See "Why preorder is enough" below.)

## Why it's distinct from `Materialize`

| | `Materialize()` | `Memoize()` |
|---|---|---|
| Build timing | eager, all at once | lazy, on first use |
| Build extent | always the whole tree | only the pre-order prefix some enumeration reaches |
| Sharing | n/a (one snapshot) | one growing buffer shared across all enumerations |
| End content (finite, fully traversed) | full `PreorderTree` | identical full `PreorderTree` |

The genuine win over "just `Materialize` lazily" is **partial + shared**: a consumer that
memoizes and then does a DFS that stops early (a `Take`, or a prune that terminates) pays only
for the prefix it touches; later enumerations reuse and extend that buffer. For a
fully-traversed finite tree the two converge on the same content.

## Why preorder is enough for both modes

`PreorderTree` already rides the generic DFS **and** BFS engine via `PreorderChildEnumerator`
(`src/Arborist/PreorderChildEnumerator.cs`): given any node it locates that node's children by
subtree-size hops, and `Dispose()` on the enumerator is how the engine signals
`SkipDescendants`/`SkipSiblings`. So:

- **DFS replay** = linear scan; `SkipDescendants` is an O(1) span hop (`i += subtreeSizes[i]`).
- **BFS replay** = the standard BFS engine driving the same child enumerator. Correct, O(N),
  but visits the array out of order (locality tax only).

Crucially, **consumer pruning comes for free**: replay is just the normal DFT/BFT engine
traversing a `PreorderTree`, and that engine already honors every `NodeTraversalStrategies`
flag. We do **not** hand-write replay-pruning. The only memoize-specific machinery is the
*incremental builder* (so a DFS early-stop consumer needn't build the whole tree first).

## Mechanism

A `MemoizedTree<TValue>` (final name TBD) holds:

- the source `ITreenumerable<TValue>`;
- a lazily-created **shared inner DFT treenumerator**, driven `TraverseAll`;
- the **growing pre-order builder**: `List<TValue> _values`, `List<int> _subtreeSizes`, plus
  parse state — a `Stack<int>` of open-parent indices and the previous scheduling depth. This is
  exactly the construction `TreeSerializer.Parse` uses (`src/Arborist.SimpleSerializer/
  TreeSerializer.cs`), but fed from the **live DFT scheduling stream** instead of a char span;
- an `_exhausted` flag.

**The feed.** A DFT driven `TraverseAll` emits each node's `SchedulingNode` visit exactly once,
in pre-order, with `Position.Depth` available. That scheduling stream *is* the pre-order value
sequence. Subtree *close* is detected from depth deltas (the array analogue of `)`): when the
next scheduling visit arrives at depth `d`, every open parent at depth `>= d` has closed, so pop
each and backfill `subtreeSizes[openIndex] = _values.Count - openIndex` (subtrees are contiguous
in pre-order). At stream exhaustion, pop and backfill all remaining open parents. Visiting visits
are ignored for structure. *(This is `Parse`'s open-stack logic, paren-deltas → depth-deltas.)*

**Fill primitive.** `EnsureBufferedThrough(...)` pulls inner `MoveNext(TraverseAll)` results,
appending values on scheduling visits and backfilling sizes on close, until the requested
boundary is buffered:
- to serve DFS value at index `k`: pull until `_values.Count > k` (or exhausted);
- to serve `SkipDescendants` at `k`: pull until node `k`'s subtree closes (next scheduling visit
  at depth `<= depth[k]`, or exhausted) so `subtreeSizes[k]` is known.

**DFS replay** — `GetDepthFirstTreenumerator()` returns a treenumerator walking the pre-order
arrays by index, calling the fill primitive when it needs an index/size beyond the buffer.
Genuinely incremental; an early-stop consumer never drives past its own frontier.

**BFS replay** — `GetBreadthFirstTreenumerator()` ensures the buffer is fully built (drive inner
to exhaustion), then rides the BFS engine over the completed `PreorderTree`. BFS over a *partial*
pre-order layout is unsafe (root/level navigation needs completed subtree sizes — `RootIndices`
hops by `subtreeSizes[0]` to find root 1, etc.), so BFS forces full build. Acceptable: the tree
is finite, and the source still runs only once.

Once fully built, the buffer **is** a `PreorderTree<TValue>` — the incremental machinery is just
an on-demand `PreorderTree` builder.

## Edge cases the implementation must cover

- **Empty tree** (`_values.Count == 0`) — replay yields nothing; build exhausts immediately.
- **Single node** — `subtreeSizes = [1]`.
- **Multi-root forest** — the builder must close top-level subtrees back to depth 0; `Parse`'s
  open-stack already does this and `PreorderTree.RootIndices` already walks top-level spans.
- **Consumer `SkipNode` vs `SkipDescendants` vs `SkipSiblings`** on replay — all inherited from
  the engine over `PreorderTree`; verify each against a known fixture, since `SkipNode` (drop
  node, keep/promote children) is structurally different from `SkipDescendants`.
- **Partial first enumeration then a second, deeper one** — buffer extends; shared inner resumes
  from where it paused (single shared instance, append-only buffer).

## Deferred (implementation-level, not blocking the decision)

- **Naming** — `Memoize` operator; returned type; reuse the existing
  `ITreenumerableBuffer<TNode> : ITreenumerable, IDisposable` marker (the memo owns a disposable
  inner, so `IDisposable` fits).
- **Thread-safety** — start single-threaded and document it (the shared inner + growing buffer is
  not thread-safe). The old stub sketched per-strategy locks for concurrent replay; add only if a
  real need appears.
- **Disposal** — disposing the memo disposes the inner; define behavior for outstanding replay
  enumerators.
- **Surface** — possibly expose `ToPreorderTree()` once fully built.
- **Home** — `Memoize` extension in `Arborist.Linq`; backing type backed by `PreorderTree` (in
  `Arborist`). This is *not* the broad project-boundary question — just this operator's placement.

## Connection to serialization (recorded, not actioned here)

The same "which traversal order does the linear store favor" question underlies both Memoize and
ser/deser. The pre-order/DFS form is a balanced-parentheses encoding (schedule = enter,
final-visit = leave; `subtreeSizes` is that bracketing in array form); the BFS-favorable dual is
level-order/LOUDS. For **in-memory** memo, one structure suffices (BFS only loses locality). For
**disk streaming**, sequential locality is single-order by nature, so a *layout knob*
(pre-order vs level-order) genuinely earns its keep — that belongs to the serialization track,
and Memoize can borrow it later if a BFS-heavy memo workload is ever benchmarked to need it.
