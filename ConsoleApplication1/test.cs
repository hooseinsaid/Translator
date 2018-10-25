using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Translation;

namespace ConsoleApplication1
{
    class test
    {
        static void Main(string[] args)
        {
           // ReaderTest();
            LexicalTest();
            //Syntaxtest();
        }
        static void ReaderTest()
        {
            Reader.Initialize(@"D:\1.txt");
            while (!Reader.Stream_reader)
            {
                Console.Write(Reader.Currentchar);
                Reader.ReadnextSym();
            }
            Reader.Close();
            Console.ReadKey();
        }
        static void LexicalTest()
        {
            LexicalAnalyzer.Initialize();
            Reader.Initialize(@"C:\Users\hussa\OneDrive\Apps\Desktop\newtonhor.txt");
            string lexlemename = "";
            Lexemes lex;
            while (!Reader.Stream_reader)
            {
                LexicalAnalyzer.ParseNextlexeme();
                lexlemename = LexicalAnalyzer.CurrentName;
                lex = LexicalAnalyzer.Currentlexem;
                Console.WriteLine("string {0} {1} -> {2}",Reader.StringNum, lexlemename, lex.ToString());

            }
            Reader.Close();
            Console.ReadKey();
        }

        /*static void Syntaxtest()
        {
            Reader.Initialize(@"D:\1.txt");
            SyntaxAnalyzer.Compile();
            List<Error> errors = SyntaxAnalyzer.Errors;
            foreach( Error error in errors)
            {
                Console.WriteLine("ошибка: строка {0} позиция {1}", error.Line, error.Position);   
            }  
            Reader.Close();
            Console.Read();
        }*/
    }
}
