namespace Arborist.Core
{
  public enum TraversalStrategy
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
