using Arborist.Linq;
using Arborist.Tests.Utils;
using Arborist.Treenumerables;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Arborist.Linq.Tests
{
  [TestClass]
  public class GetBranchesTest

  {
    [TestMethod]
    public void GetBranches()
    {
      // Arrange
      var root =
        TreeNode.Create(1,
          TreeNode.Create(2),
          TreeNode.Create(3, 4, 5));

      var treenumerable = TestTreenumerableFactory.Create<TreeNode<int>, int>(root);

      // Act
      var actual =
        treenumerable
        .GetBranches()
        .Select(nums => nums.ToArray())
        .ToArray();

      // Assert
      var expected =
        new []
        {
          new [] { 1, 2 },
          new [] { 1, 3, 4 },
          new [] { 1, 3, 5 }
        };

      Assert.AreEqual(expected.Length, actual.Length);
      foreach (var (expectedBranch, actualBranch) in expected.Zip(actual, (e, a) => (e, a)))
        CollectionAssert.AreEqual(expectedBranch, actualBranch);
    }
  }
}
