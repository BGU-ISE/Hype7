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
        private const String DBname = @"DataBase.db";
        
        private static String Connection_String = @"Data Source=..\..\..\..\..\Metric Manager\Metric Manager\bin\Debug\net5.0\DataBase.db";
        private static bool isOverDay7;
        static public string checke;

        public static SQLiteConnection connection = null;

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
                    commandVideo = new SQLiteCommand("SELECT EXISTS(SELECT 1 FROM ID WHERE video_id='" + temp + "') as flag", DAL.connection);
                    commandVideo.Prepare();
                    var readerID = commandVideo.ExecuteReader();
                    readerID.Read();
                    var res = readerID[readerID.GetName(0)];


                    if (res.ToString().Equals("0"))
                    {
                        command.CommandText = "INSERT INTO ID (video_id) VALUES('" + temp + "')";
                    }
                    else
                    {
                        command.CommandText = "UPDATE ID SET counter=counter+1 WHERE video_id='" + temp + "'";
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
    }
}
