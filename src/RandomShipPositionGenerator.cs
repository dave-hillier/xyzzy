using System;
using System.Collections.Generic;
using System.Linq;

namespace Battleships19
{
  class RandomShipPositionGenerator
  {
    private static Random rng = new Random();
    
    // The following two fields are internal so that they can be overriden by tests. This could be considered a hack,
    // but given the only use case for overriding these fields in the tests, I didnt seem much point in providing a 
    // constructor for overriding them. This method is less boilerplate than constructor injection and I picked it
    // because I don't need to change the implementation for anything but tests 
    internal Func<bool> NextOrientation = () => rng.Next(0, 2) == 1; 
    internal Func<(int column, int row)> NextCoordinates;
    private int BoardSize { get; }

    public RandomShipPositionGenerator(int boardSize)
    {
      BoardSize = boardSize;
      NextCoordinates = () => (rng.Next(0, BoardSize), rng.Next(0, BoardSize));
    }

    public List<List<string>> Generate(IEnumerable<int> shipLengths)
    {
      var iterationLimit = 2 * BoardSize * BoardSize;
      var shipPositions = new List<List<string>>();
      foreach (var length in shipLengths)
      {
        List<string> newShip;
        do
        {
          if (--iterationLimit == 0)
            throw new Exception("Could not place ship; iteration limit exceeded.");
          newShip = GenerateShip(length);
        } while (shipPositions.Any(ship => ship.Intersect(newShip).Any()));
        shipPositions.Add(newShip);
      }
      return shipPositions;
    }

    private List<string> GenerateShip(int length)
    {
      var coordinates = NextCoordinates();
      return NextOrientation() ?
        ShipFactory.Vertical((coordinates.column, coordinates.row % (BoardSize - (length - 1))), length) :
        ShipFactory.Horizontal((coordinates.column % (BoardSize - (length - 1)), coordinates.row), length);
    }
  }
}
