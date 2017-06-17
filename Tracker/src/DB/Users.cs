using Android.OS;
using Java.Interop;
using SQLite;

using Object = Java.Lang.Object;

namespace Tracker
{
    public class Users : Object, IParcelable
    {
        public Users() { }

        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string DisplayName { get; set; }

        public string Email { get; set; }

        //public DateTime CreatedAt { get; set; }

        public override string ToString()
        {
            return string.Format("[Person: ID={0}, UserName={1}, Password={2}, DisplayName={3}, Email={4}]", ID, UserName, Password, DisplayName, Email);
        }

        #region IParcelable implementation
        private static readonly GenericParcelableCreator<Users> _creator
            = new GenericParcelableCreator<Users>((parcel) => new Users(parcel));

        [ExportField ("CREATOR")]
        public static GenericParcelableCreator<Users> GetCreator()
        {
            return _creator;
        }

        public int DescribeContents()
        {
            return 0;
        }

        public void WriteToParcel(Parcel output, ParcelableWriteFlags flags)
        {
            output.WriteInt(ID);
            output.WriteString(UserName);
            output.WriteString(Password);
            output.WriteString(DisplayName);
            output.WriteString(Email);
            //output.WriteInt(CreatedAt);
        }

        public Users(Parcel input)
        {
            ID = input.ReadInt();
            UserName = input.ReadString();
            Password = input.ReadString();
            DisplayName = input.ReadString();
            Email = input.ReadString();
            //CreatedAt = input.ReadInt();
        }
        #endregion
    }
}