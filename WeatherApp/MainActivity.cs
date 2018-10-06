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
            string city = searchBar.Query;
            searchBar.ClearFocus();
            var weather = await Core.Core.GetWeather(city);

            FindViewById<TextView>(Resource.Id.City).Text = weather.City;
            FindViewById<TextView>(Resource.Id.minMaxTemp).Text = weather.Temperature;
            FindViewById<TextView>(Resource.Id.Pressure).Text = weather.Pressure;
            FindViewById<TextView>(Resource.Id.windSpeed).Text = weather.windSpeed;
        }
    }
}