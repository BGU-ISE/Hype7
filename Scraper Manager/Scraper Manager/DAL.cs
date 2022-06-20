using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SQLite;
using System.Data;
using System.Text.RegularExpressions;

namespace Scraper_Manager
{
    public static class DAL
    {
        public readonly static int SAVED_DAYS = 7;
        //private const String DBname = @"..\..\..\..\..\Metric Manager\Metric Manager\bin\Debug\net5.0\DataBase.db";
        public static bool testMood = false;
        private static String Connection_String = @"Data Source=";
        public static string realPathDB;

        private static bool isOverDay7;
        public static string IDName = "video_id";
        public static int LastIndexTable;
        public static List<string> intFileld = new List<string>();
        public static List<string> notIntField = new List<string>();
        
        static public string checke;


        public static SQLiteConnection connection = null;

        public static void SetUpDB(string[] args)
        {
            if (args.Length > 0)
            {
                string path = "..\\..\\..\\..\\..\\";
                for (int i = 0; i < args.Length; i++)
                {
                    path += args[i] + " ";
                }
                path = path.Substring(0, path.Length - 1);

                //_____________________________________chnaing args________________
                path = args[0]+"\\DataBase.db";
                Console.WriteLine("the ddb is " + path);
                //________________________________________________________

                realPathDB = path;
                Connection_String = @"Data Source=" + path;
            }
        }
        public static void OpenConnect()
        {
            if (!System.IO.File.Exists(DB()))
            {
                SQLiteConnection.CreateFile(DB());
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
        public static String DB()
        {
            if (testMood)
            {
                Connection_String = @"Data Source=..\..\..\..\..\Metric Manager\Metric Manager\bin\Debug\net5.0\DataBaseTestScraper.db";
                return @"..\..\..\..\..\Metric Manager\Metric Manager\bin\Debug\net5.0\DataBaseTestScraper.db";
            }
            Connection_String = @"Data Source=" + realPathDB;
            return DAL.realPathDB;
        }
        public static void CloseConnect()
        {
            if (connection.State != ConnectionState.Closed)
                connection.Close();
        }
        public static void InitTables(bool isDBOpen)
        {
            try
            {
                if (!isDBOpen)
                    OpenConnect();
                SQLiteCommand command = new SQLiteCommand("CREATE TABLE FilterHypeScore ("+ IDName + " TEXT, metric TEXT DEFAULT 'empty', averageScore REAL DEFAULT 0.0, slope REAL DEFAULT 0.0, formula TEXT, scoreDay1 REAL DEFAULT 0.0, scoreDay2 REAL DEFAULT 0.0, scoreDay3 REAL DEFAULT 0.0, scoreDay4 REAL DEFAULT 0.0, scoreDay5 REAL DEFAULT 0.0, scoreDay6 REAL DEFAULT 0.0, scoreDay7 REAL DEFAULT 0.0, PRIMARY KEY('" + IDName + "','metric'), FOREIGN KEY (" + IDName + ") REFERENCES ID(" + IDName + "))", connection);
                command.ExecuteNonQuery();
                command = new SQLiteCommand("CREATE TABLE ModelHypeScore (" + IDName + " TEXT, model1score REAL, PRIMARY KEY('" + IDName + "'))", connection);
                command.ExecuteNonQuery();
                command = new SQLiteCommand("CREATE TABLE PlayCountPerDay (" + IDName + " TEXT, playCountDay1 INTEGER DEFAULT 0, playCountDay2 INTEGER DEFAULT 0, playCountDay3 INTEGER DEFAULT 0, playCountDay4 INTEGER DEFAULT 0, playCountDay5 INTEGER DEFAULT 0, playCountDay6 INTEGER DEFAULT 0, playCountDay7 INTEGER DEFAULT 0,playCountAllWeek INTEGER DEFAULT 0, PRIMARY KEY('" + IDName + "'))", connection);
                command.ExecuteNonQuery();
                command = new SQLiteCommand("CREATE TABLE Hashtags (name TEXT, averageScore REAL DEFAULT 0.0, slope REAL DEFAULT 0.0, formula TEXT, scoreDay1 REAL DEFAULT 0.0, scoreDay2 REAL DEFAULT 0.0, scoreDay3 REAL DEFAULT 0.0, scoreDay4 REAL DEFAULT 0.0, scoreDay5 REAL DEFAULT 0.0, scoreDay6 REAL DEFAULT 0.0, scoreDay7 REAL DEFAULT 0.0, PRIMARY KEY('name'))", connection);
                command.ExecuteNonQuery();
                command = new SQLiteCommand("CREATE TABLE ID (" + IDName + " TEXT, counter INTEGER DEFAULT 0, PRIMARY KEY('" + IDName + "'))", connection);
                command.ExecuteNonQuery();

                command.Dispose();
                Console.WriteLine("Initialization tables.");
                if (!isDBOpen)
                    CloseConnect();
            }
            catch (SQLiteException e)
            {
                DAL.CloseConnect();
            }
        }
        public static int CreateDateTable(List<string> Fields, bool isDBOpen)
        {
            try
            {
                if (!isDBOpen)
                    OpenConnect();

                string names = "";
                foreach (string name in Fields)
                {
                    if (name.Length > 0)
                    {
                        if (intFileld.Contains(name))
                            names += name + " INTEGER, ";
                        else
                            names += name + " TEXT, ";
                    }
                }
                names += "PullDate TEXT";
                LastIndexTable = ScraperManager.LastIndexTable;

                if (LastIndexTable <= SAVED_DAYS)
                {
                    SQLiteCommand command = new SQLiteCommand("CREATE TABLE VideosInfoDay" + LastIndexTable + " (" + names + ")", connection);
                    command.ExecuteNonQuery();
                    command.Dispose();
                    //SystemManager.SaveToText((int)index);
                    if (!isDBOpen)
                        CloseConnect();
                    return LastIndexTable;
                }
                isOverDay7 = true;
                FixTableNames(true);
                ScraperManager.LastIndexTable = 7;

                if (!isDBOpen)
                    CloseConnect();

                return SAVED_DAYS;
            }
            catch (SQLiteException e)
            {
                DAL.CloseConnect();
                return -1;
            }
        }
        public static void FixTableNames(bool isDBOpen)
        {
            if (!isDBOpen)
                OpenConnect();

            // save VideosInfoDay1 in archion
            SQLiteCommand command = new SQLiteCommand("SELECT ID." + IDName + ", ID.counter FROM VideosInfoDay1 JOIN ID ON VideosInfoDay1." + IDName + " == ID." + IDName + "", connection);
            command.Prepare();
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                string id = reader[IDName].ToString();
                string counter = reader["counter"].ToString();
                if (counter.Equals("0"))
                    command = new SQLiteCommand("DELETE FROM ID WHERE " + IDName + " == '" + id + "'", connection);
                else
                    command = new SQLiteCommand("UPDATE ID SET counter=counter-1 WHERE " + IDName + "=='" + id + "'", connection);
                command.Prepare();
                command.ExecuteNonQuery();
            }

            command.Prepare();
            command.ExecuteNonQuery();

            command = new SQLiteCommand("DELETE FROM VideosInfoDay1", connection);
            command.ExecuteNonQuery();
            command.Dispose();
            SQLiteCommand command2 = new SQLiteCommand("ALTER TABLE VideosInfoDay1 RENAME TO VideosInfoDay8", connection);
            command2.ExecuteNonQuery();
            command2.Dispose();

            for (int i = 2; i <= SAVED_DAYS + 1; i++)
            {
                //command = new SQLiteCommand("", connection);
                SQLiteCommand command1 = new SQLiteCommand("ALTER TABLE VideosInfoDay" + i + " RENAME TO VideosInfoDay" + (i - 1), connection);
                command1.ExecuteNonQuery();
                command1.Dispose();
            }

            if (!isDBOpen)
                CloseConnect();
        }
        public static int GetLastIndexTable(bool isDBOpen)
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

            return (int)maxIndex + 1;
        }

