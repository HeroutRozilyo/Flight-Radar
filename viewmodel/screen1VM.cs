using Microsoft.Graph;
using PFlight.command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PFlight.viewmodel
{
    public class screen1VM : INotifyPropertyChanged
    {
        private model.screenM1 model1;
        private showFlightDCommand cm;
        public ObservableCollection<FlightModel.FlightData> FlightsIN { get; set; }
        public ObservableCollection<FlightModel.FlightData> FlightsOut { get; set; }

        public screen1VM()
        {
            model1 = new model.screenM1();
            cm = new showFlightDCommand();
            FlightsIN = new ObservableCollection<FlightModel.FlightData>(model1.FlightInWeb());
            FlightsOut = new ObservableCollection<FlightModel.FlightData>(model1.FlightOutWeb());


            for (int i = FlightsIN.Count - 1; i >= 0; i--)
            {
                if (FlightsIN[i].Source == "")
                    FlightsIN.RemoveAt(i);
            }
            for (int i = FlightsOut.Count - 1; i >= 0; i--)
            {
                if (FlightsOut[i].Destination == "")
                    FlightsOut.RemoveAt(i);
            }
        }

        public FlightModel.FlightM.Root GetRootF(string key)
        {
           return model1.GetRootF(key);
        }

        public bool addFlightDB( string key,FlightModel.FlightData f)
        {
           return model1.addFlightDB(key, f);
        }

        public event PropertyChangedEventHandler PropertyChanged;


  



    }
}
