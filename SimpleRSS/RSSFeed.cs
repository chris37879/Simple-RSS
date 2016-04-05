using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.Xml.Linq;

namespace SimpleRSS
{
    class RSSFeed
    {
        public Uri URI { get; set; }
        public List<FeedItem> FeedItems { get; set; }

        private static readonly XName RSSElement = XName.Get("rss");
        private static readonly XName ItemElement = XName.Get("item");

        public RSSFeed(string url)
        {
            this.URI = new Uri(url);
        }

        public RSSFeed(Uri uri)
        {
            this.URI = uri;
        }

        public async Task<List<FeedItem>> RefreshFeed()
        {
            //TODO: Refresh the List of FeedItems
            HttpClient client = new HttpClient();

            Task<string> contentsTask = client.GetStringAsync(this.URI);
            string contents = await contentsTask;

            XDocument doc = XDocument.Parse(contents);

            List<FeedItem> items = new List<FeedItem>();
            foreach(XElement elem in doc.Element(RSSElement).Elements(ItemElement))
            {
                FeedItem item = new FeedItem(elem);
                items.Add(item);
            }

            this.FeedItems = items;
            return items;
        }
    }
}