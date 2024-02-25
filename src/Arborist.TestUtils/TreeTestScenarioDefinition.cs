namespace Arborist.TestUtils
{
  public class TreeTestScenarioDefinition
  {
    public TreeTestScenarioDefinition(
      string treeString,
      string description,
      int testIndex,
      int testScenarioIndex)
    {
      TreeString = treeString;
      Description = description;
      TestIndex = testIndex;
      TestScenarioIndex = testScenarioIndex;
    }

    public string TreeString { get; }
    public string Description { get; }
    public int TestIndex { get; }
    public int TestScenarioIndex { get; }
  }
}
