using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptNET.ZNotation
{
  internal class CompositeSet : Set
  {
    public Set A { get; private set; }
    public Set B { get; private set; }
    Func<Set, Set, object, bool> compositor;

    public CompositeSet(Set a, Set b, Func<Set, Set, object, bool> compositor)
    {
      A = a;
      B = b;

      if (compositor == null) throw new ArgumentNullException("compositor");
      this.compositor = compositor;
    }

    public override bool Contains(object item)
    {
      return compositor(A, B, item);
    }
  }
}
