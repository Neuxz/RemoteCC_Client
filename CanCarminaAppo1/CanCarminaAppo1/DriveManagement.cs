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

namespace CanCarminaAppo1
{
    abstract class DriveManagement
    {
        public static bool createDatabase(User current, Context ct)
        {
            return false;
        }
        public static User getDatabase(Context ct)
        {
            return null;
        }
    }
}