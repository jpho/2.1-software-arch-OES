using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using OrderEntryEngine;

namespace OrderEntryDataAccess
{
    public class OrderEntryInitializer : // DropCreateDatabaseAlways<OrderEntryContext>
         DropCreateDatabaseIfModelChanges<OrderEntryContext>
    {
        protected override void Seed(OrderEntryContext context)
        {

            var locations = new List<Location>
            {
                new Location { Name="Mars", Id=1 },
                new Location { Name="Earth", Id=2 },
                new Location { Name="Moon", Id=3 },
            };
            context.Locations.AddRange(locations);
            context.SaveChanges();

            var categories = new List<Category>
            {
                new Category { Name = "New", Id = 1 },
                new Category { Name = "Pre-Owned" , Id = 2},
                new Category { Name = "Rare" , Id = 3}
            };

            context.Categories.AddRange(categories);
            context.SaveChanges();


            var products = new List<Product>
                    {
                        new Product { Name = "apple", LocationId = 2 , Description="red", Condition=Condition.Average, Price= 1, CategoryId = 1},
                        new Product { Name = "bananna" , LocationId = 1, CategoryId = 2},
                        new Product { Name = "cherry" , LocationId = 3, CategoryId = 3}
                    };

            context.Products.AddRange(products);
            context.SaveChanges();



            var customers = new List<Customer>
                    {
                        new Customer { FirstName = "Nancy" , LastName = "Lee",  Id = 1},
                        new Customer { FirstName = "Joseph", LastName = "Yang", Id = 2 },
                        new Customer { FirstName = "Dia" , LastName = "Frampton", Id = 3}
                    };

            context.Customers.AddRange(customers);
            context.SaveChanges();

            var orders = new List<Order>
            {
                new Order { Id = 1, CustomerId = 3, Status = OrderStatus.Shipped },
                new Order {  Id = 2, CustomerId = 2, Status = OrderStatus.Placed},
                new Order { Id = 3, CustomerId = 1, Status = OrderStatus.Pending }
            };

            context.Orders.AddRange(orders);
            context.SaveChanges();

            
        }
    }
}
