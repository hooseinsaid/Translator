using System;
namespace Translation
{
    class Test
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            LexicalTest();
        }
        static void LexicalTest()
        {
            Reader.Initialize(@"C:\Users\hussa\OneDrive\Apps\Desktop\newtonhor.txt");
            test.Initialize();
            while( test.currentLexem!=test.Lexems.EOF)
            {
                Console.WriteLine("string {0} {1} -> {2}", Reader.NLines, LexicalAnalyzer.currentName,LexicalAnalyzer.currentLexem);
                LexicalAnalyzer.ParseNextLexem();
            }

        }
    }
}
