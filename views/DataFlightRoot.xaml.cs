using FlightModel;
using PFlight.viewmodel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static FlightModel.FlightM;

namespace PFlight.views
{
    /// <summary>
    /// Interaction logic for DataFlightRoot.xaml
    /// </summary>
    public partial class DataFlightRoot : Page
    {
        #region variable
        WeatherVM CurrentVM { get; set; }
        Root root=new Root();
        HelperClass helperClass = new HelperClass();
        #endregion
        #region COnstructor
        public DataFlightRoot( Root r)
        {
            CurrentVM = new WeatherVM(this,r);
            CurrentVM.LatLonWeather(r);
            root = r;
            InitializeComponent();
            DataContext = CurrentVM;


            var bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.UriSource = new Uri(root.aircraft.images.medium[1].src); ;
            bitmapImage.EndInit();
            picture.Source = bitmapImage;

            flightCode.Content=root.identification.number.@default;
            desTime.Content= (helperClass.GetDateTimeFromEpohc(Convert.ToDouble(root.time.scheduled.arrival + root.airport.destination.timezone.offset))).ToString("HH:mm");
            originTime.Content= helperClass.GetDateTimeFromEpohc(Convert.ToDouble(root.time.scheduled.departure +(double) (root.airport.origin.timezone.offset))).ToString("HH:mm");
        }
        #endregion

        #region button
        private void backData_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            NavigationService ns = NavigationService.GetNavigationService(this);
            ns.Content = null;
            MainWindow parentWindow = Window.GetWindow(this) as MainWindow;
            this.Visibility = Visibility.Collapsed;
            parentWindow.dataFrame.Visibility = Visibility.Collapsed;
            parentWindow.WheatherFrame.Content = null;
            parentWindow.FrameMap.Content = null;
            parentWindow.frame.Navigate(parentWindow.mapP);

        }
        private void weatherButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow parentWindow = Window.GetWindow(this) as MainWindow;
            parentWindow.frame.Content = null;

            parentWindow.WheatherFrame.Navigate(new WeatherVP(root));
            
            parentWindow.FrameMap.Navigate(parentWindow.mapP);
           
        }
#endregion
    }
}
