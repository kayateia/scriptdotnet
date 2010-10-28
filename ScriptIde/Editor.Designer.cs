namespace ScriptIde
{
  partial class Editor
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        this.components = new System.ComponentModel.Container();
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Editor));
        this.syntaxBoxControl1 = new Alsing.Windows.Forms.SyntaxBoxControl();
        this.syntaxDocument1 = new Alsing.SourceCode.SyntaxDocument(this.components);
        this.menuStrip1 = new System.Windows.Forms.MenuStrip();
        this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
        this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
        this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.undoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.redoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
        this.cutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
        this.selectAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
        this.executeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
        this.customizeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.contentsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.indexToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.searchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
        this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.statusStrip1 = new System.Windows.Forms.StatusStrip();
        this.lbStatus = new System.Windows.Forms.ToolStripStatusLabel();
        this.splitContainer1 = new System.Windows.Forms.SplitContainer();
        this.richTextBox1 = new System.Windows.Forms.RichTextBox();
        this.menuStrip1.SuspendLayout();
        this.statusStrip1.SuspendLayout();
        this.splitContainer1.Panel1.SuspendLayout();
        this.splitContainer1.Panel2.SuspendLayout();
        this.splitContainer1.SuspendLayout();
        this.SuspendLayout();
        // 
        // syntaxBoxControl1
        // 
        this.syntaxBoxControl1.ActiveView = Alsing.Windows.Forms.ActiveView.BottomRight;
        this.syntaxBoxControl1.AutoListPosition = null;
        this.syntaxBoxControl1.AutoListSelectedText = "a123";
        this.syntaxBoxControl1.AutoListVisible = false;
        this.syntaxBoxControl1.BackColor = System.Drawing.Color.White;
        this.syntaxBoxControl1.BorderStyle = Alsing.Windows.Forms.BorderStyle.None;
        this.syntaxBoxControl1.CopyAsRTF = false;
        resources.ApplyResources(this.syntaxBoxControl1, "syntaxBoxControl1");
        this.syntaxBoxControl1.Document = this.syntaxDocument1;
        this.syntaxBoxControl1.FontName = "Courier new";
        this.syntaxBoxControl1.InfoTipCount = 1;
        this.syntaxBoxControl1.InfoTipPosition = null;
        this.syntaxBoxControl1.InfoTipSelectedIndex = 1;
        this.syntaxBoxControl1.InfoTipVisible = false;
        this.syntaxBoxControl1.LineNumberBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
        this.syntaxBoxControl1.LineNumberBorderColor = System.Drawing.Color.DodgerBlue;
        this.syntaxBoxControl1.LineNumberForeColor = System.Drawing.Color.SteelBlue;
        this.syntaxBoxControl1.LockCursorUpdate = false;
        this.syntaxBoxControl1.Name = "syntaxBoxControl1";
        this.syntaxBoxControl1.ParseOnPaste = true;
        this.syntaxBoxControl1.ShowScopeIndicator = false;
        this.syntaxBoxControl1.SmoothScroll = true;
        this.syntaxBoxControl1.SplitView = false;
        this.syntaxBoxControl1.SplitviewH = -4;
        this.syntaxBoxControl1.SplitviewV = -4;
        this.syntaxBoxControl1.TabGuideColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
        this.syntaxBoxControl1.WhitespaceColor = System.Drawing.SystemColors.ControlDark;
        // 
        // syntaxDocument1
        // 
        this.syntaxDocument1.Lines = new string[] {
        "//Bubble sort demo\r",
        "\r",
        "a=[17, 0, 5, 3,1, 2, 55];\r",
        "\r",
        "for (i=0; i < a.Length; i=i+1)\r",
        " for (j=i+1; j <  a.Length; j=j+1)\r",
        "   if (a[i] > a[j] )\r",
        "   {\r",
        "     temp = a[i]; \r",
        "     a[i] = a[j];\r",
        "     a[j] = temp;\r",
        "   }\r",
        "\r",
        "s = \'Result:\';\r",
        "for (i=0; i < a.Length; i++)\r",
        "  if (i == 0)\r",
        "    s = s + a[i];\r",
        "  else\r",
        "    s = s + \',\' + a[i];\r",
        "Console.WriteLine(s);"};
        this.syntaxDocument1.MaxUndoBufferSize = 1000;
        this.syntaxDocument1.Modified = false;
        this.syntaxDocument1.UndoStep = 0;
        // 
        // menuStrip1
        // 
        this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.helpToolStripMenuItem});
        resources.ApplyResources(this.menuStrip1, "menuStrip1");
        this.menuStrip1.Name = "menuStrip1";
        // 
        // fileToolStripMenuItem
        // 
        this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.openToolStripMenuItem,
            this.toolStripSeparator,
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.toolStripSeparator2,
            this.exitToolStripMenuItem});
        this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
        resources.ApplyResources(this.fileToolStripMenuItem, "fileToolStripMenuItem");
        // 
        // newToolStripMenuItem
        // 
        resources.ApplyResources(this.newToolStripMenuItem, "newToolStripMenuItem");
        this.newToolStripMenuItem.Name = "newToolStripMenuItem";
        this.newToolStripMenuItem.Click += new System.EventHandler(this.NewCommand);
        // 
        // openToolStripMenuItem
        // 
        resources.ApplyResources(this.openToolStripMenuItem, "openToolStripMenuItem");
        this.openToolStripMenuItem.Name = "openToolStripMenuItem";
        this.openToolStripMenuItem.Click += new System.EventHandler(this.OpenCommand);
        // 
        // toolStripSeparator
        // 
        this.toolStripSeparator.Name = "toolStripSeparator";
        resources.ApplyResources(this.toolStripSeparator, "toolStripSeparator");
        // 
        // saveToolStripMenuItem
        // 
        resources.ApplyResources(this.saveToolStripMenuItem, "saveToolStripMenuItem");
        this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
        this.saveToolStripMenuItem.Click += new System.EventHandler(this.SaveCommand);
        // 
        // saveAsToolStripMenuItem
        // 
        this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
        resources.ApplyResources(this.saveAsToolStripMenuItem, "saveAsToolStripMenuItem");
        this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.SaveAsCommand);
        // 
        // toolStripSeparator2
        // 
        this.toolStripSeparator2.Name = "toolStripSeparator2";
        resources.ApplyResources(this.toolStripSeparator2, "toolStripSeparator2");
        // 
        // exitToolStripMenuItem
        // 
        this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
        resources.ApplyResources(this.exitToolStripMenuItem, "exitToolStripMenuItem");
        this.exitToolStripMenuItem.Click += new System.EventHandler(this.ExitCommand);
        // 
        // editToolStripMenuItem
        // 
        this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.undoToolStripMenuItem,
            this.redoToolStripMenuItem,
            this.toolStripSeparator3,
            this.cutToolStripMenuItem,
            this.copyToolStripMenuItem,
            this.pasteToolStripMenuItem,
            this.toolStripSeparator4,
            this.selectAllToolStripMenuItem});
        this.editToolStripMenuItem.Name = "editToolStripMenuItem";
        resources.ApplyResources(this.editToolStripMenuItem, "editToolStripMenuItem");
        // 
        // undoToolStripMenuItem
        // 
        this.undoToolStripMenuItem.Name = "undoToolStripMenuItem";
        resources.ApplyResources(this.undoToolStripMenuItem, "undoToolStripMenuItem");
        this.undoToolStripMenuItem.Click += new System.EventHandler(this.UndoCommand);
        // 
        // redoToolStripMenuItem
        // 
        this.redoToolStripMenuItem.Name = "redoToolStripMenuItem";
        resources.ApplyResources(this.redoToolStripMenuItem, "redoToolStripMenuItem");
        this.redoToolStripMenuItem.Click += new System.EventHandler(this.RedoCommand);
        // 
        // toolStripSeparator3
        // 
        this.toolStripSeparator3.Name = "toolStripSeparator3";
        resources.ApplyResources(this.toolStripSeparator3, "toolStripSeparator3");
        // 
        // cutToolStripMenuItem
        // 
        resources.ApplyResources(this.cutToolStripMenuItem, "cutToolStripMenuItem");
        this.cutToolStripMenuItem.Name = "cutToolStripMenuItem";
        this.cutToolStripMenuItem.Click += new System.EventHandler(this.CutCommand);
        // 
        // copyToolStripMenuItem
        // 
        resources.ApplyResources(this.copyToolStripMenuItem, "copyToolStripMenuItem");
        this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
        this.copyToolStripMenuItem.Click += new System.EventHandler(this.CopyCommand);
        // 
        // pasteToolStripMenuItem
        // 
        resources.ApplyResources(this.pasteToolStripMenuItem, "pasteToolStripMenuItem");
        this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
        this.pasteToolStripMenuItem.Click += new System.EventHandler(this.PasteCommand);
        // 
        // toolStripSeparator4
        // 
        this.toolStripSeparator4.Name = "toolStripSeparator4";
        resources.ApplyResources(this.toolStripSeparator4, "toolStripSeparator4");
        // 
        // selectAllToolStripMenuItem
        // 
        this.selectAllToolStripMenuItem.Name = "selectAllToolStripMenuItem";
        resources.ApplyResources(this.selectAllToolStripMenuItem, "selectAllToolStripMenuItem");
        this.selectAllToolStripMenuItem.Click += new System.EventHandler(this.SelectAllCommand);
        // 
        // toolsToolStripMenuItem
        // 
        this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.executeToolStripMenuItem,
            this.toolStripSeparator6,
            this.customizeToolStripMenuItem,
            this.optionsToolStripMenuItem});
        this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
        resources.ApplyResources(this.toolsToolStripMenuItem, "toolsToolStripMenuItem");
        // 
        // toolStripMenuItem1
        // 
        this.toolStripMenuItem1.Name = "toolStripMenuItem1";
        resources.ApplyResources(this.toolStripMenuItem1, "toolStripMenuItem1");
        this.toolStripMenuItem1.Click += new System.EventHandler(this.ParseCommand);
        // 
        // executeToolStripMenuItem
        // 
        this.executeToolStripMenuItem.Name = "executeToolStripMenuItem";
        resources.ApplyResources(this.executeToolStripMenuItem, "executeToolStripMenuItem");
        this.executeToolStripMenuItem.Click += new System.EventHandler(this.ExecuteCommand);
        // 
        // toolStripSeparator6
        // 
        this.toolStripSeparator6.Name = "toolStripSeparator6";
        resources.ApplyResources(this.toolStripSeparator6, "toolStripSeparator6");
        // 
        // customizeToolStripMenuItem
        // 
        this.customizeToolStripMenuItem.Name = "customizeToolStripMenuItem";
        resources.ApplyResources(this.customizeToolStripMenuItem, "customizeToolStripMenuItem");
        // 
        // optionsToolStripMenuItem
        // 
        this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
        resources.ApplyResources(this.optionsToolStripMenuItem, "optionsToolStripMenuItem");
        // 
        // helpToolStripMenuItem
        // 
        this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.contentsToolStripMenuItem,
            this.indexToolStripMenuItem,
            this.searchToolStripMenuItem,
            this.toolStripSeparator5,
            this.aboutToolStripMenuItem});
        this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
        resources.ApplyResources(this.helpToolStripMenuItem, "helpToolStripMenuItem");
        // 
        // contentsToolStripMenuItem
        // 
        this.contentsToolStripMenuItem.Name = "contentsToolStripMenuItem";
        resources.ApplyResources(this.contentsToolStripMenuItem, "contentsToolStripMenuItem");
        this.contentsToolStripMenuItem.Click += new System.EventHandler(this.ShowHelpCommand);
        // 
        // indexToolStripMenuItem
        // 
        this.indexToolStripMenuItem.Name = "indexToolStripMenuItem";
        resources.ApplyResources(this.indexToolStripMenuItem, "indexToolStripMenuItem");
        this.indexToolStripMenuItem.Click += new System.EventHandler(this.ShowHelpCommand);
        // 
        // searchToolStripMenuItem
        // 
        this.searchToolStripMenuItem.Name = "searchToolStripMenuItem";
        resources.ApplyResources(this.searchToolStripMenuItem, "searchToolStripMenuItem");
        this.searchToolStripMenuItem.Click += new System.EventHandler(this.ShowHelpCommand);
        // 
        // toolStripSeparator5
        // 
        this.toolStripSeparator5.Name = "toolStripSeparator5";
        resources.ApplyResources(this.toolStripSeparator5, "toolStripSeparator5");
        // 
        // aboutToolStripMenuItem
        // 
        this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
        resources.ApplyResources(this.aboutToolStripMenuItem, "aboutToolStripMenuItem");
        this.aboutToolStripMenuItem.Click += new System.EventHandler(this.ShowHelpCommand);
        // 
        // statusStrip1
        // 
        this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lbStatus});
        resources.ApplyResources(this.statusStrip1, "statusStrip1");
        this.statusStrip1.Name = "statusStrip1";
        this.statusStrip1.ShowItemToolTips = true;
        // 
        // lbStatus
        // 
        this.lbStatus.Name = "lbStatus";
        resources.ApplyResources(this.lbStatus, "lbStatus");
        // 
        // splitContainer1
        // 
        resources.ApplyResources(this.splitContainer1, "splitContainer1");
        this.splitContainer1.Name = "splitContainer1";
        // 
        // splitContainer1.Panel1
        // 
        this.splitContainer1.Panel1.Controls.Add(this.syntaxBoxControl1);
        // 
        // splitContainer1.Panel2
        // 
        this.splitContainer1.Panel2.Controls.Add(this.richTextBox1);
        // 
        // richTextBox1
        // 
        this.richTextBox1.BackColor = System.Drawing.Color.Black;
        this.richTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
        resources.ApplyResources(this.richTextBox1, "richTextBox1");
        this.richTextBox1.ForeColor = System.Drawing.Color.White;
        this.richTextBox1.Name = "richTextBox1";
        // 
        // Editor
        // 
        resources.ApplyResources(this, "$this");
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.Controls.Add(this.splitContainer1);
        this.Controls.Add(this.statusStrip1);
        this.Controls.Add(this.menuStrip1);
        this.MainMenuStrip = this.menuStrip1;
        this.Name = "Editor";
        this.Load += new System.EventHandler(this.FormLoad);
        this.menuStrip1.ResumeLayout(false);
        this.menuStrip1.PerformLayout();
        this.statusStrip1.ResumeLayout(false);
        this.statusStrip1.PerformLayout();
        this.splitContainer1.Panel1.ResumeLayout(false);
        this.splitContainer1.Panel2.ResumeLayout(false);
        this.splitContainer1.ResumeLayout(false);
        this.ResumeLayout(false);
        this.PerformLayout();

    }

    #endregion

    private Alsing.Windows.Forms.SyntaxBoxControl syntaxBoxControl1;
    private Alsing.SourceCode.SyntaxDocument syntaxDocument1;
    private System.Windows.Forms.MenuStrip menuStrip1;
    private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
    private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
    private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem undoToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem redoToolStripMenuItem;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
    private System.Windows.Forms.ToolStripMenuItem cutToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem pasteToolStripMenuItem;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
    private System.Windows.Forms.ToolStripMenuItem selectAllToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem customizeToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem contentsToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem indexToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem searchToolStripMenuItem;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
    private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
    private System.Windows.Forms.StatusStrip statusStrip1;
    private System.Windows.Forms.ToolStripStatusLabel lbStatus;
    private System.Windows.Forms.ToolStripMenuItem executeToolStripMenuItem;
    private System.Windows.Forms.SplitContainer splitContainer1;
    private System.Windows.Forms.RichTextBox richTextBox1;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;

  }
}

