using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;

namespace WeatherApp
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        SearchView searchBar;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            searchBar = FindViewById<SearchView>(Resource.Id.searchView1);

            searchBar.QueryTextSubmit += SearchBar_QueryTextSubmit;
        }

        private async void SearchBar_QueryTextSubmit(object sender, SearchView.QueryTextSubmitEventArgs e)
        {
            ProgressBar progessBar = FindViewById<ProgressBar>(Resource.Id.progressBar1);

            if (progessBar.Visibility == Android.Views.ViewStates.Invisible)
            {
                progessBar.Visibility = Android.Views.ViewStates.Visible;
                string city = searchBar.Query;
                searchBar.ClearFocus();
                var weather = await Core.Core.GetWeather(city);

                ImageView logo = FindViewById<ImageView>(Resource.Id.imageView1);


                FindViewById<TextView>(Resource.Id.Temp).Text = weather.Temperature;
                FindViewById<TextView>(Resource.Id.Pressure).Text = weather.Pressure;
                FindViewById<TextView>(Resource.Id.windSpeed).Text = weather.WindSpeed;

                if (weather.WeatherType == "01d") logo.SetImageResource(Resource.Drawable.sun);
                else if (weather.WeatherType == "02d") logo.SetImageResource(Resource.Drawable.cloudy);
                else if (weather.WeatherType == "03d") logo.SetImageResource(Resource.Drawable.clouds);
                else if (weather.WeatherType == "10d") logo.SetImageResource(Resource.Drawable.rain);
                else if (weather.WeatherType == "13d") logo.SetImageResource(Resource.Drawable.snow);

                progessBar.Visibility = Android.Views.ViewStates.Invisible;
            }
        }
    }
}