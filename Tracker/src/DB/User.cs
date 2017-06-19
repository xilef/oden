using Android.Content;
using Android.Database;
using Android.OS;
using Java.Interop;

using Object = Java.Lang.Object;

namespace Tracker
{
    public class User : Object, IParcelable
    {
        #region Table declarations
        public static string TABLE_NAME = "User";
    
        public static string KEY_ID = "ID";
        public static string KEY_USERNAME = "USERNAME";
        public static string KEY_PASSWORD = "PASSWORD";
        public static string KEY_DISPLAYNAME = "DISPLAYNAME";
        public static string KEY_EMAIL = "EMAIL";

        public static string CREATE_TABLE = "CREATE TABLE " + TABLE_NAME + " (" +
                                        KEY_ID + " INTEGER PRIMARY KEY, " +
                                        KEY_USERNAME + " TEXT, " +
                                        KEY_PASSWORD + " TEXT, "+
                                        KEY_DISPLAYNAME + " TEXT, " +
                                        KEY_EMAIL + " TEXT)";

        public static string[] projection =
        {
            KEY_ID,
            KEY_USERNAME,
            KEY_DISPLAYNAME,
            KEY_EMAIL
        };
        #endregion

        public long ID { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string DisplayName { get; set; }

        public string Email { get; set; }

        public User() { }

        public User(ICursor cursor)
        {
            ID = cursor.GetLong(0);
            Username = cursor.GetString(1);
            DisplayName = cursor.GetString(2);
            Email = cursor.GetString(3);
        }

        public ContentValues GetContentValues()
        {
            ContentValues value = new ContentValues();

            value.Put(KEY_USERNAME, Username);
            value.Put(KEY_PASSWORD, Password);
            value.Put(KEY_DISPLAYNAME, DisplayName);
            value.Put(KEY_EMAIL, Email);

            return value;
        }

        #region IParcelable implementation
        private static readonly GenericParcelableCreator<User> _creator
            = new GenericParcelableCreator<User>((parcel) => new User(parcel));

        [ExportField("CREATOR")]
        public static GenericParcelableCreator<User> GetCreator()
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
            output.WriteString(Username);
            output.WriteString(Password);
            output.WriteString(DisplayName);
            output.WriteString(Email);
        }

        public User(Parcel input)
        {
            ID = input.ReadLong();
            Username = input.ReadString();
            Password = input.ReadString();
            DisplayName = input.ReadString();
            Email = input.ReadString();
        }
        #endregion
    }
}