using Android.App;
using Android.Graphics;
using Android.OS;
using Android.Widget;
using Java.IO;
using System.Net;

namespace Tracker
{
    [Activity(Label = "ViewMovieActivity")]
    public class ViewMovieActivity : Activity
    {
        public static string ARG_USER = "ARG_USER";

        public User LoggedIn { get; set; }

        public static string ARG_MOVIE = "ARG_MOVIE";

        public int MovieID { get; set; }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.ViewMovieActivity);

            // Create your application here

            Bundle bundle = Intent.Extras;
            LoggedIn = (User)bundle.GetParcelable(ARG_USER);
            MovieID = bundle.GetInt(ARG_MOVIE);

            TMDbLib.Objects.Movies.Movie movie = DBHandler.Instance.GetMovieDetails(MovieID);
            
            ImageView thumbnail = FindViewById<ImageView>(Resource.Id.ViewMovieThumbnail);
            thumbnail.SetImageBitmap(GetImageBitmapFromUrl(DBHandler.Instance.GetMovieImage(movie).ToString()));

            TextView title = FindViewById<TextView>(Resource.Id.ViewMovieTitle);
            title.Text = movie.Title;

            TextView description = FindViewById<TextView>(Resource.Id.ViewMovieDescription);
            description.Text = movie.Overview;
        }

        private Bitmap GetImageBitmapFromUrl(string url)
        {
            Bitmap image = null;

            using (var webClient = new WebClient())
            {
                var imageBytes = webClient.DownloadData(url);
                if (imageBytes != null && imageBytes.Length > 0)
                {
                    image = BitmapFactory.DecodeByteArray(imageBytes, 0, imageBytes.Length);
                }
            }
            return image;
        }
    }
}