using System;
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
    public int[] ShipSizes = new[] { 5, 4, 4 };
    private static Random rng = new Random();
    public Func<string> NextOrientation = () => rng.Next(0, 2) == 1 ? "h" : "v";
    public Func<(int, int)> NextCoordinates = () => (rng.Next(0, BoardSize), rng.Next(0, BoardSize));

    public static void Start(TextReader @in, TextWriter @out)
    {
      var shotsTaken = new HashSet<string>();
      var shipPositions = new List<HashSet<string>> {
        new HashSet<string> { "A1" },
        new HashSet<string> { "A2" },
        new HashSet<string> { "A3" },
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
            if (shipPositions.All(s => !s.Any()))
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
  }
}
