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

namespace PFlight.views
{
    /// <summary>
    /// Interaction logic for WeatherVP.xaml
    /// </summary>
    public partial class WeatherVP : Page
    {
        #region Variable
        WeatherVM CurrentVM { get; set; }
        #endregion

        public WeatherVP(FlightModel.FlightM.Root f)
        {
            InitializeComponent();
            CurrentVM = new WeatherVM(this);
            this.DataContext = CurrentVM;

            CurrentVM.LatLonWeather(f);
        }
        private void btnLight_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            MainWindow parentWindow = Window.GetWindow(this) as MainWindow;
            parentWindow.frame.Navigate(parentWindow.mapP);
            parentWindow.WheatherFrame.Content= null;
          
            parentWindow.FrameMap.Content = null;
            parentWindow.WheatherFrame.Content = null;
           
           


        }


    }
}
