using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1.Model.Collection
{
    public class SodaCollection : IEnumerable<Soda>
    {
        private readonly Dictionary<String, Soda> _innerCollection;
        public SodaCollection(Boolean initInventory)
        {
            this._innerCollection = new Dictionary<string, Soda>();
            if (initInventory)
                this.InitInventory();
        }


        public Soda this[string key] => this._innerCollection[key];
        internal void ReportInventory()
        {

            int i = 1;
            foreach (Soda s in this)
            {
                Console.WriteLine("{3} - {0} ({1} item(s) left), {2}kr ", s.Name, s.Quantity, s.Price, i);
                i++;
            }

        }
        private void InitInventory()
        {
            //Get from source? database?
            this.AddSoda(new Soda() { Name = "Coke", Price = 20, Quantity = 5 });
            this.AddSoda(new Soda() { Name = "Sprite", Price = 15, Quantity = 3 });
            this.AddSoda(new Soda() { Name = "Fanta", Price = 13, Quantity = 1 });

            //var inventory = new[] { new Soda { Name = "coke", Nr = 5, Price = 20, Quantity = 10 }, new Soda { Name = "sprite", Nr = 3, Price = 15, Quantity }, new Soda { Name = "fanta", Nr = 3, Price = 12 } };

        }
        public void AddSoda(Soda soda)
        {
            string key = soda.Name.ToLower();
            if (this._innerCollection.ContainsKey(key))
            {
                if (soda.Price != this._innerCollection[key].Price)
                    throw new Exception("Soda prices are inconcurent.");

                this._innerCollection[key].Quantity += soda.Quantity;

            }
            else
                this._innerCollection.Add(soda.Name.ToLower(), soda);
        }
        private Boolean TryGetSodaByIndex(Int32 index, out Soda soda)
        {
            soda = this._innerCollection.Values.ElementAt(index - 1);
            return soda != null;
        }
        public IEnumerator<Soda> GetEnumerator()
        {
            return this._innerCollection.Values.GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this._innerCollection.Values.GetEnumerator();
        }
        public bool CanDeliver(string input, out Soda selectedSoda)
        {
            Boolean retval = ((this._innerCollection.TryGetValue(input, out selectedSoda) ||
                                Int32.TryParse(input, out Int32 index) && this.TryGetSodaByIndex(index, out selectedSoda))
                                &&
                                selectedSoda.Quantity > 0);

            if (selectedSoda != null && selectedSoda.Quantity <= 0)
                Utils.Console.WriteRed("{0} is out of stock. Try selecting another brand.", selectedSoda.Name);

            return retval;

        }
    }
}
