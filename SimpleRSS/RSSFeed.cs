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
            //TODO: Cache the feed.
            HttpClient client = new HttpClient();

            string contents = await client.GetStringAsync(this.URI);

            XDocument doc = XDocument.Parse(contents);

            IEnumerable<XElement> rssItemsElems = doc.Element("rss").Element("channel").Elements("item");

            List<FeedItem> items = new List<FeedItem>();
            foreach(XElement elem in rssItemsElems)
            {
                FeedItem item = new FeedItem(elem);
                items.Add(item);
            }

            this.FeedItems = items;
            return items;
        }
    }
}