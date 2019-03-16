using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Battleships19.Battleships19.Tests")]

namespace Battleships19
{
  enum ShotResult
  {
    Miss,
    Hit,
    Sink
  }

  public class Game
  {
    public const int BoardSize = 10;
    public static void Start(TextReader @in, TextWriter @out)
    {
      var shotsTaken = new HashSet<string>();
      List<HashSet<string>> shipPositions = FixedShipPositions.Generate();

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
            var result = TakeShot(shipPositions, input);
            @out.WriteLine(result.ToString().ToUpper());

            if (shipPositions.All(cell => cell.Count == 0))
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

    private static ShotResult TakeShot(List<HashSet<string>> shipPositions, string input)
    {
      var results = shipPositions.Select(ship => ProcessShot(input, ship));
      return results.FirstOrDefault(shot => shot != ShotResult.Miss);
    }

    private static ShotResult ProcessShot(string input, HashSet<string> ship)
    {
      var hit = ship.Remove(input);
      return hit ? (ship.Count() == 0 ? ShotResult.Sink : ShotResult.Hit) : ShotResult.Miss;
    }
  }
}
