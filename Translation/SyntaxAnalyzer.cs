using System;
using System.Collections.Generic;
using System.Text;
using static Translation.LexicalAnalyzer;

namespace Translation
{
    public struct Error
    {
        public int Line;
        public int Position;
        public string ErrorMessage;
    }
    public static class SyntaxAnalyzer
    {
        public static List<Error> Errors { get; } = new List<Error>();
        private static void  Error(string ErrorMessage)
        {
            Errors.Add(new Error 
            {
                Line = Reader.NLines,
                Position = Reader.PosSimLine,
                ErrorMessage = ErrorMessage
            });
        }
        public static void CheckLexems(Lexems ExpectedLexem)
        {
            ParseNextLexem();
            if (currentLexem != ExpectedLexem)
            {
               
            }
            else
                ParseNextLexem();
        }
        public static void ParseVariablesDeclaration()
        {
            CheckLexems(Lexems.DataType);
            if (currentLexem!=Lexems.Identificator)
            {
                Error(" DataType Identificator  error message");
            }
            else
            {
                NameTable.Addidentifier(currentName, TCat.Var);
                ParseNextLexem();
            }
            while (currentLexem==Lexems.Comma)
            {
                ParseNextLexem();
                if (currentLexem != Lexems.Identificator)
                {
                    Error("Identificator  error message");
                }
                else
                {
                    NameTable.Addidentifier(currentName, TCat.Var);
                    ParseNextLexem();
                }
            }
            
        }

        public static void Compile()
        {
            Initialize();
            ParseVariablesDeclaration();
            CheckLexems(Lexems.EndOfOPeration);
        }
    }
}
