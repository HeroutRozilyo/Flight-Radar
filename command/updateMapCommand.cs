using System;
using System.Windows.Input;

namespace PFlight.command
{
    public class updateMapCommand : ICommand
    {
        public event Action<FlightModel.FlightM.Root> UpdateMap;
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
            try
            {
                var temp = (FlightModel.FlightM.Root)parameter;
            if (UpdateMap != null)
                    UpdateMap(temp);
            }
            catch(Exception e)
            {

            }
        }       
    }
}
