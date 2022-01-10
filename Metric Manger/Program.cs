using System;

namespace Hype7
{
    class Program
    {// first you need to replce every enter (Ctrl+J) in the exel
     // second you need to replace every , in the exel
     // third chane the format to number zero
        static void Main(string[] args)
        {
            //SystemManager.Setup(@"C:\Users\Almogi\Desktop\Hype7\Data", @"C:\Users\Almogi\Desktop\Hype7\ignoreHashtag.txt");
            //Metric.ReadFromCSV(@"C:\Users\Almogi\source\repos\Hype7\tiktok_17_12_21.csv");//"Sum_i_1_3(playCount_i*((0.5)^i)-playCount_i)+1*(2-9)"
            /*
            Console.WriteLine("\n");
            Console.WriteLine("Top 5 video by view:\n");
            var temp1 = SystemManager.GetResultByFieldAllTime("playCount", 5, "id, playCount, shareCount");
            foreach (var element in temp1)
            {
                Console.WriteLine(element.PrintAll());
            }
            */

            //var temp2 = SystemManager.GetResultByFieldAllTime("playCount", 5, )
            //Console.WriteLine("Top video by metric   -   Sum_i_1_4(playCountPerDay_i*(0.5)^i)\n");
            //DAL.OpenConnect();
            DateTime start = DateTime.Now;
            //DAL.CalcPlayCountPerDay();
            Console.Write("Insert metric: ");
            string metric = Console.ReadLine();
            DAL.RunMetric(metric);
            //DAL.CloseConnect();
            DateTime end = DateTime.Now;
            Console.WriteLine("Finish Test");
            Console.WriteLine("duration: " + (end - start) + " min");
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
            Console.ReadLine();
        }
    }
}
