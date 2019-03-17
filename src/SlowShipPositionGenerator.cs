using System;
using System.Collections.Generic;
using System.Linq;

namespace Battleships19
{
  class SlowShipPositionGenerator
  {
    private readonly int boardSize;
    private static Random rng = new Random();

    public SlowShipPositionGenerator(int boardSize)
    {
      this.boardSize = boardSize;
    }

    public List<HashSet<string>> Generate(List<int> shipLengths)
    {
      shipLengths.Sort();
      shipLengths.Reverse();

      var distinctLengths = shipLengths.GroupBy(l => l);
      var result = new List<HashSet<string>>();
      foreach (var length in distinctLengths)
      {
        var allPossiblePositions = new List<HashSet<string>>();
        foreach (var ship in result)
        {
          allPossiblePositions = allPossiblePositions.Where(place => !place.Intersect(ship).Any()).ToList();
        }

        for (int row = 0; row < boardSize; ++row)
        {
          for (int column = 0; column < boardSize; ++column)
          {
            if (column < boardSize - length.Key - 1)
            {
              var horizontalShip = ShipFactory.Horizontal((column, row), length.Key);
              if (result.All(place => !place.Intersect(horizontalShip).Any()))
                allPossiblePositions.Add(horizontalShip);
            }

            if (row < boardSize - length.Key - 1)
            {
              var verticalShip = ShipFactory.Vertical((column, row), length.Key);
              if (result.All(place => !place.Intersect(verticalShip).Any()))
                allPossiblePositions.Add(verticalShip);
            }
          }
        }

        foreach (var _ in length)
        {
          int index = rng.Next(0, allPossiblePositions.Count); // TODO: run out of picks?
          var randomPick = allPossiblePositions[index];
          result.Add(randomPick);
          allPossiblePositions = allPossiblePositions.Where(place => !place.Intersect(randomPick).Any()).ToList();
        }
      }

      return result;
    }
  }
}
