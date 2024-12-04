using Microsoft.VisualStudio.TestTools.UnitTesting;
using Day04;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day04.Tests
{
    [TestClass()]
    public class Day04Tests
    {
        [TestMethod]
        public void XmasDebugTest()
        {
            var result = Program.DoWork(false);

            Assert.AreEqual(18, result);
        }

        [TestMethod]
        public void XmasFullTest()
        {
            var result = Program.DoWork(true);

            Assert.AreEqual(2534, result);
        }
    }
}