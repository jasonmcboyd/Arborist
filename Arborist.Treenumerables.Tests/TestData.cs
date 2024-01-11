using Arborist.Tests.Utils;
using System;

namespace Arborist.Treenumerables.Tests
{
  public class TestData
  {
    public TestData()
    {
    }

    public TestData(
      string treeString,
      Func<NodeVisit<char>, bool> skipPredicate,
      SchedulingStrategy skipStrategy,
      string description,
      MoveNextResult<char>[] expectedDepthFirstResults,
      MoveNextResult<char>[] expectedBreadthFirstResults)
    {
      TreeString = treeString;
      SkipPredicate = skipPredicate;
      SkipStrategy = skipStrategy;
      Description = description;
      ExpectedDepthFirstResults = expectedDepthFirstResults;
      ExpectedBreadthFirstResults = expectedBreadthFirstResults;
    }

    public string TreeString { get; set; }
    public Func<NodeVisit<char>, bool> SkipPredicate { get; set; }
    public SchedulingStrategy SkipStrategy { get; set; }
    public string Description { get; set; }
    public MoveNextResult<char>[] ExpectedDepthFirstResults { get; set; }
    public MoveNextResult<char>[] ExpectedBreadthFirstResults { get; set; }
  }
}
