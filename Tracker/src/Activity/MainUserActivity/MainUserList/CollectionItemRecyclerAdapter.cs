using System.Collections.Generic;
using Android.App;
using Android.Views;
using Android.Widget;
using Android.Support.V7.Widget;
using System;

namespace Tracker
{
    class CollectionItemRecyclerAdapter : RecyclerView.Adapter
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

        public event EventHandler<int> ItemClick;

        public class CollectionItemListHolder : RecyclerView.ViewHolder
        {
            public TextView TitleText { get; set; }
            public TextView DescText { get; set; }
            public ImageView Thumbnail { get; set; }

            public CollectionItemListHolder(View viewItem, Action<int> listener) : base(viewItem)
            {
                TitleText = viewItem.FindViewById<TextView>(Resource.Id.Title);
                DescText = viewItem.FindViewById<TextView>(Resource.Id.Description);
                Thumbnail = viewItem.FindViewById<ImageView>(Resource.Id.Thumbnail);

                viewItem.Click += (sender, e) => listener(Position);
            }
        }
  
        public CollectionItemRecyclerAdapter()
        {
            _list = new List<CollectionItemList>();
        }

        public override int ItemCount { get { return _list.Count; } }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View viewItem = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.CollectionItemRow, parent, false);

            CollectionItemListHolder vh = new CollectionItemListHolder(viewItem, OnClick);
            return vh;
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            CollectionItemListHolder vh = holder as CollectionItemListHolder;

            vh.TitleText.Text = _list[position].MovieTitle;
            vh.DescText.Text = _list[position].MovieID.ToString();
            vh.Thumbnail.SetImageResource(Resource.Drawable.Icon);
        }

        void OnClick(int position)
        {
            ItemClick?.Invoke(this, position);
        }
    }
}