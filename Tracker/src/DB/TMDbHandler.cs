using TMDbLib.Client;
using Android.Database;
using TMDbLib.Objects.General;
using TMDbLib.Objects.Search;
using TMDbLib.Objects.Movies;

namespace Tracker
{
    class TMDbHandler
    {
        private static TMDbClient client;

        private static volatile TMDbHandler instance;
        private static object syncRoot = new object();

        public static TMDbHandler Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                        {
                            instance = new TMDbHandler();
                        }
                    }
                }

                return instance;
            }
        }

        private TMDbHandler()
        {
            client = new TMDbClient("784b65c9f328039fe5b4ad7bb4de2633");
            client.GetConfig();
        }

        public MatrixCursor GetUpcomingMovieList()
        {
            string[] columns = new string[] { "ID", "Title" };
            MatrixCursor cursor = new MatrixCursor(columns);

            SearchContainerWithDates<SearchMovie> results = client.GetMovieUpcomingListAsync().Result;

            foreach (SearchMovie result in results.Results)
            {
                MatrixCursor.RowBuilder builder = cursor.NewRow();
                builder.Add(result.Id);
                builder.Add(result.Title);
            }

            return cursor;
        }

        public MatrixCursor GetNowShowingMovieList()
        {
            string[] columns = new string[] { "ID", "Title" };
            MatrixCursor cursor = new MatrixCursor(columns);

            SearchContainerWithDates<SearchMovie> results = client.GetMovieNowPlayingListAsync().Result;

            foreach (SearchMovie result in results.Results)
            {
                MatrixCursor.RowBuilder builder = cursor.NewRow();
                builder.Add(result.Id);
                builder.Add(result.Title);
            }

            return cursor;
        }

        public Movie GetMovieDetails(int ID)
        {
            return client.GetMovieAsync(ID, MovieMethods.Images).Result;
        }

        // testing
        public System.Uri GetMovieImage(Movie movie)
        {
            System.Uri imageUri = null;
            foreach (string size in client.Config.Images.PosterSizes)
            {
                imageUri = client.GetImageUrl(size, movie.Images.Posters[0].FilePath);
            }

            return imageUri;
        }
    }
}