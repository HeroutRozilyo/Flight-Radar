using FlightModel;
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
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
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
        public Map map { get; set; }
        public ResourceDictionary res { get; set; }
        private double angle;
        public  event PropertyChangedEventHandler PropertyChanged;
        public WeatherVM weatherVM { get; set; }
      //  public WeatherCommand weatherCommand { get; set; }


        public double Angle
        {
            get { return angle; }
            set
            {
                angle = value;
                OnPropertyChanged(nameof(Angle));
                
                //  if (PropertyChanged != null)
                //     PropertyChanged(this, new PropertyChangedEventArgs("Angle"));
            }
        }
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this,
                new PropertyChangedEventArgs(propertyName));
        }


        public screen1VM(Map m, ResourceDictionary r)
        {
            model1 = new model.screenM1();
           // weatherVM = new WeatherVM();
             cm = new updateMapCommand();
            Angle = 0;
            map = m;
            res = r;
            //Task.Run(async()=> await getUrlF());
           getUrlF();
            cm.UpdateMap += Cm_UpdateMap;          
        }

        //to uodate the data all 10 sec. the func get the new data from the web, clean the data and return specific data on flights.
        //public async Task getUrlF()
        public void getUrlF()
        {
           // Dictionary<string, List<FlightData>> temp = await model1.getWebFlights();
             Dictionary<string, List<FlightData>> temp =  model1.getWebFlights();

            FlightsIN = new ObservableCollection<FlightModel.FlightData>(temp["Incoming"]);
            FlightsOut = new ObservableCollection<FlightModel.FlightData>(temp["Outgoing"]);
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
            putFlightOnMap(FlightsIN);
            putFlightOnMap(FlightsOut);


            Pushpin PinCurrent = new Pushpin { ToolTip = "Tel Aviv- Ben Gurion" };//המטוס עצמו
            var PlaneLocation = new Location { Latitude = 32.009444, Longitude = 34.876944 };
            PinCurrent.Location = PlaneLocation;
            map.Children.Add(PinCurrent);
        }


        //add choose flight to the DB
        public bool addFlightDB(string key, FlightModel.FlightData f)
        {
            return model1.addFlightDB(key, f);
        }

        private bool addFromDB(FlightModel.FlightData flight)
        {
            Dictionary<string, List<FlightData>> dic = model1.getFlights();
            if (dic != null)
            {
                List<FlightData> incom = dic["Incoming"];
                List<FlightData> outcom = dic["Outgoing"];

                foreach (var item in incom)
                {
                    if (flight.SourceId == item.SourceId)
                    {
                        addNewPolyLine(GetRootF(item.SourceId));
                        return false;
                    }
                }
                foreach (var item in outcom)
                {
                    if (flight.SourceId == item.SourceId)
                    {
                        addNewPolyLine(GetRootF(item.SourceId));
                        return false;
                    }
                }
            }
            return false;
        }


     


        //func that move on list of flights and put all of them on the screen after get their data
        public void putFlightOnMap(ObservableCollection<FlightModel.FlightData> flights)
        {
             //map.Children.Clear();

            FlightModel.FlightM.Root f = new FlightModel.FlightM.Root();
            for (int i = 0; i < flights.Count; i++)
            {
                try
                {
                       putOneFlight1(flights[i]);
                }
                catch(Exception e)
                {
                    int a = 0;
                }              
            }
        }
      
        private void putOneFlight1(FlightModel.FlightData flightM)
        {
            double lat = flightM.Lat;
            double lon= flightM.Long;
           
            Pushpin PinCurrent = new Pushpin { ToolTip = flightM.FlightCode };//המטוס עצמו
            PinCurrent.Tag = flightM.SourceId;
            PositionOrigin origin = new PositionOrigin { X = 0.4, Y = 0.4 };//where to put the icon of the flight
            MapLayer.SetPositionOrigin(PinCurrent, origin);

          //  Angle=FlightAngle(lat, lon);
          // PinCurrent.Style = (Style)res["FromIsrael"];

            // לחשוב מה עושים עם הצד ההפוך של המטוס מבחינת כיוונים
            if (flightM.Destination == "TLV")
            {
                if (lat > 32.009444)
                    PinCurrent.Style = (Style)res["ToIsrael"];

                else
                    PinCurrent.Style = (Style)res["FromIsrael"];
            }
            else
            {
                if (lat > 32.009444)
                    PinCurrent.Style = (Style)res["FromIsrael"];
                else
                    PinCurrent.Style = (Style)res["ToIsrael"];
            }
            var PlaneLocation = new Location { Latitude = lat, Longitude =lon };
            PinCurrent.Location = PlaneLocation;
            map.Children.Add(PinCurrent);
        }




        //func to return for specific flight by her key,all the data on her
        public FlightModel.FlightM.Root GetRootF(string key)
        {
            return model1.GetRootF(key);
        }


     


        //from the command order the by time and add polyline to the map
        private void Cm_UpdateMap(FlightModel.FlightM.Root flightM)
        {            
            addNewPolyLine(flightM);
        }

        //draw the polyline
        private void addNewPolyLine(FlightModel.FlightM.Root flightM) //list of point 
        {
            if (flightM != null)
            {


                //order by the time
                var OrderedPlaces = (from f in flightM.trail
                                     orderby f.ts
                                     select f).ToList<Trail>();

                #region pushpin airport
                Trail CurrentPlaceF = null;
                Pushpin PinOrigin = new Pushpin { ToolTip = flightM.airport.origin.name };//מוצא המטוס- שדה תעופה
                CurrentPlaceF = OrderedPlaces.First<Trail>();
                var PlaneLocation = new Location { Latitude = CurrentPlaceF.lat, Longitude = CurrentPlaceF.lng };
                if (flightM.airport.origin.code.iata != "TLV")
                {
                    PinOrigin.Location = PlaneLocation;
                    map.Children.Add(PinOrigin);
                }
                #endregion


                MapPolyline polyline = new MapPolyline(); //creat line
                polyline.Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(230, 10, 10));
                polyline.StrokeThickness = 2;
                polyline.Opacity = 0.7;
                polyline.Locations = new LocationCollection();

                foreach (var item in OrderedPlaces)
                {
                    polyline.Locations.Add(new Location(item.lat, item.lng));
                }
                removeChildren(flightM);
                map.Children.Add(polyline);



                Pushpin PinCurrent = new Pushpin { ToolTip = flightM.identification.number.@default };//המטוס עצמו
                PinCurrent.Tag = flightM.identification.id;
                PositionOrigin origin = new PositionOrigin { X = 0.4, Y = 0.4 };//where to put the icon of the flight
                MapLayer.SetPositionOrigin(PinCurrent, origin);

                ///לחשוב מה עושים עם הצד ההפוך של המטוס מבחינת כיוונים
                if (flightM.airport.destination.code.iata == "TLV")
                {

                    PinCurrent.Style = (Style)res["ToIsraelYello"];
                }
                else
                {
                    PinCurrent.Style = (Style)res["fromIsraelYello"];
                }


                var PlaneLocationl = new Location { Latitude = OrderedPlaces.Last<Trail>().lat, Longitude = OrderedPlaces.Last<Trail>().lng };
                PinCurrent.Location = PlaneLocationl;
                map.Children.Add(PinCurrent);
            }

        }

        //remove one flight from the map
        public void removeChildren(FlightModel.FlightM.Root flightM)
        { 
             List<Pushpin> mapPolygons = new List<Pushpin>();
            foreach (var item in map.Children)
            {
                if (item is Pushpin)
                {
                    mapPolygons.Add(item as Pushpin);
                }
            }
            Pushpin p= mapPolygons.Find(e => (string)e.Tag == flightM.identification.id);
            map.Children.Remove(p);
            // mapPolygons.ForEach(map.Children.Remove);

        }


        public double FlightAngle(double lat, double lon)
        {
            double latX = lat- 32.009444;
            double lonY = lon - 34.876944;
          
            var deltaX = Math.Pow(latX, 2);
            var deltaY = Math.Pow(lonY, 2);

            //atan2 for angle
            var radians = Math.Atan2(lonY, latX); 

            //radians into degrees
            return radians * (180 / Math.PI);

        }

        public void chooseFlight()
        {
           Dictionary<string,List<FlightData>> dic =model1.getFlights();
            foreach(var item in dic["Incoming"])
            {
                addNewPolyLine(GetRootF(item.SourceId));
            }
            foreach (var item in dic["Outgoing"])
            {
                addNewPolyLine(GetRootF(item.SourceId));
            }

        }


        ////get one flight and put really on the map
        //private void putOneFlight(FlightModel.FlightM.Root flightM)
        //{
        //    //order by the time
        //    var OrderedPlaces = (from f in flightM.trail
        //                         orderby f.ts
        //                         select f).ToList<Trail>();


        //    Trail CurrentPlaceL = null;
        //    Trail CurrentPlaceF = null;

        //    Pushpin PinCurrent = new Pushpin { ToolTip = flightM.identification.number.@default };//המטוס עצמו
        //    Pushpin PinOrigin = new Pushpin { ToolTip = flightM.airport.origin.name };//מוצא המטוס- שדה תעופה

        //    PositionOrigin origin = new PositionOrigin { X = 0.4, Y = 0.4 };//where to pat the icon of the flight
        //    MapLayer.SetPositionOrigin(PinCurrent, origin);

        //    CurrentPlaceL = OrderedPlaces.Last<Trail>();

        //    ///לחשוב מה עושים עם הצד ההפוך של המטוס מבחינת כיוונים
        //    if (flightM.airport.destination.code.iata == "TLV")
        //    {
        //        if (CurrentPlaceL.lat > 32.009444)
        //            PinCurrent.Style = (Style)res["ToIsrael"];
        //        else
        //            PinCurrent.Style = (Style)res["FromIsrael"];
        //    }
        //    else
        //    {
        //        if (CurrentPlaceL.lat > 32.009444)
        //            PinCurrent.Style = (Style)res["FromIsrael"];
        //        else
        //            PinCurrent.Style = (Style)res["ToIsrael"];
        //    }


        //    var PlaneLocation = new Location { Latitude = CurrentPlaceL.lat, Longitude = CurrentPlaceL.lng };
        //    PinCurrent.Location = PlaneLocation;

        //    if (flightM.airport.origin.code.iata != "TLV")
        //    {
        //        CurrentPlaceF = OrderedPlaces.First<Trail>();
        //        PlaneLocation = new Location { Latitude = CurrentPlaceF.lat, Longitude = CurrentPlaceF.lng };
        //        PinOrigin.Location = PlaneLocation;

        //        map.Children.Add(PinOrigin);
        //    }
        //    map.Children.Add(PinCurrent);

        //}

    }
}
