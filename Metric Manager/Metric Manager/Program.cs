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
            DataAnalysis();

            DateTime end = DateTime.Now;
            Console.WriteLine("duration: " + (end - start) + " min");
            Console.ReadLine();


            //var temp2 = SystemManager.GetResultByFieldAllTime("playCount", 5, )
            //Console.WriteLine("Top video by metric   -   Sum_i_1_4(playCountPerDay_i*(0.5)^i)\n");
            /*
            var temp = SystemManager.GetResultByMetricAllTime("Sum_i_1_3(playCountPerDay_i*(Sum_j_2_4(j+2))^i)+6/shareCount", 5);
            foreach (var element in temp)
            {
                Console.WriteLine(element.Print());
            }
            
            Console.WriteLine("Top hashtag:\n");
            var temp4 = SystemManager.GetTopHashtag(10, "23_12_21");
            foreach (var element in temp4)
            {
                Console.WriteLine(element.Print()); // "((playCount/shareCount)^2+commentCount)"
            }
            */
            //SystemManager.GetPlayCountPerDay("7024959365702550000", "20_12_21");
            //SystemManager.GetResultByField("playCount", 5, "19_12_21");
            //SystemManager.GetResultByField("playCount");
            //Metric.GetHashtag(Metric.GetIDByIndex(5));
            //SystemManager.GetResultByMetricAllTime("Sum_i_1_6(playCount_i*((0.5)^i-playCount_i))+7");
            //Metric.GetTopHashtag(20); Sum_1_6(playCount_i*((0.5)^i))
            //SystemManager.ReadAllFromCSV(@"C:\Users\Almogi\Desktop\Hype7\Data");
            //Metric.ReadFromText(@"C:\Users\Almogi\Desktop\Hype7\ignoreHashtag.txt"); dayOfCollect playCount
            //SystemManager.GetResultByMetricAllTime("((playCount/shareCount)^2+commentCount)", 5, "id, shareCount, playCount");

        }

        private static void StartDaySettings(string[] args)
        {
            SystemManager.InitializeData(args); // get ignore hashtags, get metrics, get path
            DAL.OpenConnect(); // create db and init table if doesnt exist, open db
            DAL.SetUpDB(); // insert today data, calc views today
            DAL.CloseConnect();
        }
        private static void DataAnalysis()
        {
            DAL.OpenConnect();
            DAL.CalcPlayCountPerDay();
            SystemManager.RunAllMetricsWeek();
            DAL.CloseConnect();
        }
    }
}
