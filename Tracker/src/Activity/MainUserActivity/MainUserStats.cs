using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;

namespace Tracker
{
    class MainUserStats : MainUserTabFragment
    {
        public MainUserStats()
        {
            mTabTitle = Application.Context.Resources.GetString(Resource.String.StatsTabTitle);
        }

        public static MainUserStats NewInstance(int page)
        {
            Bundle args = new Bundle();
            args.PutInt(ARG_PAGE, page);
            MainUserStats fragment = new MainUserStats()
            {
                Arguments = args
            };
            return fragment;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);

            View view = inflater.Inflate(Resource.Layout.MainUserStatFragment, container, false);
            TextView textView = view.FindViewById<TextView>(Resource.Id.nowShowingText);
            textView.SetText(Resource.String.StatsText);

            return view;
        }
    }
}