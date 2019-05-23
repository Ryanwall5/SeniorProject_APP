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
	public partial class ChangeStorePage : Rg.Plugins.Popup.Pages.PopupPage
    {
        private ChangeStoreViewModel _changeStoreViewModel;
		public ChangeStorePage (ShoppingUserAPP shoppingUser, UserHomePageViewModel userHomePageViewModel)
		{
			InitializeComponent ();
            _changeStoreViewModel = new ChangeStoreViewModel(shoppingUser, userHomePageViewModel);
            this.BindingContext = _changeStoreViewModel;
        }

    }
}