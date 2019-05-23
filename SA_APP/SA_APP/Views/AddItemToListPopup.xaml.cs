using Rg.Plugins.Popup.Contracts;
using SA_APP.CustomControls;
using SA_APP.Models.FromApi;
using SA_APP.Models.ToApi;
using SA_APP.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SA_APP.Views
{

    public partial class AddItemToListPopup : Rg.Plugins.Popup.Pages.PopupPage
    {
        private AddItemToListViewModel _addItemToListViewModel;

        public AddItemToListPopup(Item item, ShoppingUserAPP user)
        {
            _addItemToListViewModel = new AddItemToListViewModel(user, item);
            _addItemToListViewModel.DisplayInvalidLoginPrompt += () => DisplayAlert("Error", "Item was not added to list", "OK");
            _addItemToListViewModel.DisplayValidLoginPrompt += () => DisplayAlert("Success", "Item added", "OK");
            this.BindingContext = _addItemToListViewModel;
            InitializeComponent();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            if(_addItemToListViewModel._itemAddedSuccessfully)
            {
                //_addItemToListViewModel.DisplayValidLoginPrompt();
                DependencyService.Get<IAndroidToast>().MakeShortToast("Item added Successfully!", "submit");
            }
            else
            {
                DependencyService.Get<IAndroidToast>().MakeLongToast("Item was not added Successfully!", "error");
            }
        }

    }
}