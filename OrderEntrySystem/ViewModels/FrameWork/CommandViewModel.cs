using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace OrderEntrySystem
{
    public class CommandViewModel : ViewModel
    {
        public CommandViewModel(string displayName, ICommand command) : base(displayName)
        {
            if (command == null)
            {
                throw new Exception("Command was null.");
            }
            this.Command = command;
        }

        public ICommand Command { get; set; }
    }


}
