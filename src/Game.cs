using System.Collections.Generic;
using System.IO;
using System.Linq;

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
    private int BoardSize { get; }
    private List<List<string>> ShipPositions { get; }

    public Game(List<List<string>> shipPositions, int boardSize)
    {
      ShipPositions = shipPositions;
      BoardSize = boardSize;
    }

    public void Start(TextReader @in, TextWriter @out)
    {
      var shotsTaken = new HashSet<string>();

      var input = ReadCoordinates(@in, @out);
      while (input != null) // HACK: Check for the end of test input. Allows exiting without having to run through the whole hame.
      {
        bool valid = Coordinates.IsValid(input, BoardSize);
        if (!valid)
        {
          @out.WriteLine("ERROR: Invalid Coordinates");
        }
        else
        {
          input = input.ToUpper();
          if (shotsTaken.Contains(input))
          {
            @out.WriteLine("ERROR: You've already targeted these coordinates");
          }
          else
          {
            shotsTaken.Add(input);
            var result = TakeShot(ShipPositions, input); // I don't like the impure nature of this call
            @out.WriteLine(result.ToString().ToUpper());

            if (ShipPositions.All(cell => !cell.Any()))
            {
              @out.WriteLine("WIN: All Ships Sunk!");
              break;
            }
          }
        }
        input = ReadCoordinates(@in, @out);
      }
    }

    private static string ReadCoordinates(TextReader @in, TextWriter @out)
    {
      @out.WriteLine("Enter coordinates: ");
      return @in.ReadLine();
    }

    private static ShotResult TakeShot(List<List<string>> shipPositions, string input)
    {
      // Assumes non-empty shipPositions
      var results = shipPositions.Select(ship => ResolveShot(input, ship));
      return results.FirstOrDefault(shot => shot != ShotResult.Miss);
    }

    private static ShotResult ResolveShot(string cell, List<string> ship)
    {
      return ship.Remove(cell) ?
        (!ship.Any() ? ShotResult.Sink : ShotResult.Hit) :
        ShotResult.Miss;
    }
  }
}
