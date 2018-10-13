using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp.Core
{
    public class Core
    {
        const string key = "5f94823a419903c561ab816df86c2815";

        public static async Task<Weather> GetWeatherNow(string city)
        {
            string queryString = "http://api.openweathermap.org/data/2.5/weather?q=" + city + "&appid=" + key + "&units=metric";

            dynamic results = await DataService.GetDataFromService(queryString).ConfigureAwait(false);
            Weather weather = new Weather();
            try
            {
                weather = new Weather
                {
                    Temperature = (string)results["main"]["temp_min"] + "/" + (string)results["main"]["temp_max"] + " °C",
                    Pressure = (string)results["main"]["pressure"] + " hPa",
                    WindSpeed = (string)results["wind"]["speed"] + " m/s",
                    WeatherType = (string)results["weather"][0]["icon"]
                };
            }
            catch
            {
                return null;
            }

            return weather;
        }

        public static async Task<List<Weather>> GetWeatherFiveDay(string city)
        {
            string queryString = "http://api.openweathermap.org/data/2.5/forecast?q=" + city + "&appid=" + key + "&units=metric";

            dynamic results = await DataService.GetDataFromService(queryString).ConfigureAwait(false);

            List<Weather> fiveDayForecasts = new List<Weather>();
            Weather weather = null;
            try
            {
                for (int i = 0; i < (int)results["cnt"]; i++)
                {
                    weather = new Weather()
                    {
                        Temperature = (string)results["list"][i]["main"]["temp_min"] + "/" + (string)results["list"][i]["main"]["temp_max"] + " °C",
                        WeatherType = (string)results["list"][i]["weather"][0]["icon"],
                        WeatherDate = (int)results["list"][i]["dt"]
                    };
                    fiveDayForecasts.Add(weather);
                }
            }
            catch
            {
                return null;
            }

            return fiveDayForecasts;
        }
    }
}
