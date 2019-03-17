using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Battleships19
{
  class Grid
  {
    public static void Write(List<HashSet<string>> shipPositions, int gridSize)
    {
      WriteShipPositions(shipPositions);

      var headers = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
      Console.WriteLine($"  |{string.Join("|", headers.Take(gridSize).Select(c => c.ToString().PadLeft(2)))}|");

      var shipIndexes = CellIndexes(shipPositions);

      for (var row = 1; row <= gridSize; ++row)
      {
        var cells = Enumerable.Range(0, gridSize).Select(c =>
        {
          var cell = $"{headers[c]}{row}";
          if (shipIndexes.ContainsKey(cell))
            return shipIndexes[cell].ToString().PadLeft(2);
          return "  ";
        });

        Console.WriteLine($"{row.ToString().PadLeft(2)}|{string.Join("|", cells)}|");
      }

      Console.WriteLine();
    }

    private static Dictionary<string, int> CellIndexes(List<HashSet<string>> shipPositions)
    {
      var shipIndex = shipPositions.Select((ship, index) => new { ship, index });
      var dict = (from cells in shipIndex
                  from cell in cells.ship
                  select new { cell, cells.index }).ToDictionary(kv => kv.cell, kv => kv.index);
      return dict;
    }

    private static void WriteShipPositions(List<HashSet<string>> shipPositions)
    {
      foreach (var ship in shipPositions)
      {
        Console.WriteLine(string.Join(",", ship));
      }
      Console.WriteLine();
    }
  }
}
