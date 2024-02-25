namespace Arborist.Core
{
  // TODO: I think TraversalStrategy is a better name.
  public enum SchedulingStrategy
  {
                     // | Traverse  |  Traverse   |
                     // |   Node    | Descendants |
                     // |-----------|-------------|
    TraverseSubtree, // |     T     |      T      |
    SkipDescendants, // |     T     |      F      |
    SkipNode,        // |     F     |      T      |
    SkipSubtree,     // |     F     |      F      |
  }
}
