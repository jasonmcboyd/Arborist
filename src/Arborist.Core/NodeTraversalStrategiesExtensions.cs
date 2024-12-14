namespace Arborist.Core
{
  public static class NodeTraversalStrategiesExtensions
  {
    public static bool HasNodeTraversalStrategies(
      this NodeTraversalStrategies nodeTraversalStrategies,
      NodeTraversalStrategies strategies)
    {
      return (nodeTraversalStrategies & strategies) == strategies;
    }
  }
}
