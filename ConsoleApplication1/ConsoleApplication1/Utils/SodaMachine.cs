using ConsoleApplication1.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApplication1.Utils
{
    public class SodaMachine
    {
        private decimal _currentCredit;
        private Dictionary<String, ISoda> _sodaCollection = new Dictionary<string, ISoda>();

        public SodaMachine(Boolean initInventory = true)
        {
            this._sodaCollection = new Dictionary<string, ISoda>();
            if (initInventory)
                this.InitInventory();
        }

        public ISoda this[string key] => this._sodaCollection[key];

        /// <summary>
        /// This is the starter method for the machine
        /// </summary>
        public void Start()
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


                #region Old greeting
                //Console.WriteLine("\n\nAvailable commands:");
                //Console.WriteLine("insert (money) - Money put into money slot");
                //Console.WriteLine("order (coke, sprite, fanta) - Order from machines buttons");
                //Console.WriteLine("sms (coke, sprite, fanta) - Order sent by sms");
                //Console.WriteLine("recall - gives money back");
                //Console.WriteLine("-------");
                //Console.WriteLine("Inserted money: " + currentCredit);
                //Console.WriteLine("-------\n\n");
                #endregion


                switch (c)
                {
                    case Operation.Order:
                        this.SodaDeliveryOperation(c.Value);
                        break;

                    case Operation.Insert:

                        Decimal addingCredit;
                        do
                        {
                            Console.WriteLine("Please enter a valid amount to add as credit:");
                        }
                        while (Decimal.TryParse(Console.ReadLine(), out addingCredit) == false && addingCredit <= 0);

                        Console.WriteGreen("Added " + addingCredit + "kr to credit");
                        this.AddCredit(addingCredit);
                        break;

                    case Operation.Return:
                        Console.WriteGreen("Returning " + _currentCredit + " to customer");
                        this.ReturnMoney(_currentCredit);
                        _currentCredit = 0;

                        break;
                    case Operation.Exit:
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteRed("Unknown operation. Please try again");
                        break;
                }


            }
        }


        #region Private utils
        private void SodaDeliveryOperation(Operation c)
        {
            ISoda selectedSoda;
            String input;
            do
            {
                Console.WriteLine("Please select a soda :");
                this.ReportInventory();
                input = Console.ReadLine();
            }
            while (this.CanDeliver(input, out selectedSoda) == false);


            DeliveryMethod? deliveryMethod = null;

            do
            {
                Console.WriteLine("Please select a delivery method :");
                String[] commands = Enum.GetNames(typeof(DeliveryMethod));
                for (int i = 0; i < commands.Length; i++)
                    Console.WriteLine("{0} - {1}", i + 1, commands[i]);

                input = Console.ReadLine();
            }
            while (Converter.TryConvert(input, out deliveryMethod) == false);


            if (this.HasEnoughCredit(selectedSoda))
            {
                this.GiveSoda(selectedSoda, deliveryMethod.Value);
            }
            else
            {
                Console.WriteRed("Current credit is missing {0} kronor for this soda. Please add more credit.", selectedSoda.Price - _currentCredit);
            }
        }
        private void GiveSoda(ISoda selectedSoda, DeliveryMethod deliveryMethod)
        {
            if (selectedSoda.Quantity <= 0)
            {
                Console.WriteRed("Cannot deliver soda that with zero or less quantity.");
                return;
            }

            _currentCredit -= selectedSoda.Price;
            Console.WriteLine("{0} kr left in credit. Please use 'Return' operation to get your change.", _currentCredit);
            selectedSoda.Quantity--;


            switch (deliveryMethod)
            {
                case DeliveryMethod.Directly:
                    Console.WriteGreen("Giving soda out");
                    break;
                case DeliveryMethod.Sms:
                    Console.WriteGreen("Sending soda via SMS");
                    break;
                default:
                    throw new NotImplementedException();
            }
        }
        private void ReportInventory()
        {

            int i = 1;
            foreach (ISoda s in this._sodaCollection.Values)
            {
                Console.WriteLine("{3} - {0} ({1} item(s) left), {2}kr ", s.Name, s.Quantity, s.Price, i);
                i++;
            }

        }
        private Boolean TryGetSodaByIndex(Int32 index, out ISoda soda)
        {
            soda = this._sodaCollection.Values.ElementAt(index - 1);
            return soda != null;
        }
        private void ReturnMoney(decimal currentCredit)
        {
            //Return money here.
        }
        private void InitInventory()
        {
            //Get from source? database?
            this.AddSoda(new CocaCola() { Name = "Coke", Price = 20, Quantity = 5 });
            this.AddSoda(new Sprite() { Name = "Sprite", Price = 15, Quantity = 3 });
            this.AddSoda(new Fanta() { Name = "Fanta", Price = 13, Quantity = 1 });

            //var inventory = new[] { new Soda { Name = "coke", Nr = 5, Price = 20, Quantity = 10 }, new Soda { Name = "sprite", Nr = 3, Price = 15, Quantity }, new Soda { Name = "fanta", Nr = 3, Price = 12 } };

        }
        #endregion

        #region Public utils
        public void AddCredit(decimal v)
        {
            this._currentCredit += v;
        }
        public void AddSoda(ISoda soda)
        {
            string key = soda.Name.ToLower();
            if (this._sodaCollection.ContainsKey(key))
            {
                if (soda.Price != this._sodaCollection[key].Price)
                    throw new Exception("Soda prices are inconcurent.");

                this._sodaCollection[key].Quantity += soda.Quantity;

            }
            else
                this._sodaCollection.Add(soda.Name.ToLower(), soda);
        }
        public bool CanDeliver(string input, out ISoda selectedSoda)
        {
            Boolean retval = ((this._sodaCollection.TryGetValue(input, out selectedSoda) ||
                                Int32.TryParse(input, out Int32 index) && this.TryGetSodaByIndex(index, out selectedSoda))
                                &&
                                selectedSoda.Quantity > 0);

            if (selectedSoda != null && selectedSoda.Quantity <= 0)
                Console.WriteRed("{0} is out of stock. Try selecting another brand.", selectedSoda.Name);

            return retval;

        }
        public bool HasEnoughCredit(ISoda selectedSoda)
        {
            return this._currentCredit >= selectedSoda.Price;
        }
        #endregion
    }

}
