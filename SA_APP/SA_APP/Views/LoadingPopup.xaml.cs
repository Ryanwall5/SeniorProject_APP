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
    public partial class LoadingPopup : Rg.Plugins.Popup.Pages.PopupPage
    {
        public LoadingPopup(string message)
        {
            InitializeComponent();
            if(message != null)
            {
                loadingMessage.Text = message;
            }
            CloseWhenBackgroundIsClicked = false;
        }
    }
}