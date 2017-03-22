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
    public class MultiOrderLineViewModel : WorkspaceViewModel
    {
        
        private Repository repositorys;

        
        private Order order;

        
        public MultiOrderLineViewModel(Repository repository, Order order)
            : base("All Order Lines")
        {
            this.repositorys = repository;
            this.order = order;

            this.Commands.Clear();
            this.CreateCommands();

            List<OrderLineViewModel> orderLines =
                (from ol in this.repositorys.GetOrderLines()
                 select new OrderLineViewModel(ol, this.repositorys)).ToList();

            this.AddPropertyChangedEvent(orderLines);

            this.AllOrderLines = new ObservableCollection<OrderLineViewModel>(orderLines);

            this.repositorys.OrderLineAdded += this.OnOrderLineAdded;
        }

        
        public ObservableCollection<OrderLineViewModel> AllOrderLines { get; set; }

        
        public int NumberOfItemsSelected
        {
            get
            {
                return this.AllOrderLines.Count(vm => vm.IsSelected);
            }
        }

        
        public void AddPropertyChangedEvent(List<OrderLineViewModel> orderLines)
        {
            orderLines.ForEach(olvm => olvm.PropertyChanged += this.OnOrderLineViewModelPropertyChanged);
        }

       
        protected override void CreateCommands()
        {
            this.Commands.Add(new CommandViewModel("New...", new DelegateCommand(p => this.CreateNewOrderLineExecute())));
            this.Commands.Add(new CommandViewModel("Edit...", new DelegateCommand(p => this.EditOrderLineExecute())));
        }

       
        private void OnOrderLineAdded(object sender, OrderLineEventArgs e)
        {
            OrderLineViewModel viewModel = new OrderLineViewModel(e.OrderLine, this.repositorys);

            viewModel.PropertyChanged += this.OnOrderLineViewModelPropertyChanged;

            this.AllOrderLines.Add(viewModel);
        }

        
        private void OnOrderLineViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsSelected")
            {
                this.OnPropertyChanged("NumberOfItemsSelected");
            }
        }

       
        private void CreateNewOrderLineExecute()
        {
            OrderLine orderLine = new OrderLine { Order = this.order };
            OrderLineViewModel viewModel = new OrderLineViewModel(orderLine, this.repositorys);
            this.ShowOrderLine(viewModel);
        }

       
        private void ShowOrderLine(OrderLineViewModel viewModel)
        {
            WorkspaceWindow window = new WorkspaceWindow();
            window.Width = 400;
            window.Title = viewModel.DisplayName;

            viewModel.CloseAction = b => window.DialogResult = b;

            OrderLineView view = new OrderLineView();
            view.DataContext = viewModel;

            window.Content = view;
            window.ShowDialog();
        }

      
        private void EditOrderLineExecute()
        {
            try
            {
                OrderLineViewModel viewModel = this.AllOrderLines.SingleOrDefault(vm => vm.IsSelected);

                if (viewModel != null)
                {
                    this.ShowOrderLine(viewModel);
                }
                else
                {
                    MessageBox.Show("Please select one order line");
                }
            }
            catch
            {
                MessageBox.Show("Please select only one order line");
            }
        }
    }
}
