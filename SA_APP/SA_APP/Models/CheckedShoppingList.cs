using SA_APP.Models.FromApi;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace SA_APP.Models
{
    public class CheckedShoppingList
    {
        public CheckedShoppingList(ShoppingListAPP shoppingListAPP)
        {
            CheckedShoppingItems = new ObservableCollection<CheckedItem>();
            foreach (ShoppingItemAPP item in shoppingListAPP.items)
            {
                CheckedItem checkedItem = new CheckedItem(item);
                CheckedShoppingItems.Add(checkedItem);
            }
            Name = shoppingListAPP.name;
        }

        public string Name { get; set; }
        public ObservableCollection<CheckedItem> CheckedShoppingItems { get; set; } 
    }
}
