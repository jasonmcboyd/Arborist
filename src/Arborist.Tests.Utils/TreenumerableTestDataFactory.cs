using System.Collections.Generic;
using System.Reflection;

namespace Arborist.Tests.Utils
{
  public class TreenumerableTestDataFactory
  {
    public TreenumerableTestDataFactory(TreeTestDefinition[] treeTestDefinitions)
    {
      _TreeTestDefinitions = treeTestDefinitions;
    }

    private TreeTestDefinition[] _TreeTestDefinitions;

    public IEnumerable<TreeTestScenarioDefinition> GetTestScenarios()
    {
      for (int i = 0; i < _TreeTestDefinitions.Length; i++)
      {
        var testTree = _TreeTestDefinitions[i];

        for (int j = 0; j < testTree.TestScenarios.Count; j++)
        {
          var testScenario = testTree.TestScenarios[j];

          yield return new TreeTestScenarioDefinition(testTree.TreeString, testScenario.Description, i, j);
        }
      }
    }

    public TestScenario GetTestScenario(int testIndex, int testScenarioIndex)
      => _TreeTestDefinitions[testIndex].TestScenarios[testScenarioIndex];

    public IEnumerable<object[]> GetTestData()
    {
      foreach (var testScenario in GetTestScenarios())
        yield return new object[]
        {
          testScenario.TreeString,
          testScenario.Description,
          testScenario.TestIndex,
          testScenario.TestScenarioIndex,
        };
    }

    public string GetTestDisplayName(MethodInfo methodInfo, object[] data)
      => $"{data[0]} -> {data[1]}";
  }
}
