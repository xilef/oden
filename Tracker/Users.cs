using Android.OS;
using Java.Lang;
using SQLite;
using System;

namespace Tracker
{
    public class Users : IParcelable
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string DisplayName { get; set; }

        public override string ToString()
        {
            return string.Format("[Person: ID={0}, FirstName={1}, LastName={2}, DisplayName={3}]", ID, UserName, Password, DisplayName);
        }

        //Parcelable stuff
        public int DescribeContents()
        {
            return 0;
        }

        public void WriteToParcel(Parcel output, ParcelableWriteFlags flags)
        {
            output.WriteString(UserName);
            output.WriteString(Password);
            output.WriteString(DisplayName);
        }

        public IntPtr Handle
        {
            get { throw new NotImplementedException(); }
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}