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
          allPossiblePositions = RemoveTakenPositions(allPossiblePositions, ship);
        }

        for (int row = 0; row < boardSize; ++row)
        {
          for (int column = 0; column < boardSize - length.Key - 1; ++column)
          {
            var horizontalShip = ShipFactory.Horizontal((column, row), length.Key);
            PlaceShipIfSpace(result, allPossiblePositions, horizontalShip);
          }
        }

        for (int row = 0; row < boardSize - length.Key - 1; ++row)
        {
          for (int column = 0; column < boardSize; ++column)
          {
            var verticalShip = ShipFactory.Vertical((column, row), length.Key);
            PlaceShipIfSpace(result, allPossiblePositions, verticalShip);
          }
        }

        foreach (var _ in length)
        {
          if (allPossiblePositions.Count == 0)
          {
            throw new Exception("No remaining ship positions");
          }
          int index = rng.Next(0, allPossiblePositions.Count); // TODO: run out of picks?
          var randomPick = allPossiblePositions[index];
          result.Add(randomPick);
          allPossiblePositions = RemoveTakenPositions(allPossiblePositions, randomPick);
        }
      }
      return result;
    }

    private static List<HashSet<string>> RemoveTakenPositions(List<HashSet<string>> allPossiblePositions, HashSet<string> randomPick)
    {
      return allPossiblePositions.Where(place => !place.Intersect(randomPick).Any()).ToList();
    }

    private static void PlaceShipIfSpace(
      List<HashSet<string>> result,
      List<HashSet<string>> allPossiblePositions,
      HashSet<string> horizontalShip)
    {
      if (result.All(place => !place.Intersect(horizontalShip).Any()))
        allPossiblePositions.Add(horizontalShip);
    }
  }
}
