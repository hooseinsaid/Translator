using System;
using System.Collections.Generic;
using System.Text;
using static Translation.LexicalAnalyzer;

namespace Translation
{
   public static class SyntaxAnalyzer
    {
        public static void CheckLexems(Lexems ExpectedLexem)
        {
            if (currentLexem != ExpectedLexem)
            {
                Error();
            }
            else
                ParseNextLexem();
        }
        public static void ParseVariables()
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
        }

        private static void Error()
        {
            throw new NotImplementedException();
        }
    }
}
