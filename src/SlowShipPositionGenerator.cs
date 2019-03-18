using System;
using System.Collections.Generic;
using System.Collections.Immutable;
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

    public List<List<string>> Generate(IEnumerable<int> shipLengths)
    {
      var distinctLengths = shipLengths.OrderByDescending(k => k).GroupBy(l => l);
      var result = new List<List<string>>();
      foreach (var lengthGroup in distinctLengths)
      {
        int length = lengthGroup.Key;
        var horizontalShips = from row in Enumerable.Range(0, boardSize)
                              from column in Enumerable.Range(0, boardSize - (length - 1))
                              select ShipFactory.Horizontal((column: column, row: row), length);

        var verticalShips = from row in Enumerable.Range(0, boardSize - (length - 1))
                            from column in Enumerable.Range(0, boardSize)
                            select ShipFactory.Vertical((column: column, row: row), length);

        var allShipPositions = horizontalShips.Concat(verticalShips);
        var allPossiblePositions = allShipPositions
          .Where(possiblePosition => !result.Any(ship => ship.Any(possiblePosition.Contains)))
          .ToArray();

        foreach (var _ in lengthGroup)
        {
          var randomPickShip = Pick(allPossiblePositions);
          result.Add(randomPickShip);

          allPossiblePositions = allPossiblePositions
            .Where(place => !place.Any(randomPickShip.Contains))
            .ToArray();
        }
      }
      return result;
    }

    private static List<string> Pick(List<string>[] allPossiblePositions)
    {
      if (allPossiblePositions.Length == 0)
      {
        throw new Exception("No remaining ship positions");
      }

      int index = rng.Next(0, allPossiblePositions.Length);
      var randomPickShip = allPossiblePositions[index];
      return randomPickShip;
    }
  }
}
