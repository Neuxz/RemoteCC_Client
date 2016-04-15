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
        private const string apiTrmIDIS = "&t=";
        private const string apiAnAbML = "&a=";
        private const string apiAnML = "n";
        private const string apiAbML = "b";
        private static bool userExists;
        private User curent;

        public static bool UserExists
        {
            get
            {
                return (DriveManagementAndroid.getDatabase().phrase != null) ;
            }
        }

        //Create help[]
        private apiConnector()
        {
            curent = new User();
        }
        private apiConnector(User curent)
        {
            this.curent = curent;
        }
        public static apiConnector createReader()
        {
            return new apiConnector(DriveManagementAndroid.getDatabase());
        }
        public static apiConnector createReader(string qrResult, Context cont)
        {

            string[] signs = qrResult.Split(';');
            User temp = new User(signs[0], signs[1]);
            apiConnector apiConn = new apiConnector();
            if (!DriveManagementAndroid.createDatabase(temp, cont))
            {
                apiConn.curent = temp;
            }
            else
            {
                apiConn.curent = DriveManagementAndroid.getDatabase();
            }
                

            DriveManagement dm = new DriveManagementAndroid();
            return apiConn;
        }

        //Methods
        public bool CheckLogIN()
        {
            bool returner = false;
            try
            {
                HttpWebRequest myRequest =
                 (HttpWebRequest)WebRequest.Create("http://" + curent.usrCH + api + curent.phrase);
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
        public string Anmelden(bool anmelden,string trmIF, out bool anmeldung)
        {
            string result = getStringResponse((HttpWebRequest)WebRequest.Create("http://" + curent.usrCH + api + curent.phrase + apiTrmIDIS + trmIF + apiAnAbML + (anmelden?apiAnML:apiAbML)));

            if (result.Contains("Neuer Status: angemeldet. Danke!"))
            {
                anmeldung = true;
                return "Du wurdest angemeldet";
            }
            else if (result.Contains("Neuer Status: abgemeldet. Danke!"))
            {
                anmeldung = false;
                return "Du wurdest abgemeldet";
            }
            else if (result.Contains("Abmelden ist nicht mehr möglich."))
            {
                anmeldung = true;
                return "Änderung nicht Möglich!";
            }
            anmeldung = true;
            return "Netzwerk fehler.";
                
                    
       }
        private string getStringResponse(HttpWebRequest wq)
        {
            try
            {
                using (WebResponse response = wq.GetResponse())
                {
                    using (System.IO.StreamReader reader = new System.IO.StreamReader(response.GetResponseStream()))
                    {
                        return reader.ReadToEnd();
                    }
                }
            }
            catch(Exception ec)
            {
                return ec.ToString();
            }
        }
        public List<Appointment> getTermine()
        {
            return LoadTermine((HttpWebRequest)WebRequest.Create("http://" + curent.usrCH + api + curent.phrase + apiTrmIDIS + "alle"));
        }
        public List<Appointment> getTermine(string trmId)
        {
            return LoadTermine((HttpWebRequest)WebRequest.Create("http://" + curent.usrCH + api + curent.phrase + apiTrmIDIS + trmId));
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
    }
}