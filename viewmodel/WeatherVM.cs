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
        public double tempDivide=11.17;
        public WeatherModel.Main rootO;
        public WeatherModel.Main rootD { get; set; }
        //public WeatherCommand command { get; set; }
        private WeatherV wnd;


        public event PropertyChangedEventHandler PropertyChanged;

        public WeatherVM()
        {
            weather = new WeatherM();
           // command = new WeatherCommand(this);
        }
     
        public void openWind(FlightModel.FlightM.Root flight)
        {
            wnd = new WeatherV();
            LatLonWeather(flight);
            wnd.Show();
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

            rootD.feels_like = rootO.feels_like / tempDivide;
            rootD.temp = rootO.temp / tempDivide;
            rootD.temp_max = rootO.temp_max / tempDivide;
            rootD.temp_min = rootO.temp_min / tempDivide;


        }

    }
}
