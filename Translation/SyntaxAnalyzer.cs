using System.Collections.Generic;
using System.Diagnostics;
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
        public static string currentLabel { get; private set; }

        public static void Error(string ErrorMessage)
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
                Error("ожидалось " + ExpectedLexem.ToString());
            }
            else
                ParseNextLexem();
        }

        public static void ParseVariablesDeclaration()
        {
            CheckLexems(Lexems.DataType);
            if (currentLexem != Lexems.Identificator)
            {
                Error(" ожидалось имя переменной");
            }
            else
            {
                NameTable.Addidentifier(currentName, TCat.Var);
                ParseNextLexem();
            }
            while (currentLexem == Lexems.Comma)
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
            CodeGens.Initialize();
            NameTable.Initialize();
            Initialize();
            ParseVariablesDeclaration();
            CodeGens.DeclareVariables();
            CodeGens.DeclareSegmentStackCode();
            CheckLexems(Lexems.Begin);
            ParseInstructionsSequence();
            //  CheckLexems(Lexems.End);
            //CheckSyntax(Lexems.End);
            CodeGens.DeclareEndmainProcedure();
            CodeGens.DeclarePrintingPrecedure();
            CodeGens.DeclareEndOfCode();
        }

        private static void ParseInstructionsSequence()
        {
            while (currentLexem != Lexems.End & currentLexem != Lexems.EndOfF & currentLexem != Lexems.While)
            {
                ParseInstruction();
                ParseNextLexem();
            }
        }

        private static void ParseInstruction()
        {
            if (currentLexem == Lexems.Identificator)
            {
                Identifier identifier = Translation.NameTable.SearchByName(currentName);
                if (!identifier.Equals(new Identifier()))
                {
                    ParseInstructionAssignment();
                    CodeGens.AddInstructions("pop ax");
                    CodeGens.AddInstructions("mov " + identifier.name + ",ax");
                }
                else
                {
                    Error("необъявленная переменная " + currentName);
                }
            }
            else if (currentLexem == Lexems.Print)
                ParsePrintInstruction();
            else if (currentLexem == Lexems.If)
                ParseBranching();
            else if (currentLexem == Lexems.Do)
                ParseLoop();
        }

        private static void ParseInstructionAssignment()
        {
            ParseNextLexem();
            CheckSyntax(Lexems.Assign);
            ParseNextLexem();
            ParseExpression();
        }

        private static TType ParseExpression()
        {
            TType T = ParseAdditionsAndSubstractions();
            if (currentLexem == Lexems.NotEqual || currentLexem == Lexems.Less || currentLexem == Lexems.LessOrEqual ||
                currentLexem == Lexems.Equal || currentLexem == Lexems.Greater || currentLexem == Lexems.GreaterOrEqual
                )
            {
                string transition = "";
                switch (currentLexem)
                {
                    case Lexems.Equal:
                        transition = "jne";
                        break;

                    case Lexems.NotEqual:
                        transition = "je";
                        break;

                    case Lexems.Greater:
                        transition = "jle";
                        break;

                    case Lexems.GreaterOrEqual:
                        transition = "jl";
                        break;

                    case Lexems.Less:
                        transition = "jge";
                        break;

                    case Lexems.LessOrEqual:
                        transition = "jg";
                        break;
                }
                ParseNextLexem();
                ParseAdditionsAndSubstractions();
                CodeGens.AddInstructions("pop ax");
                CodeGens.AddInstructions("pop bx");
                CodeGens.AddInstructions("cmp bx, ax");
                CodeGens.AddInstructions(transition + " " + currentLabel);
                currentLabel = "";
                T = TType.Bool;
            }
            return T;
        }

        public static TType ParseSubExpression()
        {
            Identifier X;
            TType T = TType.None;
            if (currentLexem == Lexems.Identificator)
            {
                X = NameTable.SearchByName(currentName);
                if (!X.Equals(new Identifier()) & X.cat == TCat.Var)
                {
                    CodeGens.AddInstructions("mov ax," + currentName);
                    CodeGens.AddInstructions("push ax");
                    ParseNextLexem();
                    return X.type;
                }
                else

                    Error("unknown parameter");
                return T;
            }
            else if (currentLexem == Lexems.Number)
            {
                CodeGens.AddInstructions("mov ax," + currentName);
                CodeGens.AddInstructions("push ax");
                ParseNextLexem();
                return TType.Int;
            }
            else if (currentLexem == Lexems.LesfBracket)
            {
                ParseNextLexem();
                T = ParseExpression();
                if (currentLexem != Lexems.RightBracket)
                {
                    Error("Unknown  " + currentLexem.ToString() + " '" + currentName + "'");
                }
                else
                    ParseNextLexem();
            }
            else
                Error("Unknwon parameter");
            return T;
        }

        private static TType ParseAdditionsAndSubstractions()
        {
            TType t;
            Lexems operator_;
            if (currentLexem == Lexems.Plus || currentLexem == Lexems.Minus)
            {
                operator_ = currentLexem;
                ParseNextLexem();
                t = ParseMultiplicationAndDivision();
            }
            else
                t = ParseMultiplicationAndDivision();
            if (currentLexem == Lexems.Plus || currentLexem == Lexems.Minus)
            {
                do
                {
                    operator_ = currentLexem;
                    ParseNextLexem();
                    t = ParseMultiplicationAndDivision();
                    switch (operator_)
                    {
                        case Lexems.Plus:
                            CodeGens.AddInstructions("pop bx");
                            CodeGens.AddInstructions("pop ax");
                            CodeGens.AddInstructions("add ax,bx");
                            CodeGens.AddInstructions("push ax");
                            break;

                        case Lexems.Minus:
                            CodeGens.AddInstructions("pop bx");
                            CodeGens.AddInstructions("pop ax");
                            CodeGens.AddInstructions("sub ax,bx");
                            CodeGens.AddInstructions("push ax");
                            break;
                    }
                } while (currentLexem == Lexems.Plus || currentLexem == Lexems.Minus);
            }
            return t;
        }

        private static TType ParseMultiplicationAndDivision()
        {
            Lexems operator_;
            TType t = ParseSubExpression();
            if (currentLexem == Lexems.Multiplication || currentLexem == Lexems.Division)
            {
                do
                {
                    operator_ = currentLexem;
                    ParseNextLexem();
                    t = ParseSubExpression();
                    switch (operator_)
                    {
                        case Lexems.Multiplication:
                            CodeGens.AddInstructions("pop bx");
                            CodeGens.AddInstructions("pop ax");
                            CodeGens.AddInstructions("mul bx");
                            CodeGens.AddInstructions("push ax");
                            break;

                        case Lexems.Division:
                            CodeGens.AddInstructions("pop bx");
                            CodeGens.AddInstructions("pop ax");
                            CodeGens.AddInstructions("cwd");
                            CodeGens.AddInstructions("div bl");
                            CodeGens.AddInstructions("push ax");
                            break;

                        default:
                            break;
                    }
                } while (currentLexem == Lexems.Multiplication || currentLexem == Lexems.Division);
            }
            return t;
        }

        public static void ParseBranching()
        {
            //    //CheckLexems(Lexems.If);
            //    ParseNextLexem();
            //    ParseExpression();
            // CodeGens.AddLabel();
            // string lowlable=CodeGens.GetCurrentLabel();
            // currentLabel=lowlable;

            //    CheckLexems(Lexems.Then);
            //    ParseInstructionsSequence();
            //    while (currentLexem == Lexems.ElseIf)
            //    {
            //        ParseExpression();
            //        CheckLexems(Lexems.Then);
            //        ParseInstructionsSequence();
            //    }
            //    if (currentLexem == Lexems.Else)
            //    {
            //        ParseNextLexem();
            //        ParseInstructionsSequence();
            //    }
            //    CheckLexems(Lexems.EndIf);
            ParseNextLexem();
            CodeGens.AddLabel();
            string lowestLabel = CodeGens.GetCurrentLabel();
            currentLabel = lowestLabel;
            CodeGens.AddLabel();
            string exitLabel = CodeGens.GetCurrentLabel();
            ParseExpression();
            CheckSyntax(Lexems.Then);
            ParseInstructionsSequence();
            CodeGens.AddInstructions("jmp " + exitLabel);
            while (currentLexem == Lexems.ElseIf)
            {
                CodeGens.AddInstructions(lowestLabel + ":");
                CodeGens.AddLabel();
                lowestLabel = CodeGens.GetCurrentLabel();
                currentLabel = lowestLabel;
                ParseNextLexem();
                ParseExpression();
                CheckSyntax(Lexems.Then);
                ParseInstructionsSequence();
                CodeGens.AddInstructions("jmp " + exitLabel);
                
            }
            if(currentLexem== Lexems.Else)
            {
                CodeGens.AddInstructions(lowestLabel + ":");
               // ParseNextLexem();
                ParseInstructionsSequence();
            }
            CheckSyntax(Lexems.EndIf);
            CodeGenerator.AddInstruction(exitLabel + ":");
        }

        public static void ParseLoop()
        {
            ParseNextLexem();
            CodeGens.AddLabel();
            string TopLabel = CodeGens.GetCurrentLabel();
            CodeGens.AddLabel();
            string LowLabel = CodeGens.GetCurrentLabel();
            CodeGens.AddLabel();
            string Thirdlabel=CodeGens.GetCurrentLabel();
            currentLabel = Thirdlabel;
            CodeGens.AddInstructions("jmp " + LowLabel);
            CodeGens.AddInstructions(TopLabel + ":");
            //ParseExpression();
            ParseInstructionsSequence();
           // 
            CheckSyntax(Lexems.While);
            ParseNextLexem();
            CodeGens.AddInstructions(LowLabel + ":");
            ParseExpression();
            CodeGens.AddInstructions("jmp "+TopLabel);
            CodeGens.AddInstructions(Thirdlabel+":");
            //{
            //    ParseNextLexem();
            //    CodeGens.AddLabel();
            //    string label = CodeGens.GetCurrentLabel();
            //    CodeGens.AddInstructions(label + ":");
            //    ParseInstructionsSequence();
            //    CheckLexems(Lexems.While);
            //    ParseExpression();
            //}
        }
        public static void ParseDoWhile()
        {

        }

        private static void ParsePrintInstruction()
        {
            // CheckSyntax(Lexems.Identificator);
            ParseNextLexem();
            if (currentLexem == Lexems.Identificator)
            {
                Identifier X = NameTable.SearchByName(currentName);
                if (!X.Equals(new Identifier()))
                {
                    CodeGens.AddInstructions("push ax");
                    CodeGens.AddInstructions("mov ax," + currentName);
                    CodeGens.AddInstructions("CALL PRINT");
                    CodeGens.AddInstructions("pop ax");
                    ParseNextLexem();
                }
            }
            else
                Error("unkown parameter " + currentName);
        }

        public static void CheckSyntax(Lexems lexemcheck)
        {
            if (currentLexem != lexemcheck)
            {
                Error(" ожидолос " + lexemcheck);
            }
            // else
            //  ParseNextLexem();
        }
    }
}