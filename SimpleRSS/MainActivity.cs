using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Collections.Generic;
using Android.Util;
using Android.Content.Res;

namespace SimpleRSS
{
    [Activity(Label = "SimpleRSS", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        private RSSFeed Feed;
        private MasterFragment MasterFragment;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            this.MasterFragment = this.FragmentManager.FindFragmentById<MasterFragment>(Resource.Id.masterFragment);

            this.Feed = new RSSFeed("http://feeds.feedburner.com/androidcentral?format=xml");
            this.MasterFragment.Feed = this.Feed;

            
        }

        protected override void OnResume()
        {
            base.OnResume();
            this.RefreshList();
        }

        private async void RefreshList()
        {
            //TODO: Show Loading Indicator
            await this.Feed.RefreshFeed();
            this.MasterFragment.Feed = this.Feed;
            ((BaseAdapter)this.MasterFragment.ListAdapter).NotifyDataSetChanged();
            Console.WriteLine("Notified that dataset changed.");
            //TODO: Dismiss Loading Indicator
        }
    }
}

