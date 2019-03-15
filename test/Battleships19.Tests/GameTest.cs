using System;
using System.IO;
using Xunit;

namespace Battleships19.Tests
{
  public class GameTest
  {
    [Theory]
    [InlineData("a99", false)]
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

      var lines = output.ToString().Split("\n");

      if (isValid)
        Assert.DoesNotContain("ERROR", lines[1]);
      else
        Assert.StartsWith("ERROR", lines[1]);
    }
  }
}
