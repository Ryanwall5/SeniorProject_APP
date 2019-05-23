using SA_APP.Models.FromApi;
using SA_APP.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace SA_APP.ViewModels
{
    class EditShoppingListNameViewModel : BaseViewModel
    {
        public ICommand SubmitCommand { protected set; get; }
        public ICommand CancelCommand { protected set; get; }
        private ShoppingListAPP _shoppingList;
        private string _newShoppingListName;
        public Action DisplayInValidNameChangePrompt;
        public Action DisplayValidNameChangePrompt;

        private ShoppingListService _shoppingListService;
        private ShoppingUserAPP _shoppingUser;

        public EditShoppingListNameViewModel(ShoppingListAPP shoppingList, ShoppingUserAPP user)
        {
            _shoppingList = shoppingList;
            _shoppingUser = user;
            _shoppingListService = new ShoppingListService();
            SubmitCommand = new Command(OnSubmit);
            CancelCommand = new Command(OnCancel);
        }

        public string ShoppingListName
        {
            get
            {
                return _newShoppingListName;
            }
            set
            {
                _newShoppingListName = value;
                OnPropertyChanged();
            }
        }

        public string OldShoppingListName => _shoppingList.name;

        private void OnCancel(object obj)
        {
            Rg.Plugins.Popup.Services.PopupNavigation.Instance.PopAsync();
        }

        private void OnSubmit(object obj)
        {
            var newName = _newShoppingListName;
            var listNameChanged = _shoppingListService.ChangeShoppingListName(newName, _shoppingList.id, _shoppingUser.token);

            if(listNameChanged)
            {
                // Change the name in API
                _shoppingList.name = _newShoppingListName;
                DisplayValidNameChangePrompt();
            }
            else
            {
                DisplayInValidNameChangePrompt();
            }

            Rg.Plugins.Popup.Services.PopupNavigation.Instance.PopAsync();
        }
    }
}
