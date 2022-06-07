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
            string[] arr = new string[0];
            SystemManager.InitializeData(arr);
            Assert.IsTrue(SystemManager.GetMetricTest(), "metrics empty.");
            Assert.IsNotNull(SystemManager.path, "get path didn't work.");
            Assert.IsTrue(SystemManager.ignoreHashtag.Count > 0, "hashtag ignore list is empty.");
        }

        [TestMethod()]
        public void ConvertPathToDatetimeTest()
        {
            var date = SystemManager.ConvertPathToDatetime("C:\\Users\\Almogi\\Desktop\\githubtry\\Project\\Hype7\\Metric Manager\\Metric Manager\\Data\\UnReadData\\tiktok_1_3_2022_formated.csv");
            Assert.IsTrue(date.Year == 2022, "year not correct");
            Assert.IsTrue(date.Month == 1, "month not correct");
            Assert.IsTrue(date.Day == 3, "day not correct");
        }

        [TestMethod()]
        public void GetDataTest()
        {
            string date = "03-01-2022";
            var data = SystemManager.GetData(date);
            Assert.IsNotNull(data, "data is null");
            foreach (var element in data)
            {
                Assert.IsTrue(element.GetDate().ToString("dd-MM-yyyy").Equals(date), "date not match");
                Assert.IsTrue(element.GetData().Length > 10, "not all data imported");
                break;
            }

        }

        [TestMethod()]
        public void GetFieldsNameTest()
        {
            var lst = SystemManager.GetFieldsName();
            Assert.IsNotNull(lst, "list of field is null");
            Assert.IsTrue(lst.Count > 10, "not enougth fields");
        }

        [TestMethod()]
        public void GetDateByIndexTest()
        {
            SystemManager.GetAllData();
            var date = SystemManager.GetDateByIndex(1);
            Assert.IsTrue(date.Day == 31, "day not correct");
            Assert.IsTrue(date.Month == 12, "month not correct");
            Assert.IsTrue(date.Year == 2021, "year not correct");
            date = SystemManager.GetDateByIndex(3);
            Assert.IsTrue(date.Day == 2, "day not correct");
            Assert.IsTrue(date.Month == 1, "month not correct");
            Assert.IsTrue(date.Year == 2022, "year not correct");
        }

        [TestMethod()]
        public void GetIndexByFieldNameTest()
        {
            SystemManager.GetFieldsName();
            var index = SystemManager.GetIndexByFieldName("id");
            Assert.IsTrue(index == 0, "index id not correct");
        }

        
    }
}