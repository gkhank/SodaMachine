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

        public ISoda this[string key] => this._sodaCollection[key];

        /// <summary>
        /// This is the starter method for the machine
        /// </summary>
        public void Start(params object[] args)
        {

            while (true)
            {

                Operation? c;
                do
                {
                    Console.WriteLine("Please select an operation to proceed :");
                    String[] commands = Enum.GetNames(typeof(Operation));
                    for (int i = 0; i < commands.Length - 1; i++)
                        Console.WriteLine("{0} - {1}", i + 1, commands[i]);

                    Console.WriteLine("{0} - {1}", 0, commands.Last());
                }
                while (Converter.TryConvert(Console.ReadLine(), out c) == false);

                VendingOperationCollection collection = new VendingOperationCollection();

                if (collection.TryGetOperation(c.Value, out IVendingOperation operation))
                    operation.DoOperation(this._sodaCollection, ref this._currentCredit);
                else
                    throw new NotImplementedException();

            }
        }

    }

}
