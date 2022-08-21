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
      

        Dictionary<string, List<FlightData>> getFlights();//return all the data at the data base       
       
        FlightData getFlightCode(string val);//return one flights
        bool addOneFlights( FlightData flight);//to the DB
        bool removeOneFlights( FlightData flight);//from the DB
        FlightM.Root getRootFlights(string key);//by FLightCOde
        void cleanDB();
    }
}
