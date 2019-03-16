using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Xunit;

namespace Battleships19.Tests
{
  public class RandomShipPositionTest
  {
    private List<string> orientations = new List<string> { "h", "h", "h" };
    private List<(int, int)> startPositions = new List<(int, int)> { (0, 0), (1, 0), (2, 0) };

    [Fact]
    public void Place_single_ship_horizontal()
    {
      RandomShipPosition.NextOrientation = () => PopFirst(new List<string> { "h" });
      RandomShipPosition.NextCoordinates = () => PopFirst(startPositions);
      RandomShipPosition.Lengths = new List<int> { 5 };
      var positions = RandomShipPosition.Generate();
      Assert.Equal(new[] { "A1", "A2", "A3", "A4", "A5" }, positions.First());
    }

    [Fact]
    public void Place_single_ship_vertical()
    {
      RandomShipPosition.NextOrientation = () => PopFirst(new List<string> { "v" });
      RandomShipPosition.NextCoordinates = () => PopFirst(startPositions);
      RandomShipPosition.Lengths = new List<int> { 5 };
      var positions = RandomShipPosition.Generate();
      Assert.Equal(new[] { "A1", "B1", "C1", "D1", "E1" }, positions.First());
    }

    private T PopFirst<T>(List<T> list)
    {
      var result = list.First();
      list.RemoveAt(0);
      return result;
    }
  }
}
