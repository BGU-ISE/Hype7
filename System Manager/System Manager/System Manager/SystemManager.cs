using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace System_Manager
{
    public class SystemManager
    {
        static Runner metric_runner = new Metric_Manager_Runner();
        static Runner scraper_manager_runner = new Scraper_Manager_Runner();
        static Runner model_runner = new model_Manager_Runner();
        static Runner ui_runner = new UI_Runner();
        static List<Runner> scrapers_runners = new List<Runner>();




        static void DailyLoop(object sender, ElapsedEventArgs e)
        {
            ui_runner.kill();
            using (var countdownEvent = new CountdownEvent(scrapers_runners.Count()))
            {
                foreach (var runner in scrapers_runners)
                {

                    ThreadPool.QueueUserWorkItem((Object stateInfo) => { runner.run(); countdownEvent.Signal(); });
                }
                countdownEvent.Wait();
            }
            Console.WriteLine("all scrapers finished running");
            metric_runner.run();
            model_runner.run();
            ui_runner.run();
        }




        


        static void Main(string[] args)
        {

            scrapers_runners.Add(new Youtube_Scraper_Runner());

            const double interval60Minutes = 100;// 60 * 60 * 1000 * 24; // milliseconds to one day

            System.Timers.Timer checkForTime = new System.Timers.Timer(interval60Minutes);
            checkForTime.Elapsed += new ElapsedEventHandler(DailyLoop);
            checkForTime.Enabled = true;

            while(true)
                System.Threading.Thread.Sleep(int.MaxValue);
        }


    }
}
