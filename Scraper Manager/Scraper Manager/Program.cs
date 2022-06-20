using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scraper_Manager
{
    class program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("start");
            
            DAL.SetUpDB(args);
            DAL.OpenConnect();
            ScraperManager.run(args);
            DAL.CloseConnect();
        }
    }
}
