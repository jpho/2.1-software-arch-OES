using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderEntryEngine
{
    public class Product
    {
        public int Id { get; set; }

        public Condition Condition { get; set; }

        public virtual Category Category { get; set; }

        public int CategoryId { get; set; }
        
        public virtual Location Location { get; set; }

        public int LocationId { get; set; }

        public virtual ICollection<OrderLine> Orders { get; set; }

       
       // public string Location { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        [Required]
        public decimal Price { get; set; }
    }
}
