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
	public partial class EditShoppingListNamePopup : Rg.Plugins.Popup.Pages.PopupPage
    {
        private EditShoppingListNameViewModel editShoppingListNameView;
        public EditShoppingListNamePopup(ShoppingListAPP shoppingList, ShoppingUserAPP user)
		{
			InitializeComponent ();
            editShoppingListNameView = new EditShoppingListNameViewModel(shoppingList, user);
            editShoppingListNameView.DisplayInValidNameChangePrompt += () => DisplayAlert("Error", "Invalid Name Change, try again", "OK");
            editShoppingListNameView.DisplayValidNameChangePrompt += () => DisplayAlert("Success", "Name Change, Successful", "OK");
            this.BindingContext = editShoppingListNameView;
        }
	}
}