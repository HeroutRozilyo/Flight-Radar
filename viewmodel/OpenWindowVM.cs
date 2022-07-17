using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PFlight.viewmodel
{
    public class OpenWindowVM : INotifyPropertyChanged
    {
        public command.OpenWindowCommand command { get; set; }
        public OpenWindowVM()
        {
            command = new command.OpenWindowCommand();

        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
