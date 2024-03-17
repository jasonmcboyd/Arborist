using System.Diagnostics;

namespace Arborist.TestUtils
{
  public static class NodeVisitsDebugWriter
  {
    public static void WriteNodeVisitHeader()
    {
      Debug.WriteLine("OP      P       M  VC N");
      Debug.WriteLine("-------------------------");
    }
  }
}
