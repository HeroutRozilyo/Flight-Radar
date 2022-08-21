using FlightModel;
using Microsoft.Graph;
using Microsoft.Maps.MapControl.WPF;
using PFlight.command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

using static FlightModel.FlightM;
using Image = System.Windows.Controls.Image;
using Location = Microsoft.Maps.MapControl.WPF.Location;

namespace PFlight.viewmodel
{
    public class screen1VM : INotifyPropertyChanged
    {
        #region variable
        private model.screenM1 model1 { get; set; }
        public updateMapCommand cm { get; set; }

        public ObservableCollection<FlightModel.FlightData> FlightsIN { get; set; }
        public ObservableCollection<FlightModel.FlightData> FlightsOut { get; set; }
        public FlightData flightDataMap = new FlightData();
        public Map map { get; set; }
        public ResourceDictionary res { get; set; }
        private float angle;
        public event PropertyChangedEventHandler PropertyChanged;
        Image imagePinMap = new Image();
        ObservableCollection<string> myCollection = new ObservableCollection<string>();
        List<FlightData> incom=new List<FlightData>();
        List<FlightData> outcom=new List<FlightData>();
        string code;
        List<FlightData> dicI = new List<FlightData>();
        List<FlightData> dicO = new List<FlightData>();
        MapLayer allFlightLayer = new MapLayer();
        MapLayer YellowFlight = new MapLayer();
        public Action<object, MouseButtonEventArgs> CallMyMethodEvent;
        #endregion

        #region properties
        public Image ImagePinMap
        {
            get { return imagePinMap; }
            set
            {
                imagePinMap = value;
                OnPropertyChanged(nameof(ImagePinMap));

              
            }
        }
        public string Code
        {
            get { return code; }
            set
            {
                code = value;
                OnPropertyChanged(nameof(Code));

   
            }
        }
        public float Angle
        {
            get { return angle; }
            set
            {
                angle = value;
                OnPropertyChanged(nameof(Angle));

               
            }
        }
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this,
                new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #region constructors
        /// <summary>
        /// for the main window
        /// </summary>
        /// <param name="m"></param>
        /// <param name="r"></param>
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


        /// <summary>
        /// for the user control
        /// </summary>
        public screen1VM()
        {
            model1 = new model.screenM1();
            cm = new updateMapCommand();
        }
        #endregion



        #region update map every 10 second

        /// <summary>
        /// to uodate the data all 10 sec. the func get the new data from the web, clean the data and return specific data on flights
        /// </summary>
        public void getUrlF()
        {
            Dictionary<string, List<FlightData>> temp = model1.getWebFlights();
            allFlightLayer.Children.Clear();
            allFlightLayer = new MapLayer();
            map.Children.Remove(allFlightLayer);
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
            allFlightLayer.Children.Add(PinCurrent);

            map.Children.Add(allFlightLayer);
        }

        
        /// <summary>
        /// func that move on list of flights and put all of them on the screen after get their data
        /// </summary>
        /// <param name="flights"></param>
        public void putFlightOnMap(ObservableCollection<FlightModel.FlightData> flights)
        {

            FlightModel.FlightM.Root f = new FlightModel.FlightM.Root();
            for (int i = 0; i < flights.Count; i++)
            {
                try
                {
                    putOneFlight1(flights[i]);

                }
                catch (Exception e)
                {
                    int a = 0;
                }
            }
        }


        /// <summary>
        /// put orenge flight on map
        /// </summary>
        /// <param name="flightM"></param>
        private void putOneFlight1(FlightModel.FlightData flightM)
        {
            float lat = (float)flightM.Lat;
            float lon = (float)flightM.Long;
            Angle = FlightAngle(lat, lon);
            PositionOrigin origin = new PositionOrigin { X = 0.5, Y = 0.5 };//where to put the icon of the flight

            #region image                                                              
            Image image = new Image();
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri("C:/Users/Pc/source/repos/PFlight/Icon/ToMap.png"); //שלך
            //bitmap.UriSource = new Uri("C:/Users/da077/source/repos/PFlight/Icon/ToMap.png");// שלי
            bitmap.DecodePixelHeight = 256;
            bitmap.DecodePixelWidth = 256;

            if (flightM.Destination != "TLV")
            {
                bitmap.UriSource = new Uri("C:/Users/Pc/source/repos/PFlight/Icon/fromMap.png"); //שלך
                //bitmap.UriSource = new Uri("C:/Users/da077/source/repos/PFlight/Icon/fromMap.png");// שלי

            }
            bitmap.EndInit();
            image.Source = bitmap;

            RotateTransform rotateTransform = new RotateTransform(Angle);
            image.RenderTransform = rotateTransform;
            image.Height = 20;
            image.Width = 20;
            ImagePinMap = image;
            #endregion
            ImagePinMap.MouseDown += ImagePinMap_MouseDown;


            ImagePinMap.ToolTip = flightM.FlightCode;
            if (flightM.FlightCode == "")
                ImagePinMap.ToolTip = "Data Problem";

            var PlaneLocation = new Location { Latitude = lat, Longitude = lon };
            MapLayer mapLayer = new MapLayer();

            allFlightLayer.AddChild(ImagePinMap, PlaneLocation, origin);

        }
        /// <summary>
        /// put the yellow flight
        /// </summary>
        public void chooseFlight()

        {
            YellowFlight.Children.Clear();
            YellowFlight = new MapLayer();
            map.Children.Remove(YellowFlight);


            foreach (var item in dicI)
            {
                addNewPolyLine(GetRootF(item.SourceId));
            }
            foreach (var item in dicO)
            {
                addNewPolyLine(GetRootF(item.SourceId));
            }

            map.Children.Add(YellowFlight);
        }

        #endregion

