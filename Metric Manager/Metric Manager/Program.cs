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

            Console.WriteLine("hi");
            DateTime start = DateTime.Now;

            DataAnalysis(args);

            DateTime end = DateTime.Now;
            Console.WriteLine("duration: " + (end - start) + " min");
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
