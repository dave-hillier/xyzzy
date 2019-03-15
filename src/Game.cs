using System.IO;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Battleships19.Battleships19.Tests")]

namespace Battleships19
{
  public class Game
  {
    public const int BoardSize = 10;

    public static void Start(TextReader @in, TextWriter @out)
    {
      @out.WriteLine("Enter a coordinate to shoot: ");
      var input = @in.ReadLine();

      bool valid = Coordinates.TryParse(input);
      if (!valid)
        @out.WriteLine("ERROR: Invalid Coordinates");
    }


  }
}
