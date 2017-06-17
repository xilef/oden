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

        private DBHandler()
        {
            var docsFolder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);
            var pathToDB = System.IO.Path.Combine(docsFolder, "db_sqlcompnet.db");

            asyncConn = new SQLiteAsyncConnection(pathToDB);
            conn = new SQLiteConnection(pathToDB);

            createDatabase();
        }

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

        private void createDatabase()
        {
            try
            {
                conn.CreateTable<Users>();
            }
            catch (SQLiteException ex)
            {
                Toast.MakeText(Application.Context, ex.Message, ToastLength.Long).Show();
            }
        }

        public Users getUser(string username, string password)
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

        public async Task<int> addUser(Users data)
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
    }
}