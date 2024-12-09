using Microsoft.VisualStudio.TestTools.UnitTesting;
using Day09;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day09.Tests
{
    [TestClass()]
    public class Day09Tests
    {
        [TestMethod()]
        public void FragmentTestData()
        {
            var result = Program.DoWork(false, false);

            Assert.AreEqual(1928, result);
        }

        [TestMethod()]
        public void FragmentFullData()
        {
            var result = Program.DoWork(true, false);

            Assert.AreEqual(6211348208140, result);
        }

        [TestMethod()]
        public void NoFragmentTestData()
        {
            var result = Program.DoWork(false, true);

            Assert.AreEqual(2858, result);
        }

        [TestMethod()]
        public void NoFragmentFullData()
        {
            var result = Program.DoWork(true, true);

            Assert.AreEqual(6239783302560, result);
        }
    }
}