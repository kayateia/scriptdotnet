using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptNET.ZNotation
{
  public static class StandardSets
  {
    public static Set Empty = new FunctionSet();

    public static Set Naturals = new FunctionSet(x => Integers.Contains(x) && Check<long>(x, false, y => y > 0));
    public static Set Integers = new FunctionSet(x => (x is int || x is long || x is byte));
    public static Set Reals = new FunctionSet(x => Integers.Contains(x) || x is double || x is float);

    private static bool Check<T>(object item, bool defaultValue, Predicate<T> checker)
    {
      try
      {
        T value = (T)Convert.ChangeType(item, typeof(T));
        return checker(value);
      }
      catch
      {
        return defaultValue;
      }
    }
  }
}
