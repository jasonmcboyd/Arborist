using System;

namespace Arborist.Tests.Utils
{
  internal static class TreenumeratorStateMap
  {
    public static TreenumeratorState ToState(char character)
    {
      switch (character)
      {
        case 'S':
          return TreenumeratorState.SchedulingNode;
        case 'N':
          return TreenumeratorState.EnumerationNotStarted;
        case 'V':
          return TreenumeratorState.VisitingNode;
        case 'F':
          return TreenumeratorState.EnumerationFinished;
        default:
          throw new InvalidOperationException();
      }
    }
    public static char ToChar(TreenumeratorState state)
    {
      switch (state)
      {
        case TreenumeratorState.EnumerationNotStarted:
          return 'N';
        case TreenumeratorState.EnumerationFinished:
          return 'F';
        case TreenumeratorState.SchedulingNode:
          return 'S';
        case TreenumeratorState.VisitingNode:
          return 'V';
        default:
          throw new InvalidOperationException();
      }
    }
  }
}
