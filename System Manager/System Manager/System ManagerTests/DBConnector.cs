using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data;

namespace System_ManagerTests
{
    class DBConnector
    {


    
        private  SQLiteConnection connection = null;

        public DBConnector()
        {

        }

        public  void OpenConnect(string db_path)
        {
           String  Connection_String = @"Data Source="+db_path;

            if (connection == null )
                connection = new SQLiteConnection(Connection_String);
            if (connection.State == ConnectionState.Closed)
                connection.Open();

            Console.WriteLine("Open DB.");
        }
        public  void CloseConnect()
        {
            if (connection.State != ConnectionState.Closed)
                connection.Close();
        }

        public IEnumerable<object[]>[] getAll()
        {
            IEnumerable<object[]>[] ans = new IEnumerable<object[]>[7];
            for (int i = 0; i < ans.Length; i++)
            {
                ans[i] = getTable("VideosInfoDay" + (i+1));
            }
            return ans;
        }

        public IEnumerable<object[]> getMetrics()
        {
            return getTable("FilterHypeScore");

        }

        public IEnumerable<object[]> getModelScore()
        {
            return getTable("ModelHypeScore");

        }

        private IEnumerable<object[]> getTable(string tableName)
        {
            List<object[]> ans = new List<object[]>();
            string command_Str = "SELECT * FROM " + tableName;
            SQLiteCommand command = new SQLiteCommand(command_Str, connection);
            SQLiteDataReader reader = command.ExecuteReader();
 
            foreach (var item in reader)
            {
                object[] arr = new object[reader.FieldCount];
                reader.GetValues(arr);
                ans.Add(arr);
            }
            command.Dispose();
            return ans;
        }   


    }
}
