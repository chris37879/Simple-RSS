using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Text;
using System.Globalization;

namespace SimpleRSS
{
    public class FeedItem
    {

        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Published { get; set; }
        public string Content { get; set; }

        private XNamespace nsContent = "http://purl.org/rss/1.0/modules/content/";

        public FeedItem(XElement elem)
        {
            this.Title = elem.Element("title").Value;
            this.Description = elem.Element("description").Value;
            try
            {
                this.Published = DateTime.ParseExact(elem.Element("pubDate").Value, "ddd', 'dd MMM yyyy HH':'mm':'ss zzz", CultureInfo.InvariantCulture);
            } catch
            {
                Console.WriteLine("Invalid Date Format: " + elem.Element("pubDate").Value);
            }
            this.Content = elem.Element(this.nsContent + "encoded").Value;
        }
    }
}