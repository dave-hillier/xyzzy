using System.Collections.Generic;
using System.Linq;

namespace Battleships19
{
  class ShipFactory
  {
    public static List<string> Vertical((int column, int row) coord, int length)
    {
      return Enumerable.Range(0, length)
        .Select(x => (coord.column, coord.row + x).ToCoordinateString())
        .ToList();
    }

    public static List<string> Horizontal((int column, int row) coord, int length)
    {
      return Enumerable.Range(0, length)
        .Select(x => (coord.column + x, coord.row).ToCoordinateString())
        .ToList();
    }
  }
}
