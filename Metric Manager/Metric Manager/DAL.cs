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
        public readonly static int SAVED_DAYS = 7;
        //private const String DBname = @"DataBase.db";
        public static bool testMood = false;
        public static int LastIndexTable;
        private static String Connection_String = @"Data Source=";
        public static string SocialMedia = "youtube";
        public static string realPathDB;
        private static List<string> intFileld = new List<string>();
        private static List<string> notIntField = new List<string>();
        private static bool isOverDay7;
        static public string checke;
        public static string IDName = "video_id";
        public static string ViewName = "view_count";

        public static SQLiteConnection connection = null;

        public static void SetUpDB(string path)
        {
            // + "\\DataBase.db";
            string[] arr = path.Split("\\");
            SocialMedia = arr[arr.Length - 1];
            if (SocialMedia.Equals("youtube"))
            {
                IDName = "video_id";
                ViewName = "view_count";
            }
            if (SocialMedia.Equals("tiktok"))
            {
                IDName = "id";
                ViewName = "playCount";
            }
                
            realPathDB = path+"\\DataBase.db";
            Connection_String = @"Data Source=" + realPathDB;
        }
        public static void OpenConnect()
        {
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
        //private static int CreateDateTable(List<string> Fields)
        //{
        //    try
        //    {
        //        string names = "";
        //        foreach (string name in Fields)
        //        {
        //            if (name.Length > 0)
        //            {
        //                if (intFileld.Contains(name))
        //                    names += name + " INTEGER, ";
        //                else
        //                    names += name + " TEXT, ";
        //            }
        //        }
        //        names += "PullDate TEXT";
        //        LastIndexTable = GetLastIndexTable(true);
        //        int index = LastIndexTable + 1;

        //        if (index <= SAVED_DAYS)
        //        {
        //            SQLiteCommand command = new SQLiteCommand("CREATE TABLE VideosInfoDay" + index + " (" + names + ")", connection);
        //            command.ExecuteNonQuery();
        //            command.Dispose();
        //            SystemManager.SaveToText((int)index);
        //            return index;
        //        }
        //        isOverDay7 = true;
        //        FixTableNames();
        //        SystemManager.SaveToText(SAVED_DAYS);
        //        return SAVED_DAYS;
        //    }
        //    catch (SQLiteException e)
        //    {
        //        DAL.CloseConnect();
        //        return -1;
        //    }
        //}
        //private static void InitTables(bool isDBOpen)
        //{
        //    try
        //    {
        //        if (!isDBOpen)
        //            OpenConnect();
        //        SQLiteCommand command = new SQLiteCommand("CREATE TABLE FilterHypeScore (" + IDName + " TEXT, metric TEXT DEFAULT 'empty', averageScore REAL DEFAULT 0.0, slope REAL DEFAULT 0.0, formula TEXT, scoreDay1 REAL DEFAULT 0.0, scoreDay2 REAL DEFAULT 0.0, scoreDay3 REAL DEFAULT 0.0, scoreDay4 REAL DEFAULT 0.0, scoreDay5 REAL DEFAULT 0.0, scoreDay6 REAL DEFAULT 0.0, scoreDay7 REAL DEFAULT 0.0, PRIMARY KEY('" + IDName + "','metric'), FOREIGN KEY (" + IDName + ") REFERENCES ID(" + IDName + "))", connection);
        //        command.ExecuteNonQuery();
        //        command = new SQLiteCommand("CREATE TABLE ModelHypeScore ("+ IDName + " TEXT, model1score REAL, PRIMARY KEY('" + IDName + "'))", connection);
        //        command.ExecuteNonQuery();
        //        command = new SQLiteCommand("CREATE TABLE PlayCountPerDay (" + IDName + " TEXT, playCountDay1 INTEGER DEFAULT 0, playCountDay2 INTEGER DEFAULT 0, playCountDay3 INTEGER DEFAULT 0, playCountDay4 INTEGER DEFAULT 0, playCountDay5 INTEGER DEFAULT 0, playCountDay6 INTEGER DEFAULT 0, playCountDay7 INTEGER DEFAULT 0,playCountAllWeek INTEGER DEFAULT 0, PRIMARY KEY('" + IDName + "'))", connection);
        //        command.ExecuteNonQuery();
        //        command = new SQLiteCommand("CREATE TABLE Hashtags (name TEXT, averageScore REAL DEFAULT 0.0, slope REAL DEFAULT 0.0, formula TEXT, scoreDay1 REAL DEFAULT 0.0, scoreDay2 REAL DEFAULT 0.0, scoreDay3 REAL DEFAULT 0.0, scoreDay4 REAL DEFAULT 0.0, scoreDay5 REAL DEFAULT 0.0, scoreDay6 REAL DEFAULT 0.0, scoreDay7 REAL DEFAULT 0.0, PRIMARY KEY('name'))", connection);
        //        command.ExecuteNonQuery();
        //        command = new SQLiteCommand("CREATE TABLE ID (" + IDName + " TEXT, counter INTEGER DEFAULT 0, PRIMARY KEY('" + IDName + "'))", connection);
        //        command.ExecuteNonQuery();

        //        command.Dispose();
        //        Console.WriteLine("Initialization tables.");
        //        if (!isDBOpen)
        //            CloseConnect();
        //    }
        //    catch (SQLiteException e)
        //    {
        //        DAL.CloseConnect();
        //    }
        //}
        //private static void FixTableNames()
        //{
        //    // save VideosInfoDay1 in archion
        //    SQLiteCommand command = new SQLiteCommand("SELECT ID.video_id, ID.counter FROM VideosInfoDay1 JOIN ID ON VideosInfoDay1.video_id == ID.video_id", connection);
        //    command.Prepare();
        //    var reader = command.ExecuteReader();
        //    while (reader.Read())
        //    {
        //        string id = reader["video_id"].ToString();
        //        string counter = reader["counter"].ToString();
        //        if (counter.Equals("0"))
        //            command = new SQLiteCommand("DELETE FROM ID WHERE video_id == '" + id + "'", connection);
        //        else
        //            command = new SQLiteCommand("UPDATE ID SET counter=counter-1 WHERE video_id=='" + id + "'", connection);
        //        command.Prepare();
        //        command.ExecuteNonQuery();
        //    }

        //    command.Prepare();
        //    command.ExecuteNonQuery();

        //    command = new SQLiteCommand("DELETE FROM VideosInfoDay1", connection);
        //    command.ExecuteNonQuery();
        //    command.Dispose();
        //    SQLiteCommand command2 = new SQLiteCommand("ALTER TABLE VideosInfoDay1 RENAME TO VideosInfoDay8", connection);
        //    command2.ExecuteNonQuery();
        //    command2.Dispose();

        //    for (int i = 2; i <= SAVED_DAYS + 1; i++)
        //    {
        //        //command = new SQLiteCommand("", connection);
        //        SQLiteCommand command1 = new SQLiteCommand("ALTER TABLE VideosInfoDay" + i + " RENAME TO VideosInfoDay" + (i - 1), connection);
        //        command1.ExecuteNonQuery();
        //        command1.Dispose();
        //    }
        //}
        private static void InsertAllData(Dictionary<DateTime, List<VideoInfo>> Data, bool isDBOpen)
        {
            try
            {
                if (!isDBOpen)
                    OpenConnect();
                SQLiteCommand command = new SQLiteCommand(null, DAL.connection);
                SQLiteCommand commandVideo = new SQLiteCommand(null, DAL.connection);
                foreach (var date in Data.Keys)
                {
                    Console.WriteLine("upload information from day  " + date);
                    var Current = Data[date];
                    foreach (var element in Current)
                    {
                        VideoDAL.saveVideo(element, date.ToString(), true); // ---- date
                        commandVideo = new SQLiteCommand("SELECT EXISTS(SELECT 1 FROM ID WHERE " + IDName + "='" + element.GetID() + "') as flag", DAL.connection);
                        commandVideo.Prepare();
                        var readerID = commandVideo.ExecuteReader();
                        readerID.Read();
                        var res = readerID[readerID.GetName(0)];

                        if (res.ToString().Equals("0"))
                        {
                            command.CommandText = "INSERT INTO ID (" + IDName + ") VALUES('" + element.GetID() + "')";
                        }
                        else
                        {
                            command.CommandText = "UPDATE ID SET counter=counter+1 WHERE " + IDName + "=" + element.GetID();
                        }
                        command.Prepare();
                        command.ExecuteNonQuery();
                        readerID.Close();
                    }
                }
                command.Dispose();
                commandVideo.Dispose();
                if (!isDBOpen)
                    CloseConnect();
            }
            catch (SQLiteException e)
            {
                DAL.CloseConnect();
            }
        }
        //private static void InsertDataFromToday(List<VideoInfo> Data, int tableIndex, bool isDBOpen)
        //{
        //    try
        //    {
        //        if (!isDBOpen)
        //            OpenConnect();
        //        var date = DateTime.Now.ToString("dd-MM-yyyy");
        //        Console.WriteLine("upload information from day  " + date);
        //        SQLiteCommand command = new SQLiteCommand(null, DAL.connection);
        //        foreach (var element in Data)
        //        {
        //            element.setSerialDate(tableIndex);
        //            VideoDAL.saveVideo(element, date, true);

        //            command.CommandText = "INSERT OR REPLACE INTO ID (id) VALUES ('" + element.GetID() + "')";
        //            command.Prepare();
        //            command.ExecuteNonQuery();
        //        }
        //        command.Dispose();
        //        if (!isDBOpen)
        //            CloseConnect();
        //    }
        //    catch (SQLiteException e)
        //    {
        //        DAL.CloseConnect();
        //    }
        //}
        public static void RunMetricLastDay(string metric, bool isDBOpen)
        {
            try
            {
                if (!isDBOpen)
                    OpenConnect();
                int i = LastIndexTable;

                if (isOverDay7)
                {
                    moveColumn("FilterHypeScore", "scoreDay");
                    i = SAVED_DAYS;
                }

                SQLiteCommand command = new SQLiteCommand("SELECT " + IDName + ", (" + metric + ") as scoreDay" + i + " FROM VideosInfoDay" + i + " ORDER BY scoreDay" + i + " DESC", connection);
                command.Prepare();
                var reader = command.ExecuteReader();
                SaveResultToDB(reader, "FilterHypeScore", "scoreDay" + i);
                command.Dispose();

                command = new SQLiteCommand("SELECT " + IDName + ", scoreDay1, scoreDay2, scoreDay3, scoreDay4, scoreDay5, scoreDay6, scoreDay7 FROM FilterHypeScore WHERE metric='empty'", connection);
                command.Prepare();
                var reader2 = command.ExecuteReader();
                SaveResultToDB(reader2, "FilterHypeScore", new string[4] { "averageScore", "slope", "formula", "metric" }, new string[4] { "averageScore", "slope", "formula", "'" + metric + "'" }, IDName);
                command.Dispose();

                if (!isDBOpen)
                    CloseConnect();
            }
            catch (SQLiteException e)
            {
                DAL.CloseConnect();
            }

        }
        public static void RunMetricAllWeek(string metric, bool isDBOpen)
        {
            try
            {
                if (!isDBOpen)
                    OpenConnect();
                LastIndexTable = 8;
                SQLiteCommand command = new SQLiteCommand(null, DAL.connection);
                for (int i = 1; i < LastIndexTable; i++)
                {
                    command = new SQLiteCommand("SELECT " + IDName + ", (" + metric + ") as scoreDay" + i + " FROM VideosInfoDay" + i + " ORDER BY scoreDay" + i + " DESC", connection);
                    command.Prepare();
                    checke = command.CommandText;
                    var reader = command.ExecuteReader();
                    SaveResultToDB(reader, "FilterHypeScore", "scoreDay" + i);
                    command.Dispose();
                }
                //, scoreDay4, scoreDay5, scoreDay6, scoreDay7
                command = new SQLiteCommand("SELECT " + IDName + ", scoreDay1, scoreDay2, scoreDay3, scoreDay4, scoreDay5, scoreDay6, scoreDay7 FROM FilterHypeScore WHERE metric='empty'", connection);
                command.Prepare();
                checke = command.CommandText;
                var reader2 = command.ExecuteReader();
                SaveResultToDB(reader2, "FilterHypeScore", new string[4] { "averageScore", "slope", "formula", "metric" }, new string[4] { "averageScore", "slope", "formula", "'" + metric + "'" }, IDName);
                command.Dispose();

                if (!isDBOpen)
                    CloseConnect();
            }
            catch (SQLiteException e)
            {
                DAL.CloseConnect();
            }

        }
        public static void RunSpecialAllWeek(string metric, bool isDBOpen)
        {
            try
            {
                if (!isDBOpen)
                    OpenConnect();

                LastIndexTable = 8;
                switch (metric)
                {
                    case "hashtags":
                        if (SocialMedia.Equals("youtube"))
                            RunHashtag("tags");
                        if (SocialMedia.Equals("tiktok"))
                            RunHashtag("hashtags");
                        break;
                }

                if (!isDBOpen)
                    CloseConnect();
            }
            catch (SQLiteException e)
            {
                DAL.CloseConnect();
            }

        }
        private static void RunHashtag(string metric)
        {
            SQLiteCommand command = new SQLiteCommand(null, DAL.connection);
            for (int i = 1; i < LastIndexTable; i++)
            {
                command = new SQLiteCommand("SELECT " + metric + " as scoreDay" + i + " FROM VideosInfoDay" + i, connection);
                command.Prepare();
                checke = command.CommandText;
                var reader = command.ExecuteReader();
                SaveHashtagsToDB(reader, "Hashtags", "scoreDay" + i);
                //command.Dispose();
            }
            command = new SQLiteCommand("SELECT name, scoreDay1, scoreDay2, scoreDay3, scoreDay4, scoreDay5, scoreDay6, scoreDay7 FROM Hashtags", connection);
            command.Prepare();
            checke = command.CommandText;
            var reader2 = command.ExecuteReader();
            SaveHashtagsToDB(reader2, "Hashtags", new string[3] { "averageScore", "slope", "formula" }, new string[3] { "averageScore", "slope", "formula" }, "name");
            command.Dispose();
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
        public static void CalcPlayCountPerDay()
        {
            try
            {
                SQLiteCommand command = new SQLiteCommand(null, DAL.connection);
                LastIndexTable = GetLastIndexTable();
                int i = LastIndexTable - 1;
                //isOverDay7 = true;

                if (isOverDay7)
                {
                    moveColumn("PlayCountPerDay", "playCountDay");
                    i = SAVED_DAYS - 1;
                }
                if (LastIndexTable >= 2)
                {
                    command = new SQLiteCommand("SELECT a." + IDName + ", (b.PlayCount - a.PlayCount) as playCountDay" + (i + 1) + " From VideosInfoDay" + i + " a JOIN VideosInfoDay" + (i + 1) + " b ON a." + IDName + " == b." + IDName + "", connection);
                    command.Prepare();
                    checke = command.CommandText;
                    var reader = command.ExecuteReader();
                    SaveResultToDB(reader, "PlayCountPerDay", "playCountDay" + (i + 1));
                    command.Dispose();

                    command = new SQLiteCommand("SELECT " + IDName + ", SUM(playCountDay1 + playCountDay2 + playCountDay3 + playCountDay4 + playCountDay5 + playCountDay6 + playCountDay7) as playCountAllWeek FROM PlayCountPerDay GROUP BY " + IDName + "", connection);
                    command.Prepare();
                    checke = command.CommandText;
                    var reader5 = command.ExecuteReader();
                    SaveResultToDB(reader5, "PlayCountPerDay", "playCountAllWeek");
                    command.Dispose();
                }
            }
            catch (SQLiteException e)
            {
                DAL.CloseConnect();
            }
        }
        public static void CalcPlayCountAllWeek(bool isDBOpen)
        {
            try
            {
                if (!isDBOpen)
                    DAL.OpenConnect();

                SQLiteCommand command = new SQLiteCommand(null, DAL.connection);
                LastIndexTable = 7;
                for (int i = 1; i < LastIndexTable; i++)
                {
                    command = new SQLiteCommand("SELECT a."+ IDName + ", (b."+ViewName+ " - a." + ViewName + ") as playCountDay" + (i + 1) + " From VideosInfoDay" + i + " a JOIN VideosInfoDay" + (i + 1) + " b ON a." + IDName + " == b." + IDName + "", DAL.connection);
                    checke = command.CommandText;
                    command.Prepare();
                    var reader = command.ExecuteReader();
                    SaveResultToDB(reader, "PlayCountPerDay", "playCountDay" + (i + 1));
                    command.Dispose();
                }

                command = new SQLiteCommand("SELECT " + IDName + ", SUM(playCountDay1 + playCountDay2 + playCountDay3 + playCountDay4 + playCountDay5 + playCountDay6 + playCountDay7) as playCountAllWeek FROM PlayCountPerDay GROUP BY " + IDName + "", connection);
                command.Prepare();
                var reader5 = command.ExecuteReader();
                SaveResultToDB(reader5, "PlayCountPerDay", "playCountAllWeek");
                command.Dispose();

                if (!isDBOpen)
                    DAL.CloseConnect();
            }
            catch (SQLiteException e)
            {
                DAL.CloseConnect();
            }
            Console.WriteLine("finish calc view count.");
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
                        commandVideo.CommandText = "SELECT " + IDName + " FROM " + tableName;
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
        private static void moveColumn(string tableName, string columnName)
        {
            SQLiteCommand command = new SQLiteCommand("ALTER TABLE " + tableName + " DROP COLUMN " + columnName + "1", connection);
            checke = command.CommandText;
            command.ExecuteNonQuery();
            command.Dispose();

            for (int j = 2; j <= SAVED_DAYS; j++)
            {
                //command = new SQLiteCommand("", connection);
                command = new SQLiteCommand("ALTER TABLE " + tableName + " RENAME COLUMN " + columnName + j + " TO " + columnName + (j - 1), connection);
                checke = command.CommandText;
                command.ExecuteNonQuery();
                command.Dispose();
            }
            command = new SQLiteCommand("ALTER TABLE " + tableName + " ADD COLUMN " + columnName + "7 INTEGER DEFAULT 0", connection);
            checke = command.CommandText;
            command.ExecuteNonQuery();
            command.Dispose();
        }
        private static void SaveResultToDB(SQLiteDataReader reader, string tableName, string columnName)
        {
            SQLiteCommand command2 = new SQLiteCommand(null, connection);
            SQLiteCommand commandVideo = new SQLiteCommand(null, connection);
            try
            {
                while (reader.Read())
                {
                    string id = reader[IDName].ToString();
                    var playcount = reader[columnName].ToString(); // bug here
                    if (playcount.Length > 0)
                    {
                        if (tableName.Equals("PlayCountPerDay"))
                            commandVideo = new SQLiteCommand("SELECT EXISTS(SELECT 1 FROM " + tableName + " WHERE " + IDName + "='" + id + "') as flag", DAL.connection);
                        else
                            commandVideo = new SQLiteCommand("SELECT EXISTS(SELECT 1 FROM " + tableName + " WHERE " + IDName + "='" + id + "' AND metric='empty') as flag", DAL.connection);
                        commandVideo.Prepare();
                        checke = commandVideo.CommandText;
                        var readerID = commandVideo.ExecuteReader();
                        readerID.Read();
                        var res = readerID[readerID.GetName(0)];

                        if (res.ToString().Equals("0"))
                        {
                            command2.CommandText = "INSERT INTO " + tableName + "(" + IDName + ", " + columnName + ") VALUES('" + id + "', '" + playcount + "')";
                        }
                        else
                        {
                            if (tableName.Equals("PlayCountPerDay"))
                                command2.CommandText = "UPDATE " + tableName + " SET " + columnName + "='" + playcount + "' WHERE " + IDName + "='" + id + "'";
                            else
                                command2.CommandText = "UPDATE " + tableName + " SET " + columnName + "='" + playcount + "' WHERE " + IDName + "='" + id + "' AND metric='empty'";
                        }
                        checke = command2.CommandText;
                        command2.Prepare();
                        command2.ExecuteNonQuery();
                    }
                }
                reader.Close();
                command2.Dispose();
                commandVideo.Dispose();
            }
            catch (SQLiteException e)
            {
                DAL.CloseConnect();
            }

        }
        private static void SaveHashtagsToDB(SQLiteDataReader reader, string tableName, string columnName)
        {
            SQLiteCommand command2 = new SQLiteCommand(null, connection);
            SQLiteCommand commandVideo = new SQLiteCommand(null, connection);
            string n = columnName;
            int u = 0;
            try
            {
                while (reader.Read())
                {
                    var tags = reader[columnName].ToString();
                    if (!tags.Equals("[none]") && !tags.Equals("0") && !tags.Equals("[]"))
                    {
                        string[] arrTags = null;
                        if (SocialMedia.Equals("youtube"))
                            arrTags = tags.Split('|');
                        else if (SocialMedia.Equals("tiktok"))
                        {
                            tags = tags.Substring(1, tags.Length - 2);
                            arrTags = tags.Split("; ");
                        }
                        foreach (string tag in arrTags)
                        {
                            string tag1 = tag.ToLower();
                            commandVideo = new SQLiteCommand("SELECT EXISTS(SELECT 1 FROM " + tableName + " WHERE name='" + tag1 + "') as flag", DAL.connection);
                            commandVideo.Prepare();
                            checke = commandVideo.CommandText;
                            var readerID = commandVideo.ExecuteReader();
                            readerID.Read();
                            var res = readerID[readerID.GetName(0)];

                            if (res.ToString().Equals("0"))
                            {
                                command2.CommandText = "INSERT INTO " + tableName + "(name, " + columnName + ") VALUES('" + tag1 + "', 1)";
                            }
                            else
                            {
                                command2.CommandText = "UPDATE " + tableName + " SET " + columnName + "=" + columnName + "+1 WHERE name='" + tag1 + "'";

                            }
                            checke = command2.CommandText;
                            command2.Prepare();
                            command2.ExecuteNonQuery();
                        }
                    }
                }
                reader.Close();
                command2.Dispose();
                commandVideo.Dispose();
            }
            catch (SQLiteException e)
            {
                DAL.CloseConnect();
            }

        }
        private static void SaveResultToDB(SQLiteDataReader reader, string tableName, string[] columnName, string[] result, string idName)
        {
            SQLiteCommand command2 = new SQLiteCommand(null, connection);
            SQLiteCommand commandVideo = new SQLiteCommand(null, connection);

            while (reader.Read())
            {
                string id = reader[idName].ToString();

                if (tableName.Equals("PlayCountPerDay") || tableName.Equals("Hashtags"))
                    commandVideo = new SQLiteCommand("SELECT EXISTS(SELECT 1 FROM " + tableName + " WHERE " + idName + "='" + id + "') as flag", DAL.connection);
                else
                    commandVideo = new SQLiteCommand("SELECT EXISTS(SELECT 1 FROM " + tableName + " WHERE " + idName + "='" + id + "' AND metric='empty') as flag", DAL.connection);
                commandVideo.Prepare();
                checke = commandVideo.CommandText;
                var readerID = commandVideo.ExecuteReader();
                readerID.Read();
                var res = readerID[readerID.GetName(0)];
                readerID.Close();

                if (res.ToString().Equals("0"))
                {
                    command2.CommandText = "INSERT INTO " + tableName + "(" + idName + ", ";
                }
                else
                {
                    command2.CommandText = "UPDATE " + tableName + " SET ";
                }

                string[] days = new string[7];//savedays
                for (int j = 0; j < days.Length; j++)
                {
                    days[j] = reader["scoreDay" + (j + 1)].ToString();
                }
                try
                {
                    result[0] = CalcAvg(days);
                    result[2] = "'" + CalcFormula(days) + "'";
                    result[1] = CalcSlope(days);
                }
                catch (SQLiteException e)
                {
                    Console.WriteLine("?");
                }


                string endInsert = "";
                for (int i = 0; i < columnName.Length; i++)
                {
                    if (res.ToString().Equals("0"))
                    {
                        command2.CommandText += columnName[i] + ", ";
                        endInsert += "" + result[i] + ", ";
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
                    command2.CommandText += ") VALUES('" + id + "', '" + endInsert + "')";
                }
                else
                {
                    command2.CommandText += " WHERE " + idName + "='" + id + "'";
                    if (!tableName.Equals("Hashtags"))
                        command2.CommandText += " AND metric='empty'";
                }
                //checke = command2.CommandText;
                command2.Prepare();
                command2.ExecuteNonQuery();
            }
            reader.Close();
            command2.Dispose();
            commandVideo.Dispose();
        }
        private static void SaveHashtagsToDB(SQLiteDataReader reader, string tableName, string[] columnName, string[] result, string idName)
        {
            SQLiteCommand command2 = new SQLiteCommand(null, connection);
            SQLiteCommand commandVideo = new SQLiteCommand(null, connection);

            while (reader.Read())
            {
                string id = reader[idName].ToString();
                commandVideo = new SQLiteCommand("SELECT EXISTS(SELECT 1 FROM " + tableName + " WHERE " + idName + "='" + id + "') as flag", DAL.connection);
                commandVideo.Prepare();
                checke = commandVideo.CommandText;
                var readerID = commandVideo.ExecuteReader();
                readerID.Read();
                var res = readerID[readerID.GetName(0)];

                if (res.ToString().Equals("0"))
                {
                    command2.CommandText = "INSERT INTO " + tableName + "(" + idName + ", ";
                }
                else
                {
                    command2.CommandText = "UPDATE " + tableName + " SET ";
                }

                string[] days = new string[7];//savedays
                for (int j = 0; j < days.Length; j++)
                {
                    days[j] = reader["scoreDay" + (j + 1)].ToString();
                }
                try
                {
                    result[0] = CalcAvg(days);
                    result[2] = "'" + CalcFormula(days) + "'";
                    result[1] = CalcSlope(days);
                }
                catch (SQLiteException e)
                {
                    Console.WriteLine("?");
                }


                string endInsert = "";
                for (int i = 0; i < columnName.Length; i++)
                {
                    if (res.ToString().Equals("0"))
                    {
                        command2.CommandText += columnName[i] + ", ";
                        endInsert += "" + result[i] + ", ";
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
                    command2.CommandText += ") VALUES('" + id + "', '" + endInsert + "')";
                }
                else
                {
                    command2.CommandText += " WHERE " + idName + "='" + id + "'";
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
            for (int i = 0; i < days.Length; i++)
            {
                ans += float.Parse(days[i]);
            }
            return (ans / days.Length) + "";
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
                Point a = new Point(i + 1, double.Parse(days[i]));
            }
            return Point.getFormula();
        }

        // public functions, doesn't assume that the DB is open
        
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
        private static void CalcPlayCountAllWeek()
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
        public static String DB()
        {
            if (testMood)
            {
                Connection_String = @"Data Source=..\..\..\..\..\Metric Manager\Metric Manager\bin\Debug\net5.0\DataBaseTest.db";
                return @"..\..\..\..\..\Metric Manager\Metric Manager\bin\Debug\net5.0\DataBaseTest.db";
            }
            Connection_String = @"Data Source=" + realPathDB;
            return DAL.Connection_String;
        }
        public static void ResetDB()
        {
            testMood = true;
            DB();
            OpenConnect();
            SQLiteCommand command = new SQLiteCommand("DROP TABLE VideosInfoDay1", DAL.connection);
            command.Prepare();
            command.ExecuteNonQuery();
            command = new SQLiteCommand("DROP TABLE VideosInfoDay2", DAL.connection);
            command.Prepare();
            command.ExecuteNonQuery();
            command = new SQLiteCommand("DROP TABLE VideosInfoDay3", DAL.connection);
            command.Prepare();
            command.ExecuteNonQuery();
            command = new SQLiteCommand("DELETE FROM ID", DAL.connection);
            command.Prepare();
            command.ExecuteNonQuery();
            command.Dispose();
            CloseConnect();

        }
        public static void ResetDBToAnalysis()
        {
            SQLiteCommand command = new SQLiteCommand("DELETE FROM PlayCountPerDay", DAL.connection);
            command.Prepare();
            command.ExecuteNonQuery();
            command = new SQLiteCommand("DELETE FROM FilterHypeScore", DAL.connection);
            command.Prepare();
            command.ExecuteNonQuery();
            command = new SQLiteCommand("DELETE FROM Hashtags", DAL.connection);
            command.Prepare();
            command.ExecuteNonQuery();
            command.Dispose();

        }
    }
}

