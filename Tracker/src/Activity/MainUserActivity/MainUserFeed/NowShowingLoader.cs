using Android.Content;
using Android.Database;

namespace Tracker
{
    class NowShowingLoader : Android.Support.V4.Content.AsyncTaskLoader
    {
        private MatrixCursor NowShowingList;

        public NowShowingLoader(Context context) : base(context)
        {

        }

        public override Java.Lang.Object LoadInBackground()
        {
            return TMDbHandler.Instance.GetNowShowingMovieList();
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
            MatrixCursor oldList = NowShowingList;
            NowShowingList = currList;
            NowShowingList.MoveToPosition(-1);

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
            if (NowShowingList != null)
            {
                DeliverResult(NowShowingList);
            }

            if (TakeContentChanged())
            {
                ForceLoad();
            }
            else if (NowShowingList == null)
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

            if (NowShowingList != null)
            {
                ReleaseResources(NowShowingList);
                NowShowingList = null;
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