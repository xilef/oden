using Android.OS;
using Java.Interop;

using Object = Java.Lang.Object;

namespace Tracker
{
    class UserCollectionsList : Object, IParcelable
    {
        public UserCollectionsList() { }
       
        public int UserID { get; set; }

        public int CollectionsListID { get; set; }

        //public DateTime CreatedAt { get; set; }

        public override string ToString()
        {
            return string.Format("[Person: UserID={0}, CollectionsListID={1}]", UserID, CollectionsListID);
        }

        #region IParcelable implementation
        private static readonly GenericParcelableCreator<UserCollectionsList> _creator
            = new GenericParcelableCreator<UserCollectionsList>((parcel) => new UserCollectionsList(parcel));

        [ExportField("CREATOR")]
        public static GenericParcelableCreator<UserCollectionsList> GetCreator()
        {
            return _creator;
        }

        public int DescribeContents()
        {
            return 0;
        }

        public void WriteToParcel(Parcel output, ParcelableWriteFlags flags)
        {
            output.WriteInt(UserID);
            output.WriteInt(CollectionsListID);
        }

        public UserCollectionsList(Parcel input)
        {
            UserID = input.ReadInt();
            CollectionsListID = input.ReadInt();
        }
        #endregion
    }
}