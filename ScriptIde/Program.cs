using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ScriptIde
{
  static class Program
  {
    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
      ScriptNET.Runtime.RuntimeHost.Initialize();
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
      Application.Run(new Editor());
    }
  }
}
