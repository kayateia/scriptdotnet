namespace ScriptIDE_CF
{
    partial class ScriptIDE
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.MainMenu mainMenu1;

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
          this.mainMenu1 = new System.Windows.Forms.MainMenu();
          this.mnuRun = new System.Windows.Forms.MenuItem();
          this.mnuExit = new System.Windows.Forms.MenuItem();
          this.Code = new System.Windows.Forms.TextBox();
          this.Output = new System.Windows.Forms.TextBox();
          this.SuspendLayout();
          // 
          // mainMenu1
          // 
          this.mainMenu1.MenuItems.Add(this.mnuRun);
          this.mainMenu1.MenuItems.Add(this.mnuExit);
          // 
          // mnuRun
          // 
          this.mnuRun.Text = "Run";
          this.mnuRun.Click += new System.EventHandler(this.mnuRun_Click);
          // 
          // mnuExit
          // 
          this.mnuExit.Text = "Exit";
          this.mnuExit.Click += new System.EventHandler(this.mnuExit_Click);
          // 
          // Code
          // 
          this.Code.Location = new System.Drawing.Point(3, 3);
          this.Code.Multiline = true;
          this.Code.Name = "Code";
          this.Code.Size = new System.Drawing.Size(234, 186);
          this.Code.TabIndex = 0;
          this.Code.Text = "2^10-1;";
          // 
          // Output
          // 
          this.Output.Location = new System.Drawing.Point(3, 195);
          this.Output.Multiline = true;
          this.Output.Name = "Output";
          this.Output.Size = new System.Drawing.Size(234, 70);
          this.Output.TabIndex = 1;
          // 
          // ScriptIDE
          // 
          this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
          this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
          this.AutoScroll = true;
          this.ClientSize = new System.Drawing.Size(240, 268);
          this.Controls.Add(this.Output);
          this.Controls.Add(this.Code);
          this.Menu = this.mainMenu1;
          this.Name = "ScriptIDE";
          this.Text = "Script.NET IDE";
          this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox Code;
        private System.Windows.Forms.MenuItem mnuRun;
        private System.Windows.Forms.MenuItem mnuExit;
        private System.Windows.Forms.TextBox Output;
    }
}

