﻿using FlightModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightDL
{
    public interface IDL
    {
        Dictionary<string, List<FlightData>> getFlights();//return all the data at the data base       
        FlightData getOneFlights(string key, int val);//return one flights
        bool addOneFlights(string key, FlightData flight);
        bool removeOneFlights(string key, FlightData flight);
    }
}
