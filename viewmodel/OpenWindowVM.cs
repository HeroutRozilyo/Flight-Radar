using PFlight.command;
using PFlight.views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PFlight.viewmodel
{
    public class OpenWindowVM : INotifyPropertyChanged
    {
         public ICommand OpenCommand { get; set; }
        private MainWindow wnd;
        private firstWindow firstWnd;
        private BackgroundWorker worker;
        private bool isLoading = false;
        public OpenWindowVM(firstWindow first)
        {
            firstWnd = first;
            OpenCommand = new OpenWindowCommand(this);
           /*
            worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;
            worker.ProgressChanged += (sender, args) =>
            {
                int visibilitySetting = args.ProgressPercentage;
                IsLoading = visibilitySetting == 0 ? false : true;
            };
            worker.DoWork += WorkerDoWork;
           */
        }

        public void openWind()
        {
            wnd = new MainWindow();
            wnd.Show();
            firstWnd.Close();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        /*
        public bool IsLoading
        {
            get
            {
                return isLoading;
            }
            set
            {
                isLoading = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("IsLoading"));

            }
        }
        private ICommand loadCommand;
        public ICommand LoadCommand
        {
            get
            {
                return loadCommand ?? (loadCommand = new OpenWindowCommand(this));
            }
        }

        public void Load()
        {
            worker.RunWorkerAsync();
        }


        private void WorkerDoWork(object sender, DoWorkEventArgs e)
        {
            worker.ReportProgress(1);//shows progress bar
            Thread.Sleep(1500);     //load data
            worker.ReportProgress(0);//hides progress bar
        }
        */
        

    }
}
