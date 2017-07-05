using System.Collections.Generic;

using Android.App;
using Android.Content;
using Android.Widget;
using Android.Database.Sqlite;
using Android.Database;

namespace Tracker
{
    class DBHandler : SQLiteOpenHelper
    {
        private SQLiteDatabase mDB;

        private static int DATABASE_VERSION = 1;
        private static string DATABASE_NAME = "Tracker.db";

        private static volatile DBHandler instance;
        private static object syncRoot = new object();

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
                            instance = new DBHandler(Application.Context);
                        }
                    }
                }

                return instance;
            }
        }

        private List<CollectionItemListLoader> CollectionItemListObserver;

        private DBHandler(Context context) : base(context, DATABASE_NAME, null, DATABASE_VERSION)
        {
            mDB = WritableDatabase;

            CollectionItemListObserver = new List<CollectionItemListLoader>();
        }

        public override void OnCreate(SQLiteDatabase db)
        {
            try
            {
                db.ExecSQL(User.CREATE_TABLE);
                db.ExecSQL(Collection.CREATE_TABLE);
                db.ExecSQL(UserCollectionList.CREATE_TABLE);
                db.ExecSQL(CollectionItemList.CREATE_TABLE);
            }
            catch (SQLException ex)
            {
                Toast.MakeText(Application.Context, ex.Message, ToastLength.Long).Show();
            }
        }

        public override void OnUpgrade(SQLiteDatabase db, int oldVersion, int newVersion)
        {
            db.ExecSQL("DROP TABLE IF EXISTS " + User.TABLE_NAME);
            db.ExecSQL("DROP TABLE IF EXISTS " + Collection.TABLE_NAME);
            db.ExecSQL("DROP TABLE IF EXISTS " + UserCollectionList.TABLE_NAME);
            db.ExecSQL("DROP TABLE IF EXISTS " + CollectionItemList.TABLE_NAME);

            OnCreate(db);
        }

        public long AddUser(User newUser)
        {
            try
            {
                long rowNum = mDB.InsertOrThrow(User.TABLE_NAME, null, newUser.GetContentValues());
                return rowNum;
            }
            catch (SQLException ex)
            {
                Toast.MakeText(Application.Context, ex.Message, ToastLength.Long).Show();
                return -1;
            }
        }

        public User GetUser(string username, string password)
        {
            string selection = User.KEY_USERNAME + " = ? AND " +
                                User.KEY_PASSWORD + " = ?";
            string[] selectionArg = new string[]
            {
                username,
                password
            };

            ICursor cursor = mDB.Query(User.TABLE_NAME, User.projection, selection, selectionArg, null, null, null);

            User registeredUser = null;
            if (cursor.MoveToNext())
            {
                registeredUser = new User(cursor);
            }

            return registeredUser;
        }

        public long AddCollection(Collection newCollection)
        {
            try
            {
                long rowNum = mDB.InsertOrThrow(Collection.TABLE_NAME, null, newCollection.GetContentValues());
                return rowNum;
            }
            catch (SQLException ex)
            {
                Toast.MakeText(Application.Context, ex.Message, ToastLength.Long).Show();
                return -1;
            }
        }
        
        public bool AddUserCollection(UserCollectionList newList)
        {
            try
            {
                long rowNum = mDB.InsertOrThrow(UserCollectionList.TABLE_NAME, null, newList.GetContentValues());
                if (rowNum > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (SQLException ex)
            {
                Toast.MakeText(Application.Context, ex.Message, ToastLength.Long).Show();
                return false;
            }
        }

        public List<Collection> GetUserCollection(long userID)
        {
            string selection = "SELECT * FROM " + Collection.TABLE_NAME + " C INNER JOIN " +
                                                UserCollectionList.TABLE_NAME + " UC ON UC." + UserCollectionList.KEY_COLLECTION_ID + " = C." + Collection.KEY_ID +
                                                " WHERE UC." + UserCollectionList.KEY_USER_ID + " = ?";

            string[] selectionArg = new string[]
            {
                userID.ToString()
            };

            ICursor cursor = mDB.RawQuery(selection, selectionArg);

            List<Collection> userCollection = new List<Collection>();
            while (cursor.MoveToNext())
            {
                userCollection.Add(new Collection(cursor));
            }

            return userCollection;
        }

        public void SetCollectionItemListObserver(CollectionItemListLoader loader)
        {
            CollectionItemListObserver.Add(loader);
        }

        public bool AddCollectionItem(CollectionItemList newList)
        {
            try
            {
                long rowNum = mDB.InsertOrThrow(CollectionItemList.TABLE_NAME, null, newList.GetContentValues());
                if (rowNum > 0)
                {
                    if (CollectionItemListObserver != null)
                    {
                        foreach(CollectionItemListLoader observer in CollectionItemListObserver)
                        {
                            observer.OnContentChanged();
                        }
                    }
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (SQLException ex)
            {
                Toast.MakeText(Application.Context, ex.Message, ToastLength.Long).Show();
                return false;
            }
        }

        public bool RemoveCollectionItem(CollectionItemList list)
        {
            string selection = CollectionItemList.KEY_COLLECTION_ID + " = ? AND " +
                                CollectionItemList.KEY_MOVIE_ID + " = ?";
            string[] selectionArg = new string[]
            {
                list.CollectionID.ToString(),
                list.MovieID.ToString()
            };
            try
            {
                int deleted = mDB.Delete(CollectionItemList.TABLE_NAME, selection, selectionArg);

                if (deleted > 0)
                {
                    if (CollectionItemListObserver != null)
                    {
                        foreach (CollectionItemListLoader observer in CollectionItemListObserver)
                        {
                            observer.OnContentChanged();
                        }
                    }
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (SQLException ex)
            {
                Toast.MakeText(Application.Context, ex.Message, ToastLength.Long).Show();
                return false;
            }
        }

        public List<CollectionItemList> GetCollectionItems(long collectionID)
        {
            ICursor cursor = GetCollectionItemsCursor(collectionID);

            List<CollectionItemList> list = new List<CollectionItemList>();
            while (cursor.MoveToNext())
            {
                list.Add(new CollectionItemList(cursor));
            }

            return list;
        }

        public ICursor GetCollectionItemsCursor(long collectionID)
        {
            string selection = CollectionItemList.KEY_COLLECTION_ID + " = ?";

            string[] selectionArg = new string[]
            {
                collectionID.ToString()
            };

            return mDB.Query(CollectionItemList.TABLE_NAME, CollectionItemList.projection, selection, selectionArg, null, null, null);
        }
    }
}