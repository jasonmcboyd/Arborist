namespace Arborist.Core
{
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
