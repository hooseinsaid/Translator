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
            Do,Number,Begin,End,If,Then,Multiplication,Plus,Equal,
            Less,LessOrEqual,Greater,GreaterOrEqual,EndOfOPeration,Assign,LesfBracket,EndOfF,Division,Minus,RightBracket,
            Seperator, Operator,Comma,DataType,
            Conjuction,
            Disjunction,
            ExcMark,
            For,
            While,
            As,
            Identificator,
            Var,
            EndFor,
            Print,
            To
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
            return Lexems.Identificator;
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
            else if (Reader.CurSymbol=='\r'||Reader.CurSymbol=='\n')
            {
                Reader.ReadNextSymbol();
                ParseNextLexem();
            }
            else if (Reader.CurSymbol == '<')
            {
                //currentLexem = Lexems.Less;
                Reader.ReadNextSymbol();
                if (Reader.CurSymbol == '=')
                {
                    currentLexem = Lexems.LessOrEqual;
                    currentName = "<=";
                    Reader.ReadNextSymbol();
                }
                else
                { currentName = "<"; currentLexem = Lexems.Less;
                    Reader.ReadNextSymbol();

                }
                    
            }
            else if (Reader.CurSymbol == '+')
            {
                currentLexem = Lexems.Plus;
                currentName = "+";
                Reader.ReadNextSymbol();
            }
            else if (Reader.CurSymbol == '-')
            {
                currentLexem = Lexems.Minus;
                currentName = "-";
                Reader.ReadNextSymbol();
            }
            else if (Reader.CurSymbol == '*')
            {
                currentLexem = Lexems.Multiplication;
                currentName = "*";
                Reader.ReadNextSymbol();
            }
            else if (Reader.CurSymbol == '/')
            {
                currentLexem = Lexems.Division;
                currentName = "/";
                Reader.ReadNextSymbol();
            }
            else if (Reader.CurSymbol == '>')
            {
                
                if (Reader.CurSymbol == '=')
                {
                    currentLexem = Lexems.GreaterOrEqual;
                    currentName = ">=";
                    Reader.ReadNextSymbol();
                }
                else
                {

                    currentLexem = Lexems.Greater;
                    currentName = ">";
                    Reader.ReadNextSymbol();
                }
            }
            else if (Reader.CurSymbol == '(')
            {
                currentLexem = Lexems.LesfBracket;
                currentName = "(";
                Reader.ReadNextSymbol();
            }
            else if (Reader.CurSymbol == ')')
            {
                currentLexem = Lexems.RightBracket;
                currentName = ")";
                Reader.ReadNextSymbol();
            }
            else if (Reader.CurSymbol==',')
            {
                currentLexem = Lexems.Comma;
                currentName = ",";
                Reader.ReadNextSymbol();
            }
            else if (Reader.CurSymbol == ';')
            {
                currentLexem = Lexems.EndOfOPeration;
                currentName = ";";
                Reader.ReadNextSymbol();
            }
            else if (Reader.CurSymbol == '&')
            {
                currentLexem = Lexems.Conjuction;
                currentName = "&";
                Reader.ReadNextSymbol();
            }
            else if (Reader.CurSymbol == '|')
            {
                currentLexem = Lexems.Disjunction;
                currentName = "|";
                Reader.ReadNextSymbol();
            }
            else if (Reader.CurSymbol == '!')
            {
                currentLexem = Lexems.ExcMark;
                currentName = "!";
                Reader.ReadNextSymbol();
            }
            else if (Reader.CurSymbol == 0)
            {
                currentLexem = Lexems.EndOfF;
            }
            else if (Reader.CurSymbol==':')
            {
                Reader.ReadNextSymbol();
                if (Reader.CurSymbol == '=') {
                    currentLexem = Lexems.Assign;
                    currentName = ":" + (char)Reader.CurSymbol;
                    Reader.ReadNextSymbol();
                }
            }
            else
                throw new Exception("Unknown Symbol"+(char)Reader.CurSymbol);
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
                Num += (char)Reader.CurSymbol;
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
            AddKeyWord("do", Lexems.Do);
            AddKeyWord("to", Lexems.To);
            AddKeyWord("for", Lexems.For);
            AddKeyWord("While", Lexems.While);
            AddKeyWord("as", Lexems.As);            
            AddKeyWord("var", Lexems.DataType);   
            AddKeyWord("integer",Lexems.DataType);
            AddKeyWord("endfor", Lexems.EndFor);
            AddKeyWord("if",Lexems.If);
            AddKeyWord("print", Lexems.Print);
            //ParseNextLexem();

        }
    }
}
