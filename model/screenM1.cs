using FlightModel;
using Microsoft.Graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PFlight.model
{
    public class screenM1
    {
        FlightBL.IBL bl;

        public screenM1()
        {
            bl = new FlightBL.BL();
        }

      

        public Dictionary<string, List<FlightData>> getWebFlights()
        {
            return bl.getWebFlights();
        }




        public FlightM.Root GetRootF(string key)
        {
           return bl.getRootFlights(key);
        }

        public bool addFlightDB(FlightModel.FlightData f)
        {
           return bl.addOneFlights(f);
        }


        public Dictionary<string, List<FlightData>> getFlights()
        {
            return bl.getFlights();
        }

        public void cleanDB()
        {
            bl.cleanDB();
        }
        public FlightData getOneFlights(string code)
        {
            FlightData a = new FlightData();
            a = bl.getFlightCode(code);
            return a;

        }



    }
}
