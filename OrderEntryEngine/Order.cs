using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderEntryEngine
{
    /// <summary>
    /// The class used to represent an Order.
    /// </summary>
    public class Order
    {
        /// <summary>
        /// The ID of the order.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The customerId of the order.
        /// </summary>
        public int CustomerId { get; set; }

        /// <summary>
        /// The customer associated with the order.
        /// </summary>
        public virtual Customer Customer { get; set; }

        /// <summary>
        /// Gets or sets the orders status.
        /// </summary>
        public OrderStatus Status { get; set; }
    }
}
