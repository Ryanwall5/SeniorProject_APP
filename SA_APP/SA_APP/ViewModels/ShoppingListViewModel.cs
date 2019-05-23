using SA_APP.Models.FromApi;
using SA_APP.Services;
using SA_APP.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace SA_APP.ViewModels
{
    public class ShoppingListViewModel : BaseViewModel
    {
        public Action DisplayInvalidLoginPrompt;
        public Action DisplayValidLoginPrompt;

        private ShoppingListAPP _shoppingList;
        private ShoppingListService _shoppingListService;
        public ShoppingUserAPP _shoppingUser;

        private string _usersToken;
        public ICommand OnDeleteCommand => new Command<int>(OnDelete);
        public ICommand OnViewCommand => new Command<int>(OnView);
        public ICommand EditNameCommand { protected set; get; }

        public ShoppingListViewModel()
        {
            _shoppingListService = new ShoppingListService();
            //_shoppingList = _shoppingListService.GetInMemoryShoppingList();
        }

        public ShoppingListViewModel(ShoppingUserAPP shoppingUser, int listId)
        {
            EditNameCommand = new Command(OnEditName);
            _shoppingListService = new ShoppingListService();
            _shoppingUser = shoppingUser;
            _usersToken = shoppingUser.token;
            _shoppingList = shoppingUser.shoppingLists.FirstOrDefault(sl => sl.id == listId);
        }


        private void OnEditName(object obj)
        {
            Rg.Plugins.Popup.Services.PopupNavigation.Instance.PushAsync(new EditShoppingListNamePopup(_shoppingList, _shoppingUser));
        }

        //public ShoppingList ShoppingList
        //{
        //    get
        //    {
        //        return _shoppingList;
        //    }
        //    set
        //    {
        //        _shoppingList = value;
        //        CalculateTotalPrice();
        //        CalculateTotalQty();
        //        OnPropertyChanged();
        //    }
        //}
        public int ShoppingListId => _shoppingList.id;

        public ShoppingListAPP ShoppingList => _shoppingList;


        public ObservableCollection<ShoppingItemAPP> Items
        {
            get
            {
                return _shoppingList.items;
            }
            set
            {
                _shoppingList.items = value;
                OnPropertyChanged();
            }
        }

        public void RefreshList()
        {
            Items = _shoppingList.items;
            CalculateTotalQty();
            CalculateTotalPrice();
        }

        public void CalculateTotalPrice()
        {
            _shoppingList.totalCost = 0;
            foreach (ShoppingItemAPP item in _shoppingList.items)
            {
                _shoppingList.totalCost += Convert.ToDecimal(item.itemQuantity * item.price);
            }
        }

        public void CalculateTotalQty()
        {
            if(_shoppingList.items.Count == 0)
            {
                _shoppingList.totalItems = 0;
            }
            else
            {
                _shoppingList.totalItems = _shoppingList.items.Select(i => i.itemQuantity).Sum();
            }
        }
        private void OnDelete(int linkId)
        {
            ShoppingItemAPP item = Items.FirstOrDefault(i => i.linkId == linkId);

            bool itemRemovedFromList = _shoppingListService.DeleteItemFromShoppingList(linkId, _shoppingUser.token);

            if(itemRemovedFromList)
            {
                Items.Remove(item);
                CalculateTotalPrice();
                CalculateTotalQty();
            }
            else
            {
                Console.WriteLine("Item was not removed");
            }
        }

        private void OnView(int itemId)
        {
            Console.WriteLine($"Navigating to view item {itemId}");
        }
    }
}
