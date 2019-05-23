//using SA_APP.Repository;
using SA_APP.Models.FromApi;
using SA_APP.Models.ToApi;
using SA_APP.ViewModels;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SA_APP.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ViewShoppingItemPage : ContentPage
    {
        private int _listId;
        private ShoppingItemViewModel _shoppingItemViewModel;
        private UsersShoppingListsViewModel _usersShoppingListsViewModel;
        private ShoppingListAPP _shoppingList;
        public ViewShoppingItemPage(ShoppingUserAPP shoppingUser, ShoppingListAPP shoppingList, ShoppingItemAPP item, UsersShoppingListsViewModel usersShoppingListsViewModel = null)
        {
            InitializeComponent();
            _usersShoppingListsViewModel = usersShoppingListsViewModel;
            _shoppingItemViewModel = new ShoppingItemViewModel(shoppingUser, shoppingList, item, Navigation);
            _shoppingItemViewModel.DisplayItemRemovedPrompt += () => DisplayAlert("Success", "Item Removed From List", "OK");
            _listId = shoppingList.id;
            _shoppingList = shoppingList;
            this.BindingContext = _shoppingItemViewModel;
            qtyPicker.SelectedIndex = _shoppingItemViewModel.startingItemQuantity - 1;
            //_listId = listId;
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            if(_shoppingItemViewModel._itemRemoved)
            {
                _shoppingItemViewModel.DisplayItemRemovedPrompt();
            }
            else if (_shoppingItemViewModel.startingItemQuantity != _shoppingItemViewModel._shoppingItem.itemQuantity)
            {
                ItemListLink updatedItem = _shoppingItemViewModel._shoppingListService.UpdateLink(_shoppingItemViewModel._shoppingItem, _shoppingItemViewModel._shoppingUserApp.token);
                
                if(updatedItem == null)
                {
                    _shoppingItemViewModel.ItemQuantitySelectedItem = _shoppingItemViewModel.startingItemQuantity;
                }
                else if(updatedItem.ItemQuantity == _shoppingItemViewModel.ItemQuantitySelectedItem)
                {
                    if(_usersShoppingListsViewModel != null)
                    {
                        _usersShoppingListsViewModel.CalculateTotalPrice(_shoppingList);
                        _usersShoppingListsViewModel.CalculateTotalQty(_shoppingList);

                        DisplayAlert("Success", $"{_shoppingItemViewModel._shoppingItem.name} Quantity changed from {_shoppingItemViewModel.startingItemQuantity} -> {_shoppingItemViewModel._shoppingItem.itemQuantity} ", "Ok");
                    }
                }          
            }
        }

        protected override bool OnBackButtonPressed()
        {
            Navigation.PopModalAsync();
            return base.OnBackButtonPressed();
        }

        private void Button_Clicked(object sender, System.EventArgs e)
        {
            Navigation.PopModalAsync();
        }

    }
}