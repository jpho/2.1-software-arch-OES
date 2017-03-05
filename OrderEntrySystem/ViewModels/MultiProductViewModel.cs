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

       

        protected override void CreateCommands()
        {
           
        }
    }
}
