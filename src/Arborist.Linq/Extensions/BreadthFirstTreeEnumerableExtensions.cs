//using Arborist.Common;
//using Arborist.Core;
//using Arborist.Linq.TreeEnumerable.BreadthFirstTree;
//using System.Collections.Generic;

//namespace Arborist.Linq.Extensions
//{
//  public static class BreadthFirstTreeEnumerableExtensions
//  {
//    public static ITreenumerable<TValue> ToTreenumerable<TValue>(
//      this IBreadthFirstTreeEnumerable<TValue> source)
//    {
//      var stack = new Stack<BreadthFirstTreeEnumerableToken<TValue>>();

//      foreach (var token in source)
//        stack.Push(token);

//      // If the stack is empty, return an empty Treenumerable.
//      if (stack.Count == 1)
//        return Treenumerable.Empty<TValue>();

//      var deque = new Deque<Stack<SimpleNode<TValue>>>();

//      var bottomLevelFilled = false;

//      // First level must be a generation separator. Pop it and add a null stack to
//      // deque to avoid setting bottomLevelFilled to true on the first iteration.
//      stack.Pop();
//      deque.AddToFront(null);

//      while (stack.Count > 1)
//      {
//        var token = stack.Pop();

//        if (token.Type == BreadthFirstTreeEnumerableTokenType.GenerationSeparator)
//        {
//          bottomLevelFilled = true;
//          deque.AddToFront(null);
//          continue;
//        }

//        if (token.Type == BreadthFirstTreeEnumerableTokenType.FamilySeparator)
//        {
//          deque.AddToFront(null);
//          continue;
//        }

//        if (deque[1] == null)
//          deque[1] = new Stack<SimpleNode<TValue>>();

//        var children =
//          bottomLevelFilled
//            ? deque.RemoveFromBack()
//            : null;

//        deque[1].Push(new SimpleNode<TValue>(token.Node, children));
//      }

//      return deque[1].ToTreenumerable();
//    }

//    public static IBreadthFirstTreeEnumerable<TNode> ToReverseLevelOrderTreeEnumerable<TNode>(this IBreadthFirstTreeEnumerable<TNode> source)
//    {
//      return new BreadthFirstTreeEnumerable<TNode>(source.ReverseLevelOrderTreeEnumerable());
//    }

//    private static IEnumerable<BreadthFirstTreeEnumerableToken<TNode>> ReverseLevelOrderTreeEnumerable<TNode>(this IEnumerable<BreadthFirstTreeEnumerableToken<TNode>> source)
//    {
//      if (source == null)
//        yield break;

//      var nodes = new Stack<BreadthFirstTreeEnumerableToken<TNode>>();

//      foreach (var token in source)
//      {
//        if (token.Type == BreadthFirstTreeEnumerableTokenType.GenerationSeparator)
//        {
//          while (nodes.Count > 1)
//            yield return nodes.Pop();

//          yield return token;
//        }
//        else
//        {
//          nodes.Push(token);
//        }
//      }

//      while (nodes.Count > 1)
//        yield return nodes.Pop();
//    }
//  }
//}
