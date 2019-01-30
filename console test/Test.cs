using System;
using System.Collections.Generic;

namespace Translation
{
    internal class Test
    {
        public Test()
        {
             syntaxTest();
            //LexicalTest();
        }

        public static void syntaxTest()
        {
            Reader.Initialize(@"C:\Users\hussa\OneDrive\Apps\Desktop\newtonhor.txt");
            SyntaxAnalyzer.Compile();
            List<Error> errors = SyntaxAnalyzer.Errors;
            foreach (Error error in errors)
            {
                Console.WriteLine("ошибка: строка {0} позиция {1}", error.Line, error.Position);
                Console.WriteLine(error.ErrorMessage);
            }
            Reader.Close();
            Console.Read();
        }

        private static string Values = "";

        public static void LexicalTest()
        {
            Reader.Initialize(@"C:\Users\hussa\OneDrive\Apps\Desktop\newtonhor.txt");
            LexicalAnalyzer.Initialize();
            string lexlemename = "";
            LexicalAnalyzer.Lexems lex;
            while (Reader.CurSymbol != 0)
            {
                LexicalAnalyzer.ParseNextLexem();
                lexlemename = LexicalAnalyzer.currentName;
                lex = LexicalAnalyzer.currentLexem;
                Console.WriteLine("# string {0} {1} -> {2}", Reader.NLines, lexlemename, lex.ToString());
            }
            Console.ReadLine();

            //LexicalAnalyzer.Initialize();
            //Reader.Initialize(@"C:\Users\hussa\OneDrive\Apps\Desktop\newtonhor.txt");
            //string lexlemename = "";
            //LexicalAnalyzer.Lexems lex;
            //while (Reader.CurSymbol != 0)
            //{
            //    LexicalAnalyzer.ParseNextLexem();
            //    lexlemename = LexicalAnalyzer.currentName;
            //    lex = LexicalAnalyzer.currentLexem;
            //    Console.WriteLine("string {0} {1} -> {2}", Reader.NLines, lexlemename, lex.ToString());
            //    LexicalAnalyzer.ParseNextLexem();

            //}
        }

        public override string ToString()
        {
            return Values;
        }
    }
}