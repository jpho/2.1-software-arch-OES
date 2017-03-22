using OrderEntryDataAccess;
using OrderEntryEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderEntrySystem
{
   public class OrderLineViewModel : WorkspaceViewModel
    {
        /// <summary>
        /// The order line.
        /// </summary>
        private OrderLine line;

        
        private Repository repositorys;

        
        private bool isSelected;

        
        public OrderLineViewModel(OrderLine line, Repository repository)
            : base("Order Line")
        {
            this.line = line;
            this.repositorys = repository;
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

        
        public Product Product
        {
            get
            {
                return this.line.Product;
            }

            set
            {
                this.line.Product = value;
                this.OnPropertyChanged("Product");
            }
        }

        
        public IEnumerable<Product> Products
        {
            get
            {
                return this.repositorys.GetProducts();
            }
        }

        
        public int Quantity
        {
            get
            {
                return this.line.Quantity;
            }

            set
            {
                this.line.Quantity = value;
                this.OnPropertyChanged("Quantity");
            }
        }

       
        protected override void CreateCommands()
        {
            this.Commands.Add(new CommandViewModel("OK", new DelegateCommand(p => this.OkExecute())));
            this.Commands.Add(new CommandViewModel("Cancel", new DelegateCommand(p => this.CancelExecute())));
        }

       
        private void Save()
        {
            this.repositorys.AddOrder(this.line.Order);
            this.repositorys.AddOrderLine(this.line);
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
    }
}
