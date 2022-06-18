using Microsoft.VisualStudio.TestTools.UnitTesting;
using System_Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System_ManagerTests;
using System.Data.SQLite;
using System.Data;
using System_Manager;

namespace System_Manager.Tests
{
    [TestClass()]
    public class UnitTest
    {

        public string scraper = null;
        public string scraperManager = null;
        public string metricManger = null;
        public string modelManager = null;

        public string output_folder = @"C:\Users\Iftah\Desktop\אוניברסיטה\שנה ד\Project\Scraper Manager\System Manager\System Manager\System ManagerTests\output";
        public iProxy proxy { get; set; }



        [TestMethod()]
        public void dbConnectionTest()
        {
            int db_num = 1;
            string output_path = output_folder + "/UnitTest";
            Directory.CreateDirectory(output_path);
            string db_path = output_path + "/Database"+db_num+".db";
            Assert.IsTrue(File.Exists(db_path));
            DBConnector connector = new DBConnector();
            connector.OpenConnect(db_path);
            Assert.AreEqual(ConnectionState.Open, connector.connection.State);
            connector.CloseConnect();
            Assert.AreEqual(ConnectionState.Closed, connector.connection.State);
        }



        [TestMethod()]
        public void dbRetrivalTest()
        {
            int db_num = 1;
            string output_path = output_folder + "/UnitTest";
            Directory.CreateDirectory(output_path);
            string db_path = output_path + "/Database" + db_num + ".db";
            DBConnector connector = new DBConnector();
            connector.OpenConnect(db_path);
            IEnumerable<object[]>[] vids_before = connector.getAll();
            connector.CloseConnect();
            Assert.IsTrue(vids_before.Length == 7);
            foreach (var table in vids_before)
            {
                Assert.IsFalse(table.Count() == 0);
                Assert.AreEqual(13, table.ElementAt(0).Length);
            }


                
        }

    }
}
