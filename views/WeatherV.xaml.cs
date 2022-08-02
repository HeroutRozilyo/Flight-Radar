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
using System.Windows.Shapes;
using Weather1;

namespace PFlight.views
{
    /// <summary>
    /// Interaction logic for WeatherV.xaml
    /// </summary>
    public partial class WeatherV : Window
    {
        WeatherVM CurrentVM { get; set; }

        public  WeatherV(FlightModel.FlightM.Root f)
        {
            InitializeComponent();
            CurrentVM = new WeatherVM();
            this.DataContext = CurrentVM;

            CurrentVM.LatLonWeather(f);
        }      
    }
}
