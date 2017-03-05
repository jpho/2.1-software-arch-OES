using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderEntryEngine
{
    public class CustomerEventArgs
    {
        public CustomerEventArgs(Customer customer)
        {
            this.Customer = customer;
        }

        public Customer Customer { get; private set; }
    }
}
