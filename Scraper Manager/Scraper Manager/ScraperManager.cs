using System;
using System.IO;
using System.Collections.Generic;

namespace Scraper_Manager
{
    public class ScraperManager
    {


        public static List<string> load_history(string inputfile = "../../../input_folder/history.txt")
        {
            List<string> ans = new List<string>();

            using (var reader = new StreamReader(inputfile))
            {
                while (!reader.EndOfStream)
                {
                    ans.Add(reader.ReadLine());
                }
            }
            return ans;
        }

        public static void add_to_history(string file_name, string outputfile = "../../../input_folder/history.txt")
        {
            using (var w = File.AppendText(outputfile))
            {
                w.WriteLine(file_name);
                w.Flush();
            }
        }

        public static void run(string[] args)
        {
            List<string> history = load_history();
            string[] dirs = Directory.GetFiles("../../../input_folder", "*.csv");
            foreach (string file in dirs)
            {
                if (!history.Contains(Path.GetFileName(file)))
                {
                    string output_path = "../../../output_folder/" + Path.GetFileNameWithoutExtension(file) + "_formated.csv";

                    RecordsFile r = new RecordsFile("../../../settings.txt", file, output_path);
                    r.loadFile();
                    r.saveRecords();
                    add_to_history(Path.GetFileName(file));
                }
            }

        }
    }
}