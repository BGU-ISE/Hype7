using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SQLite;
using System.Data;
using System.Text.RegularExpressions;
using static UI.NumericMetricForm;
using static UI.Forms.ModelMetricForm;
using System.IO;
using static UI.Forms.TopHashtagsMetricForm;
using static UI.Forms.VideosDataForm;


namespace UI
{
    public static class DAL
    {
        public readonly static int SAVED_DAYS = 7;
        public static string currentDir = Environment.CurrentDirectory;
        //static readonly DirectoryInfo directory = new DirectoryInfo(
            //Path.GetFullPath(Path.Combine(currentDir, @"..\..\..\..\..\" + "Metric Manager\\Metric Manager\\bin\\Debug\\net5.0\\DataBase.db")));
        //public static string db_con = directory.ToString();
        private static String Connection_String = @"Data Source= ";
        public static string realPathDB;
        public static string SocialMedia = "youtube";
        private static bool isOverDay7;
        static public string checke;
        public static string IDName = "video_id";
        public static SQLiteConnection connection = null;
        static private List<Metric> realMetrics = new List<Metric>();

        public static Dictionary<string, List<MetricData>> Metrics = new Dictionary<string, List<MetricData>>();
        public static void OpenConnect()
        {
            if (connection == null)
                connection = new SQLiteConnection(Connection_String);
            if (connection.State == ConnectionState.Closed)
                connection.Open();

        }
        public static void SetUpDB(string[] args)
        {
            if (args.Length > 0)
            {
                realPathDB = args[0];
                Connection_String = @"Data Source=" + args[0] + "\\"+ SocialMedia + "\\DataBase.db";
            }
        }
        public static void ChangeDBName(string newName)
        {
            //string[] arr = Connection_String.Split("\\");
            //Connection_String = Connection_String.Substring(0, Connection_String.Length - arr[arr.Length - 1].Length) + newName;
            if(newName.Equals("youtube"))
                IDName = "video_id";
            if (newName.Equals("tiktok"))
                IDName = "id";
            SocialMedia = newName;
            Connection_String = @"Data Source=" + realPathDB + "\\"+ newName + "\\DataBase.db";
            connection = new SQLiteConnection(Connection_String);
        }
        public static void CloseConnect()
        {
            if (connection.State != ConnectionState.Closed)
                connection.Close();
        }

        public static List<VideoData> GetURLToHashtag(string hashtagData)
        {   var list = new List<VideoData>();   
            var set = new HashSet<string>();
            try
            {
                OpenConnect();
                
                SQLiteCommand command = new SQLiteCommand(null, DAL.connection);
                for (int i = 1; i < 7; i++)
                {
                    command = new SQLiteCommand("SELECT " + IDName +", title, channelName" + " From VideosInfoDay" + i + " WHERE tags LIKE '%" + hashtagData + "%'", DAL.connection);
                    checke = command.CommandText;
                    command.Prepare();
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        VideoData item = new VideoData(reader["title"].ToString(), "https://www.youtube.com/watch?v=" + reader[IDName].ToString(), reader["channelName"].ToString());
                        list.Add(item);
                    }
                    command.Dispose();
                }
            }
            catch (SQLiteException e)
            {
                DAL.CloseConnect();
            }
            return list;
        }

