using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;

namespace Tracker
{
    class MainUserFeed : MainUserTabFragment
    {
        public MainUserFeed()
        {
            mTabTitle = Application.Context.Resources.GetString(Resource.String.FeedTabTitle);
        }

        public static MainUserFeed NewInstance(int page)
        {
            Bundle args = new Bundle();
            args.PutInt(ARG_PAGE, page);
            MainUserFeed fragment = new MainUserFeed()
            {
                Arguments = args
            };
            return fragment;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);

            View view = inflater.Inflate(Resource.Layout.MainUserFeedFragment, container, false);
            TextView textView = view.FindViewById<TextView>(Resource.Id.textView);
            textView.SetText(Resource.String.FeedText);

            return view;
        }
    }
}