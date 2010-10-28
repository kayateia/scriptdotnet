using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

using Irony.Compiler;
using ScriptNET;
using System.Diagnostics;

namespace RunTests
{
  class Program
  {
    static void PrintHelp()
    {
      Console.WriteLine("Usage:\n  RunTests.exe folderPath");      
    }

    static void Main(string[] args)
    {
      if (args.Length != 1)
      {
        PrintHelp();
        return;
      }

      string path = args[0];
      if (!Directory.Exists(path))
      {
        PrintHelp();
        return;
      }

      DateTime now = DateTime.Now;
      ScriptNET.Runtime.RuntimeHost.Initialize();
      Console.WriteLine("Compiler inialization:"+DateTime.Now.Subtract(now));

      DirectoryInfo di = new DirectoryInfo(path);
      FileInfo[] files = di.GetFiles("*.txt");

      Console.WriteLine("");
      Console.WriteLine("Iteration 1. Parsing Test:");
      int count = 0, errors = 0;
      Stopwatch timer = new Stopwatch();
      foreach (FileInfo fileInfo in files)
      {
        string code = File.ReadAllText(fileInfo.FullName);

        timer.Reset();
        timer.Start();
        Script result = Script.Compile(code);

        if (result != null)
        {
          Console.ForegroundColor = ConsoleColor.White;
          Console.Write(String.Format("{0,-3} : ",count));
          Console.ResetColor();
          Console.Write(String.Format("{0,-25}", fileInfo.Name.Substring(0,Math.Min(fileInfo.Name.Length, 25))));
          Console.ForegroundColor = ConsoleColor.Green;
          Console.Write(" Ok");
          Console.ForegroundColor = ConsoleColor.Yellow;
          Console.WriteLine("  [" + (double)timer.ElapsedMilliseconds / 1000 + "]");
          Console.ResetColor();
        }
        else
        {
          Console.ForegroundColor = ConsoleColor.White;
          Console.Write(String.Format("{0,-3} : ", count));
          Console.ResetColor();
          Console.Write(String.Format("{0,-25}", fileInfo.Name.Substring(0, Math.Min(fileInfo.Name.Length, 25))));
          Console.ForegroundColor = ConsoleColor.Red;
          Console.WriteLine(" False");
          Console.ResetColor();
          errors++;
        }
        count++;
      }

      Console.WriteLine("");
      Console.WriteLine("Iteration 2. Execution:");
      Console.WriteLine("");
      count = 0;  errors = 0;
      foreach (FileInfo fileInfo in files)
      {
        string code = File.ReadAllText(fileInfo.FullName);
        Script result = Script.Compile(code);

        if (result != null)
        {
          object resultVal;

          Console.ForegroundColor = ConsoleColor.White;
          Console.Write(String.Format("{0,-3} : ", count));
          Console.ResetColor();
          Console.Write(String.Format("{0,-25}", fileInfo.Name.Substring(0, Math.Min(fileInfo.Name.Length, 25))));
          Console.ForegroundColor = ConsoleColor.Green;
          Console.Write(" Ok");
          Console.ForegroundColor = ConsoleColor.Yellow;
          Console.ForegroundColor = ConsoleColor.Green;
          Console.WriteLine("");
          Console.WriteLine("Execution:");
          Console.ForegroundColor = ConsoleColor.White;
          try
          {
            resultVal = result.Execute();
          }
          catch (Exception e)
          {
            resultVal = string.Format("Exception:{0}", e.Message);
          }
          Console.ForegroundColor = ConsoleColor.Green;
          Console.WriteLine("Results:");
          Console.ForegroundColor = ConsoleColor.White;

          if (resultVal != null)
            Console.WriteLine(" [(" + resultVal.GetType().Name + ")" + resultVal.ToString() + "]");
          else
            Console.WriteLine(" [NULL]");
          Console.ResetColor();
        }
        else
        {
          Console.ForegroundColor = ConsoleColor.White;
          Console.Write(String.Format("{0,-3} : ", count));
          Console.ResetColor();
          Console.Write(String.Format("{0,-25}", fileInfo.Name.Substring(0, Math.Min(fileInfo.Name.Length, 25))));
          Console.ForegroundColor = ConsoleColor.Red;
          Console.WriteLine(" False");
          Console.ResetColor();
          errors++;
        }

        count++;
      }

      Console.ResetColor();
      Console.WriteLine("\nParsed " + count + " Errors " + errors);
    }
  }
}
