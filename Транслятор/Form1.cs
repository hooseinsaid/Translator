using System;
using System.IO;
using System.Windows.Forms;
using Translation;
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
                    int numbers = 0;
                    Reader.Initialize(openFile.FileName);
                    while (Reader.CrSymbol != 0)
                    {
                        Reader.ReadNextSymbol();
                    }
                    numbers = File.ReadLines(openFile.FileName).Count();
                    input_text.Text = File.ReadAllText(openFile.FileName);
                    string ss = File.GetLastWriteTime(openFile.FileName).ToString();
                    input_text.AppendText(numbers.ToString()+"\n");
                    input_text.AppendText("Last Edited :"+ss+"\r");
                    ss = File.GetCreationTime(openFile.FileName).ToString();
                    input_text.AppendText("Created :"+ss+"\r");
                    ss = File.GetLastAccessTime(openFile.FileName).ToString();
                    input_text.AppendText("Last Access :" + ss);
                }
            }
            catch (System.Exception s)
            {
                richTextBox1.Text=s+ " ";
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
                    File.WriteAllText(openFile.FileName, input_text.Text);
                    MessageBox.Show("Saved !");
            }
            else if (dialogResult == DialogResult.No)
            {
                openFile.Reset();
                Reader.Close();
            }
        }

        }
    }
}