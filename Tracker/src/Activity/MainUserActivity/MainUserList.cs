using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using System.Collections.Generic;

namespace Tracker
{
    class MainUserList : MainUserTabFragment
    {
        public MainUserList()
        {
            mTabTitle = Application.Context.Resources.GetString(Resource.String.ListTabTitle);
        }

        public static MainUserList NewInstance(int page)
        {
            Bundle args = new Bundle();
            args.PutInt(ARG_PAGE, page);
            MainUserList fragment = new MainUserList()
            {
                Arguments = args
            };
            return fragment;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);

            View view = inflater.Inflate(Resource.Layout.MainUserListFragment, container, false);

            User user = ((MainUserActivity)Activity).mLoggedIn;
            int itemIndex = 0;
            List<Collection> userCollection = DBHandler.Instance.GetUserCollection(user.ID);

            TextView listNameText = view.FindViewById<TextView>(Resource.Id.listNameText);
            listNameText.Text = userCollection[itemIndex].Name;

            List<CollectionItemList> userCollectionItem = DBHandler.Instance.GetCollectionItems(userCollection[itemIndex].ID);
            
            CollectionItemListAdapter adapter = new CollectionItemListAdapter(Activity, userCollectionItem);
            ListView collectionList = view.FindViewById<ListView>(Resource.Id.collectionItemListView);
            collectionList.Adapter = adapter;

            return view;
        }
    }
}