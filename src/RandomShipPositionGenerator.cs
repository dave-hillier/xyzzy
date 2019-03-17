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

    public List<List<string>> Generate(IEnumerable<int> shipLengths)
    {
      var iterationLimit = 2 * BoardSize * BoardSize;
      var result = new List<List<string>>();
      foreach (var length in shipLengths)
      {
        List<string> newShip;
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

    private List<string> GenerateShip(int length)
    {
      var cooridnates = NextCoordinates();
      return NextOrientation() ?
        ShipFactory.Vertical((cooridnates.column, cooridnates.row % (BoardSize - (length - 1))), length) :
        ShipFactory.Horizontal((cooridnates.column % (BoardSize - (length - 1)), cooridnates.row), length);
    }
  }
}
