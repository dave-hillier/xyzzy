using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace Battleships19
{
  public class Coordinates
  {
    // I called this TryParse as I assumed I'd end up needing the actual coordinates but 
    // I didnt and it seemed like a reasonable enough name - Validate?
    public static bool TryParse(string input, int boardSize)
    {
      var rx = new Regex(@"([a-z]+)([0-9]+)", RegexOptions.Compiled | RegexOptions.IgnoreCase);
      var match = rx.Match(input);

      if (!match.Success || match.Groups.Count < 3)
        return false;

      var alpha = match.Groups[1].Value.ToLowerInvariant();
      if (alpha.Length > 1) // Only single letter coordinates supported
        return false;

      var column = (int)alpha[0] - 'a';
      var row = decimal.Parse(match.Groups[2].Value) - 1;
      return column >= 0 && column < boardSize &&
        row >= 0 && row < boardSize;
    }
  }
}
