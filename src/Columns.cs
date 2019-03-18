using System;
using System.Collections.Generic;
using System.Linq;

namespace Battleships19
{
  static class Columns
  {
    public static IEnumerable<string> Get(int size) => Enumerable.Range(0, size).Select(Name).ToArray();
    private const string Alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    private static string[] All = Generate();

    static string[] Generate()
    {
      var v = Alphabet.Select(s => s.ToString()).ToArray();
      return v.Concat(from x in v
                      from y in v
                      select x + y).ToArray();
    }
    public static int Index(string column) => Array.IndexOf(All, column.ToUpper());
    public static string Name(int num)
    {
      return All[num];
    }
  }
}
