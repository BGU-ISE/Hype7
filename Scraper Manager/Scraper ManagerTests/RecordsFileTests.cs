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
    public class RecordsFileTests
    {
        static string output_file;
        static string input_file;
        static string setting_file;


        private static void createSettings()
        {

            string settings_str = "id;id;;diggCount;likes;;hashtags;hashtags\ntiktok";
            File.WriteAllText(setting_file, settings_str);

        }



        [ClassInitialize()]
        public static void init(TestContext testContext)
        {
           
            input_file = "../../../input_folder_test/test.csv";
            output_file = "../../../output_folder_test/test_formatted.csv";
            setting_file = "../../../settingss.txt";

            if (File.Exists(output_file))
                File.Delete(output_file);
            if (File.Exists(setting_file))
                File.Delete(setting_file);
            createSettings();
        }
        [TestMethod()]
        public void   RecordFileTest ()
        {
            RecordsFile recordsFile = new RecordsFile(setting_file, input_file, output_file);
            Assert.AreEqual(input_file, recordsFile.inputPath);
            Assert.AreEqual(3, recordsFile.settings_dict.Count);
        }

        [TestMethod()]
        public void loadFileTest()
        {
            RecordsFile recordsFile = new RecordsFile(setting_file, input_file, output_file);
            recordsFile.loadFile();
            Assert.AreEqual(2, recordsFile.records.Count);
            Assert.AreEqual("1111111111111111111", recordsFile.records[0].values[0]);
        }

        [TestMethod()]
        public void saveRecordsTest()
        {
            RecordsFile recordsFile = new RecordsFile(setting_file, input_file, output_file);
            recordsFile.loadFile();
            recordsFile.saveRecords();
            Assert.IsTrue(File.Exists(output_file));
        }
    }
}