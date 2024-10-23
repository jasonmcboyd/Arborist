using System;

namespace Arborist
{
  public interface IChildEnumerator<TNode> : IDisposable
  {
    bool MoveNext(out NodeAndSiblingIndex<TNode> childNodeAndSiblingIndex);
  }
}
