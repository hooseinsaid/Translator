using System;
namespace Translation
{
    class Test
    {
        static void Main(string[] args)
        {
           // Console.WriteLine("Hello World!");
            LexicalTest();
        }
        static void LexicalTest()
        {

            Reader.Initialize(@"C:\Users\hussa\OneDrive\Apps\Desktop\newtonhor.txt");
            LexicalAnalyzer.Initialize();
            string lexlemename = "";
            LexicalAnalyzer.Lexems lex;
            while (Reader.CurSymbol!=0)
            {
                LexicalAnalyzer.ParseNextLexem();
                lexlemename = LexicalAnalyzer.currentName;
                lex = LexicalAnalyzer.currentLexem;
                Console.WriteLine("string {0} {1} -> {2}", Reader.NLines, lexlemename, lex.ToString());

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
    }
}
