using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static FlightModel.FlightM;
using FlightModel;

namespace PFlight.viewmodel
{

    public class ListFlightVM
    {

        private model.ListFlightM model1 { get; set; }
        public ObservableCollection<FlightModel.FlightData> FlightsINDB { get; set; }
        public ObservableCollection<FlightModel.FlightData> FlightsOutDB { get; set; }
        public ListFlightVM()
        {
            model1 = new model.ListFlightM();
            Dictionary<string, List<FlightData>> flightData = new Dictionary<string, List<FlightData>>();
            flightData = model1.getFlights();
            if (flightData.Count != 0)
            {
                FlightsINDB = new ObservableCollection<FlightModel.FlightData>(flightData["Incoming"]);
                FlightsOutDB = new ObservableCollection<FlightModel.FlightData>(flightData["Outgoing"]);
            }
            //this.autoSuggestionUserControl.AutoSuggestionList = list;



        }
        

       
        


    }
}
