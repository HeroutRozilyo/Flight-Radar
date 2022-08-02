using PFlight.viewmodel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PFlight.command
{
    public class WeatherCommand : ICommand
    {
        WeatherVM vm { get; set; }

        public WeatherCommand()
        {
       
        }
        public WeatherCommand(WeatherVM v)
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
            if (parameter != null)
                return true;
            return false;
        }

        public void Execute(object parameter)
        {
           // vm.openWind();
        }
    }
}
