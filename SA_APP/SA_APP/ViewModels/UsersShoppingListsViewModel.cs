using Rg.Plugins.Popup.Services;
using SA_APP.Models;
using SA_APP.Models.FromApi;
//using SA_APP.Repository;
using SA_APP.Services;
using SA_APP.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace SA_APP.ViewModels
{
    public class UsersShoppingListsViewModel : BaseViewModel
    {
        private ShoppingListService _shoppingListService;
        private ShoppingUserAPP _shoppingUserAPP;
        private string _shoppingListCount;
        public ICommand OnDeleteItemCommand => new Command<int>(OnDeleteItem);
        public ICommand OnRenameCommand => new Command<int>(OnRename);

        public ICommand OnViewStoreCommand => new Command<int>(OnViewStore);
        public ICommand OnViewItemCommand => new Command<ShoppingItemAPP>(OnViewItem);


        private INavigation _navigation;
        //public UsersShoppingListsViewModel()
        //{
        //    _shoppingListService = new ShoppingListService();
        //    _listofShoppingLists = _shoppingListService.GetInMemoryShoppingLists();
        //}

        public UsersShoppingListsViewModel(ShoppingUserAPP shoppingUser, INavigation navigation)
        {
            _shoppingListService = new ShoppingListService();

            _navigation = navigation;
            //ListofShoppingLists listofShoppingLists = new ListofShoppingLists();
            //foreach (ShoppingListAPP list in shoppingUser.shoppingLists)
            //{
            //    listofShoppingLists._shoppingLists.Add(list);
            //}

            _shoppingUserAPP = shoppingUser;
            RefreshList();
            // listofShoppingLists._shoppingListsCount = shoppingUser.shoppingLists.Count();

            // _listofShoppingLists = listofShoppingLists;
        }



        public ObservableCollection<ShoppingListAPP> ShoppingLists
        {
            get
            {
                return _shoppingUserAPP.shoppingLists;
            }
            set
            {
                _shoppingUserAPP.shoppingLists = value;
            }
        }

        public string ShoppingListsCount
        {
            get
            {
                return _shoppingListCount;
            }
            set
            {
                _shoppingListCount = value;
                OnPropertyChanged();
            }
        }

        public void RefreshList(ShoppingListAPP list)
        {
            CalculateTotalQty(list);
            CalculateTotalPrice(list);
        }

        public void CalculateTotalPrice(ShoppingListAPP list)
        {
            list.totalCost = 0;
            foreach (ShoppingItemAPP item in list.items)
            {
                list.totalCost += Convert.ToDecimal(item.itemQuantity * item.price);
            }
        }

        public void CalculateTotalQty(ShoppingListAPP list)
        {
            if (list.items.Count == 0)
            {
                list.totalItems = 0;
            }
            else
            {
                list.totalItems = list.items.Select(i => i.itemQuantity).Sum();
            }
        }

        public void RefreshList()
        {
            ShoppingLists = _shoppingUserAPP.shoppingLists;
            CalculateShoppingListsCount();
            foreach(var list in ShoppingLists)
            {
                RefreshList(list);
            }
        }

        private void CalculateShoppingListsCount()
        {
            ShoppingListsCount = $"Total Shopping Lists: {_shoppingUserAPP.shoppingLists.Count}"; 
        }

        public bool OnDeleteList(int listId)
        {
            ShoppingListAPP list = _shoppingUserAPP.shoppingLists.FirstOrDefault(i => i.id == listId);

            bool listRemoved = _shoppingListService.DeleteShoppingList(listId, _shoppingUserAPP.token);

            if (listRemoved)
            {
                _shoppingUserAPP.shoppingLists.Remove(list);
                RefreshList();
                return true;
            }
            else
            {
                Console.WriteLine("List was not deleted");
                return false;
            }
        }

        private void OnDeleteItem(int linkId)
        {
            ShoppingListAPP shoppingList = null;
            ShoppingItemAPP shoppingItem = null;
            bool found = false;
            foreach (ShoppingListAPP list in _shoppingUserAPP.shoppingLists)
            {
                foreach (ShoppingItemAPP item in list.items)
                {
                    if (item.linkId == linkId)
                    {
                        shoppingList = list;
                        shoppingItem = item;
                        found = true;
                        break;
                    }
                }
                if (found) { break; }
            }

            if (!found)
            {
                return;
            }

            bool itemRemovedFromList = _shoppingListService.DeleteItemFromShoppingList(linkId, _shoppingUserAPP.token);

            if (itemRemovedFromList)
            {
                shoppingList.items.Remove(shoppingItem);
                CalculateTotalPrice(shoppingList);
                CalculateTotalQty(shoppingList);
            }
            else
            {
                Console.WriteLine("Item was not removed");
            }
            Console.WriteLine("List was not deleted");

        }

        private async void OnRename(int listId)
        {
            ShoppingListAPP list = _shoppingUserAPP.shoppingLists.FirstOrDefault(i => i.id == listId);

            await PopupNavigation.Instance.PushAsync(new EditShoppingListNamePopup(list, _shoppingUserAPP));
        }

        public async void OnCreateNew()
        {
            await PopupNavigation.Instance.PushAsync(new AddShoppingListPage(_shoppingUserAPP, this));
        }

        private async void OnViewStore(int listId)
        {
            ShoppingListAPP list = _shoppingUserAPP.shoppingLists.FirstOrDefault(i => i.id == listId);

            CheckedShoppingList checkedShoppingList = new CheckedShoppingList(list);

            await _navigation.PushModalAsync(new StoreMapPage(_shoppingUserAPP, _shoppingUserAPP.homeStore.storeMap, checkedShoppingList));
        }

        private void OnViewItem(ShoppingItemAPP item)
        {
            bool found = false;
            ShoppingListAPP foundList = null;
            foreach (var list in _shoppingUserAPP.shoppingLists)
            {
                foreach (var listItem in list.items)
                {
                    if (listItem.linkId == item.linkId)
                    {
                        found = true;
                        foundList = list;
                        break;
                    }
                }
                if (found)
                {
                    break;
                }
            }

            //public ViewShoppingItemPage(ShoppingUserAPP shoppingUser, int shoppingListId, ShoppingItemAPP item)
            if (found)
            {
                _navigation.PushModalAsync(new ViewShoppingItemPage(_shoppingUserAPP, foundList, item, this));
            }
        }
    }
}
