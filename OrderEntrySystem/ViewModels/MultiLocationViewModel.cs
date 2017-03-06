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
    public class MultiLocationViewModel : WorkspaceViewModel
    {
        private Repository repositorys;

        public MultiLocationViewModel(Repository repository)
            : base("Multi Local View")
        {
            this.repositorys = repository;

            this.AllLocations = new ObservableCollection<LocationViewModel>();

            // linq
            IEnumerable<LocationViewModel> locations =
    from l in this.repositorys.GetLocations()
    select new LocationViewModel(l, this.repositorys);

            this.AllLocations = new ObservableCollection<LocationViewModel>(locations);

            // linq
            List<LocationViewModel> locationList =
    (from l in this.repositorys.GetLocations() select new LocationViewModel(l, this.repositorys)).ToList();

            locationList.ForEach(pvm => pvm.PropertyChanged += this.OnLocationViewModelPropertyChanged);

            // delegate
            this.repositorys.LocationAdded += this.OnLocationAdded;

        }

        public ObservableCollection<LocationViewModel> AllLocations { get; set; }

        private void OnLocationAdded(object sender, LocationEventArgs e)
        {
            LocationViewModel viewModel = new LocationViewModel(e.Location, repositorys);

            // delegate
            this.PropertyChanged += this.OnLocationViewModelPropertyChanged;


            this.AllLocations.Add(viewModel);
        }

        public int NumberOfItemsSelected
        {
            get
            {
                return this.AllLocations.Count(vm => vm.IsSelected);
            }
        }

        private void OnLocationViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsSelected")
            {
                this.OnPropertyChanged("NumberOfItemsSelected");
            }
        }



        /// <summary>
        /// Creates a new window to create a new customer.
        /// </summary>
        public void CreateNewLocationExecute()
        {
            this.ShowLocation(new LocationViewModel(new Location(), this.repositorys));
        }

        /// <summary>
        /// Creates a new window to edit the currently selected customer.
        /// </summary>
        public void EditLocationExecute()
        {
            try
            {
                LocationViewModel viewModel = this.AllLocations.SingleOrDefault(vm => vm.IsSelected);

                if (viewModel != null)
                {
                    this.ShowLocation(viewModel);
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
        public void ShowLocation(LocationViewModel viewModel)
        {
            WorkspaceWindow window = new WorkspaceWindow();
            window.Width = 400;
            window.Title = viewModel.DisplayName;

            viewModel.CloseAction = b => window.DialogResult = b;

            LocationView view = new LocationView();
            view.DataContext = viewModel;

            window.Content = view;
            window.ShowDialog();

        }

        protected override void CreateCommands()
        {
            this.Commands.Add(new CommandViewModel("New...", new DelegateCommand(p => this.CreateNewLocationExecute())));

            this.Commands.Add(new CommandViewModel("Edit...", new DelegateCommand(p => this.EditLocationExecute())));
        }
    }
}
