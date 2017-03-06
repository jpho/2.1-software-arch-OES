using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderEntryEngine
{
    /// <summary>
    /// The class used to represent a category.
    /// </summary>
   public class Category
    {
        /// <summary>
        /// The id for the category.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The name for the category.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The list of products for the category.
        /// </summary>
        public virtual ICollection<Product> Products { get; set; }

        /// <summary>
        /// Overrides the to string method in category.
        /// </summary>
        /// <returns>The string to return.</returns>
        public override string ToString()
        {
            return this.Name;
        }

    }
}
