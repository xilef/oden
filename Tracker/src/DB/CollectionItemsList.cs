using Android.OS;
using Java.Interop;

using Object = Java.Lang.Object;

namespace Tracker
{
    class CollectionItemsList : Object, IParcelable
    {
        public CollectionItemsList() { }

        public int CollectionsListID { get; set; }

        public string MovieName { get; set; }

        //public DateTime CreatedAt { get; set; }

        public override string ToString()
        {
            return string.Format("[Person: CollectionsListID={0}, MovieName={1}]", CollectionsListID, MovieName);
        }

        #region IParcelable implementation
        private static readonly GenericParcelableCreator<CollectionItemsList> _creator
            = new GenericParcelableCreator<CollectionItemsList>((parcel) => new CollectionItemsList(parcel));

        [ExportField("CREATOR")]
        public static GenericParcelableCreator<CollectionItemsList> GetCreator()
        {
            return _creator;
        }

        public int DescribeContents()
        {
            return 0;
        }

        public void WriteToParcel(Parcel output, ParcelableWriteFlags flags)
        {
            output.WriteInt(CollectionsListID);
            output.WriteString(MovieName);
        }

        public CollectionItemsList(Parcel input)
        {
            CollectionsListID = input.ReadInt();
            MovieName = input.ReadString();
        }
        #endregion
    }
}