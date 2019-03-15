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
      @out.WriteLine("Enter coordinates: ");
      var input = @in.ReadLine();
      while (input != null)
      {
        bool valid = Coordinates.TryParse(input);
        if (!valid)
          @out.WriteLine("ERROR: Invalid Coordinates");

        @out.WriteLine("Enter coordinates: ");
        input = @in.ReadLine();
      }
    }
  }
}
