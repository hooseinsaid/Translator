using System.Collections.Generic;

namespace Translation
{
    public class CodeGens
    {
        public static int MAX_NUMBER_STRINGS { get; private set; } = 10000;
        private static int countLabels = 0;
        public static string[] code;
        private static int codePointer = 0;

        public static void AddInstructions(string instruction)
        {
            code[codePointer++] = instruction;
        }

        public static void AddLabel()
        {
            countLabels++;
        }

        public static string GetCurrentLabel()
        {
            return "label" + countLabels.ToString();
        }

        private static void DeclaresegmentCode()
        {
            AddInstructions("data segment para public \"data\"");
        }

        public static void DeclareSegmentStackCode()
        {
            AddInstructions("PRINT_BUF DB ' ' DUP(10)");
            AddInstructions("BUFEND    DB '$'");
            AddInstructions("data ends");
            AddInstructions("stk segment stack");
            AddInstructions("db 256 dup (\"?\")");
            AddInstructions("stk ends");
            AddInstructions("code segment para public \"code\"");
            AddInstructions("main proc");
            AddInstructions("assume cs:code,ds:data,ss:stk");
            AddInstructions("mov ax,data");
            AddInstructions("mov ds,ax");
        }

        public static void DeclareEndmainProcedure()
        {
            AddInstructions("mov ax,4c00h");
            AddInstructions("int 21h");
            AddInstructions("main endp");
        }

        public static void DeclareEndOfCode()
        {
            AddInstructions("code ends");
            AddInstructions("end main");
        }

        public static void DeclareVariables()
        {
            LinkedListNode<Identifier> node = NameTable.Identifiers.First;
            while (node != null)
            {
                AddInstructions(node.Value.name + " dw 1");
                node = node.Next;
            }
        }

        public static void DeclarePrintingPrecedure()
        {
            AddInstructions("PRINT PROC NEAR");
            AddInstructions("MOV   CX, 10");
            AddInstructions("MOV   DI, BUFEND - PRINT_BUF");
            AddInstructions("PRINT_LOOP:");
            AddInstructions("MOV   DX, 0");
            AddInstructions("DIV   CX");
            AddInstructions("ADD   DL, '0'");
            AddInstructions("MOV   [PRINT_BUF + DI - 1], DL");
            AddInstructions("DEC   DI");
            AddInstructions("CMP   AL, 0");
            AddInstructions("JNE   PRINT_LOOP");
            AddInstructions("LEA   DX, PRINT_BUF");
            AddInstructions("ADD   DX, DI");
            AddInstructions("MOV   AH, 09H");
            AddInstructions("INT   21H");
            AddInstructions("RET");
            AddInstructions("PRINT ENDP");
        }

        public static void Initialize()
        {
            code = new string[MAX_NUMBER_STRINGS];
            codePointer = 0;
            DeclaresegmentCode();
        }
    }
}