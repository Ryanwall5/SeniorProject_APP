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
    public partial class StoreUserHomePage : ContentPage
    {
        public StoreUserHomePage(StoreUser user)
        {   
            InitializeComponent();
            storeUserWelcomeLbl.Text = $"Welcome to the store home page {user.fullName}";
        }
    }
}