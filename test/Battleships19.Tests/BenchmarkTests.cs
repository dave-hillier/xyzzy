using System;
using System.Collections.Generic;
using System.Diagnostics;
using Xunit;
using System.Linq;
using Xunit.Abstractions;

namespace Battleships19.Tests
{
  // Note that these are not real tests, just using a test runner to give a way to run experiments/microbenchmarks 
  public class BenchmarkTests
  {
    private readonly ITestOutputHelper _testOutputHelper;

    public BenchmarkTests(ITestOutputHelper testOutputHelper)
    {
      _testOutputHelper = testOutputHelper;
    }

    const int BoardSize = 30;
    const int ShipCount = 100;

    [Fact]
    public void TimeSlow()
    {
      var shipLengths = Enumerable.Range(0, ShipCount).Select(_ => 5).ToList();
      var positionGenerator = new SlowShipPositionGenerator(BoardSize);

      TimeGenerate(shipLengths, () => positionGenerator.Generate(shipLengths));
      _testOutputHelper.WriteLine("Slow");
    }

    [Fact]
    public void TestFill()
    {
      var shipLengths = new List<int> { 5, 4, 4, 4, 3, 2 };
      var positionGenerator = new SlowShipPositionGenerator(5);
      var positions = positionGenerator.Generate(shipLengths);
      DiagnosticHelper.WriteToConsole(positions, 5);
    }

    [Fact]
    public void TestFillRandom()
    {
      var shipLengths = new List<int> { 5, 4, 4, 4, 3, 2 };
      var positionGenerator = new RandomShipPositionGenerator(5);
      var positions = positionGenerator.Generate(shipLengths);
      DiagnosticHelper.WriteToConsole(positions, 5);
    }


    private static void TimeGenerate(List<int> shipLengths, Func<List<List<string>>> positionGenerator)
    {
      var stopwatch = new Stopwatch();
      stopwatch.Start();
      for (int i = 0; i < 20; ++i)
        positionGenerator();

      var position = positionGenerator();
      stopwatch.Stop();
      DiagnosticHelper.WriteToConsole(position, BoardSize);
      Console.WriteLine($"{stopwatch.ElapsedMilliseconds} *** ");
    }

    [Fact]
    public void TimeRandom()
    {
      var positionGenerator = new RandomShipPositionGenerator(BoardSize);
      var shipLengths = Enumerable.Range(0, ShipCount).Select(_ => 5).ToList();

      TimeGenerate(shipLengths, () => positionGenerator.Generate(shipLengths));
      _testOutputHelper.WriteLine("Random");
    }
  }
}
