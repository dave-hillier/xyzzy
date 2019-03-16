using System;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Battleships19.Battleships19.Tests")]

namespace Battleships19
{
  public class ShipPlacement
  {
    private static Random rng = new Random();
    public Func<string> NextOrientation = () => rng.Next(0, 2) == 1 ? "h" : "v";
    public Func<(int, int)> NextCoordinates = () => (rng.Next(0, Game.BoardSize), rng.Next(0, Game.BoardSize));

  }
}
