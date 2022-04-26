using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SQLite;
using Microsoft.Data.Sqlite;
using System.Text;
using System.Threading.Tasks;

namespace Scraper_Manager
{
    static class VideoDAL
    {
        static private List<string> FieldsName;
        static private string FieldsNameStr;
        static private string checke;
        public static void saveVideo(Record VideoDetails, string date, int tableIndex, bool isDBOpen)
        {
            try
            {
                if (!isDBOpen)
                    DAL.OpenConnect();
                string temp = VideoDetails.ToString();
                if (temp.Length != 0)
                {
                    SQLiteCommand command = new SQLiteCommand(null, DAL.connection);//31-12-2021
                    command.CommandText = "INSERT INTO VideosInfoDay" + tableIndex + "(" + GetFields() + ")  " +
                            " VALUES (" + temp + ", '" + date + "')"; //01-01-2022
                    DAL.checke = command.CommandText;
                    command.Prepare();
                    int changes = command.ExecuteNonQuery();
                    command.Dispose();
                    if (!isDBOpen)
                        DAL.CloseConnect();
                }
            }
            catch (SqliteException e)
            {
                DAL.CloseConnect();
            }

        }
        private static string GetFields()
        {
            if (FieldsNameStr != null && FieldsNameStr.Length > 0)
                return FieldsNameStr;
            if (FieldsName == null)
                FieldsName = ScraperManager.fieldNames;
            string ans = "";
            foreach (string field in FieldsName)
            {
                if (field.Length > 0)
                    ans += field + ", ";
            }
            //ans = ans.Substring(0, ans.Length - 3);
            ans += "PullDate";
            FieldsNameStr = ans;
            return ans;
        }
        //private static string GetValues(Record VideoDetails)
        //{
        //    var Data = VideoDetails.GetData();
        //    string ans = "";
        //    //if (Data.Length > 22)
        //    //return "";
        //    for (int i = 0; i < 21 /*Data.Length-1*/; i++)
        //    {
        //        //string temp = Data[i].Replace(" ", "\'");
        //        Data[i] = Data[i].Replace("'", " ");
        //        //Data[i] = Data[i].Replace(" ", "'");
        //        //if (i == 0)
        //        //{
        //        //    decimal h2 = Decimal.Parse(temp, System.Globalization.NumberStyles.Any);
        //        //    temp = h2 + "";
        //        //}

        //        //if(temp.Length > 0)
        //        ans += "'" + Data[i] + "', ";
        //    }
        //    var hash = Data[Data.Length - 1];
        //    hash = hash.Replace("[", string.Empty);
        //    hash = hash.Replace(" ", string.Empty);
        //    hash = hash.Replace("'", string.Empty);
        //    hash = hash.Replace("]", string.Empty);
        //    string hashToAdd = "' '";
        //    if (hash.Length > 0)
        //    {
        //        hashToAdd = "'" + hash + "'";
        //    }
        //    ans += hashToAdd;
        //    //ans = ans.Substring(0, ans.Length - 3);
        //    return ans;
        //}
    }
}
