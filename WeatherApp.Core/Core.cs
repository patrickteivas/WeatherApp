using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp.Core
{
    public class Core
    {
        public static async Task<Weather> GetWeather(string city)
        {
            const string key = "5f94823a419903c561ab816df86c2815";
            string queryString = "http://api.openweathermap.org/data/2.5/weather?q=" + city + "&appid=" + key + "&units=metric";

            dynamic results = await DataService.GetDataFromService(queryString).ConfigureAwait(false);
            Weather weather = new Weather
            {
                Temperature = (string)results["main"]["temp"] + " °C"
            };
            return weather;
        }
    }
}
