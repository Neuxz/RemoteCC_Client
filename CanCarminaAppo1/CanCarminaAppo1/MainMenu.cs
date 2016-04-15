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
    [Activity(Label = "Termin Übersicht")]
    public class MainMenu : Activity
    {
        ListView lv;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(SingsteApp.Resource.Layout.MainMenu);
            apiConnector ar = apiConnector.createReader();
            lv = FindViewById<ListView>(SingsteApp.Resource.Id.TerminListe);
            lv.Adapter = new Termin_Adapter(this, ar.getTermine());
            lv.ItemClick += Lv_ItemClick;
            lv.ItemLongClick += Lv_ItemLongClick;
        ////https://developer.xamarin.com/guides/xamarin-forms/user-interface/listview/customizing-cell-appearance/
        //    // Create your application here
        }

        private void MainMenu_Click(object sender, EventArgs e)
        {
            Console.WriteLine(e.ToString());
        }

        private void Lv_ItemLongClick(object sender, AdapterView.ItemLongClickEventArgs e)
        {
            Console.WriteLine(((Termin_Adapter)lv.Adapter)[e.Position].Trm_id);
        }

        private void Lv_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            Console.WriteLine(((Termin_Adapter)lv.Adapter)[e.Position].Trm_id);
        }
    }
}