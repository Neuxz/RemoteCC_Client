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
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace CanCarminaAppo1
{
    [Serializable]
    class apiConnector
    {
        public enum debug{
            Debug, NONdebug};
        private const string api = "/?m=api&c=";
        private const string apiTMList = "&t=";
        private User curent;
        //Create help
        private apiConnector()
        {
            curent = new User();
        }
        public static apiConnector createReader(string qrResult)
        {

            string[] signs = qrResult.Split(';');
                apiConnector apiConn = new apiConnector();
                apiConn.curent.usrID = signs[0];
                apiConn.curent.usrCH = signs[1];
            return apiConn;
        }

        //Methods
        public bool CheckLogIN()
        {
            bool returner = false;
            try
            {
                HttpWebRequest myRequest =
                 (HttpWebRequest)WebRequest.Create("http://" + curent.usrCH + api + curent.usrID);
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
            return LoadTermine((HttpWebRequest)WebRequest.Create("http://" + curent.usrCH + api + curent.usrID + apiTMList + "alle"));
        }
        public List<Appointment> getTermine(string trmId)
        {
            return LoadTermine((HttpWebRequest)WebRequest.Create("http://" + curent.usrCH + api + curent.usrID + apiTMList + trmId));
        }
        private List<Appointment> LoadTermine(HttpWebRequest myRequest)
        {
            //List<Termine> terminListe = new List<Termine>();
            List<Appointment> Resu = null;
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
        public Byte[] Serialize()
        {
            BinaryFormatter bf = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream())
            {
                bf.Serialize(ms, this);
                return ms.ToArray();
            }
        }

        public static apiConnector Deserialize(Byte[] input)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                BinaryFormatter bf = new BinaryFormatter();
                ms.Write(input, 0, input.Length);
                ms.Seek(0, SeekOrigin.Begin);
                return (apiConnector)bf.Deserialize(ms);
            }
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