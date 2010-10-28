using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptNET.ZNotation
{
    public class NamedData
    {
        Dictionary<string, object> values = new Dictionary<string, object>();

        public object this[string name]
        {
            get
            {
                if (values.Keys.Contains(name))
                    return values[name];
                else
                    return Constants.Undefined;
                        
            }
            set
            {
                if (values.Keys.Contains(name))
                    values[name] = value;
                else
                    values.Add(name, value);
            }
        }
    }
}
