using System;

namespace Arborist.Tests.Utils
{
  public class TestScenario
  {
    public Func<ITreenumerator<string>, SchedulingStrategy> SchedulingPredicate { get; set; }
    public Func<ITreenumerable<string>, ITreenumerable<string>> TreenumerableMap { get; set; }
    public string Description { get; set; }
    public MoveNextResult<string>[] ExpectedDepthFirstResults { get; set; }
    public MoveNextResult<string>[] ExpectedBreadthFirstResults { get; set; }
  }
}
