using ConsoleApplication1.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1.Operations.DeliveryOptions
{
    public class Direct : IDeliveryOption
    {

        public void Deliver(Soda selectedSoda)
        {
            selectedSoda.Quantity--;
            Utils.Console.WriteGreen("Delivering {0} directly", selectedSoda.Name);
        }

        public void ModifyCredit(Soda selectedSoda, ref decimal credit)
        {
            credit -= selectedSoda.Price;
        }
        public bool CanDeliver(Soda selectedSoda, decimal currentCredit)
        {
            return currentCredit >= selectedSoda.Price;
        }
    }
}
