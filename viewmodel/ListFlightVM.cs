using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static FlightModel.FlightM;
using FlightModel;
using System.ComponentModel;
using PFlight.model;
using System.Runtime.CompilerServices;

namespace PFlight.viewmodel
{

    public class ListFlightVM
    {

        
        
           ListFlightM model = new ListFlightM();
            //public ICommand flightDetailsCommand { get; set; }

            public ListFlightVM(FlightData _flight)
            {
                flight = _flight;
               
            }

           

            FlightData flight;
            public FlightData Flight
            {
                get
                {
                    return flight;
                }
                set
                {
                    flight = value;
                    OnPropertyChanged("Flight");
                }
            }

           

            public event PropertyChangedEventHandler PropertyChanged;
            protected void OnPropertyChanged([CallerMemberName] string name = null)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
            }
        }






    
}
