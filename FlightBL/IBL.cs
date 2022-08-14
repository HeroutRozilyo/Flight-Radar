using FlightModel;
using Microsoft.Graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightBL
{
    public interface IBL
    {
        Dictionary<string, List<FlightData>> getWebFlights(); //from the web
        //Task<Dictionary<string, List<FlightData>>> getWebFlights(); //from the web

        Dictionary<string, List<FlightData>> getFlights();//return all the data at the data base       
        FlightData getOneFlights( int val);//return one flights
        FlightData getFlightCode(string val);//return one flights
        bool addOneFlights( FlightData flight);
        bool removeOneFlights( FlightData flight);
        FlightM.Root getRootFlights(string key);
        void cleanDB();
    }
}
