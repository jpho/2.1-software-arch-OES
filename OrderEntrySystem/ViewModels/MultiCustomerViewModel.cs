using OrderEntryDataAccess;
using OrderEntryEngine;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace OrderEntrySystem
{
   public class MultiCustomerViewModel : WorkspaceViewModel
    {
        private Repository repositorys;

        public MultiCustomerViewModel(Repository repository)
            : base("Multi CUSTOMER View")
        {
            this.repositorys = repository;

            this.AllCustomers = new ObservableCollection<CustomerViewModel>();

            // linq
            IEnumerable<CustomerViewModel> products = from c in this.repositorys.GetCustomers() select new CustomerViewModel(c, this.repositorys);

            this.AllCustomers = new ObservableCollection<CustomerViewModel>(products);

            // linq
            List<CustomerViewModel> productsList =
    (from p in this.repositorys.GetCustomers() select new CustomerViewModel(p, this.repositorys)).ToList();

            productsList.ForEach(pvm => pvm.PropertyChanged += this.OnCustomerViewModelPropertyChanged);

            // delegate
            this.repositorys.CustomerAdded += this.OnCustomerAdded;

        }

        public ObservableCollection<CustomerViewModel> AllCustomers { get; set;}

        private void OnCustomerAdded(object sender, CustomerEventArgs e)
        {
            CustomerViewModel viewModel = new CustomerViewModel(e.Customer, repositorys);
            this.AllCustomers.Add(viewModel);
        }

        public int NumberOfItemsSelected
        {
            get
            {
                return this.AllCustomers.Count(vm => vm.IsSelected);
            }
        }


        private void OnCustomerViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsSelected")
            {
                this.OnPropertyChanged("NumberOfCustomersSelected");
            }
        }

        /// <summary>
        /// Creates a new window to create a new customer.
        /// </summary>
        public void CreateNewCustomerExecute()
        {
            this.ShowCustomer(new CustomerViewModel(new Customer(), this.repositorys));
        }

        /// <summary>
        /// Creates a new window to edit the currently selected customer.
        /// </summary>
        public void EditCustomerExecute()
        {
            try
            {
                CustomerViewModel viewModel = this.AllCustomers.SingleOrDefault(vm => vm.IsSelected);

                if (viewModel != null)
                {
                    this.ShowCustomer(viewModel);
                    this.repositorys.SaveToDatabase();
                }
                else if (viewModel == null)
                {
                    MessageBox.Show("Please select an item first.");
                }


            }
            catch (InvalidOperationException)
            {
                MessageBox.Show("Select 1 item only.");
            }
        }

        /// <summary>
        /// Shows the currently selected customer in a new window.
        /// </summary>
        public void ShowCustomer(CustomerViewModel viewModel)
        {
            WorkspaceWindow window = new WorkspaceWindow();
            window.Width = 400;
            window.Height = 367;
            window.Title = viewModel.DisplayName;

            viewModel.CloseAction = b => window.DialogResult = b;

            CustomerView view = new CustomerView();
            view.DataContext = viewModel;

            window.Content = view;
            window.ShowDialog();

        }

        protected override void CreateCommands()
        {
            this.Commands.Add(new CommandViewModel("New...", new DelegateCommand(p => this.CreateNewCustomerExecute())));

            this.Commands.Add(new CommandViewModel("Edit...", new DelegateCommand(p => this.EditCustomerExecute())));
        }
    }
}
