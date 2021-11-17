using ConsoleApplication1.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            SodaMachine sodaMachine = new SodaMachine();
            sodaMachine.Start(args);
        }
    }
}



//-	Resolve dependencies, do necessary changes so that it is easy to add
//- New vending functions
//- New soda types
//-	Add unit tests testing important parts of the code
//-	Make the code “pretty”, so that it’s easy for other developers to take it over, and maintain it in the future
