using System;
using System.IO;
using System.Windows.Forms;
using static Translation.CodeGens;
using static Translation.LexicalAnalyzer;
using static Translation.Reader;

namespace Транслятор
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private OpenFileDialog openFile = new OpenFileDialog();

        public string filepath { get; private set; }

        private void FileOpen(object sender, System.EventArgs e)
        {
            try
            {
                input_text.Clear();
                if (openFile.ShowDialog() == DialogResult.OK)
                {
                    Initialize(openFile.FileName);
                    filepath = openFile.FileName;
                    while (CurSymbol != 0)
                    {
                        input_text.AppendText((char)CurSymbol + "");
                        ReadNextSymbol();
                    }
                    // Translation.Reader.Close();
                    //var Code = File.ReadAllText(openFile.FileName);
                    //input_text.Text=Code;
                    //Initialize();
                }
                Translation.Reader.Close();
            }
            catch (System.Exception s)
            {
                richTextBoxOutput.Text = s + " ";
            }
        }

        private void FileSave(object sender, System.EventArgs e)
        {
            // string Codes = input_text.Text;
            if (File.Exists(openFile.FileName))
            {
                DialogResult dialogResult = MessageBox.Show("Save file ?", "Save", MessageBoxButtons.YesNoCancel);
                if (dialogResult == DialogResult.Yes)
                {
                    Translation.Reader.Close();
                    File.GetAccessControl(openFile.FileName);
                    File.WriteAllText(openFile.FileName, input_text.Text);
                    MessageBox.Show("Saved !");
                }
                else if (dialogResult == DialogResult.No)
                {
                    openFile.Reset();
                    Close();
                }
            }
        }

        private void compileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Initialize(filepath != null ? filepath : Filepath(sender, e));
                Translation.SyntaxAnalyzer.Errors.Clear();
                Translation.SyntaxAnalyzer.Compile();
                richTextBoxOutput.Clear();
                richTextBoxError.Clear();
                var Errors = Translation.SyntaxAnalyzer.Errors;
                if (Errors.Count == 0)
                {
                    richTextBoxError.Text = "Code compile Successfully !!";
                }
                else
                    Errors.ForEach(x =>
                    {
                        richTextBoxError.AppendText("Error in Line " + x.Line + " position " + x.Position + ": Error message: " + x.ErrorMessage + "\n");
                    });
                foreach (var item in code)
                {
                    if (item != null)
                    {
                        richTextBoxOutput.AppendText(item + "\n");
                    }
                    else
                        break;
                }
            }
            catch (Exception exp)
            {
                richTextBoxError.Text += (exp.Message);
            }
        }

        private string Filepath(object or, EventArgs e)
        {
            FileOpen(or, e);
            return filepath;
        }
    }
}