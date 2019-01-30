using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary1;
using System.Windows.Forms;

namespace translationsASM
{
    public partial class Form1 : Form
    {
        private static string codeFile;
        private static string code;
        public Form1()
        {
            InitializeComponent();
        }

        private void openFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog nf = new OpenFileDialog();
            richTextBox1.Text = "";
            nf.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            nf.RestoreDirectory = true;
            if (nf.ShowDialog() == DialogResult.OK)
            {
                codeFile = nf.FileName.ToString();
                code = File.ReadAllText(codeFile);
                richTextBox1.Text += code;
            }
            Reader.Initialize(codeFile);
        }
        public static int k = 0;

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            k++;
            string outputPath = "C:\\translator\\code" + k.ToString() + ".asm";
            SaveCode(outputPath);
        }
        private static void SaveCode(string nameOutputFile)
        {
            string newFileName = codeFile;
            string codeOutput = "";
            if (CodeGenerator.code != null)
            {
                foreach (var item in CodeGenerator.code)
                    codeOutput += item + "\r\n";

                System.IO.File.WriteAllText(nameOutputFile, codeOutput);
                MessageBox.Show("Код сохранен в: " + nameOutputFile + ".");
            }
            else
                MessageBox.Show("Код пустой.");
        }

        private void lexicalanalyzerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox3.Text = "";
            richTextBox2.Text = "";
            try
            {
                richTextBox2.Text += "Лексический анализ:" + "\r\n" + "\r\n";

                Reader.Initialize(codeFile);
                LexicalAnalyzer.Initialize();

                while (LexicalAnalyzer.currentLexem != LexicalAnalyzer.Lexems.EOF)
                {
                    richTextBox2.Text += LexicalAnalyzer.currentLexem + " ";

                    if (LexicalAnalyzer.currentLexem == LexicalAnalyzer.Lexems.Delimiter)
                        richTextBox2.Text += "\r\n";

                    if (LexicalAnalyzer.currentLexem == LexicalAnalyzer.Lexems.Identificator
                        && NameTable.FindByName(LexicalAnalyzer.currentName).Equals(new NameTable.Identrificator()))
                        NameTable.AddIdentrificator(LexicalAnalyzer.currentName, NameTable.tCat.Var);


                    LexicalAnalyzer.AnalyseNextLexem();
                }


                LinkedListNode<NameTable.Identrificator> identFirst = NameTable.GetListIdentifier.List.First;

                richTextBox2.Text += "\r\nИдентификаторы: ";

                while (identFirst != null)
                {
                    richTextBox2.Text += identFirst.Value.name + " ";
                    identFirst = identFirst.Next;
                }

                Reader.Close();
            }
            catch (Exception exp)
            {
                richTextBox3.Text += exp.Message;
            }
        }

        private void compileToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                richTextBox2.Text += "\r\n" + "\r\n" + "Синтаксический анализ:" + "\r\n" + "\r\n";

                Reader.Initialize(codeFile);
                SyntaxAnalyzer.Compile();

                if (SyntaxAnalyzer.errorMessages != null)
                {
                    for (int i = 0; i < SyntaxAnalyzer.errorMessages.Count; i++)
                        richTextBox3.Text += SyntaxAnalyzer.errorMessages[i] + "\r\n";
                }

                if (CodeGenerator.code != null)
                {
                    for (int i = 0; i < CodeGenerator.code.Length; i++)
                        if (CodeGenerator.code[i] != null)
                            richTextBox2.Text += CodeGenerator.code[i] + "\r\n";
                }

                Reader.Close();
            }
            catch (Exception exc)
            {
                richTextBox3.Text += exc.Message;
            }
        }
    }
    
}
