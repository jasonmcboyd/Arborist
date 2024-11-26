using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arborist.Tests
{
  [TestClass]
  public class RefSemiDequeTests
  {
    [TestMethod]
    public void AddSingleItem()
    {
      // Arrange
      var sut = new RefSemiDeque<int>();

      // Act
      sut.AddLast(1);

      // Assert
      Assert.IsTrue(Enumerable.SequenceEqual(sut, new [] { 1 }));
    }


    [TestMethod]
    public void AddMoreItemsThanInitialPartitionCapacity()
    {
      // Arrange
      var sut = new RefSemiDeque<int>(1);

      // Act
      sut.AddLast(1);
      sut.AddLast(2);

      // Assert
      Assert.IsTrue(Enumerable.SequenceEqual(sut, new [] { 1, 2 }));
    }

    [TestMethod]
    public void AddTwo_RemoveFirst()
    {
      // Arrange
      var sut = new RefSemiDeque<int>();

      // Act
      sut.AddLast(1);
      sut.AddLast(2);
      sut.RemoveFirst();

      // Assert
      Assert.IsTrue(Enumerable.SequenceEqual(sut, new [] { 2 }));
    }

    [TestMethod]
    public void AddTwo_RemoveLast()
    {
      // Arrange
      var sut = new RefSemiDeque<int>();

      // Act
      sut.AddLast(1);
      sut.AddLast(2);
      sut.RemoveLast();

      // Assert
      Assert.IsTrue(Enumerable.SequenceEqual(sut, new [] { 1 }));
    }

    [TestMethod]
    public void AddMoreItemsThanInitialPartitionCapacity_RemoveAllItemsInFirstPartition()
    {
      // Arrange
      var sut = new RefSemiDeque<int>(1);

      // Act
      sut.AddLast(1);
      sut.AddLast(2);
      sut.RemoveFirst();

      // Assert
      Assert.IsTrue(Enumerable.SequenceEqual(sut, new [] { 2 }));
    }
  }
}
