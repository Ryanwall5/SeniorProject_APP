using SA_APP.Models;
using SA_APP.Models.FromApi;
using SA_APP.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace SA_APP.ViewModels
{
    public class ShoppingItemViewModel : BaseViewModel
    {
        public ShoppingItemAPP _shoppingItem;
        public ShoppingUserAPP _shoppingUserApp;
        public ShoppingListService _shoppingListService;
        public Action DisplayItemRemovedPrompt;
        private ItemService _itemService;
        private StoreService _storeService;
        private int _selectedQtyIndex;
        public int startingItemQuantity;
        public int _itemQuantity;
        private List<int> _itemQuantityList;
        public ICommand RemoveItemCommand => new Command<int>(RemoveItem);

        private ShoppingListAPP _shoppingList;
        private INavigation _navigation;
        public bool _itemRemoved;


        public ShoppingItemViewModel(ShoppingUserAPP shoppingUser, ShoppingListAPP shoppingList, ShoppingItemAPP shoppingItem, INavigation navigation)
        {
            _shoppingListService = new ShoppingListService();
            _storeService = new StoreService();
            startingItemQuantity = shoppingItem.itemQuantity;
            _shoppingList = shoppingList;
            _shoppingUserApp = shoppingUser;
            _shoppingItem = shoppingItem;
            ItemQuantity = _shoppingItem.itemQuantity;
            ItemQuantitySelectedIndex = _shoppingItem.itemQuantity;
            _navigation = navigation;
            FillListItemQuantity();
            _itemRemoved = false;
        }

        public ShoppingItemAPP ShoppingItem => _shoppingItem;

        public List<int> ItemQuantityList => _itemQuantityList;

        public int ItemQuantity
        {
            get
            {
                return _itemQuantity;
            }
            set
            {
                _itemQuantity = value;
                OnPropertyChanged();
            }
        }

        public int ItemQuantitySelectedIndex
        {
            get
            {
                return _selectedQtyIndex;
            }
            set
            {

                _selectedQtyIndex = value;
                OnPropertyChanged();
            }
        }

        public int ItemQuantitySelectedItem
        {
            get
            {
                return _shoppingItem.itemQuantity;
            }
            set
            {

                _shoppingItem.itemQuantity = value;
                OnPropertyChanged();
            }
        }


        public string Department => _shoppingItem.department;
        public string Aisle => _shoppingItem.aisle;
        public string Section => _shoppingItem.section;
        public string Shelf => _shoppingItem.shelf;
        public string LowerDepartment => _shoppingItem.shelf;

        private async void RemoveItem(int linkId)
        {
            var itemRemoved = _shoppingListService.DeleteItemFromShoppingList(linkId, _shoppingUserApp.token);
            if (itemRemoved)
            {
                var item = _shoppingList.items.FirstOrDefault(i => i.linkId == linkId);
                _shoppingList.items.Remove(item);
                _itemRemoved = true;
               await _navigation.PopModalAsync();
            }
        }

        private void FillListItemQuantity()
        {
            _itemQuantityList = new List<int>();

            for (int i = 1; i < 100; i++)
            {
                _itemQuantityList.Add(i);
            }
        }
    }
}
