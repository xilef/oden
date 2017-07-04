using System.Collections.Generic;
using Android.App;
using Android.Views;
using Android.Widget;

namespace Tracker
{
    class CollectionItemListAdapter : BaseAdapter
    {
        private List<CollectionItemList> _list;
        public List<CollectionItemList> List
        {
            get { return _list; }
            set
            {
                _list = value;
                NotifyDataSetChanged();
            }
        }
        Activity activity;

        public CollectionItemListAdapter(Activity ac)
        {
            activity = ac;
            _list = new List<CollectionItemList>();
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
                Resource.Layout.CollectionItemRow, parent, false);

            TextView titleText = view.FindViewById<TextView>(Resource.Id.Title);
            TextView descText = view.FindViewById<TextView>(Resource.Id.Description);
            ImageView thumb = view.FindViewById<ImageView>(Resource.Id.Thumbnail);

            titleText.Text = _list[position].MovieTitle;
            descText.Text = _list[position].MovieID.ToString();
            thumb.SetImageResource(Resource.Drawable.Icon);

            return view;
        }
    }
}