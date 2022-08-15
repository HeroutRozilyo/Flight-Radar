using PFlight.viewmodel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PFlight.command
{
   public class historyCommand : ICommand
    {
        historyVM vm;
        public historyCommand(historyVM v)
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
          //  vm.GetFlightsFromDB();
        }
    }
}
