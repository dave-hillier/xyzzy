using System.Collections.Generic;
using System.Linq;

namespace Battleships19
{
  class ShipFactory
  {
    public const string Columns = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

    public static List<string> Vertical((int column, int row) coord, int length)
    {
      return Enumerable.Range(0, length)
        .Select(x => ToString(coord.column, coord.row + x))
        .ToList();
    }

    public static List<string> Horizontal((int column, int row) coord, int length)
    {
      return Enumerable.Range(0, length)
        .Select(x => ToString(coord.column + x, coord.row))
        .ToList();
    }

    private static string ToString(int column, int row)
    {
      return $"{Columns[column]}{1 + row}";
    }
  }
}
