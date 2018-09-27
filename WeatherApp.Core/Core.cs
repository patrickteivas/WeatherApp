using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp.Core
{
    class Core
    {
        public static async Task<Weather> GetWeather(string zipCode)
        {
            const string key = "eedbac29d881f642f8ec65a25b4a3c50";
            string queryString = "http://api.openweathermap.org/data/2.5/weather?q=Tallinn&appid=" + key;

            dynamic results = await DataService.GetDataFromService(queryString).ConfigureAwait(false);
            Weather weather = new Weather();
            weather.Temperature = (string)results["main"]["temp"] + " C";
            return weather;
        }
    }
}
