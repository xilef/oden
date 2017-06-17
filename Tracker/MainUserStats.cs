using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
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

        new public static MainUserStats newInstance(int page)
        {
            Bundle args = new Bundle();
            args.PutInt(ARG_PAGE, page);
            MainUserStats fragment = new MainUserStats();
            fragment.Arguments = args;
            return fragment;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);

            View view = inflater.Inflate(Resource.Layout.MainUserFeedFragment, container, false);
            TextView textView = view.FindViewById<TextView>(Resource.Id.textView);
            textView.SetText(Resource.String.StatsText);

            return view;
        }
    }
}