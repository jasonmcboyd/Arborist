using System.Diagnostics;

namespace Arborist.TestUtils
{
  public static class NodeVisitsDebugWriter
  {
    public static void WriteNodeVisitHeader()
    {
      Debug.WriteLine("OP       M  VC N");
      Debug.WriteLine("------------------");
    }
  }
}
