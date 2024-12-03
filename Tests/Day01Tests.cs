using Microsoft.VisualStudio.TestTools.UnitTesting;
using Day01;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Day01.Tests
{
    [TestClass()]
    public class Day01Tests
    {
        [TestMethod]
        public void TestDataFullString()
        {
            var result = Program.DoWork(true,false);

            Assert.AreEqual(11, result);
        }

        [TestMethod]
        public void FullDataFullString()
        {
            var result = Program.DoWork(false, false);

            Assert.AreEqual(2742123, result);
        }
        [TestMethod]
        public void TestDataSkippedString()
        {
            var result = Program.DoWork(true, true);

            Assert.AreEqual(31, result);
        }
        [TestMethod]
        public void FullDataSkippedString()
        {
            var result = Program.DoWork(false, true);

            Assert.AreEqual(21328497, result);
        }
    }
}