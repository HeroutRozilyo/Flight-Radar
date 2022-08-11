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
    /// Interaction logic for ListFlightVP.xaml
    /// </summary>
    /// 

    public partial class ListFlightVP : Page
    {
        public static ListFlightVM CurrentVM { get; set; }
        public ListFlightVP()
        {
            InitializeComponent();
            
            CurrentVM = new ListFlightVM();
            this.DataContext = CurrentVM;
        }
        private void inlist_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           

        }
    
    }
}
