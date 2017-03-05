using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace OrderEntrySystem
{
    public class DelegateCommand : ICommand
    {

        private readonly Action<object> command;

        public DelegateCommand(Action<object> command)
        {
            if (command == null)
            {
                throw new Exception("Command was null");
            }
            this.command = command;

        }

        public event EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
            }
            remove
            {
                CommandManager.RequerySuggested -= value;
            }
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            this.command(parameter);
        }


        ////public event EventHandler CanExecuteChanged;

        ////public bool CanExecute(object parameter)
        ////{
        ////    throw new NotImplementedException();
        ////}

        ////public void Execute(object parameter)
        ////{
        ////    throw new NotImplementedException();
        ////}
    }
}
