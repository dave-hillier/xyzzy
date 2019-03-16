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
    private List<bool> orientations = new List<bool> { true, true, true };
    private List<(int, int)> startPositions = new List<(int, int)> { (0, 0), (1, 0), (2, 0) };

    [Fact]
    public void Place_single_ship_horizontal()
    {
      RandomShipPosition.NextOrientation = () => PopFirst(new List<bool> { true });
      RandomShipPosition.NextCoordinates = () => PopFirst(startPositions);
      RandomShipPosition.Lengths = new List<int> { 5 };
      var positions = RandomShipPosition.Generate();
      Assert.Equal(new[] { "A1", "A2", "A3", "A4", "A5" }, positions.First());
    }

    [Fact]
    public void Place_single_ship_vertical()
    {
      RandomShipPosition.NextOrientation = () => PopFirst(new List<bool> { false });
      RandomShipPosition.NextCoordinates = () => PopFirst(startPositions);
      RandomShipPosition.Lengths = new List<int> { 5 };
      var positions = RandomShipPosition.Generate();
      Assert.Equal(new[] { "A1", "B1", "C1", "D1", "E1" }, positions.First());
    }

    [Fact]
    public void Two_ships()
    {
      RandomShipPosition.NextOrientation = () => PopFirst(orientations);
      RandomShipPosition.NextCoordinates = () => PopFirst(startPositions);
      RandomShipPosition.Lengths = new List<int> { 5, 4 };
      var positions = RandomShipPosition.Generate();
      Assert.Equal(new[] { "A1", "A2", "A3", "A4", "A5" }, positions[0]);
      Assert.Equal(new[] { "B1", "B2", "B3", "B4" }, positions[1]);
    }

    [Fact]
    public void Overlap()
    {
      var overlapStartPositions = new List<(int, int)> { (0, 0), (0, 0), (1, 0) };
      RandomShipPosition.NextOrientation = () => PopFirst(orientations);
      RandomShipPosition.NextCoordinates = () => PopFirst(overlapStartPositions);
      RandomShipPosition.Lengths = new List<int> { 5, 4 };
      var positions = RandomShipPosition.Generate();
      Assert.Equal(new[] { "A1", "A2", "A3", "A4", "A5" }, positions[0]);
      Assert.Equal(new[] { "B1", "B2", "B3", "B4" }, positions[1]);
    }

    private T PopFirst<T>(List<T> list)
    {
      var result = list.First();
      list.RemoveAt(0);
      return result;
    }
  }
}
