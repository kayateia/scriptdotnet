using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using ScriptNET;
using ScriptNET.Runtime;

namespace ScriptIDE_CF
{
    public partial class ScriptIDE : Form
    {
        public ScriptIDE()
        {
            InitializeComponent();
        }

        private void mnuRun_Click(object sender, EventArgs e)
        {
            this.Output.Text = "";

            try
            {
                Script s = Script.Compile(Code.Text);
                
                object rez = s.Execute();
                Output.Text = "Execution succeded.";
                if (rez != null)
                    Output.Text += "Result:" + rez.ToString();
            }
            catch (ScriptSyntaxErrorException s)
            {
                this.Output.Text = "Syntax error."+s.Message;
            }
            catch (ScriptException se)
            {
                this.Output.Text = "Script exception."+se.Message;
            }
        }

        private void mnuExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}