using System;
using SA_APP.Models;
using SA_APP.Models.FromApi;
using SA_APP.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SA_APP.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AddShoppingListPage : Rg.Plugins.Popup.Pages.PopupPage
    {
        private CreateShoppingListViewModel _createShoppingList;
        private UsersShoppingListsViewModel _usersShoppingListsViewModel;
        public AddShoppingListPage (ShoppingUserAPP shoppingUser, UsersShoppingListsViewModel usersShoppingListsViewModel = null)
		{
            _usersShoppingListsViewModel = usersShoppingListsViewModel;
            _createShoppingList = new CreateShoppingListViewModel(shoppingUser, usersShoppingListsViewModel);
            this.BindingContext = _createShoppingList;
            _createShoppingList.DisplayInvalidShoppingListPrompt += () => DisplayAlert("Error", "Shopping List Not Created, try again", "OK");
            _createShoppingList.DisplayValidShoppingListPrompt += () => DisplayAlert("Success", "Shopping List Created, Successful", "OK");
            InitializeComponent();
		}

        public AddShoppingListPage()
        {
            InitializeComponent();
            _createShoppingList = new CreateShoppingListViewModel();
            this.BindingContext = _createShoppingList;
            _createShoppingList.DisplayInvalidShoppingListPrompt += () => DisplayAlert("Error", "Shopping List Not Created, try again", "OK");
            _createShoppingList.DisplayValidShoppingListPrompt += () => DisplayAlert("Success", "Shopping List Created, Successful", "OK");
        }

        private async void ShoppingListName_Completed(object sender, EventArgs e)
        {
            _createShoppingList.SubmitCommand.Execute(null);

            if(_createShoppingList._createdSuccessful)
            {        
                await Rg.Plugins.Popup.Services.PopupNavigation.Instance.PopAsync();

                if(_usersShoppingListsViewModel != null)
                {
                    _usersShoppingListsViewModel.RefreshList();
                }
            }
            else
            {
                _createShoppingList.DisplayInvalidShoppingListPrompt();
            }
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
             await Rg.Plugins.Popup.Services.PopupNavigation.Instance.PopAsync();
        }
    }
}