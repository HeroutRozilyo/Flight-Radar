using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightDL;
using FlightModel;

namespace FlightBL
{
    // להחליט אם צריך לעשות פקטורי או לא לשכבת DL/

    public class BL : IBL
    {
        #region singelton
        static readonly BL instance = new BL();
        static BL() { }
        public static BL Instance { get => instance; }
        #endregion

        private IDL DL = new DB();
        public AsynceAdapter DLAdapter = new AsynceAdapter();

        public Dictionary<string, List<FlightData>> getCurrentFlights()
        {
            Dictionary<string, List<FlightData>> to = null;
            try
            {
                to = DLAdapter.GetCurrentFlights();
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
