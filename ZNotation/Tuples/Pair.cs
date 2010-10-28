using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptNET.ZNotation
{

    public class Pair : Tuple
    {
        public Pair()
            : base(2)
        {
        }

        public Pair(object a, object b)
            : base(2)
        {
            this[0] = a;
            this[1] = b;
        }
    }
}
