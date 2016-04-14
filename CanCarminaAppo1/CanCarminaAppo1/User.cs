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
    class User
    {
        public string usrID;
        public string usrCH;
        public string phrase;
        public List<Appointment> storage = new List<Appointment>();
        public User() { }
        public User(string phrase, string chr)
        {
            this.phrase = phrase;
            this.usrCH = chr;

        }
        public void updateLocalTerminlist(List<Appointment> newList)
        {
            foreach(Appointment ap in newList)
            {
                if(!storage.Any(appointment => appointment.Trm_id.Equals(ap.Trm_id)))
                {
                    storage.Add(ap);
                    ///Alert new Appointment
                }
            }
        }
    }
}