using Arborist.Tests.Utils;
using System;

namespace Arborist.Treenumerables.Tests
{
  public class TestScenario
  {
    public Func<NodeVisit<string>, SchedulingStrategy> SchedulingPredicate { get; set; }
    public string Description { get; set; }
    public MoveNextResult<string>[] ExpectedDepthFirstResults { get; set; }
    public MoveNextResult<string>[] ExpectedBreadthFirstResults { get; set; }
  }
}
