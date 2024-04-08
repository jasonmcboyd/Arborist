using Arborist.Core;
using System;

namespace Arborist.TestUtils
{
  internal static class TreenumeratorModeMap
  {
    public static TreenumeratorMode ToMode(char character)
    {
      switch (character)
      {
        case 'S':
          return TreenumeratorMode.SchedulingNode;
        case 'V':
          return TreenumeratorMode.VisitingNode;
        default:
          throw new InvalidOperationException();
      }
    }
    public static char ToChar(TreenumeratorMode mode)
    {
      switch (mode)
      {
        case TreenumeratorMode.SchedulingNode:
          return 'S';
        case TreenumeratorMode.VisitingNode:
          return 'V';
        default:
          throw new InvalidOperationException();
      }
    }
  }
}
