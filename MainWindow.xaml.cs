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
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static FlightModel.FlightM;
using System.Threading;
using System.ComponentModel;

namespace PFlight
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static screen1VM CurrentVM { get; set; }
        private bool isTimerRun { get; set; }  
        BackgroundWorker timer;

        public MainWindow()
        {
            InitializeComponent();
            CurrentVM = new screen1VM(myMap, Resources);
            this.DataContext = CurrentVM;

            timer = new BackgroundWorker();
            isTimerRun = true;
            timer.DoWork += Timer_DoWork;
            timer.ProgressChanged += Timer_ProgressChanged;
            timer.WorkerReportsProgress = true;
            timer.RunWorkerAsync();
          
        }

        private void Timer_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            myMap.Children.Clear();
            CurrentVM.getUrlF();
            CurrentVM.chooseFlight();
        }

        private void Timer_DoWork(object sender, DoWorkEventArgs e)
        {
            while (isTimerRun)
                try 
                {
                    timer.ReportProgress(1);
                    Thread.Sleep(15000);                  
                }
                catch (ThreadInterruptedException) { }
        }

        private void inlist_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var list = (System.Windows.Controls.ListView)sender; //to get the line

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




        private void Window_Closing(object sender, CancelEventArgs e)
        {
            if (timer.IsBusy)
            {
                timer.CancelAsync();
            }

        }

       


    }
}
