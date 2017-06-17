using Android.App;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V4.App;
using Android.Support.V4.View;
using Android.Widget;

namespace Tracker
{
    [Activity(Label = "Testing")]
    public class MainUserActivity : FragmentActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.MainUserActivity);

            // Create your application here
            ViewPager viewPager = FindViewById<ViewPager>(Resource.Id.viewpager);
            viewPager.Adapter = new MainUserTabAdapter(SupportFragmentManager);

            TabLayout tabLayout = FindViewById<TabLayout>(Resource.Id.sliding_tabs);
            tabLayout.SetupWithViewPager(viewPager);

            TextView welcomeText = FindViewById<TextView>(Resource.Id.welcomeText);
            welcomeText.Text = GetString(Resource.String.WelcomeText) + " MJ";
        }
    }
}