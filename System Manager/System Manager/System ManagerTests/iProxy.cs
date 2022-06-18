using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System_ManagerTests
{
    public interface iProxy
    {
        void scrapers(string directory);
        void scraper_manager(string directory);
        void metrics(string directory);
        void model(string directory);
    }
}
