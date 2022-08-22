using PFlight.viewmodel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
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
    /// Interaction logic for MapP.xaml
    /// </summary>
    public partial class MapP : Page
    {
        #region Variable
        public static screen1VM CurrentVM { get; set; }
        #endregion
        public MapP()
        {
            InitializeComponent();
            CurrentVM = new screen1VM(myMap, Resources);
            this.DataContext = CurrentVM;


        }

       
       
    }
}