        public static List<HashtagData> GetHashtags(string orderBy, int limit)
        {
            List<HashtagData> ans = new List<HashtagData>();
            try
            {
                OpenConnect();

                SQLiteCommand command = new SQLiteCommand("SELECT * FROM Hashtags ORDER BY " + orderBy + " DESC LIMIT " + limit, DAL.connection);
                command.Prepare();

                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    HashtagData metricData = new HashtagData(reader["name"].ToString(), float.Parse(reader["slope"].ToString()), float.Parse(reader["averageScore"].ToString()), float.Parse(reader["scoreDay1"].ToString()), float.Parse(reader["scoreDay2"].ToString()), float.Parse(reader["scoreDay3"].ToString()), float.Parse(reader["scoreDay4"].ToString()), float.Parse(reader["scoreDay5"].ToString()), float.Parse(reader["scoreDay6"].ToString()), float.Parse(reader["scoreDay7"].ToString()));
                    //metricData.SetURL(GetVideoName(metricData) + " " + GetChannelName(metricData));
                    ans.Add(metricData);
                }

                command.Dispose();
                CloseConnect();
            }
            catch (SQLiteException e)
            {
                DAL.CloseConnect();
            }
            return ans;
        }
        public static List<MetricData> GetMetrics(string metric, string orderBy, int limit)
        {
            List<MetricData> ans = new List<MetricData>();
            try
            {
                OpenConnect();

                SQLiteCommand command = new SQLiteCommand("SELECT * FROM FilterHypeScore WHERE metric == '"+metric+"' ORDER BY "+orderBy+" DESC LIMIT "+limit, DAL.connection);
                command.Prepare();

                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    MetricData metricData = new MetricData(reader["metric"].ToString(), reader[IDName].ToString(), float.Parse(reader["slope"].ToString()), float.Parse(reader["averageScore"].ToString()),  float.Parse(reader["scoreDay1"].ToString()), float.Parse(reader["scoreDay2"].ToString()), float.Parse(reader["scoreDay3"].ToString()), float.Parse(reader["scoreDay4"].ToString()), float.Parse(reader["scoreDay5"].ToString()), float.Parse(reader["scoreDay6"].ToString()), float.Parse(reader["scoreDay7"].ToString()));
                    if(SocialMedia.Equals("youtube"))
                        metricData.SetURL(GetVideoName(metricData) + " " + GetChannelName(metricData));
                    else if (SocialMedia.Equals("tiktok"))
                        metricData.SetURL(GetVideoURLTiktok(metricData));
                    ans.Add(metricData);
                }
                
                command.Dispose();
                CloseConnect();
            }
            catch (SQLiteException e)
            {
                DAL.CloseConnect();
            }
            return ans;
        }

        public static List<ModelPrediction> GetModelPredictions(string socialMedia, string orderBy, int limit)
        {
            List<ModelPrediction> ans = new List<ModelPrediction>();
            try
            {
                ChangeDBName(socialMedia.Substring(0, socialMedia.Length - 5).ToLower());
                OpenConnect();
                SQLiteCommand command = new SQLiteCommand("SELECT * FROM ModelHypeScore ORDER BY " + orderBy + " DESC LIMIT " + limit, DAL.connection);
                command.Prepare();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    ModelPrediction metricData = null;
                    if (socialMedia == "YoutubeModel")
                    {
                        metricData = new ModelPrediction("https://www.youtube.com/watch?v=" + reader[IDName].ToString(), float.Parse(reader["model1score"].ToString()), float.Parse(reader["denormalize_score"].ToString()));
                    }
                    else if(socialMedia == "TiktokModel")
                    {
                        metricData = new ModelPrediction(reader["webVideoUrl"].ToString(), float.Parse(reader["model1score"].ToString()), float.Parse(reader["denormalize_score"].ToString()));
                    }
                    
                    ans.Add(metricData);
                }

                reader.Close();
                command.Dispose();
                CloseConnect();
            }
            catch (SQLiteException e)
            {
                DAL.CloseConnect();
            }
            return ans;
        }

        
        
        public static string GetChannelName(MetricData metricData)
        {
            string ans = "";
            int i = 1;
            if (metricData.Score7 > 0)
                i = 7;
            else if (metricData.Score6 > 0)
                i = 6;
            else if (metricData.Score5 > 0)
                i = 5;
            else if (metricData.Score4 > 0)
                i = 4;
            else if (metricData.Score3 > 0)
                i = 3;
            else if (metricData.Score2 > 0)
                i = 2;
            try
            {
                
                // ----- change to url
                SQLiteCommand command = new SQLiteCommand("SELECT channelName FROM VideosInfoDay"+i+" WHERE "+IDName+" == '" + metricData.ID + "'", DAL.connection);
                command.Prepare();

                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    ans = reader["channelName"].ToString();
                }

                command.Dispose();
                
            }
            catch (SQLiteException e)
            {
                DAL.CloseConnect();
            }
            return ans;
        }
        public static string GetVideoName(MetricData metricData)
        {
            string ans = "";
            int i = 1;
            if (metricData.Score7 > 0)
                i = 7;
            else if (metricData.Score6 > 0)
                i = 6;
            else if (metricData.Score5 > 0)
                i = 5;
            else if (metricData.Score4 > 0)
                i = 4;
            else if (metricData.Score3 > 0)
                i = 3;
            else if (metricData.Score2 > 0)
                i = 2;
            try
            {

                // ----- change to url
                SQLiteCommand command = new SQLiteCommand("SELECT title FROM VideosInfoDay" + i + " WHERE "+IDName+" == '" + metricData.ID + "'", DAL.connection);
                command.Prepare();

                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    ans = reader["title"].ToString();
                }

                command.Dispose();

            }
            catch (SQLiteException e)
            {
                DAL.CloseConnect();
            }
            return ans;
        }
        public static string GetVideoURLTiktok(MetricData metricData)
        {
            string ans = "";
            try
            {

                // ----- change to url
                SQLiteCommand command = new SQLiteCommand("SELECT webVideoRel FROM ModelHypeScore WHERE " + IDName + " == '" + metricData.ID + "'", DAL.connection);
                command.Prepare();

                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    ans = reader["webVideoRel"].ToString();
                }

                command.Dispose();

            }
            catch (SQLiteException e)
            {
                DAL.CloseConnect();
            }
            return ans;
        }
        public static List<string> GetMetricsNames()
        {
            List<string> ans = new List<string>();
            try
            {
                OpenConnect();
                SQLiteCommand command = new SQLiteCommand("SELECT DISTINCT metric FROM FilterHypeScore", DAL.connection);

                command.Prepare();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    string metric = reader["metric"].ToString();
                    //Metrics[metric] = new List<MetricData>();
                    ans.Add(metric);
                }
                command.Dispose();

                CloseConnect();
            }
            catch (SQLiteException e)
            {
                DAL.CloseConnect();
            }
            return ans;
        }
        public static List<string> GetFilterNames()
        {
            List<string> ans = new List<string>();
            try
            {
                OpenConnect();
                SQLiteCommand command = new SQLiteCommand("SELECT name FROM PRAGMA_TABLE_INFO('FilterHypeScore')", DAL.connection);

                command.Prepare();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    string metric = reader["name"].ToString();
                    if (!metric.Equals(IDName) && !metric.Equals("metric") && !metric.Equals("formula") && !metric.Contains("scoreDay"))
                        ans.Add(metric);
                }
                command.Dispose();

                CloseConnect();
            }
            catch (SQLiteException e)
            {
                DAL.CloseConnect();
            }
            return ans;
        }
        public static List<string> GetModelNames()
        {
            List<string> ans = new List<string>();
            try
            {
                OpenConnect();
                SQLiteCommand command = new SQLiteCommand("SELECT name FROM PRAGMA_TABLE_INFO('ModelHypeScore')", DAL.connection);

                command.Prepare();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    string metric = reader["name"].ToString();
                    ans.Add(metric);
                }
                command.Dispose();

                CloseConnect();
            }
            catch (SQLiteException e)
            {
                DAL.CloseConnect();
            }
            return ans;
        }

        // metrics
        public static List<string> GetMetricsFormula()
        {
            LoadMetrics();
            List<string> ans = new List<string>();
            foreach (Metric element in realMetrics)
            {
                ans.Add(element.GetMetric());
            }
            return ans;
        }
        public static void AddMetric(string MetricName, string MetricFormula)
        {
            LoadMetrics();

            Metric temp = GetMetric(MetricFormula);
            if (temp == null)
                realMetrics.Add(new Metric(MetricFormula, MetricName));
            else
            {
                Console.WriteLine("this metric exist");
                if (temp.GetName().Equals("empty"))
                {
                    RemoveMetric(temp);
                    realMetrics.Add(new Metric(MetricFormula, MetricName));
                    Console.WriteLine("update metrics name");
                }
            }

            SaveMetrics();

        }
        private static Metric GetMetric(string metric)
        {
            LoadMetrics();
            foreach (Metric element in realMetrics)
            {
                if (element.GetMetric().Equals(metric))
                    return element;
            }
            return null;
        }
        public static void RemoveMetric(string MetricFormula)
        {
            LoadMetrics();
            RemoveMetric(GetMetric(MetricFormula));
        }
        private static void RemoveMetric(Metric metric)
        {
            LoadMetrics();
            realMetrics.Remove(metric);
            SaveMetrics();
            //LoadMetrics();
        }
        private static void LoadMetrics()
        {
            string text = System.IO.File.ReadAllText(realPathDB + "\\"+SocialMedia+"\\metricToRun.txt");
            realMetrics = new List<Metric>();
            string[] arr = text.Split(";;");
            for (int i = 0; i < arr.Length; i++)
            {
                string[] MetricInfo = arr[i].Split(";");
                realMetrics.Add(new Metric(MetricInfo[0], MetricInfo[1]));
            }
        }
        public static void SaveMetrics()
        {
            File.WriteAllText(realPathDB + "\\" + SocialMedia + "\\metricToRun.txt", String.Empty);
            string ans = "";
            foreach (Metric metric in realMetrics)
            {
                ans += metric.GetMetric() + ";" + metric.GetName() + ";;";
            }
            if (ans.Length > 0)
                ans = ans.Substring(0, ans.Length - 2);
            using (var w = File.AppendText(realPathDB + "\\" + SocialMedia + "\\metricToRun.txt"))
            {
                w.WriteLine(ans);
                w.Flush();
            }
            
            LoadMetrics();
        }
        class Metric
        {
            private string name;
            private string input;
            private string[] fieldsNameForMetric;

            public Metric(string input)
            {
                this.name = "empty";
                this.input = input; //CalcMetricSum(input);
                fieldsNameForMetric = Regex.Split(input, @"[(|)|+|/|\-|*|^|\.]");
            }
            public Metric(string input, string name)
            {
                this.name = name;
                this.input = input; //CalcMetricSum(input);
                fieldsNameForMetric = Regex.Split(input, @"[(|)|+|/|\-|*|^|\.]");
            }
            public string GetMetric() { return input; }
            public string[] GetFields() { return fieldsNameForMetric; }
            public string GetName() { return name; }
        }
    }
}
