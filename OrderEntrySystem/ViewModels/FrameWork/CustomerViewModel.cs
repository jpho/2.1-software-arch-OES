using OrderEntryDataAccess;
using OrderEntryEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace OrderEntrySystem
{
    public class CustomerViewModel : WorkspaceViewModel
    {
        private bool isSelected;

        private Customer customer;

        private Repository repositorys;

        private ICommand saveCommand;

        public CustomerViewModel(Customer customer, Repository repository)
            : base("Customer")
        {
            this.customer = customer;
            this.repositorys = repository;
        }

        public string FirstName
        {
            get
            {
                return this.customer.FirstName;
            }
            set
            {
                this.customer.FirstName = value;
                this.OnPropertyChanged("FirstName");
            }
        }

        public string LastName
        {
            get
            {
                return this.customer.LastName;
            }
            set
            {
                this.customer.LastName = value;
                this.OnPropertyChanged("LastName");
            }
        }

        public string Phone
        {
            get
            {
                return this.customer.Phone;
            }
            set
            {
                this.customer.Phone = value;
                this.OnPropertyChanged("Phone");
            }
        }

        public string Email
        {
            get
            {
                return this.customer.Email;
            }
            set
            {
                this.customer.Email = value;
                this.OnPropertyChanged("Email");
            }
        }

        public string Address
        {
            get
            {
                return this.customer.Address;
            }
            set
            {
                this.customer.Address = value;
                this.OnPropertyChanged("Address");
            }
        }

        public string City
        {
            get
            {
                return this.customer.City;
            }
            set
            {
                this.customer.City = value;
                this.OnPropertyChanged("City");
            }
        }

        public string State
        {
            get
            {
                return this.customer.State;
            }
            set
            {
                this.customer.State = value;
                this.OnPropertyChanged("State");
            }
        }

        public ICommand SaveCommand
        {
            get
            {
                if (this.saveCommand == null)
                {
                    this.saveCommand = new DelegateCommand(c => this.Save());
                }
                return this.saveCommand;
            }
        }

        public void Save()
        {
            this.repositorys.AddCustomer(this.customer);
            this.repositorys.SaveToDatabase();
        }

        public bool IsSelected
        {
            get
            {
                return this.isSelected;
            }
            set
            {
                this.isSelected = value;
                this.OnPropertyChanged("IsSelected");
            }
        }

        /// <summary>
        /// Saves the changes in the window.
        /// </summary>
        private void OkExecute()
        {
            this.Save();
            this.CloseAction(true);
        }

        /// <summary>
        /// Discards the changes in the window.
        /// </summary>
        private void CancelExecute()
        {
            this.CloseAction(false);

        }

        /// <summary>
        /// The create commands associated with the class.
        /// </summary>
        protected override void CreateCommands()
        {
            this.Commands.Add(new CommandViewModel("OK", new DelegateCommand(p => this.OkExecute())));

            this.Commands.Add(new CommandViewModel("Cancel", new DelegateCommand(p => this.CancelExecute())));
        }
    }
}
