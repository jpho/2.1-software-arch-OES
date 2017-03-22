using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderEntryEngine
{
    public class CategoryEventArgs
    {
        public CategoryEventArgs(Category category)
        {
            this.Category = category;
        }

        public Category Category { get; private set; }
    }
}
