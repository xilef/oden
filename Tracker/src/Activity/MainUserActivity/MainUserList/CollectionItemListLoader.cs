using Android.Content;
using Android.Database;

namespace Tracker
{
    class CollectionItemListLoader : Android.Support.V4.Content.AsyncTaskLoader
    {
        private ICursor collectionItemList;
        private long collectionID;

        public CollectionItemListLoader(Context context, long ID) : base(context)
        {
            collectionID = ID;
        }

        public override Java.Lang.Object LoadInBackground()
        {
            return (Java.Lang.Object)DBHandler.Instance.GetCollectionItemsCursor(collectionID);
        }

        public override void DeliverResult(Java.Lang.Object data)
        {
            if (IsReset)
            {
                if (data != null)
                {
                    ReleaseResources(data);
                }
            }

            ICursor currList = Android.Runtime.Extensions.JavaCast<ICursor>(data);
            ICursor oldList = collectionItemList;
            collectionItemList = currList;
            collectionItemList.MoveToPosition(-1);

            if (IsStarted)
            {
                base.DeliverResult(data);
            }

            if (oldList != null && oldList != currList)
            {
                ReleaseResources(data);
            }
        }

        protected override void OnStartLoading()
        {
            if (collectionItemList != null)
            {
                DeliverResult((Java.Lang.Object)collectionItemList);
            }

            if (TakeContentChanged())
            {
                ForceLoad();
            }
            else if (collectionItemList == null)
            {
                ForceLoad();
            }
        }

        protected override void OnStopLoading()
        {
            CancelLoad();
        }

        protected override void OnReset()
        {
            OnStopLoading();

            if (collectionItemList != null)
            {
                ReleaseResources((Java.Lang.Object)collectionItemList);
                collectionItemList = null;
            }
        }

        public override void OnCanceled(Java.Lang.Object data)
        {
            base.OnCanceled(data);
            ReleaseResources(data);
        }

        public override void ForceLoad()
        {
            base.ForceLoad();
        }

        private void ReleaseResources(Java.Lang.Object data)
        {
            ICursor cursor = Android.Runtime.Extensions.JavaCast<ICursor>(data);
            cursor.Close();
        }
    }
}