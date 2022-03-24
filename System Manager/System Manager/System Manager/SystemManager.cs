using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System_Manager
{
    class SystemManager
    {
        static Runner metric_runner = new Metric_Manager_Runner();
        static Runner scraper_manager_runner = new Scraper_Manager_Runner();
        static List<Runner> scrapers_runners = new List<Runner>();

        static void Main(string[] args)
        {
            scrapers_runners.Add(new TikTok_Scraper_Runner());

            foreach (var runner in scrapers_runners)
            {

                runner.run();
            }
            Console.WriteLine("finished all scrapers");
            
        }


    }
}
