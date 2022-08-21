using FlightModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseEF1
{
    public class IDL
    {
        /// <summary>
        /// add One Flight to the DB
        /// </summary>
        /// <param name="f"></param>
        /// <returns></returns>
        public bool addOneFlights(FlightData f)
        {
            using (var db = new DB())
            {
                if (db.Flights.Find(f.FlightCode) == null)
                {
                    f.Id = ++Config.IdFlightCounter;
                    db.Flights.Add(f);
                    db.SaveChanges();
                    return true;
                }

            }
            return false;
        }

        //return all the data at the data base
        public Dictionary<string, List<FlightData>> getFlights()
        {
            Dictionary<string, List<FlightData>> result = new Dictionary<string, List<FlightData>>();
            using (var db = new DB())
            {
                var flightsI = (from f in db.Flights
                                where f.Destination == "TLV"
                                select f).ToList<FlightData>();

                result.Add("Incoming", flightsI);

                var flightsO = (from f in db.Flights
                                where f.Destination != "TLV"
                                select f).ToList<FlightData>();

                result.Add("Outgoing", flightsO);
            }
            return result;
        }

        

        public bool removeOneFlights( FlightData flight)
        {
            using (var db = new DB())
            {
                if (db.Flights.FirstOrDefault(f => f.FlightCode == flight.FlightCode) != null)
                {
                    db.Flights.Remove(flight);
                    db.SaveChanges();
                    return true;
                }

            }

            return false;
        }

        public void cleanDB()
        {
            using (var db = new DB())
            {
                foreach (var item in db.Flights)
                {
                    db.Flights.Remove(item);
                }

                db.SaveChanges();

            }
        }

        //return 1 flight by her code
        public FlightData getFlightCode(string val)
        {
            FlightData flight = new FlightData();
            using (var db = new DB())
            {

                flight = (from f in db.Flights
                          where f.FlightCode == val
                          select f).FirstOrDefault();

            }
            return flight;
        }

    }
}
