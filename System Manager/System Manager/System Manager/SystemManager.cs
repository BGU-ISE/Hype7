using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace System_Manager
{
    public class SystemManager
    {
        //static Runner metric_runner = new Metric_Manager_Runner();
       // static Runner scraper_manager_runner = new Scraper_Manager_Runner();
        static List<Runner> scrapers_runners = new List<Runner>();
        static GUI_Runner guiRunner;


        static void real_main()
        {
            (new Model_Manager_Runner("youtube")).run();
        }
        static void Main(string[] args)
        {
            //real_main();
            //return;
           // scrapers_runners.Add(new TikTok_Scraper_Runner());
            scrapers_runners.Add(new Youtube_Scraper_Runner());

            guiRunner = new GUI_Runner();

            using (var countdownEvent = new CountdownEvent(scrapers_runners.Count()))
            {
                foreach (var runner in scrapers_runners)
                {

                    ThreadPool.QueueUserWorkItem((Object stateInfo) => 
                    {
                        runner.run();
                        (new Scraper_Manager_Runner(runner.directory)).run();
                        (new Metric_Manager_Runner(runner.directory)).run();
                        (new Model_Manager_Runner(runner.directory)).run();
                        guiRunner.add_db(runner.directory);
                        countdownEvent.Signal();
                    });
                }
                countdownEvent.Wait();
                guiRunner.run();
            }
            Console.WriteLine("finished everything for the day!");
            
        }


    }
}
