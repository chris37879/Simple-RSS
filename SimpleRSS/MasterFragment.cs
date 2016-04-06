using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace SimpleRSS
{
    public class MasterFragment : ListFragment
    {
        private RSSFeed feed;
        public RSSFeed Feed {
            get { return this.feed; }
            set
            {
                if(this.ListAdapter != null)
                { 
                    ((FeedItemAdapter)this.ListAdapter).Items = value.FeedItems;
                }
                this.feed = value;
            }
        }

        public MasterFragment() : base()
        {
            //Empty constructor to make fragment inflater happy.
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.MasterFragment, container, false);
            return view;
        }

        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);

            this.ListAdapter = new FeedItemAdapter(this.Activity, this.Feed.FeedItems);

            var detailLayout = this.Activity.FindViewById<View>(Resource.Id.detailLayout);
        }


        private class FeedItemAdapter : BaseAdapter<FeedItem>
        {
            private Activity Activity;
            public List<FeedItem> Items;

            public FeedItemAdapter(Activity activity, List<FeedItem> items)
            {
                this.Activity = activity;
                this.Items = items;
            }

            public override FeedItem this[int position]
            {
                get
                {
                    return this.Items.ElementAt(position);
                }
            }

            public override int Count
            {
                get
                {
                    return this.Items.Count();
                }
            }

            public override long GetItemId(int position)
            {
                return position;
            }

            public override View GetView(int position, View convertView, ViewGroup parent)
            {
                Console.WriteLine("GetView Called.");
                ViewHolder holder = null;
                var view = convertView;

                if (view != null)
                {
                    holder = view.Tag as ViewHolder;
                }

                if (holder == null)
                {
                    holder = new ViewHolder();
                    view = this.Activity.LayoutInflater.Inflate(Android.Resource.Layout.SimpleListItem1, null);
                    holder.Title = view.FindViewById<TextView>(Android.Resource.Id.Text1);
                    view.Tag = holder;
                }

                holder.Title.Text = this[position].Title;

                return view;
            }

            private class ViewHolder : Java.Lang.Object
            {
                public TextView Title;
            }
        }
    }
}