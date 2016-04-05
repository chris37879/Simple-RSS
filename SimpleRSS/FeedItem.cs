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
    class FeedItem
    {

        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Published { get; set; }
        public string Content { get; set; }

        private static readonly XName TitleElement = XName.Get("title");
        private static readonly XName DescriptionElement = XName.Get("description");
        private static readonly XName PublishDateElement = XName.Get("pubDate");
        private static readonly XName ContentElement = XName.Get("content:encoded");

        public FeedItem(XElement elem)
        {
            this.Title = elem.Element(TitleElement).Value;
            this.Description = elem.Element(DescriptionElement).Value;
            this.Published = DateTime.ParseExact(elem.Element(PublishDateElement).Value, "ddd MMM dd hh:mm:ss zzz yyyy", CultureInfo.InvariantCulture);
            this.Content = elem.Element(ContentElement).Value;
        }
    }
}