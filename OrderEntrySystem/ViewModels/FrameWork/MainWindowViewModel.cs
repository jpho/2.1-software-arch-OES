using OrderEntryDataAccess;
using OrderEntryEngine;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace OrderEntrySystem
{
    public class MainWindowViewModel : WorkspaceViewModel
    {

        private Repository repositorys;

        private ObservableCollection<WorkspaceViewModel> viewModels;

        

        public MainWindowViewModel(string displayName)
            : base(displayName)
        {
            displayName = "Order Entry System – Yang";
            this.repositorys = new Repository();
        }

        public ObservableCollection<WorkspaceViewModel> ViewModels
        {
            get
            {
                if (this.viewModels == null)
                {
                    this.viewModels = new ObservableCollection<WorkspaceViewModel>();
                }

                return this.viewModels;
            }
        }

        public void CreateNewProduct()
        {
            Product product = new Product { LocationId = 1 };

            ProductViewModel productView = new ProductViewModel(product, repositorys);

            productView.RequestClose += this.OnWorkspaceRequestClose;            

            this.viewModels.Add(productView);

            this.ActivateViewModel(productView);

        }

        public void CreateNewCustomer()
        {
            Customer customer = new Customer();

            CustomerViewModel customerView = new CustomerViewModel(customer, repositorys);

            customerView.RequestClose += this.OnWorkspaceRequestClose;

            this.viewModels.Add(customerView);

            this.ActivateViewModel(customerView);

        }

        public void CreateNewLocation()
        {
            Location location = new Location();

            LocationViewModel locationView = new LocationViewModel(location, repositorys);

            locationView.RequestClose += this.OnWorkspaceRequestClose;

            this.viewModels.Add(locationView);

            this.ActivateViewModel(locationView);
        }

        public void ShowAllLocations()
        {
            MultiLocationViewModel viewModel = this.ViewModels.FirstOrDefault(vm => vm is MultiLocationViewModel) as MultiLocationViewModel;

            if (viewModel == null)
            {
                viewModel = new MultiLocationViewModel(repositorys);
                viewModel.RequestClose += this.OnWorkspaceRequestClose;
                this.ViewModels.Add(viewModel);

            }

            this.ActivateViewModel(viewModel);
        }

      

        private void Test_RequestClose(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void ActivateViewModel(WorkspaceViewModel viewModel)
        {
            ICollectionView collectionView = CollectionViewSource.GetDefaultView(this.viewModels);

            if (collectionView != null)
            {
                collectionView.MoveCurrentTo(viewModel);
            }
        }

        

        private void OnWorkspaceRequestClose(object sender, EventArgs e)
        {
            this.ViewModels.Remove(sender as WorkspaceViewModel);
           // this.viewModels.Remove(sender as CustomerViewModel);
        }


        public void ShowAllProducts()
        {
            MultiProductViewModel viewModel = this.ViewModels.FirstOrDefault(vm => vm is MultiProductViewModel) as MultiProductViewModel;

            if (viewModel == null)
            {
                viewModel = new MultiProductViewModel(repositorys);
                viewModel.RequestClose += this.OnWorkspaceRequestClose;
                this.ViewModels.Add(viewModel);

            }

            this.ActivateViewModel(viewModel);

        }

        public void ShowAllCustomers()
        {
            MultiCustomerViewModel viewModel = this.ViewModels.FirstOrDefault(vm => vm is MultiCustomerViewModel) as MultiCustomerViewModel;

            if (viewModel == null)
            {
                viewModel = new MultiCustomerViewModel(repositorys);
                viewModel.RequestClose += this.OnWorkspaceRequestClose;
                this.ViewModels.Add(viewModel);

            }

            this.ActivateViewModel(viewModel);
        }

        public void ShowAllCategories()
        {
            MultiCategoryViewModel viewModel = this.ViewModels.FirstOrDefault(vm => vm is MultiCategoryViewModel) as MultiCategoryViewModel;

            if (viewModel == null)
            {
                viewModel = new MultiCategoryViewModel(repositorys);
                viewModel.RequestClose += this.OnWorkspaceRequestClose;
                this.ViewModels.Add(viewModel);

            }

            this.ActivateViewModel(viewModel);
        }

        protected override void CreateCommands()
        {

          

            this.Commands.Add(new CommandViewModel("View All Products", new DelegateCommand(p => this.ShowAllProducts())));

            this.Commands.Add(new CommandViewModel("View All Customers", new DelegateCommand(p => this.ShowAllCustomers())));

           this.Commands.Add(new CommandViewModel("View all location", new DelegateCommand(p => this.ShowAllLocations())));

            this.Commands.Add(new CommandViewModel("View all Categories", new DelegateCommand(p => this.ShowAllCategories())));


        }

       

        
    }
}
