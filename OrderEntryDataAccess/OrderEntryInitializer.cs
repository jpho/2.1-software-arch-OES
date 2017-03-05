using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using OrderEntryEngine;

namespace OrderEntryDataAccess
{
    public class OrderEntryInitializer : DropCreateDatabaseAlways<OrderEntryContext>
    {
        protected override void Seed(OrderEntryContext context)
        {
            var products = new List<Product>
                    {
                        new Product { Name = "apple" },
                        new Product { Name = "bananna" },
                        new Product { Name = "cherry" }
                    };

            context.Products.AddRange(products);
            context.SaveChanges();



            var customers = new List<Customer>
                    {
                        new Customer { FirstName = "Nancy" },
                        new Customer { FirstName = "Joseph" },
                        new Customer { FirstName = "Dia" }
                    };

            context.Customers.AddRange(customers);
            context.SaveChanges();

            var locations = new List<Location>
            {
                new Location { Name="Mars" },
                new Location { Name="Earth" },
                new Location { Name="Moon" },
            };
            context.Locations.AddRange(locations);
            context.SaveChanges();
        }
    }
}
