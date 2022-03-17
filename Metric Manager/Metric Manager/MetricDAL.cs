using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hype7
{
    static class MetricDAL
    {
        public static void saveUser(MetricOld UserDetails)
        {
            try
            {
                DAL.OpenConnect();
                //SqliteCommand command = new SqliteCommand(null, DAL.connection);
                //command.CommandText = "INSERT INTO Users(UserName, Password)  " +
                        //" VALUES (@User_UserName, @User_Password)";
                /*SqliteParameter UserName = new SqliteParameter("@User_UserName", UserDetails.getUserName());
                SqliteParameter Password = new SqliteParameter("@User_Password", UserDetails.getPassword());
                command.Parameters.Add(UserName);
                command.Parameters.Add(Password);
                command.Prepare();
                int changes = command.ExecuteNonQuery();
                command.Dispose();
                DAL.CloseConnect();
                MileStone4.DataAcces_Layer.Logger.Log.Info("The registrtion of " + UserDetails.getUserName() + " is Succeeded");
                */
            }
            catch (SqliteException e)
            {
               // MileStone4.DataAcces_Layer.Logger.Log.Fatal("while the saving process of " + UserDetails.getUserName() + " accord, there was an error -" + e.Message);
                DAL.CloseConnect();
            }

        }
    }
}
