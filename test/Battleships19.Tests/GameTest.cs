using System;
using System.IO;
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
      var output = new StringWriter();
      var input = new StringReader($"{coords}\n");
      Game.Start(input, output);

      string[] lines = ToLines(output);

      if (isValid)
        Assert.DoesNotContain("ERROR", lines[1]);
      else
        Assert.StartsWith("ERROR", lines[1]);
    }

    private static string[] ToLines(StringWriter output)
    {
      return output.ToString().Split("\n");
    }

    [Fact]
    public void Can_retry_shots()
    {
      var output = new StringWriter();
      var input = new StringReader($"Z1\nZ2\n");
      Game.Start(input, output);

      string[] lines = ToLines(output);

      Assert.StartsWith("ERROR", lines[1]);
      Assert.StartsWith("ERROR", lines[2]);
    }
  }
}
