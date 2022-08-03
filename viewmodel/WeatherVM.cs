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
        WeatherM weather { get; set; }
        public  CloseCommand closeCommand { get; set; }
        public double tempDivide=11.17;
        public WeatherModel.Main rootO;
        public WeatherModel.Main rootD;
        string originAname;
        string destinationAname;
        WeatherV wnd;

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


        public event PropertyChangedEventHandler PropertyChanged;

      
        public WeatherVM(WeatherV v)
        {
            weather = new WeatherM();
            wnd = v;
            closeCommand = new CloseCommand(this);
        }
     

        public async void LatLonWeather(FlightModel.FlightM.Root flight)
        {
            double latO = flight.airport.origin.position.latitude;
            double lanO = flight.airport.origin.position.longitude;
            double latD = flight.airport.destination.position.latitude;
            double lonD = flight.airport.destination.position.longitude;

            rootO= await weather.LatLonWeather(latO, lanO);
            rootD = await weather.LatLonWeather(latD, lonD);

            rootO.feels_like = rootO.feels_like / tempDivide;
            rootO.temp = rootO.temp / tempDivide;
            rootO.temp_max = rootO.temp_max/ tempDivide;
            rootO.temp_min = rootO.temp_min / tempDivide;

            rootD.feels_like = rootD.feels_like / tempDivide;
            rootD.temp = rootD.temp / tempDivide;
            rootD.temp_max = rootD.temp_max / tempDivide;
            rootD.temp_min = rootD.temp_min / tempDivide;
            RootD = rootD;
            RootO = rootO;
           
            OriginAname = "Origin Airport:\n "+ flight.airport.origin.name;
            DestinationAname = "Destination Airport:\n " + flight.airport.destination.name;
           
        }

        public void closeWnd()
        {
            wnd.Close();
        }

    }
}
