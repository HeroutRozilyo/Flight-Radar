using FlightModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PFlight.model
{
   
    internal class ListFlightM
    {
        private FlightBL.IBL bl;
        public ListFlightM()
        {
            bl = new FlightBL.BL();
        }
        public Dictionary<string, List<FlightData>> getFlights()
        {
            return bl.getFlights();
        }
    }
}
