using SA_APP.Models;
using SA_APP.Models.FromApi;
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
    public partial class UserHome : MasterDetailPage
    {

        private ShoppingUserAPP _shoppingUser;
        public UserHome(ShoppingUserAPP user)
        {
            _shoppingUser = user;
            InitializeComponent();

            masterPage = new MasterPage();
            Master = masterPage;
            Detail = new NavigationPage(new UserHomeDetailPage(user));
            MasterBehavior = MasterBehavior.Popover;
            masterPage.listView.ItemSelected += ListView_ItemSelected;
        }

        private async void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as MasterPageItem;

            if (item != null)
            {
                if (item.Title == "Logout")
                {
                    foreach (var page in Navigation.NavigationStack)
                    {
                        await Navigation.PopAsync();
                    }

                    foreach (var page in Navigation.ModalStack)
                    {
                        await Navigation.PopModalAsync();
                    }
                }
                else if(item.Title == "Shopping Lists")
                {
                    ShoppingListPage shoppingListPage = new ShoppingListPage(_shoppingUser);
                    Detail = new NavigationPage(shoppingListPage);
                    masterPage.listView.SelectedItem = null;
                    IsPresented = false;
                }
                else if (item.Title == "Store")
                {
                    StorePage storePage = new StorePage(_shoppingUser);
                    Detail = new NavigationPage(storePage);
                   // Detail = storePage;
                    masterPage.listView.SelectedItem = null;
                    IsPresented = false;
                }
                else if (item.Title == "Home")
                {
                    UserHomeDetailPage home = new UserHomeDetailPage(_shoppingUser);
                    Detail = new NavigationPage(home);
                    masterPage.listView.SelectedItem = null;
                    IsPresented = false;
                }
                else
                {
                    Detail = new NavigationPage((Page)Activator.CreateInstance(item.TargetType));
                    masterPage.listView.SelectedItem = null;
                    IsPresented = false;
                }
            }
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }

    }
}