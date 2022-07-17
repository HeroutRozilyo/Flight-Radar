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

        }
    }
}
