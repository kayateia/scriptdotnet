using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using ScriptNET;

namespace ScriptIde_SL
{
  public partial class Page : UserControl
  {
    public Page()
    {
      InitializeComponent();
      
      TextBox code = FindName("codeText") as TextBox;
      code.Text = 
       @"a = [17,-2, 0,-3, 5, 3,1, 2, 55];

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
             s += a[i];";
    }

    public void WriteLine(object rez)
    {
      resultText.Text = (rez == null ? "Null" : rez.ToString()) + "\r\n" + resultText.Text;
    }

    public void Write(object rez)
    {
      resultText.Text = (rez == null ? "Null" : rez.ToString()) + resultText.Text;
    }


    private void Execute(object sender, RoutedEventArgs e)
    {
      TextBox code = FindName("codeText") as TextBox;
      TextBox resultText = FindName("resultText") as TextBox;

      string result = null;

      try
      {
        Script s = Script.Compile(code.Text);
        s.Context.SetItem("Console", this);
        object rez = s.Execute();
        result = rez == null ? "Null" : rez.ToString();
      }
      catch(Exception ex)
      {
        result = ex.Message;
      }

      resultText.Text = result + "\r\n" + resultText.Text;
    }
  }
}
