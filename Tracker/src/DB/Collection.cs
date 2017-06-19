using Android.Content;
using Android.Database;
using Android.OS;
using Java.Interop;

using Object = Java.Lang.Object;

namespace Tracker
{
    public class Collection : Object, IParcelable
    {
        #region Table declarations
        public static string TABLE_NAME = "Collection";

        public static string KEY_ID = "ID";
        public static string KEY_NAME = "NAME";
        public static string KEY_CREATOR_ID = "CREATORID";

        public static string CREATE_TABLE = "CREATE TABLE " + TABLE_NAME + " (" +
                                        KEY_ID + " INTEGER PRIMARY KEY, " +
                                        KEY_NAME + " TEXT, " +
                                        KEY_CREATOR_ID + " INTEGER)";

        public static string[] projection =
        {
            KEY_ID,
            KEY_NAME,
            KEY_CREATOR_ID
        };
        #endregion

        public long ID { get; set; }

        public string Name { get; set; }

        public long CreatorID { get; set; }

        public Collection() { }

        public Collection(ICursor cursor)
        {
            ID = cursor.GetLong(0);
            Name = cursor.GetString(1);
            CreatorID = cursor.GetLong(2);
        }

        public ContentValues GetContentValues()
        {
            ContentValues value = new ContentValues();
            
            value.Put(KEY_NAME, Name);
            value.Put(KEY_CREATOR_ID, CreatorID);

            return value;
        }

        #region IParcelable implementation
        private static readonly GenericParcelableCreator<Collection> _creator
            = new GenericParcelableCreator<Collection>((parcel) => new Collection(parcel));

        [ExportField("CREATOR")]
        public static GenericParcelableCreator<Collection> GetCreator()
        {
            return _creator;
        }

        public int DescribeContents()
        {
            return 0;
        }

        public void WriteToParcel(Parcel output, ParcelableWriteFlags flags)
        {
            output.WriteLong(ID);
            output.WriteString(Name);
            output.WriteLong(CreatorID);
        }

        public Collection(Parcel input)
        {
            ID = input.ReadLong();
            Name = input.ReadString();
            CreatorID = input.ReadLong();
        }
        #endregion
    }
}