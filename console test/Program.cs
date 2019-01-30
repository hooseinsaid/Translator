using System;
using Translation;

namespace console_test
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Test _test = new Test();
            string[] values = _test.ToString().Split('#');

            foreach (var item in values)
            {
                Console.WriteLine(item);
            }
        }
    }
}