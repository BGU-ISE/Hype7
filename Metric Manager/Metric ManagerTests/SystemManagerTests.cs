using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hype7;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hype7.Tests
{
    [TestClass()]
    public class SystemManagerTests
    {
        [TestMethod()]
        public void InitializeDataTest()
        {
            Assert.IsTrue(SystemManager.GetMetricTest(), "metrics empty.");
            Assert.IsNotNull(SystemManager.path, "get path didn't work.");
            Assert.IsTrue(SystemManager.ignoreHashtag.Count > 0, "hashtag ignore list is empty.");
        }
    }
}