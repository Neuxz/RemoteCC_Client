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

namespace SingsteApp
{
    abstract class DriveManagement
    {
        public abstract bool createDatabase(User current, Context ct);
        public abstract User getDatabase(Context ct);
    }
}
