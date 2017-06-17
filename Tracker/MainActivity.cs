using Android.App;
using Android.Widget;
using Android.OS;
using Android.Views;
using System;
using Android.Content;

namespace Tracker
{
    [Activity(Label = "Tracker", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView (Resource.Layout.LoginActivity);

            EditText userNameText = FindViewById<EditText>(Resource.Id.userNameText);
            EditText passwordText = FindViewById<EditText>(Resource.Id.passwordText);
            Button loginBtn = FindViewById<Button>(Resource.Id.loginBtn);

            loginBtn.Click += CheckLogin;
        }

        private void CheckLogin(object s, EventArgs e)
        {
            if (true)
            {
                var intent = new Intent(this, typeof(MainUserActivity));
                StartActivity(intent);
            }
        }
    }
}

