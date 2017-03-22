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
    public class MultiProductViewModel : WorkspaceViewModel
    {
        private Repository repositorys;

        public MultiProductViewModel(Repository repository)
            : base("Multi PRODUCT View")
        {
            this.repositorys = repository;

            this.AllProducts = new ObservableCollection<ProductViewModel>();

            // linq
            IEnumerable<ProductViewModel> products =
    from p in this.repositorys.GetProducts()
    select new ProductViewModel(p, this.repositorys);

            this.AllProducts = new ObservableCollection<ProductViewModel>(products);

            // linq
            List<ProductViewModel> productsList =
    (from p in this.repositorys.GetProducts() select new ProductViewModel(p, this.repositorys)).ToList();

            productsList.ForEach(pvm => pvm.PropertyChanged += this.OnProductViewModelPropertyChanged);

            // delegate
            this.repositorys.ProductAdded += this.OnProductAdded;

        }

        public ObservableCollection<ProductViewModel> AllProducts { get; set; }

        private void OnProductAdded(object sender, ProductEventArgs e)
        {
            ProductViewModel viewModel = new ProductViewModel(e.Product, repositorys);

            // delegate
            this.PropertyChanged += this.OnProductViewModelPropertyChanged;
            

            this.AllProducts.Add(viewModel);
        }

        public int NumberOfItemsSelected
        {
            get
            {
                return this.AllProducts.Count(vm => vm.IsSelected);
            }
        }

        private void OnProductViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsSelected")
            {
                this.OnPropertyChanged("NumberOfItemsSelected");
            }
        }

        /// <summary>
        /// Executes the create a new product command.
        /// </summary>
       private void CreateNewProductExecute()
        {
            this.ShowProduct(new ProductViewModel(new Product(), this.repositorys));
        }


        private void EditProductExecute()
        {
            try
            {
                ProductViewModel viewModel = this.AllProducts.SingleOrDefault(vm => vm.IsSelected);

                if (viewModel != null)
                {
                    this.ShowProduct(viewModel);
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
        /// Shows the product in a new window.
        /// </summary>
        /// <param name="viewModel">The type of viewModel to show.</param>
        private void ShowProduct(ProductViewModel viewModel)
        {
            WorkspaceWindow window = new WorkspaceWindow();
            window.Width = 400;
            window.Title = viewModel.DisplayName;

            viewModel.CloseAction = b => window.DialogResult = b;

            ProductView view = new ProductView();
            view.DataContext = viewModel;

            window.Content = view;
            window.ShowDialog();
        }

        protected override void CreateCommands()
        {
            this.Commands.Add(new CommandViewModel("New...", new DelegateCommand(p => this.CreateNewProductExecute())));

            this.Commands.Add(new CommandViewModel("Edit...", new DelegateCommand(p => this.EditProductExecute())));


        }
    }
}
