using System;
using System.Collections.Generic;
using System.Diagnostics;
using Xunit;
using System.Linq;

namespace Battleships19.Tests
{
  public class BenchmarkTests
  {
    const int BoardSize = 26;
    const int ShipCount = 100;

    [Fact]
    public void TimeSlow()
    {
      var shipLengths = Enumerable.Range(0, ShipCount).Select(_ => 5).ToList();
      var positionGenerator = new SlowShipPositionGenerator(BoardSize);

      TimeGenerate(shipLengths, () => positionGenerator.Generate(shipLengths));
      Console.WriteLine("Slow");
    }

    [Fact]
    public void TestFill()
    {
      var shipLengths = new List<int> { 5, 4, 4, 4, 3, 2 };
      var positionGenerator = new SlowShipPositionGenerator(5);
      var positions = positionGenerator.Generate(shipLengths);
      Grid.Write(positions, 5);
    }

    [Fact]
    public void TestFillRandom()
    {
      var shipLengths = new List<int> { 5, 4, 4, 4, 3, 2 };
      var positionGenerator = new RandomShipPositionGenerator(5);
      var positions = positionGenerator.Generate(shipLengths);
      Grid.Write(positions, 5);
    }


    private static void TimeGenerate(List<int> shipLengths, Func<List<List<string>>> positionGenerator)
    {
      var stopwatch = new Stopwatch();
      stopwatch.Start();
      for (int i = 0; i < 20; ++i)
        positionGenerator();

      var position = positionGenerator();
      stopwatch.Stop();
      Grid.Write(position, BoardSize);
      Console.WriteLine($"{stopwatch.ElapsedMilliseconds} *** ");
    }

    [Fact]
    public void TimeRandom()
    {
      var positionGenerator = new RandomShipPositionGenerator(BoardSize);
      var shipLengths = Enumerable.Range(0, ShipCount).Select(_ => 5).ToList();

      TimeGenerate(shipLengths, () => positionGenerator.Generate(shipLengths));
      Console.WriteLine("Random");
    }
  }
}
