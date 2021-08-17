using ConsoleApplication1.Model;
using ConsoleApplication1.Model.Collection;
using System;

namespace ConsoleApplication1.Operations
{
    class Insert : IVendingOperation
    {
        public void DoOperation(SodaCollection sodaCollection, ref decimal credit)
        {
            Decimal addingCredit;
            do
            {
                Console.WriteLine("Please enter a valid amount to add as credit:");
            }
            while (Decimal.TryParse(Console.ReadLine(), out addingCredit) == false && addingCredit <= 0);

            Utils.Console.WriteGreen("Added " + addingCredit + "kr to credit");
            this.AddCredit(ref credit, addingCredit);
        }

        private void AddCredit(ref decimal credit, decimal addingCredit)
        {
            credit += addingCredit;
        }
    }
}
