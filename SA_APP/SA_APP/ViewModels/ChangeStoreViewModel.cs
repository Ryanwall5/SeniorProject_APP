using SA_APP.Models.FromApi;
using SA_APP.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace SA_APP.ViewModels
{
    public class ChangeStoreViewModel : BaseViewModel
    {
        
        public ShoppingUserAPP _shoppingUser;
        private StoreService _storeService;
        private ShoppingListService _shoppingListService;
        private UserHomePageViewModel _userHomePage;

        public ChangeStoreViewModel(ShoppingUserAPP shoppingUser, UserHomePageViewModel userHomePage)
        {
            _shoppingUser = shoppingUser;
            _userHomePage = userHomePage;
            _storeService = new StoreService();
            _shoppingListService = new ShoppingListService();
            init();
        }

        public async void init()
        {
            var stores = _storeService.GetStores();
            foreach (Store s in stores)
            {
                StoreNames.Add(s.name);
                _stores.Add(s);
            }
        }

        private List<Store> _stores { get; set; } = new List<Store>();
        public Command OnCancelCommand => new Command(OnCancel);

        public Command ChangeStoreCommand => new Command(ChangeStore);

        public List<string> StoreNames { get; private set; } = new List<string>();
        private int _selectedIndex;
        public string _storeName;

        public int StoreSelectedIndex
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

        public string StoreSelectedItem
        {
            get
            {
                return _storeName;
            }
            set
            {

                _storeName = value;
                OnPropertyChanged();
            }
        }

        public string HomeStore => _shoppingUser.homeStore.name;
        public string StoreName => _shoppingUser.homeStore.name;

        private async void ChangeStore(object obj)
        {
            if(_storeName != _shoppingUser.homeStore.name)
            {
                var id = _stores[StoreSelectedIndex].id;
                var shoppingUser = _storeService.ChangeStore(id, _shoppingUser.token);

                _shoppingUser.homeStore = shoppingUser.homeStore;
                
                _shoppingUser.shoppingLists.Clear();
                foreach (ShoppingList list in shoppingUser.shoppingLists)
                {
                    ShoppingListAPP listAPP = new ShoppingListAPP
                    {
                        id = list.id,
                        name = list.name,
                        timeOfCreation = list.timeOfCreation,
                        totalCost = list.totalCost,
                        totalItems = list.totalItems,
                        items = new ObservableCollection<ShoppingItemAPP>()
                    };

                    foreach (ShoppingItem item in list.items)
                    {
                        ShoppingItemAPP shoppingItemAPP = new ShoppingItemAPP
                        {
                            linkId = item.linkId,
                            image = item.image,
                            inStock = item.inStock,
                            price = item.price,
                            stockAmount = item.stockAmount,
                            itemQuantity = item.itemQuantity,
                            name = item.name,
                            departmentId = item.departmentId,
                            lowerDepartmentId = item.lowerDepartmentId,
                            aisleId = item.aisleId
                        };

                        listAPP.items.Add(shoppingItemAPP);
                    }

                    _shoppingUser.shoppingLists.Add(listAPP);
                }

                _userHomePage.UpdateUserInformation();

                await Rg.Plugins.Popup.Services.PopupNavigation.Instance.PopAsync();
            }       
        }
        private async void OnCancel(object obj)
        {
            await Rg.Plugins.Popup.Services.PopupNavigation.Instance.PopAsync();
        }
    }
}
