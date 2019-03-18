using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Xunit;

namespace Battleships19.Tests
{
  public class GameTest
  {
    private List<List<string>> positions = new FixedShipPositionGenerator().Generate(new List<int> { 4, 4, 5 });

    [Theory]
    [InlineData("", false)]
    [InlineData("a99", false)]
    [InlineData("a999999999999999999", false)]
    [InlineData("aa99", false)]
    [InlineData("z1", false)]
    [InlineData("a0", false)]
    [InlineData("oo", false)]
    [InlineData("00", false)]
    [InlineData("aa", false)]
    [InlineData("a1", true)]
    [InlineData("A1", true)]
    [InlineData("J1", true)]
    [InlineData("C10", true)]
    [InlineData("J10", true)]
    public void Coordinate_validation(string coords, bool isValid)
    {
      var game = new Game(positions, 10);

      var lines = RunGame(game, $"{coords}\n");

      if (isValid)
        Assert.DoesNotContain("ERROR", lines[0]);
      else
        Assert.StartsWith("ERROR", lines[0]);
    }

    [Fact]
    public void Can_take_multiple_failing_shots()
    {
      var game = new Game(positions, 10);

      var lines = RunGame(game, $"Z1\nZ2\n");

      Assert.StartsWith("ERROR", lines[0]);
      Assert.StartsWith("ERROR", lines[1]);
    }

    [Fact]
    public void Same_coordiates()
    {
      var game = new Game(positions, 10);

      var lines = RunGame(game, $"A1\nA1\n");

      Assert.StartsWith("ERROR", lines[1]);
    }

    [Fact]
    public void Shoot_all_cells()
    {
      RunFullGame(positions);
    }

    [Fact]
    public void Random_ship_positions()
    {
      var positionGenerator = new RandomShipPositionGenerator(10);
      RunFullGame(positionGenerator.Generate(new List<int> { 5, 4, 4 }));
    }

    [Fact]
    public void Slow_ship_positions()
    {
      var positionGenerator = new SlowShipPositionGenerator(10);
      RunFullGame(positionGenerator.Generate(new List<int> { 5, 4, 4 }));
    }

    private void RunFullGame(List<List<string>> positions)
    {
      var game = new Game(positions, 10);
      var input = GenerateAllCoordinates();

      var lines = RunGame(game, input.ToString());

      foreach (var line in lines)
      {
        Assert.DoesNotContain("ERROR", line);
      }

      Assert.Equal(3 + 3 + 4, lines.Count(l => l.Contains("HIT")));
      Assert.Equal(3, lines.Count(l => l.Contains("SINK")));

      Assert.Contains("WIN", lines[lines.Count() - 2]);
    }

    private static string GenerateAllCoordinates()
    {
      var columns = "ABCDEFGHIJ";
      var rows = 10;

      var input = new StringBuilder();
      foreach (var column in columns)
      {
        for (var row = 1; row <= rows; ++row)
        {
          input.Append($"{column}{row}\n");
        }
      }
      return input.ToString();
    }

    private static string[] RunGame(Game game, string stringInput)
    {
      var output = new StringWriter();
      var input = new StringReader(stringInput);


      game.Start(input, output);
      return ToLines(output).Where(line => !line.Contains("Enter")).ToArray();
    }
    private static string[] ToLines(StringWriter output)
    {
      return output.ToString().Split("\n");
    }
  }
}
