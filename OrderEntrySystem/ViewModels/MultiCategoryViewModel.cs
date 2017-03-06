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
    public class MultiCategoryViewModel : WorkspaceViewModel
    {
        private Repository repositorys;

        public MultiCategoryViewModel(Repository repository)
            : base("Multi Category View")
        {
            this.repositorys = repository;

            this.AllCategories = new ObservableCollection<CategoryViewModel>();

            // linq
            IEnumerable<CategoryViewModel> categories =
    from c in this.repositorys.GetCategories()
    select new CategoryViewModel(c, this.repositorys);

            this.AllCategories = new ObservableCollection<CategoryViewModel>(categories);

            // linq
            List<CategoryViewModel> categoriesList =
    (from c in this.repositorys.GetCategories() select new CategoryViewModel(c, this.repositorys)).ToList();

            categoriesList.ForEach(pvm => pvm.PropertyChanged += this.OnCategoryViewModelPropertyChanged);

            // delegate
            this.repositorys.CategoryAdded += this.OnCategoryAdded;

        }

        /// <summary>
        /// Gets or sets the list of all categories.
        /// </summary>
        public ObservableCollection<CategoryViewModel> AllCategories { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnCategoryAdded(object sender, CategoryEventArgs e)
        {
            CategoryViewModel viewModel = new CategoryViewModel(e.Category, repositorys);

            // delegate
            this.PropertyChanged += this.OnCategoryViewModelPropertyChanged;


            this.AllCategories.Add(viewModel);
        }

        public int NumberOfItemsSelected
        {
            get
            {
                return this.AllCategories.Count(vm => vm.IsSelected);
            }
        }

        private void OnCategoryViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsSelected")
            {
                this.OnPropertyChanged("NumberOfItemsSelected");
            }
        }



        /// <summary>
        /// Creates a new window to create a new customer.
        /// </summary>
        public void CreateNewCategoryExecute()
        {
            this.ShowCategory(new CategoryViewModel(new Category(), this.repositorys));
        }

        /// <summary>
        /// Creates a new window to edit the currently selected customer.
        /// </summary>
        public void EditCategoryExecute()
        {
            try
            {
                CategoryViewModel viewModel = this.AllCategories.SingleOrDefault(vm => vm.IsSelected);

                if (viewModel != null)
                {
                    this.ShowCategory(viewModel);
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
        public void ShowCategory(CategoryViewModel viewModel)
        {
            WorkspaceWindow window = new WorkspaceWindow();
            window.Width = 400;
            window.Title = viewModel.DisplayName;

            viewModel.CloseAction = b => window.DialogResult = b;

            CategoryView view = new CategoryView();
            view.DataContext = viewModel;

            window.Content = view;
            window.ShowDialog();

        }

        protected override void CreateCommands()
        {
            this.Commands.Add(new CommandViewModel("New...", new DelegateCommand(p => this.CreateNewCategoryExecute())));

            this.Commands.Add(new CommandViewModel("Edit...", new DelegateCommand(p => this.EditCategoryExecute())));
        }
    }
}
