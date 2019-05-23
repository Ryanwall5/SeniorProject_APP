using Rg.Plugins.Popup.Contracts;
using Rg.Plugins.Popup.Pages;
using SA_APP.Models.FromApi;
using SA_APP.Models.ToApi;
using SA_APP.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace SA_APP.ViewModels
{
    public class AddItemToListViewModel : BaseViewModel
    {
        private ShoppingUserAPP _shoppingUserAPP;
        private Item _storeItem;
        private int _selectedIndex;
        public string _shoppingListName;
        public ShoppingListService _shoppingListService;
        public Action DisplayInvalidLoginPrompt;
        public Action DisplayValidLoginPrompt;
        public ICommand SubmitCommand { protected set; get; }
        public ICommand CancelCommand { protected set; get; }

        private int _selectedQtyIndex;
        public int startingItemQuantity;
        public int _itemQuantity;
        private List<int> _itemQuantityList;

        public bool _itemAddedSuccessfully;

        public AddItemToListViewModel(ShoppingUserAPP shoppingUserAPP, Item item)
        {
            _shoppingUserAPP = shoppingUserAPP;
            _storeItem = item;
            _shoppingListService = new ShoppingListService();
            SubmitCommand = new Command(OnSubmit);
            CancelCommand = new Command(OnCancel);
            FillShoppingListNames();
            FillListItemQuantity();
            _itemAddedSuccessfully = false;
        }

        private async void OnCancel(object obj)
        {
            await Rg.Plugins.Popup.Services.PopupNavigation.Instance.PopAsync();
        }

        private async void OnSubmit(object obj)
        {
            if (_shoppingUserAPP.shoppingLists.FirstOrDefault(list => list.name == _shoppingListName) == null)
            {
                _itemAddedSuccessfully = false;
                await Rg.Plugins.Popup.Services.PopupNavigation.Instance.PopAsync();
                return;
            }

            var shoppingList = _shoppingUserAPP.shoppingLists.FirstOrDefault(list => list.name == _shoppingListName);
            ItemListLink itemListLink = new ItemListLink
            {
                ItemId = _storeItem.Id,
                ListId = shoppingList.id,
                ItemQuantity = _itemQuantity
            };

            var itemAdded = _shoppingListService.AddItemToShoppingList(_shoppingUserAPP.token, itemListLink);

            if(itemAdded != null)
            {

                ShoppingItemAPP shoppingItemAPP = new ShoppingItemAPP
                {
                    linkId = itemAdded.linkId,
                    image = _storeItem.Image,
                    aisle = _storeItem.aisle,
                    aisleId = _storeItem.AisleId,
                    department = _storeItem.department,
                    departmentId = _storeItem.DepartmentId,
                    lowerDepartment = _storeItem.lowerDepartment,
                    lowerDepartmentId = _storeItem.LowerDepartmentId,
                    section = _storeItem.section,
                    sectionId = _storeItem.SectionId,
                    shelf = _storeItem.shelf,
                    shelfId = _storeItem.ShelfId,
                    slot = _storeItem.slot,
                    slotId = _storeItem.SlotId,
                    inStock = _storeItem.InStock,
                    stockAmount = _storeItem.StockAmount,
                    name = _storeItem.Name,
                    itemQuantity = _itemQuantity,
                    price = Convert.ToDouble(_storeItem.Price)
                };

                shoppingList.items.Add(shoppingItemAPP);
                _itemAddedSuccessfully = true;
                await Rg.Plugins.Popup.Services.PopupNavigation.Instance.PopAsync();

            }
            else
            {
                _itemAddedSuccessfully = false;
                await Rg.Plugins.Popup.Services.PopupNavigation.Instance.PopAsync();
            }
        }

        public string ItemName => $"Add item: {_storeItem.Name}";

        public Item StoreItem => _storeItem;

        public List<string> ShoppingListNames { get; private set; }

        public int ShoppingListSelectedIndex
        {
            get
            {
                return _selectedIndex;
            }
            set
            {

                _selectedIndex = value;
                OnPropertyChanged();
            }
        }

        public string ShoppingListSelectedItem
        {
            get
            {
                return _shoppingListName;
            }
            set
            {

                _shoppingListName = value;
                OnPropertyChanged();
            }
        }


        public List<int> ItemQuantity => _itemQuantityList;

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
                return _itemQuantity;
            }
            set
            {

                _itemQuantity = value;
                OnPropertyChanged();
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

        private void FillShoppingListNames()
        {
            ShoppingListNames = new List<string>();

            foreach (ShoppingListAPP list in _shoppingUserAPP.shoppingLists)
            {
                ShoppingListNames.Add(list.name);
            }
        }
    }
}
