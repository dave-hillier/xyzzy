﻿using System;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Battleships19.Tests")]

namespace Battleships19
{
  class Program
  {
    static void Main(string[] args)
    {
      Game.ShipPositions = RandomShipPosition.Generate();
      Game.Start(Console.In, Console.Out);
    }
  }
}
