using System;
using System.Collections.Generic;

namespace Translation
{
    class LexicalAnalyzer
    {
        public enum Lexems
        {
            None,Name,Number,Begin,End,If,Then,Multiplication,Division,Plus,Equal,
            Less,LessOrEqual,Semi,Assign,LesfBracket,EOF,Divde,Minus,RightBracket,
            Seperator, Operator,Comma,DataType
        }

        private struct Keyword
        {
            public string word;
            public Lexems lex;
        }
        private readonly List<Keyword> Keywords;
        private readonly int KeyWordPointer;

        private void AddKeyWord(string Keyword,Lexems lexem)
        {
            Keyword kw = new Keyword
            {
                word = Keyword, lex = lexem
            };
            Keywords.Add(kw);
        }

        private Lexems RecievedKeyWord(string keyword)
        {
            for (int i = KeyWordPointer - 1; i >= 0; i--)
            {
                if (Keywords[KeyWordPointer].word==keyword)
                {
                    return Keywords[KeyWordPointer].lex;
                }
            }
            return Lexems.Name;
        }
        public Lexems currentLexem;
        public string currentName;


        private void CheckNextLexems()
        {
            while (Reader.CrSymbol==' ')
            {
                Reader.ReadNextSymbol();
            } 
            if (char.IsLetter((char)Reader.CrSymbol))
            {
                
            }
            else if (char.IsDigit((char)Reader.CrSymbol))
            {

            }
            else if (Reader.CrSymbol=='\n')
            {
                Reader.ReadNextSymbol();
                currentLexem = Lexems.Seperator;
            }
            else if (Reader.CrSymbol=='<')
            {
                Reader.ReadNextSymbol();
                if (Reader.CrSymbol == '=')
                {
                    Reader.ReadNextSymbol();
                    currentLexem = Lexems.LessOrEqual;
                }
                else
                    currentLexem = Lexems.Less;
            }
            else if (Reader.CrSymbol=='+')
            {
                Reader.ReadNextSymbol();
                currentLexem = Lexems.Plus;
            }
        }

        private void CheckID()
        {
            string ID="";
            do
            {
                ID += Reader.CrSymbol;
                Reader.ReadNextSymbol();
            }
            while (char.IsLetter((char)Reader.CrSymbol));
            currentName = ID;
            currentLexem = RecievedKeyWord(ID);

        }
    }
}
