using System.Collections.Generic;
using System.Linq;

namespace Battleships19
{
  class ShipFactory
  {
    public const string Columns = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

    public static HashSet<string> Vertical((int column, int row) coord, int length)
    {
      return Enumerable.
        Range(0, length).
        Select(x => ToString(coord.column, coord.row + x)).
        ToHashSet();
    }

    public static HashSet<string> Horizontal((int column, int row) coord, int length)
    {
      return Enumerable.
        Range(0, length).
        Select(x => ToString(coord.column + x, coord.row)).
        ToHashSet();
    }

    private static string ToString(int column, int row)
    {
      return $"{Columns[column]}{1 + row}";
    }
  }
}
