using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptNET.ZNotation
{
    public static class Constants
    {
        public static Undefined Undefined = Undefined.Value;
    }

    public class Undefined
    {
        private Undefined() { }

        public static Undefined Value = new Undefined ();

        public override string ToString()
        {
            return "Undefined";
        }
    }
}
