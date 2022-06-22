using Microsoft.VisualStudio.TestTools.UnitTesting;
using Scraper_Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data;
using System.IO;

namespace Scraper_Manager.Tests
{
    [TestClass()]
    public class DALTests
    {
        public static int countTest = 0;

        [TestInitialize()]
        public void InitTest()
        {
            if (countTest == 0)
            {
                using (var w = new StreamWriter("../../../input_folder/history.txt"))
                {
                    w.WriteLine("");
                    w.Flush();
                }

                DAL.testMood = true;
                DAL.DB();
                DAL.OpenConnect();

                int num = DAL.GetLastIndexTable(true);
                Assert.IsTrue(num == 1, "didn't read correctly the number of tables.");

                string[] arr = new string[1];
                arr[0] = "..\\..\\..\\..\\..\\Scraper Manager\\Scraper ManagerTests\\input_folder";
                ScraperManager.run(arr);

                DAL.CloseConnect();
            }

        }
        [TestCleanup()]
        public void CleanTest()
        {
            if (countTest == 6)
            {
                DAL.testMood = true;
                DAL.DB();
                DAL.OpenConnect();

                DAL.ResetDB();

                int num = DAL.GetLastIndexTable(true);
                Assert.IsTrue(num == 1, "didn't read correctly the number of tables.");

                SQLiteCommand command = new SQLiteCommand(null, DAL.connection);
                command.CommandText = "SELECT * FROM ID";
                command.Prepare();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Assert.Fail("there is somethig in ID");
                }
                reader.Close();
                command.Dispose();

                DAL.CloseConnect();
                DAL.testMood = false;
                DAL.DB();
            }
        }
        [TestMethod()]
        public void OpenConnectTest()
        {
            countTest++;
            DAL.testMood = true;
            DAL.DB();
            DAL.OpenConnect();

            String DBName = @"..\..\..\..\..\Metric Manager\Metric Manager\bin\Debug\net5.0\DataBaseTestScraper.db";
            Assert.IsTrue(System.IO.File.Exists(DBName), "DB not created.");
            SQLiteCommand command = new SQLiteCommand(null, DAL.connection);
            command.CommandText = "SELECT name FROM sqlite_master WHERE type = 'table'";
            command.Prepare();
            var reader = command.ExecuteReader();
            bool filter = false;
            bool model = false;
            bool play = false;
            bool tag = false;
            bool id = false;

            while (reader.Read())
            {
                string tableName = reader["name"].ToString();
                if (tableName.Contains("FilterHypeScore"))
                    filter = true;
                if (tableName.Contains("ModelHypeScore"))
                    model = true;
                if (tableName.Contains("PlayCountPerDay"))
                    play = true;
                if (tableName.Contains("Hashtags"))
                    tag = true;
                if (tableName.Contains("ID"))
                    id = true;
            }
            Assert.IsTrue(filter, "FilterHypeScore table doesn't created.");
            Assert.IsTrue(model, "ModelHypeScore table doesn't created.");
            Assert.IsTrue(play, "PlayCountPerDay table doesn't created.");
            Assert.IsTrue(tag, "Hashtags table doesn't created.");
            Assert.IsTrue(id, "ID table doesn't created.");

            DAL.CloseConnect();
            DAL.testMood = false;
            DAL.DB();
        }
        [TestMethod()]
        public void CloseConnectTest()
        {
            countTest++;
            DAL.testMood = true;
            DAL.DB();
            if (DAL.connection != null && DAL.connection.State == ConnectionState.Open)
                DAL.CloseConnect();

            //Assert.IsNull(DAL.connection, "connection is open");
            DAL.OpenConnect();
            Assert.IsNotNull(DAL.connection, "connection is null");
            Assert.IsTrue(DAL.connection.State == ConnectionState.Open, "connection is not open");
            DAL.CloseConnect();
            Assert.IsTrue(DAL.connection.State == ConnectionState.Closed, "connection is not close");

            DAL.testMood = false;
            DAL.DB();
        }

