using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SimplexMethod
{
    public class DataModel : INotifyPropertyChanged
    {
        private Constraints constraints;

        public Constraints Constraints
        {
            get
            {
                return constraints;
            }
            set
            {
                constraints = value;
                NotifyPropertyChanged();
            }
        }

        public DataModel(Constraints c)
        {
            Constraints = c;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
