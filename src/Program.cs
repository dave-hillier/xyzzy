using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Battleships19.Tests")]

namespace Battleships19
{
  class Program
  {
    private const int BoardSize = 10;

    static void Main(string[] args)
    {
      var positionGenerator = new SlowShipPositionGenerator(BoardSize);
      var shipLengths = new List<int> { 5, 4, 4 };

      var shipPositions = positionGenerator.Generate(shipLengths);
      Grid.Write(shipPositions, BoardSize);

      var game = new Game(shipPositions, BoardSize);
      game.Start(Console.In, Console.Out);
    }
  }
}
