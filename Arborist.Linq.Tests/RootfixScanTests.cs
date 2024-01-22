//using Arborist.Linq;
//using Arborist.Tests.Utilities;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using System.Linq;

//namespace Arborist.Linq.Tests
//{
//  [TestClass]
//  public class RootfixScanTests
//  {
//    [TestMethod]
//    public void PreOrderTraversal_TwoLevels()
//    {
//      // Arrange
//      var root = TreeNode.Create(1, 2, 3);

//      var treenumerable = TestTreenumerableFactory.Create(root);

//      // Act
//      var actual =
//        treenumerable
//        .RootfixScan()
//        .PreOrderTraversal()
//        .Select(x => x.Value)
//        .ToArray();

//      // Assert
//      var expected = new[] { 1, 3, 4 };

//      Assert.That.SequencesAreEqual(expected, actual);
//    }
//  }
//}