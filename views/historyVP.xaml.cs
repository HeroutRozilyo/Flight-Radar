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
    /// Interaction logic for historyVP.xaml
    /// </summary>
    public partial class historyVP : Page
    {
        historyVM CurrentVM { get; set; }
    

        public historyVP()
        {
            InitializeComponent();
            CurrentVM = new historyVM();
            this.DataContext = CurrentVM;
            nodata.Visibility = Visibility.Hidden;
            calendar.DisplayDateEnd = DateTime.Today;
            

        }


        private void calendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
               nodata.Visibility = Visibility.Hidden;
            var v = calendar.SelectedDates;
            DateTime[] dates = new DateTime[v.Count()];
            for(int i = 0; i < v.Count(); i++)
            {
                dates[i] = v[i];
            }
            if (CurrentVM.GetFlightsFromDB(dates))
                nodata.Visibility = Visibility.Visible;
        }
       
    }
}
