using Rg.Plugins.Popup.Services;
using SA_APP.Models.FromApi;
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
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ViewStoreItemPage : ContentPage
	{

        private ShoppingUserAPP _user;
        private Item _item;
        private StoreItemViewModel _storeItemViewModel; 

		public ViewStoreItemPage (Item item, ShoppingUserAPP user)
		{
            InitializeComponent();
            _storeItemViewModel = new StoreItemViewModel(item, user);

            //addItemIcon.GestureRecognizers.Add(new TapGestureRecognizer
            //{
            //    Command = _storeItemViewModel.AddToShoppingListCommand,
            //    NumberOfTapsRequired = 1
            //});

            this.BindingContext = _storeItemViewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
           
        }
        //private void Button_Clicked(object sender, EventArgs e)
        //{
        //    PopupNavigation.Instance.PushAsync(new AddItemToListPopup(_item, _user));
        //}
    }
}