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
using Newtonsoft.Json;

namespace WeatherApp
{
    [Activity(Label = "ListView")]
    public class ListView : ListActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            //var selectedItemId = Intent.Extras.GetInt("Weather");
            List<Weather> fiveDaysForecast = JsonConvert.DeserializeObject<List<Weather>>(Intent.GetStringExtra("Weather"));
            ListAdapter = new CustomAdapter(this, fiveDaysForecast);
        }
    }
}