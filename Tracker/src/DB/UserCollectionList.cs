using Android.Content;
using Android.Database;
using Android.OS;
using Java.Interop;

using Object = Java.Lang.Object;

namespace Tracker
{
    public class UserCollectionList : Object, IParcelable
    {
        #region Table declarations
        public static string TABLE_NAME = "UserCollection";

        public static string KEY_USER_ID = "ID";
        public static string KEY_COLLECTION_ID = "CREATORID";

        public static string CREATE_TABLE = "CREATE TABLE " + TABLE_NAME + " (" +
                                        KEY_USER_ID + " INTEGER, " +
                                        KEY_COLLECTION_ID + " INTEGER)";

        public static string[] projection =
        {
            KEY_USER_ID,
            KEY_COLLECTION_ID
        };
        #endregion

        public long UserID { get; set; }

        public long CollectionID { get; set; }

        public UserCollectionList() { }

        public UserCollectionList(ICursor cursor)
        {
            UserID = cursor.GetLong(0);
            CollectionID = cursor.GetLong(1);
        }

        public ContentValues GetContentValues()
        {
            ContentValues value = new ContentValues();

            value.Put(KEY_USER_ID, UserID);
            value.Put(KEY_COLLECTION_ID, CollectionID);

            return value;
        }

        #region IParcelable implementation
        private static readonly GenericParcelableCreator<UserCollectionList> _creator
            = new GenericParcelableCreator<UserCollectionList>((parcel) => new UserCollectionList(parcel));

        [ExportField("CREATOR")]
        public static GenericParcelableCreator<UserCollectionList> GetCreator()
        {
            return _creator;
        }

        public int DescribeContents()
        {
            return 0;
        }

        public void WriteToParcel(Parcel output, ParcelableWriteFlags flags)
        {
            output.WriteLong(UserID);
            output.WriteLong(CollectionID);
        }

        public UserCollectionList(Parcel input)
        {
            UserID = input.ReadLong();
            CollectionID = input.ReadLong();
        }
        #endregion
    }
}