using FlightModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseEF
{
    public class IDL
    {

        public bool addOneFlights(string key, FlightData f)
        {
            using (var db = new DB())
            {               
                    if (db.Flights.Find(f.Id) == null)
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
                var flightsI =  (from f in db.Flights
                                 where f.Destination=="TLV"
                                 select f).ToList<FlightData>();

                result.Add("Incoming", flightsI);

                var flightsO = (from f in db.Flights
                                where f.Destination != "TLV"
                                select f).ToList<FlightData>();

                result.Add("Outgoing", flightsO);
            }
            return result;
        }

        //return one flights
        public FlightData getOneFlights( int val)
        {
            FlightData flight = null;
            using (var db = new DB())
            {
             
               flight = (from f in db.Flights
                         where f.Id == val
                         select f).FirstOrDefault();
               
            }
            return flight;
        }

        public FlightData getFlightCode(string val)
        {
            FlightData flight = new FlightData();
            using (var db = new DB())
            {

                flight = (from f in db.Flights
                          where f.FlightCode== val
                          select f).FirstOrDefault();

            }
            return flight;
        }



        public bool removeOneFlights(string key, FlightData flight)
        {
            using (var db = new DB())
            {
                  if (db.Flights.FirstOrDefault(f => f.Id == flight.Id) != null)
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
                foreach(var item in db.Flights)
                {
                    db.Flights.Remove(item);
                }

                db.SaveChanges();

            }
        }

        //Dictionary<string, List<FlightData>> getFlights();//return all the data at the data base       
        //FlightData getOneFlights(string key, int val);//return one flights
        //bool addOneFlights(string key, FlightData flight);
        //bool removeOneFlights(string key, FlightData flight);
    }
}
