using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Weather1
{
    public class WeatherAdapter
    {
        public async Task<WeatherModel.Main> WeatherAtLatLon(double lat, double lon)
        {
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3;
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

            using (var webClient = new System.Net.WebClient())
            {
                string url = $"https://api.openweathermap.org/data/2.5/weather?lat={lat}&lon={lon}&appid=786327f79f8c581abe6b976b2035f49d";

                var json = await webClient.DownloadStringTaskAsync(url);
                WeatherModel.Root root = JsonConvert.DeserializeObject<WeatherModel.Root>(json);

                return root.main;

            }
        }
    }
}
