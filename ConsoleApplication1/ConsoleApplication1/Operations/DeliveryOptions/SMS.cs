using ConsoleApplication1.Model;
using System;

namespace ConsoleApplication1.Operations.DeliveryOptions
{
    public class SMS : IDeliveryOption
    {
        public bool CanDeliver(Soda selectedSoda, decimal currentCredit)
        {
            selectedSoda.Quantity--;
            return true;
        }

        public void Deliver(Soda selectedSoda)
        {
            Utils.Console.WriteGreen("Delivering soda via SMS, no credit deduction will be made");
        }

        public void ModifyCredit(Soda selectedSoda, ref decimal credit)
        {
        }
    }
}
