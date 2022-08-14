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
        public static screen1VM CurrentVM { get; set; }
        Root root=new Root();
        public DataFlightRoot( Root r)
        {
            CurrentVM = new screen1VM();
            root = r;
            InitializeComponent();
            DataContext = root;
        }

        private void backData_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            NavigationService ns = NavigationService.GetNavigationService(this);
            ns.Content = null;
            MainWindow parentWindow = Window.GetWindow(this) as MainWindow;
            this.Visibility = Visibility.Collapsed;
            parentWindow.dataFrame.Visibility = Visibility.Collapsed;

        }
    }
}
