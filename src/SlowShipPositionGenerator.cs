using System;
using System.Collections.Generic;
using System.Linq;

namespace Battleships19
{
  // An experiment in an alternative algorithm for ship placement.
  // Will provide a more compact placement of ships
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
      var shipPositions = new List<List<string>>();
      foreach (var shipLengthGroup in distinctLengths)
      {
        int length = shipLengthGroup.Key;
        var allPossiblePositions = GetAllPossibleShipPositions(length, shipPositions);

        foreach (var _ in shipLengthGroup)
        {
          var randomPickShip = Pick(allPossiblePositions);
          shipPositions.Add(randomPickShip);

          // remove from possible positions
          allPossiblePositions = allPossiblePositions
            .Where(place => !place.Any(randomPickShip.Contains))
            .ToArray();
        }
      }
      return shipPositions;
    }

    private List<string>[] GetAllPossibleShipPositions(int length, List<List<string>> shipPositions)
    {
      // calculate all possible positions for an empty board
      var horizontalShips = from row in Enumerable.Range(0, boardSize)
        from column in Enumerable.Range(0, boardSize - (length - 1))
        select ShipFactory.Horizontal((column: column, row: row), length);

      var verticalShips = from row in Enumerable.Range(0, boardSize - (length - 1))
        from column in Enumerable.Range(0, boardSize)
        select ShipFactory.Vertical((column: column, row: row), length);

      var allShipPositions = horizontalShips.Concat(verticalShips);
      
      // remove any interesting with existing ships
      return allShipPositions
        .Where(possiblePosition => !shipPositions.Any(ship => ship.Any(possiblePosition.Contains)))
        .ToArray();
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
