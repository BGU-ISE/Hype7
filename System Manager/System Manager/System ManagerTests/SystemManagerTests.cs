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

namespace System_Manager.Tests
{
    [TestClass()]
    public class SystemManagerTests
    {

        public string scraper = null;
        public string scraperManager = null;
        public string metricManger = null;
        public string modelManager = null;

        public string output_folder = @"C:\Users\Iftah\Desktop\אוניברסיטה\שנה ד\Project\Scraper Manager\System Manager\System Manager\System ManagerTests\output";
        public iProxy proxy{ get; set; }

        private void scrapeToDB(string output_path)
        {
            //string output_path = output_folder + "/scrapeToDB" + test_num;
            string db_path = output_path + "/Database.db";
    
            string[] pre_scraped_data = Directory.GetFiles(output_path);
            proxy.scrapers(scraper, output_path);
            string[] scraped_data = Directory.GetFiles(output_path);
            Assert.AreEqual(pre_scraped_data.Length + 1, scraped_data.Length);

            string newData = null;
            foreach (string file in scraped_data)
            {
                if (!pre_scraped_data.Contains(file))
                {
                    newData = file;
                    break;
                }
            }
            if (newData == null)
            {
                Assert.Fail();

            }

            proxy.scraper_manager(scraperManager, newData, db_path);
            
            
        }



        [TestMethod()]
        public void scrapeToDBTest1()
        {
            int test_num = 1;
            string output_path = output_folder + "/scrapeToDB" + test_num;
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
            

            scrapeToDB(output_path);
            connector.OpenConnect(db_path);
            IEnumerable<object[]>[] vids_after = connector.getAll();

            connector.CloseConnect();

            Assert.IsTrue(vids_after.Length == 7);
            foreach (var table in vids_after)
            {
                Assert.IsFalse(table.Count() == 0);
                Assert.AreEqual(13, table.ElementAt(0).Length);
            }



            Assert.AreEqual(521, vids_after[6].Count());
            Assert.AreEqual(vids_after[1].ElementAt(23)[0], vids_before[1].ElementAt(23)[0]);


        }


        [TestMethod()]
        public void MetricTest()
        {

            int test_num = 1;
            string output_path = output_folder + "/MetricTest" + test_num;
            Directory.CreateDirectory(output_path);
            string db_path = output_path + "/Database.db";
            DBConnector connector = new DBConnector();
            connector.OpenConnect(db_path);
            IEnumerable<object[]> metrics_before = connector.getMetrics();
            connector.CloseConnect();
            Assert.IsFalse(metrics_before.Count() == 0);
            Assert.AreEqual(12, metrics_before.ElementAt(0).Length);



            proxy.metrics(metricManger, db_path);

            connector.OpenConnect(db_path);
            IEnumerable<object[]> metrics_after = connector.getMetrics();
            connector.CloseConnect();
            Assert.IsFalse(metrics_after.Count() == 0);
            Assert.IsTrue(metrics_after.Count() > metrics_before.Count());
            Assert.AreEqual(12, metrics_after.ElementAt(0).Length);


            Assert.IsTrue(metrics_after.Where((x) => x[0].Equals(metrics_before.ElementAt(19)[0])).Count() !=0 );

            Assert.IsTrue(metrics_after.Count() > metrics_before.Count());

            int metricsCounter1 = 0;
            int metricsCounter2 = 0;
            foreach (var record in metrics_after)
            {
                if(record[0].Equals("TWGvntl9itE"))
                {
                    metricsCounter1++;
                    Assert.IsFalse(record[5].Equals(0.0) || record[5].Equals("0.0"));
                }
                if (record[0].Equals("vU0YSKg7En0"))
                {
                    metricsCounter2++;
                    Assert.IsFalse(record[5].Equals(0.0) || record[5].Equals("0.0"));
                }
            }
            Assert.IsFalse(metricsCounter1 == 0);
            Assert.AreEqual(metricsCounter1, metricsCounter2);
            Assert.Fail();
        }





        [TestMethod()]
        public void modelTest()
        {
            int test_num = 1;
            string output_path = output_folder + "/modelTest" + test_num;
            Directory.CreateDirectory(output_path);
            string db_path = output_path + "/Database.db";
            Assert.Fail();
            DBConnector connector = new DBConnector();
            connector.OpenConnect(db_path);
            IEnumerable<object[]> model_before = connector.getMetrics();
            connector.CloseConnect();
            Assert.IsFalse(model_before.Count() == 0);
            Assert.AreEqual(2, model_before.ElementAt(0).Length);


            proxy.metrics(metricManger, db_path);

            connector.OpenConnect(db_path);
            IEnumerable<object[]> model_after = connector.getMetrics();
            connector.CloseConnect();
            Assert.IsFalse(model_after.Count() == 0);
            Assert.IsTrue(model_after.Count() > model_before.Count());
            Assert.AreEqual(2, model_after.ElementAt(0).Length);


            bool apear = false;
            foreach (var record in model_after)
            {
                if( record[0].Equals("thJgU9jkdU4"))
                {
                    Assert.IsFalse(apear);
                    apear = true;
                    Assert.IsFalse(record[1].Equals(0) || record[1].Equals("0.0"));

                }
            }
            Assert.IsTrue(apear);


        }




    }
}