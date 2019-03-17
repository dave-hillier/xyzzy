using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Battleships19
{
  class FixedShipPositionGenerator
  {
    public List<HashSet<string>> Generate(List<int> shipLengths)
    {
      return new List<HashSet<string>> {
        new HashSet<string> { "A1", "A2", "A3", "A4" },
        new HashSet<string> { "B1", "B2", "B3", "B4" },
        new HashSet<string> { "C1", "C2", "C3", "C4", "C5" },
      };
    }
  }
}
