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
using System.Windows.Threading;

namespace PFlight.views
{
    /// <summary>
    /// Interaction logic for firstWindow.xaml
    /// </summary>
    public partial class firstWindow : Window
    {
        #region Variable
        public OpenWindowVM CurrentVM { get; set; }
        #endregion
        public firstWindow()
        {
            InitializeComponent();
           
            CurrentVM = new OpenWindowVM(this);
            this.DataContext = CurrentVM;

            IProgress<int> p = new Progress<int>((x) =>
            {
                pbStatus.Value = x;
                if (x == 100)
                {
                    
                    this.Dispatcher.Invoke(() =>
                    {
                        var viewModel = (OpenWindowVM)DataContext;
                        if (viewModel.OpenCommand.CanExecute(null))
                            viewModel.OpenCommand.Execute(null);
                    });
                    
                }


            });
            Task.Run(() =>
            {
                
                var x = 0;
                for (int i = 0; i < 10; i++)
                {
                    System.Threading.Thread.Sleep(100);
                    x += 10;
                    p.Report(x); //report back progress 
                }
               

            });

        }

        
        private void Window_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (this.WindowState == WindowState.Normal)
                this.WindowState = WindowState.Maximized;
            else
                this.WindowState = WindowState.Normal;
        }

       
        
       
    }
}
