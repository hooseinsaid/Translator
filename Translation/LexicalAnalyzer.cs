using System;
using System.Collections.Generic;

namespace Translation
{
    public static class LexicalAnalyzer
    {
        private static List<Keyword> Keywords;
        private static int KeyWordPointer;
        public static Lexems currentLexem;
        public static string currentName;
        
        public enum Lexems
        {
            None,Name,Number,Begin,End,If,Then,Multiplication,Plus,Equal,
            Less,LessOrEqual,Greater,GreaterOrEqual,Semi,Assign,LesfBracket,EndOfF,Division,Minus,RightBracket,
            Seperator, Operator,Comma,DataType,
            Conjuction,
            Disjunction,
            ExcMark,
            For,
            While,
            As
        }

        private struct Keyword
        {
            public string word;
            public Lexems lex;
        }
        

        public static void AddKeyWord(string Keyword,Lexems lexem)
        {
            Keyword kw = new Keyword
            {
                word = Keyword, lex = lexem
            };
            Keywords.Add(kw);
        }

        public static Lexems GetKeyWord(string key)
        {
            foreach (Keyword keyword in Keywords)
                if (keyword.word.ToLower().Equals(key.ToLower()))
                    return keyword.lex;
            return Lexems.Name;
        }
        public static void ParseNextLexem()
        {
            while (Reader.CurSymbol==' ')
            {
                Reader.ReadNextSymbol();
            }
            if (Char.IsLetter((char)Reader.CurSymbol))
            {
                ParseID();
            }
            else if (Char.IsDigit((char)Reader.CurSymbol))
            {
                ParseNumber();
            }
            else if (Reader.CurSymbol == '\n')
            {
                Reader.ReadNextSymbol();
                currentLexem = Lexems.Seperator;
            }
            else if (Reader.CurSymbol == '<')
            {
                Reader.ReadNextSymbol();
                if (Reader.CurSymbol == '=')
                {
                    Reader.ReadNextSymbol();
                    currentLexem = Lexems.LessOrEqual;
                }
                else
                    currentLexem = Lexems.Less;
            }
            else if (Reader.CurSymbol == '+')
            {
                Reader.ReadNextSymbol();
                currentLexem = Lexems.Plus;
            }
            else if (Reader.CurSymbol == '-')
            {
                Reader.ReadNextSymbol();
                currentLexem = Lexems.Minus;
            }
            else if (Reader.CurSymbol == '*')
            {
                Reader.ReadNextSymbol();
                currentLexem = Lexems.Multiplication;
            }
            else if (Reader.CurSymbol == '/')
            {
                Reader.ReadNextSymbol();
                currentLexem = Lexems.Division;
            }
            else if (Reader.CurSymbol == '>')
            {
                Reader.ReadNextSymbol();
                if (Reader.CurSymbol == '=')
                {
                    Reader.ReadNextSymbol();
                    currentLexem = Lexems.GreaterOrEqual;
                }
                else
                    currentLexem = Lexems.Greater;
            }
            else if (Reader.CurSymbol == '(')
            {
                Reader.ReadNextSymbol();
                currentLexem = Lexems.LesfBracket;
            }
            else if (Reader.CurSymbol == ')')
            {
                Reader.ReadNextSymbol();
                currentLexem = Lexems.RightBracket;
            }
            else if (Reader.CurSymbol==',')
            {
                currentLexem = Lexems.Comma;
            }
            else if (Reader.CurSymbol == ';')
            {
                currentLexem = Lexems.Semi;
            }
            else if (Reader.CurSymbol == '&')
            {
                currentLexem = Lexems.Conjuction;
            }
            else if (Reader.CurSymbol == '|')
            {
                currentLexem = Lexems.Disjunction;
            }
            else if (Reader.CurSymbol == '!')
            {
                currentLexem = Lexems.ExcMark;
            }
            else if (Reader.CurSymbol == 0)
            {
                currentLexem = Lexems.EndOfF;
            }
            else
                throw new Exception("Unknown Symbol");
           
        }

        public static void ParseID()
        {
            string ID="";
            do
            {
                ID += (char)Reader.CurSymbol;
                Reader.ReadNextSymbol();
            }
            while (char.IsLetter((char)Reader.CurSymbol));
            currentName = ID;
            currentLexem = GetKeyWord(ID);

        }
        public static void ParseNumber()
        {
            string Num = "";
            do
            {
                Num += Reader.CurSymbol;
                Reader.ReadNextSymbol();
            } while (char.IsDigit((char)Reader.CurSymbol));
            currentName = Num;
            currentLexem = Lexems.Number;
        }
        public static void Initialize()
        {
            Keywords = new List<Keyword>();
            AddKeyWord("begin", Lexems.Begin);
            AddKeyWord("int", Lexems.DataType);
            AddKeyWord("end", Lexems.End);
            AddKeyWord("then", Lexems.Then);
            AddKeyWord("do", Lexems.Name);
            AddKeyWord("for", Lexems.For);
            AddKeyWord("While", Lexems.While);
            AddKeyWord("as", Lexems.As);
            ParseNextLexem();
        }
    }
}
