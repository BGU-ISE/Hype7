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

        public string output_folder = @"../../../Data";
        public static iProxy proxy { get; set; }

        [ClassInitialize()]
        public static void classInit(TestContext context)
        {
            proxy = new Proxy(new RealProxy());
        }



            [TestMethod()]
        public void scrapeToDBTest1()
        {

            string output_path = output_folder + "/scrapeToDB";
            Directory.CreateDirectory(output_path+"/logs");
            string db_path = output_path + "/Database.db";
            if (File.Exists(db_path))
                File.Delete(db_path);
            File.Copy(output_folder + "/DBOrigin/full.db", db_path);
            if (File.Exists(output_path+"/settings.txt"))
                File.Delete(output_path + "/settings.txt");
            File.Copy(output_folder + "/DBOrigin/settings.txt", output_path + "/settings.txt");
            if (File.Exists(output_path + "/history.txt"))
                File.Delete(output_path + "/history.txt");
            using (var temp = File.Create(output_path + "/history.txt")) ;
            //File.SetAttributes(output_path + "/history.txt", FileAttributes.Normal);
            
            DBConnector connector = new DBConnector();
            connector.OpenConnect(db_path);
            IEnumerable<object[]>[] vids_before = connector.getAll();
            connector.CloseConnect();
            proxy.scraper(output_path);
            proxy.scraper_manager("scrapeToDB");
           

            connector.OpenConnect(db_path);
            IEnumerable<object[]>[] vids_after = connector.getAll();
            connector.CloseConnect();


            for (int i = 1; i < 7; i++)
            {
                Assert.IsTrue(vids_after[i-1].Count() ==  vids_before[i].Count());
            }
            Assert.IsTrue(vids_after[6].Count() > vids_after[5].Count());
            Assert.AreEqual(vids_after[1].ElementAt(23)[0], vids_before[2].ElementAt(23)[0]);


        }


        [TestMethod()]
        public void MetricTest()
        {

            string output_path = output_folder + "/MetricTest" ;
            Directory.CreateDirectory(output_path+"/logs");
            string db_path = output_path + "/Database.db";
            if (File.Exists(db_path))
                File.Delete(db_path);
            File.Copy(output_folder + "/DBOrigin/full+s.db", db_path);
            if (File.Exists(output_path + "/settings.txt"))
                File.Delete(output_path + "/settings.txt");
            File.Copy(output_folder + "/DBOrigin/settings.txt", output_path + "/settings.txt");
            if (File.Exists(output_path + "/history.txt"))
                File.Delete(output_path + "/history.txt");
            using (var temp = File.Create(output_path + "/history.txt")) ;

            DBConnector connector = new DBConnector();
            connector.OpenConnect(db_path);
            IEnumerable<object[]> metrics_before = connector.getMetrics();
            connector.CloseConnect();



            proxy.metrics("MetricTest");

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
       
            string output_path = output_folder + "/modelTest";
            Directory.CreateDirectory(output_path +"/logs");
            string db_path = output_path + "/Database.db";

            if (File.Exists(db_path))
                File.Delete(db_path);
            File.Copy(output_folder + "/DBOrigin/full+m.db", db_path);
            if (File.Exists(output_path + "/settings.txt"))
                File.Delete(output_path + "/settings.txt");
            File.Copy(output_folder + "/DBOrigin/settings.txt", output_path + "/settings.txt");
            if (File.Exists(output_path + "/history.txt"))
                File.Delete(output_path + "/history.txt");
            using (var temp = File.Create(output_path + "/history.txt")) ;


            DBConnector connector = new DBConnector();
            connector.OpenConnect(db_path);
            IEnumerable<object[]> model_before = connector.getModelScore();
            connector.CloseConnect();

            proxy.model("modelTest");

            connector.OpenConnect(db_path);
            IEnumerable<object[]> model_after = connector.getModelScore();
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