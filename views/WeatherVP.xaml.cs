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
        WeatherVM CurrentVM { get; set; }

        
        public WeatherVP(FlightModel.FlightM.Root f)
        {
            InitializeComponent();
            CurrentVM = new WeatherVM(this);
            this.DataContext = CurrentVM;

            CurrentVM.LatLonWeather(f);
        }
    }
}
