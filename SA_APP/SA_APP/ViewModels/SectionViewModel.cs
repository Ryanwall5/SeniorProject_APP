using Rg.Plugins.Popup.Services;
using SA_APP.Models;
using SA_APP.Models.FromApi;
using SA_APP.Services;
using SA_APP.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace SA_APP.ViewModels
{
    public class SectionViewModel
    {
        public Action DisplayInvalidLoginPrompt;
        public Action DisplayValidLoginPrompt;
        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        public ICommand AddToListCommand => new Command<string>(AddToList);
        public ICommand ViewItemCommand => new Command<string>(ViewItem);

        private INavigation _navigation;
        public ShoppingUserAPP _shoppingUserApp;
        public Aisle _aisle;
        public CheckedShoppingList _shoppingList;
        private StoreMapService _storeMapService;


        public SectionViewModel(Aisle aisle, ShoppingUserAPP user, INavigation navigation, CheckedShoppingList shoppingList)
        {
            _navigation = navigation;
            _shoppingUserApp = user;
            _aisle = aisle;
            _storeMapService = new StoreMapService();
            _shoppingList = shoppingList;
            //GetSections();
        }

        private async void GetSections()
        {
            //await PopupNavigation.Instance.PushAsync(new LoadingPopup("Grabbing items"));

            //_aisle.Sections = await _storeMapService.GetSections(_aisle.Id, _shoppingUserApp.homeStore.id);

            //await PopupNavigation.Instance.PopAsync();
        }


        private async void AddToList(string section_shelf_slot)
        {
            string[] split = section_shelf_slot.Split(',');

            var sectionId = Convert.ToInt32(split[0]);
            var shelfId = Convert.ToInt32(split[1]);
            var slotId = Convert.ToInt32(split[2]);

            Section section = _aisle.Sections.FirstOrDefault(sec => sec.Id == sectionId);

            Shelf shelf = section.Shelves.FirstOrDefault(sh => sh.Id == shelfId);

            ShelfSlot slot = shelf.Slots.FirstOrDefault(sh => sh.Id == slotId);

            await PopupNavigation.Instance.PushAsync(new AddItemToListPopup(slot.Item, _shoppingUserApp));
        }

        private async void ViewItem(string section_shelf_slot)
        {
            string[] split = section_shelf_slot.Split(',');

            var sectionId = Convert.ToInt32(split[0]);
            var shelfId = Convert.ToInt32(split[1]);
            var slotId = Convert.ToInt32(split[2]);

            Section section = _aisle.Sections.FirstOrDefault(sec => sec.Id == sectionId);

            Shelf shelf = section.Shelves.FirstOrDefault(sh => sh.Id == shelfId);

            ShelfSlot slot = shelf.Slots.FirstOrDefault(sh => sh.Id == slotId);

            await _navigation.PushModalAsync(new ViewStoreItemPage(slot.Item, _shoppingUserApp));
        }

        internal void RemoveItem(Item item)
        {
            CheckedItem checkedItem = _shoppingList.CheckedShoppingItems.FirstOrDefault(i => i.slotId == item.SlotId);
            _shoppingList.CheckedShoppingItems.Remove(checkedItem);
        }
    }
}
