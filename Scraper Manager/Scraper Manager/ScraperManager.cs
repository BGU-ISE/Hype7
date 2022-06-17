using System;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Scraper_Manager
{
    public static class ScraperManager
    {
        public static int LastIndexTable;
        public static List<string> fieldNames = new List<string>();
        public static List<string> load_history(string inputFolder = "../../../input_folder")
        {
            string history_path = inputFolder + "/history.txt";
            Console.WriteLine("Reading history....");
            List<string> ans = new List<string>();

            using (var reader = new StreamReader(history_path))
            {
                while (!reader.EndOfStream)
                {
                    ans.Add(reader.ReadLine());
                }
            }
            return ans;
        }

        public static void add_to_history(string file_name, string inputFolder = "../../../input_folder")
        {
            string history_path = inputFolder + "/history.txt";
            using (var w = File.AppendText(history_path))
            {
                w.WriteLine(file_name);
                w.Flush();
            }
        }
        private static void ReadFromText()
        {
            string text = System.IO.File.ReadAllText("../../../../../Metric Manager/Metric Manager/Data/lastIndexTable.txt");
            text = text.Replace(" ", string.Empty);
            string[] ignore = text.Split(",");
            LastIndexTable = int.Parse(ignore[0]);
        }
        public static void run(string[] args)
        {
            List<string> history = load_history();
            string[] dirs = Directory.GetFiles("../../../input_folder", "*.csv");
            LastIndexTable = DAL.GetLastIndexTable(true);

            foreach (string file in dirs)
            {
                if (!history.Contains(Path.GetFileName(file)))
                {
                    string output_path = "../../../output_folder/" + Path.GetFileNameWithoutExtension(file) + "_formated.csv";
                    Console.WriteLine("Reading file:" + Path.GetFileName(file));
                    RecordsFile r = new RecordsFile(folder+"/settings.txt", file, output_path);
                    r.loadFile();
                    string fileName = Path.GetFileName(output_path);
                    fieldNames = r.output_names;
                    DAL.InitIntField(r);
                    DAL.CreateDateTable(fieldNames, true);
                    Console.WriteLine("Create table day " + LastIndexTable);
                    DAL.InsertDataFromToday(r, LastIndexTable, ConvertStringToDatetime(fileName), true);
                    Console.WriteLine("insert information to DB.");
                    r.saveRecords();
                    Console.WriteLine("Saving to file:" + Path.GetFileName(output_path));
                    add_to_history(Path.GetFileName(file), folder);
                    Console.WriteLine("Done with file:" + Path.GetFileName(file) +" adding to history");
                    File.Delete(file);
                }
            }
        }
        public static DateTime ConvertStringToDatetime(string time)
        {
            var arr1 = Regex.Split(time, @"[a-zA-Z|(|)|+|/|\-|*|^|_|\.]");
            DateTime date = new DateTime();
            for (int j = 0; j < arr1.Length; j++)
            {
                if (arr1[j].Length > 0)
                {
                    if (arr1[j].Length == 2)
                        arr1[j] = "20" + arr1[j];
                    date = new DateTime(int.Parse(arr1[j]), int.Parse(arr1[j + 2]), int.Parse(arr1[j + 1]));
                    j = arr1.Length;
                }
            }
            return date;
        }
        
        
        
    }
}