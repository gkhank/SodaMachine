using Microsoft.VisualStudio.TestTools.UnitTesting;
using ConsoleApplication1.Model;
using ConsoleApplication1.Utils;

namespace ConsoleApplication1.Test
{
    [TestClass]
    public class SodaMachineTest
    {
        [TestMethod]
        public void ZeroQuantityTest()
        {
            SodaMachine machine = new SodaMachine(false);
            ISoda testSoda = new CocaCola() { Name = "test", Price = 10, Quantity = 0 };
            machine.AddSoda(testSoda);
            Assert.IsFalse(machine.CanDeliver(testSoda.Name, out ISoda temp));
        }
        [TestMethod]
        public void PlusQuantityTest()
        {
            SodaMachine machine = new SodaMachine(false);
            ISoda testSoda = new CocaCola() { Name = "test", Price = 10, Quantity = 1 };
            machine.AddSoda(testSoda);
            Assert.IsTrue(machine.CanDeliver(testSoda.Name, out ISoda temp));
        }

        [TestMethod]
        public void ZeroCreditTest()
        {
            SodaMachine machine = new SodaMachine(false);

            ISoda testSoda = new CocaCola() { Name = "test", Price = 10, Quantity = 1 };
            machine.AddSoda(testSoda);

            //Do not add credit

            Assert.IsFalse(machine.HasEnoughCredit(machine["test"]));
        }
        [TestMethod]
        public void NotEnoughCreditTest()
        {
            SodaMachine machine = new SodaMachine(false);

            ISoda testSoda = new CocaCola() { Name = "test", Price = 10, Quantity = 1 };
            machine.AddSoda(testSoda);

            //Add credit that is not enough
            machine.AddCredit(9);

            Assert.IsFalse(machine.HasEnoughCredit(machine["test"]));
        }
        [TestMethod]
        public void EnoughCreditTest()
        {
            SodaMachine machine = new SodaMachine(false);

            ISoda testSoda = new CocaCola() { Name = "test", Price = 10, Quantity = 1 };
            machine.AddSoda(testSoda);

            //Add credit that is enough for the purchase
            machine.AddCredit(10);

            Assert.IsTrue(machine.HasEnoughCredit(machine["test"]));
        }
    }
}