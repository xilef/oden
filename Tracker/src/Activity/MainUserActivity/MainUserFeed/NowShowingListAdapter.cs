using System.Collections.Generic;
using Android.App;
using Android.Views;
using Android.Widget;

namespace Tracker
{
    class NowShowingListAdapter : BaseAdapter
    {
        // TODO: test list
        private List<MovieEntry> _list;
        public List<MovieEntry> List
        {
            get { return _list; }
            set
            {
                _list = value;
                NotifyDataSetChanged();
            }
        }
        Activity activity;

        public NowShowingListAdapter(Activity ac)
        {
            activity = ac;
            _list = new List<MovieEntry>();
        }

        public override int Count { get { return _list.Count; } }

        public override Java.Lang.Object GetItem(int position)
        {
            return _list[position];
        }

        public override long GetItemId(int position)
        {
            return 0L;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = convertView ?? activity.LayoutInflater.Inflate(
                Resource.Layout.NowShowingRow, parent, false);

            TextView titleText = view.FindViewById<TextView>(Resource.Id.Title);
            TextView descText = view.FindViewById<TextView>(Resource.Id.Description);
            ImageView thumb = view.FindViewById<ImageView>(Resource.Id.Thumbnail);

            titleText.Text = _list[position].Title.ToString();
            descText.Text = _list[position].MovieID.ToString();
            thumb.SetImageResource(Resource.Drawable.Icon);

            return view;
        }
    }
}