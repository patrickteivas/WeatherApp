using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using System;
using Android.Graphics;
using System.Net;
using System.Collections.Generic;
using Android.Content;
using Newtonsoft.Json;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;

namespace WeatherApp
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        SearchView searchBar;
        ProgressBar progessBar;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            AppCenter.Start("7bf1b67d-3306-49af-afe7-ef37acdb1814", typeof(Analytics), typeof(Crashes));

            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            progessBar = FindViewById<ProgressBar>(Resource.Id.progressBar1);
            searchBar = FindViewById<SearchView>(Resource.Id.searchView1);

            searchBar.QueryTextSubmit += SearchBar_QueryTextSubmit;
            FindViewById<Button>(Resource.Id.fiveDayForecast).Click += FiveDayForecast_Click;
        }

        private async void SearchBar_QueryTextSubmit(object sender, SearchView.QueryTextSubmitEventArgs e)
        {
            if (progessBar.Visibility == Android.Views.ViewStates.Invisible)
            {
                ImageView logo = FindViewById<ImageView>(Resource.Id.imageView1);

                FindViewById<TextView>(Resource.Id.Temp).Text = "";
                FindViewById<TextView>(Resource.Id.Pressure).Text = "";
                FindViewById<TextView>(Resource.Id.windSpeed).Text = "";

                logo.SetImageResource(0);

                progessBar.Visibility = Android.Views.ViewStates.Visible;
                searchBar.ClearFocus();

                var weather = await Core.Core.GetWeatherNow(searchBar.Query);
                if (weather != null)
                {
                    FindViewById<TextView>(Resource.Id.Temp).Text = weather.Temperature;
                    FindViewById<TextView>(Resource.Id.Pressure).Text = weather.Pressure;
                    FindViewById<TextView>(Resource.Id.windSpeed).Text = weather.WindSpeed;

                    SetIcon(weather.WeatherType, logo);
                }
                else
                {
                    FindViewById<TextView>(Resource.Id.Temp).Text = "Something went wrong.";
                }
                progessBar.Visibility = Android.Views.ViewStates.Invisible;
                FindViewById<Button>(Resource.Id.fiveDayForecast).Visibility = Android.Views.ViewStates.Visible;
            }
        }

        private async void FiveDayForecast_Click(object sender, System.EventArgs e)
        {
            if (progessBar.Visibility == Android.Views.ViewStates.Invisible && searchBar.Query != "")
            {
                progessBar.Visibility = Android.Views.ViewStates.Visible;

                List<Weather> fiveDaysForecast = await Core.Core.GetWeatherFiveDay(searchBar.Query);
                var listViewActivity = new Intent(this, typeof(ListView));

                listViewActivity.PutExtra("Weather", JsonConvert.SerializeObject(fiveDaysForecast));
                progessBar.Visibility = Android.Views.ViewStates.Invisible;

                StartActivity(listViewActivity);
            }
        }

        public static void SetIcon(string icon, ImageView image)
        {
            Bitmap imageBitmap = null;
            using (var webClient = new WebClient())
            {
                var imageBytes =
                    webClient.DownloadData(new Uri("http://openweathermap.org/img/w/" + icon + ".png"));
                imageBitmap = BitmapFactory.DecodeByteArray(imageBytes, 0, imageBytes.Length);
            }
            image.SetImageBitmap(imageBitmap);
        }
    }
}