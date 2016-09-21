using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VendingMachine;

namespace TestVendingMachine
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestDeposit1()
        {
            var bm = new BeerMachine();
            Assert.AreEqual(true, bm.DepositCoin(1));
        }

        [TestMethod]
        public void TestDeposit2()
        {
            var bm = new BeerMachine();
            Assert.AreEqual(true, bm.DepositCoin(2));
        }

        [TestMethod]
        public void TestDeposit5()
        {
            var bm = new BeerMachine();
            Assert.AreEqual(true, bm.DepositCoin(5));
        }

        [TestMethod]
        public void TestDeposit10()
        {
            var bm = new BeerMachine();
            Assert.AreEqual(true, bm.DepositCoin(10));
        }

        [TestMethod]
        public void TestDeposit3()
        {
            var bm = new BeerMachine();
            Assert.AreEqual(false, bm.DepositCoin(3));
        }

        [TestMethod]
        public void TestcheckEnoughToBuy1()
        {
            var bm = new BeerMachine();
            bm.DepositedTotal = 10;
            Assert.AreEqual(false, bm.checkEnoughToBuy('L'));
        }

        [TestMethod]
        public void TestcheckEnoughToBuy2()
        {
            var bm = new BeerMachine();
            bm.DepositedTotal = 15;
            Assert.AreEqual(true, bm.checkEnoughToBuy('L'));
        }

        [TestMethod]
        public void TestcheckEnoughToBuy3()
        {
            var bm = new BeerMachine();
            bm.DepositedTotal = 15;
            Assert.AreEqual(false, bm.checkEnoughToBuy('G'));
        }

        [TestMethod]
        public void TestReturnChange1()
        {
            var bm = new BeerMachine();
            bm.DepositedTotal = 15;
            bm.ReturnChange('L');
            Assert.AreEqual(3, bm.DepositedTotal);
        }

        [TestMethod]
        public void TestReturnChange2()
        {
            var bm = new BeerMachine();
            bm.DepositedTotal = 15;
            bm.ReturnChange('B');
            Assert.AreEqual(0, bm.DepositedTotal);
        }

        [TestMethod]
        public void TestReduceInventory1()
        {
            var bm = new BeerMachine();
            bm.DepositedTotal = 15;
            bm.ReduceInventory('B');
            var b = bm.GetBeerByCode('B');
            Assert.AreEqual(9, b.Inventory);
            bm.ReduceInventory('B');
            Assert.AreEqual(8, b.Inventory);
        }

        [TestMethod]
        public void TestAddInventory()
        {
            var bm = new BeerMachine();
            bm.DepositedTotal = 15;
            bm.ReduceInventory('B');
            var b = bm.GetBeerByCode('B');
            Assert.AreEqual(9, b.Inventory);
            bm.AddInventory('B', 1);
            Assert.AreEqual(10, b.Inventory);
        }

    }
}
