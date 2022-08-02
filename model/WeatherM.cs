using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PFlight.model
{
    public class WeatherM
    {
       
        Weather1.WeatherAdapter weather { get; set; }

        public WeatherM()
        {
            weather = new Weather1.WeatherAdapter();
        }

        public async Task<Weather1.WeatherModel.Main> LatLonWeather(double lat,double lon)
        {
           return await weather.WeatherAtLatLon(lat, lon);
        }
    }
}
