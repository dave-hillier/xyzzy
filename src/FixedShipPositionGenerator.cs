using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Battleships19
{
  class FixedShipPositionGenerator
  {
    public List<List<string>> Generate(IEnumerable<int> shipLengths)
    {
      return shipLengths.Select((l, i) => ShipFactory.Horizontal((0, i), l)).ToList();
    }
  }
}
