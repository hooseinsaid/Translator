using System;
using System.IO;
using System.Windows.Forms;
using static Translation.LexicalAnalyzer;
using static Translation.Reader;
using System.Linq;

namespace Транслятор
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private OpenFileDialog openFile = new OpenFileDialog();
        private void FileOpen(object sender, System.EventArgs e)
        {
            try
            {
                input_text.Clear();
                if (openFile.ShowDialog() == DialogResult.OK)
                    {
                    Initialize(openFile.FileName);
                    while (CurSymbol != 0)
                    {
                        input_text.AppendText((char)CurSymbol + "");
                        ReadNextSymbol();
                    }
                    Translation.Reader.Close();
                    //Initialize();
                    //while (currentLexem != Lexems.EndOfF)
                    //{
                    //    input_text.AppendText(currentLexem + "");
                    //    ParseNextLexem();
                    //}
                    //Translation.Reader.Close();
                    //var Code = File.ReadAllText(openFile.FileName);
                    //input_text.Text=Code;
                    //Initialize();

                    Initialize(openFile.FileName);
                    Initialize();
                    string lexlemename = "";
                    Lexems lex;
                    while (CurSymbol != 0)
                    {
                        ParseNextLexem();
                        lexlemename = currentName;
                        lex = currentLexem;
                        OutputRicxhBox.AppendText(("String Line : "+ NLines + "   "+ lexlemename + "  ->  "+ lex.ToString()+"\n"));

                    }
                    Console.ReadLine();
                }
            }
            catch (System.Exception s)
            {
                OutputRicxhBox.Text=s+ " ";
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
                    File.WriteAllText(openFile.FileName+"0", input_text.Text);
                    MessageBox.Show("Saved !");
            }
            else if (dialogResult == DialogResult.No)
            {
                openFile.Reset();
                Close();
            }
        }

        }
    }
}