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


        
        public virtual Category Category
        {
            get
            {
                return this.product.Category;
            }
            set
            {
                this.product.Category = value;
                this.OnPropertyChanged("Category");
            }
        }

        public IEnumerable<Category> Categories
        {
            get
            {
                return this.repositorys.GetCategories();
            }
        }



        public virtual Location Location {
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

        public IEnumerable<Location> Locations
        {
            get
            {
                return this.repositorys.GetLocations();
            }
        }


        public ProductViewModel(Product product, Repository repository)
            : base("Product")
            
        {
            this.product = product;
            this.repositorys = repository;
            
        }

       
        public Condition Condition
        {
            get
            {
                return this.product.Condition;
            }
            set
            {
                this.product.Condition = value;
                this.OnPropertyChanged("Condition");
            }
        }

        public IEnumerable<Condition> Conditions
        {
           get
            {
                return Enum.GetValues(typeof(Condition)) as IEnumerable<Condition>;
            }
        }

       /// <summary>
       /// The name of the product.
       /// </summary>
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
