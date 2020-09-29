using System;
using System.Collections.Generic;
using System.Linq;

namespace Battleships19
{
  internal static class Columns
  {
    private static string[] All = Generate();

    private static string[] Generate()
    {
      const string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
      var v = alphabet.Select(s => s.ToString()).ToArray();
      return v.Concat(from x in v
                      from y in v
                      select x + y).ToArray();
    }
    public static IEnumerable<string> Get(int size) => Enumerable.Range(0, size).Select(Name).ToArray();
    public static int Index(string column) => Array.IndexOf(All, column.ToUpper());
    public static string Name(int num)
    {
      return All[num];
    }
  }
}
