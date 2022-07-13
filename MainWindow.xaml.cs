using FlightModel;
using PFlight.viewmodel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace PFlight
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public screen1VM CurrentVM { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            CurrentVM = new screen1VM();
            this.DataContext = CurrentVM;
        }

        private void inList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            FlightData flightI = inList.SelectedItem as FlightModel.FlightData;
            detailsP.DataContext=CurrentVM.GetRootF(flightI.SourceId);
            bool flag=CurrentVM.addFlightDB("Incoming", flightI);
            if(flag)
            {
                MessageBox.Show("Flight details saved successfully", "DB", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
                MessageBox.Show("Flight details already exsis", "DB", MessageBoxButton.OK, MessageBoxImage.Error);

        }

        private void outList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            FlightData flightO = outList.SelectedItem as FlightModel.FlightData;
            detailsP.DataContext = CurrentVM.GetRootF(flightO.SourceId);
            bool flag = CurrentVM.addFlightDB("Outgoing", flightO);
            if (flag)
            {
                MessageBox.Show("Flight details saved successfully", "DB", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
                MessageBox.Show("Flight details already exsis", "DB", MessageBoxButton.OK, MessageBoxImage.Error);

        }
        
    }
}
