using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace OrderEntrySystem
{
    public abstract class WorkspaceViewModel : ViewModel
    {
        private DelegateCommand closeCommand;

        private ObservableCollection<CommandViewModel> commands = new ObservableCollection<CommandViewModel>();

        public WorkspaceViewModel(string displayName)
            : base(displayName)
        {
            this.CreateCommands();
        }

        public event EventHandler RequestClose;

        ////public EventHandler RequestClose(object sender, EventArgs e)
        ////{
        ////    throw new Exception("I guess we closed the event?");
        ////}

        public ICommand CloseCommand
        {
            get
            {
                if (this.closeCommand == null)
                {
                    this.closeCommand = new DelegateCommand(p => this.OnRequestClose());
                }
                return this.closeCommand;
            }
        }

        public ObservableCollection<CommandViewModel> Commands
        {
            get
            {
                return this.commands;
            }
        }

        protected abstract void CreateCommands();

        private void OnRequestClose()
        {
            if (this.RequestClose != null)
            {
                this.RequestClose(this, EventArgs.Empty);
            }
        }
    }
}
