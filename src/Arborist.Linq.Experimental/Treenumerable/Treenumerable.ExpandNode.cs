using Arborist.Core;
using System;

namespace Arborist.Linq.Experimental
{
  public static partial class Treenumerable
  {
    public static ITreenumerable<T> ExpandNode<T>(
      this ITreenumerable<T> rootStock,
      ITreenumerable<T> scion)
      => ExpandNode(rootStock, _ => scion, _ => true);

    public static ITreenumerable<T> ExpandNode<T>(
      this ITreenumerable<T> rootStock,
      ITreenumerable<T> scion,
      Func<NodeVisit<T>, bool> predicate)
      => ExpandNode(rootStock, _ => scion, predicate);

    public static ITreenumerable<T> ExpandNode<T>(
      this ITreenumerable<T> rootStock,
      Func<NodeVisit<T>, ITreenumerable<T>> scionGenerator)
      => ExpandNode(rootStock, scionGenerator, _ => true);

    public static ITreenumerable<T> ExpandNode<T>(
      this ITreenumerable<T> rootStock,
      Func<NodeVisit<T>, ITreenumerable<T>> scionGenerator,
      Func<NodeVisit<T>, bool> predicate)
      => rootStock.Graft(scionGenerator, predicate).Where(step => !predicate(step));
  }
}
