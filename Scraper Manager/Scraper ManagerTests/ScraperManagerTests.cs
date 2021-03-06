using Microsoft.VisualStudio.TestTools.UnitTesting;
using Scraper_Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scraper_Manager.Tests
{
    [TestClass()]
    public class ScraperManagerTests
    {
        [TestMethod()]
        public void ConvertStringToDatetimeTest()
        {
            var date = ScraperManager.ConvertStringToDatetime("22.01.06_US_reread_videos.csv");
            Assert.IsTrue(date.Year == 2022, "year not correct");
            Assert.IsTrue(date.Month == 6, "month not correct");
            Assert.IsTrue(date.Day == 1, "day not correct");
        }
    }
}