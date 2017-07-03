using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;
using System;

namespace Tracker
{
    [Activity(Label = "Tracker", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        public static string ARG_USER = "ARG_USER";

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView (Resource.Layout.LoginActivity);

            Button loginBtn = FindViewById<Button>(Resource.Id.loginBtn);
            loginBtn.Click += LoginOnClick;

            Button newUserBtn = FindViewById<Button>(Resource.Id.newUserBtn);
            newUserBtn.Click += NewUserRegisterOnClick;
        }

        private void LoginOnClick(object s, EventArgs e)
        { 
            EditText userNameText = FindViewById<EditText>(Resource.Id.userNameText);
            EditText passwordText = FindViewById<EditText>(Resource.Id.passwordText);
 
            if (userNameText.Text.Trim().Length != 0 && passwordText.Text.Trim().Length != 0)
            {
                User user = DBHandler.Instance.GetUser(userNameText.Text, passwordText.Text);

                if (user != null)
                {
                    var intent = new Intent(this, typeof(MainUserActivity));
                    var bundle = new Bundle();

                    bundle.PutParcelable(MainUserActivity.ARG_USER, user);
                    intent.PutExtras(bundle);

                    StartActivity(intent);
                }
                else
                {
                    Toast.MakeText(this, "Username or password incorrect!", ToastLength.Long).Show();
                }
            }
            else
            {
                Toast.MakeText(this, "Missing fields!", ToastLength.Long).Show();
            }
        }

        private void NewUserRegisterOnClick(object s, EventArgs e)
        {
            var intent = new Intent(this, typeof(NewUserRegisterActivity));
            StartActivity(intent);
        }
    }
}

