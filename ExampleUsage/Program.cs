using System;

using ScriptNET;
using ScriptNET.Runtime;

namespace ExampleUsage
{
  class Program
  {   
    //[FileIOPermission(SecurityAction.Deny, ViewAndModify="c:\\")]
    static void Main(string[] args)
    {
      RuntimeHost.Initialize();

      Script.RunCode(
      @" 
         About();
         Console.WriteLine('Sorting an array with Script.NET');
         a = [17,-2, 0,-3, 5, 3,1, 2, 55];

         for (i=0; i < a.Length; i=i+1)
          for (j=i+1; j <  a.Length; j=j+1)
           if (a[i] > a[j] )
           {
             temp = a[i]; 
             a[i] = a[j];
             a[j] = temp;
           }

          s = 'Results:';
          for (i=0; i < a.Length; i++)
           if (i!=0)
             s = s + ',' + a[i];
           else
             s += a[i];
          Console.WriteLine(s);
          ");

      Console.ReadKey();
    }
    
  }
}
