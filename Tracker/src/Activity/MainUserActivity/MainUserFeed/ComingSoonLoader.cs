using Android.Content;
using Android.Database;

namespace Tracker
{
    class ComingSoonLoader : Android.Support.V4.Content.AsyncTaskLoader
    {
        private MatrixCursor ComingSoonList;

        public ComingSoonLoader(Context context) : base(context)
        {

        }

        public override Java.Lang.Object LoadInBackground()
        {
            return DBHandler.Instance.GetUpcomingMovieList();
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

            MatrixCursor currList = Android.Runtime.Extensions.JavaCast<MatrixCursor>(data);
            MatrixCursor oldList = ComingSoonList;
            ComingSoonList = currList;
            ComingSoonList.MoveToFirst();

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
            if (ComingSoonList != null)
            {
                DeliverResult(ComingSoonList);
            }

            if (TakeContentChanged())
            {
                ForceLoad();
            }
            else if (ComingSoonList == null)
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

            if (ComingSoonList != null)
            {
                ReleaseResources(ComingSoonList);
                ComingSoonList = null;
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
            MatrixCursor cursor = Android.Runtime.Extensions.JavaCast<MatrixCursor>(data);
            cursor.Close();
        }
    }
}