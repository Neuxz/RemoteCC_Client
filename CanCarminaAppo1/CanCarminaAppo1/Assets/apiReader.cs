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
    class apiReader
    {
        public enum debug{
            Debug, NONdebug};
        private const string api = "/?m=api&c=";
        private string usrID;
        private string usrCH;
        private apiReader()
        {
            usrID = "";
            usrCH = "";
        }
        public static apiReader createReader(string qrResult)
        {
            apiReader ar = new apiReader();
            return ar;
        }
        [Obsolete("Only for Debug and Tests")]
        public static apiReader createReader()
        {
            apiReader ar = new apiReader();
            ar.usrID = "ichpassword";
            ar.usrCH = "cancarmina.de";
            return ar;
        }
    }
}