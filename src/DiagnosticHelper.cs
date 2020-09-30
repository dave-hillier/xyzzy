using System;
using System.Collections.Generic;
using System.Linq;

namespace Battleships19
{
  // Untested as it's not required for the spec, but it makes things easier to test when playing
  internal static class DiagnosticHelper
  {
    public static void WriteToConsole(List<List<string>> shipPositions, int gridSize)
    {
      WriteShipPositionsToConsole(shipPositions);
      WriteGrid(shipPositions, gridSize);
    }

    private static void WriteGrid(List<List<string>> shipPositions, int gridSize)
    {
      var headers = Columns.Get(gridSize).ToArray();

      var old = Console.ForegroundColor;
      Console.ForegroundColor = ConsoleColor.Green;
      Console.WriteLine($"  |{string.Join("|", headers.Select(c => c.ToString().PadLeft(2)))}|");
      Console.ForegroundColor = old;

      var shipIndexes = CellIndexes(shipPositions);

      for (var row = 1; row <= gridSize; ++row)
      {
        var r = row;
        var cells = Enumerable.Range(0, gridSize).Select(c =>
        {
          var cell = $"{headers[c]}{r}";
          return shipIndexes.ContainsKey(cell) ? shipIndexes[cell].ToString().PadLeft(2) : "  ";
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
      return (from cells in shipIndex
        from cell in cells.ship
        select new { cell, cells.index }).ToDictionary(kv => kv.cell, kv => kv.index);
    }

    private static void WriteShipPositionsToConsole(List<List<string>> shipPositions)
    {
      foreach (var ship in shipPositions)
      {
        Console.WriteLine(string.Join(",", ship));
      }
      Console.WriteLine();
    }
  }
}
