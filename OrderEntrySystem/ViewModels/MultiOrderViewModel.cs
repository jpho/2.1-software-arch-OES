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
    public class MultiOrderViewModel : WorkspaceViewModel
    {
        private Repository repositorys;

        public MultiOrderViewModel(Repository repository)
            : base("Multi Order")
        {
            this.repositorys = repository;

            this.AllOrders = new ObservableCollection<OrderViewModel>();

            // linq
            IEnumerable<OrderViewModel> orders = from o in this.repositorys.GetOrders() select new OrderViewModel(o, this.repositorys);

            this.AllOrders = new ObservableCollection<OrderViewModel>(orders);

            // linq
            List<OrderViewModel> ordersList =
    (from o in this.repositorys.GetOrders() select new OrderViewModel(o, this.repositorys)).ToList();

            ordersList.ForEach(ovm => ovm.PropertyChanged += this.OnOrderViewModelPropertyChanged);

            // delegate
            this.repositorys.OrderAdded += this.OnOrderAdded;
        }

        /// <summary>
        /// 
        /// </summary>
        public ObservableCollection<OrderViewModel> AllOrders { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int NumberOfItemsSelected
        {
            get
            {
                return this.AllOrders.Count(vm => vm.IsSelected);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnOrderAdded(object sender, OrderEventArgs e)
        {
            OrderViewModel viewModel = new OrderViewModel(e.Order, repositorys);
            this.AllOrders.Add(viewModel);
        }

        private void OnOrderViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsSelected")
            {
                this.OnPropertyChanged("NumberOfOrdersSelected");
            }
        }

        private void CreateNewOrderExecute()
        {
            this.ShowOrder(new OrderViewModel(new Order(), this.repositorys));
        }

        private void EditOrderExecute()
        {
            try
            {
                OrderViewModel viewModel = this.AllOrders.SingleOrDefault(vm => vm.IsSelected);

                if (viewModel != null)
                {
                    this.ShowOrder(viewModel);
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

        private void ShowOrder(OrderViewModel viewModel)
        {
            WorkspaceWindow window = new WorkspaceWindow();
            window.Width = 400;
            window.Title = viewModel.DisplayName;

            viewModel.CloseAction = b => window.DialogResult = b;

            OrderView view = new OrderView();
            view.DataContext = viewModel;

            window.Content = view;
            window.ShowDialog();
        }

        protected override void CreateCommands()
        {
           // this.Commands.Add(new CommandViewModel("New...", new DelegateCommand(p => this.CreateNewOrderExecute())));

            this.Commands.Add(new CommandViewModel("Edit...", new DelegateCommand(p => this.EditOrderExecute())));
        }
    }
}
