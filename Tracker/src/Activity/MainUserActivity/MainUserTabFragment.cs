using Android.Support.V4.App;
using Android.OS;
using Android.Views;

using Activity = Android.App.Activity;

namespace Tracker
{
    public class MainUserTabFragment : Fragment
    {
        public static string ARG_PAGE = "ARG_PAGE";
        
        public string mTabTitle { get; set; }
        public int mPage { get; set; }

        public MainUserTabFragment() { }

        public static MainUserTabFragment newInstance(int page)
        {
            Bundle args = new Bundle();
            args.PutInt(ARG_PAGE, page);
            MainUserTabFragment fragment = new MainUserTabFragment();
            fragment.Arguments = args;
            return fragment;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);

            View view = inflater.Inflate(Resource.Layout.MainUserTab, container, false);
            return view;
        }
    }
}