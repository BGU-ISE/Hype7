using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Data;
using System.Text.RegularExpressions;
using Microsoft.Data.Sqlite;

namespace Hype7
{
    public static class DAL
    {
        private const String DB = @"DataBase.db";
        private const String Connection_String = @"Data Source=DataBase.db";
        private static string[] intFileldArr = {"id", "authorMeta_following", "authorMeta_fans", "authorMeta_heart", "authorMeta_video" };
        private static List<string> intFileld = new List<string>();
        private static bool isOverDay7;
        static private string checke;

        public static SQLiteConnection connection = null;

        public static void SetUpDB()
        {
            OpenConnect(); // create db if nesesery, and open it
            //InsertAllData(SystemManager.GetAllData(), true);
            int indexCreatesTable = CreateDateTable(SystemManager.GetFieldsName());
            InsertDataFromToday(SystemManager.GetData(DateTime.Now.ToString("dd-MM-yyyy")), indexCreatesTable, true); // DateTime.Now.ToString("dd-MM-yyyy")
            CalcPlayCountPerDay();
            //CalcPlayCountAllWeek(true);
            Console.WriteLine("finish setup for DB.");
        }
        
        public static void OpenConnect()
        {
            if (!System.IO.File.Exists(DB))
            {
                SQLiteConnection.CreateFile(DB);
                connection = new SQLiteConnection(Connection_String);
                connection.Open();
                InitTables(true);
                isOverDay7 = false;
            }
            
            if (connection == null)
                connection = new SQLiteConnection(Connection_String);
            if (connection.State == ConnectionState.Closed)
                connection.Open();

            Console.WriteLine("Open DB.");
        }
        public static void CloseConnect()
        {
            if (connection.State != ConnectionState.Closed)
                connection.Close();
        }
        private static int CreateDateTable(List<string> Fields)
        {
            try
            {
                string names = "";
                foreach (string name in Fields)
                {
                    if (name.Length > 0)
                        names += name + " TEXT, ";
                }
                names += "PullDate TEXT";
                int index = GetLastIndexTable(true) + 1;
                
                if(index <= 7)
                {
                    SQLiteCommand command = new SQLiteCommand("CREATE TABLE VideosInfoDay" + index + " (" + names + ")", connection);
                    command.ExecuteNonQuery();
                    command.Dispose();
                    return index;
                }
                isOverDay7 = true;
                FixTableNames();
                return 7;
            }
            catch (SqliteException e)
            {
                DAL.CloseConnect();
                return -1;
            }
        }
        private static void InitTables(bool isDBOpen)
        {
            try
            {
                if (!isDBOpen)
                    OpenConnect();
                SQLiteCommand command1 = new SQLiteCommand("CREATE TABLE FilterHypeScore (id TEXT, metric TEXT, SQLquery TEXT, score REAL, PRIMARY KEY('id'))", connection);
                command1.ExecuteNonQuery();
                SQLiteCommand command2 = new SQLiteCommand("CREATE TABLE ModelHypeScore (id TEXT, model1score REAL, PRIMARY KEY('id'))", connection);
                command2.ExecuteNonQuery();
                SQLiteCommand command3 = new SQLiteCommand("CREATE TABLE PlayCountPerDay (id TEXT, playCountDay1 INTEGER DEFAULT 0, playCountDay2 INTEGER DEFAULT 0, playCountDay3 INTEGER DEFAULT 0, playCountDay4 INTEGER DEFAULT 0, playCountDay5 INTEGER DEFAULT 0, playCountDay6 INTEGER DEFAULT 0, playCountDay7 INTEGER DEFAULT 0,playCountAllWeek INTEGER DEFAULT 0, PRIMARY KEY('id'))", connection);
                command3.ExecuteNonQuery();
                command1.Dispose();
                command2.Dispose();
                command3.Dispose();
                Console.WriteLine("Initialization tables.");
                if (!isDBOpen)
                    CloseConnect();
            }
            catch (SqliteException e)
            {
                DAL.CloseConnect();
            }
        }
        private static void FixTableNames()
        {
            // save VideosInfoDay1 in archion
            SQLiteCommand command = new SQLiteCommand("DELETE FROM VideosInfoDay1", connection);
            command.ExecuteNonQuery();
            command.Dispose();
            SQLiteCommand command2 = new SQLiteCommand("ALTER TABLE VideosInfoDay1 RENAME TO VideosInfoDay8", connection);
            command2.ExecuteNonQuery();
            command2.Dispose();

            for (int i=2; i<=8; i++)
            {
                //command = new SQLiteCommand("", connection);
                SQLiteCommand command1 = new SQLiteCommand("ALTER TABLE VideosInfoDay" + i + " RENAME TO VideosInfoDay" + (i - 1), connection);
                command1.ExecuteNonQuery();
                command1.Dispose();
            }
        }
        private static void InsertAllData(Dictionary<DateTime, List<VideoInfo>> Data, bool isDBOpen)
        {
            try
            {
                if (!isDBOpen)
                    OpenConnect();
                foreach (var date in Data.Keys)
                {
                    Console.WriteLine("upload information from day  " + date);
                    var Current = Data[date];
                    foreach (var element in Current)
                    {
                        VideoDAL.saveVideo(element, date.ToString(), true); // ---- date
                    }
                }
                if (!isDBOpen)
                    CloseConnect();
            }
            catch (SqliteException e)
            {
                DAL.CloseConnect();
            }
        }
        private static void InsertDataFromToday(List<VideoInfo> Data, int tableIndex, bool isDBOpen)
        {
            try
            {
                if (!isDBOpen)
                    OpenConnect();
                var date = DateTime.Now.ToString("dd-MM-yyyy");
                Console.WriteLine("upload information from day  " + date);
                foreach (var element in Data)
                {
                    element.setSerialDate(tableIndex);
                    VideoDAL.saveVideo(element, date, true);
                }
                if (!isDBOpen)
                    CloseConnect();
            }
            catch (SqliteException e)
            {
                DAL.CloseConnect();
            }
        }
        private static void RunMetric(string metric, bool isDBOpen)
        {
            try
            {
                if (!isDBOpen)
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
                if (!isDBOpen)
                    CloseConnect();
            }
            catch (SqliteException e)
            {
                DAL.CloseConnect();
            }

        }
        private static void RunQuery(string query, bool isDBOpen)
        {
            try
            {
                if (!isDBOpen)
                    OpenConnect();
                SQLiteCommand command = new SQLiteCommand(query, connection);
                command.ExecuteNonQuery();
                command.Dispose();
                if (!isDBOpen)
                    CloseConnect();
            }
            catch (SqliteException e)
            {
                DAL.CloseConnect();
            }

        }
        private static void CalcPlayCountPerDay()
        {
            try
            {
                SQLiteCommand command = new SQLiteCommand(null, DAL.connection);
                SQLiteCommand command2 = new SQLiteCommand(null, connection);
                SQLiteCommand commandVideo = new SQLiteCommand(null, connection);
                int i = GetLastIndexTable(true);
                isOverDay7 = true;
                if (isOverDay7)
                {
                    command = new SQLiteCommand("ALTER TABLE PlayCountPerDay DROP COLUMN playCountDay1", connection);
                    command.ExecuteNonQuery();
                    command.Dispose();

                    for (int j = 2; j <= 7; j++)
                    {
                        //command = new SQLiteCommand("", connection);
                        command = new SQLiteCommand("ALTER TABLE PlayCountPerDay RENAME COLUMN playCountDay" + j + " TO playCountDay" + (j - 1), connection);
                        command.ExecuteNonQuery();
                        command.Dispose();
                    }
                    command = new SQLiteCommand("ALTER TABLE PlayCountPerDay ADD COLUMN playCountDay7 INTEGER DEFAULT 0", connection);
                    command.ExecuteNonQuery();
                    command.Dispose();
                    i = 6;
                }

                //Console.WriteLine("calc play count for day number " + (i + 1));
                command = new SQLiteCommand("SELECT a.id, (b.PlayCount - a.PlayCount) as PlayPerDay From VideosInfoDay" + i + " a JOIN VideosInfoDay" + (i + 1) + " b ON a.id == b.id", connection);
                checke = command.CommandText;
                command.Prepare();
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    string id = reader["id"].ToString();
                    int playcount = int.Parse(reader["PlayPerDay"].ToString());
                    commandVideo = new SQLiteCommand("SELECT EXISTS(SELECT 1 FROM PlayCountPerDay WHERE id='" + id + "') as flag", DAL.connection);
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
                command.Dispose();
                command2.Dispose();
                commandVideo.Dispose();

                command = new SQLiteCommand("SELECT id, SUM(playCountDay1 + playCountDay2 + playCountDay3 + playCountDay4 + playCountDay5 + playCountDay6 + playCountDay7) as total FROM PlayCountPerDay GROUP BY id", connection);
                command.Prepare();
                var reader5 = command.ExecuteReader();
                while (reader5.Read())
                {
                    string id = reader5["id"].ToString();
                    int playcount = int.Parse(reader5["total"].ToString());
                    commandVideo = new SQLiteCommand("SELECT EXISTS(SELECT 1 FROM PlayCountPerDay WHERE id='" + id + "') as flag", DAL.connection);
                    //checke = commandVideo.CommandText;
                    commandVideo.Prepare();
                    var readerID = commandVideo.ExecuteReader();

                    readerID.Read();
                    var res = readerID[readerID.GetName(0)];
                    if (res.ToString().Equals("0"))
                    {
                        command2 = new SQLiteCommand("INSERT INTO PlayCountPerDay(id, playCountAllWeek) VALUES(" + id + ", " + playcount + ")", DAL.connection);
                    }
                    else
                    {
                        command2 = new SQLiteCommand("UPDATE PlayCountPerDay SET playCountAllWeek=" + playcount + " WHERE id=" + id, DAL.connection);
                    }
                    //checke = command2.CommandText;
                    command2.Prepare();
                    command2.ExecuteNonQuery();
                }
                reader5.Close();
                command.Dispose();
                command2.Dispose();
                commandVideo.Dispose();
            }
            catch (SqliteException e)
            {
                DAL.CloseConnect();
            }
        }
        private static void CalcPlayCountAllWeek(bool isDBOpen)
        {
            try
            {
                if (!isDBOpen)
                    DAL.OpenConnect();
                int n = GetLastIndexTable(true);
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
                if (!isDBOpen)
                    DAL.CloseConnect();
            }
            catch (SqliteException e)
            {
                DAL.CloseConnect();
            }
        }

        private static int GetLastIndexTable(bool isDBOpen)
        {
            float maxIndex = 0;
            try
            {
                if (!isDBOpen)
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
                        var readerVideo = commandVideo.ExecuteScalarAsync();
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
            catch (SqliteException e)
            {
                DAL.CloseConnect();
            }
            return (int)maxIndex;
        }
        private static bool isNumericField(string name)
        {
            return false;
        }

        // public functions, doesn't assume that the DB is open
        public static void InitTables(List<string> Fields)
        {
            InitTables(false);
        }
        public static void InsertAllData(Dictionary<DateTime, List<VideoInfo>> Data)
        {
            InsertAllData(Data, false);
        }
        public static int GetLastIndexTable()
        {
            return GetLastIndexTable(false);
        }
        public static void CalcPlayCountPerDayAll()
        {
            //CalcPlayCountPerDayAll(false);
        }
        public static void CalcPlayCountAllWeek()
        {
            CalcPlayCountAllWeek(false);
        }
        public static void RunQuery(string query)
        {
            RunQuery(query, false); 
        }
        public static void RunMetric(string metric)
        {
            RunMetric(metric, false);
        }
    }
}
