using Android.Content;
using Android.Database;
using Android.OS;
using Java.Interop;

using Object = Java.Lang.Object;

namespace Tracker
{
    public class CollectionItemList : Object, IParcelable
    {
        #region Table declarations
        public static string TABLE_NAME = "CollectionItem";
        
        public static string KEY_COLLECTION_ID = "COLLECTIONID";
        public static string KEY_MOVIE_ID = "MOVIEID";
        public static string KEY_MOVIE_TITLE = "MOVIETITLE";

        public static string CREATE_TABLE = "CREATE TABLE " + TABLE_NAME + " (" +
                                        KEY_COLLECTION_ID + " INTEGER, " +
                                        KEY_MOVIE_ID + " INTEGER, " +
                                        KEY_MOVIE_TITLE + " TEXT)";

        public static string[] projection =
        {
            KEY_COLLECTION_ID,
            KEY_MOVIE_ID,
            KEY_MOVIE_TITLE
        };
        #endregion

        public long CollectionID { get; set; }

        public int MovieID { get; set; }

        public string MovieTitle { get; set; }

        public CollectionItemList() { }

        public CollectionItemList(ICursor cursor)
        {
            CollectionID = cursor.GetLong(0);
            MovieID = cursor.GetInt(1);
            MovieTitle = cursor.GetString(2);
        }

        public ContentValues GetContentValues()
        {
            ContentValues value = new ContentValues();
            
            value.Put(KEY_COLLECTION_ID, CollectionID);
            value.Put(KEY_MOVIE_ID, MovieID);
            value.Put(KEY_MOVIE_TITLE, MovieTitle);

            return value;
        }

        #region IParcelable implementation
        private static readonly GenericParcelableCreator<CollectionItemList> _creator
            = new GenericParcelableCreator<CollectionItemList>((parcel) => new CollectionItemList(parcel));

        [ExportField("CREATOR")]
        public static GenericParcelableCreator<CollectionItemList> GetCreator()
        {
            return _creator;
        }

        public int DescribeContents()
        {
            return 0;
        }

        public void WriteToParcel(Parcel output, ParcelableWriteFlags flags)
        {
            output.WriteLong(CollectionID);
            output.WriteInt(MovieID);
            output.WriteString(MovieTitle);
        }

        public CollectionItemList(Parcel input)
        {
            CollectionID = input.ReadLong();
            MovieID = input.ReadInt();
            MovieTitle = input.ReadString();
        }
        #endregion
    }
}