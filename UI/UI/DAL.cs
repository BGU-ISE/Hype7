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

namespace UI
{
    public static class DAL
    {
        public readonly static int SAVED_DAYS = 7;
        private const String DBname = @"DataBase.db";
        public static string currentDir = Environment.CurrentDirectory;
        static readonly DirectoryInfo directory = new DirectoryInfo(
            Path.GetFullPath(Path.Combine(currentDir, @"..\..\..\..\..\" + "Metric Manager\\Metric Manager\\bin\\Debug\\net5.0\\DataBase.db")));
        public static string db_con = directory.ToString();
        private static String Connection_String = @"Data Source= " + db_con;
        private static bool isOverDay7;
        static public string checke;
        public static string IDName = "video_id";
        public static SQLiteConnection connection = null;

        public static Dictionary<string, List<MetricData>> Metrics = new Dictionary<string, List<MetricData>>();
        public static void OpenConnect()
        {
            if (connection == null)
                connection = new SQLiteConnection(Connection_String);
            if (connection.State == ConnectionState.Closed)
                connection.Open();

        }
        public static void CloseConnect()
        {
            if (connection.State != ConnectionState.Closed)
                connection.Close();
        }

        public static HashSet<string> GetURLToHashtag(string hashtagData)
        {   
            var set = new HashSet<string>();
            try
            {
                OpenConnect();
                
                SQLiteCommand command = new SQLiteCommand(null, DAL.connection);
                for (int i = 1; i < 7; i++)
                {
                    command = new SQLiteCommand("SELECT " + IDName + " From VideosInfoDay" + i + " WHERE tags LIKE '%" + hashtagData + "%'", DAL.connection);
                    checke = command.CommandText;
                    command.Prepare();
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        set.Add("https://www.youtube.com/watch?v=" + reader[IDName].ToString());
                    }
                    command.Dispose();
                }
            }
            catch (SQLiteException e)
            {
                DAL.CloseConnect();
            }
            return set;
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
                    MetricData metricData = new MetricData(reader["metric"].ToString(), reader["video_id"].ToString(), float.Parse(reader["slope"].ToString()), float.Parse(reader["averageScore"].ToString()),  float.Parse(reader["scoreDay1"].ToString()), float.Parse(reader["scoreDay2"].ToString()), float.Parse(reader["scoreDay3"].ToString()), float.Parse(reader["scoreDay4"].ToString()), float.Parse(reader["scoreDay5"].ToString()), float.Parse(reader["scoreDay6"].ToString()), float.Parse(reader["scoreDay7"].ToString()));
                    metricData.SetURL(GetVideoName(metricData) + " " + GetChannelName(metricData));
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

        public static List<ModelPrediction> GetModelPredictions(string socialMedia, int limit)
        {
            List<ModelPrediction> ans = new List<ModelPrediction>();
            try
            {
                OpenConnect();
                SQLiteCommand command = new SQLiteCommand("SELECT * FROM "+ socialMedia +" ORDER BY model1score DESC LIMIT " + limit, DAL.connection);
                command.Prepare();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    ModelPrediction metricData = new ModelPrediction(reader["id"].ToString(),  float.Parse(reader["model1score"].ToString()));
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
                SQLiteCommand command = new SQLiteCommand("SELECT channelName FROM VideosInfoDay"+i+" WHERE video_id == '" + metricData.ID + "'", DAL.connection);
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
                SQLiteCommand command = new SQLiteCommand("SELECT title FROM VideosInfoDay" + i + " WHERE video_id == '" + metricData.ID + "'", DAL.connection);
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
                    if (!metric.Equals("video_id") && !metric.Equals("metric") && !metric.Equals("formula") && !metric.Contains("scoreDay"))
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

    }
}
