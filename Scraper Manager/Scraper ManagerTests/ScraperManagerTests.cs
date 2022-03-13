using Microsoft.VisualStudio.TestTools.UnitTesting;
using Scraper_Manager;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scraper_Manager.Tests
{
    [TestClass()]
    public class ScraperManagerTests
    {
        static string history_file;
        static string output_dir;
        static string input_dir;
        static string setting_file;


        private static void createSettings()
        {
            
            string settings_str = "id;id;;diggcount;likes;;hashtags;hashtags\ntiktok";
            File.WriteAllText(setting_file, settings_str);
  
        }


        private static void createHistory()
        {
            if (File.Exists(history_file))
                File.Delete(history_file);
            File.WriteAllText(history_file, "testfile1\n");

        }

        [ClassInitialize()]
        public static void init(TestContext testContext)
        {
            history_file = "../../../input_folder_test/history.txt";
            input_dir = "../../../input_folder_test";
            output_dir = "../../../output_folder_test";
            setting_file = "../../../settingss.txt";
           
            if (File.Exists(setting_file))
                File.Delete(setting_file);
            if (File.Exists(history_file))
                File.Delete(history_file);
            createSettings();
        }
        /*
        [ClassCleanup()]
        public void Ccleanup()
        {
            string[] files = Directory.GetFiles(output_dir);
            foreach (var file in files)
            {
                File.Delete(file);
            }

            if (File.Exists(setting_file))
                File.Delete(setting_file);
            if (File.Exists(history_file))
                File.Delete(history_file);
        }
        
        */
        [TestInitialize()]
        public void test_init()
        {
            createHistory();
        }


        [TestMethod()]
        public void load_historyTest()
        {

            List<string> ans = ScraperManager.load_history(history_file);
            Console.WriteLine("here1");

            Assert.AreEqual(ans.Count, 1);
            Assert.AreEqual(ans[0], "testfile1");
        }

       
        [TestMethod()]
        public void add_to_historyTest()
        {
            ScraperManager.add_to_history("testfile2", history_file);
            List<string> ans = ScraperManager.load_history(history_file);
            Console.WriteLine("here2");
            Assert.AreEqual(ans.Count, 2);
            Assert.AreEqual(ans[1], "testfile2");

        }
    }
}