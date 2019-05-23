using SA_APP.Models.FromApi;
using SA_APP.ViewModels;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SA_APP.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ViewShoppingListPage : ContentPage
    {
        ShoppingListViewModel _shoppingListViewModel;

        public ViewShoppingListPage()
        {
            InitializeComponent();
            _shoppingListViewModel = new ShoppingListViewModel();
            this.BindingContext = _shoppingListViewModel;
        }

        public ViewShoppingListPage(ShoppingUserAPP shoppingUser, int listId)
        {
            try
            {
                InitializeComponent();
                _shoppingListViewModel = new ShoppingListViewModel(shoppingUser, listId);
                editIcon.GestureRecognizers.Add(new TapGestureRecognizer
                {
                    Command = _shoppingListViewModel.EditNameCommand,
                    NumberOfTapsRequired = 1
                });
                this.BindingContext = _shoppingListViewModel;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private void OnDelete_Clicked(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var itemId = int.Parse(button.CommandParameter.ToString());
            _shoppingListViewModel.OnDeleteCommand.Execute(itemId);
        }

        //private void OnView_Clicked(object sender, EventArgs e)
        //{
        //    var button = (Button)sender;
        //    var itemId = int.Parse(button.CommandParameter.ToString());
        //    _shoppingListViewModel.OnViewCommand.Execute(itemId);
        //    Navigation.PushAsync(new ViewShoppingItemPage());
        //}

        async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
                return;

            //Deselect Item
            ((ListView)sender).SelectedItem = null;
            await Navigation.PushModalAsync(new ViewShoppingItemPage(_shoppingListViewModel._shoppingUser, _shoppingListViewModel.ShoppingList, (ShoppingItemAPP)e.Item));
        }

        protected override bool OnBackButtonPressed()
        {
            Navigation.PopModalAsync();
            return base.OnBackButtonPressed();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }


        protected override void OnAppearing()
        {
            try
            {
                base.OnAppearing();
                _shoppingListViewModel.RefreshList();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }

        }

        //private void ChangeName_Clicked(object sender, EventArgs e)
        //{
        //    _changeNameEntry.IsEnabled = true;
        //    _changeNameEntry.Focus();
        //}
    }
}