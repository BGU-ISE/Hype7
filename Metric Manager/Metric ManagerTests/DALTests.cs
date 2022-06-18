using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hype7;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data;

namespace Hype7.Tests
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
                DAL.testMood = true;
                DAL.DB();
                string[] arr = new string[0];
                SystemManager.InitializeData(arr);
                DAL.OpenConnect();
                DAL.CalcPlayCountAllWeek(true);
                SystemManager.RunAllMetricsWeek();
                DAL.CloseConnect();
            }

        }
        [TestCleanup()]
        public void CleanTest()
        {
            if(countTest == 4)
            {
                string[] arr = new string[0];
                SystemManager.InitializeData(arr);
                DAL.OpenConnect();

                DAL.ResetDBToAnalysis();

                SQLiteCommand command = new SQLiteCommand(null, DAL.connection);
                command.CommandText = "SELECT * FROM PlayCountPerDay";
                command.Prepare();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Assert.Fail("there is somethig in PlayCountPerDay");
                }
                reader.Close();

                command.CommandText = "SELECT * FROM FilterHypeScore";
                command.Prepare();
                var reader1 = command.ExecuteReader();
                while (reader1.Read())
                {
                    Assert.Fail("there is somethig in FilterHypeScore");
                }
                reader1.Close();

                command.CommandText = "SELECT * FROM Hashtags";
                command.Prepare();
                var reader2 = command.ExecuteReader();
                while (reader2.Read())
                {
                    Assert.Fail("there is somethig in Hashtags");
                }
                reader2.Close();

                command.Dispose();
                DAL.CloseConnect();

                DAL.testMood = false;
                DAL.DB();
            }
        }

        [TestMethod()]
        public void aOpenConnectTest()
        {
            DAL.OpenConnect();

            String DBName = @"..\..\..\..\..\Metric Manager\Metric Manager\bin\Debug\net5.0\DataBaseTest.db";
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
            reader.Close();
            command.Dispose();

            Assert.IsTrue(filter, "FilterHypeScore table doesn't created.");
            Assert.IsTrue(model, "ModelHypeScore table doesn't created.");
            Assert.IsTrue(play, "PlayCountPerDay table doesn't created.");
            Assert.IsTrue(tag, "Hashtags table doesn't created.");
            Assert.IsTrue(id, "ID table doesn't created.");

            DAL.CloseConnect();
            countTest++;
        }

        [TestMethod()]
        public void CloseConnectTest()
        {
            if (DAL.connection != null && DAL.connection.State == ConnectionState.Open)
                DAL.CloseConnect();

            DAL.OpenConnect();
            Assert.IsNotNull(DAL.connection, "connection is null");
            Assert.IsTrue(DAL.connection.State == ConnectionState.Open, "connection is not open");
            DAL.CloseConnect();
            Assert.IsTrue(DAL.connection.State == ConnectionState.Closed, "connection is not close");
            countTest++;
        }

        [TestMethod()]
        public void fCalcPlayCountAllWeekTest()
        {
            string[] arr = new string[0];
            SystemManager.InitializeData(arr);
            DAL.OpenConnect();

            SQLiteCommand command = new SQLiteCommand(null, DAL.connection);
            command.CommandText = "SELECT * FROM PlayCountPerDay WHERE " + DAL.IDName + " = 'kBeQasWa3ts'";
            command.Prepare();
            var reader = command.ExecuteReader();
            command.Dispose();
            while (reader.Read())
            {
                Assert.IsTrue(reader["playCountDay1"].ToString().Equals("0"), "didn't calc play count per day 1.");
                Assert.IsTrue(reader["playCountDay2"].ToString().Equals("26053"), "didn't calc play count per day 2.");
                Assert.IsTrue(reader["playCountDay3"].ToString().Equals("17584"), "didn't calc play count per day 3.");
                Assert.IsTrue(reader["playCountDay4"].ToString().Equals("8956"), "didn't calc play count per day 4.");
                Assert.IsTrue(reader["playCountDay5"].ToString().Equals("5046"), "didn't calc play count per day 5.");
                Assert.IsTrue(reader["playCountDay6"].ToString().Equals("3545"), "didn't calc play count per day 6.");
                Assert.IsTrue(reader["playCountDay7"].ToString().Equals("2205"), "didn't calc play count per day 7.");
                Assert.IsTrue(reader["playCountAllWeek"].ToString().Equals("63389"), "didn't calc all play count.");

            }
            reader.Close();

            countTest++;
            DAL.CloseConnect();
        }
        [TestMethod()]
        public void RunAllMetricsWeekTest()
        {
            string[] arr = new string[0];
            SystemManager.InitializeData(arr);
            DAL.OpenConnect();

            // metrics
            SQLiteCommand command = new SQLiteCommand(null, DAL.connection);
            command.CommandText = "SELECT * FROM FilterHypeScore WHERE " + DAL.IDName + " = '0E-WkKWfHFk'";
            command.Prepare();
            var reader = command.ExecuteReader();
            bool flagPlayCount = false;
            bool flagmMetric = false;

            while (reader.Read())
            {
                var str = reader["metric"].ToString();
                if (str.Equals("view_count"))
                {
                    flagPlayCount = true;
                    string h = reader["slope"].ToString();
                    Assert.IsTrue(reader["averageScore"].ToString().Equals("1583855.8"), "calc averageScore in metric not correct.");
                    Assert.IsTrue(reader["slope"].ToString().Equals("137406.60714285713"), "calc slope in metric not correct.");
                }
                if (str.Equals("view_count/comment_count"))
                {
                    flagmMetric = true;
                    Assert.IsTrue(reader["scoreDay1"].ToString().Equals("3033"), "calc ScoreDay1 in metric not correct.");
                    Assert.IsTrue(reader["scoreDay2"].ToString().Equals("3367"), "calc ScoreDay2 in metric not correct.");
                    Assert.IsTrue(reader["scoreDay3"].ToString().Equals("3628"), "calc ScoreDay3 in metric not correct.");
                    Assert.IsTrue(reader["scoreDay4"].ToString().Equals("3843"), "calc ScoreDay4 in metric not correct.");
                    Assert.IsTrue(reader["scoreDay5"].ToString().Equals("3927"), "calc ScoreDay5 in metric not correct.");
                    Assert.IsTrue(reader["scoreDay6"].ToString().Equals("4061"), "calc ScoreDay6 in metric not correct.");
                    Assert.IsTrue(reader["scoreDay7"].ToString().Equals("4192"), "calc ScoreDay7 in metric not correct.");
                    Assert.IsTrue(reader["formula"].ToString().Equals("2983.8571428571427 + 184.42857142857142 * X"), "calc formula in metric not correct.");
                }
            }
            reader.Close();
            Assert.IsTrue(flagPlayCount, "didn't calc PlayCount metric.");
            Assert.IsTrue(flagmMetric, "didn't calc metric 2.");

            // hashtags
            command.CommandText = "SELECT * FROM Hashtags WHERE name = 'minecraft' OR name = 'boxing'";
            command.Prepare();
            var reader1 = command.ExecuteReader();
            bool minecraft = false;
            bool boxing = false;

            while (reader1.Read())
            {
                var str = reader1["name"].ToString();
                if (str.Equals("minecraft"))
                {
                    minecraft = true;
                    string k = reader1["slope"].ToString();
                    Assert.IsTrue(reader1["averageScore"].ToString().Equals("18.285715"), "calc averageScore in tags not correct.");
                    Assert.IsTrue(reader1["slope"].ToString().Equals("2.2857142857142856"), "calc slope in tags not correct.");
                }
                if (str.Equals("boxing"))
                {
                    boxing = true;
                    Assert.IsTrue(reader1["scoreDay1"].ToString().Equals("3"), "calc ScoreDay1 in tags not correct.");
                    Assert.IsTrue(reader1["scoreDay2"].ToString().Equals("3"), "calc ScoreDay2 in tags not correct.");
                    Assert.IsTrue(reader1["scoreDay3"].ToString().Equals("5"), "calc ScoreDay3 in tags not correct.");
                    Assert.IsTrue(reader1["scoreDay4"].ToString().Equals("8"), "calc ScoreDay4 in tags not correct.");
                    Assert.IsTrue(reader1["scoreDay5"].ToString().Equals("11"), "calc ScoreDay5 in tags not correct.");
                    Assert.IsTrue(reader1["scoreDay6"].ToString().Equals("11"), "calc ScoreDay6 in tags not correct.");
                    Assert.IsTrue(reader1["scoreDay7"].ToString().Equals("11"), "calc ScoreDay7 in tags not correct.");
                    Assert.IsTrue(reader1["formula"].ToString().Equals("0.8571428571428571 + 1.6428571428571428 * X"), "calc formula in tags not correct.");
                }
            }
            reader1.Close();
            Assert.IsTrue(minecraft, "didn't calc hashtag - minecraft.");
            Assert.IsTrue(boxing, "didn't calc hashtag - boxing.");

            command.Dispose();
            DAL.CloseConnect();
            countTest++;
        }
    }
}