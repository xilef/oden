using Android.App;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V4.App;
using Android.Support.V4.View;
using Android.Widget;
using System.Collections.Generic;

namespace Tracker
{
    [Activity(Label = "Testing")]
    public class MainUserActivity : FragmentActivity
    {
        public static string ARG_USER = "ARG_USER";

        public User LoggedIn { get; set; }

        public static string ARG_COLLECTION = "ARG_COLLECTION";

        public List<Collection> UserCollection { get; private set; }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.MainUserActivity);

            Bundle bundle = Intent.Extras;
            LoggedIn = (User)bundle.GetParcelable(ARG_USER);

            // get user collection list parcelable
            UserCollection = new List<Collection>();
            var parcelList = bundle.GetParcelableArrayList(ARG_COLLECTION);

            foreach (Bundle listBundle in parcelList)
            {
                UserCollection.Add(new Collection(listBundle));
            }
 
            ViewPager viewPager = FindViewById<ViewPager>(Resource.Id.viewpager);
            viewPager.Adapter = new MainUserTabAdapter(SupportFragmentManager);

            TabLayout tabLayout = FindViewById<TabLayout>(Resource.Id.sliding_tabs);
            tabLayout.SetupWithViewPager(viewPager);

            TextView welcomeText = FindViewById<TextView>(Resource.Id.welcomeText);
            welcomeText.Text = GetString(Resource.String.WelcomeText) + " " + LoggedIn.DisplayName + "!";
        }
    }
}