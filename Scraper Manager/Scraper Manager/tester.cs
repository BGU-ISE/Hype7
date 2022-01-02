using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO; 

namespace Scraper_Manager
{
    class tester
    {
        public static void Main2(string[] args)
        {
            try
            {
                // Only get files that begin with the letter "c".
                string[] dirs = Directory.GetFiles("../../../input_folder", "*.csv");
                foreach (string dir in dirs)
                {
                    Console.WriteLine(Path.GetFileName(dir));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The process failed: {0}", e.ToString());
            }
        }
    }
}
