using Microsoft.Graph;
using Microsoft.Maps.MapControl.WPF;
using PFlight.command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using static FlightModel.FlightM;
using Location = Microsoft.Maps.MapControl.WPF.Location;

namespace PFlight.viewmodel
{
    public class screen1VM : INotifyPropertyChanged
    {
        private model.screenM1 model1 { get; set; }
        public updateMapCommand cm { get; set; }
        public ObservableCollection<FlightModel.FlightData> FlightsIN { get; set; }
        public ObservableCollection<FlightModel.FlightData> FlightsOut { get; set; }
        public Map map {get;set;}
        public ResourceDictionary res { get; set; }
        public screen1VM()
        {
            model1 = new model.screenM1();
            cm = new updateMapCommand();
            FlightsIN = new ObservableCollection<FlightModel.FlightData>(model1.FlightInWeb());
            FlightsOut = new ObservableCollection<FlightModel.FlightData>(model1.FlightOutWeb());
            cm.UpdateMap += Cm_UpdateMap;



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

        private void Cm_UpdateMap(FlightModel.FlightM.Root flightM)
        {
            //order by the time
            var OrderedPlaces = (from f in flightM.trail
                                 orderby f.ts
                                 select f).ToList<Trail>();
            
            addNewPolyLine(OrderedPlaces);

            Trail CurrentPlace = null;
             Pushpin PinCurrent = new Pushpin { ToolTip = flightM.identification.number.@default };//המטוס עצמו
            //Pushpin PinCurrent = new Pushpin { ToolTip = flightM.airport.origin.position.latitude };
            Pushpin PinOrigin = new Pushpin { ToolTip = flightM.airport.origin.name };//מוצא המטוס- שדקה תעופה

            PositionOrigin origin = new PositionOrigin { X = 0.4, Y = 0.4 };//where to pat the icon of the flight
            MapLayer.SetPositionOrigin(PinCurrent, origin);

            CurrentPlace = OrderedPlaces.Last<Trail>();

            ///לחשוב מה עושים עם הצד ההפוך של המטוס מבחינת כיוונים
            if (flightM.airport.destination.code.iata=="TLV")
            {
                if(CurrentPlace.lat > 32.009444)
                    PinCurrent.Style = (Style)res["ToIsrael"];
                else
                    PinCurrent.Style = (Style)res["FromIsrael"];
            }
            else
            {
                if(CurrentPlace.lat > 32.009444)
                    PinCurrent.Style= (Style)res["FromIsrael"];
                else
                    PinCurrent.Style = (Style)res["ToIsrael"];
            }

            
            var PlaneLocation = new Location { Latitude = CurrentPlace.lat, Longitude = CurrentPlace.lng };
            PinCurrent.Location = PlaneLocation;

            CurrentPlace = OrderedPlaces.First<Trail>();
            PlaneLocation = new Location { Latitude = CurrentPlace.lat, Longitude = CurrentPlace.lng };
            PinOrigin.Location = PlaneLocation;

            map.Children.Add(PinOrigin);
            map.Children.Add(PinCurrent);


        }

        private void addNewPolyLine(List<Trail> orderedPlaces) //list of point 
        {
            MapPolyline polyline = new MapPolyline(); //creat line
            polyline.Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(230,10,10));
            polyline.StrokeThickness = 2;
            polyline.Opacity = 0.7;
            polyline.Locations = new LocationCollection();

            foreach(var item in orderedPlaces)
            {
                polyline.Locations.Add(new Location(item.lat, item.lng));
            }
           // map.Children.Clear();
            map.Children.Add(polyline);

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
