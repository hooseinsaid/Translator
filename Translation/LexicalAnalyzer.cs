using System;
using System.Collections.Generic;

namespace Translation
{
    public static class LexicalAnalyzer
    {
        private static List<Keyword> Keywords;

        //private static int KeyWordPointer;
        public static Lexems currentLexem;

        public static string currentName;

        public enum Lexems
        {
            Do, Number, Begin, End, If, Then, Multiplication, Plus, Equal,
            Less, LessOrEqual, Greater, GreaterOrEqual, EndOfOPeration, Assign, LesfBracket, EndOfF, Division, Minus, RightBracket,
            Seperator, Operator, Comma, DataType, Conjuction, Disjunction, ExcMark,
            For, While, As, Identificator, Var, EndFor, Print, To, NotEqual, ElseIf, Else, EndIf,
            EndWhile
        }

        private struct Keyword
        {
            public string word;
            public Lexems lex;
        }

        public static void AddKeyWord(string Keyword, Lexems lexem)
        {
            Keyword kw = new Keyword
            {
                word = Keyword,
                lex = lexem
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
            while (Reader.CurSymbol == ' ')
            {
                Reader.ReadNextSymbol();
            }
            if (char.IsLetter((char)Reader.CurSymbol))
            {
                ParseID();
            }
            else if (char.IsDigit((char)Reader.CurSymbol))
            {
                ParseNumber();
            }
            else if (Reader.CurSymbol == '\r' || Reader.CurSymbol == '\n')
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
                    return;
                }
                else
                {
                    currentName = "<"; currentLexem = Lexems.Less;
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
                Reader.ReadNextSymbol();
                if (Reader.CurSymbol == '=')
                {
                    currentLexem = Lexems.GreaterOrEqual;
                    currentName = ">=";
                    Reader.ReadNextSymbol();
                    return;
                }
                    currentLexem = Lexems.Greater;
                    currentName = ">";
                   
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
            else if (Reader.CurSymbol == ',')
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
                if (Reader.CurSymbol == '=')
                {
                    currentLexem = Lexems.NotEqual;
                    currentName = "!" + (char)Reader.CurSymbol;
                    Reader.ReadNextSymbol();
                }
            }
            else if (Reader.CurSymbol == 0)
            {
                currentLexem = Lexems.EndOfF;
            }
            else if (Reader.CurSymbol == ':')
            {
                Reader.ReadNextSymbol();
                if (Reader.CurSymbol == '=')
                {
                    currentLexem = Lexems.Assign;
                    currentName = ":" + (char)Reader.CurSymbol;
                    Reader.ReadNextSymbol();
                }
            }
            else if (Reader.CurSymbol == '=')
            {
                Reader.ReadNextSymbol();
                if (Reader.CurSymbol == '=')
                {
                    currentName = "=" + (char)Reader.CurSymbol;
                    currentLexem = Lexems.Equal;
                    Reader.ReadNextSymbol();
                }
                else
                {
                    currentLexem = Lexems.Assign;
                    currentName = "" + (char)Reader.CurSymbol;
                }
            }
            else
                throw new Exception("Unknown Symbol " + (char)Reader.CurSymbol);
        }

        public static void ParseID()
        {
            string ID = "";
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
            AddKeyWord("integer", Lexems.DataType);
            AddKeyWord("endfor", Lexems.EndFor);
            AddKeyWord("if", Lexems.If);
            AddKeyWord("print", Lexems.Print);
            AddKeyWord("ElseIf", Lexems.ElseIf);
            AddKeyWord("endif", Lexems.EndIf);
            AddKeyWord("else", Lexems.Else);
            AddKeyWord("endwhile", Lexems.EndWhile)
;            //ParseNextLexem();
        }
    }
}