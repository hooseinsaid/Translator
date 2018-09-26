using System;
using System.IO;

namespace Translation
{
    public class Reader
    {
        public static int NLines=0, PosSimLine=0, CrSymbol=0;
        static StreamReader reader;
        public static void  ReadNextSymbol()
        {
            CrSymbol = reader.Read();
            if (CrSymbol == -1)
            {
                CrSymbol = 0;
            }
            else if (CrSymbol == '\n')
            {
                NLines++;
                PosSimLine = 0;
            }
            else if (CrSymbol == '\r' || CrSymbol == '\t')
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
