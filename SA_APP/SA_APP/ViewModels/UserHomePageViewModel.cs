using SA_APP.Models.FromApi;
using SA_APP.Services;
using SA_APP.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace SA_APP.ViewModels
{
    public class UserHomePageViewModel : BaseViewModel
    {
        private INavigation _navigation;
        private ShoppingUserAPP _shoppingUser;
        public Command FocusedSearchBarCommand => new Command(OnSearchBarFocused);
        private ItemService _itemService;

        private string _storeName;
        private string _storeAddress;

        public UserHomePageViewModel(ShoppingUserAPP shoppingUser, INavigation navigation)
        {
            _shoppingUser = shoppingUser;
            _navigation = navigation;
            _itemService = new ItemService();
            UpdateUserInformation();

        }

        public string UserName => _shoppingUser.fullName;
        public string StoreName
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

        public string StoreAddress
        {
            get
            {
                return _storeAddress;
            }
            set
            {
                _storeAddress = value;
                OnPropertyChanged();
            }
        }
        
        public ObservableCollection<Item> StoreItems { get; set; } = new ObservableCollection<Item>();

        private void GetStoreItems()
        {
            var items = _itemService.GetThreeStoreItemsForHomePage(_shoppingUser.homeStore.id);

            foreach(Item i in items)
            {
                StoreItems.Add(i);
            }
        }

        public void UpdateUserInformation()
        {
            StoreAddress = _shoppingUser.homeStore.address.street + ", " + _shoppingUser.homeStore.address.city + ", " + _shoppingUser.homeStore.address.state;
            StoreName = _shoppingUser.homeStore.name;
            StoreItems.Clear();
            GetStoreItems();
        }

        private async void OnSearchBarFocused(object obj)
        {
            await _navigation.PushModalAsync(new SearchItemsPage(_shoppingUser));
        }
    }
}
