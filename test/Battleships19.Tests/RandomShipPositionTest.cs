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
    [Fact]
    public void Place_single_ship_horizontal()
    {
      var orientation = new List<bool> { true };
      var coordinates = new List<(int, int)> { (0, 0), (1, 0), (2, 0) };
      var length = new List<int> { 5 };

      Setup(orientation, coordinates, length);

      var positions = RandomShipPosition.Generate();

      Assert.Equal(new[] { "A1", "A2", "A3", "A4", "A5" }, positions.First());
    }


    [Fact]
    public void Place_single_ship_vertical()
    {
      var orientation = new List<bool> { false };
      var coordinates = new List<(int, int)> { (0, 0), (1, 0), (2, 0) };
      Setup(orientation, coordinates, new List<int> { 5 });

      var positions = RandomShipPosition.Generate();

      Assert.Equal(new[] { "A1", "B1", "C1", "D1", "E1" }, positions.First());
    }

    [Fact]
    public void Two_ships()
    {
      var orientation = new List<bool> { true, true, true };
      var coordinates = new List<(int, int)> { (0, 0), (1, 0), (2, 0) };
      Setup(orientation, coordinates, new List<int> { 5, 4 });

      var positions = RandomShipPosition.Generate();
      Assert.Equal(new[] { "A1", "A2", "A3", "A4", "A5" }, positions[0]);
      Assert.Equal(new[] { "B1", "B2", "B3", "B4" }, positions[1]);
    }

    [Fact]
    public void Overlap()
    {
      var overlapStartPositions = new List<(int, int)> { (0, 0), (0, 0), (1, 0) };
      var coordinates = new List<bool> { true, true, true };
      Setup(coordinates, overlapStartPositions, new List<int> { 5, 4 });

      var positions = RandomShipPosition.Generate();

      Assert.Equal(new[] { "A1", "A2", "A3", "A4", "A5" }, positions[0]);
      Assert.Equal(new[] { "B1", "B2", "B3", "B4" }, positions[1]);
    }

    [Fact]
    public void Off_grid()
    {
      var offGridStartPositions = new List<(int, int)> { (9, 9), (0, 0) };
      var orientation = new List<bool> { true, true, true };
      Setup(orientation, offGridStartPositions, new List<int> { 4 });

      var positions = RandomShipPosition.Generate();

      Assert.Equal(new[] { "J4", "J5", "J6", "J7" }, positions[0]);
    }

    [Fact]
    public void At_Edge()
    {
      var coordinates = new List<(int, int)> { (9, 0), (0, 0) };
      var orientation = new List<bool> { true, true, true };
      Setup(orientation, coordinates, new List<int> { 4 });
      var positions = RandomShipPosition.Generate();

      Assert.Equal(new[] { "J1", "J2", "J3", "J4" }, positions[0]);
    }

    private void Setup(List<bool> orientation, List<(int, int)> coordinates, List<int> list)
    {
      RandomShipPosition.NextOrientation = () => PopFirst(orientation);
      RandomShipPosition.NextCoordinates = () => PopFirst(coordinates);
      RandomShipPosition.Lengths = list;
    }

    private T PopFirst<T>(List<T> list)
    {
      var result = list.First();
      list.RemoveAt(0);
      return result;
    }
  }
}
