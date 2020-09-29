using System.Collections.Generic;
using System.Linq;

namespace Battleships19
{
  // Strategy used for tests; place the ships in predictable positions.
  class FixedShipPositionGenerator
  {
    public List<List<string>> Generate(IEnumerable<int> shipLengths)
    {
      return shipLengths.Select((l, i) => ShipFactory.Horizontal((0, i), l)).ToList();
    }
  }
}
