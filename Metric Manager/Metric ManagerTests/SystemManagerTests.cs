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
            string[] arr = new string[1];
            arr[0] = "..\\..\\..\\..\\..\\Metric Manager\\Metric Manager\\Data\\test\\youtube";
            SystemManager.InitializeData(arr);
            Assert.IsTrue(SystemManager.GetMetricTest(), "metrics empty.");
            Assert.IsNotNull(SystemManager.path, "get path didn't work.");
            Assert.IsTrue(SystemManager.ignoreHashtag.Count > 0, "hashtag ignore list is empty.");
        }

        [TestMethod()]
        public void ConvertPathToDatetimeTest()
        {
            var date = SystemManager.ConvertPathToDatetime("C:\\Users\\Almogi\\Desktop\\githubtry\\Project\\Hype7\\Metric Manager\\Metric Manager\\Data\\UnReadData\\22.26.04_US_videos_formated.csv");
            Assert.IsTrue(date.Year == 2022, "year not correct");
            Assert.IsTrue(date.Month == 4, "month not correct");
            Assert.IsTrue(date.Day == 26, "day not correct");
        }

        [TestMethod()]
        public void GetDataTest()
        {
            string date = "26-05-2022";
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
            Assert.IsTrue(date.Day == 26, "day 1 not correct");
            Assert.IsTrue(date.Month == 5, "month 1 not correct");
            Assert.IsTrue(date.Year == 2022, "year 1 not correct");
            date = SystemManager.GetDateByIndex(3);
            Assert.IsTrue(date.Day == 28, "day 3 not correct");
            Assert.IsTrue(date.Month == 5, "month 3 not correct");
            Assert.IsTrue(date.Year == 2022, "year 3 not correct");
            date = SystemManager.GetDateByIndex(7);
            Assert.IsTrue(date.Day == 1, "day 7 not correct");
            Assert.IsTrue(date.Month == 6, "month 7 not correct");
            Assert.IsTrue(date.Year == 2022, "year 7 not correct");
        }

        [TestMethod()]
        public void GetIndexByFieldNameTest()
        {
            string[] arr = new string[1];
            arr[0] = "..\\..\\..\\..\\..\\Metric Manager\\Metric Manager\\Data\\test\\youtube";
            SystemManager.InitializeData(arr);
            SystemManager.GetFieldsName();
            var index = SystemManager.GetIndexByFieldName(DAL.IDName);
            Assert.IsTrue(index == 0, "index id not correct");
        }

        
    }
}