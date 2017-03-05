using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderEntryEngine
{
   public class LocationEventArgs
    {
        public LocationEventArgs(Location location)
        {
            this.Location = location;
        }

        public Location Location { get; private set; }
    }
}
