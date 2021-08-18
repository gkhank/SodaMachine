using ConsoleApplication1.Model;
using System;

namespace ConsoleApplication1.Model

{
    public interface IDeliveryOption
    {
        void Deliver(Soda selectedSoda);
        void ModifyCredit(Soda selectedSoda, ref decimal credit);
        Boolean CanDeliver(Soda selectedSoda, decimal currentCredit);
    }
}
