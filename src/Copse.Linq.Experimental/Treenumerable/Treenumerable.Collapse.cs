using Copse.Core;
using System;
using System.Collections.Generic;

namespace Copse.Linq.Experimental
{
  public static partial class Treenumerable
  {
    public static IEnumerable<T> Collapse<T>(
      this ITreenumerable<T> rootStock,
      ITreenumerable<T> scion,
      Func<NodeVisit<T>, bool> predicate)
    {
      throw new NotImplementedException();
    }
  }
}
