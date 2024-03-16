﻿using Arborist.Core;
using System;

namespace Arborist.TestUtils
{
  public class TestScenario
  {
    public Func<NodeVisit<string>, SchedulingStrategy> SchedulingStrategySelector { get; set; }
    public Func<ITreenumerable<string>, ITreenumerable<string>> TreenumerableMap { get; set; }
    public string Description { get; set; }
    public NodeVisit<string>[] ExpectedDepthFirstResults { get; set; }
    public NodeVisit<string>[] ExpectedBreadthFirstResults { get; set; }
  }
}
