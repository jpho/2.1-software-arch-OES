using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderEntryEngine
{
    public class OrderEventArgs
    {
        public OrderEventArgs(Order order)
        {
            this.Order = order;
        }

        public Order Order { get; private set; }
    }
}
