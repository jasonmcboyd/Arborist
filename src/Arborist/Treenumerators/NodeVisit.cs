﻿using Arborist.Core;
using System;
using System.Collections;

namespace Arborist.Treenumerators
{
  internal struct NodeVisit<TNode>
  {
    public NodeVisit(
      TreenumeratorMode mode,
      TNode node,
      int visitCount,
      NodePosition position,
      NodeTraversalStrategies nodeTraversalStrategies)
    {
      Mode = mode;
      Node = node;
      VisitCount = visitCount;
      Position = position;
      NodeTraversalStrategies = nodeTraversalStrategies;
    }

    public TreenumeratorMode Mode { get; set; }
    public TNode Node { get; set; }
    public int VisitCount { get; set; }
    public NodePosition Position { get; set; }
    public NodeTraversalStrategies NodeTraversalStrategies { get; set; }

    public override string ToString()
    {
      var node =
        (Node is IEnumerator enumerator)
        ? enumerator.Current.ToString()
        : Node.ToString();

      return $"{Position}  {ModeToChar()}  {VisitCount}  {node}";
    }

    private char ModeToChar()
    {
      switch (Mode)
      {
        case TreenumeratorMode.SchedulingNode:
          return 'S';
        case TreenumeratorMode.VisitingNode:
          return 'V';
        default:
          throw new NotImplementedException();
      }
    }
  }
}
