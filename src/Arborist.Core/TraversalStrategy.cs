namespace Arborist.Core
{
  public enum TraversalStrategy
  {
    // TODO: Add option to stop traversing entirely
                     // | Traverse  |  Traverse   |
                     // |   Node    | Descendants |
                     // |-----------|-------------|
    TraverseSubtree, // |     T     |      T      |
    SkipDescendants, // |     T     |      F      |
    //SkipNode,        // |     F     |      T      |
    SkipSubtree,     // |     F     |      F      |
  }
}
