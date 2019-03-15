using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

[assembly: InternalsVisibleTo("Battleships19.Battleships19.Tests")]

namespace Battleships19
{
  public class Coordinates
  {
    public static bool TryParse(string input)
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
      if (column < 0 || column >= Game.BoardSize)
      {
        return false;
      }

      var row = int.Parse(match.Groups[2].Value) - 1;
      if (row < 0 || row >= Game.BoardSize)
      {
        return false;
      }

      return true;
    }
  }
}
