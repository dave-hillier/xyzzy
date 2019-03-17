using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Battleships19
{
  class RandomShipPositionGenerator
  {
    private static Random rng = new Random();
    internal Func<bool> NextOrientation = () => rng.Next(0, 2) == 1; // Internal for test override

    internal Func<(int column, int row)> NextCoordinates; // Internal for test override
    public int BoardSize { get; set; }

    public RandomShipPositionGenerator(int boardSize)
    {
      BoardSize = boardSize;
      NextCoordinates = () => (rng.Next(0, BoardSize), rng.Next(0, BoardSize));
    }

    public List<HashSet<string>> Generate(List<int> shipLengths)
    {
      var iterationLimit = 100;
      var result = new List<HashSet<string>>();
      foreach (var length in shipLengths)
      {
        HashSet<string> newShip;
        do
        {
          if (--iterationLimit == 0)
            throw new Exception("Could not place ship; iteration limit exceeded.");
          newShip = GenerateShip(length);
        } while (result.Any(ship => ship.Intersect(newShip).Any()));
        result.Add(newShip);
      }
      return result;
    }

    private HashSet<string> GenerateShip(int length)
    {
      var cooridnates = NextCoordinates();
      return NextOrientation() ?
        ShipFactory.Vertical((cooridnates.column, cooridnates.row % (BoardSize - length)), length) :
        ShipFactory.Horizontal((cooridnates.column % (BoardSize - length), cooridnates.row), length);
    }
  }
}