        public static void InitIntField(RecordsFile data)
        {
            if (intFileld.Count == 0)
            {
                SetNumericField(data);
                SetStringField();
            }
            int g = 0;

        }
        public static void SetNumericField(RecordsFile data)
        {
            bool firstTime = true;
            foreach (Record element in data.records)
            {
                List<int> numbers = new List<int>();
                if (firstTime)
                {
                    int i = 0;
                    foreach (string field in element.values)
                    {
                        string temp = field.Substring(1, field.Length - 2);
                        if (isNumeric(temp))
                            numbers.Add(i);
                        i++;
                    }

                    int j = 0;
                    foreach (string name in ScraperManager.fieldNames)
                    {
                        if (numbers.Contains(j))
                        {
                            DAL.AddIntField(name);
                        }
                        j++;
                    }

                    firstTime = false;
                }
            }
        }
        private static bool isNumeric(string value)
        {
            return Regex.IsMatch(value, "^[0-9\\s]+$");
        }
        public static void SetStringField()
        { // everything with id or date
            notIntField.Add(IDName);
            notIntField.Add("musicMeta_musicId");
            notIntField.Add("trending_date");

            foreach (string name in notIntField)
            {
                intFileld.Remove(name);
            }
        }
        public static void AddIntField(string value)
        {
            intFileld.Add(value);
        }

        public static void InsertDataFromToday(RecordsFile Data, int tableIndex, DateTime dateTrue, bool isDBOpen)
        {
            try
            {
                if (!isDBOpen)
                    OpenConnect();
                var date = DateTime.Now.ToString("dd-MM-yyyy");
                Console.WriteLine("upload information from day  " + dateTrue);
                SQLiteCommand command = new SQLiteCommand(null, DAL.connection);
                SQLiteCommand commandVideo = new SQLiteCommand(null, DAL.connection);
                foreach (var element in Data.records)
                {
                    //element.setSerialDate(tableIndex);
                    VideoDAL.saveVideo(element, dateTrue.ToString("dd-MM-yyyy"), tableIndex, true);
                    string temp = element.GetID();
                    //temp.Replace("\"", string.Empty);
                    //temp.Replace('"', '');
                    temp = temp.Substring(1, temp.Length - 2);
                    //Console.WriteLine(temp);
                    commandVideo = new SQLiteCommand("SELECT EXISTS(SELECT 1 FROM ID WHERE " + IDName + "='" + temp + "') as flag", DAL.connection);
                    commandVideo.Prepare();
                    var readerID = commandVideo.ExecuteReader();
                    readerID.Read();
                    var res = readerID[readerID.GetName(0)];


                    if (res.ToString().Equals("0"))
                    {
                        command.CommandText = "INSERT INTO ID (" + IDName + ") VALUES('" + temp + "')";
                    }
                    else
                    {
                        command.CommandText = "UPDATE ID SET counter=counter+1 WHERE " + IDName + "='" + temp + "'";
                    }
                    command.Prepare();
                    command.ExecuteNonQuery();
                    readerID.Close();
                }
                command.Dispose();
                if (!isDBOpen)
                    CloseConnect();
            }
            catch (SQLiteException e)
            {
                DAL.CloseConnect();
            }
        }
        public static void ResetDB()
        {
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

        }
    }
}
