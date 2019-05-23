using System.ComponentModel;

using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.CompilerServices;

namespace SA_APP.Models
{
    public class BaseModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
