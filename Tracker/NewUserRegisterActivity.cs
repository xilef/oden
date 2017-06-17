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
            registerBtn.Click += registerOnClick;
        }

        async private void registerOnClick(object s, EventArgs e)
        {
            TextView userNameText = FindViewById<TextView>(Resource.Id.usernameRegisterText);
            TextView passwordText = FindViewById<TextView>(Resource.Id.passwordRegisterText);
            TextView displayNameText = FindViewById<TextView>(Resource.Id.displayNameRegisterText);

            if (userNameText.Text.Trim().Length != 0 && passwordText.Text.Trim().Length != 0 && displayNameText.Text.Trim().Length != 0)
            {
                var result = await DBHandler.Instance.addUser(new Users { UserName = userNameText.Text, Password = passwordText.Text, DisplayName = displayNameText.Text });

                userNameText.Text = "";
                passwordText.Text = "";
                displayNameText.Text = "";

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