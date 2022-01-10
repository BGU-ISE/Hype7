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


        private void createSettings()
        {
            Console.WriteLine("got");
            string settings_str = "id;id;;diggcount;likes;;hashtags;hashtags\ntiktok";
            File.WriteAllText(setting_file, settings_str);
        }
        
        [ClassInitialize()]
        public static void init(TestContext testContext)
        {
            history_file = "../../../input_folder_test/history.txt";
            input_dir = "../../../input_folder_test";
            output_dir = "../../../output_folder_test";
            setting_file = "../../../settingss.txt";
           /* string[] files = Directory.GetFiles(output_dir);
            foreach (var file in files)
            {
                File.Delete(file);
            }
         
            if (File.Exists(setting_file))
                File.Delete(setting_file);
            if (File.Exists(history_file))
                File.Delete(history_file);*/
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
        

        [TestInitialize()]*/

        [TestMethod()]
        public void load_historyTest()
        {
            createSettings();
            Assert.IsTrue(true);
        }

       
        [TestMethod()]
        public void add_to_historyTest()
        {
            Assert.Fail();
        }
    }
}