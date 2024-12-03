using Microsoft.VisualStudio.TestTools.UnitTesting;
using Day3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Day03.Tests
{
    [TestClass()]
    public class Day03Tests
    {
        [TestMethod]
        public void TestDataFullString()
        {
            var result = Program.DoWork(false,false);

            Assert.AreEqual(161, result);
        }

        [TestMethod]
        public void FullDataFullString()
        {
            var result = Program.DoWork(false, true);

            Assert.AreEqual(183380722, result);
        }
        [TestMethod]
        public void TestDataSkippedString()
        {
            var result = Program.DoWork(true, false);

            Assert.AreEqual(48, result);
        }
        [TestMethod]
        public void FullDataSkippedString()
        {
            var result = Program.DoWork(true, true);

            Assert.AreEqual(82733683, result);
        }
    }
}