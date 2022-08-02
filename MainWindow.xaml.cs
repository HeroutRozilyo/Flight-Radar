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
using PFlight.command;
using PFlight.views;

namespace PFlight
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static screen1VM CurrentVM { get; set; }
        public HolidayVM HolidayVM { get; set; }
        private bool isTimerRun { get; set; }
        private Root lastChoos;
        BackgroundWorker timer;
        WeatherV weatherV;
        public MainWindow()
        {
            InitializeComponent();
            CurrentVM = new screen1VM(myMap, Resources);
            this.DataContext = CurrentVM;
            weatherButton.IsEnabled = false;

            HolidayVM = new HolidayVM();
            dpicker.SelectedDate = DateTime.Today;


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
            weatherB(flightM);
            
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

        private async void dpicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            string date= dpicker.SelectedDate.ToString();
            string result = await  HolidayVM.isHoliday(date);
            textday.Text = result;


        }

        //keep the last choos flight in order to show the weather
        public void weatherB(Root f)
        {
            weatherButton.IsEnabled = true;
            lastChoos = f;
        }

        private void weatherButton_Click(object sender, RoutedEventArgs e)
        {
            weatherV = new WeatherV(lastChoos);
            weatherV.Show();
        }
    }
}
