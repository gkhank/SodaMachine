using ConsoleApplication1.Model;
using ConsoleApplication1.Model.Collection;
using System;
using System.Collections.Generic;

namespace ConsoleApplication1.Operations.Commands
{
    public class Order : IVendingOperation
    {
        public void DoOperation(SodaCollection sodaCollection, ref decimal credit)
        {
            Soda selectedSoda;
            String input;
            do
            {
                Console.WriteLine("Please select a soda :");
                sodaCollection.ReportInventory();
                input = Console.ReadLine();
            }
            while (sodaCollection.CanDeliver(input, out selectedSoda) == false);


            AssemblyCollection<IDeliveryOption> collection = new AssemblyCollection<IDeliveryOption>("ConsoleApplication1.Operations.DeliveryOptions");

            IDeliveryOption deliveryOption = null;
            do
            {
                Console.WriteLine("Please select an operation to proceed :");
                collection.PrintOptions();
                input = Console.ReadLine();

            }
            while (((Int32.TryParse(input, out Int32 intInput) && collection.TryGetByIndex(intInput, out deliveryOption)) == false) &&
                    collection.TryGet(input, out deliveryOption) == false);


            if (deliveryOption.CanDeliver(selectedSoda, credit))
            {
                deliveryOption.Deliver(selectedSoda);
                deliveryOption.ModifyCredit(selectedSoda, ref credit);
            }
            else
            {
                Utils.Console.WriteRed("Current credit is missing {0} kronor for this soda. Please add more credit.", selectedSoda.Price - credit);
            }
        }

    }
}
