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
using Android.Webkit;

namespace SimpleRSS
{
    public class DetailFragment : Fragment
    {
        public static DetailFragment NewInstance(string content)
        {
            var fragment = new DetailFragment { Arguments = new Bundle() };
            fragment.Arguments.PutString("content", content);
            return fragment;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.DetailFragment, container, false);
            WebView webView = view.FindViewById<WebView>(Resource.Id.webView);

            string html = @"
            <!DOCTYPE html>
            <html>
                <head>
                    <style>
                    img { width: 100% !important; }
                    </style>
                </head>
                <body>";

            html += Arguments.GetString("content");

            html += @"
            </body>
            </html>";

            webView.LoadData(html, "text/html; charset=utf-8", "UTF-8");

            return view;
        }
    }
}