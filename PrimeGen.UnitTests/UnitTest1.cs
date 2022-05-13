using Microsoft.VisualStudio.TestTools.UnitTesting;
using PrimeGenCS;
using System;
using System.Collections.Generic;
using System.Numerics;

namespace PrimeGen.UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            // arrange
            var _dataAccess = new MockDataAccess();

            var _logic = new PrimeLogic(_dataAccess);
            _logic.MaxPrime = 41;
            // act

            _logic.GeneratePrimes(null, 0);
            

            // assert
            Assert.IsTrue(_dataAccess.LongPrimes.Contains(2));
            Assert.IsTrue(_dataAccess.LongPrimes.Contains(3));
            Assert.IsTrue(_dataAccess.LongPrimes.Contains(5));
            Assert.IsTrue(_dataAccess.LongPrimes.Contains(7));
            Assert.IsTrue(_dataAccess.LongPrimes.Contains(11));
            Assert.IsTrue(_dataAccess.LongPrimes.Contains(13));
            Assert.IsTrue(_dataAccess.LongPrimes.Contains(17));
            Assert.IsTrue(_dataAccess.LongPrimes.Contains(19));
            Assert.IsTrue(_dataAccess.LongPrimes.Contains(23));
            Assert.IsTrue(_dataAccess.LongPrimes.Contains(29));
            Assert.IsTrue(_dataAccess.LongPrimes.Contains(31));
            Assert.IsTrue(_dataAccess.LongPrimes.Contains(37));
            Assert.IsTrue(_dataAccess.LongPrimes.Contains(41));

            Assert.IsFalse(_dataAccess.LongPrimes.Contains(1));
            Assert.IsFalse(_dataAccess.LongPrimes.Contains(4));
            Assert.IsFalse(_dataAccess.LongPrimes.Contains(6));
            Assert.IsFalse(_dataAccess.LongPrimes.Contains(8));
            Assert.IsFalse(_dataAccess.LongPrimes.Contains(9));
            Assert.IsFalse(_dataAccess.LongPrimes.Contains(10));
            Assert.IsFalse(_dataAccess.LongPrimes.Contains(12));
            Assert.IsFalse(_dataAccess.LongPrimes.Contains(14));
            Assert.IsFalse(_dataAccess.LongPrimes.Contains(16));
            Assert.IsFalse(_dataAccess.LongPrimes.Contains(18));
            Assert.IsFalse(_dataAccess.LongPrimes.Contains(20));
            Assert.IsFalse(_dataAccess.LongPrimes.Contains(21));


        }
    }
}
