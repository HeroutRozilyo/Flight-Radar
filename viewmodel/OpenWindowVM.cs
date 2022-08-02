using PFlight.command;
using PFlight.views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PFlight.viewmodel
{
    public class OpenWindowVM : INotifyPropertyChanged
    {
        public ICommand OpenCommand { get; set; }
        private MainWindow wnd;
        private firstWindow firstWnd;
        public OpenWindowVM(firstWindow first)
        {
            firstWnd = first;
            OpenCommand = new OpenWindowCommand(this);
        }

        public void openWind()
        {
            wnd = new MainWindow();
            wnd.Show();
            firstWnd.Close();
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
