using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightDL;
using FlightModel;

namespace FlightBL
{
    

    public class BL : IBL
    {
        

        private IDL DL = new DB();
        public AsynceAdapter DLAdapter = new AsynceAdapter();

        public Dictionary<string, List<FlightData>> getWebFlights()
        {
            Dictionary<string, List<FlightData>> to = null;
            try
            {
                to = DLAdapter.GetWebFlights();
            }
            catch (Exception e)
            {
                /////////////////////////
            }
            return to;
        }

        public Dictionary<string, List<FlightData>> getFlights()
        {
            return DL.getFlights();
        }

        public FlightData getOneFlights(string key, int val)
        {
            return DL.getOneFlights(key, val);
        }

        public bool addOneFlights(string key, FlightData flight)
        {
            if (DL.addOneFlights(key, flight) == true) return true;
            throw new Exception("the current flight allready exsis.");
        }

        public bool removeOneFlights(string key, FlightData flight)
        {
            if (DL.removeOneFlights(key, flight) == true) return true;
            throw new Exception("the current flight unexsis.");  //לבדוק ניסוח האנגלית בזריקה
        }
    }
}
