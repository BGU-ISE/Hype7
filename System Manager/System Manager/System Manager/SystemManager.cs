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
        static List<Runner> scrapers_runners = new List<Runner>();
        static GUI_Runner guiRunner;

        static void Main(string[] args)
        {
            scrapers_runners.Add(new Youtube_Scraper_Runner());
            while (true)
            {

                GUI_Runner previous_gui = guiRunner;

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
                    if (previous_gui != null)
                        previous_gui.kill();
                    guiRunner.run();
                }
                Console.WriteLine("finished everything for the day!");

                DateTime tomorrow = DateTime.Today.AddDays(1);
                
                while(DateTime.Today.CompareTo(tomorrow) != 0)
                {
                    System.Threading.Thread.Sleep(10000);
                }

                
            }
        }


    }
}
