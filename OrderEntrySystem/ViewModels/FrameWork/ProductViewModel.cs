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
    public class ProductViewModel : WorkspaceViewModel
    {
        public MultiProductViewModel AllProducts;

        private bool isSelected;

        private Product product;

        private Repository repositorys;

        public ICommand saveCommand;


        public ProductViewModel(Product product, Repository repository)
            : base("Product")
            
        {
            this.product = product;
            this.repositorys = repository;
            
        }

        public string Location
        {
            get
            {
                return this.product.Location;
            }
            set
            {
                this.product.Location = value;
                this.OnPropertyChanged("Location");
            }
        }

        public string Name
        {
            get
            {
                return this.product.Name;
            }
            set
            {
                this.product.Name = value;
                this.OnPropertyChanged("Name");
            }
        }

        public string Description
        {
            get
            {
                return this.product.Description;
            }
            set
            {
                this.product.Description = value;
                this.OnPropertyChanged("Description");
            }
        }

        public decimal Price
        {
            get
            {
                return this.product.Price;
            }
            set
            {
                this.product.Price = value;
                this.OnPropertyChanged("Price");
            }
        }

        public ICommand SaveCommand
        {
            get
            {
                if (this.saveCommand == null)
                {
                    this.saveCommand = new DelegateCommand(p => this.Save());
                }
                return this.saveCommand;
            }
        }

        public void Save()
        {
            this.repositorys.AddProduct(this.product);
        }


        protected override void CreateCommands()
        {
            ////throw new Exception("Didn't do this part yet.");
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


    }
}
