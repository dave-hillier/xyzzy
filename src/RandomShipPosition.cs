using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Battleships19
{
  class RandomShipPosition
  {
    public static List<int> Lengths = new List<int> { 5, 4, 4 };
    private const string columns = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    private static Random rng = new Random();
    public static Func<string> NextOrientation = () => rng.Next(0, 2) == 1 ? "h" : "v";
    public static Func<(int column, int row)> NextCoordinates = () => (rng.Next(0, Game.BoardSize), rng.Next(0, Game.BoardSize));

    private static HashSet<string> Horizontal((int column, int row) coord, int length)
    {
      return Enumerable.
        Range(0, length).
        Select(x => $"{columns[coord.column + x]}{1 + coord.row}").
        ToHashSet();
    }

    private static HashSet<string> Vertical((int column, int row) coord, int length)
    {
      return Enumerable.
        Range(0, length).
        Select(x => $"{columns[coord.column]}{1 + coord.row + x}").
        ToHashSet();
    }

    public static List<HashSet<string>> Generate()
    {
      return Lengths.Select(l =>
      {
        var o = NextOrientation();
        var c = NextCoordinates();

        return o == "h" ? Horizontal(c, l) : Vertical(c, l);
      }).ToList();
    }
  }
}
