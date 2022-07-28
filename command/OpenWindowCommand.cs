using PFlight.viewmodel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PFlight.command
{
    public class OpenWindowCommand : ICommand
    {
        OpenWindowVM vm;
        public OpenWindowCommand(OpenWindowVM v)
        {
            vm = v;
        }
        public event EventHandler CanExecuteChanged
        {
            add
            { CommandManager.RequerySuggested += value; }

            remove
            { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {           
                return true;         
        }

        public void Execute(object parameter)
        {
            vm.openWind();
        }
    }
}
