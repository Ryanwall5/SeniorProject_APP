using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace SA_APP.Models.FromApi
{
    public class ListofShoppingLists
    {

        public ListofShoppingLists()
        {
            _shoppingLists = new ObservableCollection<ShoppingListAPP>();
        }

        public int _shoppingListsCount
        {
            get
            {
                return _shoppingListsCount;
            }
            set
            {
                _shoppingListsCount = value;
            }
        }

        public ObservableCollection<ShoppingListAPP> _shoppingLists { get; set; }
    }
}
