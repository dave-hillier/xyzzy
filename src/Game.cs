using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;

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
    public int BoardSize { get; set; }
    public List<List<string>> ShipPositions { get; }

    public Game(List<List<string>> shipPositions, int boardSize)
    {
      ShipPositions = shipPositions;
      BoardSize = boardSize;
    }

    private const string ErrorInvalidCoordinates = "ERROR: Invalid Coordinates";
    private const string ErrorAlreadyTargetted = "ERROR: You've already targetted these coordinates";
    private const string WinAllShipsSunk = "WIN: All Ships Sunk!";

    public void Start(TextReader @in, TextWriter @out)
    {
      var shotsTaken = new HashSet<string> { };

      var input = ReadCoordinates(@in, @out);
      while (input != null) // Returns null when test input has ended. 
      {
        bool valid = Coordinates.TryParse(input, BoardSize);
        if (!valid)
        {
          @out.WriteLine(ErrorInvalidCoordinates);
        }
        else
        {
          input = input.ToUpper();
          if (shotsTaken.Contains(input))
          {
            @out.WriteLine(ErrorAlreadyTargetted);
          }
          else
          {
            shotsTaken.Add(input);
            var result = TakeShot(ShipPositions, input);
            @out.WriteLine(result.ToString().ToUpper());

            if (ShipPositions.All(cell => !cell.Any()))
            {
              @out.WriteLine(WinAllShipsSunk);
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
      var results = shipPositions.Select(ship => ProcessShot(input, ship));
      return results.FirstOrDefault(shot => shot != ShotResult.Miss);
    }

    private static ShotResult ProcessShot(string input, List<string> ship)
    {
      return ship.Remove(input) ?
        (ship.Count() == 0 ? ShotResult.Sink : ShotResult.Hit) :
        ShotResult.Miss;
    }
  }
}
