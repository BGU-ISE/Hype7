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
        /*if (testMood)
            {
                LastIndexTable = GetLastIndexTable(true);
                InitIntField();
                int indexCreatesTable = CreateDateTable(SystemManager.GetFieldsName());
                InsertDataFromToday(SystemManager.GetData(DateTime.Now.ToString("04-01-2022")), indexCreatesTable, true);
                indexCreatesTable = CreateDateTable(SystemManager.GetFieldsName());
                InsertDataFromToday(SystemManager.GetData(DateTime.Now.ToString("05-01-2022")), indexCreatesTable, true);
                CalcPlayCountPerDay();
                //InsertAllData(SystemManager.GetAllData(), true);
                //CalcPlayCountAllWeek(true);
            }*/
        [TestMethod()]
        public void OpenConnectTest()
        {
            DAL.testMood = true;
            DAL.DB();
            DAL.OpenConnect();
            Assert.IsTrue(System.IO.File.Exists("DataBaseTest.db"), "DB not created.");
            SQLiteCommand command = new SQLiteCommand(null, DAL.connection);
            command.CommandText = "SELECT name FROM sqlite_master WHERE type = 'table'";
            command.Prepare();
            var reader = command.ExecuteReader();
            bool flag = false;
            while (reader.Read())
            {
                string tableName = reader["name"].ToString();
                if (tableName.Contains("FilterHypeScore"))
                {
                    flag = true;
                    break;
                }
            }
            Assert.IsTrue(flag, "init tables doesn't created.");
            DAL.CloseConnect();
            DAL.testMood = false;
            DAL.DB();
        }

        [TestMethod()]
        public void SetUpDBTest()
        {
            DAL.testMood = true;
            DAL.DB();
            DAL.OpenConnect();
            DAL.SetUpDB();
            SQLiteCommand command = new SQLiteCommand(null, DAL.connection);
            command.CommandText = "SELECT video_id, playCountDay3 FROM PlayCountPerDay WHERE video_id = '7038764928076877103'";
            command.Prepare();
            var reader = command.ExecuteReader();
            bool flag = false;
            while (reader.Read())
            {
                string str = reader["playCountDay3"].ToString();
                flag = str.Length > 0;
            }
            Assert.IsTrue(flag, "didn't calc play count per day.");
            int j = DAL.GetLastIndexTable();
            Assert.IsTrue(DAL.GetLastIndexTable() != 0, "didn't create last table.");
            DAL.ResetDB();
            DAL.testMood = false;
            DAL.DB();
        }

        [TestMethod()]
        public void CloseConnectTest()
        {
            Assert.IsNull(DAL.connection, "connection is open");
            DAL.OpenConnect();
            Assert.IsNotNull(DAL.connection, "connection is null");
            Assert.IsTrue(DAL.connection.State == ConnectionState.Open, "connection is not open");
            DAL.CloseConnect();
            Assert.IsTrue(DAL.connection.State == ConnectionState.Closed, "connection is not close");
        }
    }
}