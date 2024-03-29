﻿using FlightModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseEF1
{
   public class AsynceAdapter
    {
        private const string AllURL = @"https://data-cloud.flightradar24.com/zones/fcgi/feed.js?faa=1&bounds=38.805%2C24.785%2C29.014%2C40.505&satellite=1&mlat=1&flarm=1&adsb=1&gnd=1&air=1&vehicles=1&estimated=1&maxage=14400&gliders=1&stats=1";
        private const string FlightURL = @"https://data-live.flightradar24.com/clickhandler/?version=1.5&flight=";



        //return the all the flights.
        public Dictionary<string, List<FlightData>> GetWebFlights()
        {

            Dictionary<string, List<FlightData>> Result = new Dictionary<string, List<FlightData>>(); // return to BL

            JObject AllFlightData = null;


            List<FlightData> Incoming = new List<FlightData>();
            List<FlightData> Outgoing = new List<FlightData>();

            using (var webClient = new System.Net.WebClient())
            {
                HelperClass Helper = new HelperClass();
                var json = webClient.DownloadString(AllURL);
                AllFlightData = JObject.Parse(json);

                try
                {
                    //move on all the flights
                    foreach (var item in AllFlightData)
                    {
                        var key = item.Key;
                        if (key == "full_count" || key == "version") continue;
                        if (item.Value[11].ToString() == "TLV")
                        {
                            Outgoing.Add(new FlightData
                            {
                                Id = -1,
                                Source = item.Value[11].ToString(),
                                Destination = item.Value[12].ToString(),
                                SourceId = key,
                                Long = Convert.ToDouble(item.Value[2]),
                                Lat = Convert.ToDouble(item.Value[1]),
                                DataAndTime = Helper.GetDateTimeFromEpohc(Convert.ToDouble(item.Value[10])),
                                FlightCode = item.Value[13].ToString()

                            });

                        }
                        if (item.Value[12].ToString() == "TLV")
                        {
                            Incoming.Add(new FlightData
                            {
                                Id = -1,
                                Source = item.Value[11].ToString(),
                                Destination = item.Value[12].ToString(),
                                SourceId = key,
                                Long = Convert.ToDouble(item.Value[2]),
                                Lat = Convert.ToDouble(item.Value[1]),
                                DataAndTime = Helper.GetDateTimeFromEpohc(Convert.ToDouble(item.Value[10])),
                                FlightCode = item.Value[13].ToString()

                            });

                        }
                    }
                }
                catch (Exception e) { Debug.Print(e.Message); }

            }

            Result.Add("Incoming", Incoming);
            Result.Add("Outgoing", Outgoing);
            return Result;
        }

        /// <summary>
        /// return root according Code FLight
        /// </summary>
        /// <param name="key"></param>
        /// <returns> root</returns>
        public FlightM.Root GetFlightData(string key)
        {
            var CurrentUrl = FlightURL + key;

            FlightM.Root root = null;

            using (var webClient = new System.Net.WebClient())
            {
                var json = webClient.DownloadString(CurrentUrl);
                try
                {

                    var v = JsonConvert.DeserializeObject<FlightM.Root>(json);
                    root = (FlightM.Root)v;
                }
                catch (Exception e)
                {
                    int a = 0;
                }
            }
            return root;
        }
    }
}
