using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace CanCarminaAppo1
{
    [Activity(Label = "CanCarminaAppo1", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resourcelay
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it

            FindViewById<Button>(Resource.Id.MyButton).Click += delegate 
            {
                apiReader ar = apiReader.createReader("ichpassword;cancarmina.de");
                if(ar.CheckLogIN())
                {
                    FindViewById<EditText>(Resource.Id.editText1).Text = ar.getTermine();
                }
            };
            FindViewById<Button>(Resource.Id.button1).Click += delegate
            {
                new AlertDialog.Builder(this).SetNeutralButton("Ok", delegate { }).SetMessage("Not Implemented yet!").SetTitle("Debug Error").Show();
            };
        }


    }
}

