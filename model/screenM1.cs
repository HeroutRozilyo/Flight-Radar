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

        public List<FlightData> FlightInWeb()
        {
            Dictionary<string, List<FlightData>> temp = getWebFlights();
            List<FlightData> incom= temp["Incoming"];
            return incom;
        }

        public List<FlightData> FlightOutWeb()
        {
            Dictionary<string, List<FlightData>> temp = getWebFlights();
            List<FlightData> outgo = temp["Outgoing"];
            return outgo;
        }

        public FlightM.Root GetRootF(string key)
        {
           return bl.getRootFlights(key);
        }

        public bool addFlightDB(string key,FlightModel.FlightData f)
        {
           return bl.addOneFlights(key,f);
        }

    }
}
