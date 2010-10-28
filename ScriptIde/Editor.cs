//
// (c) Petro Protsyk 8.08.2008
//
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.IO;
using ScriptNET;
using ScriptNET.Runtime;

namespace ScriptIde
{
  public partial class Editor : Form
  {
      string fileName = "text1.txt";
      public string FileName
      {
          get
          {
              return fileName;
          }
          set
          {
              fileName = value;
              lbStatus.Text = "Using [" + fileName + "]";
          }
      }

    public Editor()
    {
      InitializeComponent();
    }

    private void FormLoad(object sender, EventArgs e)
    {
      syntaxDocument1.SetSyntaxFromEmbeddedResource(Assembly.GetExecutingAssembly(), "ScriptIde.Resources.ScriptNET.syn");
    }

    #region Time Measure
    private DateTime BeginAction()
    {
      return DateTime.Now;
    }

    private TimeSpan EndAction(DateTime startTime)
    {
      return DateTime.Now.Subtract(startTime);
    }
    #endregion

    #region Commands
    private void OpenCommand(object sender, EventArgs e)
    {
      OpenFileDialog openDialog = new OpenFileDialog();
      openDialog.Filter = "All|*.*;";
      if (openDialog.ShowDialog() == DialogResult.OK)
      {
        syntaxDocument1.Lines = File.ReadAllLines(openDialog.FileName);
        FileName = openDialog.FileName;
      }
    }

    private void ExitCommand(object sender, EventArgs e)
    {
      Application.Exit();
    }

    private void ParseCommand(object sender, EventArgs e)
    {
      lbStatus.ToolTipText = "";
      lbStatus.Text = "Start parsing...";
      try
      {
        DateTime time = BeginAction();
        Script.Compile(syntaxDocument1.Text);
        lbStatus.Text = "Parsing successful, " + EndAction(time).ToString();
        lbStatus.ToolTipText = "";
      }
      catch (ScriptSyntaxErrorException s)
      {
        lbStatus.Text = "Syntax error";
        lbStatus.ToolTipText = s.Message;
      }
      catch (ScriptException)
      {
        lbStatus.Text = "Script exception";
      }
      catch
      {
        lbStatus.Text = "Application exception";
      }
    }

    private void ExecuteCommand(object sender, EventArgs e)
    {
      lbStatus.ToolTipText = "";
      lbStatus.Text = "Parsing...";
      try
      {
        Script s = Script.Compile(syntaxDocument1.Text);
        s.Context.SetItem("Console", this);

        lbStatus.Text = "Parsing successful";
        lbStatus.Text = "Executing...";
        Update();
        DateTime time = BeginAction();
        object rez = s.Execute();
        lbStatus.Text = "Execution succeded, "+EndAction(time).ToString();
        if (rez != null)
          lbStatus.ToolTipText = "Result:" + rez.ToString();
        else
          lbStatus.ToolTipText = "";
      }
      catch (ScriptSyntaxErrorException s)
      {
        lbStatus.Text = "Syntax error";
        lbStatus.ToolTipText = s.Message;
      }
      catch (ScriptException)
      {
        lbStatus.Text = "Script exception";
      }
      catch
      {
        lbStatus.Text = "Application exception";
      }
    }

    private void PasteCommand(object sender, EventArgs e)
    {
      syntaxBoxControl1.Paste();
    }

    private void CopyCommand(object sender, EventArgs e)
    {
      syntaxBoxControl1.Copy();
    }

    private void CutCommand(object sender, EventArgs e)
    {
      syntaxBoxControl1.Cut();
    }

    private void RedoCommand(object sender, EventArgs e)
    {
      syntaxBoxControl1.Redo();
    }

    private void UndoCommand(object sender, EventArgs e)
    {
      syntaxBoxControl1.Undo();
    }

    private void SelectAllCommand(object sender, EventArgs e)
    {
      syntaxBoxControl1.SelectAll();
    }

    private void NewCommand(object sender, EventArgs e)
    {
      syntaxDocument1.Clear();
      FileName = "text1.txt";
    }

    private void ShowHelpCommand(object sender, EventArgs e)
    {
        Help.ShowHelp(this, "Documentation.chm");
    }

    private void SaveCommand(object sender, EventArgs e)
    {
        if (fileName != null)
        {
            SaveToFile();
        }
    }

    private void SaveAsCommand(object sender, EventArgs e)
    {
        SaveFileDialog saveDialog = new SaveFileDialog();
        saveDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
        if (saveDialog.ShowDialog() == DialogResult.OK)
        {
            fileName = saveDialog.FileName;
            SaveToFile();
        }
    }

    private void SaveToFile()
    {
        try
        {
            File.WriteAllText(fileName, syntaxDocument1.Text);
            lbStatus.Text = "[" + fileName + "] saved";
        }
        catch
        {
            lbStatus.Text = "can't save";
        }
    }
    #endregion   

    #region Console
    public void Write(object o)
    {
      richTextBox1.Text += o.ToString();
      richTextBox1.SelectionStart = richTextBox1.Text.Length;
      richTextBox1.ScrollToCaret();
    }

    public void WriteLine(object o)
    {
      richTextBox1.Text += "\r\n" + o.ToString();
      richTextBox1.SelectionStart = richTextBox1.Text.Length;
      richTextBox1.ScrollToCaret();
    }
    #endregion
  }
}
