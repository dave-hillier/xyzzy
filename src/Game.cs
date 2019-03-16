using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Battleships19.Battleships19.Tests")]

namespace Battleships19
{
  public class Game
  {
    public const int BoardSize = 10;
    public static void Start(TextReader @in, TextWriter @out)
    {
      var shotsTaken = new HashSet<string>();
      var shipPositions = new List<HashSet<string>> {
        new HashSet<string> { "A1" },
        new HashSet<string> { "B1" },
        new HashSet<string> { "C1" },
      };

      @out.WriteLine("Enter coordinates: ");
      var input = @in.ReadLine();
      while (input != null)
      {
        bool valid = Coordinates.TryParse(input);
        if (!valid)
          @out.WriteLine("ERROR: Invalid Coordinates");
        else
        {
          if (shotsTaken.Contains(input))
          {
            @out.WriteLine("ERROR: Already taken");
          }
          else
          {
            shotsTaken.Add(input);
            foreach (var ship in shipPositions)
            {
              ProcessShotOnShip(@out, input, ship);
            }
            if (AllSunk(shipPositions))
            {
              @out.WriteLine("WIN");
              break;
            }
          }
        }

        @out.WriteLine("Enter coordinates: ");
        input = @in.ReadLine();
      }
    }

    private static void ProcessShotOnShip(TextWriter @out, string input, HashSet<string> ship)
    {
      var hit = ship.Remove(input.ToUpper());
      if (hit)
      {
        @out.WriteLine("HIT");
        if (ship.Count() == 0)
        {
          @out.WriteLine("SINK");
        }
      }
    }

    private static bool AllSunk(List<HashSet<string>> shipPositions)
    {
      return shipPositions.All(cell => cell.Count == 0);
    }
  }
}
