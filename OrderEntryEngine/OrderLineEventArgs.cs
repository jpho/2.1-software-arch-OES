using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderEntryEngine
{
    public class OrderLineEventArgs
    {
        public OrderLineEventArgs(OrderLine orderLine)
        {
            this.OrderLine = orderLine;
        }

        public OrderLine OrderLine { get; private set; }
    }
}