        [TestMethod()]
        public void CreateDateTableTest()
        {
            countTest++;
            DAL.testMood = true;
            DAL.DB();
            DAL.OpenConnect();

            SQLiteCommand command = new SQLiteCommand(null, DAL.connection);
            command.CommandText = "SELECT name FROM sqlite_master WHERE type = 'table'";
            command.Prepare();
            var reader = command.ExecuteReader();
            bool day1 = false;
            bool day2 = false;
            bool day3 = false;

            while (reader.Read())
            {
                string tableName = reader["name"].ToString();
                if (tableName.Equals("VideosInfoDay1"))
                    day1 = true;
                if (tableName.Equals("VideosInfoDay2"))
                    day2 = true;
                if (tableName.Equals("VideosInfoDay3"))
                    day3 = true;
            }
            Assert.IsTrue(day1, "VideosInfoDay1 table doesn't created.");
            Assert.IsTrue(day2, "VideosInfoDay2 table doesn't created.");
            Assert.IsTrue(day3, "VideosInfoDay3 table doesn't created.");

            DAL.CloseConnect();
            DAL.testMood = false;
            DAL.DB();
        }

        [TestMethod()]
        public void GetLastIndexTableTest()
        {
            countTest++;
            DAL.testMood = true;
            DAL.DB();
            DAL.OpenConnect();

            int num = DAL.GetLastIndexTable(true);
            Assert.IsTrue(num == 4, "didn't read correctly the number of tables.");

            DAL.CloseConnect();
            DAL.testMood = false;
            DAL.DB();
        }

        [TestMethod()]
        public void InitIntFieldTest()
        {
            countTest++;
            bool like = false;
            bool cat = false;
            bool view = false;
            bool com = false;
            foreach (var element in DAL.intFileld)
            {
                if (element.Equals("likes"))
                    like = true;
                if (element.Equals("category"))
                    cat = true;
                if (element.Equals("view_count"))
                    view = true;
                if (element.Equals("comment_count"))
                    com = true;
            }
            Assert.IsTrue(like, "like didn't count as an integer.");
            Assert.IsTrue(cat, "category didn't count as an integer.");
            Assert.IsTrue(view, "view_count didn't count as an integer.");
            Assert.IsTrue(com, "comment_count didn't count as an integer.");
        }

        [TestMethod()]
        public void InsertDataFromTodayTest()
        {
            countTest++;
            DAL.testMood = true;
            DAL.DB();
            DAL.OpenConnect();

            SQLiteCommand command = new SQLiteCommand(null, DAL.connection);
            command.CommandText = "SELECT * FROM VideosInfoDay1 WHERE " + DAL.IDName + " = 'gU-bilE6qfo'";
            command.Prepare();
            var reader = command.ExecuteReader();
            command.Dispose();
            while (reader.Read())
            {
                Assert.IsTrue(reader["title"].ToString().Equals("#4 MAVERICKS vs #3 WARRIORS | FULL GAME HIGHLIGHTS | May 26, 2022"), "didn't insert correctly column title.");
                Assert.IsTrue(reader["channelid"].ToString().Equals("UCWJ2lWNubArHWmf3FIHbfcQ"), "didn't insert correctly column channelid.");
                Assert.IsTrue(reader["channelName"].ToString().Equals("NBA"), "didn't insert correctly column channelName.");
                Assert.IsTrue(reader["category"].ToString().Equals("17"), "didn't insert correctly column category.");
                Assert.IsTrue(reader["likes"].ToString().Equals("43303"), "didn't insert correctly column likes.");
                Assert.IsTrue(reader["view_count"].ToString().Equals("3486952"), "didn't insert correctly column view_count.");
                Assert.IsTrue(reader["comment_count"].ToString().Equals("4508"), "didn't insert correctly column comment_count.");
                Assert.IsTrue(reader["comments_disabled"].ToString().Equals("False"), "didn't insert correctly column comments_disabled.");
                Assert.IsTrue(reader["ratings_disabled"].ToString().Equals("False"), "didn't insert correctly column ratings_disabled.");
                Assert.IsTrue(reader["description"].ToString().Contains("Stay up-to-date on news"), "didn't insert correctly column description.");
                Assert.IsTrue(reader["tags"].ToString().Equals("Basketball|G League|NBA|game-0042100315"), "didn't insert correctly column tags.");
                Assert.IsTrue(reader["PullDate"].ToString().Equals("01-06-2022"), "didn't insert correctly column PullDate.");
            }
            reader.Close();

            SQLiteCommand command1 = new SQLiteCommand(null, DAL.connection);
            command1.CommandText = "SELECT * FROM ID WHERE " + DAL.IDName + " = 'gU-bilE6qfo'";
            command1.Prepare();
            var reader1 = command1.ExecuteReader();
            command1.Dispose();
            while (reader1.Read())
            {
                Assert.IsTrue(reader1["counter"].ToString().Equals("2"), "didn't insert correctly counter to ID.");
            }
            reader1.Close();

            DAL.CloseConnect();
            DAL.testMood = false;
            DAL.DB();
        }
    }
}