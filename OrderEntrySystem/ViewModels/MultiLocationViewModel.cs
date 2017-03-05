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



        protected override void CreateCommands()
        {

        }
    }
}
