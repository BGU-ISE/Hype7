using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System_Manager;

namespace System_ManagerTests
{
    class RealProxy : iProxy
    {
        public void metrics(string directory)
        {
            (new Metric_Manager_Runner(directory)).run();
        }

        public void model(string directory)
        {
            throw new NotImplementedException();
        }

        public void scrapers(string directory)
        {
            throw new NotImplementedException();
        }

        public void scraper_manager(string directory)
        {
            throw new NotImplementedException();
        }
    }
}
