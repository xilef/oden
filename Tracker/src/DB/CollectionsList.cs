using Android.OS;
using Java.Interop;
using SQLite;

using Object = Java.Lang.Object;

namespace Tracker
{
    public class CollectionsList : Object, IParcelable
    {
        public CollectionsList() { }

        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        public int CreatorID { get; set; }

        //public DateTime CreatedAt { get; set; }

        public override string ToString()
        {
            return string.Format("[Person: ID={0}, CreatorID={1}]", ID, CreatorID);
        }

        #region IParcelable implementation
        private static readonly GenericParcelableCreator<CollectionsList> _creator
            = new GenericParcelableCreator<CollectionsList>((parcel) => new CollectionsList(parcel));

        [ExportField("CREATOR")]
        public static GenericParcelableCreator<CollectionsList> GetCreator()
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
            output.WriteInt(CreatorID);
        }

        public CollectionsList(Parcel input)
        {
            ID = input.ReadInt();
            CreatorID = input.ReadInt();
        }
        #endregion
    }
}