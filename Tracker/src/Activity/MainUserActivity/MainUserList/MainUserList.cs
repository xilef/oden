using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using System.Collections.Generic;
using Android.Support.V4.Content;
using System;
using Android.Database;
using Android.Support.V7.Widget;

namespace Tracker
{
    class MainUserList : MainUserTabFragment, Android.Support.V4.App.LoaderManager.ILoaderCallbacks
    {
        private CollectionItemRecyclerAdapter collectionItemRecyclerAdapter;

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

            collectionItemRecyclerAdapter = new CollectionItemRecyclerAdapter();
            collectionItemRecyclerAdapter.ItemClick += OnListClicked;

            RecyclerView collectionList = view.FindViewById<RecyclerView>(Resource.Id.collectionItemRecyclerView);
            LinearLayoutManager layout = new LinearLayoutManager(Activity);
            collectionList.SetLayoutManager(layout);
            collectionList.SetAdapter(collectionItemRecyclerAdapter);

            LoaderManager.InitLoader(COLLECTIONITEMLIST_LOADER_ID, null, this);

            return view;
        }

        private void OnListClicked(object sender, int position)
        {
            var intent = new Android.Content.Intent(Activity, typeof(ViewMovieActivity));

            CollectionItemList selected = (CollectionItemList)((ListView)sender).Adapter.GetItem(position);

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
            collectionItemRecyclerAdapter.List = null;
        }

        public void OnLoadFinished(Loader loader, Java.Lang.Object data)
        {
            ICursor cursor = Android.Runtime.Extensions.JavaCast<ICursor>(data);
            List<CollectionItemList> list = new List<CollectionItemList>();

            while (cursor.MoveToNext())
            {
                list.Add(new CollectionItemList(cursor));
            }

            collectionItemRecyclerAdapter.List = list;
        }
    }
}