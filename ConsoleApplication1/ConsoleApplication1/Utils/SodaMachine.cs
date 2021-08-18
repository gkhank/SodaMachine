using ConsoleApplication1.Model;
using ConsoleApplication1.Model.Collection;
using ConsoleApplication1.Operations;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApplication1.Utils
{
    public class SodaMachine
    {
        private decimal _currentCredit;
        private readonly SodaCollection _sodaCollection;

        public SodaMachine(Boolean initInventory = true)
        {
            this._sodaCollection = new SodaCollection(initInventory);
        }

        /// <summary>
        /// This is the starter method for the machine
        /// </summary>
        public void Start(params object[] args)
        {

            while (true)
            {
                AssemblyCollection<IVendingOperation> collection = new AssemblyCollection<IVendingOperation>("ConsoleApplication1.Operations.Commands");

                IVendingOperation operation = null;
                string input;
                do
                {
                    Console.WriteLine("Please select an operation to proceed :");
                    collection.PrintOptions();
                    input = Console.ReadLine();

                }
                while (((Int32.TryParse(input, out Int32 intInput) && collection.TryGetByIndex(intInput, out operation)) == false) &&
                        collection.TryGet(input, out operation) == false);

                operation.DoOperation(this._sodaCollection, ref this._currentCredit);

            }
        }

    }

}
