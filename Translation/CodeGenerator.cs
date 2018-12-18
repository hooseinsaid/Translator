using System;
using System.Collections.Generic;
using System.Text;

namespace Translation
{
    public static class CodeGenerator
    {
        private static List<string> _Code = new List<string>(); //список, содержащий строки кода
        private static int _Labels = 0;

        /// <summary>
        /// возвращает массив строк кода
        /// </summary>
        public static string[] Code
        {
            get
            {
                return _Code.ToArray();
            }
        }

        private static int CodePointer = 0;

        /// <summary>
        /// метод, добавляющий строку кода в массив строк кода
        /// </summary>
        /// <param name="instruction">строка кода</param>
        public static void AddInstruction(string instruction)
        {
            _Code.Add(instruction);
            CodePointer++;
        }

        /// <summary>
        /// метод. объявляющий сегмент данных
        /// </summary>
        public static void DeclareDataSegment()
        {
            AddInstruction("data segment");
        }

        /// <summary>
        /// метод. объявляющий сегмент кода
        /// </summary>
        public static void DeclareStackCodeSegment()
        {
            AddInstruction("PRINT_BUF DB ' ' DUP(10)");
            AddInstruction("BUFEND DB '$'");
            AddInstruction("data ends");
            AddInstruction("stk segment stack");
            AddInstruction("db 256 dup (\"?\")");
            AddInstruction("stk ends");
            AddInstruction("code  segment");
            AddInstruction("assume cs:code, ds:data, ss:stk");
            AddInstruction("main proc");
            AddInstruction("mov ax,data");
            AddInstruction("mov ds,ax");
        }

        /// <summary>
        /// метод, объявляющий завершение основной процедуры
        /// </summary>
        public static void DeclareMainProcedureStopping()
        {
            AddInstruction("mov ax,4c00h");
            AddInstruction("int 21h");
            AddInstruction("main endp");
        }

        /// <summary>
        /// метод, декларирующий конец кода
        /// </summary>
        public static void DeclareCodeEnds()
        {
            AddInstruction("code ends");
            AddInstruction("end main");
        }

        /// <summary>
        /// метод, объявляющий процедуру печати
        /// </summary>
        public static void DeclarePrintProcedure()
        {
            AddInstruction("MOV CX,10");
            AddInstruction("MOV DI,BUFEND-PRINT_BUF");
            AddInstruction("PRINT_LOOP:");
            AddInstruction("MOV DX,0");
            AddInstruction("DIV   CX");
            AddInstruction("ADD   DL, '0'");
            AddInstruction("MOV   [PRINT_BUF + DI - 1], DL");
            AddInstruction("DEC   DI");
            AddInstruction("CMP   AL, 0");
            AddInstruction("JNE   PRINT_LOOP");
            AddInstruction("LEA   DX, PRINT_BUF");
            AddInstruction("ADD   DX, DI");
            AddInstruction("MOV   AH, 09H");
            AddInstruction("INT   21H");
        }

        /// <summary>
        /// метод, объявляющий объявление переменных
        /// </summary>
        public static void DeclareVariables()
        {
            //LinkedListNode<Identificator> node = NameTable.Identificators.First;
            //while (node != null)
            //{
            //    AddInstruction(node.Value.name + " dw 1 ");
            //    node = node.Next;
            //}
        }

        /// <summary>
        /// метод. инициализирующий генератор кода
        /// </summary>
        public static void Iitialize()
        {
            _Code.Clear();
            _Labels = 0;
        }

        /// <summary>
        /// метод, разбирающий постфиксную запис выражения
        /// с последующей генерацией кода
        /// </summary>
        /// <param name="Postfix">постфиксная запись</param>
        public static void AssigmentPostfixExprressionGeneration(List<string> Postfix)
        {
            //bool flagFirstTime = true;
            for (int i = 0; i < Postfix.Count; i++)
            {
                switch (Postfix[i])
                {
                    case "+":
                        {
                            //if(flagFirstTime)
                            //{
                            //AddInstruction("mov ax," + Postfix[i-2]);
                            //AddInstruction("add ax," + Postfix[i-1]);
                            //AddInstruction("push ax");
                            //flagFirstTime = false;
                            //break;
                            //}
                            //else
                            //{
                            AddInstruction("pop bx");
                            AddInstruction("pop ax");
                            AddInstruction("add ax,bx");
                            AddInstruction("push ax");
                            break;
                            //}
                        }
                    case "-":
                        {
                            //if(flagFirstTime)
                            //{
                            //    AddInstruction("mov ax," + Postfix[i-2]);
                            //    AddInstruction("sub ax," + Postfix[i-1]);
                            //    AddInstruction("push ax");
                            //    flagFirstTime = false;
                            //    break;
                            //}
                            //else
                            //{
                            AddInstruction("pop bx");
                            AddInstruction("pop ax");
                            AddInstruction("sub ax,bx");
                            AddInstruction("push ax");
                            break;
                            //}
                        }
                    case "*":
                        {
                            //if(flagFirstTime)
                            //{
                            //    AddInstruction("mov ax," + Postfix[i-2]);
                            //    AddInstruction("mul " + Postfix[i-1]);
                            //    AddInstruction("push ax");
                            //    flagFirstTime = false;
                            //    break;
                            //}
                            //else
                            //{
                            AddInstruction("pop bx");
                            AddInstruction("pop ax");
                            AddInstruction("mul bx");
                            AddInstruction("push ax");
                            break;
                            //}
                        }
                    case "/":
                        {
                            //if(flagFirstTime)
                            //{
                            //    AddInstruction("mov ax," + Postfix[i-2]);
                            //    AddInstruction("sub " + Postfix[i-1]);
                            //    AddInstruction("push ax");
                            //    flagFirstTime = false;
                            //    break;
                            //}
                            //else
                            //{
                            AddInstruction("pop bx");
                            AddInstruction("pop ax");
                            AddInstruction("div bx");
                            AddInstruction("push ax");
                            break;
                            //}
                        }
                    default:
                        {
                            AddInstruction("mov ax," + Postfix[i]);
                            AddInstruction("push ax");
                            break;
                        }
                }
            }
            AddInstruction("pop ax");
        }

        /// <summary>
        /// метод, добавляющий метку в код
        /// </summary>
        public static void Addlabel()
        {
            _Labels++;
            AddInstruction(string.Format("Label{0}:", _Labels));
        }

        /// <summary>
        /// метод. получающий последнюю метку
        /// </summary>
        /// <returns> последняя метка</returns>
        public static string GetCurrentlabel()
        {
            return "Label" + _Labels.ToString();
        }
    }
}
