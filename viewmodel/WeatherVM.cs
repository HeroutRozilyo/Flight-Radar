using FlightModel;
using PFlight.command;
using PFlight.model;
using PFlight.views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weather1;
using static FlightModel.FlightM;

namespace PFlight.viewmodel
{
    public class WeatherVM : INotifyPropertyChanged
    {
        #region variable
        WeatherM weather { get; set; }
        public  CloseCommand closeCommand { get; set; }
        public double tempDivide=11.17;
        public WeatherModel.Main rootO;
        public WeatherModel.Main rootD;
        string originAname;
        string destinationAname;
        WeatherVP wnd;
        Root flightData = new Root();
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Properties
        public string OriginAname
        {
            get { return originAname; }
            set
            {
                originAname = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("OriginAname"));
            }
        }
        public Root FlightData
        {
            get { return flightData; }
            set
            {
                flightData = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("OriginAname"));
            }
        }
        public string DestinationAname
        {
            get { return destinationAname; }
            set
            {
                destinationAname = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("DestinationAname"));
            }
        }


        public WeatherModel.Main RootD
        {
            get { return rootD; }
            set { rootD = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("RootD"));
                    }
        }

        public WeatherModel.Main RootO
        {
            get { return rootO; }
            set
            {
                rootO = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("RootO"));
            }
        }
        #endregion



        #region Constructors

        /// <summary>
        /// for Weather Page
        /// </summary>
        /// <param name="v"></param>
        public WeatherVM(WeatherVP v)
        {
            weather = new WeatherM();
            wnd = v;
            
            closeCommand = new CloseCommand(this);
        }
        /// <summary>
        /// for data Page
        /// </summary>
        /// <param name="v"></param>
        /// <param name="r"></param>
        public WeatherVM(DataFlightRoot v,Root r)
        {
            weather = new WeatherM();
            FlightData = r;
             closeCommand = new CloseCommand(this);
        }
        #endregion


        /// <summary>
        /// initialization
        /// </summary>
        /// <param name="flight"></param>
        public async void LatLonWeather(FlightModel.FlightM.Root flight)
        {
            if (flight != null)
            {
                double latO = Math.Round(flight.airport.origin.position.latitude, 2);
                double lanO = Math.Round(flight.airport.origin.position.longitude,2);
                double latD = Math.Round(flight.airport.destination.position.latitude,2);
                double lonD = Math.Round(flight.airport.destination.position.longitude);

                rootO = await weather.LatLonWeather(latO, lanO);
                rootD = await weather.LatLonWeather(latD, lonD);

                rootO.feels_like = Math.Round(rootO.feels_like / tempDivide,2);
                rootO.temp = Math.Round(rootO.temp / tempDivide,2);
                rootO.temp_max = Math.Round(rootO.temp_max / tempDivide,2);
                rootO.temp_min =Math.Round( rootO.temp_min / tempDivide,2);
                

                rootD.feels_like = Math.Round(rootD.feels_like / tempDivide,2);
                rootD.temp = Math.Round(rootD.temp / tempDivide,2);
                rootD.temp_max = Math.Round(rootD.temp_max / tempDivide,2);
                rootD.temp_min = Math.Round(rootD.temp_min / tempDivide,2);
                RootD = rootD;
                RootO = rootO;

                OriginAname = "Origin Airport:\n " + flight.airport.origin.name;
                DestinationAname = "Destination Airport:\n " + flight.airport.destination.name;
            }
           
        }

       
    }
}
