using Metric_Manager;
using System;
using System.IO;

namespace Hype7
{
    class Program
    {// first you need to replce every enter (Ctrl+J) in the exel
     // second you need to replace every , in the exel
     // third chane the format to number zero
        static void Main(string[] args)
        {
            DateTime start = DateTime.Now;

            StartDaySettings(args);
            // scraper need to run
            //DataAnalysis();

            DateTime end = DateTime.Now;
            Console.WriteLine("duration: " + (end - start) + " min");
        }

        private static void StartDaySettings(string[] args)
        {
            SystemManager.InitializeData(args); // get ignore hashtags, get metrics, get path
            DAL.OpenConnect(); // create db and init table if doesnt exist, open db
            DAL.SetUpDB(); // insert today data, calc views today
            DAL.CloseConnect();
        }
        private static void DataAnalysis(string[] args)
        {
            SystemManager.InitializeData(args);
            DAL.OpenConnect();
            DAL.ResetDBToAnalysis();
            DAL.CalcPlayCountAllWeek(true);
            SystemManager.RunAllMetricsWeek();
            DAL.CloseConnect();
        }
    }
}
