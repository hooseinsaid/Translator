using System;
using System.IO;

namespace Translation
{
    public class Reader
    {
        public static int NLines=0, PosSimLine=0, CurSymbol=0;
        static StreamReader reader;
        public static void  ReadNextSymbol()
        {
            CurSymbol = reader.Read();
            if (CurSymbol == -1)
            {
                CurSymbol = 0;
            }
            else if (CurSymbol == '\n')
            {
                NLines++;
                PosSimLine = 0;
            }
            else if (CurSymbol == '\r' || CurSymbol == '\t')
            {
                ReadNextSymbol();
            }
            else
                PosSimLine++;
            
        }
        public static  void Initialize(string filepath)
        {
            if(File.Exists(filepath))
            {
                reader = new StreamReader(filepath);
                NLines = 1;
                PosSimLine = 0;
                ReadNextSymbol();
            }
        }
        public  static void Close()
        {
            reader.Close();
        }
    }
}
