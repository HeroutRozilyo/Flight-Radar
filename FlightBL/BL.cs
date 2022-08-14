using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataBaseEF1;
using FlightModel;
using Microsoft.Graph;

namespace FlightBL
{
    

    public class BL : IBL
    {


        private IDL DL = new IDL();
        public AsynceAdapter DLAdapter = new AsynceAdapter();

     //    public async Task<Dictionary<string, List<FlightData>>> getWebFlights()
       public Dictionary<string, List<FlightData>> getWebFlights()
        {
            Dictionary<string, List<FlightData>> to = null;
            try
            {
                to =  DLAdapter.GetWebFlights();
                //to = await DLAdapter.GetWebFlights();

            }
            catch (Exception e)
            {
                /////////////////////////
            }
            return  to;
        }

        public FlightM.Root getRootFlights(string key)
        {

            try
            {
                return DLAdapter.GetFlightData(key);
            }
            catch (Exception e)
            {
                /////////////////////////
            }
            return null;
        }

        public Dictionary<string, List<FlightData>> getFlights()
        {
            return DL.getFlights();
        }
         public FlightData getFlightCode(string val)
        {
            FlightData a = new FlightData();
            a = DL.getFlightCode(val);
            return a ;
        }

        public FlightData getOneFlights( int val)
        {
            return DL.getOneFlights( val);
        }

        public bool addOneFlights( FlightData flight)
        {
            if (DL.addOneFlights(flight) == true)
                return true;
            return false;
        }

        public bool removeOneFlights( FlightData flight)
        {
            if (DL.removeOneFlights( flight) == true) return true;
            throw new Exception("the current flight unexsis.");  //לבדוק ניסוח האנגלית בזריקה
        }

        public void cleanDB()
        {
            DL.cleanDB();
        }

    
    }
}
