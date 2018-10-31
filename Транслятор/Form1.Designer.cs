namespace Транслятор
{
    partial class Form1
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.compilerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Результат = new System.Windows.Forms.GroupBox();
            this.OutputRicxhBox = new System.Windows.Forms.RichTextBox();
            this.Исходный_код = new System.Windows.Forms.GroupBox();
            this.input_text = new System.Windows.Forms.RichTextBox();
            this.Состояния_компиляции = new System.Windows.Forms.GroupBox();
            this.richTextBox3 = new System.Windows.Forms.RichTextBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.label1 = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            this.Результат.SuspendLayout();
            this.Исходный_код.SuspendLayout();
            this.Состояния_компиляции.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.compilerToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(584, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.FileOpen);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.FileSave);
            // 
            // compilerToolStripMenuItem
            // 
            this.compilerToolStripMenuItem.Name = "compilerToolStripMenuItem";
            this.compilerToolStripMenuItem.Size = new System.Drawing.Size(68, 20);
            this.compilerToolStripMenuItem.Text = "Compiler";
            // 
            // Результат
            // 
            this.Результат.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Результат.AutoSize = true;
            this.Результат.Controls.Add(this.OutputRicxhBox);
            this.Результат.Location = new System.Drawing.Point(294, 51);
            this.Результат.Name = "Результат";
            this.Результат.Size = new System.Drawing.Size(270, 313);
            this.Результат.TabIndex = 1;
            this.Результат.TabStop = false;
            this.Результат.Text = "Результат";
            // 
            // OutputRicxhBox
            // 
            this.OutputRicxhBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.OutputRicxhBox.Location = new System.Drawing.Point(3, 16);
            this.OutputRicxhBox.Name = "OutputRicxhBox";
            this.OutputRicxhBox.ReadOnly = true;
            this.OutputRicxhBox.Size = new System.Drawing.Size(264, 294);
            this.OutputRicxhBox.TabIndex = 0;
            this.OutputRicxhBox.Text = "";
            // 
            // Исходный_код
            // 
            this.Исходный_код.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Исходный_код.AutoSize = true;
            this.Исходный_код.Controls.Add(this.input_text);
            this.Исходный_код.Location = new System.Drawing.Point(12, 51);
            this.Исходный_код.Name = "Исходный_код";
            this.Исходный_код.Size = new System.Drawing.Size(266, 313);
            this.Исходный_код.TabIndex = 2;
            this.Исходный_код.TabStop = false;
            this.Исходный_код.Text = "Исходный код";
            // 
            // input_text
            // 
            this.input_text.Dock = System.Windows.Forms.DockStyle.Fill;
            this.input_text.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.input_text.Location = new System.Drawing.Point(3, 16);
            this.input_text.Name = "input_text";
            this.input_text.Size = new System.Drawing.Size(260, 294);
            this.input_text.TabIndex = 1;
            this.input_text.Text = "";
            // 
            // Состояния_компиляции
            // 
            this.Состояния_компиляции.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Состояния_компиляции.AutoSize = true;
            this.Состояния_компиляции.Controls.Add(this.richTextBox3);
            this.Состояния_компиляции.Location = new System.Drawing.Point(12, 381);
            this.Состояния_компиляции.Name = "Состояния_компиляции";
            this.Состояния_компиляции.Size = new System.Drawing.Size(552, 179);
            this.Состояния_компиляции.TabIndex = 2;
            this.Состояния_компиляции.TabStop = false;
            this.Состояния_компиляции.Text = "Состояния компиляции";
            // 
            // richTextBox3
            // 
            this.richTextBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox3.Location = new System.Drawing.Point(3, 16);
            this.richTextBox3.Name = "richTextBox3";
            this.richTextBox3.ReadOnly = true;
            this.richTextBox3.Size = new System.Drawing.Size(546, 160);
            this.richTextBox3.TabIndex = 0;
            this.richTextBox3.Text = "";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(130, 50);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 13);
            this.label1.TabIndex = 3;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 572);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Состояния_компиляции);
            this.Controls.Add(this.Исходный_код);
            this.Controls.Add(this.Результат);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.Результат.ResumeLayout(false);
            this.Исходный_код.ResumeLayout(false);
            this.Состояния_компиляции.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem compilerToolStripMenuItem;
        private System.Windows.Forms.GroupBox Результат;
        private System.Windows.Forms.GroupBox Исходный_код;
        private System.Windows.Forms.GroupBox Состояния_компиляции;
        private System.Windows.Forms.RichTextBox OutputRicxhBox;
        private System.Windows.Forms.RichTextBox input_text;
        private System.Windows.Forms.RichTextBox richTextBox3;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.Label label1;
    }
}

