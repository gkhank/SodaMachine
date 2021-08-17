using ConsoleApplication1.Model;
using ConsoleApplication1.Model.Collection;
using System;
using System.Collections.Generic;

namespace ConsoleApplication1.Operations
{
    class Order : IVendingOperation
    {
        public void DoOperation(SodaCollection sodaCollection, ref decimal credit)
        {
            ISoda selectedSoda;
            String input;
            do
            {
                Console.WriteLine("Please select a soda :");
                sodaCollection.ReportInventory();
                input = Console.ReadLine();
            }
            while (sodaCollection.CanDeliver(input, out selectedSoda) == false);


            DeliveryMethod? deliveryMethod = null;

            do
            {
                Console.WriteLine("Please select a delivery method :");
                String[] commands = Enum.GetNames(typeof(DeliveryMethod));
                for (int i = 0; i < commands.Length; i++)
                    Console.WriteLine("{0} - {1}", i + 1, commands[i]);

                input = Console.ReadLine();
            }
            while (Utils.Converter.TryConvert(input, out deliveryMethod) == false);


            if (this.HasEnoughCredit(credit, selectedSoda))
            {
                this.GiveSoda(credit, selectedSoda, deliveryMethod.Value);
            }
            else
            {
                Utils.Console.WriteRed("Current credit is missing {0} kronor for this soda. Please add more credit.", selectedSoda.Price - credit);
            }
        }

        private void GiveSoda(decimal currentCredit, ISoda selectedSoda, DeliveryMethod deliveryMethod)
        {
            if (selectedSoda.Quantity <= 0)
            {
                Utils.Console.WriteRed("Cannot deliver soda that with zero or less quantity.");
                return;
            }

            currentCredit -= selectedSoda.Price;
            Console.WriteLine("{0} kr left in credit. Please use 'Return' operation to get your change.", currentCredit);
            selectedSoda.Quantity--;


            switch (deliveryMethod)
            {
                case DeliveryMethod.Directly:
                    Utils.Console.WriteGreen("Giving soda out");
                    break;
                case DeliveryMethod.Sms:
                    Utils.Console.WriteGreen("Sending soda via SMS");
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        public bool HasEnoughCredit(Decimal currentCredit, ISoda selectedSoda)
        {
            return currentCredit >= selectedSoda.Price;
        }
    }
}
