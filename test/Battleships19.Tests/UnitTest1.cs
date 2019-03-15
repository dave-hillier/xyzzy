using System;
using System.IO;
using Xunit;

namespace Battleships19.Tests
{
  public class GameTest
  {
    [Theory]
    [InlineData("a99")]
    [InlineData("z1")]
    [InlineData("a0")]
    [InlineData("oo")]
    [InlineData("aa")]
    public void Rejects_invalid_coordinates(string coords)
    {
      var writer = new StringWriter();
      Game.Start(writer, new StringReader($"{coords}\n"));

      var lines = writer.ToString().Split("\n");

      Assert.StartsWith("ERROR", lines[1]);
    }

    [Theory]
    [InlineData("a1")]
    [InlineData("A1")]
    [InlineData("J1")]
    [InlineData("J10")]
    public void Accepts_coordinates_on_grid(string coords)
    {
      var writer = new StringWriter();
      Game.Start(writer, new StringReader($"{coords}\n"));

      var lines = writer.ToString().Split("\n");

      Assert.DoesNotContain("ERROR", lines[1]);
    }
  }
}
