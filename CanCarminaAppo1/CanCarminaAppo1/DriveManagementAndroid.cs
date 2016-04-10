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

namespace CanCarminaAppo1
{
    class DriveManagementAndroid : DriveManagement
    {
        private static string dbName = "Temp.cach";
        private string databasePath = Path.Combine(Android.OS.Environment.ExternalStorageDirectory.ToString(), dbName);
        public override bool createDatabase(User current)
        {
            if(!File.Exists(databasePath))
            {
                //http://stackoverflow.com/questions/18715613/use-a-local-database-in-xamarin
                //https://forums.xamarin.com/discussion/6990/how-to-correctly-save-and-read-files
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
            }
        }
    }
}