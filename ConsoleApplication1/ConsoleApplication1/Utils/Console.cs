using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1.Utils
{
    internal static class Console
    {
        internal static void WriteLine(string v, params object[] parameters)
        {
            System.Console.WriteLine(v, parameters);
        }
        internal static string ReadLine()
        {
            return System.Console.ReadLine();
        }
        internal static void WriteGreen(string v, params object[] parameters)
        {
            System.Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(v, parameters);
            System.Console.ResetColor();
        }
        internal static void WriteRed(string v, params object[] parameters)
        {
            System.Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(v, parameters);
            System.Console.ResetColor();
        }
    }
}
