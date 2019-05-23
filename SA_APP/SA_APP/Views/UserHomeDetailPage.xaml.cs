using Rg.Plugins.Popup.Services;
using SA_APP.Models.FromApi;
using SA_APP.Services;
using SA_APP.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SA_APP.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class UserHomeDetailPage : ContentPage
	{
        private SearchBar _searchBar;
        private UserHomePageViewModel _userHomePageViewModel;
        public UserHomeDetailPage()
        {

        }
		public UserHomeDetailPage (ShoppingUserAPP shoppingUser)
		{
            try
            {
                InitializeComponent();
                _userHomePageViewModel = new UserHomePageViewModel(shoppingUser, Navigation);

                _searchBar = this.FindByName<SearchBar>("itemSearchBar");
                _searchBar.Focused += _searchBar_Focused;

                storeLogo.GestureRecognizers.Add(new TapGestureRecognizer
                {
                    Command = new Command(() => { PopupNavigation.Instance.PushAsync(new ChangeStorePage(shoppingUser, _userHomePageViewModel)); }),
                    NumberOfTapsRequired = 1
                });

                shoppingListLogo.GestureRecognizers.Add(new TapGestureRecognizer
                {
                    Command = new Command(() => { PopupNavigation.Instance.PushAsync(new AddShoppingListPage(shoppingUser)); }),
                    NumberOfTapsRequired = 1
                });

                this.Title = "Welcome, " + shoppingUser.fullName;
                this.BindingContext = _userHomePageViewModel;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
           
		}

        private void _searchBar_Focused(object sender, FocusEventArgs e)
        {
            _userHomePageViewModel.FocusedSearchBarCommand.Execute(null);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }
    }
}