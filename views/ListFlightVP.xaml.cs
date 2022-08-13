using FlightModel;
using MahApps.Metro.Controls;
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
    /// Interaction logic for ListFlightVP.xaml
    /// </summary>
    /// 

    public partial class ListFlightVP : Page
    {
        public static ListFlightVM CurrentVM { get; set; }
        public ListFlightVP( FlightData flight)
        {
            InitializeComponent();

           CurrentVM=new ListFlightVM(flight);
           
           
            DataContext= CurrentVM;
            
          

        }
        private void backData_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            NavigationService ns = NavigationService.GetNavigationService(this);
            ns.Content = null;
            MainWindow parentWindow = Window.GetWindow(this) as MainWindow;
            this.Visibility=Visibility.Collapsed;
            parentWindow.dataFrame.Visibility = Visibility.Collapsed;

            backData.Visibility = Visibility.Collapsed;

        }


    }
}
