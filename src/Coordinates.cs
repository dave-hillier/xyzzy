using System.Text.RegularExpressions;

namespace Battleships19
{
  public static class Coordinates
  {
    // Test the input for validity. Valid Battleships coordinates in the current board return true 
    public static bool IsValid(string input, int boardSize)
    {
      var rx = new Regex(@"([a-z]+)([0-9]+)", RegexOptions.Compiled | RegexOptions.IgnoreCase);
      var match = rx.Match(input);

      if (!match.Success || match.Groups.Count < 3)
        return false;

      var alpha = match.Groups[1].Value.ToLowerInvariant();
      var column = Columns.Index(alpha);
      var row = decimal.Parse(match.Groups[2].Value) - 1;

      return column >= 0 && column < boardSize &&
        row >= 0 && row < boardSize;
    }

    // Convert from column, row tuple to a Battleships formatted coordinate
    public static string ToCoordinateString(this (int column, int row) tuple)
    {
      var (column, row) = tuple;
      return $"{Columns.Name(column)}{1 + row}";
    }
  }
}
