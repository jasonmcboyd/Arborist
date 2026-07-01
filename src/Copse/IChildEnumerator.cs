using System;

namespace Copse
{
  public interface IChildEnumerator<TNode> : IDisposable
  {
    bool MoveNext(out NodeAndSiblingIndex<TNode> childNodeAndSiblingIndex);
  }
}
