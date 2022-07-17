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
using static FlightModel.FlightM;

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
            CurrentVM = new screen1VM(myMap, Resources);
            this.DataContext = CurrentVM;
    

        }

       

        private void inlist_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var list = (ListView)sender; //to get the line

            FlightData flightO = list.SelectedItem as FlightModel.FlightData;
            Root flightM = CurrentVM.GetRootF(flightO.SourceId);
            detailsP.DataContext = flightM;

            if (flightO.Source == "TLV")
            {
                CurrentVM.addFlightDB("Outgoing", flightO);
            }
            else
            {
                   CurrentVM.addFlightDB("Incoming", flightO);
            }
      
            if (CurrentVM.cm.CanExecute(flightM))
                CurrentVM.cm.Execute(flightM);
        }

    }
}
