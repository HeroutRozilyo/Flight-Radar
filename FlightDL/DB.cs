using FlightModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightDL
{
    public class DB : IDL
    {


        //private Dictionary<string, List<FlightData>> Flights = new Dictionary<string, List<FlightData>>();
        private List<FlightData> FIncoming = new List<FlightData>();
        private List<FlightData> FOutgoing = new List<FlightData>();

        //return all the data at the data base
        public Dictionary<string, List<FlightData>> getFlights()
        {
            Dictionary<string, List<FlightData>> Result = new Dictionary<string, List<FlightData>>();
            Result.Add("Incoming", FIncoming);
            Result.Add("Outgoing", FOutgoing);
            return Result;

        }

        //return one flights
        public FlightData getOneFlights(string key, int val)
        {////////לפי מה מזהים טיסה???
            FlightData flight = null;
            if (key == "Incoming")
            {
                flight = FIncoming.FirstOrDefault(f => f.Id == val);
            }
            else
            {
                flight = FOutgoing.FirstOrDefault(f => f.Id == val);
            }
            return flight;

            /*
            foreach(FlightData v in Flights[key])
            {
                if (v.Id == val)
                    return v;
            }
        return null;
            */
        }

        public bool addOneFlights(string key, FlightData flight)// צריך לראות אם צריך עשות clone לכל ההכנסות שלנו.
        {
            if (key == "Incoming")
            {
                if (FIncoming.FirstOrDefault(f => f.Id == flight.Id) == null)
                {
                    flight.Id = ++Config.IdFlightCounter;
                    FIncoming.Add(flight);
                    return true;

                }
            }
            else
            {
                if (FOutgoing.FirstOrDefault(f => f.Id == flight.Id) == null)
                {
                    flight.Id = ++Config.IdFlightCounter;
                    FOutgoing.Add(flight);
                    return true;
                }
            }
            return false;
        }

        public bool removeOneFlights(string key, FlightData flight)
        {

            if (key == "Incoming")
            {
                if (FIncoming.FirstOrDefault(f => f.Id == flight.Id) != null)
                {
                    FIncoming.Remove(flight);
                    return true;
                }
            }
            else
            {
                if (FOutgoing.FirstOrDefault(f => f.Id == flight.Id) != null)
                {
                    FOutgoing.Remove(flight);
                    return true;

                }
            }
            return false;
        }

    }
}
