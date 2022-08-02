using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PFlight.model
{
    public class WeatherM
    {
        Weather.WeatherAdapter weather { get; set; }

        public WeatherM()
        {
            weather = new Weather.WeatherAdapter();
        }

        public async Task<Weather.WeatherModel.Main> LatLonWeather(double lat,double lon)
        {
           return await weather.WeatherAtLatLon(lat, lon);
        }
    }
}
