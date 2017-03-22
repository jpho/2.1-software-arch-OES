using OrderEntryDataAccess;
using OrderEntryEngine;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderEntrySystem
{
    public class OrderViewModel : WorkspaceViewModel
    {
        private Order order;

        private Repository repositorys;

        private bool isSelected;

        private MultiOrderLineViewModel filteredLineViewModel;

        public OrderViewModel(Order order, Repository repository)
            : base("Order")
        {
            this.order = order;
            this.repositorys = repository;
            this.order.Lines = new List<OrderLine>();

            this.filteredLineViewModel = new MultiOrderLineViewModel(this.repositorys, this.order);
            this.filteredLineViewModel.AllOrderLines = this.FilteredLines;


            // this.order.CustomerId = 1;
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
        

        public OrderStatus Status
        {
            get
            {
                return this.order.Status;
            }
            set
            {
                this.order.Status = value;
                this.OnPropertyChanged("Status");
            }
        }


        public Customer Customer
        {
            get
            {
                return this.order.Customer;
            }
            set
            {
                this.order.Customer = value;
                this.OnPropertyChanged("Customer");
            }
        }

        public IEnumerable<OrderStatus> OrderStatuses
        {
            get
            {
                return Enum.GetValues(typeof(OrderStatus))  as IEnumerable<OrderStatus>;
            }
        }

        public MultiOrderLineViewModel FilteredLineViewModel
        {

            get
            {
                return this.filteredLineViewModel;
            }
        }

        public ObservableCollection<OrderLineViewModel> FilteredLines
        {
            get
            {
                var orderLines =
                     (from ol in this.order.Lines
                      select new OrderLineViewModel(ol, this.repositorys)).ToList();

                this.FilteredLineViewModel.AddPropertyChangedEvent(orderLines);

                return new ObservableCollection<OrderLineViewModel>(orderLines);
            }

            

        }
       

        private void Save()
        {
            this.repositorys.AddOrder(this.order);
            this.repositorys.SaveToDatabase();
        }

        private void OkExecute()
        {
            this.Save();
            this.CloseAction(true);
        }

        private void CancelExecute()
        {
            this.CloseAction(false);
        }

        protected override void CreateCommands()
        {
            this.Commands.Add(new CommandViewModel("OK", new DelegateCommand(p => this.OkExecute())));

            this.Commands.Add(new CommandViewModel("Cancel", new DelegateCommand(p => this.CancelExecute())));
        }
    }
}
