using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Rg.Plugins.Popup.Services;
using SA_APP.CustomControls;
using SA_APP.Models;
using SA_APP.Models.FromApi;
using SA_APP.Services;
using SA_APP.Views;
using Xamarin.Forms;

namespace SA_APP.ViewModels
{
    public class AislesViewModel
    {
        private ShoppingUserAPP _shoppingUserAPP;
        public LowerDepartment _lowerDepartment;
        private INavigation _navigation;
        public ICommand AisleClickedCommand => new Command<int>(GoToSection);
        private StoreMapService _storeMapService;
        public CheckedShoppingList _shoppingList;
        public AislesViewModel(ShoppingUserAPP shoppingUserAPP, LowerDepartment lowerDepartment, INavigation navigation, CheckedShoppingList shoppingList)
        {
            this._shoppingUserAPP = shoppingUserAPP;
            this._lowerDepartment = lowerDepartment;
            this._navigation = navigation;
            _shoppingList = shoppingList;
            _storeMapService = new StoreMapService();
        }

        // TODO: Fix this function
        private async void GoToSection(int aisleId)
        {
            Aisle aisle = _lowerDepartment.Aisles.FirstOrDefault(ld => ld.Id == aisleId);

         
            await PopupNavigation.Instance.PushAsync(new LoadingPopup("Grabbing items"));

            aisle.Sections = await _storeMapService.GetSections(aisle.Id, _shoppingUserAPP.homeStore.id);

            await PopupNavigation.Instance.PopAsync();

            if(aisle.Sections == null || aisle.Sections.Count == 0)
            {
                DependencyService.Get<IAndroidToast>().MakeLongToast("No items added for this aisle", "error");
                return;
            }

            await _navigation.PushModalAsync(new SectionPage(_shoppingUserAPP, aisle, _shoppingList));
        }
    }
}
