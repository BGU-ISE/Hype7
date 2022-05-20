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
    public class RecordTests
    {

        

        [TestMethod()]
        public void RecordTest()
        {
            string[] names = { "id", "hashtags" };
            Record record = new Record("\"1111111111111111111\", \"[{ \"\"id\"\":\"\"229207\"\",\"\"name\"\":\"\"fyp\"\",\"\"title\"\":\"\"\"\",\"\"cover\"\":\"\"\"\"},{\"\"id\"\":\"\"1598498371111942\"\",\"\"name\"\":\"\"foryourpage\"\",\"\"title\"\":\"\"\"\",\"\"cover\"\":\"\"\"\"}]\"", names, "tiktok" );
            Assert.AreEqual(2, record.values.Length);
            Assert.AreEqual("1111111111111111111", record.values[0]);
            Assert.AreEqual("id", record.values_names[0]);
        }

        [TestMethod()]
        public void setSwapOrderTest()
        {
            string[] names = { "id", "hashtags" };
            string[] new_names = { "hashtags2", "id2" };
            Record record = new Record("\"1111111111111111111\", \"[{ \"\"id\"\":\"\"229207\"\",\"\"name\"\":\"\"fyp\"\",\"\"title\"\":\"\"\"\",\"\"cover\"\":\"\"\"\"},{\"\"id\"\":\"\"1598498371111942\"\",\"\"name\"\":\"\"foryourpage\"\",\"\"title\"\":\"\"\"\",\"\"cover\"\":\"\"\"\"}]\"", names, "tiktok");
            Dictionary<int, int> swaporder = new Dictionary<int, int>();
            swaporder.Add(0, 1);
            swaporder.Add(1, 0);
            record.setSwapOrder(swaporder, new_names);
            Assert.AreEqual(2, record.values.Length);
            Assert.AreEqual("1111111111111111111", record.values[1]);
            Assert.AreNotEqual("1111111111111111111", record.values[0]);
            Assert.AreEqual("id2", record.values_names[1]);
        }

        [TestMethod()]
        public void reformat_hashtagsTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void ToStringTest()
        {
            string[] names = { "id", "hashtags" };
            string[] new_names = { "hashtags2", "id2" };
            Record record = new Record("\"1111111111111111111\", \"[{ \"\"id\"\":\"\"229207\"\",\"\"name\"\":\"\"fyp\"\",\"\"title\"\":\"\"\"\",\"\"cover\"\":\"\"\"\"},{\"\"id\"\":\"\"1598498371111942\"\",\"\"name\"\":\"\"foryourpage\"\",\"\"title\"\":\"\"\"\",\"\"cover\"\":\"\"\"\"}]\"", names, "tiktok");

            Assert.IsTrue(record.ToString().Contains("1111111111111111111"));

        }
    }
}