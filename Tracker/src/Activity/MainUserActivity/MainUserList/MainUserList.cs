using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using System.Collections.Generic;
using Android.Support.V4.Content;
using System;
using Android.Database;

namespace Tracker
{
    class MainUserList : MainUserTabFragment, Android.Support.V4.App.LoaderManager.ILoaderCallbacks
    {
        private CollectionItemListAdapter collectionItemListAdapter;

        private const int COLLECTIONITEMLIST_LOADER_ID = 1;

        private List<Collection> UserCollection;

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

            User user = ((MainUserActivity)Activity).LoggedIn;
            UserCollection = DBHandler.Instance.GetUserCollection(user.ID);

            TextView listNameText = view.FindViewById<TextView>(Resource.Id.listNameText);
            listNameText.Text = UserCollection[0].Name;
            
            collectionItemListAdapter = new CollectionItemListAdapter(Activity);
            ListView collectionList = view.FindViewById<ListView>(Resource.Id.collectionItemListView);
            collectionList.Adapter = collectionItemListAdapter;
            collectionList.ItemClick += OnListClicked;

            LoaderManager.InitLoader(COLLECTIONITEMLIST_LOADER_ID, null, this);

            return view;
        }

        private void OnListClicked(object sender, AdapterView.ItemClickEventArgs e)
        {
            var intent = new Android.Content.Intent(Activity, typeof(ViewMovieActivity));

            CollectionItemList selected = (CollectionItemList)((ListView)sender).Adapter.GetItem(e.Position);

            Bundle bundle = new Bundle();

            bundle.PutParcelable(ViewMovieActivity.ARG_USER, ((MainUserActivity)Activity).LoggedIn);
            bundle.PutInt(ViewMovieActivity.ARG_MOVIE, selected.MovieID);

            // set user collection list parcelable
            List<IParcelable> parcelList = new List<IParcelable>();
            foreach (Collection item in ((MainUserActivity)Activity).UserCollection)
            {
                parcelList.Add(item.ToBundle());
            }
            bundle.PutParcelableArrayList(ViewMovieActivity.ARG_COLLECTION, parcelList);

            intent.PutExtras(bundle);

            StartActivity(intent);
        }

        public Loader OnCreateLoader(int id, Bundle args)
        {
            CollectionItemListLoader loader = new CollectionItemListLoader(Activity, UserCollection[0].ID);
            DBHandler.Instance.SetCollectionItemListObserver(loader);
            return loader;
        }

        public void OnLoaderReset(Loader loader)
        {
            collectionItemListAdapter.List = null;
        }

        public void OnLoadFinished(Loader loader, Java.Lang.Object data)
        {
            ICursor cursor = Android.Runtime.Extensions.JavaCast<ICursor>(data);
            List<CollectionItemList> list = new List<CollectionItemList>();

            while (cursor.MoveToNext())
            {
                list.Add(new CollectionItemList(cursor));
            }

            collectionItemListAdapter.List = list;
        }
    }
}