using ConsoleApplication1.Model;
using ConsoleApplication1.Model.Collection;
using System;

namespace ConsoleApplication1.Operations.Commands
{
    public class Return : IVendingOperation
    {
        public void DoOperation(SodaCollection sodaCollection, ref decimal credit)
        {
            Utils.Console.WriteGreen("Returning " + credit + " to customer");
            this.ReturnMoney(ref credit);
        }

        private void ReturnMoney(ref decimal credit)
        {
            credit = 0;
        }
    }
}
