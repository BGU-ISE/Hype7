using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System_ManagerTests
{
    public interface iProxy
    {
        void scrapers(string exePath, string outputFolder);
        void scraper_manager(string exePath, string inputPath, string DBPath);
        void metrics(string exePath, string DBPath);
        void model(string exePath, string DBPath);
    }
}
