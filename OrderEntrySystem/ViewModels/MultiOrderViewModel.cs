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

        private Customer customer;

        public MultiOrderViewModel(Repository repository, Customer customer)
            : base("Multi Order")
        {
            this.repositorys = repository;

            this.customer = customer;

            this.Commands.Clear();
            this.CreateCommands();

            this.AllOrders = new ObservableCollection<OrderViewModel>();

            // linq
            IEnumerable<OrderViewModel> orders = from o in this.repositorys.GetOrders() select new OrderViewModel(o, this.repositorys);

            this.AllOrders = new ObservableCollection<OrderViewModel>(orders);

            // linq
            List<OrderViewModel> ordersList =
    (from o in this.repositorys.GetOrders() select new OrderViewModel(o, this.repositorys)).ToList();
            this.AddPropertyChangedEvent(ordersList);

            // delegate
            this.repositorys.OrderAdded += this.OnOrderAdded;
        }

        public void AddPropertyChangedEvent(List<OrderViewModel> ordersList)
        {
            ordersList.ForEach(ovm => ovm.PropertyChanged += this.OnOrderViewModelPropertyChanged);
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
            Order order = new Order();
            order.Customer = customer;
            this.ShowOrder(new OrderViewModel(order, this.repositorys));
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
            if (this.customer != null)
            {
                this.Commands.Add(new CommandViewModel("New...", new DelegateCommand(p => this.CreateNewOrderExecute())));

                this.Commands.Add(new CommandViewModel("Edit...", new DelegateCommand(p => this.EditOrderExecute())));
            }
        }
    }
}
