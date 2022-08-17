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
        public OpenWindowVM CurrentVM { get; set; }
         
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
                    //update data grid here
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
                //Your heavy tasks can go here 
                var x = 0;
                for (int i = 0; i < 10; i++)
                {
                    System.Threading.Thread.Sleep(100);
                    x += 10;
                    p.Report(x); //report back progress 
                }
               

            });

        }

        /*
        private void Window_ContentRendered(object sender, EventArgs e)
        {
            BackgroundWorker worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;
            worker.DoWork += worker_DoWork;
            worker.ProgressChanged += worker_ProgressChanged;

            worker.RunWorkerAsync();

        }
        void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            for (int i = 0; i < 100; i++)
            {
                (sender as BackgroundWorker).ReportProgress(i);
                Thread.Sleep(30);
            }
            this.Dispatcher.Invoke(() =>
            {
                var viewModel = (OpenWindowVM)DataContext;
              //  if (viewModel.OpenCommand.CanExecute(null))
                  //  viewModel.OpenCommand.Execute(null);
            });
           
        }

        void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            pbStatus.Value = e.ProgressPercentage;
        }
        */
        private void Window_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (this.WindowState == WindowState.Normal)
                this.WindowState = WindowState.Maximized;
            else
                this.WindowState = WindowState.Normal;
        }

        private void pbStatus_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }
        
       
    }
}
