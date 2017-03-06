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
    public class CategoryViewModel : WorkspaceViewModel
    {
        public MultiCategoryViewModel AllCategories;

        private ICommand saveCommand;

        private bool isSelected;

        private Repository repositorys;

        private Category category;

        public CategoryViewModel(Category category, Repository repository)
            : base("Category")
        {
            this.category = category;
            this.repositorys = repository;

        }

        /// <summary>
        /// Gets or sets the Id of the category.
        /// </summary>
        public int Id
        {
            get
            {
                return this.category.Id;
            }
            set
            {
                this.category.Id = value;
                this.OnPropertyChanged("Id");
            }
        }

        /// <summary>
        /// Gets or sets the value indicating if the category is selected or not.
        /// </summary>
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
        /// Gets and sets the categories name.
        /// </summary>
        public string Name
        {
            get
            {
                return this.category.Name;
            }
            set
            {
                this.category.Name = value;
                this.OnPropertyChanged("Name");
            }
        }

        /// <summary>
        /// Gets or sets the categories list of products.
        /// </summary>
        public virtual ICollection<Product> Products
        {
            get
            {
                return this.category.Products;
            }
            set
            {
                this.category.Products = value;
                this.OnPropertyChanged("Products");
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
            this.repositorys.AddCategories(this.category);
            this.repositorys.SaveToDatabase();
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
