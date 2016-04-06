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
using Android.Content.Res;

namespace SimpleRSS
{
    public class MasterFragment : ListFragment
    {
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

        private bool IsTablet = false;
        private RSSFeed feed;

        public MasterFragment() : base()
        {
            //Empty constructor
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

            this.IsTablet = this.Activity.FindViewById(Resource.Id.detailFragment) != null;

            this.ListAdapter = new FeedItemAdapter(this.Activity, this.Feed.FeedItems);

            if (this.IsTablet)
            {
                this.ListView.ChoiceMode = ChoiceMode.Single;
            }
        }

        public override void OnListItemClick(ListView l, View v, int position, long id)
        {
            base.OnListItemClick(l, v, position, id);
            this.ShowDetail(position);
        }

        private void ShowDetail(int position)
        {
            if (this.IsTablet)
            {
                this.ListView.SetItemChecked(position, true);
                var detailFragment = DetailFragment.NewInstance(this.Feed.FeedItems[position].Content);
                var fragmentTransaction = FragmentManager.BeginTransaction();
                fragmentTransaction.Replace(Resource.Id.detailFragment, detailFragment);
                fragmentTransaction.SetTransition(FragmentTransit.FragmentFade);
                fragmentTransaction.Commit();
            } else
            {
                var intent = new Intent();
                intent.SetClass(Activity, typeof(DetailActivity));
                intent.PutExtra("content", this.Feed.FeedItems[position].Content);
                this.StartActivity(intent);
            }
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