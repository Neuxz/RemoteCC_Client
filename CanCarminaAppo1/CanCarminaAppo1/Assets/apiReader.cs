using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Org.Json;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.Net;

namespace CanCarminaAppo1
{
    class apiConnector
    {
        public enum debug{
            Debug, NONdebug};
        private const string api = "/?m=api&c=";
        private const string apiTMList = "&t=";
        private string usrID;
        private string usrCH;
        private bool terminAlle = true;
        //Create help
        private apiConnector()
        {
            usrID = "";
            usrCH = "";
        }
        public static apiConnector createReader(string qrResult)
        {
            string[] signs = qrResult.Split(';');
            apiConnector ar = new apiConnector();
            ar.usrID = signs[0];
            ar.usrCH = signs[1];
            return ar;
        }
        [Obsolete("Only for Debug and Tests")]
        public static apiConnector createReader()
        {
            apiConnector ar = new apiConnector();
            ar.usrID = "ichpassword";
            ar.usrCH = "cancarmina.de";
            return ar;
        }

        //Methods
        public bool CheckLogIN()
        {
            bool returner = false;
            try
            {
                HttpWebRequest myRequest =
                 (HttpWebRequest)WebRequest.Create("http://" + usrCH + api + usrID);
                Console.WriteLine(myRequest.GetResponse());
                using (WebResponse response = myRequest.GetResponse())
                {
                    Console.WriteLine(response.GetResponseStream());
                    using (System.IO.StreamReader reader = new System.IO.StreamReader(response.GetResponseStream()))
                    {
                        string debug = reader.ReadToEnd();
                        Console.WriteLine(debug);
                        returner = bool.Parse(debug);
                    }
                }
                //Console.WriteLine(myRequest.);";
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return returner;
        }

        public List<Appointment> getTermine()
        {
            //List<Termine> terminListe = new List<Termine>();
            List<Appointment> Resu = null;

            HttpWebRequest myRequest =
                 (HttpWebRequest)WebRequest.Create("http://" + usrCH + api + usrID + apiTMList + (terminAlle?"alle": "alle"));
            Console.WriteLine(myRequest.GetResponse());
            using (WebResponse response = myRequest.GetResponse())
            {
                Console.WriteLine(response.GetResponseStream());
                using (System.IO.StreamReader reader = new System.IO.StreamReader(response.GetResponseStream()))
                {
                    Resu = Appointment.CreateAppointmentList(ParseJson.JsonArrayToDictionaryList(reader.ReadToEnd()));

                }
            }

            return Resu;
        }
        [Obsolete("Ony for Debug")]
        public String AppointmentToList(List<Appointment> resu)
        {
            string mesu = "";
            foreach(Appointment sesu in resu)
            {
                mesu += sesu.ToString();
            }
            return mesu;
        }
        public struct Termine
        {
            public int trmID;
            public DateTime datumZeit;
            public DateTime beginn;
            public DateTime ende;
            public string titel;
            public string beschreibung;
            public string adresse;
            public bool istAngemeldet;
        }
    }
}