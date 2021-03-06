﻿using OrderEntryEngine;
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

        public event EventHandler<CategoryEventArgs> CategoryAdded;

        public event EventHandler<OrderEventArgs> OrderAdded;

        public event EventHandler<OrderLineEventArgs> OrderLineAdded;

        public void AddOrderLine(OrderLine line)
        {
            if (!this.ContainsOrderLine(line))
            {
                this.context.OrderLines.Add(line);

                if (this.OrderLineAdded != null)
                {
                    this.OrderLineAdded(this, new OrderLineEventArgs(line));
                }
            }
        }

        public List<OrderLine> GetOrderLines()
        {
            return this.context.OrderLines.ToList();
        }

        private bool ContainsOrderLine(OrderLine line)
        {
            return this.GetOrderLine(line.Id) != null;
        }

        private OrderLine GetOrderLine(int id)
        {
            return this.context.OrderLines.Find(id);
        }

        public void AddLocation(Location location)
        {
            if (this.ContainsLocation(location) == false)
            {
                this.context.Locations.Add(location);
                if (this.LocationAdded != null)
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

       public void AddCategories(Category category)
        {
            if (this.ContainsCategory(category) == false)
            {
                this.context.Categories.Add(category);

                if (this.CategoryAdded != null)
                {
                    // this.CustomerAdded(customer, new CustomerEventArgs(customer));

                    this.context.Categories.Add(category);
                }
            }
        }

        /// <summary>
        /// Add order
        /// </summary>
        /// <param name="order"></param>
        public void AddOrder(Order order)
        {
            if (this.ContainsOrder(order) == false)
            {
                this.context.Orders.Add(order);

                if (this.OrderAdded != null)
                {
                    this.context.Orders.Add(order);
                }
            }
        }

        public bool ContainsOrder(Order order)
        {
            return this.GetOrder(order.Id) != null;
        }

        private Order GetOrder(int id)
        {
            return this.context.Orders.Find(id);
        }

        public List<Order> GetOrders()
        {
            return this.context.Orders.ToList();
        }
        

       

        public bool ContainsCategory(Category category)
        {
            return this.GetCategory(category.Id) != null;
        }

        private Category GetCategory(int id)
        {
            return this.context.Categories.Find(id);
        }

        public List<Category> GetCategories()
        {
            return this.context.Categories.ToList();
        }

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
