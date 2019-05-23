using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace SA_APP.Models.FromApi
{
    public class BaseUser : BaseModel
    {
        private Store _store;

        public string email { get; set; }

        public string fullName { get; set; }

        public string token { get; set; }

        public string role { get; set; }

        public Store homeStore
        {
            get
            {
                return _store;
            }
            set
            {
                _store = value;
                OnPropertyChanged();
            }
        }

    }
}
