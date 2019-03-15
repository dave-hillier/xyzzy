using System;
using System.IO;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Battleships19.Battleships19.Tests")]

namespace Battleships19
{
  class Program
  {
    static void Main(string[] args)
    {
      Game.Start(Console.Out, Console.In);
    }




  }

  public class Game
  {
    public static void Start(TextWriter @out, TextReader @in)
    {
      @out.WriteLine("Enter a coordinate to shoot: ");
      var x = @in.ReadLine();
      @out.WriteLine("ERROR: Invalid Coordinates");
    }
  }
}
