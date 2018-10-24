using System;
using System.Collections.Generic;
using System.Text;
using static Translation.LexicalAnalyzer;

namespace Translation
{
   public static class SyntaxAnalyzer
    {
         static List<string> ErrorCatcher = new List<string>(); 
        public static void CheckLexems(Lexems ExpectedLexem)
        {
            if (currentLexem != ExpectedLexem)
            {
                Error();
            }
            else
                ParseNextLexem();
        }
        public static void ParseVariablesDeclaration()
        {
            CheckLexems(Lexems.DataType);
            if (currentLexem!=Lexems.Identificator)
            {
                Error();
            }
            else
            {
                NameTable.Addidentifier(currentName, NameTable.TCat.Var);
                ParseNextLexem();
            }
            while (currentLexem==Lexems.Comma)
            {
                if (currentLexem != Lexems.Identificator)
                    Error();
                else
                {
                    NameTable.Addidentifier(currentName, NameTable.TCat.Var);
                }
            }
        }

        public static void Compile()
        {
            Initialize();
            ParseVariablesDeclaration();
            CheckLexems(Lexems.Seperator);
        }

        private static void Error()
        {
            throw new NotImplementedException();
        }
    }
}
