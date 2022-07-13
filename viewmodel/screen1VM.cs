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
            var OrderedPlaces = (from f in flightM.trail
                                 orderby f.ts
                                 select f).ToList<Trail>();

            addNewPolyLine(OrderedPlaces);
        }

        private void addNewPolyLine(List<Trail> orderedPlaces)
        {
            MapPolyline polyline = new MapPolyline();
            //polyline.fill
            polyline.Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(230,10,10));
            polyline.StrokeThickness = 1;
            polyline.Opacity = 0.7;
            polyline.Locations = new LocationCollection();

            foreach(var item in orderedPlaces)
            {
                polyline.Locations.Add(new Location(item.lat, item.lng));
            }
            myMap.Children.clear();

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
