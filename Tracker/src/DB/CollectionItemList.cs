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

        public static string CREATE_TABLE = "CREATE TABLE " + TABLE_NAME + " (" +
                                        KEY_COLLECTION_ID + " INTEGER, " +
                                        KEY_MOVIE_ID + " TEXT)";

        public static string[] projection =
        {
            KEY_COLLECTION_ID,
            KEY_MOVIE_ID
        };
        #endregion

        public long CollectionID { get; set; }

        public string MovieID { get; set; }

        public CollectionItemList() { }

        public CollectionItemList(ICursor cursor)
        {
            CollectionID = cursor.GetLong(0);
            MovieID = cursor.GetString(1);
        }

        public ContentValues GetContentValues()
        {
            ContentValues value = new ContentValues();
            
            value.Put(KEY_COLLECTION_ID, CollectionID);
            value.Put(KEY_MOVIE_ID, MovieID);

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
            output.WriteString(MovieID);
        }

        public CollectionItemList(Parcel input)
        {
            CollectionID = input.ReadLong();
            MovieID = input.ReadString();
        }
        #endregion
    }
}