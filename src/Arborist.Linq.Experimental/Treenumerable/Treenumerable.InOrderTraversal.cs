using Arborist.Core;
using System;
using System.Collections.Generic;

namespace Arborist.Linq.Experimental
{
  public static partial class Treenumerable
  {
    public static IEnumerable<T> InOrderOrderTraversal<T>(this ITreenumerable<T> source)
    {
      // TODO:
      // I have avoided this for a long time because of the ambiguity about when
      // the parent node should be yielded. On a lark, I started pondering what it
      // would mean to support left and right rotations, today, and what that might
      // look like. That too, has a lot of ambiguity. I was perusing the Wikipedia
      // page about tree rotations and happened to notice a section explaining that
      // the in-order traversal of a binary tree is invariant under rotations.
      //
      // It ocurred to me that if I maintained that invariance, I might be able to
      // constrain the problem enough that I can eliminate the ambiguity. After
      // some more thought I decided if I enforce the following rules:
      //
      // Given a Tree *, a left rotation operation L, a right rotation operation R,
      // and a in-order traversal operation IO, and the following rules:
      //
      // 1. L(R(*)) = * = R(L(*))
      // 2. IO(L(*)) = IO(*) = IO(R(*))
      //
      // These rules can be satisfied if the in-order traversal operation yields
      // the parent node after the first child subtree.
      //
      // Rotations are going to be a little tricky, but after trying out several
      // different approaches, I think I have a good idea of how to implement them.

      throw new NotImplementedException();
    }
  }
}
