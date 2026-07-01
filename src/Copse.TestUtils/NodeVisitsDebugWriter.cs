using System.Diagnostics;

namespace Copse.TestUtils
{
  public static class NodeVisitsDebugWriter
  {
    public static void WriteNodeVisitHeader()
    {
      Debug.WriteLine("OP      M  VC N");
      Debug.WriteLine("----------------");
    }
  }
}