        /// <summary>
        /// add choose flight to the DB
        /// </summary>
        public bool addFlightDB( FlightModel.FlightData f)
        {
            if (f.Destination == "TLV")            
                dicI.Add(f);           
            else
                dicO.Add(f);
            return model1.addFlightDB(f);
        }




        #region for autoSuggestionUserControl
        public bool ListFromDB()
        {
            Dictionary<string, List<FlightData>> dic = model1.getFlights();
            myCollection.Clear();

            if (dic != null)
            {
                 incom = dic["Incoming"];
                outcom = dic["Outgoing"];
               


                return true;
            }
            return false;
            }
        public ObservableCollection<string>  getObserverList()
        {
            if (ListFromDB())
            {

                foreach (FlightData temp in incom)
                {
                    myCollection.Add(temp.FlightCode);

                }
                foreach (FlightData temp in outcom)
                {
                    myCollection.Add(temp.FlightCode);

                }
            }
            return myCollection;

        }
        #endregion








        /// <summary>
        /// event click tothe pkane on map
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ImagePinMap_MouseDown(object sender, MouseButtonEventArgs e)
        {

            var a = (Image)sender; //to get the line
            string code = a.ToolTip.ToString();
            flightDataMap = FlightsIN.Where(x=>x.FlightCode==code).FirstOrDefault();
            if(flightDataMap==null)
                flightDataMap = FlightsOut.Where(x => x.FlightCode == code).FirstOrDefault();
            if (CallMyMethodEvent != null)
            {
                CallMyMethodEvent.Invoke(sender,e);
            }

        }


        #region Get flight

        /// <summary>
        /// func to return for specific flight by her key,all the data on her
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public FlightModel.FlightM.Root GetRootF(string key)
        {
            return model1.GetRootF(key);
        }
        /// <summary>
        /// return by codeFLight
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public FlightData GetFlightCode(string key)
        {
            FlightData a=new FlightData();
            a= model1.getOneFlights(key);
            return a;
        }
        #endregion


        /// <summary>
        /// from the command order the by time and add polyline to the map (UpdateMap)
        /// </summary>
        /// <param name="flightM"></param>
        private void Cm_UpdateMap(FlightModel.FlightM.Root flightM)
        {
            map.Children.Remove(YellowFlight);
            addNewPolyLine(flightM);
            map.Children.Add(YellowFlight);

        }

       /// <summary>
       /// Add new polyline
       /// </summary>
       /// <param name="flightM"></param>
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
                    YellowFlight.Children.Add(PinOrigin);
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
                YellowFlight.Children.Add(polyline);



               //המטוס עצמו
                PositionOrigin origin = new PositionOrigin { X = 0.5, Y = 0.5 };//where to put the icon of the flight
                
                var PlaneLocationl = new Location { Latitude = OrderedPlaces.Last<Trail>().lat, Longitude = OrderedPlaces.Last<Trail>().lng };
                float lat = (float)OrderedPlaces.Last<Trail>().lat;
                float lon = (float)OrderedPlaces.Last<Trail>().lng;
                float angle1;


                angle1 = FlightAngle(lat, lon);
                #region image

                Image image = new Image();
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri("C:/Users/Pc/source/repos/PFlight/Icon/airplaneToIsrael.png");
               // bitmap.UriSource = new Uri("C:/Users/da077/source/repos/PFlight/Icon/airplaneToIsrael.png");

                bitmap.DecodePixelHeight = 256;
                bitmap.DecodePixelWidth = 256;
               
               


                if (flightM.airport.destination.code.iata != "TLV")
                {
                 bitmap.UriSource = new Uri("C:/Users/Pc/source/repos/PFlight/Icon/airplane.png");
                 // bitmap.UriSource = new Uri("C:/Users/da077/source/repos/PFlight/Icon/airplane.png");

                }
                bitmap.EndInit();
                image.Source = bitmap;

                RotateTransform rotateTransform = new RotateTransform(angle1);
                image.RenderTransform = rotateTransform;
                image.Height = 20;
                image.Width = 20;
                #endregion
                ImagePinMap = image;
                ImagePinMap.MouseDown += ImagePinMap_MouseDown; ;
                Code = flightM.identification.number.@default;
                ImagePinMap.ToolTip = Code; 
                if (flightM.identification.number.@default == "")
                    ImagePinMap.ToolTip = "Data Problem";


                YellowFlight.AddChild(ImagePinMap, PlaneLocationl, origin);
            }

        }

        /// <summary>
        /// remove one flight from the map
        /// </summary>
        /// <param name="flightM"></param>
        public void removeChildren(FlightModel.FlightM.Root flightM)
        { 
             List<Image> mapPolygons = new List<Image>();
            foreach (var item in allFlightLayer.Children)
            {
                if (item is Image)
                {
                    
                    {

                        mapPolygons.Add(item as Image);
                    }
                }
            }
            Image p = mapPolygons.Find(e => (e.ToolTip.ToString() == flightM.identification.number.@default));
            map.Children.Remove(allFlightLayer);
            allFlightLayer.Children.Remove(p);
            map.Children.Add(allFlightLayer);
            

        }


        /// <summary>
        ///  calculate the angle of the flight 
        /// </summary>
        /// <param name="lat"></param>
        /// <param name="lon"></param>
        /// <returns></returns>
        public float FlightAngle(float lat, float lon)
        {
            float latX = (float)(lat - 32.009444);
            float lonY = (float)(lon - 34.876944);
          
            var deltaX = Math.Pow(latX, 2);
            var deltaY = Math.Pow(lonY, 2);

            //atan2 for angle
            var radians = Math.Atan2(deltaX, deltaY); 

            //radians into degrees
            return (float)(radians * (180 / Math.PI));

        }

        public void cleanDB()
        {
            model1.cleanDB();
        }


    }
}
