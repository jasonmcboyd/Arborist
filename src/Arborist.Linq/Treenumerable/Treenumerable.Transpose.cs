using Arborist.Core;
using Arborist.Treenumerables;
using System.Collections.Generic;

namespace Arborist.Linq
{
  public static partial class Treenumerable
  {
    public static ITreenumerable<TNode> Transpose<TNode>(
      this ITreenumerable<TNode> source)
    {
      IEnumerable<SimpleNode<TNode>> GetRootNodes()
      {
        var levels = source.GetLevels();

        var stack = new RefSemiDeque<TNode>();

        foreach (var level in levels)
        {
          foreach (var node in level)
            stack.AddLast(node);

          var rootNode = new SimpleNode<TNode>(stack.RemoveLast());

          while (stack.Count > 0)
            rootNode = new SimpleNode<TNode>(stack.RemoveLast(), new[] { rootNode });

          yield return rootNode;
        }
      }

      return new SimpleNodeTreenumerable<TNode>(GetRootNodes());
    }
  }
}
