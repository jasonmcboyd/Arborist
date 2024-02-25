using Arborist.Core;
using System;
using System.Collections.Generic;

namespace Arborist.Linq
{
  public static partial class Treenumerable
  {
    public static IEnumerable<T> Invert<T>(
      this ITreenumerable<T> rootStock,
      ITreenumerable<T> scion,
      Func<NodeVisit<T>, bool> predicate)
    {
      throw new NotImplementedException();
    }
  }
}
