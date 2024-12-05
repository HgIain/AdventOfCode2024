using Microsoft.VisualStudio.TestTools.UnitTesting;
using Day05v2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day05v2.Tests
{
    [TestClass()]
    public class Day05Tests
    {
        [TestMethod()]
        public void TestDataInOrder()
        {
            var result = Program.DoWork(false, true);

            Assert.AreEqual(143, result);
        }
        [TestMethod()]
        public void FullDataInOrder()
        {
            var result = Program.DoWork(true, true);

            Assert.AreEqual(4905, result);
        }
        [TestMethod()]
        public void TestDataOutOfOrder()
        {
            var result = Program.DoWork(false, false);

            Assert.AreEqual(123, result);
        }
        [TestMethod()]
        public void FullDataOutOfOrder()
        {
            var result = Program.DoWork(true, false);

            Assert.AreEqual(6204, result);
        }
    }
}