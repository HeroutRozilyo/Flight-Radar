#region using
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
using PFlight.model;
using Microsoft.Maps.MapControl.WPF;
#endregion

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
       public MapP mapP=new MapP();
        ListFlightVP ListFlight;
        public ObservableCollection<string> list = new ObservableCollection<string>();



        public MainWindow()
        {
            InitializeComponent();
            CurrentVM = new screen1VM(mapP.myMap, Resources);
            this.autoSuggestionUserControl.AutoSuggestionList = CurrentVM.getObserverList();
            this.DataContext = CurrentVM;
            // CurrentVM.cleanDB();
            //weatherButton.IsEnabled = false;

            CurrentVM.CallMyMethodEvent += ImagePinMap_MouseDown;
            frame.Navigate(mapP);
         



          
          
            dataFrame.Visibility = Visibility.Collapsed;
           // backData.Visibility = Visibility.Collapsed;

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
            mapP.myMap.Children.Clear();
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
            OpenDataFlight(flightM, flightO);
            /*
            if (flightM != null)
            {
                dataFrame.Navigate(new DataFlightRoot(flightM));
                dataFrame.Visibility = Visibility.Visible;
                //detailsP.DataContext = flightM;
                
            }
            else
                System.Windows.MessageBox.Show("There is a problem loading the data", "My App", MessageBoxButton.OK, MessageBoxImage.Error);
            weatherB(flightM);

            CurrentVM.addFlightDB( flightO);
         
            this.autoSuggestionUserControl.AutoSuggestionList =  CurrentVM.getObserverList();

            if (CurrentVM.cm.CanExecute(flightM))
                CurrentVM.cm.Execute(flightM);
            */

        }

        private void ImagePinMap_MouseDown(object sender, MouseButtonEventArgs e)
        {

            FlightM.Root root=new FlightM.Root();

            FlightData flightData = CurrentVM.flightDataMap;
            
            if(flightData != null)
                 root = CurrentVM.GetRootF(flightData.SourceId);
             OpenDataFlight(root, flightData);
            
            


        }

        private void OpenDataFlight(Root  flightM,FlightData flightO)
        {
            if (flightM != null)
            {
                dataFrame.Navigate(new DataFlightRoot(flightM));
                dataFrame.Visibility = Visibility.Visible;
                //detailsP.DataContext = flightM;

            }
            else
                System.Windows.MessageBox.Show("There is a problem loading the data", "My App", MessageBoxButton.OK, MessageBoxImage.Error);
            weatherB(flightM);

            CurrentVM.addFlightDB(flightO);

            this.autoSuggestionUserControl.AutoSuggestionList = CurrentVM.getObserverList();

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
           // weatherButton.IsEnabled = true;
            lastChoos = f;
        }
       

       
        private void Window_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            

        }

        

        

           

        private void autoSuggestionUserControl_selectedChangeUC(object sender, EventArgs e)
        {
           FlightData flightData=  autoSuggestionUserControl.flightData;
            dataFrame.Visibility = Visibility.Visible;
            dataFrame.Navigate(new ListFlightVP(flightData));
           // backData.Visibility = Visibility.Visible;



        }

        private void History_Click(object sender, RoutedEventArgs e)
        {
            historyVW window=new historyVW();
            window.Show();

        }
       
    }
}
