using Android.App;
using Android.OS;
using Android.Widget;
using System;
using System.Collections.Generic;

namespace Tracker
{
    [Activity(Label = "ViewMovieActivity")]
    public class ViewMovieActivity : Activity
    {
        public static string ARG_USER = "ARG_USER";

        public User LoggedIn { get; set; }

        public static string ARG_MOVIE = "ARG_MOVIE";

        public int MovieID { get; set; }

        public static string ARG_COLLECTION = "ARG_COLLECTION";

        public List<Collection> UserCollection { get; private set; }

        private TMDbLib.Objects.Movies.Movie Movie;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.ViewMovieActivity);

            Bundle bundle = Intent.Extras;
            LoggedIn = (User)bundle.GetParcelable(ARG_USER);
            MovieID = bundle.GetInt(ARG_MOVIE);

            // get user collection list parcelable
            UserCollection = new List<Collection>();
            var parcelList = bundle.GetParcelableArrayList(ARG_COLLECTION);

            foreach (Bundle listBundle in parcelList)
            {
                UserCollection.Add(new Collection(listBundle));
            }

            Movie = TMDbHandler.Instance.GetMovieDetails(MovieID);
            
            ImageView thumbnail = FindViewById<ImageView>(Resource.Id.ViewMovieThumbnail);
            thumbnail.SetImageBitmap(Helper.GetImageBitmapFromUrl(TMDbHandler.Instance.GetMovieImage(Movie).ToString()));

            TextView title = FindViewById<TextView>(Resource.Id.ViewMovieTitle);
            title.Text = Movie.Title;

            TextView description = FindViewById<TextView>(Resource.Id.ViewMovieDescription);
            description.Text = Movie.Overview;

            Button addToListBtn = FindViewById<Button>(Resource.Id.addToListBtn);
            addToListBtn.Click += AddToListOnClick;
        }

        private void AddToListOnClick(object s, EventArgs e)
        {
            List<CollectionItemList> list = DBHandler.Instance.GetCollectionItems(UserCollection[0].ID);

            foreach (CollectionItemList item in list)
            {
                if (item.MovieID == MovieID)
                {
                    Toast.MakeText(this, "Already added!", ToastLength.Long).Show();
                    return;
                }
            }

            CollectionItemList listItem = new CollectionItemList
            {
                CollectionID = UserCollection[0].ID,
                MovieID = MovieID,
                MovieTitle = Movie.Title
            };

            if (DBHandler.Instance.AddCollectionItem(listItem))
            {
                Toast.MakeText(this, "Added to collection!", ToastLength.Long).Show();
            }
            else
            {
                Toast.MakeText(this, "Failed to add to collection!", ToastLength.Long).Show();
            }
        }
    }
}