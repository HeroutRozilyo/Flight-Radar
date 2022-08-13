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
        public static screen1VM CurrentVM { get; set; }
        BackgroundWorker timer;
        private bool isTimerRun { get; set; }
        public MapP()
        {
            InitializeComponent();
            CurrentVM = new screen1VM(myMap, Resources);
            this.DataContext = CurrentVM;
           // CurrentVM.cleanDB();
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
}
}
