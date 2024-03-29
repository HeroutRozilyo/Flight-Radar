﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HebDates
{
    public class HebAdapter
    {
      
        /// <summary>
        /// holiday from web
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public async Task<string> IfHoliday(DateTime date)
        {
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3;

            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

            using (var webClient = new System.Net.WebClient())
            {
                var yyyy = date.ToString("yyyy");
                var mm = date.ToString("MM");
                var dd = date.ToString("dd");
                string url = $"https://www.hebcal.com./converter?cfg=json&date={yyyy}-{mm}-{dd}&g2h=1&strict=1";

                var json = await webClient.DownloadStringTaskAsync(url);
                HebModel.Root root = JsonConvert.DeserializeObject<HebModel.Root>(json);

                //If there an event that contains the word Erev
                foreach (var e in root.events)
                {
                    if (e.Contains("Erev"))
                        return e.ToString();
                }

               

            }

            return "no holiday";
        

        }


    }
}
