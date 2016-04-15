using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace SingsteApp
{
    [Activity(Label = "Singste App", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            if (!apiConnector.UserExists)
            {

                // Set our view from the "main" layout resourcelay
                SetContentView(SingsteApp.Resource.Layout.Main);

                // Get our button from the layout resource,
                // and attach an event to it
                FindViewById<Spinner>(SingsteApp.Resource.Id.spinner1).MotionEventSplittingEnabled = false;
                FindViewById<Spinner>(SingsteApp.Resource.Id.spinner1).Visibility = ViewStates.Invisible;
                FindViewById<Button>(SingsteApp.Resource.Id.MyButton).Click += delegate
                {
                    if (FindViewById<EditText>(SingsteApp.Resource.Id.editText1).Text != String.Empty)
                    {
                        FindViewById<Spinner>(SingsteApp.Resource.Id.spinner1).MotionEventSplittingEnabled = true;
                        FindViewById<Spinner>(SingsteApp.Resource.Id.spinner1).Visibility = ViewStates.Visible;
                        apiConnector ar = apiConnector.createReader(FindViewById<EditText>(SingsteApp.Resource.Id.editText1).Text + ";cancarmina.de", this);
                        if (ar.CheckLogIN())
                        {
                            Intent newMain = new Intent(this, typeof(MainMenu));
                            StartActivity(newMain);
                        }
                    }
                    else
                    {
                        new AlertDialog.Builder(this).SetNeutralButton("Ok", delegate { }).SetMessage("Bitte geben sie ihren Benutzercode ein.").SetTitle("Kein Benutzer code.").Show();
                    }
                };
                FindViewById<Button>(SingsteApp.Resource.Id.button1).Click += delegate
                {
                    new AlertDialog.Builder(this).SetNeutralButton("Ok", delegate { }).SetMessage("Not Implemented yet!").SetTitle("Debug Error").Show();
                };
            }
            else
            {
                Intent newMain = new Intent(this, typeof(MainMenu));
                StartActivity(newMain);
                Finish();
            }
        }


    }
}

