using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using Microsoft.Data.Sqlite;
using System.Data.SQLite;

namespace Hype7
{
    static class VideoDAL
    {
        static private List<string> FieldsName;
        static private string FieldsNameStr;
        static private string checke;
        public static void saveVideo(VideoInfo VideoDetails, string date, bool isDBOpen)
        {
            //var dateFormat = date.ToShortDateString();
            //string[] arr = dateFormat.Split("/");
            // date.ToShortDateString()

            try
            {
                if (!isDBOpen)
                    DAL.OpenConnect();
                string temp = GetValues(VideoDetails);
                if (temp.Length != 0)
                {
                    SQLiteCommand command = new SQLiteCommand(null, DAL.connection);
                    command.CommandText = "INSERT INTO VideosInfoDay"+ VideoDetails.GetSerialDate() + "(" + GetFields() + ")  " +
                            " VALUES (" + GetValues(VideoDetails) + ", '" + date + "')";
                    checke = command.CommandText;
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
                FieldsName = SystemManager.GetFieldsName();
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
        private static string GetValues(VideoInfo VideoDetails)
        {
            var Data = VideoDetails.GetData();
            string ans = "";
            //if (Data.Length > 22)
                //return "";
            for (int i=0; i<21 /*Data.Length-1*/; i++)
            {
                //string temp = Data[i].Replace(" ", "\'");
                Data[i] = Data[i].Replace("'", " ");
                //Data[i] = Data[i].Replace(" ", "'");
                //if (i == 0)
                //{
                //    decimal h2 = Decimal.Parse(temp, System.Globalization.NumberStyles.Any);
                //    temp = h2 + "";
                //}

                //if(temp.Length > 0)
                ans += "'" + Data[i] + "', ";
            }
            var hash = Data[Data.Length - 1];
            hash = hash.Replace("[", string.Empty);
            hash = hash.Replace(" ", string.Empty);
            hash = hash.Replace("'", string.Empty);
            hash = hash.Replace("]", string.Empty);
            string hashToAdd = "' '";
            if(hash.Length > 0)
            {
                hashToAdd = "'" + hash + "'";
            }
            ans += hashToAdd;
            //ans = ans.Substring(0, ans.Length - 3);
            return ans;
        }
    }
}
