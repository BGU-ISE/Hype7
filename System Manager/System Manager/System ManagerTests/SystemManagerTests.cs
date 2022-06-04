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
/*            proxy.scrapers(scraper, output_path);
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

            proxy.scraper_manager(scraperManager, newData, db_path);*/
            
            
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
            scrapeToDB(output_path);
            connector.OpenConnect(db_path);
            IEnumerable<object[]>[] vids_after = connector.getAll();
            Assert.AreEqual(521, vids_after[6].Count());
            Assert.AreEqual(vids_after[1].ElementAt(23)[0], vids_before[1].ElementAt(23)[0]);

            connector.CloseConnect();

        }
        

        [TestMethod()]
        public void MetricTest()
        {

           
            Assert.Fail();
        }

        [TestMethod()]
        public void modelTest()
        {
            Assert.Fail();
        }




    }
}