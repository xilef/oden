using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using System.Collections.Generic;

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

            User user = ((MainUserActivity)Activity).mLoggedIn;
            int itemIndex = 0;
            List<Collection> userCollection = DBHandler.Instance.GetUserCollection(user.ID);

            List<CollectionItemList> userCollectionItem = DBHandler.Instance.GetCollectionItems(userCollection[itemIndex].ID);

            NowShowingListAdapter nowShowingAdapter = new NowShowingListAdapter(Activity, userCollectionItem);
            ListView nowShowingList = view.FindViewById<ListView>(Resource.Id.nowShowingListView);
            nowShowingList.Adapter = nowShowingAdapter;

            ComingSoonListAdapter comingSoonAdapter = new ComingSoonListAdapter(Activity, userCollectionItem);
            ListView comingSoonList = view.FindViewById<ListView>(Resource.Id.comingSoonListView);
            comingSoonList.Adapter = comingSoonAdapter;

            return view;
        }
    }
}