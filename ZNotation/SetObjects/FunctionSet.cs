using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptNET.ZNotation
{
  public class FunctionSet : Set
  {
    Predicate<object> generativeFunction = x => false;

    public FunctionSet()
    {
    }

    public FunctionSet(Predicate<object> generativeFunction)
    {
      this.generativeFunction = generativeFunction;
    }

    public override bool Contains(object item)
    {
      bool result = false;
      try
      {
        result = generativeFunction(item);
      }
      catch
      {
        result = false;
      }

      return result;
    }
  }
}
