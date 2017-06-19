using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.Provider;
using Android.Views;
using Android.Widget;

namespace Tracker
{
    class TestAdapter : BaseAdapter
    {
        List<CollectionItemList> list;
        Activity activity;

        public TestAdapter(Activity ac, List<CollectionItemList> dbList)
        {
            activity = ac;

            list = dbList;
        }

        public override int Count { get { return list.Count; } }

        public override Java.Lang.Object GetItem(int position)
        {
            return null;
        }

        public override long GetItemId(int position)
        {
            return 0L;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = convertView ?? activity.LayoutInflater.Inflate(
                Resource.Layout.testListRow, parent, false);

            TextView titleText = view.FindViewById<TextView>(Resource.Id.Title);
            TextView descText = view.FindViewById<TextView>(Resource.Id.Description);
            ImageView thumb = view.FindViewById<ImageView>(Resource.Id.Thumbnail);

            titleText.Text = list[position].MovieID.ToString();
            descText.Text = list[position].CollectionID.ToString();
            thumb.SetImageResource(Resource.Drawable.Icon);

            return view;
        }
    }
}