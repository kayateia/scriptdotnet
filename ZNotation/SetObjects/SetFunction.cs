using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptNET.ZNotation
{
  /// <summary>
  /// Base function
  /// </summary>
  public class SetFunction : SetRelation, Function
  {
    public SetFunction()
      : base()
    {
    }

    public SetFunction(int cardinality)
      : base(cardinality)
    {
    }

    public object Evaluate(params object[] args)
    {
      if (args.Length != Cardinality - 1)
        throw new WrongCardinalityException();

      Tuple[] tuples = items.ToArray();
      foreach (Tuple tuple in tuples)
      {
        bool found = true;
        for (int i = 0; i < Cardinality - 1; i++)
          if ((tuple[i] == null && args[i] != null) ||
              (tuple[i] != null && !tuple[i].Equals(args[i])))
          {
            found = false;
            break;
          }

        if (found) return tuple[Cardinality - 1];
      }

      return Constants.Undefined;
    }
  }
}
