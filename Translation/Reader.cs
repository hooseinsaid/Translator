using System.IO;

namespace Translation
{
    public class Reader
    {
        public static int NLines = 0, PosSimLine = 0, CurSymbol = 0;
        private static StreamReader reader;

        public static void ReadNextSymbol()
        {
            CurSymbol = (char)reader.Read();
            if (CurSymbol == char.MaxValue)
            {
                CurSymbol = 0;
                return;
            }

            else if (CurSymbol == '\n')
            {
                NLines++;
                PosSimLine = 0;
                return;
            }
            else if (CurSymbol == '\r' || CurSymbol == '\t')
            {
                PosSimLine = 0;
            }
            else
                PosSimLine++;
        }

        public static void Initialize(string filepath)
        {
            try
            {
                if (File.Exists(filepath))
                {
                    reader = new StreamReader(filepath);
                    NLines = 1;
                    PosSimLine = 0;
                    ReadNextSymbol();
                }
            }
            catch
            {
                throw new FileNotFoundException("File not found ");
            }
        }

        public static void Close()
        {
            reader.Close();
        }
    }
}