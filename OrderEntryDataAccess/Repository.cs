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


        public void AddLocation(Location location)
        {
            if (this.ContainsLocation(location) == false)
            {
                this.context.Locations.Add(location);
                if (this.ProductAdded != null)
                {
                    // this.LocationAdded(location, new LocationEventArgs(location));
                    this.context.Locations.Add(location);
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
                this.context.Products.Add(product);

                if (this.ProductAdded != null)
                {
                    // this.ProductAdded(product, new ProductEventArgs(product));
                    this.context.Products.Add(product);
                }
            }
        }

        public void AddCustomer(Customer customer)
        {
            if (this.ContainsCustomer(customer) == false)
            {
                this.context.Customers.Add(customer);

                if (this.CustomerAdded != null)
                {
                   // this.CustomerAdded(customer, new CustomerEventArgs(customer));

                    this.context.Customers.Add(customer);
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
            return this.context.Products.ToList();
        }

        public List<Customer> GetCustomers()
        {
            return this.context.Customers.ToList();
        }

        public List<Location> GetLocations()
        {
            return this.context.Locations.ToList();
        }

        public void SaveToDatabase()
        {
            this.context.SaveChanges();
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
