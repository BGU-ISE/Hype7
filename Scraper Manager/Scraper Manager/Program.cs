﻿using System;
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
            //args[0] = "C:\\Users\\Almogi\\Desktop\\githubtry\\Project\\Hype7\\System Manager\\System Manager\\System Manager\\Data\\youtube";
            DAL.SetUpDB(args);
            DAL.OpenConnect();
            ScraperManager.run(args);
            DAL.CloseConnect();
        }
    }
}
