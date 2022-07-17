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

namespace PFlight.views
{
    /// <summary>
    /// Interaction logic for firstWindow.xaml
    /// </summary>
    public partial class firstWindow : Window
    {
        public OpenWindowVM CurrentVM { get; set; }
         
        public firstWindow()
        {
            InitializeComponent();
            CurrentVM = new OpenWindowVM();
            this.DataContext = CurrentVM;

        }
    }
}
