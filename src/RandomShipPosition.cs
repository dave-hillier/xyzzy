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
    public static Func<bool> NextOrientation = () => rng.Next(0, 2) == 1;
    public static Func<(int column, int row)> NextCoordinates = () => (rng.Next(0, Game.BoardSize), rng.Next(0, Game.BoardSize));

    private static HashSet<string> Horizontal((int column, int row) coord, int length)
    {
      return Enumerable.
        Range(0, length).
        Select(x => ToString(coord.column, coord.row + x)).
        ToHashSet();
    }

    private static HashSet<string> Vertical((int column, int row) coord, int length)
    {
      return Enumerable.
        Range(0, length).
        Select(x => ToString(coord.column + x, coord.row)).
        ToHashSet();
    }

    private static string ToString(int column, int row)
    {
      return $"{columns[column]}{1 + row}";
    }

    public static List<HashSet<string>> Generate()
    {
      var result = new List<HashSet<string>>();
      foreach (var length in Lengths)
      {
        HashSet<string> ship;
        do
        {
          ship = GenerateShip(length);
        } while (result.Any(s => s.Intersect(ship).Any())); // TODO: iteration limit
        result.Add(ship);
      }
      return result;
    }

    private static HashSet<string> GenerateShip(int length)
    {
      var cooridnates = NextCoordinates();
      return NextOrientation() ?
        Horizontal((cooridnates.column, cooridnates.row % (Game.BoardSize - length)), length) :
        Vertical((cooridnates.column % (Game.BoardSize - length), cooridnates.row), length);
    }
  }
}
