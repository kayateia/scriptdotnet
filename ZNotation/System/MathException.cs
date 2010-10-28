using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptNET.ZNotation
{
    public class MathException : Exception
    {
        public MathException()
        {
        }

        public MathException(string message):base(message)
        {
        }
    }

    public class WrongCardinalityException : MathException
    {

        public WrongCardinalityException()
            : base("Wrong Cardinality")
        {
        }
    }
}
