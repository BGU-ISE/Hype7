using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SQLite;
using System.Data.SqlClient;
using System.Data;
using System.Text.RegularExpressions;

namespace Hype7
{
    public static class DAL
    {
        private const String DB = @"DataBase.db";
        private const String Connection_String = @"Data Source=DataBase.db;Version=3;";
        private static string[] intFileldArr = {"id", "authorMeta_following", "authorMeta_fans", "authorMeta_heart", "authorMeta_video" };
        private static List<string> intFileld = new List<string>();
        static private string checke;

        public static SQLiteConnection connection = null;

        public static void SetUpDB()
        {
            OpenConnect();
            CloseConnect();
            Console.WriteLine("finish setup for DB.");
        }
        public static void OpenConnect()
        {
            if (!System.IO.File.Exists(DB))
            {
                Console.WriteLine("Create DB.");
                SQLiteConnection.CreateFile(DB);
                connection = new SQLiteConnection(Connection_String);
                InitTables(SystemManager.GetFieldsName());
                Console.WriteLine("Initialization tables.");
                InsertData(SystemManager.GetAllData());
                Console.WriteLine("Insert data to DB.");
                if (connection.State == ConnectionState.Closed)
                    connection.Open();
                
            }
            else
            { // ------ remove else
                if (connection == null)
                    connection = new SQLiteConnection(Connection_String);
                if (connection.State == ConnectionState.Closed)
                    connection.Open();
            }
        }
        private static void InitTables(List<string> Fields)
        {
            try
            {
                OpenConnect();
                string names = "";
                foreach (string name in Fields)
                {
                    if (name.Length > 0)
                        names += name + " TEXT, ";
                }
                //names = names.Substring(0, names.Length - 3);
                SQLiteCommand command = new SQLiteCommand("", connection);
                names += "PullDate TEXT";
                for (int i = 1; i <= 7; i++)
                {
                    string sql = "CREATE TABLE VideosInfoDay" + i + " (" + names + ")";
                    command = new SQLiteCommand(sql, connection);
                    command.ExecuteNonQuery();
                }
                command.Dispose();
                SQLiteCommand command1 = new SQLiteCommand("CREATE TABLE FilterHypeScore (id TEXT, metric TEXT, SQLquery TEXT, score REAL, PRIMARY KEY('id'))", connection);
                command1.ExecuteNonQuery();
                SQLiteCommand command2 = new SQLiteCommand("CREATE TABLE ModelHypeScore (id TEXT, model1score REAL, PRIMARY KEY('id'))", connection);
                command2.ExecuteNonQuery();
                SQLiteCommand command3 = new SQLiteCommand("CREATE TABLE PlayCountPerDay (id TEXT, playCountDay1 INTEGER, playCountDay2 INTEGER, playCountDay3 INTEGER, playCountDay4 INTEGER, playCountDay5 INTEGER, playCountDay6 INTEGER, playCountDay7 INTEGER,playCountAllWeek INTEGER, PRIMARY KEY('id'))", connection);
                command3.ExecuteNonQuery();
                command1.Dispose();
                command2.Dispose();
                command3.Dispose();
                CloseConnect();
            }
            catch (SQLiteException e)
            {
                DAL.CloseConnect();
            }

        }
        private static bool isNumericField(string name)
        {
            return false;
        }
        private static void InsertData(Dictionary<DateTime, List<VideoInfo>> Data)
        {
            try
            {
                OpenConnect();
                foreach (var date in Data.Keys)
                {
                    Console.WriteLine("upload information from day  " + date);
                    var Current = Data[date];
                    foreach (var element in Current)
                    {
                        VideoDAL.saveVideo(element, date, true);
                    }
                }
                CloseConnect();
            }
            catch (SQLiteException e)
            {
                DAL.CloseConnect();
            }
        }
        public static int GetIndexOfNotEmptyTable()
        {
            float maxIndex = 0;
            try
            {
                OpenConnect();
                SQLiteCommand command = new SQLiteCommand(null, DAL.connection);
                command.CommandText = "SELECT name FROM sqlite_master WHERE type = 'table'";
                command.Prepare();
                var reader = command.ExecuteReader();
                SQLiteCommand commandVideo = new SQLiteCommand(null, DAL.connection);
                while (reader.Read())
                {
                    string tableName = reader["name"].ToString();
                    if (tableName.Contains("Video"))
                    {
                        commandVideo.CommandText = "SELECT id FROM " + tableName;
                        commandVideo.Prepare();
                        var readerVideo = commandVideo.ExecuteScalar(CommandBehavior.SingleResult);
                        if (readerVideo != null)
                        {
                            var result = Regex.Matches(tableName, @"[0-9]");
                            if (result.Count > 0)
                                maxIndex = MathF.Max(maxIndex, float.Parse(result[0].Value));
                        }
                    }
                }
                command.Dispose();
                commandVideo.Dispose();
                reader.Close();
            }
            catch (SQLiteException e)
            {
                DAL.CloseConnect();
            }
            return (int)maxIndex;
        }
        public static void RunMetric(string metric)
        {
            try
            {
                OpenConnect();
                SQLiteCommand command = new SQLiteCommand("SELECT id, ("+metric+") as score FROM VideosInfoDay1", connection);
                checke = command.CommandText;
                command.Prepare();
                var reader = command.ExecuteReader();
                SQLiteCommand command2 = new SQLiteCommand(null, connection);
                while (reader.Read())
                {
                    string id = reader["id"].ToString();
                    string score = reader["score"].ToString();
                    command2.CommandText = "INSERT INTO FilterHypeScore(id, metric, score) VALUES('"+ id + "', '"+ metric + "', '" + score + "')";
                    command2.Prepare();
                    command2.ExecuteNonQuery();
                }
                reader.Close();
                command.Dispose();
                command2.Dispose();
                CloseConnect();
            }
            catch (SQLiteException e)
            {
                DAL.CloseConnect();
            }

        }
        public static void RunQuery(string query)
        {
            try
            {
                OpenConnect();
                SQLiteCommand command = new SQLiteCommand(query, connection);
                command.ExecuteNonQuery();
                command.Dispose();
                CloseConnect();
            }
            catch (SQLiteException e)
            {
                DAL.CloseConnect();
            }

        }
        public static void RunQueryIfOpen()
        {
            float maxIndex = 0;
            try
            {
                OpenConnect();
                SQLiteCommand command = new SQLiteCommand(null, DAL.connection);
                command.CommandText = "SELECT a.id, (b.PlayCount - a.PlayCount) as PlayPerDay From VideosInfoDay1 a JOIN VideosInfoDay2 b ON a.id == b.id";
                command.Prepare();
                var reader = command.ExecuteReader();
                SQLiteCommand commandVideo = new SQLiteCommand(null, DAL.connection);
                while (reader.Read())
                {
                    string tableName = reader["id"].ToString();
                    string playcount = reader["PlayPerDay"].ToString();
                    int i = 0;
                }
                command.Dispose();
                commandVideo.Dispose();
                reader.Close();
            }
            catch (SQLiteException e)
            {
                DAL.CloseConnect();
            }
        }
        public static void CalcPlayCountPerDay()
        {
            try
            {
                DAL.OpenConnect();
                int n = GetIndexOfNotEmptyTable();
                SQLiteCommand command = new SQLiteCommand(null, DAL.connection);
                SQLiteCommand command2 = new SQLiteCommand(null, connection);
                for (int i=1; i<n; i++)
                {
                    Console.WriteLine("calc play count for day number " + (i+1));
                    command.CommandText = "SELECT a.id, (b.PlayCount - a.PlayCount) as PlayPerDay From VideosInfoDay"+i+ " a JOIN VideosInfoDay" + (i+1)+" b ON a.id == b.id";
                    checke = command.CommandText;
                    command.Prepare();
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        string id = reader["id"].ToString();
                        int playcount = int.Parse(reader["PlayPerDay"].ToString());
                        SQLiteCommand commandVideo = new SQLiteCommand("SELECT EXISTS(SELECT 1 FROM PlayCountPerDay WHERE id='"+ id + "') as flag", DAL.connection);
                        checke = commandVideo.CommandText;
                        commandVideo.Prepare();
                        var readerID = commandVideo.ExecuteReader();
                        
                        readerID.Read();
                        var res = readerID[readerID.GetName(0)];
                        if (res.ToString().Equals("0"))
                        {
                            command2.CommandText = "INSERT INTO PlayCountPerDay(id, playCountDay" + (i + 1) +") VALUES("+ id + ", "+ playcount + ")";
                        }
                        else
                        {
                            command2.CommandText = "UPDATE PlayCountPerDay SET playCountDay" + (i + 1) + "="+ playcount + " WHERE id=" + id;
                        }
                        checke = command2.CommandText;
                        command2.Prepare();
                        command2.ExecuteNonQuery();
                    }
                    reader.Close();
                }
                command.Dispose();
                command2.Dispose();
                DAL.CloseConnect();
            }
            catch (SQLiteException e)
            {
                DAL.CloseConnect();
            }
        }
        public static void CalcPlayCountAllWeek()
        {
            try
            {
                DAL.OpenConnect();
                int n = GetIndexOfNotEmptyTable();
                SQLiteCommand command = new SQLiteCommand(null, DAL.connection);
                SQLiteCommand command2 = new SQLiteCommand(null, connection);
                for (int i = 1; i < n; i++)
                {
                    Console.WriteLine(i);
                    command.CommandText = "SELECT a.id, (b.PlayCount - a.PlayCount) as PlayPerDay From VideosInfoDay" + i + " a JOIN VideosInfoDay" + (i + 1) + " b ON a.id == b.id";
                    checke = command.CommandText;
                    command.Prepare();
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        string id = reader["id"].ToString();
                        int playcount = int.Parse(reader["PlayPerDay"].ToString());
                        SQLiteCommand commandVideo = new SQLiteCommand("SELECT EXISTS(SELECT 1 FROM PlayCountPerDay WHERE id='" + id + "') as flag", DAL.connection);
                        checke = commandVideo.CommandText;
                        commandVideo.Prepare();
                        var readerID = commandVideo.ExecuteReader();

                        readerID.Read();
                        var res = readerID[readerID.GetName(0)];
                        if (res.ToString().Equals("0"))
                        {
                            command2.CommandText = "INSERT INTO PlayCountPerDay(id, playCountDay" + (i + 1) + ") VALUES(" + id + ", " + playcount + ")";
                        }
                        else
                        {
                            command2.CommandText = "UPDATE PlayCountPerDay SET playCountDay" + (i + 1) + "=" + playcount + " WHERE id=" + id;
                        }
                        checke = command2.CommandText;
                        command2.Prepare();
                        command2.ExecuteNonQuery();
                    }
                    reader.Close();
                }
                command.Dispose();
                command2.Dispose();
                DAL.CloseConnect();
            }
            catch (SQLiteException e)
            {
                DAL.CloseConnect();
            }
        }
        public static void CloseConnect()
        {
            if (connection.State != ConnectionState.Closed)
                connection.Close();
        }
    }
}
