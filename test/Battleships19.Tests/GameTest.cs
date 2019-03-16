using System;
using System.IO;
using System.Linq;
using System.Text;
using Xunit;

namespace Battleships19.Tests
{
  public class GameTest
  {
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
      var lines = RunGame($"{coords}\n");

      if (isValid)
        Assert.DoesNotContain("ERROR", lines[1]);
      else
        Assert.StartsWith("ERROR", lines[1]);
    }

    [Fact]
    public void Can_take_multiple_failing_shots()
    {
      var lines = RunGame($"Z1\nZ2\n");

      Assert.StartsWith("ERROR", lines[1]);
      Assert.StartsWith("ERROR", lines[3]);
    }

    [Fact]
    public void Same_coordiates()
    {
      var lines = RunGame($"A1\nA1\n");

      Assert.StartsWith("ERROR", lines[3]);
    }

    [Fact]
    public void Shoot_all_cells()
    {
      var input = GenerateAllCoordinates();
      var lines = RunGame(input.ToString());

      foreach (var line in lines)
      {
        Assert.DoesNotContain("ERROR", line);
      }

      Assert.Equal(3, lines.Count(l => l.Contains("SINK")));
      Assert.Equal(10, lines.Count(l => l.Contains("HIT")));
      Assert.Equal(30, lines.Count(l => l.Contains("MISS")));

      Assert.Contains("WIN", lines[lines.Count() - 2]);
    }

    private static string GenerateAllCoordinates()
    {
      var columns = "ABCDEFGHIJ";
      var rows = 10;

      var input = new StringBuilder();
      for (var row = 1; row < rows; ++row)
      {
        foreach (var column in columns)
        {
          input.Append($"{column}{row}\n");
        }
      }
      return input.ToString();
    }

    private static string[] RunGame(string stringInput)
    {
      var output = new StringWriter();
      var input = new StringReader(stringInput);
      Game.Start(input, output);
      return ToLines(output);
    }
    private static string[] ToLines(StringWriter output)
    {
      return output.ToString().Split("\n");
    }
  }
}
