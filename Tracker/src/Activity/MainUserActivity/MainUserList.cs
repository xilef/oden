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

            List<UserCollectionsList> dbList = new List<UserCollectionsList>();

            for (int x = 0; x < 20; x++)
            {
                dbList.Add(new UserCollectionsList
                {
                    UserID = 1,
                    CollectionsListID = x
                });
            }

            TestAdapter test = new TestAdapter(Activity, dbList);
            ListView testList = view.FindViewById<ListView>(Resource.Id.testListView);
            testList.Adapter = test;

            return view;
        }
    }
}