using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptNET.ZNotation
{
    interface Function
    {
        object Evaluate(params object[] args);
    }
}
