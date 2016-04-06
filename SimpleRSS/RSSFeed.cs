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
using Android.Net;
using System.IO;

namespace SimpleRSS
{
    public class RSSFeed
    {
        public System.Uri URI { get; set; }
        public List<FeedItem> FeedItems { get; set; } = new List<FeedItem>();
        
        public RSSFeed(string url)
        {
            this.URI = new System.Uri(url);
        }

        public RSSFeed(System.Uri uri)
        {
            this.URI = uri;
        }

        public async Task<List<FeedItem>> RefreshFeed()
        {
            ConnectivityManager connectivityManager = (ConnectivityManager)Application.Context.GetSystemService(Context.ConnectivityService);
            NetworkInfo activeConnection = connectivityManager.ActiveNetworkInfo;
            bool isOnline = (activeConnection != null) && activeConnection.IsConnected;
            string contents = "";

            var filePath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "feed.xml");
            if (isOnline)
            {
                HttpClient client = new HttpClient();
                contents = await client.GetStringAsync(this.URI);
                File.WriteAllText(filePath, contents);
            } else
            {
                contents = File.ReadAllText(filePath, Encoding.UTF8);
                if(contents.Length == 0)
                {
                    //TODO: There was no cached content, display an error.
                }
            }

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