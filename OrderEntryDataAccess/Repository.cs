using OrderEntryEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderEntryDataAccess
{
    public class Repository
    {
        public OrderEntryContext context = new OrderEntryContext();

        public event EventHandler<LocationEventArgs> LocationAdded;

        public event EventHandler<ProductEventArgs> ProductAdded;

        public event EventHandler<CustomerEventArgs> CustomerAdded;

        private List<Customer> customers = new List<Customer>();

        private List<Product> products = new List<Product>();

        private List<Location> locations = new List<Location>();

        public void AddLocation(Location location)
        {
            if (this.ContainsLocation(location) == false)
            {
                this.locations.Add(location);
                if (this.ProductAdded != null)
                {
                    this.LocationAdded(location, new LocationEventArgs(location));
                }
            }
        }

        private bool ContainsLocation(Location location)
        {
            return this.GetLocation(location.Id) != null;
        }

        public void AddProduct(Product product)
        {
            if (this.ContainsProduct(product) == false)
            {
                this.products.Add(product);
                if (this.ProductAdded != null)
                {
                    this.ProductAdded(product, new ProductEventArgs(product));
                }
            }
        }

        public void AddCustomer(Customer customer)
        {
            if (this.ContainsCustomer(customer) == false)
            {
                this.customers.Add(customer);
                if (this.CustomerAdded != null)
                {
                    this.CustomerAdded(customer, new CustomerEventArgs(customer));
                }
            }
        }

        private bool ContainsCustomer(Customer customer)
        {
            return this.GetCustomer(customer.Id) != null;
        }

        private bool ContainsProduct(Product product)
        {
            return this.GetProduct(product.Id) != null;

        }

        ////public Repository()
        ////{
                                
        ////}

        public List<Product> GetProducts()
        {
            return this.products;
        }

        public List<Customer> GetCustomers()
        {
            return this.customers;
        }

        public List<Location> GetLocations()
        {
            return this.locations;
        }

        public void SaveToDatabase()
        {

        }

        private Product GetProduct(int id)
        {
            return this.context.Products.Find(id);
        }

        private Customer GetCustomer(int id)
        {
            return this.context.Customers.Find(id);
        }

        private Location GetLocation(int id)
        {
            return this.context.Locations.Find(id);
        }

    }
}
