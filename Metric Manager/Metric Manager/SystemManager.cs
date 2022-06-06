using Metric_Manager;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Hype7
{
    public static class SystemManager
    {

        static private Dictionary<DateTime, List<VideoInfo>> Data = new Dictionary<DateTime, List<VideoInfo>>();
        static private Dictionary<string, int> indexByName = new Dictionary<string, int>();
        static public string path = null;
        static private string dataDir = null;
        static private List<Metric> Metrics = new List<Metric>();
        static public List<string> ignoreHashtag = new List<string>();
        static private int hashtag;
        static public List<string> Fields = new List<string>();
        static public Dictionary<string, string> settings_dict = new Dictionary<string, string>();
        static public List<string> values_order = new List<string>();

        public static void InitializeData(string[] args)
        {
            if (path == null && args.Length == 0)
                path = Path.Combine(GetPath(), "Data");
            else if (args.Length > 0)
            {
                path = args[0];
                var arr = path.Split("\\");
                dataDir = arr[arr.Length - 1];
                path = path.Substring(0, path.Length - (dataDir.Length + 2));
            }
            ReadFromText(Path.Combine(path, "ignoreHashtag.txt"));
            LoadMetrics();
        }
        private static void Setup(string pathFolder, string ignoreHashPath)
        {
            ReadAllFromCSV(pathFolder);
            ReadFromText(ignoreHashPath);
            ManageDate();
        }
        private static void ManageDate()
        {
            var prevDate = Data.Keys.ToList().Min();
            foreach (var date in Data.Keys)
            {
                var difference = date - prevDate;
                int days = (int)difference.TotalDays + 1;
                var lst = Data[date];
                foreach (VideoInfo element in lst)
                {
                    element.setSerialDate(days);
                }
            }
        }
        private static List<VideoInfo> ReadFromCSV(string name, DateTime date, bool justFirst)
        {
            List<VideoInfo> dataCurrent = new List<VideoInfo>();
            using (var reader = new StreamReader(name))
            {
                bool isHeadLine = true;
                string file_content = "";
                List<string> lines = new List<string>();
                int count = 0;
                while (!reader.EndOfStream && count < 200000)
                {
                    char c = (char)reader.Read();
                    if (c == ((char)13) && !reader.EndOfStream)
                    {
                        char c2 = (char)reader.Read();
                        if (c2 == ((char)10))
                        {
                            var values = file_content.Split(',');
                            if (isHeadLine)
                            {
                                for (int i = 0; i < values.Length; i++)
                                {
                                    values[i] = values[i].Replace(" ", string.Empty);
                                    indexByName[values[i]] = i;
                                }
                                isHeadLine = false;
                            }
                            else
                            {
                                string[] arr = new string[values.Length];
                                for (int i = 0; i < values.Length; i++)
                                {
                                    arr[i] = values[i];
                                }
                                dataCurrent.Add(new VideoInfo(arr, date));
                                if (justFirst)
                                    return dataCurrent;
                            }
                            file_content = "";
                        }
                        else
                            file_content += c + c2;
                    }
                    else
                        file_content += c;
                    count++;
                }
                hashtag = indexByName["hashtags"];
            }
            //System.IO.File.Move(name, Path.Combine(path, @"Data\\ReadData"), true);
            return dataCurrent;
        }
        private static void ReadAllFromCSV(string path)
        {
            string[] fileArray = Directory.GetFiles(path);
            for (int i = 0; i < fileArray.Length; i++)
            {
                DateTime date = ConvertPathToDatetime(fileArray[i]);
                var temp = ReadFromCSV(fileArray[i], date, false);
                Data[date] = temp;
            }
        }
        private static void ReadFieldNameFromCSV(string path)
        {
            string[] fileArray = Directory.GetFiles(path);
            using (var reader = new StreamReader(fileArray[0]))
            {
                bool isHeadLine = true;
                string file_content = "";
                List<string> lines = new List<string>();
                while (!reader.EndOfStream)
                {
                    char c = (char)reader.Read();
                    if (c == ((char)13) && !reader.EndOfStream)
                    {
                        char c2 = (char)reader.Read();
                        if (c2 == ((char)10))
                        {
                            var values = file_content.Split(',');
                            if (isHeadLine)
                            {
                                for (int i = 0; i < values.Length; i++)
                                {
                                    values[i] = values[i].Replace(" ", string.Empty);
                                    indexByName[values[i]] = i;
                                }
                                isHeadLine = false;
                            }
                            else
                            {
                                break;
                            }
                            file_content = "";
                        }
                        else
                            file_content += c + c2;
                    }
                    else
                        file_content += c;

                }
            }
        }
        private static void ReadFromText(string name)
        {
            string text = System.IO.File.ReadAllText(name);
            text = text.Replace(" ", string.Empty);
            string[] ignore = text.Split(",");
            for (int i = 0; i < ignore.Length; i++)
            {
                ignoreHashtag.Add(ignore[i]);
            }
        }
        public static void SaveToText(int num)
        {
            //num++;
            using (var w = new StreamWriter(Path.Combine(path, "lastIndexTable.txt")))
            {
                w.WriteLine(num);
                w.Flush();
            }
        }
        private static Metric GetMetric(string metric, string name)
        {
            foreach (Metric element in Metrics)
            {
                if (element.GetMetric().Equals(metric) && element.GetName().Equals(name))
                    return element;
            }
            return null;
        }
        private static Metric GetMetric(string metric)
        {
            foreach (Metric element in Metrics)
            {
                if (element.GetMetric().Equals(metric))
                    return element;
            }
            return null;
        }
        public static void AddMetric(string metric)
        {
            if (GetMetric(metric) == null)
                Metrics.Add(new Metric(metric));
            else
            {
                Console.WriteLine("this metric exist");
            }
        }
        public static void AddMetric(string metric, string name)
        {
            Metric temp = GetMetric(metric);
            if (temp == null)
                Metrics.Add(new Metric(metric, name));
            else
            {
                Console.WriteLine("this metric exist");
                if (temp.GetName().Equals("empty"))
                {
                    RemoveMetric(temp);
                    Metrics.Add(new Metric(metric, name));
                    Console.WriteLine("update metrics name");
                }

            }
        }
        private static void RemoveMetric(Metric metric)
        {
            Metrics.Remove(metric);
        }
        public static void RunAllMetrics()
        {
            foreach (Metric metric in Metrics)
            {
                DAL.RunMetricLastDay(metric.GetMetric(), true);
            }
        }
        public static void RunAllMetricsWeek()
        {
            foreach (Metric metric in Metrics)
            {
                if (metric.GetName().Contains("special"))
                    DAL.RunSpecialAllWeek(metric.GetMetric(), true);
                else
                    DAL.RunMetricAllWeek(metric.GetMetric(), true);
            }
            Console.WriteLine("finish run all metrics.");
        }
        public static void RunMetricByName(string name)
        {
            foreach (Metric metric in Metrics)
            {
                if (metric.GetName().Equals(name))
                {
                    DAL.RunMetricLastDay(metric.GetMetric(), true);
                    return;
                }
            }
        }
        public static void LoadMetrics()
        {
            string text = System.IO.File.ReadAllText(Path.Combine(path, "metricToRun.txt"));

            string[] arr = text.Split(";;");
            for (int i = 0; i < arr.Length; i++)
            {
                string[] MetricInfo = arr[i].Split(";");
                Metrics.Add(new Metric(MetricInfo[0], MetricInfo[1]));
            }
        }
        public static void SaveMetrics()
        {
            File.WriteAllText(Path.Combine(path, "metricToRun.txt"), String.Empty);
            string ans = "";
            foreach (Metric metric in Metrics)
            {
                ans += metric.GetMetric() + ";" + metric.GetName() + ";;";
            }
            if (ans.Length > 0)
                ans = ans.Substring(0, ans.Length - 2);
            using (var w = File.AppendText(Path.Combine(path, "metricToRun.txt")))
            {
                w.WriteLine(ans);
                w.Flush();
            }

            string text = System.IO.File.ReadAllText(Path.Combine(path, "metricToRun.txt"));

            string[] arr = text.Split(";;");
            for (int i = 0; i < arr.Length; i++)
            {
                string[] MetricInfo = arr[i].Split(";");
                Metrics.Add(new Metric(MetricInfo[0], MetricInfo[1]));
            }
        }

        public static List<VideoInfo> GetResultByFieldAndTime(string fieldName, string time)
        {
            DateTime date = ConvertStringToDatetime(time);
            var tempData = Data[date];
            tempData.Sort((a, b) => b.GetFieldByIndex(indexByName[fieldName]).CompareTo(a.GetFieldByIndex(indexByName[fieldName])));
            return tempData;
        }
        public static List<VideoInfo> GetResultByFieldAndTime(string fieldName, int resultCount, string time)
        {
            var temp = GetResultByFieldAndTime(fieldName, time).GetRange(0, resultCount);
            return GetResultByFieldAndTime(fieldName, time).GetRange(0, resultCount);
        }
        public static List<VideoInfo> GetResultByFieldAndTime(string fieldName, int resultCount, string fieldReturn, string time)
        {
            var temp = selectField(fieldReturn, GetResultByFieldAndTime(fieldName, time).GetRange(0, resultCount));
            return temp;
        }

        public static List<VideoInfo> GetResultByFieldAllTime(string fieldName)
        {
            List<VideoInfo> all = new List<VideoInfo>();
            foreach (var element in Data.Keys)
            {
                all = MergeList(all, Data[element], true);
            }
            all.Sort((a, b) => b.GetFieldByIndex(indexByName[fieldName]).CompareTo(a.GetFieldByIndex(indexByName[fieldName])));
            return all;
        }
        public static List<VideoInfo> GetResultByFieldAllTime(string fieldName, int resultCount)
        {
            var temp = GetResultByFieldAllTime(fieldName).GetRange(0, resultCount);
            return GetResultByFieldAllTime(fieldName).GetRange(0, resultCount);
        }
        public static List<VideoInfo> GetResultByFieldAllTime(string fieldName, int resultCount, string fieldReturn)
        {
            var temp = selectField(fieldReturn, GetResultByFieldAllTime(fieldName).GetRange(0, resultCount));
            return temp;
        }

        public static List<VideoInfo> GetResultByMetricAndTime(string metric, string time)
        {
            DateTime date = ConvertStringToDatetime(time);
            var tempData = Data[date];

            MetricOld metricFun = new MetricOld(metric);
            tempData.Sort((a, b) => metricFun.Eval(b).CompareTo(metricFun.Eval(a)));
            return tempData;
        }
        public static List<VideoInfo> GetResultByMetricAndTime(string metric, int resultCount, string time)
        {
            return GetResultByMetricAndTime(metric, time).GetRange(0, resultCount);
        }
        public static List<VideoInfo> GetResultByMetricAndTime(string metric, int resultCount, string fieldReturn, string time)
        {
            //var temp = selectField(fieldReturn, GetResultByMetricAndTime(metric, time).GetRange(0, resultCount));
            return selectField(fieldReturn, GetResultByMetricAndTime(metric, time).GetRange(0, resultCount));
        }

        public static List<VideoInfo> GetResultByMetricAllTime(string metric)
        {
            MetricOld metricFun = new MetricOld(metric);
            List<VideoInfo> all = new List<VideoInfo>();
            foreach (var element in Data.Keys)
            {
                all = MergeList(all, Data[element], true);
            }
            all.Sort((a, b) => metricFun.Eval(b).CompareTo(metricFun.Eval(a)));
            return all;
        }
        public static List<VideoInfo> GetResultByMetricAllTime(string metric, int resultCount)
        {
            return GetResultByMetricAllTime(metric).GetRange(0, resultCount);
        }
        public static List<VideoInfo> GetResultByMetricAllTime(string metric, int resultCount, string fieldReturn)
        {
            var temp = selectField(fieldReturn, GetResultByMetricAllTime(metric).GetRange(0, resultCount));
            return selectField(fieldReturn, GetResultByMetricAllTime(metric).GetRange(0, resultCount));
        }

        public static double GetPlayCountPerDay(string id, string time)
        {
            DateTime date = ConvertStringToDatetime(time);
            var temp = date.AddDays(-1);
            var a = GetInfoByID(id, temp);
            if (a == null)
                return 0;
            string prev = a.GetFieldByIndex(indexByName["playCount"]);
            string now = GetInfoByID(id, date).GetFieldByIndex(indexByName["playCount"]);
            return double.Parse(now) - double.Parse(prev);
        }
        public static double GetPlayCountPerDay(string id, DateTime time)
        {
            //DateTime date = ConvertStringToDatetime(time);
            if (time.Equals(Data.Keys.First()))
                return 0;
            var temp = time.AddDays(-1);
            var a = GetInfoByID(id, temp);
            if (a == null)
                return 0;
            string prev = a.GetFieldByIndex(indexByName["playCount"]);
            var b = GetInfoByID(id, time);
            if (b == null)
                return 0;
            string now = b.GetFieldByIndex(indexByName["playCount"]);
            double ans = double.Parse(now) - double.Parse(prev);
            if (ans < 0)
                return 0;
            return ans;
        }
        public static string GetFieldPerDay(string id, string field, DateTime time)
        {
            return GetInfoByID(id, time).GetFieldByIndex(indexByName[field]);
        }

        public static List<string> GetHashtag(string id, string time)
        {
            DateTime date = ConvertStringToDatetime(time);
            var ans = Data[date][GetIndexByID(id, time)].GetHashtag(hashtag);
            return ans;
        }
        public static List<Hashtag> GetTopHashtag(int resultCount, string time)
        {
            DateTime date = ConvertStringToDatetime(time);
            List<Hashtag> hashtagCount = new List<Hashtag>();
            foreach (VideoInfo element in Data[date])
            {
                var temp = element.GetHashtag(hashtag);
                temp = RemoveIgnoreHashtag(temp);
                foreach (string hashString in temp)
                {
                    Hashtag hash = new Hashtag(hashString);
                    if (hashtagCount.Contains(hash))
                    {
                        var toRemove = GetHashtagByValue(hashString, hashtagCount);
                        hash = new Hashtag(toRemove);
                        hash.Add();
                        hashtagCount.Remove(toRemove);
                        hashtagCount.Add(hash);
                    }
                    else
                        hashtagCount.Add(hash);
                }
            }
            hashtagCount.Sort((a, b) => b.GetCount().CompareTo(a.GetCount()));
            if (hashtagCount.Count <= resultCount)
                return hashtagCount;
            hashtagCount = hashtagCount.GetRange(0, resultCount);
            return hashtagCount;
        }
        private static List<string> RemoveIgnoreHashtag(List<string> lst)
        {
            foreach (var element in ignoreHashtag)
            {
                lst.Remove(element);
            }
            return lst;
        }
        public static Hashtag GetHashtagByValue(string value, List<Hashtag> hashtagCount)
        {
            foreach (Hashtag element in hashtagCount)
            {
                if (element.GetValue().Equals(value))
                    return element;
            }
            return null;
        }

        public static VideoInfo GetInfoByID(string id, string time)
        {
            DateTime date = ConvertStringToDatetime(time);
            foreach (VideoInfo element in Data[date])
            {
                if (element.GetID().Equals(id))
                    return element;
            }
            return null;
        }
        public static VideoInfo GetInfoByID(string id, DateTime time)
        {
            //DateTime date = ConvertStringToDatetime(time);
            foreach (VideoInfo element in Data[time])
            {
                if (element.GetID().Equals(id))
                    return element;
            }
            return null;
        }
        public static string GetIDByIndex(int index, string time)
        {
            DateTime date = ConvertStringToDatetime(time);
            int i = 0;
            foreach (VideoInfo element in Data[date])
            {
                if (i == index)
                    return element.GetID();
            }
            return "";
        }
        public static int GetIndexByID(string id, string time)
        {
            DateTime date = ConvertStringToDatetime(time);
            int index = 0;
            foreach (VideoInfo element in Data[date])
            {
                if (element.GetID().Equals(id))
                    return index;
                index++;
            }
            return -1;
        }


        private static List<VideoInfo> selectField(string fieldReturn, List<VideoInfo> allData)
        {
            fieldReturn = fieldReturn.Replace(" ", string.Empty);
            string[] fieldSelected = fieldReturn.Split(",");

            List<VideoInfo> ans = new List<VideoInfo>();
            foreach (var element in allData)
            {
                string[] trim = new string[fieldSelected.Length];
                for (int i = 0; i < fieldSelected.Length; i++)
                {
                    trim[i] = element.GetFieldByIndex(indexByName[fieldSelected[i]]);
                }
                ans.Add(new VideoInfo(trim, element.GetDate()));
            }
            return ans;
        }
        private static DateTime ConvertStringToDatetime(string time)
        {
            var arr1 = Regex.Split(time, @"[a-zA-Z|(|)|+|/|\-|*|^|_|\.]");
            DateTime date = new DateTime();
            for (int j = 0; j < arr1.Length; j++)
            {
                if (arr1[j].Length > 0)
                {
                    if (j == 0 && arr1[j].Length == 2)
                        arr1[j] = "20" + arr1[j];
                    date = new DateTime(int.Parse(arr1[j]), int.Parse(arr1[j + 2]), int.Parse(arr1[j + 1]));
                    j = arr1.Length;
                }
            }
            return date;
        }
        public static DateTime ConvertPathToDatetime(string time)
        {
            string[] arr = time.Split("\\");
            return ConvertStringToDatetime(arr[arr.Length - 1]);
        }
        private static List<VideoInfo> MergeList(List<VideoInfo> lst1, List<VideoInfo> lst2, bool removeDuplicate)
        {
            List<VideoInfo> ans = lst1;
            foreach (var element in lst2)
            {
                if (removeDuplicate && ans.Contains(element))
                {
                    ans.Remove(element);
                }
                ans.Add(element);
            }
            return ans;
        }

        public static int GetIndexByFieldName(string name)
        {
            return indexByName[name];
        }
        public static DateTime GetDateByIndex(int index)
        {
            int i = 1;
            foreach (var element in Data.Keys)
            {
                if (i == index)
                    return element;
                i++;
            }
            return new DateTime();
        }
        public static List<string> GetFieldsName()
        {
            if (path == null)
                path = GetPath();
            //if (indexByName == null || indexByName.Count == 0)
            //    ReadFieldNameFromCSV(path + "\\UnReadData");
            if (Fields == null || Fields.Count == 0)
            {
                load_settings();
                Fields = values_order;
            }

            return Fields;
            //return indexByName.Keys.ToList();
        }
        private static List<string> load_settings()
        {
            //Dictionary<string, string>  settings_dict = new Dictionary<string, string>();
            //List<string> values_order = new List<string>();

            using (var reader = new StreamReader("../../../../../Scraper Manager/Scraper Manager/settings.txt"))
            {
                if (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(";;");
                    var pairs = new List<string[]>();
                    foreach (string pair in values)
                    {
                        if (!settings_dict.ContainsKey(pair.Split(";")[0]))
                        {
                            settings_dict.Add(pair.Split(";")[0], pair.Split(";")[1]);
                            values_order.Add(pair.Split(";")[1]);
                        }

                    }

                }
                if (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    string origin = line;
                }
            }
            return values_order;
        }
        public static Dictionary<DateTime, List<VideoInfo>> GetAllData()
        {
            if (path == null)
                path = GetPath();
            if (Data == null || Data.Count == 0)
            {
                Setup(path + "\\UnReadData", path + "\\ignoreHashtag.txt");
            }
            return Data;
        }
        public static List<VideoInfo> GetData(string currentDate)
        {
            if (path == null)
                path = GetPath();
            string[] fileArray = Directory.GetFiles(path + "\\UnReadData");
            for (int i = 0; i < fileArray.Length; i++)
            {
                DateTime date = ConvertPathToDatetime(fileArray[i]);
                if (date.ToString("dd-MM-yyyy").Equals(currentDate))
                {
                    return ReadFromCSV(fileArray[i], date, false);
                }
            }
            return null; // Data[date];
        }
        public static void SetNumericField()
        {
            if (path == null)
                path = GetPath();
            //string[] fileArray = Directory.GetFiles(path + "\\UnReadData");
            load_settings();
            string[] fileArray = Directory.GetFiles("../../../../../Scraper Manager/Scraper Manager/input_folder", "*.csv");
            for (int i = 0; i < fileArray.Length; i++)
            {
                DateTime date = ConvertPathToDatetime(fileArray[i]);
                //if (date.ToString("dd-MM-yyyy").Equals(currentDate))
                //{
                List<VideoInfo> lst = ReadFromCSV(fileArray[i], date, true);
                foreach (VideoInfo video in lst)
                {
                    string[] arr = video.GetData();
                    List<int> numbers = new List<int>();
                    for (int j = 0; j < arr.Length; j++)
                    {
                        arr[j] = arr[j].Substring(1, arr[j].Length - 2);
                        if (isNumeric(arr[j]))
                            numbers.Add(j);
                    }
                    foreach (string name in indexByName.Keys)
                    {
                        if (numbers.Contains(indexByName[name] + 1))
                        {
                            if (settings_dict.ContainsKey(name))
                                DAL.AddIntField(settings_dict[name]);
                        }
                    }
                }
                //}
            }
        }
        private static string GetPath()
        {
            var current = Directory.GetCurrentDirectory();
            string parent = System.IO.Directory.GetParent(current).FullName;
            parent = System.IO.Directory.GetParent(parent).FullName;
            parent = System.IO.Directory.GetParent(parent).FullName;
            return parent;
        }
        public static bool GetMetricTest()
        {
            return (Metrics != null && Metrics.Count > 0);
        }
        private static bool isNumeric(string value)
        {
            return Regex.IsMatch(value, "^[0-9\\s]+$");
        }
    }
}
