using Arborist.Tests.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arborist.Treenumerables.Tests
{
  public static class TestDataFactory
  {
    public static IEnumerable<TestData> GetTestData()
    {
      yield return new TestData
      {
        TreeString = "a,b,c",
        SkipPredicate = null,
        Description = "No skipping",
        ExpectedBreadthFirstResults = new MoveNextResult<char>[]
        {
          (TreenumeratorState.SchedulingNode, 'a', 0, 0, 0),
          (TreenumeratorState.SchedulingNode, 'b', 0, 1, 0),
          (TreenumeratorState.SchedulingNode, 'c', 0, 1, 0),
          (TreenumeratorState.VisitingNode,   'b', 1, 0, 0),
          (TreenumeratorState.VisitingNode,   'b', 2, 0, 0),
          (TreenumeratorState.SchedulingNode, 'c', 0, 2, 0),
          (TreenumeratorState.VisitingNode,   'c', 1, 1, 0),
          (TreenumeratorState.VisitingNode,   'c', 2, 1, 0),
        },
        ExpectedDepthFirstResults = new MoveNextResult<char>[]
        {
          (TreenumeratorState.SchedulingNode, 'a', 0, 0, 0),
          (TreenumeratorState.SchedulingNode, 'b', 0, 1, 0),
          (TreenumeratorState.VisitingNode,   'a', 1, 0, 0),
          (TreenumeratorState.VisitingNode,   'a', 2, 0, 0),
          (TreenumeratorState.VisitingNode,   'b', 1, 1, 0),
          (TreenumeratorState.VisitingNode,   'b', 2, 1, 0),
        }
      };
      yield return new TestData
      {
        TreeString = "a,b,c",
        SkipPredicate = visit => visit.Node == 'a',
        Description = "No skipping",
        ExpectedBreadthFirstResults = new MoveNextResult<char>[]
        {
          (TreenumeratorState.SchedulingNode, 'a', 0, 0, 0),
          (TreenumeratorState.SchedulingNode, 'b', 0, 1, 0),
          (TreenumeratorState.VisitingNode,   'b', 1, 0, 0),
          (TreenumeratorState.VisitingNode,   'b', 2, 0, 0),
          (TreenumeratorState.SchedulingNode, 'c', 0, 2, 0),
          (TreenumeratorState.VisitingNode,   'c', 1, 1, 0),
          (TreenumeratorState.VisitingNode,   'c', 2, 1, 0),
        },
        ExpectedDepthFirstResults = new MoveNextResult<char>[]
        {
          (TreenumeratorState.SchedulingNode, 'a', 0, 0, 0),
          (TreenumeratorState.SchedulingNode, 'b', 0, 1, 0),
          (TreenumeratorState.VisitingNode,   'a', 1, 0, 0),
          (TreenumeratorState.VisitingNode,   'a', 2, 0, 0),
          (TreenumeratorState.VisitingNode,   'b', 1, 1, 0),
          (TreenumeratorState.VisitingNode,   'b', 2, 1, 0),
        }
      };
    }
  }
}
