using SA_APP.Models.FromApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace SA_APP.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StorePage : ContentPage
    {
        private ShoppingUserAPP _shoppingUser;

        public StorePage()
        {

        }
        public StorePage(ShoppingUserAPP shoppingUser)
        {
            InitializeComponent();
            _shoppingUser = shoppingUser;

            if(_shoppingUser.homeStore.name == "Fred Meyer")
            {
                storeLogoImage.Source = "fred_meyer_logo.png";
            }
            else if(_shoppingUser.homeStore.name == "Safeway")
            {
                storeLogoImage.Source = "Safeway.jpg";
            }

            websiteLbl.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(() => ViewStoreWebsite_Clicked()),
                NumberOfTapsRequired = 1
            });

            phoneNumberLbl.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(() => CallStore_Clicked()),
                NumberOfTapsRequired = 1
            });
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            //storeLogoImage.Source = ImageSource.FromFile("fred_meyer_logo.png");
            //homeStoreLbl.Text = $"{_shoppingUser.homeStore.name}";
            addressLbl.Text = $"{_shoppingUser.homeStore.address.street} {_shoppingUser.homeStore.address.city}, {_shoppingUser.homeStore.address.state} {_shoppingUser.homeStore.address.zip}";
            phoneNumberLbl.Text = $"{_shoppingUser.homeStore.phoneNumber}";
            websiteLbl.Text = $"{_shoppingUser.homeStore.website}";



            double latitude = Convert.ToDouble(_shoppingUser.homeStore.address.latitude);
            double longitude = Convert.ToDouble(_shoppingUser.homeStore.address.longitude);

            //storeGoogleMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(latitude, longitude), Distance.FromKilometers(5)));
            storeGoogleMap.MoveToRegion(new MapSpan(new Position(latitude, longitude), 0.01, 0.01));
 
            string street = _shoppingUser.homeStore.address.street;
            string state = _shoppingUser.homeStore.address.state;
            string zip = _shoppingUser.homeStore.address.zip.ToString();
            string city = _shoppingUser.homeStore.address.city;


            var position = new Position(latitude, longitude); // Latitude, Longitude
            var pin = new Pin
            {
                Type = PinType.Place,
                Position = position,
                Label = _shoppingUser.homeStore.name,
                Address = $"{street} {city}, {state} {zip}"
            };

            storeGoogleMap.Pins.Add(pin);

        }
        // <Button Clicked = "ViewMap_Clicked" Text="View Store Map"/>
        private async void ViewMap_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new StoreMapPage(_shoppingUser, _shoppingUser.homeStore.storeMap));
        }

        //<Button Clicked = "SearchStoreItems_Clicked" Text="Search Store Items"/>
        private async  void SearchStoreItems_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SearchItemsPage(_shoppingUser));
        }

        //<Button Clicked = "ViewStoreWebsite_Clicked" Text="View Store Website"/>
        private async void ViewStoreWebsite_Clicked()
        {
            await DisplayAlert($"Visit {_shoppingUser.homeStore.website}", $"View {_shoppingUser.homeStore.name}'s website", "OK");
            Uri storeWebsiteUri = new Uri("http://"+_shoppingUser.homeStore.website);
            Device.OpenUri(storeWebsiteUri);
        }

        //<Button Clicked = "CallStore_Clicked" Text="Call Store"/>
        private async void CallStore_Clicked()
        {
            await DisplayAlert($"Call {_shoppingUser.homeStore.phoneNumber}", $"Calling {_shoppingUser.homeStore.name}'s", "CALL");

            Uri callStoreUri = new Uri($"tel:{_shoppingUser.homeStore.phoneNumber}");

            Device.OpenUri(callStoreUri);
        }


    }
}