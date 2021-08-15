using ConsoleApplication1.Model;
using ConsoleApplication1.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ConsoleApplication1.Test
{

    [TestClass]
    public class SodaMachineTest
    {

        [TestMethod]
        public void ZeroQuantityTest() {

            SodaMachine machine = new SodaMachine(false);
            machine.AddSoda("Test", 10, 0);
            Assert.IsFalse(machine.CanDeliver("test", out Soda temp));
        }
        [TestMethod]
        public void PlusQuantityTest()
        {
            SodaMachine machine = new SodaMachine(false);
            machine.AddSoda("Test", 10, 1);
            Assert.IsTrue(machine.CanDeliver("test", out Soda temp));
        }
        [TestMethod]
        public void NotEnoughCreditTest()
        {
            SodaMachine machine = new SodaMachine(false);
            machine.AddSoda("Test", 10, 1);
            Assert.IsFalse(machine.HasEnoughCredit(machine["test"]));
        }
        [TestMethod]
        public void EnoughCreditTest()
        {
            SodaMachine machine = new SodaMachine(false);
            machine.AddSoda("Test", 10, 1);
            machine.AddCredit(10);
            Assert.IsTrue(machine.HasEnoughCredit(machine["test"]));
        }
    }
}
