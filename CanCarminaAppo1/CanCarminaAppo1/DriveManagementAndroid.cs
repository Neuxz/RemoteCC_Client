using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.IO;
using Android.Content.Res;
using Mono.Data.Sqlite;
using System.Data.SqlClient;
//using System.Data.SqlClient;

namespace CanCarminaAppo1
{
    class DriveManagementAndroid : DriveManagement
    {
        private static string dbName = "Temp.cach";
        private static string databasePath = Path.Combine(Android.OS.Environment.ExternalStorageDirectory.ToString(), dbName);
        public static new bool createDatabase(User current, Context ct)
        {
            if (!File.Exists(databasePath))
            {
                //http://stackoverflow.com/questions/18715613/use-a-local-database-in-xamarin
                //https://forums.xamarin.com/discussion/6990/how-to-correctly-save-and-read-files
                AssetManager assets = new ContextWrapper(ct).Assets;
                using (BinaryReader br = new BinaryReader(assets.Open(dbName)))
                {
                    using (BinaryWriter bw = new BinaryWriter(new FileStream(databasePath, FileMode.Create)))
                    {
                        byte[] buffer = new byte[2048];
                        int len = 0;
                        while ((len = br.Read(buffer, 0, buffer.Length)) > 0)
                        {
                            bw.Write(buffer, 0, len);
                        }
                    }
                }
                using (SqliteConnection co = new SqliteConnection(databasePath))
                {
                    co.Open();
                    SqliteCommand cmd = co.CreateCommand();//   .CreateCommand();
                    cmd.CommandText = "Insert into User (ID,Name, Chor, Appointments) values(@id, @name, @chor, @appoi)";
                    List<SqliteParameter> sqlisat = new List<SqliteParameter>() {
                    new SqliteParameter("@id", current.usrID),
                    new SqliteParameter("@name", current.phrase),
                    new SqliteParameter("@chor", current.usrCH),
                    new SqliteParameter("@appoi", current.storage)
                    };
                    sqlisat.ForEach(cmdPam => cmd.Parameters.Add(cmdPam));
                    cmd.ExecuteNonQuery();
                }
            }
            return current.Equals(getDatabase(ct));
        }

        public static new User getDatabase(Context ct)
        {
            User result = new User();
            using (SqliteConnection co = new SqliteConnection(databasePath))
                {
                co.Open();
                SqliteCommand cmd = co.CreateCommand();
                cmd.CommandText = "Selct * From User";
                try
                {
                    SqliteDataReader read = cmd.ExecuteReader();
                    if(read.Read())
                    {
                        result.usrID = (string)read["ID"];
                        result.phrase = (string)read["Name"];
                        result.usrCH = (string)read["Chor"];
                        result.storage = (List<Appointment>)read["Appointments"];
                    }
                }
                catch(Exception ex)
                {

                }
                cmd.ExecuteNonQuery();
            }
            return result;
        }
    }
}