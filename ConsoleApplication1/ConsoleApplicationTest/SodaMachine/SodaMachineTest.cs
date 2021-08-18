using Microsoft.VisualStudio.TestTools.UnitTesting;
using ConsoleApplication1.Model;
using ConsoleApplication1.Model.Collection;
using ConsoleApplication1.Operations.DeliveryOptions;

namespace ConsoleApplication1.Test
{
    [TestClass]
    public class SodaMachineTest
    {
        [TestMethod]
        public void ZeroQuantityTest()
        {
            SodaCollection collection = new SodaCollection(false);
            Soda testSoda = new Soda() { Name = "test", Price = 10, Quantity = 0 };
            collection.AddSoda(testSoda);
            Assert.IsFalse(collection.CanDeliver(testSoda.Name, out Soda temp));
        }
        [TestMethod]
        public void PlusQuantityTest()
        {
            SodaCollection collection = new SodaCollection(false);
            Soda testSoda = new Soda() { Name = "test", Price = 10, Quantity = 1 };
            collection.AddSoda(testSoda);
            Assert.IsTrue(collection.CanDeliver(testSoda.Name, out Soda temp));
        }

        [TestMethod]
        public void ZeroCreditTest()
        {
            Soda testSoda = new Soda() { Name = "test", Price = 10, Quantity = 1 };

            Direct directDelivery = new Direct();

            Assert.IsFalse(directDelivery.CanDeliver(testSoda, 0));
        }
        [TestMethod]
        public void NotEnoughCreditTest()
        {
            Soda testSoda = new Soda() { Name = "test", Price = 10, Quantity = 1 };

            Direct directDelivery = new Direct();

            Assert.IsFalse(directDelivery.CanDeliver(testSoda, 9));
        }
        [TestMethod]
        public void EnoughCreditTest()
        {
            Soda testSoda = new Soda() { Name = "test", Price = 10, Quantity = 1 };

            Direct directDelivery = new Direct();

            Assert.IsFalse(directDelivery.CanDeliver(testSoda, 10));
        }
    }
}