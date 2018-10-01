using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp.Core
{
    public class Core
    {
        public static async Task<Weather> GetWeather(/*string zipCode*/)
        {
            string key = "eedbac29d881f642f8ec65a25b4a3c50";
            string queryString = "http://api.openweathermap.org/data/2.5/weather?q=Tallinn&appid=" + key + "&units=metric";

            dynamic results = await DataService.GetDataFromService(queryString).ConfigureAwait(false);
            Weather weather = new Weather
            {
                Temperature = (string)results["main"]["temp"] + " °C"

            };
            return weather;
        }
    }
}
