using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Battleships19
{
  class Grid
  {
    public static void Write(List<List<string>> shipPositions, int gridSize)
    {
      WriteShipPositions(shipPositions);

      var headers = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
      var old = Console.ForegroundColor;
      Console.ForegroundColor = ConsoleColor.Green;
      Console.WriteLine($"  |{string.Join("|", headers.Take(gridSize).Select(c => c.ToString().PadLeft(2)))}|");
      Console.ForegroundColor = old;

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

        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write($"{row.ToString().PadLeft(2)}|");
        Console.ForegroundColor = old;
        Console.WriteLine($"{string.Join("|", cells)}|");
      }

      Console.WriteLine();
    }

    private static Dictionary<string, int> CellIndexes(List<List<string>> shipPositions)
    {
      var shipIndex = shipPositions.Select((ship, index) => new { ship, index });
      var dict = (from cells in shipIndex
                  from cell in cells.ship
                  select new { cell, cells.index }).ToDictionary(kv => kv.cell, kv => kv.index);
      return dict;
    }

    private static void WriteShipPositions(List<List<string>> shipPositions)
    {
      foreach (var ship in shipPositions)
      {
        Console.WriteLine(string.Join(",", ship));
      }
      Console.WriteLine();
    }
  }
}
