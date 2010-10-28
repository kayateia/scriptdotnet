using System;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ScriptIDE_CF
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [MTAThread]
        static void Main()
        {
            ScriptNET.Runtime.RuntimeHost.Initialize();
            Application.Run(new ScriptIDE());
        }
    }
}