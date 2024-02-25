using Arborist.TestUtils;
using Arborist.SimpleSerializer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Arborist.Tests
{
  [TestClass]
  public class TokenizerTests
  {
    public static IEnumerable<object[]> GetData()
    {
      yield return new object[]
      {
        "a,b,c",
        new[]
        {
          "a",
          ",",
          "b",
          ",",
          "c"
        }
      };
      yield return new object[]
      {
        "a(b,c)",
        new[]
        {
          "a",
          "(",
          "b",
          ",",
          "c",
          ")"
        }
      };
      yield return new object[]
      {
        "a(b(c))",
        new[]
        {
          "a",
          "(",
          "b",
          "(",
          "c",
          ")",
          ")",
        }
      };
      yield return new object[]
      {
        "a(b(c,d))",
        new[]
        {
          "a",
          "(",
          "b",
          "(",
          "c",
          ",",
          "d",
          ")",
          ")",
        }
      };
      yield return new object[]
      {
        "a(b,c),d(e,f)",
        new[]
        {
          "a",
          "(",
          "b",
          ",",
          "c",
          ")",
          "d",
          ",",
          "(",
          "e",
          ",",
          "f",
          ")",
        }
      };
    }

    [TestMethod]
    [DynamicData(nameof(GetData), DynamicDataSourceType.Method)]
    public void Tokenize(string treeString, string[] expected)
    {
      // Act
      Debug.WriteLine("-----Tree-----");
      Debug.WriteLine(treeString);

      Debug.WriteLine("\r\n-----Expected Values-----");
      foreach (var value in expected)
        Debug.WriteLine(value);

      Debug.WriteLine("\r\n-----Actual Values-----");
      var actual =
        Tokenizer
        .Tokenize(treeString)
        .Do(token => Debug.WriteLine(token))
        .Select(token => token.Symbol)
        .ToArray();

      // Assert
      CollectionAssert.AreEquivalent(expected, actual);
    }
  }
}
