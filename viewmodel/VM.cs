using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PFlight.viewmodel
{
    public class VM : INotifyPropertyChanged
    {
        //model- bl
        public ObservableCollection<FlightModel.FlightData>  Flights { get; set; }
        public FlightBL.IBL BL { get; set; }
   

        public VM()
        {
            Flights = new ObservableCollection<FlightModel.FlightData>(); ////
        
            BL = new FlightBL.BL();
            Flights.CollectionChanged += Flights_CollectionChanged; 
        }

        private void Flights_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
           
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
