using Android.App;
using Android.OS;
using Android.Widget;
using SQLite;
using System;
using System.Threading.Tasks;

namespace Tracker
{
    [Activity(Label = "NewUserRegisterActivity")]
    public class NewUserRegisterActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.NewUserRegister);

            Button registerBtn = FindViewById<Button>(Resource.Id.registerBtn);
            registerBtn.Click += RegisterOnClick;
        }

        async private void RegisterOnClick(object s, EventArgs e)
        {
            TextView userNameText = FindViewById<TextView>(Resource.Id.usernameRegisterText);
            TextView passwordText = FindViewById<TextView>(Resource.Id.passwordRegisterText);
            TextView displayNameText = FindViewById<TextView>(Resource.Id.displayNameRegisterText);
            TextView emailText = FindViewById<TextView>(Resource.Id.emailRegisterText);

            DateTime timeStamp = DateTime.Now;

            if (userNameText.Text.Trim().Length != 0 && 
                passwordText.Text.Trim().Length != 0 && 
                displayNameText.Text.Trim().Length != 0 &&
                emailText.Text.Trim().Length != 0)
            {
                Users newUser = new Users
                {
                    UserName = userNameText.Text,
                    Password = passwordText.Text,
                    DisplayName = displayNameText.Text,
                    Email = emailText.Text,
                };
                var result = await DBHandler.Instance.AddUser(newUser);

                userNameText.Text = "";
                passwordText.Text = "";
                displayNameText.Text = "";
                emailText.Text = "";

                if (result == 0)
                {
                    Toast.MakeText(this, "User registered!", ToastLength.Long).Show();
                }
                else
                {
                    Toast.MakeText(this, "User already exists!", ToastLength.Long).Show();
                }
            }
            else
            {
                Toast.MakeText(this, "Missing fields!", ToastLength.Long).Show();
            }
        }
    }
}