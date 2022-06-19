using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System_Manager;

namespace System_ManagerTests
{
    /// <summary>
    /// use the youtube scraper
    /// </summary>
    class RealProxy : iProxy
    {
        public void metrics(string directory)
        {
            (new Metric_Manager_Runner(directory)).run();
        }

        public void model(string directory)
        {
            (new Model_Manager_Runner(directory)).run();
        }

        public Process runGui(string[] directory)
        {
            GUI_Runner runner = new GUI_Runner();
            foreach (var item in directory)
            {
                runner.add_db(item);
            }
            runner.run();
            return runner.process;
        }

        public void scraper(string directory)
        {
            Youtube_Scraper_Runner a = (new Youtube_Scraper_Runner(directory));
            Console.WriteLine(a.directory);
           //(new Youtube_Scraper_Runner(directory)).run();
        }

        public void scraper_manager(string directory)
        {
            (new Scraper_Manager_Runner(directory)).run();
        }
    }
}
