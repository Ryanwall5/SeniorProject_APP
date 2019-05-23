using Rg.Plugins.Popup.Services;
using SA_APP.CustomControls;
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
    public class LowerDepartmentsViewModel
    {
        public Action DisplayInvalidLoginPrompt;
        public Action DisplayValidLoginPrompt;
        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        public ICommand LowerDepartmentClickedCommand => new Command<int>(GoToAisles);

        public ShoppingUserAPP _shoppingUserAPP;
        public Department _department;
        private INavigation _navigation;
        public CheckedShoppingList _shoppingList;
        private StoreMapService _storeMapService;
        //private StoreMapService

        public LowerDepartmentsViewModel(ShoppingUserAPP shoppingUser, Department department, INavigation navigation, CheckedShoppingList shoppingList = null)
        {
            _shoppingUserAPP = shoppingUser;
            _department = department;
            _navigation = navigation;

            _storeMapService = new StoreMapService();
            var lowerDepartments = _storeMapService.GetLowerDepartments(_department.Id);
            _department.LowerDepartments = lowerDepartments;
            _shoppingList = shoppingList;
            //DisplayInvalidLoginPrompt += () => DisplayAlert("Error", "Invalid Login, try again", "OK");
            //DisplayValidLoginPrompt += () => DisplayAlert("Success", "Login, Successful", "OK");
        }

        private void GetLowerDepartments()
        {
            
        }

        private async void GoToAisles(int lowerDepartmentId)
        {
            LowerDepartment lowerDepartment = _department.LowerDepartments.FirstOrDefault(ld => ld.Id == lowerDepartmentId);
            if(lowerDepartment.AisleCount == 1)
            {
                var aisles = await _storeMapService.GetAisles(lowerDepartment.Id);
                if(aisles.Count == 0 || aisles == null)
                {
                    DependencyService.Get<IAndroidToast>().MakeLongToast("No aisles added for this department", "error");
                    return;
                }
                else
                {
                    await PopupNavigation.Instance.PushAsync(new LoadingPopup("Grabbing items"));

                    Aisle aisle = aisles.First();
                    aisle.Sections = await _storeMapService.GetSections(aisle.Id, _shoppingUserAPP.homeStore.id);

                    await PopupNavigation.Instance.PopAsync();
                    //Aisle aisle = lowerDepartment.Aisles.First();
                    //var aisles = _storeMapService.GetAisles(lowerDepartment.Id);
                    await _navigation.PushModalAsync(new SectionPage(_shoppingUserAPP, aisle, _shoppingList));
                }
            }
            else
            {
                var aisles = await _storeMapService.GetAisles(lowerDepartment.Id);
                lowerDepartment.Aisles = aisles;
                await _navigation.PushModalAsync(new AislesPage(_shoppingUserAPP, lowerDepartment, _shoppingList));
            }
        }
    }
}
