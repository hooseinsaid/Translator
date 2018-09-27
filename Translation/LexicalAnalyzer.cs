using System;
using System.Collections.Generic;

namespace Translation
{
    public static class LexicalAnalyzer
    {
        /// <summary>
        /// Перечисление содержащие все лексемы
        /// </summary>
        public enum Lexems
        {
            None, Name, Number, Begin, End, If, ElseIf, Else, EndIf, While, Then, Multi, Division, Plus, Minus, Do, EndWhile, For, EndFor, Bool, To,
            Equal, NotEqual, More, Less, MoreOrEqual, LessOrEqual, Semi, Assign, LeftBracket, RightBracket, EOF, Separator, Print, Comma, DataType, Until, EndUntil,
            Conjunction, Disjunction, XOR, Var, Colon, ExcPoint
        }

        /// <summary>
        /// Сопоставляет ключевое слово с лексемой определённого типа
        /// </summary>
        private struct Keyword
        {
            public string word; // Слово
            public Lexems lex;  // Лексема
        }

        private static List<Keyword> keywords;          // Ключевые слова
        public static Lexems currentLexem { get; set; } // Текущая лексема
        public static string currentName { get; set; }  // Текущее имя

        /// <summary>
        /// Добавить ключевое слово
        /// </summary>
        /// <param name="key">Ключевое слово</param>
        /// <param name="lexem">Лексема</param>
        public static void AddKeyword(string key, Lexems lexem)
        {
            Keyword keyword = new Keyword();
            keyword.word = key;
            keyword.lex = lexem;
            keywords.Add(keyword);
        }

        /// <summary>
        /// Получить ключевое слово
        /// </summary>
        /// <param name="key">Ключевое слово</param>
        /// <returns>Лексема Name</returns>
        public static Lexems GetKeyword(string key)
        {
            foreach (Keyword keyword in keywords)
                if (keyword.word.ToLower().Equals(key.ToLower()))
                    return keyword.lex;
            return Lexems.Name;
        }

        /// <summary>
        /// Разобрать следующую лексему
        /// </summary>
        public static void ParseNextLexem()
        {
            while (Reader.CurSymbol == ' ')
                Reader.ReadNextSymbol();
            if (Char.IsLetter((char)Reader.CurSymbol))
                ParseIdentifier();
            else if (Char.IsDigit((char)Reader.CurSymbol))
                ParseNumber();
            else if (Reader.CurSymbol == '\n')
            {
                Reader.ReadNextSymbol();
                currentLexem = Lexems.Separator;
            }
            else if (Reader.CurSymbol == '(')
            {
                Reader.ReadNextSymbol();
                currentLexem = Lexems.LeftBracket;
            }
            else if (Reader.CurSymbol == ')')
            {
                Reader.ReadNextSymbol();
                currentLexem = Lexems.RightBracket;
            }
            else if (Reader.CurSymbol == ',')
            {
                Reader.ReadNextSymbol();
                currentLexem = Lexems.Comma;
            }
            else if (Reader.CurSymbol == ';')
            {
                Reader.ReadNextSymbol();
                currentLexem = Lexems.Semi;
            }
            else if (Reader.CurSymbol == '*')
            {
                Reader.ReadNextSymbol();
                currentLexem = Lexems.Multi;
            }
            else if (Reader.CurSymbol == '/')
            {
                Reader.ReadNextSymbol();
                currentLexem = Lexems.Division;
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
            else if (Reader.CurSymbol == '>')
            {
                Reader.ReadNextSymbol();
                if (Reader.CurSymbol == '=')
                {
                    Reader.ReadNextSymbol();
                    currentLexem = Lexems.MoreOrEqual;
                }
                else
                    currentLexem = Lexems.More;
            }
            else if (Reader.CurSymbol == '=')
            {
                Reader.ReadNextSymbol();
                currentLexem = Lexems.Assign;
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
            else if (Reader.CurSymbol == '^')
            {
                Reader.ReadNextSymbol();
                currentLexem = Lexems.XOR;
            }
            else if (Reader.CurSymbol == '|')
            {
                Reader.ReadNextSymbol();
                currentLexem = Lexems.Disjunction;
            }
            else if (Reader.CurSymbol == '&')
            {
                Reader.ReadNextSymbol();
                currentLexem = Lexems.Conjunction;
            }
            else if (Reader.CurSymbol == ':')
            {
                Reader.ReadNextSymbol();
                ParseIdentifier();
                //currentLexem = Lexems.Colon;
            }
            else if (Reader.CurSymbol == '!')
            {
                Reader.ReadNextSymbol();
                currentLexem = Lexems.ExcPoint;
            }
            else if (Reader.CurSymbol == 0)
            {
                currentLexem = Lexems.EOF;
            }
            else
                throw new Exception("Incorrect symbol");
        }

        /// <summary>
        /// Разобрать число
        /// </summary>
        public static void ParseNumber()
        {
            string number = "";
            do
            {
                number += (char)Reader.CurSymbol;
                Reader.ReadNextSymbol();
            }
            while (Char.IsDigit((char)Reader.CurSymbol));
            // Проверка на переполнение типа int
            /*if (!int.TryParse(number, out int rnum))
            {
                throw new Exception("Impossible to convert number.");
            }*/
            currentName = number;
            currentLexem = Lexems.Number;
        }

        /// <summary>
        /// Разобрать идентификатор
        /// </summary>
        public static void ParseIdentifier()
        {
            string identifier = "";
            do
            {
                identifier += (char)Reader.CurSymbol;
                Reader.ReadNextSymbol();
            }
            while (Char.IsLetter((char)Reader.CurSymbol));
            currentName = identifier;
            currentLexem = GetKeyword(identifier);
        }

        /// <summary>
        /// Инициализируем ключевые слова
        /// </summary>
        public static void Initialize()
        {
            keywords = new List<Keyword>();
            AddKeyword("begin", Lexems.Begin);
            AddKeyword("int", Lexems.DataType);
            AddKeyword("print", Lexems.Print);
            AddKeyword("end", Lexems.End);
            AddKeyword("while", Lexems.While);
            AddKeyword("if", Lexems.If);
            AddKeyword("else", Lexems.Else);
            AddKeyword("elseif", Lexems.ElseIf);
            AddKeyword("endif", Lexems.EndIf);
            AddKeyword("then", Lexems.Then);
            AddKeyword("endwhile", Lexems.EndWhile);
            AddKeyword("bool", Lexems.DataType);
            AddKeyword("until", Lexems.Until);
            AddKeyword("enduntil", Lexems.EndUntil);
            AddKeyword("for", Lexems.For);
            AddKeyword("endfor", Lexems.EndFor);
            AddKeyword("to", Lexems.To);
            AddKeyword("do", Lexems.Do);
            AddKeyword("var", Lexems.Var);
            //AddKeyword(":", Lexems.Colon);
            AddKeyword("logical", Lexems.DataType);
            AddKeyword("integer", Lexems.DataType);
            ParseNextLexem();
        }
    }
}
