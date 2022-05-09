using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SQLite;
using System.Data;
using System.Text.RegularExpressions;
using static UI.Form2;

namespace UI
{
    public static class DAL
    {
        public readonly static int SAVED_DAYS = 7;
        private const String DBname = @"DataBase.db";

        private static String Connection_String = @"Data Source=C:\Users\Almogi\Desktop\githubtry\Project\Hype7\Metric Manager\Metric Manager\bin\Debug\net5.0\DataBase.db";
        private static bool isOverDay7;
        static public string checke;

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
                    MetricData metricData = new MetricData(reader["metric"].ToString(), reader["metric"].ToString(), reader["id"].ToString(), float.Parse(reader["slope"].ToString()), float.Parse(reader["averageScore"].ToString()), reader["formula"].ToString(), float.Parse(reader["scoreDay1"].ToString()), float.Parse(reader["scoreDay2"].ToString()), float.Parse(reader["scoreDay3"].ToString()), float.Parse(reader["scoreDay4"].ToString()), float.Parse(reader["scoreDay5"].ToString()), float.Parse(reader["scoreDay6"].ToString()), float.Parse(reader["scoreDay7"].ToString()));
                    metricData.SetURL(GetURL(metricData));
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
        public static string GetURL(MetricData metricData)
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
                SQLiteCommand command = new SQLiteCommand("SELECT text FROM VideosInfoDay"+i+" WHERE id == '" + metricData.ID + "'", DAL.connection);
                command.Prepare();

                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    ans = reader["text"].ToString();
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
    }
}
