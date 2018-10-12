using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace WeatherApp
{
    class CustomAdapter : BaseAdapter<string>
    {
        readonly List<Weather> items;
        readonly Activity context;

        public CustomAdapter(Activity context, List<Weather> items) : base()
        {
            this.context = context;
            this.items = items;
        }

        public override string this[int position]
        {
            get { return items[position].WeatherDate.ToString(); }
        }

        public override int Count
        {
            get { return items.Count; }
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View view = convertView;

            if (view == null)
                view = context.LayoutInflater.Inflate(Resource.Layout.CustomRow, null);

            view.FindViewById<TextView>(Resource.Id.minMaxTemp).Text = items[position].Temperature;
            DateTime unixTime = DateTimeOffset.FromUnixTimeSeconds(items[position].WeatherDate).DateTime.ToLocalTime();
            view.FindViewById<TextView>(Resource.Id.date).Text = unixTime.Day + "." + unixTime.Month;
            view.FindViewById<TextView>(Resource.Id.clock).Text = unixTime.Hour + ":" + unixTime.Minute;
            MainActivity.SetIcon(items[position].WeatherType, view.FindViewById<ImageView>(Resource.Id.icon));
            return view;
        }
    }
}