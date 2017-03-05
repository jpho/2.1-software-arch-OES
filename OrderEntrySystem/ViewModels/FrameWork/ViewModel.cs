using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderEntrySystem
{
    public class ViewModel
    {

        public string DisplayName { get; private set; }

        public ViewModel(string displayName)
        {
            this.DisplayName = displayName;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual  void OnPropertyChanged(string propertyName)
        {
            var handler = this.PropertyChanged;

            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
            
        }
    }
}
