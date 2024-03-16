﻿using Arborist.Core;
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
        case 'N':
          return TreenumeratorMode.EnumerationNotStarted;
        case 'V':
          return TreenumeratorMode.VisitingNode;
        case 'F':
          return TreenumeratorMode.EnumerationFinished;
        default:
          throw new InvalidOperationException();
      }
    }
    public static char ToChar(TreenumeratorMode mode)
    {
      switch (mode)
      {
        case TreenumeratorMode.EnumerationNotStarted:
          return 'N';
        case TreenumeratorMode.EnumerationFinished:
          return 'F';
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
