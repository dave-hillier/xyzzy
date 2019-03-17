using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Battleships19
{
  class RandomShipPositionGenerator
  {
    private const string columns = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    private static Random rng = new Random();
    internal Func<bool> NextOrientation = () => rng.Next(0, 2) == 1; // Internal for tests
    public int BoardSize { get; set; }
    internal Func<(int column, int row)> NextCoordinates; // Internal for tests

    public RandomShipPositionGenerator(int boardSize)
    {
      BoardSize = boardSize;
      NextCoordinates = () => (rng.Next(0, BoardSize), rng.Next(0, BoardSize));
    }

    private static HashSet<string> Vertical((int column, int row) coord, int length)
    {
      return Enumerable.
        Range(0, length).
        Select(x => ToString(coord.column, coord.row + x)).
        ToHashSet();
    }

    private static HashSet<string> Horizontal((int column, int row) coord, int length)
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

    public List<HashSet<string>> Generate(List<int> shipLengths)
    {
      var result = new List<HashSet<string>>();
      foreach (var length in shipLengths)
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

    private HashSet<string> GenerateShip(int length)
    {
      var cooridnates = NextCoordinates();
      return NextOrientation() ?
        Vertical((cooridnates.column, cooridnates.row % (BoardSize - length)), length) :
        Horizontal((cooridnates.column % (BoardSize - length), cooridnates.row), length);
    }
  }
}
