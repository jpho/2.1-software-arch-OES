using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderEntryEngine
{
    public class Location
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public virtual ICollection<Product> Products { get; set; }

        public override string ToString()
        {
            return this.Name;
        }

        protected void CreateCommands()
        {

        }

        private void Save()
        {

        }
    }
}
