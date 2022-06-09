using Microsoft.VisualStudio.TestTools.UnitTesting;
using Scraper_Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data;

namespace Scraper_Manager.Tests
{
    [TestClass()]
    public class DALTests
    {
        [TestMethod()]
        public void OpenConnectTest()
        {
            DAL.testMood = true;
            DAL.DB();
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
            DAL.testMood = true;
            DAL.DB();
            if (DAL.connection != null && DAL.connection.State == ConnectionState.Open)
                DAL.CloseConnect();

            Assert.IsNull(DAL.connection, "connection is open");
            DAL.OpenConnect();
            Assert.IsNotNull(DAL.connection, "connection is null");
            Assert.IsTrue(DAL.connection.State == ConnectionState.Open, "connection is not open");
            DAL.CloseConnect();
            Assert.IsTrue(DAL.connection.State == ConnectionState.Closed, "connection is not close");

            DAL.testMood = false;
            DAL.DB();
        }
    }
}