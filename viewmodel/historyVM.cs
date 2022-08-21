using PFlight.command;
using PFlight.model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PFlight.viewmodel
{
    /// <summary>
    /// search history 
    /// </summary>
    public class historyVM : INotifyPropertyChanged
    {
        #region variable
        private screenM1 model { get; set; }

        private ObservableCollection<FlightModel.FlightData> flights;// list of the flight search result
        DateTime dates;//the chosen date
        #endregion

        public historyVM()
        {
           
            model = new screenM1();
            dates = DateTime.Today;
            flights = new ObservableCollection<FlightModel.FlightData>();
            GetAllFlightHistory();
            
        }

        #region property
        public DateTime Dates
        {
            get
            {
                return dates;
            }
            set
            {
                dates = value;
                OnPropertyChanged(nameof(Dates));

            }
        }


        
        public ObservableCollection<FlightModel.FlightData> Flights
        {
            get
            {
                return flights;
            }
            set
            {
                flights = value;
                OnPropertyChanged(nameof(Flights));

            }
        }
        #endregion
        /// <summary>
        /// when create filter
        /// </summary>
        /// <param name="dates"></param>
        /// <returns></returns>
        public bool GetFlightsFromDB(DateTime[] dates)
        {
            DateTime fromTime = dates[0];
            DateTime toTime = dates[dates.Length - 1].AddDays(1);

            Dictionary<string,List<FlightModel.FlightData>> dic = model.getFlights();
            List<FlightModel.FlightData> fl = new List<FlightModel.FlightData>();
            foreach (var item in dic["Incoming"])
            {
               if ( fromTime <= item.DataAndTime  && item.DataAndTime < toTime)
                    fl.Add(item);
            }
            foreach (var item in dic["Outgoing"])
            {
                if (fromTime <= item.DataAndTime && item.DataAndTime < toTime)
                    fl.Add(item);
            }
            Flights = new ObservableCollection<FlightModel.FlightData>(fl);
            if (Flights.Count()==0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// to insert data to FLight
        /// </summary>
        /// <returns></returns>
        public bool GetAllFlightHistory()
        {
           ;

            Dictionary<string, List<FlightModel.FlightData>> dic = model.getFlights();
            List<FlightModel.FlightData> fl = new List<FlightModel.FlightData>();
            foreach (var item in dic["Incoming"])
            {
               
                    fl.Add(item);
            }
            foreach (var item in dic["Outgoing"])
            {
               
                    fl.Add(item);
            }
            Flights = new ObservableCollection<FlightModel.FlightData>(fl);
            if (Flights.Count() == 0)
                return true;
            else
                return false;
        }


        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this,
                new PropertyChangedEventArgs(propertyName));
        }
    }
}
