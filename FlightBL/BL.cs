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

        #region variable
        private IDL DL = new IDL();
        public AsynceAdapter DLAdapter = new AsynceAdapter();
        #endregion

        #region Get FLight

        /// <summary>
        /// every 10 second
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, List<FlightData>> getWebFlights()
        {
            Dictionary<string, List<FlightData>> to = null;
            try
            {
                to =  DLAdapter.GetWebFlights();
              

            }
            catch (Exception e)
            {
               
            }
            return  to;
        }
        /// <summary>
        /// to get flight data (root)
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
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

        /// <summary>
        /// return all the flights in the Data Base
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, List<FlightData>> getFlights()
        {
            return DL.getFlights();
        }

        /// <summary>
        /// return flight by code
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
         public FlightData getFlightCode(string val)
        {
            FlightData a = new FlightData();
            a = DL.getFlightCode(val);
            return a ;
        }

        #endregion

        #region add,remove,clean

        /// <summary>
        /// add spesific flight to dataBase
        /// </summary>
        /// <param name="flight"></param>
        /// <returns></returns>
        public bool addOneFlights( FlightData flight)
        {
            if (DL.addOneFlights(flight) == true)
                return true;
            return false;
        }

        /// <summary>
        /// delete flight from dataBase
        /// </summary>
        /// <param name="flight"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public bool removeOneFlights( FlightData flight)
        {
            if (DL.removeOneFlights( flight) == true) return true;
            throw new Exception("the current flight unexsis.");  //לבדוק ניסוח האנגלית בזריקה
        }
        /// <summary>
        /// clean DataBase
        /// </summary>
        public void cleanDB()
        {
            DL.cleanDB();
        }
        #endregion


    }
}
