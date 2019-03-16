using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Battleships19.Battleships19.Tests")]

namespace Battleships19
{
  enum ShotResult
  {
    Hit,
    Miss,
    Sink
  }

  public class Game
  {
    public const int BoardSize = 10;
    public static void Start(TextReader @in, TextWriter @out)
    {
      var shotsTaken = new HashSet<string>();
      List<HashSet<string>> shipPositions = TestShipPositions.Generate();

      @out.WriteLine("Enter coordinates: ");
      var input = @in.ReadLine();
      while (input != null)
      {
        bool valid = Coordinates.TryParse(input);
        if (!valid)
          @out.WriteLine("ERROR: Invalid Coordinates");
        else
        {
          input = input.ToUpper();
          if (shotsTaken.Contains(input))
          {
            @out.WriteLine("ERROR: Already taken");
          }
          else
          {

            shotsTaken.Add(input);
            bool miss = true;
            foreach (var ship in shipPositions)
            {
              ShotResult result = ProcessShot(input, ship);
              if (result != ShotResult.Miss)
              {
                miss = false;
                @out.WriteLine(result.ToString().ToUpper());
              }
            }
            if (miss)
              @out.WriteLine("MISS");

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

    private static ShotResult ProcessShot(string input, HashSet<string> ship)
    {
      var hit = ship.Remove(input);
      return hit ? (ship.Count() == 0 ? ShotResult.Sink : ShotResult.Hit) : ShotResult.Miss;
    }

    private static bool AllSunk(List<HashSet<string>> shipPositions)
    {
      return shipPositions.All(cell => cell.Count == 0);
    }
  }
}
