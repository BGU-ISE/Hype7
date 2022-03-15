using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Data;
using System.Text.RegularExpressions;
using Aspose.Cells;
using Metric_Manager;

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
            int indexCreatesTable = CreateDateTable(SystemManager.GetFieldsName());
            InsertDataFromToday(SystemManager.GetData(DateTime.Now.ToString("dd-MM-yyyy")), indexCreatesTable, true); // DateTime.Now.ToString("dd-MM-yyyy")
            CalcPlayCountPerDay();
            RunMetricLastDay("playCount", true);
            CloseConnect();
            //InitTables(true);
            //RunMetricAllWeek("(playCount/commentCount)+shareCount", true);
            //RunMetricLastDay("playCount", true);
            //InsertAllData(SystemManager.GetAllData(), true);
            //int indexCreatesTable = CreateDateTable(SystemManager.GetFieldsName());
            //InsertDataFromToday(SystemManager.GetData(DateTime.Now.ToString("dd-MM-yyyy")), indexCreatesTable, true); // DateTime.Now.ToString("dd-MM-yyyy")
            //CalcPlayCountPerDay();
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
            catch (SQLiteException e)
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
                SQLiteCommand command1 = new SQLiteCommand("CREATE TABLE FilterHypeScore (id TEXT, metric TEXT, SQLquery TEXT, averageScore REAL DEFAULT 0.0, slope REAL DEFAULT 0.0, formula TEXT, scoreDay1 REAL DEFAULT 0.0, scoreDay2 REAL DEFAULT 0.0, scoreDay3 REAL DEFAULT 0.0, scoreDay4 REAL DEFAULT 0.0, scoreDay5 REAL DEFAULT 0.0, scoreDay6 REAL DEFAULT 0.0, scoreDay7 REAL DEFAULT 0.0)", connection);
                command1.ExecuteNonQuery();
                SQLiteCommand command2 = new SQLiteCommand("CREATE TABLE ModelHypeScore (id TEXT, model1score REAL, PRIMARY KEY('id'))", connection);
                //command2.ExecuteNonQuery();
                SQLiteCommand command3 = new SQLiteCommand("CREATE TABLE PlayCountPerDay (id TEXT, playCountDay1 INTEGER DEFAULT 0, playCountDay2 INTEGER DEFAULT 0, playCountDay3 INTEGER DEFAULT 0, playCountDay4 INTEGER DEFAULT 0, playCountDay5 INTEGER DEFAULT 0, playCountDay6 INTEGER DEFAULT 0, playCountDay7 INTEGER DEFAULT 0,playCountAllWeek INTEGER DEFAULT 0, PRIMARY KEY('id'))", connection);
                //command3.ExecuteNonQuery();
                command1.Dispose();
                command2.Dispose();
                command3.Dispose();
                Console.WriteLine("Initialization tables.");
                if (!isDBOpen)
                    CloseConnect();
            }
            catch (SQLiteException e)
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
            catch (SQLiteException e)
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
            catch (SQLiteException e)
            {
                DAL.CloseConnect();
            }
        }
        private static void RunMetricLastDay(string metric, bool isDBOpen)
        {
            try
            {
                if (!isDBOpen)
                    OpenConnect();
                int i = GetLastIndexTable(true);
                isOverDay7 = true;
                if (isOverDay7)
                {
                    moveColumn("FilterHypeScore", "scoreDay");
                    i = 7;
                }

                SQLiteCommand command = new SQLiteCommand("SELECT id, (" + metric + ") as scoreDay" + i + " FROM VideosInfoDay" + i + " ORDER BY scoreDay" + i + " DESC", connection);
                command.Prepare();
                var reader = command.ExecuteReader();
                SaveResultToDB(reader, "FilterHypeScore", "scoreDay" + i);
                command.Dispose();

                command = new SQLiteCommand("SELECT id, scoreDay1, scoreDay2, scoreDay3, scoreDay4, scoreDay5, scoreDay6, scoreDay7 FROM FilterHypeScore WHERE metric='empty'", connection);
                command.Prepare();
                var reader2 = command.ExecuteReader();
                SaveResultToDB(reader2, "FilterHypeScore", new string[4] { "averageScore", "slope", "formula", "metric" }, new string[4] { "averageScore", "slope", "formula", "'" + metric + "'" });
                command.Dispose();

                if (!isDBOpen)
                    CloseConnect();
            }
            catch (SQLiteException e)
            {
                DAL.CloseConnect();
            }

        }
        private static void RunMetricAllWeek(string metric, bool isDBOpen)
        {
            try
            {
                if (!isDBOpen)
                    OpenConnect();
                int n = GetLastIndexTable(true);
                
                SQLiteCommand command = new SQLiteCommand(null, DAL.connection);
                for (int i = 1; i < n; i++)
                {
                    command = new SQLiteCommand("SELECT id, (" + metric + ") as scoreDay" + i + " FROM VideosInfoDay" + i + " ORDER BY scoreDay" + i + " DESC", connection);
                    command.Prepare();
                    var reader = command.ExecuteReader();
                    SaveResultToDB(reader, "FilterHypeScore", "scoreDay" + i);
                    command.Dispose();
                }

                command = new SQLiteCommand("SELECT id, scoreDay1, scoreDay2, scoreDay3, scoreDay4, scoreDay5, scoreDay6, scoreDay7 FROM FilterHypeScore WHERE metric='empty'", connection);
                command.Prepare();
                var reader2 = command.ExecuteReader();
                SaveResultToDB(reader2, "FilterHypeScore", new string[4] { "averageScore", "slope", "formula", "metric" }, new string[4] { "averageScore", "slope", "formula", "'" + metric + "'" });
                command.Dispose();

                if (!isDBOpen)
                    CloseConnect();
            }
            catch (SQLiteException e)
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
            catch (SQLiteException e)
            {
                DAL.CloseConnect();
            }

        }
        private static void CalcPlayCountPerDay()
        {
            try
            {
                SQLiteCommand command = new SQLiteCommand(null, DAL.connection);
                int i = GetLastIndexTable(true);
                //isOverDay7 = true;

                if (isOverDay7)
                {
                    moveColumn("PlayCountPerDay", "playCountDay");
                    i = 6;
                }

                command = new SQLiteCommand("SELECT a.id, (b.PlayCount - a.PlayCount) as playCountDay From VideosInfoDay" + i + " a JOIN VideosInfoDay" + (i + 1) + " b ON a.id == b.id", connection);
                command.Prepare();
                var reader = command.ExecuteReader();
                SaveResultToDB(reader, "PlayCountPerDay", "playCountDay"+ (i + 1));
                command.Dispose();

                command = new SQLiteCommand("SELECT id, SUM(playCountDay1 + playCountDay2 + playCountDay3 + playCountDay4 + playCountDay5 + playCountDay6 + playCountDay7) as playCountAllWeek FROM PlayCountPerDay GROUP BY id", connection);
                command.Prepare();
                var reader5 = command.ExecuteReader();
                SaveResultToDB(reader5, "PlayCountPerDay", "playCountAllWeek");
                command.Dispose();
            }
            catch (SQLiteException e)
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
                for (int i = 1; i < n; i++)
                {
                    command = new SQLiteCommand("SELECT a.id, (b.PlayCount - a.PlayCount) as playCountDay From VideosInfoDay" + i + " a JOIN VideosInfoDay" + (i + 1) + " b ON a.id == b.id", DAL.connection);
                    //checke = command.CommandText;
                    command.Prepare();
                    var reader = command.ExecuteReader();
                    SaveResultToDB(reader, "PlayCountPerDay", "playCountDay"+ (i + 1));
                    command.Dispose();
                }

                if (!isDBOpen)
                    DAL.CloseConnect();
            }
            catch (SQLiteException e)
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
            catch (SQLiteException e)
            {
                DAL.CloseConnect();
            }
            return (int)maxIndex;
        }
        private static bool isNumericField(string name)
        {
            return false;
        }
        private static void moveColumn(string tableName, string columnName)
        {
            SQLiteCommand command = new SQLiteCommand("ALTER TABLE " + tableName + " DROP COLUMN " + columnName + "1", connection);
            command.ExecuteNonQuery();
            command.Dispose();

            for (int j = 2; j <= 7; j++)
            {
                //command = new SQLiteCommand("", connection);
                command = new SQLiteCommand("ALTER TABLE " + tableName + " RENAME COLUMN " + columnName + j + " TO " + columnName + (j - 1), connection);
                command.ExecuteNonQuery();
                command.Dispose();
            }
            command = new SQLiteCommand("ALTER TABLE " + tableName + " ADD COLUMN " + columnName + "7 INTEGER DEFAULT 0", connection);
            command.ExecuteNonQuery();
            command.Dispose();
        }
        private static void SaveResultToDB(SQLiteDataReader reader, string tableName, string columnName)
        {
            SQLiteCommand command2 = new SQLiteCommand(null, connection);
            SQLiteCommand commandVideo = new SQLiteCommand(null, connection);

            while (reader.Read())
            {
                string id = reader["id"].ToString();
                var playcount = reader[columnName].ToString();
                if(tableName.Equals("PlayCountPerDay"))
                    commandVideo = new SQLiteCommand("SELECT EXISTS(SELECT 1 FROM " + tableName + " WHERE id='" + id + "') as flag", DAL.connection);
                else
                    commandVideo = new SQLiteCommand("SELECT EXISTS(SELECT 1 FROM " + tableName + " WHERE id='" + id + "' AND metric='empty') as flag", DAL.connection);
                commandVideo.Prepare();
                //checke = commandVideo.CommandText;
                var readerID = commandVideo.ExecuteReader();
                readerID.Read();
                var res = readerID[readerID.GetName(0)];

                if (res.ToString().Equals("0"))
                {
                    command2.CommandText = "INSERT INTO " + tableName + "(id, " + columnName + ") VALUES(" + id + ", " + playcount + ")";
                }
                else
                {
                    command2.CommandText = "UPDATE " + tableName + " SET " + columnName + "=" + playcount + " WHERE id=" + id + " AND metric='empty'";
                }
                //checke = command2.CommandText;
                command2.Prepare();
                command2.ExecuteNonQuery();
            }
            reader.Close();
            command2.Dispose();
            commandVideo.Dispose();
        }
        private static void SaveResultToDB(SQLiteDataReader reader, string tableName, string[] columnName, string[] result)
        {
            SQLiteCommand command2 = new SQLiteCommand(null, connection);
            SQLiteCommand commandVideo = new SQLiteCommand(null, connection);

            while (reader.Read())
            {
                string id = reader["id"].ToString();

                if (tableName.Equals("PlayCountPerDay"))
                    commandVideo = new SQLiteCommand("SELECT EXISTS(SELECT 1 FROM " + tableName + " WHERE id='" + id + "') as flag", DAL.connection);
                else
                    commandVideo = new SQLiteCommand("SELECT EXISTS(SELECT 1 FROM " + tableName + " WHERE id='" + id + "' AND metric='empty') as flag", DAL.connection);
                commandVideo.Prepare();
                var readerID = commandVideo.ExecuteReader();
                readerID.Read();
                var res = readerID[readerID.GetName(0)];
                
                if (res.ToString().Equals("0"))
                {
                    command2.CommandText = "INSERT INTO " + tableName + "(id, ";
                }
                else
                {
                    command2.CommandText = "UPDATE " + tableName + " SET ";
                }

                string[] days = new string[7];
                for (int j = 0; j < days.Length; j++)
                {
                    days[j] = reader["scoreDay" + (j + 1)].ToString();
                }

                result[0] = CalcAvg(days);
                result[2] = "'" + CalcFormula(days) + "'";
                result[1] = CalcSlope(days);

                string endInsert = "";
                for (int i = 0; i < columnName.Length; i++)
                {
                    if (res.ToString().Equals("0"))
                    {
                        command2.CommandText += columnName[i] + ", ";
                        endInsert += result[i] + ", ";
                    }
                    else
                    {
                        command2.CommandText += columnName[i] + "=" + result[i] + ", ";
                    }
                }
                command2.CommandText = command2.CommandText.Substring(0, command2.CommandText.Length - 2);
                if (endInsert.Length > 0)
                    endInsert = endInsert.Substring(0, endInsert.Length - 2);

                if (res.ToString().Equals("0"))
                {
                    command2.CommandText += ") VALUES(" + id + ", " + endInsert + ")";
                }
                else
                {
                    command2.CommandText += " WHERE id=" + id + " AND metric='empty'";
                }
                //checke = command2.CommandText;
                command2.Prepare();
                command2.ExecuteNonQuery();
            }
            reader.Close();
            command2.Dispose();
            commandVideo.Dispose();
        }
        private static string CalcAvg(string[] days)
        {
            float ans = 0;
            for(int i=0; i<days.Length; i++)
            {
                ans += float.Parse(days[i]);
            }
            return (ans/days.Length) + "";
        }
        private static string CalcSlope(string[] days)
        {
            return Point.getBeta() + "";
        }
        private static string CalcFormula(string[] days)
        {
            Point.pointList = new List<Point>();
            for (int i = 0; i < days.Length; i++)
            {
                Point a = new Point(i+1, double.Parse(days[i]));
            }
            return Point.getFormula();
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
            //RunMetric(metric, false);
        }
    }
}
