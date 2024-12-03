using Microsoft.VisualStudio.TestTools.UnitTesting;
using Day02;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.AccessControl;

namespace Day02.Tests
{
    [TestClass()]
    public class Day02Tests
    {
        [TestMethod]
        public void TestDataNoErrors()
        {
            var result = Program.DoWork(false, 0);

            Assert.AreEqual(2, result);
        }

        [TestMethod]
        public void FullDataNoErrors()
        {
            var result = Program.DoWork(true, 0);

            Assert.AreEqual(356, result);
        }
        [TestMethod]
        public void TestDataOneError()
        {
            var result = Program.DoWork(false, 1);

            Assert.AreEqual(4, result);
        }
        [TestMethod]
        public void FullDataOneError()
        {
            var result = Program.DoWork(true, 1);

            Assert.AreEqual(413, result);
        }
    }
}