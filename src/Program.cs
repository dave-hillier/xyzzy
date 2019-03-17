using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Battleships19.Tests")]

namespace Battleships19
{
  class Program
  {
    static void Main(string[] args)
    {
      var positionGenerator = new RandomShipPositionGenerator(10);
      var shipLengths = new List<int> { 5, 4, 4 };
      var shipPositions = positionGenerator.Generate(shipLengths);
      var game = new Game(shipPositions);

      game.Start(Console.In, Console.Out);
    }
  }
}
