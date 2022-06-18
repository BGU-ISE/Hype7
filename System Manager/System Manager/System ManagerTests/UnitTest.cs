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
using System.Diagnostics;

namespace System_Manager.Tests
{
    [TestClass()]
    public class UnitTest
    {

        public string scraper = null;
        public string scraperManager = null;
        public string metricManger = null;
        public string modelManager = null;

        public string output_folder = @"../../../Data";
        public static iProxy proxy { get; set; }

        [ClassInitialize()]
        public static void classInit(TestContext context)
        {
            proxy = new Proxy(new RealProxy());
        }

        [TestMethod()]
        public void scrapeYTStabilityTest()
        {
            string directory = "scrapeYTStabilityTest";
            try
            {
                proxy.scraper(directory);
            }
            catch(Exception e)
            {
                Assert.Fail(e.Message);
            }


        }


        [TestMethod()]
        public void metricStabilityTest()
        {
            string good_dir = "metricStabilityTestGood";
            string bad_dir = "metricStabilityTestBad";

            DateTime old_change_good = File.GetLastWriteTime(good_dir + "/DataBase.db");
            DateTime old_change_bad = File.GetLastWriteTime(bad_dir + "/DataBase.db");
            try
            {
                proxy.scraper(good_dir);
                proxy.scraper(bad_dir);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
            DateTime new_change_good = File.GetLastWriteTime(good_dir + "/DataBase.db");
            DateTime new_change_bad = File.GetLastWriteTime(bad_dir + "/DataBase.db");

            Assert.AreNotEqual(old_change_good, new_change_good);
            Assert.AreNotEqual(old_change_bad, new_change_bad);

        }
        [TestMethod()]
        public void modelStabilityTest()
        {
            string good_dir = "modelStabilityTestGood";
            string bad_dir = "modelStabilityTestBad";

            DateTime old_change_good = File.GetLastWriteTime(good_dir + "/DataBase.db");
            DateTime old_change_bad = File.GetLastWriteTime(bad_dir + "/DataBase.db");
            try
            {
                proxy.model(good_dir);
                proxy.model(bad_dir);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
            DateTime new_change_good = File.GetLastWriteTime(good_dir + "/DataBase.db");
            DateTime new_change_bad = File.GetLastWriteTime(bad_dir + "/DataBase.db");

            Assert.AreNotEqual(old_change_good, new_change_good);
            Assert.AreNotEqual(old_change_bad, new_change_bad);
        }
        [TestMethod()]
        public void scraperManagerStabilityTest()
        {
            string good_dir = "scraperManagerStabilityTestGood";
            string bad_dir = "scraperManagerStabilityTestBad";

            DateTime old_change_good = File.GetLastWriteTime(good_dir + "/DataBase.db");
            DateTime old_change_bad = File.GetLastWriteTime(bad_dir + "/DataBase.db");
            try
            {
                proxy.scraper_manager(good_dir);
                proxy.scraper_manager(bad_dir);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
            DateTime new_change_good = File.GetLastWriteTime(good_dir + "/DataBase.db");
            DateTime new_change_bad = File.GetLastWriteTime(bad_dir + "/DataBase.db");

            Assert.AreNotEqual(old_change_good, new_change_good);
            Assert.AreNotEqual(old_change_bad, new_change_bad);
        }

        [TestMethod()]
        public void GUIStabilityTest()
        {
            string good_dir = "GUIStabilityTestGood";
            string bad_dir = "GUIStabilityTestBad";
            Process proc = new Process();
            try
            {
                proc = proxy.runGui(Directory.GetFiles(good_dir));
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
            Assert.IsFalse(proc.HasExited);
            proc.Kill();

            try
            {
                proc = proxy.runGui(Directory.GetFiles(bad_dir));
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
            proc.Kill();

        }


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
