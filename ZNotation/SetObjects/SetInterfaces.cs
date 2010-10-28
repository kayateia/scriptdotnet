using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptNET.ZNotation
{
  public abstract class Set
  {
    public abstract bool Contains(object item);
  }

  public abstract class ModifiableSet : Set
  {
    public abstract void Add(object item);
    public abstract void Remove(object item);
  }
}
