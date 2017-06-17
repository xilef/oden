using Android.App;
using Android.Widget;
using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Tracker
{
    class DBHandler
    {
        private static volatile DBHandler instance;
        private static object syncRoot = new object();

        public static SQLiteAsyncConnection asyncConn;
        public static SQLiteConnection conn;

        public static DBHandler Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                        {
                            instance = new DBHandler();

                        }
                    }
                }

                return instance;
            }
        }

        private DBHandler()
        {
            var docsFolder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);
            var pathToDB = System.IO.Path.Combine(docsFolder, "db_sqlcompnet.db");

            asyncConn = new SQLiteAsyncConnection(pathToDB);
            conn = new SQLiteConnection(pathToDB);

            CreateDatabase();
        }

        private void CreateDatabase()
        {
            try
            {
                conn.CreateTable<Users>();
                conn.CreateTable<UserCollectionsList>();
                conn.CreateTable<CollectionsList>();
                conn.CreateTable<CollectionItemsList>();
            }
            catch (SQLiteException ex)
            {
                Toast.MakeText(Application.Context, ex.Message, ToastLength.Long).Show();
            }
        }

        public Users GetUser(string username, string password)
        {
            List<Users> user = conn.Query<Users>("select * from Users where UserName = ? and Password = ?", username, password);

            if (user.Count > 0)
            {
                return user[0];
            }
            else
            {
                return null;
            }
        }

        public async Task<int> AddUser(Users data)
        {
            try
            {
                var result = await asyncConn.InsertAsync(data);
                return result;
            }
            catch (SQLiteException ex)
            {
                Toast.MakeText(Application.Context, ex.Message, ToastLength.Long).Show();
                return -1;
            }
        }

        public async Task<List<UserCollectionsList>> GetUserCollections(int UserID)
        {
            try
            {
                List<UserCollectionsList> list = await asyncConn.QueryAsync<UserCollectionsList>("select * from UserCollectionsList where UserId = ?", UserID);
                return list;
            }
            catch (SQLiteException ex)
            {
                Toast.MakeText(Application.Context, ex.Message, ToastLength.Long).Show();
                return null;
            }
        }
    }
}