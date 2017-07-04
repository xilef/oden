using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using System.Collections.Generic;
using Android.Support.V4.Content;
using Android.Database;

namespace Tracker
{
    class MainUserFeed : MainUserTabFragment, Android.Support.V4.App.LoaderManager.ILoaderCallbacks
    {
        private NowShowingListAdapter nowShowingAdapter;
        private ComingSoonListAdapter comingSoonAdapter;

        private const int NOWSHOWING_LOADER_ID = 1;
        private const int COMINGSOON_LOADER_ID = 2;

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

            nowShowingAdapter = new NowShowingListAdapter(Activity);
            ListView nowShowingList = view.FindViewById<ListView>(Resource.Id.nowShowingListView);
            nowShowingList.Adapter = nowShowingAdapter;
            nowShowingList.ItemClick += OnListClicked;

            SetupListViews(nowShowingList);

            comingSoonAdapter = new ComingSoonListAdapter(Activity);
            ListView comingSoonList = view.FindViewById<ListView>(Resource.Id.comingSoonListView);
            comingSoonList.Adapter = comingSoonAdapter;
            comingSoonList.ItemClick += OnListClicked;

            SetupListViews(comingSoonList);

            LoaderManager.InitLoader(NOWSHOWING_LOADER_ID, null, this);
            LoaderManager.InitLoader(COMINGSOON_LOADER_ID, null, this);

            return view;
        }

        private void OnListClicked(object sender, AdapterView.ItemClickEventArgs e)
        {
            var intent = new Android.Content.Intent(Activity, typeof(ViewMovieActivity));

            MovieEntry selected = (MovieEntry)((ListView)sender).Adapter.GetItem(e.Position);

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

        private void SetupListViews(ListView list)
        {
            RelativeLayout.LayoutParams listParam = (RelativeLayout.LayoutParams)list.LayoutParameters;

            int anchor = listParam.GetRule(LayoutRules.Below);

            RelativeLayout.LayoutParams param = new RelativeLayout.LayoutParams(ViewGroup.LayoutParams.WrapContent, ViewGroup.LayoutParams.WrapContent);
            param.AddRule(LayoutRules.Below, anchor);
            param.AddRule(LayoutRules.CenterHorizontal);

            ProgressBar progress = new ProgressBar(Activity)
            {
                LayoutParameters = param,
                Indeterminate = true
            };

            list.EmptyView = progress;

            RelativeLayout parent = (RelativeLayout)list.Parent;
            parent.AddView(progress);
        }

        public Loader OnCreateLoader(int id, Bundle args)
        {
            Loader newLoader = null;
            switch (id)
            {
                case NOWSHOWING_LOADER_ID:
                    newLoader = new NowShowingLoader(Activity);
                    break;
                case COMINGSOON_LOADER_ID:
                    newLoader = new ComingSoonLoader(Activity);
                    break;
            }
            return newLoader;
        }

        public void OnLoaderReset(Loader loader)
        {
            switch (loader.Id)
            {
                case NOWSHOWING_LOADER_ID:
                    nowShowingAdapter.List = null;
                    break;
                case COMINGSOON_LOADER_ID:
                    comingSoonAdapter.List = null;
                    break;
            }
        }

        public void OnLoadFinished(Loader loader, Java.Lang.Object data)
        {
            ICursor cursor = Android.Runtime.Extensions.JavaCast<ICursor>(data);
            List<MovieEntry> list = new List<MovieEntry>();

            while (cursor.MoveToNext())
            {
                list.Add(new MovieEntry(cursor));
            }

            switch (loader.Id)
            {
                case NOWSHOWING_LOADER_ID:
                    nowShowingAdapter.List = list;
                    break;
                case COMINGSOON_LOADER_ID:
                    comingSoonAdapter.List = list;
                    break;
            }
        }
    }
}