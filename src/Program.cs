using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

[assembly: InternalsVisibleTo("Battleships19.Battleships19.Tests")]

namespace Battleships19
{
  class Program
  {
    static void Main(string[] args)
    {
      Game.Start(Console.Out, Console.In);
    }
  }

  public class Game
  {
    const int BoardSize = 10;

    public static void Start(TextWriter @out, TextReader @in)
    {
      @out.WriteLine("Enter a coordinate to shoot: ");
      var input = @in.ReadLine();

      bool valid = TryParse(input);
      if (!valid)
        @out.WriteLine("ERROR: Invalid Coordinates");
    }

    private static bool TryParse(string input)
    {
      var rx = new Regex(@"([a-z]+)([0-9]+)", RegexOptions.Compiled | RegexOptions.IgnoreCase);
      var match = rx.Match(input);

      if (!match.Success || match.Groups.Count < 3)
      {
        return false;
      }

      var alpha = match.Groups[1].Value.ToLowerInvariant();
      if (alpha.Length > 1)
      {
        return false;
      }

      var column = (int)alpha[0] - 'a';
      if (column < 0 || column >= BoardSize)
      {
        return false;
      }

      var row = int.Parse(match.Groups[2].Value) - 1;
      if (row < 0 || row >= BoardSize)
      {
        return false;
      }

      return true;
    }
  }
}
