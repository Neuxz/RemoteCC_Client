using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
/*
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;*/

namespace CanCarminaAppo1.Assets
{
    class JsonArrayParser
    {
        private JsonArrayParser()
        {

        }
        public static List<Termin> parseArrayToTerminList(string JasonArrString)
        {
            List<Termin> TerList = new List<Termin>();
            char[] c = new char[2];
            c[0] = '[';
            c[1] = ']';
            JasonArrString = JasonArrString.Trim(c);
            string[] temda = JasonArrString.Split('{');
            return TerList;
        }

    }
}