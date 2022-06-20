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


        public static string output_folder = @"../../../Data";
        public static iProxy proxy { get; set; }
        const string db = "/DataBase.db";
        
        
        private static void setupDB(string test_name, string db_name)
        {
            string test_file_name = "/" + test_name;
            string db_file_name = "/" + db_name;
            Directory.CreateDirectory(output_folder +  test_file_name +"/logs");
            if (File.Exists(output_folder + test_file_name + db))
                File.Delete(output_folder + test_file_name + db);
            File.Copy(output_folder + "/DBOrigin" + db_file_name, output_folder + test_file_name + db);

            if (File.Exists(output_folder + test_file_name + "/settings.txt"))
                File.Delete(output_folder + test_file_name + "/settings.txt");
          
            File.Copy(output_folder + "/DBOrigin/settings.txt", output_folder + test_file_name + "/settings.txt");
            if (File.Exists(output_folder + test_file_name + "/history.txt"))
                File.Delete(output_folder + test_file_name + "/history.txt");
            File.Create(output_folder + test_file_name + "/history.txt");
        }
        
        [ClassInitialize()]
        public static void classInit(TestContext context)
        {
            proxy = new Proxy(new RealProxy());





            Directory.CreateDirectory(output_folder + "/scrapeYTStabilityTest");
            setupDB("scraperManagerStabilityTestfull", "full+m.db");
            setupDB("metricStabilityTestfull", "full+s.db");
            setupDB("modelStabilityTestfull", "full+m.db");
            setupDB("GUIStabilityTestfull", "full.db");


            setupDB("scraperManagerStabilityTesthalf", "half+m.db");
            //setupDB("metricStabilityTesthalf", "half+s.db");
    //        setupDB("modelStabilityTesthalf", "half+m.db");
           // setupDB("GUIStabilityTesthalf", "half+m.db");


        }



        [TestMethod()]
        public void scrapeYTStabilityTest()
        {
            string directory =  "scrapeYTStabilityTest";
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



            
            string full_dir =  "metricStabilityTestfull";
      //      string half_dir =  "metricStabilityTesthalf";

            DateTime old_change_full = File.GetLastWriteTime(output_folder+"/"+ full_dir + "/DataBase.db");
       //     DateTime old_change_half = File.GetLastWriteTime(output_folder + "/" + half_dir + "/DataBase.db");


            try
            {
                //proxy.metrics(half_dir);
                proxy.metrics(full_dir);
             
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
            DateTime new_change_full = File.GetLastWriteTime(output_folder + "/" + full_dir + "/DataBase.db");
            //DateTime new_change_half = File.GetLastWriteTime(output_folder + "/" + half_dir + "/DataBase.db");

            Assert.AreNotEqual(old_change_full, new_change_full);
           // Assert.AreNotEqual(old_change_half, new_change_half);

        }
        [TestMethod()]
        public void modelStabilityTest()
        {
            string full_dir = "modelStabilityTestfull";
          //  string half_dir =  "modelStabilityTesthalf";

            DateTime old_change_full = File.GetLastWriteTime(output_folder + "/" + full_dir + "/DataBase.db");
            //DateTime old_change_half = File.GetLastWriteTime(output_folder + "/" + half_dir + "/DataBase.db");
            try
            {
                proxy.model(full_dir);
              //  proxy.model(half_dir);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
            DateTime new_change_full = File.GetLastWriteTime(output_folder + "/" + full_dir + "/DataBase.db");
           // DateTime new_change_half = File.GetLastWriteTime(output_folder + "/" + half_dir + "/DataBase.db");

            Assert.AreNotEqual(old_change_full, new_change_full, "full fail");
           // Assert.AreNotEqual(old_change_half, new_change_half, "half fail");
        }
        [TestMethod()]
        public void scraperManagerStabilityTest()
        {
            if (File.Exists(output_folder + "/scraperManagerStabilityTestfull/sample_scraping.csv"))
            {
                File.Delete(output_folder + "/scraperManagerStabilityTestfull/sample_scraping.csv");
                
            }
            File.Copy(output_folder + "/DBOrigin/sample_scraping.csv", output_folder + "/scraperManagerStabilityTestfull/sample_scraping.csv");


            if (File.Exists(output_folder + "/scraperManagerStabilityTesthalf/sample_scraping.csv"))
            {
                File.Delete(output_folder + "/scraperManagerStabilityTesthalf/sample_scraping.csv");

            }
            File.Copy(output_folder + "/DBOrigin/sample_scraping.csv", output_folder + "/scraperManagerStabilityTesthalf/sample_scraping.csv");



            string full_dir = "scraperManagerStabilityTestfull";
            string half_dir = "scraperManagerStabilityTesthalf";

            DateTime old_change_full = File.GetLastWriteTime(output_folder + "/" + full_dir +"/DataBase.db");
            DateTime old_change_half = File.GetLastWriteTime(output_folder + "/" + half_dir + "/DataBase.db");
            try
            {
                proxy.scraper_manager(full_dir);
                proxy.scraper_manager(half_dir);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
            DateTime new_change_full = File.GetLastWriteTime(output_folder + "/" + full_dir + "/DataBase.db");
            DateTime new_change_half = File.GetLastWriteTime(output_folder + "/" + half_dir + "/DataBase.db");

            Assert.AreNotEqual(old_change_full, new_change_full,  ("failed. old time was " + old_change_full.ToString() +" new is" + new_change_full.ToString()  ) );
            Assert.AreNotEqual(old_change_half, new_change_half);
        }

        [TestMethod()]
        public void GUIStabilityTest()
        {
            string full_dir = output_folder + "/GUIStabilityTestfull";
            string half_dir = output_folder + "/GUIStabilityTesthalf";
            Process proc = new Process();
            try
            {
                proc = proxy.runGui(Directory.GetFiles(full_dir));
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
            Assert.IsFalse(proc.HasExited);
            proc.Kill();
            /*
            try
            {
                proc = proxy.runGui(Directory.GetFiles(half_dir));
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
            proc.Kill();*/

        }


        [TestMethod()]
        public void dbConnectionTest()
        {
            string output_path = output_folder + "/UnitTest";
            Directory.CreateDirectory(output_path);
            string db_path = output_path + "/Database.db";
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
            string output_path = output_folder + "/UnitTest";
            Directory.CreateDirectory(output_path);
            string db_path = output_path + "/Database.db";
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
