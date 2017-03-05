using OrderEntryDataAccess;
using OrderEntryEngine;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        protected override void CreateCommands()
        {

        }
    }
}
