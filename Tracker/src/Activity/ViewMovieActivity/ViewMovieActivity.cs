using Android.App;
using Android.Graphics;
using Android.OS;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMDbLib.Objects.Movies;

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

        private List<CollectionItemList> UserCollectionItem { get; set; }

        private TMDbLib.Objects.Movies.Movie Movie { get; set; }

        private TextView MovieTitleText { get; set; }
        private TextView DescriptionText { get; set; }
        private ImageView ThumbnailImage { get; set; }

        private ProgressBar Bar { get; set; }

        private Button AddToListBtn { get; set; }

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

            AddToListBtn = FindViewById<Button>(Resource.Id.addToListBtn);
            AddToListBtn.Click += AddToListOnClick;

            MovieTitleText = FindViewById<TextView>(Resource.Id.ViewMovieTitle);
            DescriptionText = FindViewById<TextView>(Resource.Id.ViewMovieDescription);
            ThumbnailImage = FindViewById<ImageView>(Resource.Id.ViewMovieThumbnail);

            Bar = FindViewById<ProgressBar>(Resource.Id.viewMovieProgress);

            LoadMovieData();
        }

        private async void LoadMovieData()
        {
            Bitmap image = null;

            ShowProgessBar();
 
            await Task.Run(() => 
            {
                UserCollectionItem = DBHandler.Instance.GetCollectionItems(UserCollection[0].ID);
                Movie = TMDbHandler.Instance.GetMovieDetails(MovieID);
                image = Helper.GetImageBitmapFromUrl(TMDbHandler.Instance.GetMovieImage(Movie).ToString());
            });

            ThumbnailImage.SetImageBitmap(image);
            MovieTitleText.Text = Movie.Title.ToString();
            DescriptionText.Text = Movie.Overview.ToString();

            HideProgressBar();
        }

        private void AddToListOnClick(object s, EventArgs e)
        {
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

            ((Button)s).Enabled = false;
        }

        private void ShowProgessBar()
        {
            Bar.Visibility = Android.Views.ViewStates.Visible;

            MovieTitleText.Visibility = Android.Views.ViewStates.Invisible;
            DescriptionText.Visibility = Android.Views.ViewStates.Invisible;
            ThumbnailImage.Visibility = Android.Views.ViewStates.Invisible;

            AddToListBtn.Enabled = false;
        }

        private void HideProgressBar()
        {
            Bar.Visibility = Android.Views.ViewStates.Invisible;

            MovieTitleText.Visibility = Android.Views.ViewStates.Visible;
            DescriptionText.Visibility = Android.Views.ViewStates.Visible;
            ThumbnailImage.Visibility = Android.Views.ViewStates.Visible;
    
            foreach (CollectionItemList item in UserCollectionItem)
            {
                if (item.MovieID == MovieID)
                {
                    return;
                }
            }

            AddToListBtn.Enabled = true;
        }
    }
}