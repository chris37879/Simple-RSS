using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Collections.Generic;
using Android.Util;

namespace SimpleRSS
{
    [Activity(Label = "SimpleRSS", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        private RSSFeed Feed;

        protected async override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            Console.WriteLine("OnCreate Ran.");

            this.Feed = new RSSFeed("http://feeds.feedburner.com/androidcentral?format=xml");

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            List<FeedItem> items = await this.Feed.RefreshFeed();
            foreach(var item in items)
            {
                Console.WriteLine(item.Title);
            }
        }
    }
}

