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

        private void RegisterOnClick(object s, EventArgs e)
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
                User newUser = new User
                {
                    Username = userNameText.Text,
                    Password = passwordText.Text,
                    DisplayName = displayNameText.Text,
                    Email = emailText.Text
                };
                long userID = DBHandler.Instance.AddUser(newUser);

                userNameText.Text = "";
                passwordText.Text = "";
                displayNameText.Text = "";
                emailText.Text = "";

                if (userID != -1)
                {
                    Collection newCollection = new Collection
                    {
                        Name = "Default List",
                        CreatorID = userID
                    };
                    long collectionID = DBHandler.Instance.AddCollection(newCollection);

                    if (collectionID != -1)
                    {
                        UserCollectionList newList = new UserCollectionList
                        {
                            UserID = userID,
                            CollectionID = collectionID
                        };

                        bool result = DBHandler.Instance.AddUserCollection(newList);

                        if (result)
                        {
                            Random r = new Random(System.Environment.TickCount);
                            int testCount = r.Next(10);

                            for (int x = 0; x < testCount; x++)
                            {
                                CollectionItemList list = new CollectionItemList
                                {
                                    CollectionID = collectionID,
                                    MovieID = collectionID + " Test Data " + x
                                };

                                DBHandler.Instance.AddCollectionItem(list);
                            }
                            Toast.MakeText(this, "User registered!", ToastLength.Long).Show();
                        }
                        else
                        {
                            Toast.MakeText(this, "Unable to link user to collection!", ToastLength.Long).Show();
                        }
                    }
                    else
                    {
                        Toast.MakeText(this, "Unable to add collection!", ToastLength.Long).Show();
                    }
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